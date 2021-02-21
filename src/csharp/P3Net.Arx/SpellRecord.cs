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

        //TODO: Use TimeSpan
        public int duration { get; set; }

        //TODO: Should this be handled by Guild class instead, how should it work if we add a new spell - don't want to update guilds
        public bool[] guilds { get; set; } = new bool[14];
    }
}