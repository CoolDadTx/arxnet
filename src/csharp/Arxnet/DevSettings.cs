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
    /// <summary>Provides developer settings.</summary>
    public class DevSettings
    {
        /// <summary>Gets or sets whether to support character creation sequence.</summary>
        public bool EnableCharacterCreation { get; set; } = true;

        /// <summary>Gets or sets whether teleporting is allowed.</summary>
        public bool EnableTeleporting { get; set; }
    }
}