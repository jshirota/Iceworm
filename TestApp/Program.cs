using ArcGIS.Core.Geometry;
using Iceworm;

using var context = new FeatureClass<Airport>("Sample.geodatabase", "airport_pt");

foreach (var airport in context.OrderBy(x => x.Name_e).Query())
{
    Console.WriteLine($"{airport.Name_e} {airport.Prv_Code}");
}

record Airport(
    int ObjectID
    , string Name_e
    , short Prv_Code
    //, MapPoint Shape
    );

class AirportBase
{
    public short Prv_Code { get; }
}

class Airport2 : AirportBase
{
    public int ObjectID { get; init; }
    public string Name_e { get; private set; } = default!;
}
