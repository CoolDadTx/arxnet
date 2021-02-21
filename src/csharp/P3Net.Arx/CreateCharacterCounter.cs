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
using System.Drawing;

namespace P3Net.Arx
{
    public class CreateCharacterCounter
    {
        public int value1 { get; set; }
        public int value2 { get; set; }

        public Point Position { get; set; }
        
        public int speed { get; set; } // decrement from this value until zero to slow down refresh of counter displat
        public int speed_initial { get; set; } // used to reset speed value above when it reaches zero
    }
}