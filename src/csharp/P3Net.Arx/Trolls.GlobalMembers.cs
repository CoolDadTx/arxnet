﻿/*
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
    public static partial class GlobalMembers
    {
        public static void ShopTrolls ()
        {
            SetAutoMapFlag(plyr.map, 56, 56);
            SetAutoMapFlag(plyr.map, 56, 57);
            SetAutoMapFlag(plyr.map, 56, 58);
            SetAutoMapFlag(plyr.map, 57, 56);
            SetAutoMapFlag(plyr.map, 57, 57);
            SetAutoMapFlag(plyr.map, 57, 58);

            plyr.status = GameStates.Module; // shopping
            var trollsMenu = 1; // high level menu
            LoadShopImage(16); // trolls
            if (CheckForQuestItem(1))
                trollsMenu = 2;

            while (trollsMenu > 0)
            {
                while (trollsMenu == 1) // main menu
                {
                    ClearShopDisplay();

                    // Need to check for possession of ring halves!

                    if (plyr.trollsDefeated)
                    {
                        CyText(2, "Thief! Thou hast dared to steal from the");
                        CyText(3, "Trolls! Now thou must fight the horde!");
                    }

                    if (plyr.trollsVisited == false)
                    {
                        BText(2, 1, "Welcome Brave Quester! I need thy help.");
                        BText(2, 2, "Goblins have taken half the magic ring");
                        BText(2, 3, "my father got from the Dwarf. Great");
                        BText(2, 4, "reward is thine for bringing it back.");
                    }
                    if ((plyr.trollsVisited) && (plyr.trollsChallenged == false) && (plyr.trollsReforged == false))
                    {
                        CyText(2, "Why art thou back emptyhanded?");
                        CyText(4, "Go get the ring half!");
                    }

                    if ((plyr.trollsVisited) && (plyr.trollsChallenged == false) && (plyr.trollsReforged))
                    {
                        CyText(2, $"Welcome {plyr.name}.");
                        CyText(4, "Hast thou more business here?");
                    }

                    if ((plyr.trollsVisited) && (plyr.trollsChallenged) && (plyr.trollsDefeated == false))
                    {
                        CyText(2, "Art thou back again, Goblin-face?");
                        CyText(4, "Thou'll not escape this time!");
                    }

                    UpdateDisplay();

                    var music = plyr.musicStyle ? 4 : 1;                    
                    PlayShopMusic(music);
                    
                    trollLyricsFilename = "trolls.txt";
                    LoadLyrics(trollLyricsFilename);

                    var key = GetSingleKey();

                    if (key == "SPACE")
                    {
                        if (plyr.trollsDefeated)
                        {
                            trollsMenu = 0;
                            plyr.trollsCombat = true;
                        } else
                        {
                            trollsMenu = 2;
                        };
                    } // leave shop and start combat!
                }

                while (trollsMenu == 2) // secondary menu
                {
                    if (plyr.trollsChallenged)
                        trollsMenu = 3;
                    ClearShopDisplay();

                    if ((plyr.trollsChallenged == false) && (CheckForQuestItem(1) == false))
                    {
                        CyText(1, "What shall you do?");
                        BText(6, 3, "(1) Demand the Troll's ring or");
                        BText(6, 4, "(0) Leave?");
                    }

                    if ((plyr.trollsChallenged == false) && (CheckForQuestItem(1)))
                    {
                        CyText(1, "Thou hast brought my treasure!");
                        CyText(2, "Give me the ring half!");

                        BText(6, 4, "(1) Give up the ring or");
                        BText(6, 5, "(2) Refuse to give it up");
                    }

                    UpdateDisplay();
                    var key = GetSingleKey();

                    if ((plyr.trollsChallenged == false) && (CheckForQuestItem(1) == false))
                    {
                        if (key == "0")
                            trollsMenu = 0;
                        if (key == "1")
                        {
                            plyr.trollsChallenged = true;
                            trollsMenu = 3;
                        }
                    }
                    if ((plyr.trollsChallenged == false) && (CheckForQuestItem(1)))
                    {
                        if (key == "1")
                            trollsMenu = 4;
                        if (key == "2")
                        {
                            plyr.trollsChallenged = true;
                            trollsMenu = 3;
                        }
                    }
                }

                while (trollsMenu == 3) // third menu
                {
                    ClearShopDisplay();
                    UpdateLyrics();

                    if ((plyr.trollsChallenged) && (CheckForQuestItem(1) == false))
                    {
                        CyText(2, "I'll not give it up so easily!");
                        CyText(4, "Prepare to do battle!");
                    }

                    if ((plyr.trollsChallenged) && (CheckForQuestItem(1)))
                    {
                        CyText(2, "Foul friend to Goblins!");
                        CyText(4, "Prepare to die.");
                    }

                    UpdateDisplay();
                    var key = GetSingleKey();
                    if (key == "SPACE")
                    {
                        trollsMenu = 0;
                        plyr.trollsCombat = true;
                    } // leave shop and start combat!
                }

                while (trollsMenu == 4) // fourth menu
                {
                    ClearShopDisplay();
                    UpdateLyrics();
                    CyText(2, "Thou art wise not to risk my wrath.");
                    CyText(4, "Thy reward is my thanks.");
                    UpdateDisplay();

                    var key = GetSingleKey();

                    if (key == "SPACE")
                    {
                        trollsMenu = 2;
                        plyr.trollsReforged = true;
                        var ringRef = GetQuestItemRef(1);
                        itemBuffer[ringRef].location = 0; // move this item to the void
                    }
                }
            }
            plyr.trollsVisited = true;
            StopShopMusic();
            LeaveShop();
        }

        #region Review Data

        public static string trollLyricsFilename;
        #endregion
    }
}