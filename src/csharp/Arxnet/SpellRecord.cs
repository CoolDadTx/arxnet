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
    public class SpellRecord
    {
        public string name { get; set; }

        public int percentage { get; set; }

        public int cost { get; set; }

        public int effect { get; set; }

        public int negativeValue { get; set; }

        public int positiveValue { get; set; }

        public int duration { get; set; }

        public bool[] guilds { get; set; } = new bool[14];
    }
}