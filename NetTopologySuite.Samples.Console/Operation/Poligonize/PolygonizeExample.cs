using System;
using System.Collections.Generic;
using GeoAPI.Geometries;
using GeoAPI.IO.WellKnownText;
using GisSharpBlog.NetTopologySuite.Geometries;
using GisSharpBlog.NetTopologySuite.Operation.Polygonize;
using NetTopologySuite.Coordinates;

namespace GisSharpBlog.NetTopologySuite.Samples.Operation.Poligonize
{
    /// <summary>  
    /// Example of using Polygonizer class to polygonize a set of fully noded linestrings.
    /// </summary>	
    public class PolygonizeExample
    {
        [STAThread]
        public static void main(String[] args)
        {
            PolygonizeExample test = new PolygonizeExample();
            try
            {
                test.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }


        internal virtual void Run()
        {
            IGeometryFactory<BufferedCoordinate> geoFactory =
                new GeometryFactory<BufferedCoordinate>(new BufferedCoordinateSequenceFactory());
            WktReader<BufferedCoordinate> rdr
                = new WktReader<BufferedCoordinate>(geoFactory, null);
            List<IGeometry<BufferedCoordinate>> lines
                = new List<IGeometry<BufferedCoordinate>>();

            // isolated edge
            lines.Add(rdr.Read("LINESTRING (0 0 , 10 10)"));
            lines.Add(rdr.Read("LINESTRING (185 221, 100 100)")); //dangling edge
            lines.Add(rdr.Read("LINESTRING (185 221, 88 275, 180 316)"));
            lines.Add(rdr.Read("LINESTRING (185 221, 292 281, 180 316)"));
            lines.Add(rdr.Read("LINESTRING (189 98, 83 187, 185 221)"));
            lines.Add(rdr.Read("LINESTRING (189 98, 325 168, 185 221)"));

            Polygonizer<BufferedCoordinate> polygonizer
                = new Polygonizer<BufferedCoordinate>();
            polygonizer.Add(lines);

            IList<IPolygon<BufferedCoordinate>> polys = polygonizer.Polygons;

            Console.WriteLine("Polygons formed (" + polys.Count + "):");
            foreach (object obj in polys)
            {
                Console.WriteLine(obj);
            }
        }
    }
}