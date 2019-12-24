/*
 * Copyright © Michael Taylor (P3Net)
 * All Rights Reserved
 *
 * http://www.michaeltaylorp3.net
 * 
 * Converted code from ARX C++ (http://www.landbeyond.net/arx/index.php)
 * Code converted using C++ to C# Code Converter, Tangible Software (https://www.tangiblesoftwaresolutions.com/)
 */
using System;
using System.Drawing;

namespace P3Net.Arx
{
    //TODO: Should this be a struct - only used in one place for levels
    public class ZoneRect
    {
        //NOTE: Guessing here
        public Point TopLeft 
        {
            //TODO: Use auto property once obsolete is removed
            get => _topLeft;
            set => _topLeft = value;
        }

        public Point BottomRight
        {
            //TODO: Use auto property once obsolete is removed
            get => _bottomRight;
            set => _bottomRight = value;
        }

        [Obsolete("Use TopLeft")]
        public int x1 
        {
            get => _topLeft.X;
            set => _topLeft.X = value;
        }

        [Obsolete("Use TopLeft")]
        public int y1
        {
            get => _topLeft.Y;
            set => _topLeft.Y = value;
        }

        [Obsolete("Use BottomRight")]
        public int x2
        {
            get => _bottomRight.X;
            set => _bottomRight.X = value;
        }

        [Obsolete("Use BottomRight")]
        public int y2
        {
            get => _bottomRight.Y;
            set => _bottomRight.Y = value;
        }

        public int zoneRef { get; set; }

        #region Private Members

        private Point _topLeft;
        private Point _bottomRight;
        #endregion
    }
}