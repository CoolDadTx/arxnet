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
    public enum Scenarios
    {
        City,
        Dungeon,
        Arena,
        Palace,
        Wilderness,
        Revelation,
        Destiny,

        //TODO: Change to init value to eliminate this 
        Unknown = 255,
    }
}