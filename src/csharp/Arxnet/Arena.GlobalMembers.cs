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
    public partial class GlobalMembers
    {
        public static void ArenaSouthernEntrance ()
        {
            var menu = 1; // high level menu
            string key;
            plyr.status = GameStates.Module; // special module

            LoadShopImage(25);

            while (menu > 0)
            {
                while (menu == 1) // main menu
                {
                    ClearShopDisplay();
                    CyText(1, "You are at the southern entrance");
                    CyText(3, " to the Arena of Xebec's Demise.");
                    CyText(5, "Dost thou wish to?");
                    BText(9, 7, "  (1) Enter the Arena");
                    BText(9, 8, "  (0) Leave");
                    UpdateDisplay();

                    key = GetSingleKey();
                    if (key == "0")
                        menu = 0;
                    if (key == "1")
                        menu = 2;
                }

                while (menu == 2) // Enter the Arena attempt
                {
                    do
                    {
                        ClearShopDisplay();

                        var str = (plyr.gender == 1) ? "Away with you knave!" : "Away with you scullion!";
                        CyText(2, str);
                        CyText(4, "Only those of the great houses");
                        CyText(6, "may enter here.");
                        UpdateDisplay();
                        key = GetSingleKey();
                        if (key != "")
                            menu = 0;
                    } while (key == "");
                }

            } // end main while loop

            LeaveShop();
        }

        public static void ArenaNorthernEntrance ()
        {
            plyr.status = GameStates.Module; // special module

            while (true)
            {
                ClearShopDisplay();
                CyText(1, "You are at the northern entrance");
                CyText(3, " to the Arena of Xebec's Demise.");
                CyText(5, "Dost thou wish to?");
                BText(9, 7, "  (1) Enter the Arena");
                BText(9, 8, "  (0) Leave the Arena");
                UpdateDisplay();

                var key = GetSingleKey();                
                if (key == "0")
                {
                    plyr.x = 16;
                    plyr.y = 1;
                    plyr.z_offset = 1.0F;
                    plyr.scenario = Scenarios.City;
                    plyr.status = GameStates.Explore; // explore
                    break;
                }

                if (key == "1")
                {
                    plyr.x = 16;
                    plyr.y = 3;
                    plyr.z_offset = 1.0F;
                    plyr.scenario = Scenarios.Arena;
                    plyr.status = GameStates.Explore; // explore
                    break;
                }                
            }
        }

        public static void ArenaWesternEntrance ()
        {
            string key;
            do
            {
                ClearShopDisplay();
                CyText(1, "You are at the western entrance");
                CyText(3, "to the Arena. A heavy portcullis");
                CyText(5, "blocks the entrance.");
                CyText(8, "(0) Leave");
                UpdateDisplay();

                key = GetSingleKey();
            } while (key != "0");
            LeaveShop();
        }
    }
}