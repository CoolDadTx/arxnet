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
    public enum GraphicsMode
    {
        AtariSmall = 0,
        AlternateSmall = 1,
        AlternateLarge = 2
    }

    public static class GraphicsModeExtensions
    {
        //TODO: Make this an interface or facade so this is not needed everywhere
        public static bool UseAlternateTextures ( this GraphicsMode source ) => source != GraphicsMode.AtariSmall;

        //TODO: Make this an interface or facade so this is not needed everywhere
        public static bool UseLargeSize ( this GraphicsMode source ) => source == GraphicsMode.AlternateLarge;

        //TODO: Make this an interface or facade so this is not needed everywhere
        public static bool UseOriginalSize ( this GraphicsMode source ) => source != GraphicsMode.AlternateLarge;
    }
}