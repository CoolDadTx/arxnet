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
    public class Bank
    {
        public string name { get; set; }

        public BankAccount[] accounts { get; set; } = Arrays.InitializeWithDefaultInstances<BankAccount>(3);
        public int location { get; set; } // match with location text description number

        //TODO: Use TimeRange
        public int openingHour { get; set; }
        public int closingHour { get; set; }
        public int jobProbability { get; set; }
        public int gemCost { get; set; }
        public int jewelCost { get; set; }
    }
}