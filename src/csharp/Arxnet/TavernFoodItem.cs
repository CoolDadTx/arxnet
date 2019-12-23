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
    public class TavernFoodItem
    {
        public int basePrice { get; set; }

        public int foodPacketAdded { get; set; }

        public int hungerRemoved { get; set; }

        public string name { get; set; }
    }
}