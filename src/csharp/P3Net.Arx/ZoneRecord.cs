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
    public class ZoneRecord
    {
        public int arch { get; set; }

        public int ceiling { get; set; }

        public int door { get; set; }

        public int floor { get; set; }

        public int wall { get; set; }
    }
}