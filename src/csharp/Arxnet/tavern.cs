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
using SFML.Audio;

namespace P3Net.Arx
{
    public class Tavern
    {
        public bool classy { get; set; }

        public int closingHour { get; set; }

        public int jobProbability { get; set; }

        public int location { get; set; } // match with location text description number

        public int membershipFee { get; set; } // usually 0

        public string name { get; set; }

        public int openingHour { get; set; }

        public float priceFactor { get; set; }
    }
}