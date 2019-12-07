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
using System.Linq;

namespace P3Net.Arx
{
    /* VAULTS.CPP
		 *
		 * TODO:
		 *
		 *  Choose random item
		 *  Check for guards
		 *
		 */

    public enum VaultMenus
    {
        MenuLeft,
        MenuDraggedOutside,
        MenuMain,
        MenuGrabABag,
        MenuSearching
    }

    public partial class GlobalMembers
    {
        public static string bagDesc = "copper coins";

        public static int bagType = 0;
        public static sf.Time gdt = new sf.Time();
        public static sf.Clock gmyclock = new sf.Clock();
        public static sf.Time guardCheckTime = new sf.Time();
        public static string vaultName = "NO NAME.";

        public static int vmenu;

        public static void AddVaultToMap ()
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

        public static void CheckForGuard ()
        {
            var x = Randn(0, 100);
            if (x < 5)
            {
                if (plyr.stolenFromVault == 1)
                    plyr.stolenFromVault = 2;
                vmenu = (int)VaultMenus.MenuDraggedOutside;
            }
        }

        public static void DisplayVaultModuleText ()
        {
            if (vmenu == (int)VaultMenus.MenuMain)
            {
                CyText(1, $"You are in {vaultName}");
                CyText(3, $"You see a bag of {bagDesc}.");
                BText(1, 5, "Do you (1) Grab the bag and run,");
                BText(8, 6, "(2) Search for something else or");
                BText(8, 7, "(0) Leave?");
            } else if (vmenu == (int)VaultMenus.MenuGrabABag)
            {
                CyText(1, $"@@You grab the bag of {bagDesc}@@and run!");
            } else if (vmenu == (int)VaultMenus.MenuSearching)
            {
                CyText(1, "@@@Searching...@@@(Hit SPACE key to stop searching)");
            } else if (vmenu == (int)VaultMenus.MenuDraggedOutside)
            {
                if (plyr.stolenFromVault == 0)
                    CyText(1, "@@A guard escorts you@@out of the bank's vault.");
                else
                    CyText(1, "@@A guard drags you outside.");
            }
        }

        public static void GrabBag ()
        {
            var amount = Randn(0, 10) + 40;
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

        public static void ProcessVaultMenuInput ()
        {
            var key = ReadKey();

            switch (vmenu)
            {
                case VaultMenus.MenuMain:
                if (key == "0")
                    vmenu = (int)VaultMenus.MenuLeft;
                if (key == "1")
                    vmenu = (int)VaultMenus.MenuGrabABag;
                if (key == "2")
                    vmenu = (int)VaultMenus.MenuSearching;
                break;
                case VaultMenus.MenuGrabABag:
                if (key == "SPACE")
                {
                    GrabBag();
                    vmenu = (int)VaultMenus.MenuLeft;
                }
                break;
                case VaultMenus.MenuSearching:
                if (key == "SPACE")
                {
                    SetBagType();
                    vmenu = (int)VaultMenus.MenuMain;
                }
                break;
                case VaultMenus.MenuDraggedOutside:
                if (key == "SPACE")
                    vmenu = (int)VaultMenus.MenuLeft;
                break;
            }
        }

        public static void RunVault ()
        {
            vmenu = (int)VaultMenus.MenuMain;
            AddVaultToMap();
            LoadShopImage(27);
            SetBagType();

            while (vmenu != (int)VaultMenus.MenuLeft)
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
                }
            }
        }

        public static void SetBagType ()
        {
            // Copper, Silver, Gold, Gems or Jewels
            var x = Randn(0, 4);

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
    }
}