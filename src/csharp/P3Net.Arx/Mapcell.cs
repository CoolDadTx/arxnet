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
    public class Mapcell
    {
        public int ceiling { get; set; }

        public int east { get; set; }

        public int eastHeight { get; set; }

        public int floor { get; set; }

        public int location { get; set; }

        public int north { get; set; }

        public int northHeight { get; set; }

        public int south { get; set; }

        public int southHeight { get; set; }

        public int special { get; set; }

        public int west { get; set; }

        public int westHeight { get; set; }

        public int zone { get; set; }
    }
}