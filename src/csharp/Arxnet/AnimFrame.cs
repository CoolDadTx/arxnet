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
    public class AnimFrame
    {
        public int xOffset { get; set; } // 0 for most animations
        public int yOffset { get; set; } // 0 for most animations
        public int image { get; set; }
        public int duration { get; set; }
    }
}