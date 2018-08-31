using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Drawing;
using System.IO;

namespace TileDLStitch
{
    class Program
    {

        private const double EarthRadius = 6378137;
        private const double MinLatitude = -85.05112878;
        private const double MaxLatitude = 85.05112878;
        private const double MinLongitude = -180;
        private const double MaxLongitude = 180;
        static int tileSize = 256;
        static double iRes = 2 * Math.PI * 6378137 / tileSize;
        static double oShift = 2 * Math.PI * 6378137 / 2.0;
        static int zoom = 20;
        static int mapSize = Convert.ToInt32(tileSize * Math.Pow(2, zoom));
        static bool ratioSwap;

        static void Main(string[] args)
        {

            Point one = new Point(34.09318, -84.18273);
            Point two = new Point(34.09158, -84.17739);

            Extent ex = new Extent(one, two);

            // Useless - 8/21/2018
            //ex = RatioExtent(ex, 13, 8, out ratioSwap);

            TList<Tile> myTiles = GetTiles(ex, zoom);

            if(myTiles.Count > 2000)
            {
                Console.Write("Query too large!");
            }
            else
            {
                string token = "";

                DownloadTiles(myTiles, token);

                Bitmap combined = CombineTiles(myTiles);
                combined.Save(String.Format(@"C:\temp\images\final_{0}.jpg", DateTime.Now.Ticks), System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }

        static Extent RatioExtent(Extent originalExt, int ratioWidth, int ratioHeight, out bool isSwap)
        {
            double originalWidth = (originalExt.UL.X * -1) - (originalExt.BR.X * -1);
            double originalHeight = originalExt.UL.Y - originalExt.BR.Y;

            double workingHeight, workingWidth;

            if (originalHeight > originalWidth)
            { 
                isSwap = true;
                workingHeight = originalWidth;
                workingWidth = originalHeight;    
            }
            else
            {
                isSwap = false;
                workingWidth = originalWidth;
                workingHeight = originalHeight;
            }

            double newSideLength = workingWidth / ratioWidth * ratioHeight;
            double newSideDelta = (newSideLength - workingHeight) / 2;

            if(isSwap)
            {
                originalExt.UL.X = (originalExt.UL.X - newSideDelta);
                originalExt.BR.X = (originalExt.BR.X + newSideDelta);
            }
            else
            {
                originalExt.UL.Y = originalExt.UL.Y + newSideDelta;
                originalExt.BR.Y = originalExt.BR.Y - newSideDelta;
            }

            return originalExt;
        }


        static Point MetersToPixels(Point p, int zoomLevel)
        {
            double res = iRes / (2 * zoomLevel);
            p.X = (p.X + oShift) / res;
            p.Y = (p.Y + oShift) / res;

            return p;
        }

        static Point LatLonToMeters(Point p)
        {
            p.X = p.X * oShift / 180;
            p.Y = Math.Log(Math.Tan((90 + p.Y) * Math.PI / 360)) / (Math.PI / 180);
            p.Y = p.Y * oShift / 180;

            return p;
        }

        static void DownloadTiles(TList<Tile> tiles, string token)
        {
            foreach (Tile t in tiles)
            {
                try
                {
                    var c = new WebClient();
                    c.DownloadFile(new Uri(String.Format("https://tileserver/server/rest/services/Hosted/tileservicename/MapServer/tile/{0}/{1}/{2}?blankTile=true&token={3}&j={4}-{5}-{6}", t.Zoom, t.Col, t.Row, token, t.Zoom, t.ImageCol, t.ImageRow)), String.Format(@"C:\temp\images\{0}", t.FileName));
                }
                catch
                {
                    Bitmap b = new Bitmap(256, 256);
                    b.Save(String.Format(@"C:\temp\images\{0}", t.FileName), System.Drawing.Imaging.ImageFormat.Jpeg);
                }
            }
        }

        public static Bitmap CombineTiles(TList<Tile> tileList)
        {
            Bitmap finalImage = null;

            int tileH = 0, tileW = 0;

            foreach(Tile t in tileList)
            {
                t.Image = new Bitmap(String.Format(@"C:\temp\images\{0}", t.FileName));

                tileH = t.Image.Height;
                tileW = t.Image.Width;    
            }

            finalImage = new Bitmap(tileW * tileList.Rows, tileH * tileList.Columns);

            using (Graphics g = Graphics.FromImage(finalImage))
            {
                int rowOffset = 0, colOffset = 0;
                
                foreach(Tile t in tileList)
                {
                    rowOffset = tileH * t.ImageCol;
                    colOffset = tileW * t.ImageRow;
                    g.DrawImage(t.Image, new Rectangle(colOffset, rowOffset, tileW, tileH));
                    t.Image.Dispose();
                    File.Delete(String.Format(@"C:\temp\images\{0}", t.FileName));
                }
            }
            return finalImage;
        }

        public class TList<Type> : List<Type>
        {
            public int Rows { get; set; }
            public int Columns { get; set; }
        }


        static public TList<Tile> GetTiles(Extent ex, int zL)
        {
            TList<Tile> output = new TList<Tile>();

            Tile upperLeft = GetTile(ex.UL, zL, -1, -1);
            Tile bottomRight = GetTile(ex.BR, zL, +1, +1);

            List<int> rows = Enumerable.Range(upperLeft.Row, bottomRight.Row - upperLeft.Row).ToList();
            List<int> cols = Enumerable.Range(upperLeft.Col, bottomRight.Col - upperLeft.Col).ToList();

            output.Rows = rows.Count();
            output.Columns = cols.Count();

            foreach(int c in cols)
            {
                foreach(int r in rows)
                {
                    output.Add(new Tile(r, c, zL, rows.IndexOf(r), cols.IndexOf(c)));
                }
            }

            if(output.Count() > 2000)
            {
                Console.WriteLine("TOO BIG");
                output = GetTiles(ex, zL - 1);
            }

            return output;
        }

        static public Tile GetTile(Point p, int zL, int xMod, int yMod)
        {
            double _lat = p.Y;
            double _lon = p.X;
            int _zoom = zL;

            double _latRad = _lat * Math.PI / 180;

            double _n = Math.Pow(2, _zoom);

            int xTile = Convert.ToInt32(Math.Floor(_n * ((_lon + 180) / 360)));
            int yTile = Convert.ToInt32(Math.Floor(_n * (1 - (Math.Log(Math.Tan(_latRad) + 1 / Math.Cos(_latRad)) / Math.PI)) / 2));

            Console.WriteLine(String.Format("r: {0} c: {1} z: {2}", xTile, yTile, zL));

            return new Tile(xTile + xMod, yTile + yMod, zL, 0, 0);
        }

        public static void PixelXYToTileXY(int pixelX, int pixelY, out int tileX, out int tileY)
        {
            tileX = pixelX / 256;
            tileY = pixelY / 256;
        }

        public static void TileXYToPixelXY(int tileX, int tileY, out int pixelX, out int pixelY)
        {
            pixelX = tileX * 256;
            pixelY = tileY * 256;
        }

        public static void PixelXYToLatLong(int pixelX, int pixelY, int levelOfDetail, out double latitude, out double longitude)
        {
            double mapSize = MapSize(levelOfDetail);
            double x = (Clip(pixelX, 0, mapSize - 1) / mapSize) - 0.5;
            double y = 0.5 - (Clip(pixelY, 0, mapSize - 1) / mapSize);

            latitude = 90 - 360 * Math.Atan(Math.Exp(-y * 2 * Math.PI)) / Math.PI;
            longitude = 360 * x;
        }

        public static uint MapSize(int levelOfDetail)
        {
            return (uint)256 << levelOfDetail;
        }

        public static double MapScale(double latitude, int levelOfDetail, int screenDpi)
        {
            return GroundResolution(latitude, levelOfDetail) * screenDpi / 0.0254;
        }

        public static double GroundResolution(double latitude, int levelOfDetail)
        {
            latitude = Clip(latitude, MinLatitude, MaxLatitude);
            return Math.Cos(latitude * Math.PI / 180) * 2 * Math.PI * EarthRadius / MapSize(levelOfDetail);
        }

        private static double Clip(double n, double minValue, double maxValue)
        {
            return Math.Min(Math.Max(n, minValue), maxValue);
        }
    }



    public class Tile
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public int ImageRow { get; set; }
        public int ImageCol { get; set; }
        public int Zoom { get; set; }
        public Bitmap Image { get; set; }

        public string FileName
        {
            get
            {
                return String.Format("{0}_{1}_{2}.jpg", this.Zoom, this.Col, this.Row);
            }
        }

        public Tile()
        {

        }

        public Tile(int r, int c, int z, int imgR, int imgC)
        {
            this.Row = r;
            this.Col = c;
            this.Zoom = z;
            this.ImageCol = imgC;
            this.ImageRow = imgR;

            Console.WriteLine("Tile Created: " + r + " / " + c);
        }
    }

    public class Extent
    {
        public Point UL { get; set; }
        public Point BR { get; set; }

        public Extent(Point _ul, Point _br)
        {
            this.UL = _ul;
            this.BR = _br;
        }
    }

    public class Point
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Point() { }

        public override string ToString()
        {
            return string.Format("{0}, {1}", this.Y, this.X);
        }

        public Point(double _y, double _x)
        {
            this.X = _x;
            this.Y = _y;
        }
    }
}
