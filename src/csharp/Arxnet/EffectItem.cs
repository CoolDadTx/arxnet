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
    //TODO: If we implement Effect class is this needed?
    public class EffectItem
    {
        public int duration { get; set; }

        public int effect { get; set; }

        public int negativeValue { get; set; }

        public int positiveValue { get; set; }
    }
}