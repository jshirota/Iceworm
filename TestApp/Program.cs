using ArcGIS.Core.Geometry;
using Iceworm;

using var featureClass = new FeatureClass<City>("world.geodatabase/Cities");

var n = 0;

foreach (var city in featureClass
    .Where(x => x.Pop > 1000000)
    .OrderBy(x => x.Pop, true)
    .Query())
{
    Console.WriteLine($"{++n} {city.City_Name} {city.Pop}");
}

public record City(
    int ObjectID,
    string City_Name,
    int Pop,
    MapPoint Shape);
