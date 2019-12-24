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
    //TODO: Should this be a struct
    public class Teleport
    {
        //TODO: Should this be Direction?
        public int new_facing { get; set; }

        public int new_map { get; set; }

        public Point Location
        {
            //TODO: Use auto property once obsolete is removed
            get => _location;
            set => _location = value;
        }

        [Obsolete("Use Location")]
        public int new_x
        {
            get => _location.X;
            set => _location.X = value;
        }

        [Obsolete("Use Location")]
        public int new_y
        {
            get => _location.Y;
            set => _location.Y = value;
        }

        //TODO: Rename this to something meaningful
        public int @ref { get; set; }

        #region Private Members

        private Point _location;
        #endregion
    }
}