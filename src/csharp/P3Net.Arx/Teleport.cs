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
    //TODO: Should this be a struct
    public class Teleport
    {
        //TODO: Should this be Direction?
        public int new_facing { get; set; }

        public int new_map { get; set; }

        public Point Position { get; set; }
       
        //TODO: Rename this to something meaningful
        public int @ref { get; set; }
    }
}