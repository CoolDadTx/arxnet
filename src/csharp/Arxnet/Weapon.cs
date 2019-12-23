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
    public class Weapon
    {
        public string article { get; set; }
        public string name { get; set; }
        public string desc { get; set; }
        public string descMon { get; set; }
        public int type { get; set; }

        public int flags { get; set; }
        public int minStrength { get; set; }
        public int minDexterity { get; set; }
        public int useStrength { get; set; }
        public int blunt { get; set; }
        public int sharp { get; set; }
        public int earth { get; set; }
        public int air { get; set; }
        public int fire { get; set; }
        public int water { get; set; }
        public int power { get; set; }
        public int magic { get; set; } // mental
        public int good { get; set; } // cleric
        public int evil { get; set; }
        public int cold { get; set; }
        public int weight { get; set; }
        public int hp { get; set; }
        public int maxHP { get; set; }
        public int alignment { get; set; }
        public int melee { get; set; }
        public int ammo { get; set; }
        public int parry { get; set; }
    }
}