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
    public partial class GlobalMembers
    {
        public static void ShopLift ()
        {
            var liftMenu = 1; // high level menu

            plyr.status = GameStates.Module; // shopping

            LoadShopImage(22);

            while (liftMenu > 0)
            {
                while (liftMenu == 1) // main menu
                {
                    ClearShopDisplay();
                    CyText(1, "In the elevator you find three buttons.");
                    CyText(3, "Which do you press?");
                    BText(11, 5, "(1) The red button");
                    BText(11, 6, "(2) The green button");
                    BText(11, 7, "(3) The blue button");
                    UpdateDisplay();

                    var key = GetSingleKey();

                    if (key == "0")
                        liftMenu = 0;
                    if (key == "down")
                        liftMenu = 0;
                }
            }
            LeaveShop();
        }

        // extern Player plyr;
        // extern sf::RenderWindow App;
    }
}