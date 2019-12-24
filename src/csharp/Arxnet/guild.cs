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
    public class Guild
    {
        public string name { get; set; }

        [Obsolete("Use Location")]
        public int x
        {
            get => _location.X;
            set => _location.X = value;
        }

        [Obsolete("Use Location")]
        public int y 
        {
            get => _location.Y;
            set => _location.Y = value;
        }

        public Point Location        
        {
            //TODO: Make auto once obsolete members are removed
            get => _location;
            set => _location = value;
        }

        //TODO: Use Range
        public int minAlignment { get; set; }

        //TODO: Use Range
        public int maxAlignment { get; set; }

        public int minLevel { get; set; }

        public int type { get; set; }

        public int enemyGuild { get; set; }
                
        public int fullDues { get; set; }

        public int associateDues { get; set; }

        #region Private Members
        
        private Point _location;
        #endregion
    }
}