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
        public static void ShopUndeadKing ()
        {
            plyr.status = GameStates.Module; // shopping

            SetAutoMapFlag(plyr.map, 13, 26);
            SetAutoMapFlag(plyr.map, 14, 26);
            SetAutoMapFlag(plyr.map, 13, 27);
            SetAutoMapFlag(plyr.map, 14, 27);

            LoadShopImage(24);

            ModuleMessage("As you gaze at the wall of the crypt@@the ghostly figure of King Danjor@@appears.  The apparition speaks:");
            if (plyr.undeadKingVisited)
                ModuleMessage("I can tell you no more@@than you already know.");
            else
            {
                plyr.undeadKingVisited = true;
                ModuleMessage("Long ago, on a distant soil,@@The Keepers came. What became of@@my home, I know not. I awoke in");
                ModuleMessage("a strange world. In it were some@@like myself, but most were strange@@creatures. Many among us forsook the");
                ModuleMessage("Way of Knowledge and became sloth,@@turning to thievery and murder for@@their daily bread. Only I and seven");
                ModuleMessage("others remained true. We learned to@@look behind the mask of this world,@@and to see The Keepers, who brought");
                ModuleMessage("us here to quarrel and fight amongst@@ourselves for their amusement. We@@taught others our knowledge and stole");
                ModuleMessage("the weapons of light from the Keepers.@@A great battle was made; but, in the@@end, we were defeated.");
                ModuleMessage("Lest our dreams of home and freedom@@die with us on this alien soil,@@The Seven took an oath so strong");
                ModuleMessage("that it bound them beyond death:@@We shall await one that will have@@the strength to carry on our hope.");                
                ModuleMessage($"You are that one,@@{plyr.name}.@@Other than the gifts that you have");
                ModuleMessage("received from The Seven, I can only@@offer you a portion of the staff of@@Acrinimiril. The Keepers consider");
                ModuleMessage("Acrinimiril mad and are not@@aware of his true knowledge.@@Seek this knowledge!");

                // Create the staff piece if it's not already in players inventory
                if (!CheckForQuestItem(6))
                {
                    int itemRef = CreateQuestItem(6);
                    itemBuffer[itemRef].location = 10;
                }
            }

            // Move the player outside the Undead Palace
            plyr.Position = new System.Drawing.Point(14, 28);

            //MLT: Double to float
            plyr.z_offset = 1.6F; // position player just outside door
            plyr.status = GameStates.Explore; // explore
        }
    }
}