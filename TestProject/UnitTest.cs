using ArcGIS.Core.Geometry;
using Iceworm;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProject;

[TestClass]
public class UnitTest
{
    [TestMethod]
    public void Sort1()
    {
        var mapping = new Dictionary<string, string> { { "Name", "name_e" } };
        using var context = new FeatureClass<Airport>("Sample.geodatabase/airport_pt", mapping);

        var query = context
            .Where(x => x.Name.StartsWith("B"))
            .OrderBy(x => x.Name);

        var airports1 = query.Query().ToArray();
        var airports2 = airports1.OrderBy(x => x.Name).ToArray();
        var airports3 = airports1.OrderByDescending(x => x.Name).ToArray();

        Assert.IsTrue(airports1.SequenceEqual(airports2));
        Assert.IsFalse(airports1.SequenceEqual(airports3));
    }

    [TestMethod]
    public void Sort2()
    {
        var mapping = new Dictionary<string, string> { { "Name", "name_e" } };
        using var context = new FeatureClass<Airport>("Sample.geodatabase/airport_pt", mapping);

        var query = context
            .Where(x => x.Name.StartsWith("B"))
            .OrderBy(x => x.ObjectID);

        var airports1 = query.Query().ToArray();
        var airports2 = airports1.OrderBy(x => x.ObjectID).ToArray();
        var airports3 = airports1.OrderByDescending(x => x.ObjectID).ToArray();

        Assert.IsTrue(airports1.SequenceEqual(airports2));
        Assert.IsFalse(airports1.SequenceEqual(airports3));
    }

    [TestMethod]
    public void Wgs84()
    {
        var mapping = new Dictionary<string, string> { { "Name", "name_e" } };
        using var context = new FeatureClass<Airport>("Sample.geodatabase/airport_pt", mapping);

        var query = context
            .OrderBy(x => x.ObjectID);

        using var context2 = new FeatureClass<Airport>("Sample.geodatabase/airport_pt", mapping, SpatialReferenceBuilder.CreateSpatialReference(4326));
        var query4326 = context2
            .OrderBy(x => x.ObjectID);

        var points1 = query.Query().Take(10).Select(x => x.Shape.Project(4326)).ToArray();
        var points2 = query4326.Query().Take(10).Select(x => x.Shape).ToArray();

        foreach (var (p1, p2) in points1.Zip(points2))
        {
            Assert.AreEqual(p1.X, p2.X, 0.01);
            Assert.AreEqual(p1.Y, p2.Y, 0.01);
        }
    }

    [TestMethod]
    public void NotStartsWith()
    {
        var mapping = new Dictionary<string, string> { { "Name", "name_e" } };
        using var context = new FeatureClass<Pipeline>("Sample.geodatabase/piplelines_1", mapping);

        var query = context
            .Where(x => !x.Owner.StartsWith("P"))
            .OrderBy(x => x.Owner);

        foreach (var pipeline in query.Query())
        {
            Assert.IsFalse(pipeline.Owner.StartsWith("P"));
        }
    }

    [TestMethod]
    public void Poly()
    {
        using var context = new FeatureClass<ProtectedArea>("Sample.geodatabase/prot_areas_p");

        var query = context
            .Where(x => x.Name_EN.Contains("Ice"))
            .OrderBy(x => x.Class);

        foreach (var pipeline in query.Query())
        {
            Assert.IsTrue(pipeline.Name_EN.Contains("Ice"));
        }
    }

    [TestMethod]
    public void Wkt()
    {
        using var context = new FeatureClass<ProtectedArea>("Sample.geodatabase/prot_areas_p");

        var query = context
            .OrderBy(x => x.Name_EN);

        foreach (var protectedArea in query.Query())
        {
            var wkt = protectedArea.Shape.ExportToWKT();
            var imported = wkt.ImportFromWKT(protectedArea.Shape.SpatialReference);
            var wkt2 = imported.ExportToWKT();
            Assert.IsTrue(imported.Equals2(protectedArea.Shape));
            Assert.AreEqual(wkt, wkt2);
        }
    }

    [TestMethod]
    public void Json()
    {
        using var context = new FeatureClass<ProtectedArea>("Sample.geodatabase/prot_areas_p");

        var query = context
            .OrderBy(x => x.Name_EN);

        foreach (var protectedArea in query.Query())
        {
            var json = protectedArea.Shape.ExportToJson();
            var imported = json.ImportFromJson();
            var json2 = imported.ExportToJson();
            Assert.IsTrue(imported.Equals2(protectedArea.Shape));
            Assert.AreEqual(json, json2);
        }
    }

    [TestMethod]
    public void Init()
    {
        using var context = new FeatureClass<ProtectedArea>("Sample.geodatabase/prot_areas_p");

        var query = context
            .OrderBy(x => x.Name_EN);
    }

    [TestMethod]
    public void Insert()
    {
        var mapping = new Dictionary<string, string> { { "Name", "name_e" } };
        using var context = new FeatureClass<Airport>("Sample.geodatabase/airport_pt", mapping);

        var airports = context.Query().Take(10).ToArray();

        var oids = context.Insert(airports);

        Assert.AreEqual(10, oids.Distinct().Count());

        var airport1 = context.Insert(airports[0]);
        var airport2 = context.Insert(airports[0]);

        Assert.AreEqual(airport1.ObjectID + 1, airport2.ObjectID);
    }

    [TestMethod]
    public void Update()
    {
        var mapping = new Dictionary<string, string> { { "Name", "name_e" } };
        using var context = new FeatureClass<Airport>("Sample.geodatabase/airport_pt", mapping);

        context.Update(x => x with { Class = "Ottawa" });

        foreach (var airport in context.Query())
        {
            Assert.AreEqual("Ottawa", airport.Class);
        }
    }

    [TestMethod]
    public void Update_Destructive()
    {
        var mapping = new Dictionary<string, string> { { "Name", "name_e" } };
        using var context = new FeatureClass<Airport2>("Sample.geodatabase/airport_pt", mapping);

        context
            .Where(x => x.Name.StartsWith("A"))
            .Update(x => x.Class = $"{x.Name[..5]}!");

        foreach (var airport in context.Query())
        {
            if (airport.Name.StartsWith("A"))
                Assert.AreEqual($"{airport.Name[..5]}!", airport.Class);
        }
    }

    [TestMethod]
    public void Delete()
    {
        var mapping = new Dictionary<string, string> { { "Name", "name_e" } };
        using var context = new FeatureClass<Airport2>("Sample.geodatabase/airport_pt", mapping);

        context.Where(x => x.Name.StartsWith("A")).Delete();

        Assert.AreEqual(0, context.Where(x => x.Name.StartsWith("A")).Query().Count());
    }
}

record Airport(int ObjectID, string Name, string Class, MapPoint Shape);
record Pipeline(int ObjectID, string Owner, string Commod_E, Polyline Shape);
record ProtectedArea(int ObjectID, string Name_EN, int Class, Polygon Shape);

class Airport2
{
    public int ObjectID { get; set; }
    public string Name { get; set; } = default!;
    public string Class { get; set; } = default!;
}
