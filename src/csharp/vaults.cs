﻿using System;
using System.Collections.Generic;
using System.Linq;
namespace P3Net.Arx
{
	public enum Menus
	{
		MenuLeft,
		MenuDraggedOutside,
		MenuMain,
		MenuGrabABag,
		MenuSearching
	}

	public partial class GlobalMembers
	{
		/* VAULTS.CPP
		 *
		 * TODO:
		 *
		 *  Choose random item
		 *  Check for guards
		 *
		 */



		/* VAULT.H
		 *
		 * TODO:
		
		 *
		 */

		public static void RunVault()
		{
			vmenu = (int)Menus.MenuMain;
			AddVaultToMap();
			LoadShopImage(27);
			SetBagType();

			while (vmenu != (int)Menus.MenuLeft)
			{
				gdt = gmyclock.restart();

				ClearShopDisplay();
				DisplayVaultModuleText();
				UpdateDisplay();
				ProcessVaultMenuInput();

				guardCheckTime += gdt;
				if (guardCheckTime >= sf.seconds(0.8f)) // was 0.8f
				{
					  CheckForGuard();
					  guardCheckTime = sf.Time.Zero;
					  //addMinute();
				}


			}
		}
		public static void ProcessVaultMenuInput()
		{
			string key = ReadKey();

			switch (vmenu)
			{
				case Menus.MenuMain:
					if (key == "0")
						vmenu = (int)Menus.MenuLeft;
					if (key == "1")
						vmenu = (int)Menus.MenuGrabABag;
					if (key == "2")
						vmenu = (int)Menus.MenuSearching;
					break;
				case Menus.MenuGrabABag:
					if (key == "SPACE")
					{
						GrabBag();
						vmenu = (int)Menus.MenuLeft;
					}
					break;
				case Menus.MenuSearching:
					if (key == "SPACE")
					{
						SetBagType();
						vmenu = (int)Menus.MenuMain;
					}
					break;
				case Menus.MenuDraggedOutside:
					if (key == "SPACE")
						vmenu = (int)Menus.MenuLeft;
					break;
			}
		}
		public static void DisplayVaultModuleText()
		{
			if (vmenu == (int)Menus.MenuMain)
			{
				string str = "You are in " + vaultName;
				string str2 = "You see a bag of " + bagDesc + ".";
				CyText(1, str);
				CyText(3, str2);
				BText(1, 5, "Do you (1) Grab the bag and run,");
				BText(8, 6, "(2) Search for something else or");
				BText(8, 7, "(0) Leave?");
			} else if (vmenu == (int)Menus.MenuGrabABag)
			{
				string str = "@@You grab the bag of " + bagDesc + "@@and run!";
				CyText(1, str);
		} else if (vmenu == (int)Menus.MenuSearching)
		{
				string str = "@@@Searching...@@@(Hit SPACE key to stop searching)";
				CyText(1, str);
		} else if (vmenu == (int)Menus.MenuDraggedOutside)
		{
				if (plyr.stolenFromVault == 0)
					CyText(1, "@@A guard escorts you@@out of the bank's vault.");
				else
					CyText(1, "@@A guard drags you outside.");
		}
		}
		public static void AddVaultToMap()
		{
				if (plyr.x == 2)
				{
					SetAutoMapFlag(plyr.map, 2, 2);
					vaultName = "Gram's Gold Exchange vault.";
				}
				if (plyr.x == 30)
				{
					SetAutoMapFlag(plyr.map, 30, 57);
					vaultName = "the First City vault.";
				}
		}
		public static void SetBagType()
		{
			// Copper, Silver, Gold, Gems or Jewels
			int x = Randn(0, 4);

			switch (x)
			{
				case 0:
					bagType = 0;
					bagDesc = "copper coins";
					break;
				case 1:
					bagType = 1;
					bagDesc = "silver coins";
					break;
				case 2:
					bagType = 2;
					bagDesc = "gold coins";
					break;
				case 3:
					bagType = 3;
					bagDesc = "gems";
					break;
				case 4:
					bagType = 4;
					bagDesc = "jewels";
					break;
				default:
					bagType = 0;
					bagDesc = "copper coins";
					break;
			}
		}
		public static void GrabBag()
		{
			int amount = Randn(0, 10) + 40;
			if (bagType == 0)
				plyr.copper += amount;
			if (bagType == 1)
				plyr.silver += amount;
			if (bagType == 2)
				plyr.gold += amount;
			if (bagType == 3)
				plyr.gems += amount;
			if (bagType == 4)
				plyr.jewels += amount;

			// Alignment penalty
			plyr.stolenFromVault = 1; // Determines what happens if discovered by guard
			plyr.alignment = plyr.alignment - 5;
			if (plyr.alignment < 0)
				plyr.alignment = 0;
		}
		public static void CheckForGuard()
		{
			int x = Randn(0, 100);
			if (x < 5)
			{
				if (plyr.stolenFromVault == 1)
					plyr.stolenFromVault = 2;
				vmenu = (int)Menus.MenuDraggedOutside;
			}
		}

		public static int vmenu;
		public static sf.Clock gmyclock = new sf.Clock();
		public static sf.Time gdt = new sf.Time();
		public static sf.Time guardCheckTime = new sf.Time();

		public static int bagType = 0;
		public static string bagDesc = "copper coins";
		public static string vaultName = "NO NAME.";

	}
}