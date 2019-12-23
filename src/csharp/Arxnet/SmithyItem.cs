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
using System.Linq;

namespace P3Net.Arx
{
    public class SmithyItem
    {
        public int basePrice { get; set; }

        public int itemRef { get; set; }

        public string name { get; set; }

        public int type { get; set; } // 177 - armour, 178 - weapon
    }
}