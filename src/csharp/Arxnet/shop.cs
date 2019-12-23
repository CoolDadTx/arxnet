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

namespace P3Net.Arx
{
    public class Shop
    {
        public int closingHour { get; set; }

        public float initialPriceFactor { get; set; }

        public int location { get; set; } // match with location text description number

        public float minimumPriceFactor { get; set; }

        public string name { get; set; }

        public int openingHour { get; set; }
    }
}