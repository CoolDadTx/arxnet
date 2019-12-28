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

        public Point Position
        {
            //TODO: Use auto property once obsolete is removed
            get => _position;
            set => _position = value;
        }

        [Obsolete("Use Position")]
        public int new_x
        {
            get => _position.X;
            set => _position.X = value;
        }

        [Obsolete("Use Position")]
        public int new_y
        {
            get => _position.Y;
            set => _position.Y = value;
        }

        //TODO: Rename this to something meaningful
        public int @ref { get; set; }

        #region Private Members

        private Point _position;
        #endregion
    }
}