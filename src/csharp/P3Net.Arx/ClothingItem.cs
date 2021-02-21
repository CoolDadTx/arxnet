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
    public class ClothingItem
    {
        // Description is created from values below if name blank
        // Clothing items have no body location
        // 4 items can be worn at same time
        public string name { get; set; }
        public int quality { get; set; }
        public int colour { get; set; }
        public int fabric { get; set; }
        public int type { get; set; }
        public int weight { get; set; }
    }
}