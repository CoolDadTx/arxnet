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
    //TODO: Make base class for Jobs
    public class InnJob
    {
        public float fatigueRate { get; set; }

        public float majorWoundProbability { get; set; }

        public int maxIncome { get; set; }

        public int minIncome { get; set; }

        public float minorWoundProbability { get; set; }

        public string name { get; set; }

        //TODO: Use requiremens type
        public string statRequirementName { get; set; }

        public int statRequirementValue { get; set; }
    }
}