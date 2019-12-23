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

using SFML.Audio;

namespace P3Net.Arx
{
    public class Guild
    {
        public string name { get; set; }

        public int x { get; set; }

        public int y { get; set; }

        public int minAlignment { get; set; }

        public int maxAlignment { get; set; }

        public int minLevel { get; set; }

        public int type { get; set; }

        public int enemyGuild { get; set; }
                
        public int fullDues { get; set; }

        public int associateDues { get; set; }
    }
}