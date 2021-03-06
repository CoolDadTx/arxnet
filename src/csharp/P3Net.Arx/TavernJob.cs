﻿/*
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
    public class TavernJob
    {
        public float fatigueRate { get; set; }

        public float majorWoundProbability { get; set; }

        public int maxIncome { get; set; }

        public int minIncome { get; set; }

        public float minorWoundProbability { get; set; }

        public string name { get; set; }

        //TODO: Make a list of requirements with stats being one of the options
        public string statRequirementName { get; set; }

        public int statRequirementValue { get; set; }
    }
}