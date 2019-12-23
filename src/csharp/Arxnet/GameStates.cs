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
    public enum GameStates
    {
        Explore = 1,

        //TODO: Defined as Shopping in most references
        Module = 2,
        Encounter = 3,
        Dead = 4

        //TODO: State 5 is called Dead but doesn't line up with this value
    }
}