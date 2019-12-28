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
using System.Linq;

namespace P3Net.Arx
{
    public class CreateCharacterCounter
    {
        public int value1 { get; set; }
        public int value2 { get; set; }

        public Point Position
        {
            get => _position;
            set => _position = value;
        }

        // always constant
        [Obsolete("Use Position")]
        public int x
        {
            get => _position.X;
            set => _position.X = value;
        }

        // can be 1-8 , 2 can be 9-16?
        [Obsolete("Use Position")]
        public int y
        {
            get => _position.Y;
            set => _position.Y = value;
        }

        public int speed { get; set; } // decrement from this value until zero to slow down refresh of counter displat
        public int speed_initial { get; set; } // used to reset speed value above when it reaches zero

        #region Private Members

        //TODO: Remove when obsolete removed
        private Point _position;
        #endregion
    }
}