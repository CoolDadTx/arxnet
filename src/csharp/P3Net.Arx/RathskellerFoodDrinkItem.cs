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
    public class RathskellerFoodDrinkItem
    {
        public int alcoholValue { get; set; }

        public int basePrice { get; set; } // Multiplied by 2 to get cost in silvers

        public int hungerValue { get; set; }

        public string name { get; set; }

        public int thirstValue { get; set; }
    }
}