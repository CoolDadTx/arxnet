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
    //TODO: Should this be part of core Item type?
    public class ShopClothingItem
    {
        public int itemRef { get; set; }

        public int price { get; set; }

        public int type { get; set; } // 180 - clothing?
    }
}