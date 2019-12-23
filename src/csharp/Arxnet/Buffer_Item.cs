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
    public class Buffer_Item
    {
        public int type { get; set; } // 83, 03, etc
        public int index { get; set; } // in appropriate array (e.g. armour array) dependent on type above
                                       // 0 - limbo, 1 - floor, 10 - carried, 11 - primary, 12 - secondary,
                                       // 13 - worn, 14 - head, 15 - arms, 16 - legs, 17 - body
        public int location { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int level { get; set; }
        public int hp { get; set; } // hp or no. of charges or no. of items for generic items like food packets

        public string name { get; set; }
        public int maxHP { get; set; }
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
        public int alignment { get; set; }
        public int melee { get; set; }
        public int ammo { get; set; }
        public int parry { get; set; }
    }
}