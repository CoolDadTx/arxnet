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
    public static partial class GlobalMembers
    {
        public static void Staircase ()
        {
            var staircaseMenu = 1; // high level menu
            
            plyr.status = GameStates.Module; //shopping

            var stairwayUp = true;
            if ((plyr.Position.X == 50) && (plyr.Position.Y == 3) && (plyr.map == 0))
                stairwayUp = false;
            if ((plyr.Position.X == 59) && (plyr.Position.Y == 62) && (plyr.map == 0))
                stairwayUp = false;
            if ((plyr.Position.X == 49) && (plyr.Position.Y == 17) && (plyr.map == 1))
                stairwayUp = false;
            if ((plyr.Position.X == 16) && (plyr.Position.Y == 17) && (plyr.map == 1))
                stairwayUp = false;
            if ((plyr.Position.X == 16) && (plyr.Position.Y == 48) && (plyr.map == 1))
                stairwayUp = false;
            if ((plyr.Position.X == 48) && (plyr.Position.Y == 48) && (plyr.map == 1))
                stairwayUp = false;
            if ((plyr.Position.X == 17) && (plyr.Position.Y == 12) && (plyr.map == 2))
                stairwayUp = false;

            // Need to determine if up or down staircase            
            if (stairwayUp)
                LoadShopImage(6);
            else
                LoadShopImage(7);

            while (staircaseMenu > 0)
            {
                while (staircaseMenu == 1) // main menu
                {
                    ClearShopDisplay();

                    if (stairwayUp)
                        CyText(1, "A stairway leads up, do you take it?");
                    else
                        CyText(1, "A stairway leads down, do you take it?");
                    CyText(3, "( es or  o)");
                    SetFontColor(40, 96, 244, 255);
                    CyText(3, " Y      N  ");
                    SetFontColor(215, 215, 215, 255);
                    UpdateDisplay();

                    var key = GetSingleKey();

                    if (key == "N")
                    {
                        staircaseMenu = 0;
                        LeaveShop();
                    }
                    if (key == "Y")
                    {
                        ;
                        MoveMapLevel();
                        plyr.status = GameStates.Explore;
                        staircaseMenu = 0;
                    }
                    ;
                }
            }
        }
    }
}