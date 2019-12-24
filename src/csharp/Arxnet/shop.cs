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
    //TODO: This should be base type of other "shops"
    public class Shop
    {
        //TODO: Use TimeRange
        public int closingHour { get; set; }

        public float initialPriceFactor { get; set; }

        //TODO: What does this mean?
        public int location { get; set; } // match with location text description number

        public float minimumPriceFactor { get; set; }

        public string name { get; set; }

        //TODO: Use TimeRange
        public int openingHour { get; set; }
    }
}