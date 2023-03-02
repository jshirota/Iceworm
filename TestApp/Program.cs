using ArcGIS.Core.Geometry;
using Iceworm;
using System.Data;

using var context = new FeatureClass<Airport>("Sample.geodatabase", "airport_pt");

foreach (var airport in context.OrderBy(x => x.Name_e).Query())
{
    Console.WriteLine(airport);
}

record Airport(
    int ObjectID
    , string Name_e
    //, MapPoint Shape
    );
