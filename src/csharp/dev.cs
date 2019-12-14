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
using System.Collections.Generic;
using System.Linq;

// dev.cpp
// Developer Settings for use during development and testing
// All flags are 0 = Off, 1 = On

/* dev.h
 * Developer Settings to speed up development and testing
 */

namespace P3Net.Arx
{
	 public enum OnOff
	 {
		Off,
		On
	 }

	public class DevSettings
	{
		public OnOff CHARACTER_CREATION { get; set; } // Skip the full character creation sequence for the City and the Dungeon
		public OnOff TELEPORT_OPTION { get; set; } // Option for teleporting around map
	}

	public partial class GlobalMembers
	{
		public static void SetDeveloperFlags()
		{
			AR_DEV.CHARACTER_CREATION = OnOff.On;
			AR_DEV.TELEPORT_OPTION = OnOff.Off;
		}		

		public static DevSettings AR_DEV = new DevSettings();

        //extern devSettings AR_DEV;
    }
}