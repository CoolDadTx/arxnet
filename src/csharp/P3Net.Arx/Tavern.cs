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
    public class Tavern
    {
        public bool classy { get; set; }

        //TODO: Use TimeRange
        public int closingHour { get; set; }

        public int jobProbability { get; set; }

        //TODO: Review how this is used
        public int location { get; set; } // match with location text description number

        //TODO: Should we use a currency type?
        public int membershipFee { get; set; } // usually 0

        public string name { get; set; }

        //TODO: Use TimeRange
        public int openingHour { get; set; }

        public float priceFactor { get; set; }
    }
}