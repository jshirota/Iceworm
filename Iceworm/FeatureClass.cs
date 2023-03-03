using ArcGIS.Core.Data;
using ArcGIS.Core.Geometry;
using ArcGIS.Core.Hosting;
using System.Reflection;
using System.Runtime.Serialization;

namespace Iceworm;

public class FeatureClass<T> : IDisposable
{
    public Geodatabase? Geodatabase => this.datastore as Geodatabase;
    public Table Table { get; }

    internal readonly Mapping mapping = new();
    private static bool hostInitialized;

    private readonly Datastore datastore;
    private readonly SpatialReference? outputSpatialReference;
    private readonly string oidFieldName;
    private readonly List<string> whereClauses = new();
    private readonly List<string> orderByClauses = new();
    private (Geometry filterGeometry, SpatialRelationship spatialRelationship) spatialFilter;

    public FeatureClass(string database, string table, Dictionary<string, string>? propertyNameToFieldName = null, SpatialReference? outputSpatialReference = null, bool initializeHost = true)
    {
        if (!hostInitialized && initializeHost)
        {
            hostInitialized = true;
#pragma warning disable CA1416 // Validate platform compatibility
            Thread.CurrentThread.SetApartmentState(ApartmentState.Unknown);
            Thread.CurrentThread.SetApartmentState(ApartmentState.STA);
#pragma warning restore CA1416 // Validate platform compatibility
            Host.Initialize();
        }

        var databasePath = Path.GetFullPath(database).ToLower();
        var tableName = table.ToLower();

        Datastore GetDatastore()
        {
            var uri = new Uri(databasePath);

            if (databasePath.EndsWith(".gdb"))
                return new Geodatabase(new FileGeodatabaseConnectionPath(uri));

            if (databasePath.EndsWith(".geodatabase"))
                return new Geodatabase(new MobileGeodatabaseConnectionPath(uri));

            if (databasePath.EndsWith(".sde"))
                return new Geodatabase(new DatabaseConnectionFile(uri));

            if (tableName.EndsWith(".shp") || tableName.EndsWith(".dbf") || File.Exists($@"{databasePath}\{tableName}.shp") || File.Exists($@"{databasePath}\{tableName}.dbf"))
                return new FileSystemDatastore(new FileSystemConnectionPath(uri, FileSystemDatastoreType.Shapefile));

            if (tableName.Contains(".dwg"))
                return new FileSystemDatastore(new FileSystemConnectionPath(uri, FileSystemDatastoreType.Cad));

            throw new InvalidOperationException($@"'{databasePath}\{tableName}' is not a supported format.");
        }

        this.datastore = GetDatastore();

        this.Table = this.datastore switch
        {
            FileSystemDatastore fileSystemDatastore => fileSystemDatastore.OpenDataset<Table>(table),
            Geodatabase geodatabase => geodatabase.OpenDataset<Table>(table),

            _ => throw new InvalidOperationException()
        };

        var fields = this.Table.GetDefinition().GetFields().ToDictionary(x => x.Name.ToLower());

        this.outputSpatialReference = outputSpatialReference;
        this.oidFieldName = fields.Values.Single(x => x.FieldType == FieldType.OID).Name;

        foreach (var p in typeof(T).GetProperties())
        {
            var fieldName = propertyNameToFieldName?.ContainsKey(p.Name) == true
                ? propertyNameToFieldName[p.Name].ToLower() : p.Name.ToLower();

            if (!fields.ContainsKey(fieldName))
                throw new InvalidOperationException($"'{fieldName}' was not found in '{this.Table.GetName()}'.");

            this.mapping.LowerCaseFieldName.Add(fieldName, (p, fields[fieldName]));
            this.mapping.PropertyName.Add(p.Name, (p, fields[fieldName]));
        }
    }

    public FeatureClass(string tablePath, Dictionary<string, string>? propertyNameToFieldName = null, SpatialReference? outputSpatialReference = null, bool initializeHost = true)
        : this(Path.GetDirectoryName(Path.GetFullPath(tablePath))!, Path.GetFileName(tablePath), propertyNameToFieldName, outputSpatialReference, initializeHost)
    {
    }

    private FeatureClass(FeatureClass<T> context)
    {
        this.mapping = context.mapping;
        this.datastore = context.datastore;
        this.Table = context.Table;
        this.outputSpatialReference = context.outputSpatialReference;
        this.oidFieldName = context.oidFieldName;
        this.whereClauses = context.whereClauses;
        this.orderByClauses = context.orderByClauses;
        this.spatialFilter = context.spatialFilter;
    }

    private FeatureClass(FeatureClass<T> context, Geometry filterGeometry, SpatialRelationship spatialRelationship) : this(context)
    {
        this.spatialFilter = (filterGeometry, spatialRelationship);
    }

    private FeatureClass(FeatureClass<T> context, string whereClause) : this(context)
    {
        this.whereClauses = this.whereClauses.Concat(new[] { whereClause }).ToList();
    }

    private FeatureClass(FeatureClass<T> context, string orderByField, bool descending) : this(context)
    {
        this.orderByClauses = this.orderByClauses.Concat(new[] { $"{orderByField}{(descending ? " DESC" : "")}" }).ToList();
    }

    public FeatureClass<T> Where(Geometry filterGeometry, SpatialRelationship spatialRelationship = SpatialRelationship.Intersects)
    {
        return new(this, filterGeometry, spatialRelationship);
    }

    public FeatureClass<T> Where(string whereClause)
    {
        return new(this, $"({whereClause})");
    }

    public FeatureClass<T> OrderBy(string field, bool descending = false)
    {
        return new(this, field, descending);
    }

    private IEnumerable<T> IterateCursor(Func<T, T>? edit = null)
    {
        var filter = new SpatialQueryFilter
        {
            SubFields = string.Join(",", this.mapping.LowerCaseFieldName.Keys),
            WhereClause = string.Join(" AND ", this.whereClauses),
            PostfixClause = this.orderByClauses.Any() ? $"ORDER BY {string.Join(",", this.orderByClauses)}" : "",
            OutputSpatialReference = this.outputSpatialReference
        };

        if (this.spatialFilter.filterGeometry is not null)
        {
            filter.FilterGeometry = this.spatialFilter.filterGeometry;
            filter.SpatialRelationship = this.spatialFilter.spatialRelationship;
        }

        using var rowCursor = Table.Search(filter, false);
        while (rowCursor.MoveNext())
        {
            using var row = rowCursor.Current;
            var item = (T)FormatterServices.GetUninitializedObject(typeof(T));

            foreach (var (p, f) in mapping.PropertyName.Values)
            {
                var value = row[f.Name];

                if (value is null)
                    continue;

                try
                {
                    var type = Nullable.GetUnderlyingType(p.PropertyType) ?? p.PropertyType;
                    var convertedValue = Convert.ChangeType(value, type);

                    if (p.SetMethod is null)
                    {
                        var backingField = FindBackingField(p.Name)
                            ?? throw new InvalidOperationException("No backing field found.");

                        backingField.SetValue(item, convertedValue);
                    }
                    else
                    {
                        p.SetValue(item, convertedValue);
                    }
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException($"Could not set {typeof(T)}.{p.Name} to {value}.", ex);
                }
            }

            if (edit is null)
            {
                yield return item;
            }
            else
            {
                var after = edit(item);

                foreach (var (p, f) in this.mapping.PropertyName.Values)
                {
                    if (f.IsEditable)
                        row[f.Name] = p.GetValue(after);
                }

                row.Store();

                yield return after;
            }
        }
    }

    private static FieldInfo? FindBackingField(string propertyName)
    {
        static IEnumerable<Type> GetTypes(Type? type)
        {
            while (true)
            {
                if (type is null)
                    yield break;

                yield return type;
                type = type.BaseType;
            }
        }

        return GetTypes(typeof(T))
            .Select(x => x.GetField($"<{propertyName}>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic))
            .OfType<FieldInfo>()
            .FirstOrDefault();
    }

    public IEnumerable<T> Query()
    {
        return IterateCursor();
    }

    public int[] Insert(IEnumerable<T> items)
    {
        var oids = new List<int>();

        using (var insertCursor = this.Table.CreateInsertCursor())
        {
            using var rowBuffer = this.Table.CreateRowBuffer();
            foreach (var item in items)
            {
                foreach (var (p, f) in this.mapping.PropertyName.Values)
                {
                    if (f.IsEditable)
                        rowBuffer[f.Name] = p.GetValue(item);
                }

                var oid = insertCursor.Insert(rowBuffer);
                oids.Add(Convert.ToInt32(oid));
            }
        }

        return oids.ToArray();
    }

    public T Insert(T item)
    {
        var oids = this.Insert(new[] { item });
        var context = new FeatureClass<T>(this);
        return context.Where($"{this.oidFieldName} = {oids.Single()}").Query().Single();
    }

    public void Update(Func<T, T> edit)
    {
        _ = this.IterateCursor(edit).ToArray();
    }

    public void Update(Action<T> edit)
    {
        _ = this.IterateCursor(x => { edit(x); return x; }).ToArray();
    }

    public void Delete()
    {
        var filter = new SpatialQueryFilter
        {
            WhereClause = string.Join(" AND ", this.whereClauses)
        };

        if (this.spatialFilter.filterGeometry is not null)
        {
            filter.FilterGeometry = this.spatialFilter.filterGeometry;
            filter.SpatialRelationship = this.spatialFilter.spatialRelationship;
        }

        this.Table.DeleteRows(filter);
    }

    public void Dispose()
    {
        this.Table.Dispose();
        this.datastore.Dispose();

        GC.SuppressFinalize(this);
    }
}

internal class Mapping
{
    public Dictionary<string, (PropertyInfo propertyInfo, Field field)> LowerCaseFieldName { get; } = new();
    public Dictionary<string, (PropertyInfo propertyInfo, Field field)> PropertyName { get; } = new();
}
