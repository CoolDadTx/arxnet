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
		public bool CHARACTER_CREATION { get; set; } // Skip the full character creation sequence for the City and the Dungeon
		public bool TELEPORT_OPTION { get; set; } // Option for teleporting around map
	}

	public partial class GlobalMembers
	{
		public static void SetDeveloperFlags()
		{
			AR_DEV.CHARACTER_CREATION = OnOff.On;
			AR_DEV.TELEPORT_OPTION = OnOff.Off;
		}

		//extern devSettings AR_DEV;


		public static DevSettings AR_DEV = new DevSettings();

	}
}