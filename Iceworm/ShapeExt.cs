using ArcGIS.Core.Geometry;

namespace Iceworm;

public static class ShapeExt
{
    private static readonly IGeometryEngine ge = GeometryEngine.Instance;

    public static Polyline AccelerateForRelationalOperations(this Polyline geometry)
        => (Polyline)ge.AccelerateForRelationalOperations(geometry);

    public static Polygon AccelerateForRelationalOperations(this Polygon geometry)
        => (Polygon)ge.AccelerateForRelationalOperations(geometry);

    public static double Area(this Polygon geometry)
        => ge.Area(geometry);

    public static Polygon AutoComplete(this Polygon geometry, Polyline completionLine)
        => ge.AutoComplete(geometry, completionLine);

    public static Polyline Boundary(this Envelope geometry)
        => (Polyline)ge.Boundary(geometry);

    public static Multipoint Boundary(this Polyline geometry)
        => (Multipoint)ge.Boundary(geometry);

    public static Polyline Boundary(this Polygon geometry)
        => (Polyline)ge.Boundary(geometry);

    public static Polygon Buffer(this Geometry geometry, double distance)
        => (Polygon)ge.Buffer(geometry, distance);

    public static Multipart CalculateNonSimpleMs(this Multipart geometry, double defaultMValue)
        => ge.CalculateNonSimpleMs(geometry, defaultMValue);

    public static Multipart CalculateNonSimpleZs(this Multipart geometry, double defaultZValue)
        => ge.CalculateNonSimpleZs(geometry, defaultZValue);

    public static Multipart CalibrateByMs(this Multipart geometry, IEnumerable<MapPoint> points, UpdateMMethod updateMMethod, double cutOffDistance)
        => ge.CalibrateByMs(geometry, points, updateMMethod, cutOffDistance);

    public static Envelope CenterAt(this Envelope geometry, double x, double y)
        => ge.CenterAt(geometry, x, y);

    public static MapPoint Centroid(this Geometry geometry)
        => ge.Centroid(geometry);

    public static T Clip<T>(this T geometry, Envelope envelope) where T : Geometry
        => (T)ge.Clip(geometry, envelope);

    public static Polyline ConstructGeodeticLineFromDistance(this MapPoint fromPoint, GeodeticCurveType geodeticCurveType, double length, double azimuth, LinearUnit linearUnit, CurveDensifyMethod densifyMode, double densifyParameter)
        => ge.ConstructGeodeticLineFromDistance(geodeticCurveType, fromPoint, length, azimuth, linearUnit, densifyMode, densifyParameter);

    public static Polyline ConstructGeodeticLineFromPoints(this MapPoint fromPoint, GeodeticCurveType geodeticCurveType, MapPoint toPoint, LinearUnit linearUnit, CurveDensifyMethod densifyMode, double densifyParameter)
        => ge.ConstructGeodeticLineFromPoints(geodeticCurveType, fromPoint, toPoint, linearUnit, densifyMode, densifyParameter);

    public static Multipatch ConstructMultipatchExtrude(this Multipart geometry, double zOffset)
        => ge.ConstructMultipatchExtrude(geometry, zOffset);

    public static Multipatch ConstructMultipatchExtrudeAlongLine(this Multipart geometry, Coordinate3D fromCoordinate, Coordinate3D toCoordinate)
        => ge.ConstructMultipatchExtrudeAlongLine(geometry, fromCoordinate, toCoordinate);

    public static Multipatch ConstructMultipatchExtrudeAlongVector3D(this Multipart geometry, Coordinate3D coordinate)
        => ge.ConstructMultipatchExtrudeAlongVector3D(geometry, coordinate);

    public static Multipatch ConstructMultipatchExtrudeFromToZ(this Multipart geometry, double fromZ, double toZ)
        => ge.ConstructMultipatchExtrudeFromToZ(geometry, fromZ, toZ);

    public static Multipatch ConstructMultipatchExtrudeToZ(this Multipart geometry, double toZ)
        => ge.ConstructMultipatchExtrudeToZ(geometry, toZ);

    public static MapPoint ConstructPointFromAngleDistance(this MapPoint geometry, double angle, double distance, SpatialReference? spatialReference = null)
        => ge.ConstructPointFromAngleDistance(geometry, angle, distance, spatialReference);

    public static IReadOnlyList<Polygon> ConstructPolygonsFromPolylines(this IEnumerable<Polyline> polylines)
        => ge.ConstructPolygonsFromPolylines(polylines);

    public static bool Contains(this Geometry geometry, Geometry geometry2)
        => ge.Contains(geometry, geometry2);

    public static Polygon ConvexHull(this Geometry geometry)
        => (Polygon)ge.ConvexHull(geometry);

    public static bool Crosses(this Geometry geometry, Geometry geometry2)
        => ge.Crosses(geometry, geometry2);

    public static IReadOnlyList<T> Cut<T>(this T geometry, Polyline cutter) where T : Multipart
        => ge.Cut(geometry, cutter).Cast<T>().ToList();

    public static T DensifyByAngle<T>(this Multipart geometry, double maxAngleDeviation) where T : Multipart
        => (T)ge.DensifyByAngle(geometry, maxAngleDeviation);

    public static T DensifyByDeviation<T>(this T geometry, double maxDeviationMeters) where T : Multipart
        => (T)ge.DensifyByDeviation(geometry, maxDeviationMeters);

    public static T DensifyByLength<T>(this T geometry, double maxSegmentLength) where T : Multipart
        => (T)ge.DensifyByLength(geometry, maxSegmentLength);

    public static T DensifyByLength3D<T>(this T geometry, double maxSegmentLength) where T : Multipart
        => (T)ge.DensifyByLength3D(geometry, maxSegmentLength);

    public static T Difference<T>(this T geometry, T geometry2) where T : Geometry
        => (T)ge.Difference(geometry, geometry2);

    public static bool Disjoint(this Geometry geometry, Geometry geometry2)
        => ge.Disjoint(geometry, geometry2);

    public static bool Disjoint3D(this Geometry geometry, Geometry geometry2)
        => ge.Disjoint3D(geometry, geometry2);

    public static double Distance(this Geometry geometry, Geometry geometry2)
        => ge.Distance(geometry, geometry2);

    public static double Distance3D(this Geometry geometry, Geometry geometry2)
        => ge.Distance3D(geometry, geometry2);

    public static bool Equals2(this Geometry geometry, Geometry geometry2)
        => ge.Equals(geometry, geometry2);

    public static Envelope Expand(this Envelope geometry, double dx, double dy, bool asRatio)
        => ge.Expand(geometry, dx, dy, asRatio);

    public static Envelope Expand(this Envelope geometry, double dx, double dy, double dz, bool asRatio)
        => ge.Expand(geometry, dx, dy, dz, asRatio);

    public static byte[] ExportToEsriShape(this Geometry geometry, EsriShapeExportFlags exportFlags = EsriShapeExportFlags.EsriShapeExportDefaults)
        => ge.ExportToEsriShape(exportFlags, geometry);

    public static string ExportToJson(this Geometry geometry, JsonExportFlags exportFlags = JsonExportFlags.JsonExportDefaults)
        => ge.ExportToJson(exportFlags, geometry);

    public static byte[] ExportToWKB(this Geometry geometry, WkbExportFlags exportFlags = WkbExportFlags.WkbExportDefaults)
        => ge.ExportToWKB(exportFlags, geometry);

    public static string ExportToWKT(this Geometry geometry, WktExportFlags exportFlags = WktExportFlags.WktExportDefaults)
        => ge.ExportToWKT(exportFlags, geometry);

    public static Polyline Extend(this Polyline geometry, Polyline extender, ExtendFlags extendFlags)
        => ge.Extend(geometry, extender, extendFlags);

    public static T Generalize<T>(this Geometry geometry, double maxDeviation, bool removeDegenerateParts = false, bool preserveCurves = false) where T : Geometry
        => (T)ge.Generalize(geometry, maxDeviation, removeDegenerateParts, preserveCurves);

    public static T Generalize3D<T>(this Geometry geometry, double maxDeviation) where T : Geometry
        => (T)ge.Generalize3D(geometry, maxDeviation);

    public static double GeodesicArea(this Polygon geometry)
        => ge.GeodesicArea(geometry);

    public static Polygon GeodesicBuffer(this Geometry geometry, double distance)
        => (Polygon)ge.GeodesicBuffer(geometry, distance);

    public static Polygon GeodesicBuffer(this Geometry geometry, double distance, LinearUnit distanceUnit)
        => (Polygon)ge.GeodesicBuffer(geometry, distance, distanceUnit);

    public static Polygon GeodesicBuffer(this IEnumerable<Geometry> geometries, double distance)
        => (Polygon)ge.GeodesicBuffer(geometries, distance);

    public static Polygon GeodesicBuffer(this IEnumerable<Geometry> geometries, double distance, LinearUnit distanceUnit)
        => (Polygon)ge.GeodesicBuffer(geometries, distance, distanceUnit);

    public static double GeodesicDistance(this Geometry geometry, Geometry geometry2)
        => ge.GeodesicDistance(geometry, geometry2);

    public static double GeodesicDistance(this Geometry geometry, Geometry geometry2, LinearUnit distanceUnit)
        => ge.GeodesicDistance(geometry, geometry2, distanceUnit);

    public static Geometry GeodesicEllipse(this SpatialReference spatialReference, GeodesicEllipseParameter parameter)
        => ge.GeodesicEllipse(parameter, spatialReference);

    public static double GeodesicLength(this Geometry geometry)
        => ge.GeodesicLength(geometry);

    public static double GeodesicLength(this Geometry geometry, LinearUnit outputUnit)
        => ge.GeodesicLength(geometry, outputUnit);

    public static Geometry GeodesicSector(this SpatialReference spatialReference, GeodesicSectorParameter parameter)
        => ge.GeodesicSector(parameter, spatialReference);

    public static T GeodeticDensifyByDeviation<T>(this T geometry, double maxDeviation, LinearUnit deviationUnit, GeodeticCurveType curveType) where T : Multipart
        => (T)ge.GeodeticDensifyByDeviation(geometry, maxDeviation, deviationUnit, curveType);

    public static T GeodeticDensifyByLength<T>(this T geometry, double maxSegmentLength, LinearUnit lengthUnit, GeodeticCurveType curveType) where T : Multipart
        => (T)ge.GeodeticDensifyByLength(geometry, maxSegmentLength, lengthUnit, curveType);

    public static double GeodeticDistanceAndAzimuth(this MapPoint geometry, MapPoint point2, GeodeticCurveType curveType, out double azimuth12, out double azimuth21)
        => ge.GeodeticDistanceAndAzimuth(geometry, point2, curveType, out azimuth12, out azimuth21);

    public static double GeodeticDistanceAndAzimuth(this MapPoint geometry, MapPoint point2, GeodeticCurveType curveType, LinearUnit distanceUnit, out double azimuth12, out double azimuth21)
        => ge.GeodeticDistanceAndAzimuth(geometry, point2, curveType, distanceUnit, out azimuth12, out azimuth21);

    public static IReadOnlyList<MapPoint> GeodeticMove(this IEnumerable<MapPoint> points, SpatialReference spatialReference, double distance, LinearUnit distanceUnit, double azimuth, GeodeticCurveType curveType)
        => ge.GeodeticMove(points, spatialReference, distance, distanceUnit, azimuth, curveType);

    public static int GetEsriShapeSize(this Geometry geometry, EsriShapeExportFlags exportFlags)
        => ge.GetEsriShapeSize(exportFlags, geometry);

    public static void GetMinMaxM(this Multipart geometry, out double minM, out double maxM)
        => ge.GetMinMaxM(geometry, out minM, out maxM);

    public static MonotonicType GetMMonotonic(this Multipart geometry)
        => ge.GetMMonotonic(geometry);

    public static void GetMsAtDistance(this Multipart geometry, double distance, AsRatioOrLength asRatioOrLength, out double mValue1, out double mValue2)
        => ge.GetMsAtDistance(geometry, distance, asRatioOrLength, out mValue1, out mValue2);

    public static Polyline GetNormalsAtM(this Multipart geometry, double mValue, double length)
        => ge.GetNormalsAtM(geometry, mValue, length);

    public static Multipoint GetPointsAtM(this Multipart geometry, double mValue, double offset)
        => ge.GetPointsAtM(geometry, mValue, offset);

    public static Polyline GetSubCurve(this Multipart geometry, double fromDistance, double toDistance, AsRatioOrLength asRatioOrLength)
        => ge.GetSubCurve(geometry, fromDistance, toDistance, asRatioOrLength);

    public static Polyline GetSubCurve3D(this Multipart geometry, double fromDistance, double toDistance, AsRatioOrLength asRatioOrLength)
        => ge.GetSubCurve3D(geometry, fromDistance, toDistance, asRatioOrLength);

    public static Polyline GetSubCurveBetweenMs(this Multipart geometry, double fromM, double toM)
        => ge.GetSubCurveBetweenMs(geometry, fromM, toM);

    public static int GetWKBSize(this Geometry geometry, WkbExportFlags exportFlags)
        => ge.GetWKBSize(exportFlags, geometry);

    public static Polygon GraphicBuffer(this Geometry geometry, double distance, LineJoinType joinType, LineCapType capType, double miterLimit, double maxDeviation, int maxVerticesInFullCircle)
        => (Polygon)ge.GraphicBuffer(geometry, distance, joinType, capType, miterLimit, maxDeviation, maxVerticesInFullCircle);

    public static IReadOnlyList<Polygon> GraphicBuffer(this IEnumerable<Geometry> geometries, double distance, LineJoinType joinType, LineCapType capType, double miterLimit, double maxDeviation, int maxVerticesInFullCircle)
        => ge.GraphicBuffer(geometries, distance, joinType, capType, miterLimit, maxDeviation, maxVerticesInFullCircle).Cast<Polygon>().ToList();

    public static Geometry ImportFromEsriShape(this byte[] buffer, SpatialReference spatialReference, EsriShapeImportFlags importFlags = EsriShapeImportFlags.EsriShapeImportDefaults)
        => ge.ImportFromEsriShape(importFlags, buffer, spatialReference);

    public static Geometry ImportFromJson(this string jsonString, JsonImportFlags importFlags = JsonImportFlags.JsonImportDefaults)
        => ge.ImportFromJson(importFlags, jsonString);

    public static Geometry ImportFromWKB(this byte[] buffer, SpatialReference spatialReference, WkbImportFlags importFlags = WkbImportFlags.WkbImportDefaults)
        => ge.ImportFromWKB(importFlags, buffer, spatialReference);

    public static Geometry ImportFromWKT(this string wktString, SpatialReference spatialReference, WktImportFlags importFlags = WktImportFlags.WktImportDefaults)
        => ge.ImportFromWKT(importFlags, wktString, spatialReference);

    public static T InsertMAtDistance<T>(this T geometry, double mValue, double distance, AsRatioOrLength asRatioOrLength, bool createNewPart, out bool splitHappened, out int partIndex, out int segmentIndex) where T : Multipart
        => (T)ge.InsertMAtDistance(geometry, mValue, distance, asRatioOrLength, createNewPart, out splitHappened, out partIndex, out segmentIndex);

    public static T InterpolateMsBetween<T>(this T geometry, int fromPart, int fromPoint, int toPart, int toPoint) where T : Multipart
        => (T)ge.InterpolateMsBetween(geometry, fromPart, fromPoint, toPart, toPoint);

    public static Geometry Intersection(this Geometry geometry, Geometry geometry2)
        => ge.Intersection(geometry, geometry2);

    public static Geometry Intersection(this Geometry geometry, Geometry geometry2, GeometryDimensionType resultDimensionType)
        => ge.Intersection(geometry, geometry2, resultDimensionType);

    public static bool Intersects(this Geometry geometry, Geometry geometry2)
        => ge.Intersects(geometry, geometry2);

    public static bool IsSimpleAsFeature(this Geometry geometry, bool forceIsSimple = false)
        => ge.IsSimpleAsFeature(geometry, forceIsSimple);

    public static Geometry LabelPoint(this Geometry geometry)
        => ge.LabelPoint(geometry);

    public static double Length(this Geometry geometry)
        => ge.Length(geometry);

    public static double Length3D(this Geometry geometry)
        => ge.Length3D(geometry);

    public static T Move<T>(this T geometry, double dx, double dy) where T : Geometry
        => (T)ge.Move(geometry, dx, dy);

    public static T Move<T>(this T geometry, double dx, double dy, double dz) where T : Geometry
        => (T)ge.Move(geometry, dx, dy, dz);

    public static MapPoint MovePointAlongLine(this Multipart geometry, double distanceAlong, bool asRatio, double offset, SegmentExtensionType extensionType)
        => ge.MovePointAlongLine(geometry, distanceAlong, asRatio, offset, extensionType);

    public static IReadOnlyList<Geometry> MultipartToSinglePart(this Geometry geometry)
        => ge.MultipartToSinglePart(geometry);

    public static ProximityResult NearestPoint(this Geometry geometry, MapPoint point)
        => ge.NearestPoint(geometry, point);

    public static ProximityResult NearestPoint3D(this Geometry geometry, MapPoint point)
        => ge.NearestPoint3D(geometry, point);

    public static ProximityResult NearestVertex(this Geometry geometry, MapPoint point)
        => ge.NearestVertex(geometry, point);

    public static T NormalizeCentralMeridian<T>(this T geometry) where T : Geometry
        => (T)ge.NormalizeCentralMeridian(geometry);

    public static T Offset<T>(this T geometry, double distance, OffsetType offsetType, double bevelRatio) where T : Geometry
        => (T)ge.Offset(geometry, distance, offsetType, bevelRatio);

    public static bool Overlaps(this Geometry geometry, Geometry geometry2)
        => ge.Overlaps(geometry, geometry2);

    public static T Project<T>(this T geometry, SpatialReference spatialReference) where T : Geometry
        => (T)ge.Project(geometry, spatialReference);

    public static T Project<T>(this T geometry, int wkid) where T : Geometry
        => geometry.Project(SpatialReferenceBuilder.CreateSpatialReference(wkid));

    public static T ProjectEx<T>(this T geometry, ProjectionTransformation projTransformation) where T : Geometry
        => (T)ge.ProjectEx(geometry, projTransformation);

    public static Polyline QueryNormal(this Multipart geometry, SegmentExtensionType extensionType, double distanceAlongCurve, AsRatioOrLength asRatioOrLength, double normalLength)
        => ge.QueryNormal(geometry, extensionType, distanceAlongCurve, asRatioOrLength, normalLength);

    public static LineSegment QueryNormal(this Segment geometry, SegmentExtensionType extensionType, double distanceAlongCurve, AsRatioOrLength asRatioOrLength, double normalLength)
        => ge.QueryNormal(geometry, extensionType, distanceAlongCurve, asRatioOrLength, normalLength);

    public static MapPoint QueryPoint(this Multipart geometry, SegmentExtensionType extensionType, double distanceAlongCurve, AsRatioOrLength asRatioOrLength)
        => ge.QueryPoint(geometry, extensionType, distanceAlongCurve, asRatioOrLength);

    public static MapPoint QueryPoint(this Segment geometry, SegmentExtensionType extensionType, double distanceAlongCurve, AsRatioOrLength asRatioOrLength)
        => ge.QueryPoint(geometry, extensionType, distanceAlongCurve, asRatioOrLength);

    public static MapPoint QueryPointAndDistance(this Multipart geometry, SegmentExtensionType extensionType, MapPoint inPoint, AsRatioOrLength asRatioOrLength, out double distanceAlongCurve, out double distanceFromCurve, out LeftOrRightSide whichSide)
        => ge.QueryPointAndDistance(geometry, extensionType, inPoint, asRatioOrLength, out distanceAlongCurve, out distanceFromCurve, out whichSide);

    public static MapPoint QueryPointAndDistance(this Segment geometry, SegmentExtensionType extensionType, MapPoint inPoint, AsRatioOrLength asRatioOrLength, out double distanceAlongCurve, out double distanceFromCurve, out LeftOrRightSide whichSide)
        => ge.QueryPointAndDistance(geometry, extensionType, inPoint, asRatioOrLength, out distanceAlongCurve, out distanceFromCurve, out whichSide);

    public static MapPoint QueryPointAndDistance3D(this Multipart geometry, SegmentExtensionType extensionType, MapPoint inPoint, AsRatioOrLength asRatioOrLength, out double distanceAlongCurve, out double distanceFromCurve)
        => ge.QueryPointAndDistance3D(geometry, extensionType, inPoint, asRatioOrLength, out distanceAlongCurve, out distanceFromCurve);

    public static MapPoint QueryPointAndDistance3D(this Segment geometry, SegmentExtensionType extensionType, MapPoint inPoint, AsRatioOrLength asRatioOrLength, out double distanceAlongCurve, out double distanceFromCurve)
        => ge.QueryPointAndDistance3D(geometry, extensionType, inPoint, asRatioOrLength, out distanceAlongCurve, out distanceFromCurve);

    public static Polyline QueryTangent(this Multipart geometry, SegmentExtensionType extensionType, double distanceAlongCurve, AsRatioOrLength asRatioOrLength, double tangentLength)
        => ge.QueryTangent(geometry, extensionType, distanceAlongCurve, asRatioOrLength, tangentLength);

    public static LineSegment QueryTangent(this Segment geometry, SegmentExtensionType extensionType, double distanceAlongCurve, AsRatioOrLength asRatioOrLength, double tangentLength)
        => ge.QueryTangent(geometry, extensionType, distanceAlongCurve, asRatioOrLength, tangentLength);

    public static T ReflectAboutLine<T>(this Geometry geometry, LineSegment reflectionLine) where T : Geometry
        => (T)ge.ReflectAboutLine(geometry, reflectionLine);

    public static bool Relate(this Geometry geometry, Geometry geometry2, string relateString)
        => ge.Relate(geometry, geometry2, relateString);

    public static T ReplaceNaNZs<T>(this T geometry, double zValue) where T : Geometry
        => (T)ge.ReplaceNaNZs(geometry, zValue);

    public static T Reshape<T>(this T geometry, Polyline reshaper) where T : Multipart
        => (T)ge.Reshape(geometry, reshaper);

    public static T ReverseOrientation<T>(this T geometry) where T : Multipart
        => (T)ge.ReverseOrientation(geometry);

    public static T Rotate<T>(this T geometry, MapPoint origin, double rotationAngle) where T : Geometry
        => (T)ge.Rotate(geometry, origin, rotationAngle);

    public static T Scale<T>(this T geometry, MapPoint origin, double sx, double sy) where T : Geometry
        => (T)ge.Scale(geometry, origin, sx, sy);

    public static T SetAndInterpolateMsBetween<T>(this T geometry, double fromM, double toM) where T : Multipart
        => (T)ge.SetAndInterpolateMsBetween(geometry, fromM, toM);

    public static T SetConstantZ<T>(this T geometry, double zValue) where T : Multipart
        => (T)ge.SetConstantZ(geometry, zValue);

    public static T SetMsAsDistance<T>(this T geometry, AsRatioOrLength asRatioOrLength) where T : Multipart
        => (T)ge.SetMsAsDistance(geometry, asRatioOrLength);

    public static double ShapePreservingArea(this Geometry geometry)
        => ge.ShapePreservingArea(geometry);

    public static double ShapePreservingArea(this Geometry geometry, AreaUnit areaUnit)
        => ge.ShapePreservingArea(geometry, areaUnit);

    public static double ShapePreservingLength(this Geometry geometry)
        => ge.ShapePreservingLength(geometry);

    public static double ShapePreservingLength(this Geometry geometry, LinearUnit lengthUnit)
        => ge.ShapePreservingLength(geometry, lengthUnit);

    public static Polygon SideBuffer(this Polyline geometry, double distance, LeftOrRightSide side, LineCapType capType)
        => (Polygon)ge.SideBuffer(geometry, distance, side, capType);

    public static IReadOnlyList<Polygon> SideBuffer(this IEnumerable<Polyline> geometries, double distance, LeftOrRightSide side, LineCapType capType)
        => ge.SideBuffer(geometries, distance, side, capType).Cast<Polygon>().ToList();

    public static T SimplifyAsFeature<T>(this T geometry, bool forceSimplify = false) where T : Geometry
        => (T)ge.SimplifyAsFeature(geometry, forceSimplify);

    public static Polyline SimplifyPolyline(this Polyline geometry, SimplifyType simplifyType, bool forceSimplify)
        => ge.SimplifyPolyline(geometry, simplifyType, forceSimplify);

    public static IReadOnlyList<Polygon> SlicePolygonIntoEqualParts(this Polygon geometry, int numParts, double angle, SliceType sliceType)
        => ge.SlicePolygonIntoEqualParts(geometry, numParts, angle, sliceType);

    public static T SplitAtPoint<T>(this T geometry, MapPoint splitPoint, bool projectOnto, bool createPart, out bool splitOccurred, out int partIndex, out int segmentIndex) where T : Multipart
        => (T)ge.SplitAtPoint(geometry, splitPoint, projectOnto, createPart, out splitOccurred, out partIndex, out segmentIndex);

    public static T SymmetricDifference<T>(this T geometry, T geometry2) where T : Geometry
        => (T)ge.SymmetricDifference(geometry, geometry2);

    public static bool Touches(this Geometry geometry, Geometry geometry2)
        => ge.Touches(geometry, geometry2);

    public static int Transform2D(this Coordinate2D[] inCoordinates, ProjectionTransformation projectionTransformation, ref Coordinate2D[] outCoordinates, bool removeClippedCoordinates = true)
        => ge.Transform2D(inCoordinates, projectionTransformation, ref outCoordinates, removeClippedCoordinates);

    public static int Transform3D(this Coordinate3D[] inCoordinates, ProjectionTransformation projectionTransformation, ref Coordinate3D[] outCoordinates, bool removeClippedCoordinates = true)
        => ge.Transform3D(inCoordinates, projectionTransformation, ref outCoordinates, removeClippedCoordinates);

    public static T Union<T>(this T geometry, T geometry2) where T : Geometry
        => (T)ge.Union(geometry, geometry2);

    public static bool Within(this Geometry geometry, Geometry geometry2)
        => ge.Within(geometry, geometry2);
}
