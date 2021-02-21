/*
 * Copyright © Michael Taylor (P3Net)
 * All Rights Reserved
 *
 * http://www.michaeltaylorp3.net
 */
using System;
using System.Drawing;

namespace P3Net.Arx
{
    public static class DrawingPointExtensions
    {
        public static Point AdjustX ( this Point source, int offset ) => new Point(source.X + offset, source.Y);

        public static Point AdjustY ( this Point source, int offset ) => new Point(source.X, source.Y + offset);

        public static Point AdjustXY ( this Point source, int offsetX, int offsetY ) => new Point(source.X + offsetX, source.Y + offsetY);

        public static Point WithX ( this Point source, int x ) => new Point(x, source.Y);

        public static Point WithY ( this Point source, int y ) => new Point(source.X, y);
    }
}
