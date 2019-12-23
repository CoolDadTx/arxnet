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
    public class BufferItem
    {
        public int offset { get; set; } // item number in inventory[] OR binary offset???
        public string name { get; set; } // Added for convenience of building item lists?

        // 0 - limbo, 1 - floor, 10 - carried, 11 - primary, 12 - secondary,
        // 13 - worn, 14 - head, 15 - arms, 16 - legs, 17 - body
        public int location { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int level { get; set; }
    }
}