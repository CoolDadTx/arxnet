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

        [Obsolete("Use Position")]
        public int x
        {
            get => _position.X;
            set => _position.X = value;
        }

        [Obsolete("Use Position")]
        public int y 
        {
            get => _position.Y;
            set => _position.Y = value;
        }

        public Point Position        
        {            
            get => _position;
            set => _position = value;
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

        //TODO: Make auto once obsolete members are removed
        private Point _position;
        #endregion
    }
}