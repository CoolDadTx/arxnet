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
        public static string goblinLyricsFilename;

        public static void ShopGoblins ()
        {
            SetAutoMapFlag(plyr.map, 2, 13);
            SetAutoMapFlag(plyr.map, 2, 14);
            SetAutoMapFlag(plyr.map, 2, 15);
            SetAutoMapFlag(plyr.map, 1, 13);
            SetAutoMapFlag(plyr.map, 1, 14);
            SetAutoMapFlag(plyr.map, 1, 15);

            plyr.status = 2; // shopping
            var goblinsMenu = 1; // high level menu
            LoadShopImage(17); // goblins
            if (CheckForQuestItem(0))
                goblinsMenu = 2;
            while (goblinsMenu > 0)
            {
                while (goblinsMenu == 1) // main menu
                {
                    ClearShopDisplay();

                    // Need to check for possession of ring halves!

                    if (plyr.goblinsDefeated)
                    {
                        CyText(2, "Thief! Thou hast dared to steal from the");
                        CyText(3, "goblins! Now thou must fight the horde!");
                    }

                    if (plyr.goblinsVisited == false)
                    {
                        BText(2, 1, "Welcome, Sir Traveller! Lend me thine");
                        BText(2, 2, "assistance. Trolls have stolen half ");
                        BText(2, 3, "the magic ring The Dwarf made for me.");
                        BText(2, 4, "Bring it back and I'll reward thee. Go!");
                    }
                    if ((plyr.goblinsVisited) && (plyr.goblinsChallenged == false) && (plyr.goblinsReforged == false))
                    {
                        CyText(2, "Kind Knight, where is the ring? Don't");
                        CyText(4, "darken this hall again without it!");
                    }

                    if ((plyr.goblinsVisited) && (plyr.goblinsChallenged == false) && (plyr.goblinsReforged))
                    {
                        var str = $"Greetings {plyr.name}.";
                        CyText(2, str);
                        CyText(4, "To what do we owe this pleasure?");
                    }

                    if ((plyr.goblinsVisited) && (plyr.goblinsChallenged) && (plyr.goblinsDefeated == false))
                    {
                        CyText(2, "Art thou tired of living?");
                        CyText(4, "Stand and fight!");
                    }

                    UpdateDisplay();

                    if (plyr.musicStyle == 0)
                    {
                        PlayShopMusic(2);
                        goblinLyricsFilename = "goblins.txt";
                    }
                    if (plyr.musicStyle == 1)
                    {
                        PlayShopMusic(5);
                        goblinLyricsFilename = "goblins.txt";
                    }
                    LoadLyrics(goblinLyricsFilename);

                    var key = GetSingleKey();

                    if ((key == "SPACE") && (plyr.goblinsDefeated))
                    {
                        goblinsMenu = 0;
                        plyr.goblinsCombat = true;
                    } // leave shop and start combat!
                    if ((key == "SPACE") && (plyr.goblinsDefeated == false))
                        goblinsMenu = 2;
                }

                while (goblinsMenu == 2) // secondary menu
                {
                    if (plyr.goblinsChallenged)
                        goblinsMenu = 3;
                    ClearShopDisplay();

                    if ((plyr.goblinsChallenged == false) && (CheckForQuestItem(0) == false))
                    {
                        CyText(1, "What shall you do?");
                        BText(6, 3, "(1) Demand the Goblin's ring or");
                        BText(6, 4, "(0) Leave?");
                    }

                    if ((plyr.goblinsChallenged == false) && (CheckForQuestItem(0)))
                    {
                        CyText(1, "Thou hast brought my treasure!");
                        CyText(2, "Give me the ring half!");

                        BText(6, 4, "(1) Give up the ring or");
                        BText(6, 5, "(2) Refuse to give it up");
                    }

                    UpdateDisplay();
                    var key = GetSingleKey();

                    if ((plyr.goblinsChallenged == false) && (CheckForQuestItem(0) == false))
                    {
                        if (key == "0")
                            goblinsMenu = 0;
                        if (key == "1")
                        {
                            plyr.goblinsChallenged = true;
                            goblinsMenu = 3;
                        }
                    }
                    if ((plyr.goblinsChallenged == false) && (CheckForQuestItem(0)))
                    {
                        if (key == "1")
                            goblinsMenu = 4;
                        if (key == "2")
                        {
                            plyr.goblinsChallenged = true;
                            goblinsMenu = 3;
                        }
                    }
                }

                while (goblinsMenu == 3) // third menu
                {
                    ClearShopDisplay();
                    UpdateLyrics();

                    if ((plyr.goblinsChallenged) && (CheckForQuestItem(0) == false))
                    {
                        CyText(2, "Thou cans't have the ring only by");
                        CyText(4, "defeating me! Prepare to duel!");
                    }

                    if ((plyr.goblinsChallenged) && (CheckForQuestItem(0)))
                    {
                        CyText(2, "Foul friend to Trolls!");
                        CyText(4, "Prepare to die.");
                    }

                    UpdateDisplay();
                    var key = GetSingleKey();
                    if (key == "SPACE")
                    {
                        goblinsMenu = 0;
                        plyr.goblinsCombat = true;
                    } // leave shop and start combat!
                }

                while (goblinsMenu == 4) // fourth menu
                {
                    ClearShopDisplay();
                    UpdateLyrics();
                    CyText(2, "Thou art wise not to risk my wrath.");
                    CyText(4, "Thy reward is my thanks.");
                    UpdateDisplay();

                    var key = GetSingleKey();

                    if (key == "SPACE")
                    {
                        goblinsMenu = 2;
                        plyr.goblinsReforged = true;
                        int ringRef = GetQuestItemRef(0);
                        itemBuffer[ringRef].location = 0; // move this item to the void
                    }
                }
            }
            plyr.goblinsVisited = true;
            StopShopMusic();
            LeaveShop();
        }
    }
}