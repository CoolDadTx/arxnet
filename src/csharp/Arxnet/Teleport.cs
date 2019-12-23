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
    public class Teleport
    {
        public int new_facing { get; set; }

        public int new_map { get; set; }

        public int new_x { get; set; }

        public int new_y { get; set; }

        public int @ref { get; set; }
    }
}