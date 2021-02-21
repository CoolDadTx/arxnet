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
    public class Map
    {
        public int background { get; set; }

        public string description { get; set; }

        public string filename { get; set; }

        //TODO: Should this really be settable?
        public Size Size
        {
            //TODO: Use auto property once obsolete
            get => _size;
            set => _size = value;
        }

        [Obsolete("Use Size")]
        public int height
        {
            get => _size.Height;
            set => _size.Height = value;
        }

        [Obsolete("Use Size")]
        public int width
        {
            get => _size.Width;
            set => _size.Width = value;
        }

        #region Private Members

        private Size _size;
        #endregion
    }
}