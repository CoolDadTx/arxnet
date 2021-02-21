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
    //TODO: Make a class with options to control resolution, texture quality, etc

    public enum DisplayOptions
    {
        AtariSmall = 0,
        AlternateSmall = 1,
        AlternateLarge = 2
    }

    public static class DisplayOptionsExtensions
    {
        //TODO: Make this an interface or facade so this is not needed everywhere
        public static bool UseAlternateTextures ( this DisplayOptions source ) => source != DisplayOptions.AtariSmall;

        //TODO: Make this an interface or facade so this is not needed everywhere
        public static bool UseLargeSize ( this DisplayOptions source ) => source == DisplayOptions.AlternateLarge;

        //TODO: Make this an interface or facade so this is not needed everywhere
        public static bool UseOriginalSize ( this DisplayOptions source ) => source != DisplayOptions.AlternateLarge;
    }
}