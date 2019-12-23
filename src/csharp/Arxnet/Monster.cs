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
    public class Monster
    {
        public int type { get; set; }
        public string name { get; set; }
        public string pluName { get; set; }
        public string armorText { get; set; }
        public int behaviour { get; set; } // evil, bad, neutral, good
        public int alignment { get; set; }
        public int stealth { get; set; }
        public int randomStrength { get; set; }
        public int randomSkill { get; set; }
        public int randomIntelligence { get; set; }
        public int randomSpeed { get; set; }
        public int hp { get; set; }
        public int randomHP { get; set; }
        public int maxHP { get; set; }
        public int image { get; set; } // becomes first frame in animations array
        public int image2 { get; set; } // becomes last frame in animations array

        public int sta { get; set; }
        public int cha { get; set; }
        public int str { get; set; }
        public int inte { get; set; }
        public int wis { get; set; }
        public int skl { get; set; }
        public int spd { get; set; }

        public int aBlunt { get; set; }
        public int aSharp { get; set; }
        public int aEarth { get; set; }
        public int aAir { get; set; }
        public int aFire { get; set; }
        public int aWater { get; set; }
        public int aPower { get; set; }
        public int aMagic { get; set; }
        public int aGood { get; set; }
        public int aEvil { get; set; }
        public int aCold { get; set; }

        public int tPotions { get; set; }
        public int tEquipment { get; set; }
        public int tFood { get; set; }
        public int tWater { get; set; }
        public int tTorches { get; set; }
        public int tTimepieces { get; set; }
        public int tCompasses { get; set; }
        public int tKeys { get; set; }
        public int tCrystals { get; set; }
        public int tCopper { get; set; }
        public int tSilver { get; set; }
        public int tGold { get; set; }
        public int tGems { get; set; }
        public int tJewels { get; set; }

        public int w1 { get; set; }
        public int w2 { get; set; }
        public int w3 { get; set; }
        public int w4 { get; set; }
        public int w5 { get; set; }
        public int w6 { get; set; }
        public int c1 { get; set; }
        public int c2 { get; set; }
        public int c3 { get; set; }
        public int c4 { get; set; }
        public int c5 { get; set; }
        public int c6 { get; set; }

        //TODO: Separate the monster definition from the current encounter so we don't have to copy this data each time
        public Monster CopyFrom ( Monster source ) => MemberwiseClone() as Monster;
    }
}