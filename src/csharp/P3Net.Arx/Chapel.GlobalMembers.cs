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

namespace P3Net.Arx
{
    public static partial class GlobalMembers
    {
        public static void ShopChapel ()
        {
            // Module for Dungeon Chapel

            var chapelMenu = 1; // high level menu
            plyr.status = GameStates.Module; // shopping

            LoadShopImage(18);

            while (chapelMenu > 0)
            {
                while (chapelMenu == 1) // main menu
                {
                    ClearShopDisplay();
                    CyText(1, "Welcome to the Dungeon Chapel.");
                    CyText(3, "What would you like to do?");
                    BText(8, 5, "(1) Pray");
                    BText(8, 6, "(2) Listen to a sermon");
                    BText(8, 7, "(3) Consult with a priest");
                    BText(8, 8, "(4) Make a donation");
                    BText(8, 9, "(0) Leave");
                    UpdateDisplay();
                    PlayShopMusic(3);

                    var key = GetSingleKey();

                    if (key == "0")
                        chapelMenu = 0;
                    if (key == "down")
                        chapelMenu = 0;
                }
            }

            StopShopMusic();
            LeaveShop();
        }                
    }
}