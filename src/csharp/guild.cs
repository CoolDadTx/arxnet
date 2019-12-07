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
    public class Guild
    {
        public int associateDues { get; set; }

        public int enemyGuild { get; set; }

        public int fullDues { get; set; }

        public int maxAlignment { get; set; }

        public int minAlignment { get; set; }

        public int minLevel { get; set; }

        public string name { get; set; }

        public int type { get; set; }

        public int x { get; set; }

        public int y { get; set; }
    }

    public class GuildSpell
    {
        public int cost { get; set; }

        public int index { get; set; }

        public string name { get; set; }
    }

    public partial class GlobalMembers
    {
        public static string guildLyricsFilename;

        public static int guildNo;

        public static Guild[] guilds =
        {
            new Guild()
            {
                name = "Thieves Guild",
                x = 43,
                y = 29,
                minAlignment = 64,
                maxAlignment = 128,
                minLevel = 2,
                type = 1,
                enemyGuild = 8,
                fullDues = 150,
                associateDues = 100
            },
            new Guild()
            {
                name = "Blue Wizards Guild",
                x = 18,
                y = 16,
                minAlignment = 120,
                maxAlignment = 192,
                minLevel = 2,
                type = 2,
                enemyGuild = 6,
                fullDues = 150,
                associateDues = 100
            },
            new Guild()
            {
                name = "Light Wizards Guild",
                x = 2,
                y = 59,
                minAlignment = 144,
                maxAlignment = 255,
                minLevel = 2,
                type = 2,
                enemyGuild = 5,
                fullDues = 150,
                associateDues = 100
            },
            new Guild()
            {
                name = "Green Wizards Academy",
                x = 11,
                y = 21,
                minAlignment = 96,
                maxAlignment = 160,
                minLevel = 2,
                type = 2,
                enemyGuild = 4,
                fullDues = 150,
                associateDues = 100
            },
            new Guild()
            {
                name = "Red Wizards University",
                x = 47,
                y = 49,
                minAlignment = 48,
                maxAlignment = 127,
                minLevel = 2,
                type = 1,
                enemyGuild = 3,
                fullDues = 150,
                associateDues = 100
            },
            new Guild()
            {
                name = "Dark Wizards Guild",
                x = 33,
                y = 42,
                minAlignment = 0,
                maxAlignment = 64,
                minLevel = 2,
                type = 1,
                enemyGuild = 2,
                fullDues = 150,
                associateDues = 100
            },
            new Guild()
            {
                name = "Star Wizards Guild",
                x = 27,
                y = 52,
                minAlignment = 120,
                maxAlignment = 176,
                minLevel = 2,
                type = 2,
                enemyGuild = 1,
                fullDues = 150,
                associateDues = 100
            },
            new Guild()
            {
                name = "Wizards of Chaos Guild",
                x = 50,
                y = 4,
                minAlignment = 64,
                maxAlignment = 128,
                minLevel = 2,
                type = 1,
                enemyGuild = 9,
                fullDues = 150,
                associateDues = 100
            },
            new Guild()
            {
                name = "Wizards of Law Guild",
                x = 61,
                y = 14,
                minAlignment = 132,
                maxAlignment = 208,
                minLevel = 2,
                type = 2,
                enemyGuild = 0,
                fullDues = 150,
                associateDues = 100
            },
            new Guild()
            {
                name = "Guild of Order",
                x = 57,
                y = 14,
                minAlignment = 132,
                maxAlignment = 255,
                minLevel = 2,
                type = 2,
                enemyGuild = 7,
                fullDues = 150,
                associateDues = 100
            },
            new Guild()
            {
                name = "Physicians Guild",
                x = 5,
                y = 49,
                minAlignment = 128,
                maxAlignment = 224,
                minLevel = 2,
                type = 2,
                enemyGuild = 11,
                fullDues = 150,
                associateDues = 100
            },
            new Guild()
            {
                name = "Assassins Guild",
                x = 55,
                y = 61,
                minAlignment = 16,
                maxAlignment = 112,
                minLevel = 2,
                type = 1,
                enemyGuild = 10,
                fullDues = 150,
                associateDues = 100
            },
            new Guild()
            {
                name = "Mercenaries' Guild",
                x = 32,
                y = 8,
                minAlignment = 64,
                maxAlignment = 128,
                minLevel = 5,
                type = 1,
                enemyGuild = 13,
                fullDues = 150,
                associateDues = 100
            },
            new Guild()
            {
                name = "Paladins' Guild",
                x = 25,
                y = 19,
                minAlignment = 152,
                maxAlignment = 255,
                minLevel = 5,
                type = 2,
                enemyGuild = 12,
                fullDues = 150,
                associateDues = 100
            }
        };

        public static GuildSpell[] guildSpells = Arrays.InitializeWithDefaultInstances<GuildSpell>(35);

        public static int itemQuantity; // used for number of charges to be paid for

        public static sf.Music Music1 = new sf.Music();

        public const int numberOfGuilds = 14; // 14 excluding - 2 extras in City

        public static void ClearGuildDisplay ()
        {
            clock1.restart();
            if (plyr.scenario == 0)
                ClearShopDisplay();
            if ((plyr.scenario == 1) && (guilds[guildNo].type == 1))
                ClearShopDisplay();
            if ((plyr.scenario == 1) && (guilds[guildNo].type == 2))
                ClearShopDisplay();
            UpdateLyrics();
            iCounter++;
        }

        public static int GetGuildNo ()
        {
            var guild_no = 0;

            if (plyr.scenario == 0) // City
            {
                for (var i = 0; i < numberOfGuilds; i++) // Max number of guild objects
                {
                    if ((guilds[i].x == plyr.x) && (guilds[i].y == plyr.y))
                        guild_no = i; // The number of the guild you have entered
                }
            }

            if (plyr.scenario == 1) // Dungeon
            {
                if (plyr.location == 49)
                    guild_no = 7; // chaos
                if (plyr.location == 50)
                    guild_no = 9; // order
                if (plyr.location == 51)
                    guild_no = 8; // law
                if (plyr.location == 55)
                    guild_no = 0; // thieves
                if (plyr.location == 138)
                    guild_no = 2; // light
                if (plyr.location == 181)
                    guild_no = 5; // dark
                if (plyr.location == 42)
                    guild_no = 12; // mercenaries
                if (plyr.location == 41)
                    guild_no = 13; // paladins
            }
            return guild_no;
        }

        public static void PracticeSpells ()
        {
            // Based on SelectItem code using "pages" of spells hence reference to "pages > 2" etc

            var itemRef = 9999; // Nothing selected
            var selectDesc = "Would you like to practice your spell of";

            var menuitem1 = 255; // 255 is used here as nil
            var menuitem2 = 255;
            var menuitem3 = 255;
            var menuitem4 = 255;
            var selectDone = false;

            var no_items = plyr.spellIndex; // Number of spells in players inventory
            var cur_idx = 0;
            var pages = 0;
            var page = 3;
            var page_item = 0;
            pages = 0;

            var noPages = no_items / 4; // based on 4 oncreen items per page
            pages += noPages;
            var tempRemainder = no_items % 4;
            if (tempRemainder != 0)
                pages++;

            while (!selectDone)
            {
                if (page > 2) // Variable items
                {
                    var keypressed = false;
                    while (!keypressed)
                    {
                        ClearGuildDisplay();
                        CyText(1, selectDesc);
                        BText(5, 3, "(1)");
                        BText(5, 4, "(2)");
                        BText(5, 5, "(3)");
                        BText(5, 6, "(4)");
                        BText(2, 8, "Item #, Forward, Back, or ESC to exit");
                        SetFontColour(40, 96, 244, 255);
                        BText(2, 8, "     #  F        B        ESC");
                        SetFontColour(215, 215, 215, 255);

                        page_item = 1;
                        cur_idx = ((page - 3) * 4);
                        menuitem1 = 9999; // 9999 is used as nil
                        menuitem2 = 9999;
                        menuitem3 = 9999;
                        menuitem4 = 9999;

                        while ((cur_idx < plyr.spellIndex) && (page_item < 5))
                        {
                            var str = $"{spells[(spellBuffer[cur_idx].no)].name} {Itos(spellBuffer[cur_idx].percentage)}%";
                            BText(9, (page_item + 2), str);
                            switch (page_item)
                            {
                                case 1:
                                menuitem1 = cur_idx;
                                break;
                                case 2:
                                menuitem2 = cur_idx;
                                break;
                                case 3:
                                menuitem3 = cur_idx;
                                break;
                                case 4:
                                menuitem4 = cur_idx;
                                break;
                            }
                            page_item++;
                            cur_idx++;
                        }
                        UpdateDisplay();

                        var key_value = GetSingleKey();
                        if ((key_value == "1") && (menuitem1 != 9999))
                        {
                            itemRef = menuitem1;
                            keypressed = true;
                            selectDone = true;
                        }
                        if ((key_value == "2") && (menuitem2 != 9999))
                        {
                            itemRef = menuitem2;
                            keypressed = true;
                            selectDone = true;
                        }
                        if ((key_value == "3") && (menuitem3 != 9999))
                        {
                            itemRef = menuitem3;
                            keypressed = true;
                            selectDone = true;
                        }
                        if ((key_value == "4") && (menuitem4 != 9999))
                        {
                            itemRef = menuitem4;
                            keypressed = true;
                            selectDone = true;
                        }

                        if (key_value == "ESC")
                        {
                            keypressed = true;
                            selectDone = true;
                        }
                        if ((key_value == "B") && (page > 3))
                        {
                            keypressed = true;
                            page--;
                        }
                        if ((key_value == "up") && (page > 3))
                        {
                            keypressed = true;
                            page--;
                        }
                        if ((key_value == "F") && (pages > (page - 2)))
                        {
                            keypressed = true;
                            page++;
                        }
                        if ((key_value == "down") && (pages > (page - 2)))
                        {
                            keypressed = true;
                            page++;
                        }
                    }
                }
            }

            if (itemRef != 9999)
            {
                var practiceHours = 4;
                while (practiceHours > 0)
                {
                    ClearGuildDisplay();
                    CyText(2, "Practicing the spell of");
                    var str = spells[(spellBuffer[itemRef].no)].name;
                    CyText(4, str);
                    UpdateDisplay();
                    sf.sleep(sf.seconds(1));
                    AddHour();
                    practiceHours--;
                }
                DeductCoins(0, 100, 0);
                spellBuffer[itemRef].percentage++; // Add 1% to spell percentage
            }
        }

        public static void ShopGuild ()
        {
            int itemChoice;
            var menuStartItem = 0;
            var guildSpellsNo = 0;
            guildNo = GetGuildNo();
            var guildSpellIndex = 0;
            Console.Write(guildNo);
            Console.Write("\n");
            if (guildNo == 5)
            {
                SetAutoMapFlag(plyr.map, 33, 41);
                SetAutoMapFlag(plyr.map, 33, 42);
                SetAutoMapFlag(plyr.map, 33, 43);
            }

            // Stock guild spells each visit - crash with name?
            for (var i = 0; i < 35; i++)
            {
                if (spells[i].guilds[guildNo] == 1)
                {
                    guildSpellsNo++;
                    guildSpells[guildSpellIndex].cost = spells[i].cost;
                    guildSpells[guildSpellIndex].name = spells[i].name;
                    guildSpells[guildSpellIndex].index = i;
                    guildSpellIndex++;
                }
            }

            guildSpellsNo -= 6; // adjustment for menu
            if (guildNo == 0)
                guildSpellsNo--; // DIRTY FIX FOR THIEVES GUILD!

            var musicPlaying = false;

            if (!musicPlaying)
            {
                if (plyr.musicStyle == 0)
                {
                    if (plyr.scenario == 0)
                    {
                        Music1.openFromFile("data/audio/cityGuild.ogg");
                        guildLyricsFilename = "goodGuild.txt";
                    }
                    if ((plyr.scenario == 1) && (guilds[guildNo].type == 1))
                    {
                        Music1.openFromFile("data/audio/evilGuild.ogg");
                        guildLyricsFilename = "evilGuild.txt";
                    }
                    if ((plyr.scenario == 1) && (guilds[guildNo].type == 2))
                    {
                        Music1.openFromFile("data/audio/goodGuild.ogg");
                        guildLyricsFilename = "goodGuild.txt";
                    }
                }
                if (plyr.musicStyle == 1)
                {
                    if (guilds[guildNo].type == 1)
                    {
                        Music1.openFromFile("data/audio/B/evilGuild.ogg");
                        guildLyricsFilename = "evilGuild.txt";
                    }
                    if (guilds[guildNo].type == 2)
                    {
                        Music1.openFromFile("data/audio/B/goodGuild.ogg");
                        guildLyricsFilename = "goodGuild.txt";
                    }
                }
                LoadLyrics(guildLyricsFilename);
                Music1.play();
                musicPlaying = true;
            }

            var guildMenu = 1; // high level menu
            
            plyr.status = 2; // shopping

            if (plyr.scenario == 0)
                LoadShopImage(14);
            if ((plyr.scenario == 1) && (guilds[guildNo].type == 1))
                LoadShopImage(4);
            if ((plyr.scenario == 1) && (guilds[guildNo].type == 2))
                LoadShopImage(5);

            // Check for enemy guilds. Assume both full and associate membership counts
            var enemyGuild = false;
            for (var i = 0; i <= numberOfGuilds; i++)
            {
                if ((plyr.guildMemberships[i] == 255) && (guilds[i].enemyGuild == guildNo))
                    enemyGuild = true;
                if ((plyr.guildMemberships[i] == 240) && (guilds[i].enemyGuild == guildNo))
                    enemyGuild = true;
            }

            if (enemyGuild)
            {
                while (guildMenu == 1)
                {
                    ClearGuildDisplay();
                    CyText(2, "Leave!  Thou art not wanted here scum!");
                    UpdateDisplay();
                    var key = GetSingleKey();
                    if (key != "")
                        guildMenu = 0;
                }
            }

            if (plyr.guildMemberships[guildNo] == 255)
                guildMenu = 11; // Full member menu
            if (plyr.guildMemberships[guildNo] == 240)
                guildMenu = 11; // Associate member menu

            while (guildMenu > 0)
            {
                while (guildMenu == 1) // main menu
                {
                    if (plyr.guildAwards[guildNo] == false)
                    {
                        var guildawardnotchecked = true;

                        if (guildNo == 0)
                            plyr.skl++;
                        if (guildNo == 1)
                            plyr.speed++;
                        if (guildNo == 2)
                            plyr.wis++;
                        if (guildNo == 3)
                            plyr.sta++;
                        if (guildNo == 4)
                            plyr.str++;
                        if (guildNo == 5)
                            plyr.chr++;
                        if (guildNo == 6)
                        {
                            plyr.str++;
                            plyr.maxhp += 5;
                            plyr.hp += 5;
                        }
                        if (guildNo == 7)
                            plyr.chr++;
                        if (guildNo == 8)
                            plyr.wis++;
                        if (guildNo == 9)
                            plyr.inte++;
                        if (guildNo == 10)
                        {
                            plyr.hp += 3;
                            plyr.maxhp += 3;
                        }
                        if (guildNo == 11)
                            plyr.stealth += 30;
                        if (guildNo == 12)
                            plyr.str++;
                        if (guildNo == 13)
                            plyr.sta++;

                        while (guildawardnotchecked)
                        {
                            ClearGuildDisplay();
                            var text = new ostringstream();
                            if (guildNo == 0)
                                text << "Master Thieves show you techniques@that improve your skill.";
                            if (guildNo == 1)
                                text <<
                            "A Mage from the Blue Wizards@Guild uses special magic to@increase your physical speed to " <<
                            plyr.speed <<
                            ".";
                            if (guildNo == 2)
                                text <<
                            "A Mage from the Light Wizards@Guild uses special magic to@increase your wisdom to " <<
                            plyr.wis <<
                            ".";
                            if (guildNo == 3)
                                text <<
                            "A Mage from the Green Wizards Academy@uses special magic to@increase your stamina to " <<
                            plyr.sta <<
                            ".";
                            if (guildNo == 4)
                                text <<
                            "A Mage from the Red Wizards University@uses special magic to@increase your strength to " <<
                            plyr.str <<
                            ".";
                            if (guildNo == 5)
                                text <<
                            "A Mage from the Dark Wizards Guild@uses special magic to increase@your charm to " <<
                            plyr.chr <<
                            ".";
                            if (guildNo == 6)
                                text <<
                            "An Arch Mage from the Star Wizards@Guild uses special magic to increase@your hit points and strength.";
                            if (guildNo == 7)
                                text <<
                            "A Mage from the Wizards of Chaos Guild@uses special magic to increase@your charm to " <<
                            plyr.chr <<
                            ".";
                            if (guildNo == 8)
                                text <<
                            "A Mage from the Wizards of Law Guild@uses special magic to increase@your wisdom to " <<
                            plyr.wis <<
                            ".";
                            if (guildNo == 9)
                                text <<
                            "A Mage from the Guild of Order uses@special magic to increase your@intelligence to " <<
                            plyr.inte <<
                            ".";
                            if (guildNo == 10)
                                text <<
                            "A Doctor from the Physicians Guild@teaches you first aid beyond what@is commonly known. This increases@your hit points.";
                            if (guildNo == 11)
                                text <<
                            "A Master Assassin shows you some basic@forms of hiding and quiet approach@which increase your ability to@surprise attackers.";
                            if (guildNo == 12)
                                text <<
                            "A Mercenary from the Mercenaries Guild@uses special magic to increase@your strength to " <<
                            plyr.str <<
                            ".";
                            if (guildNo == 13)
                                text <<
                            "A Knight from the Paladin's Guild uses@special magic to increase your stamina.";

                            CText(text.str());
                            CyText(9, "< Press any key to continue >");
                            UpdateDisplay();

                            var key = GetSingleKey();
                            if ((key != "") && (key != "up") && (key != "down") && (key != "I") && (key != "K"))
                                guildawardnotchecked = false;
                        }
                        plyr.guildAwards[guildNo] = true;
                    }

                    ClearGuildDisplay();
                    var str = $"Welcome to the {guilds[guildNo].name}.";
                    CyText(1, str);
                    BText(6, 3, "(1) Apply for Guild membership.");
                    BText(6, 5, "(0) Leave.");
                    UpdateDisplay();

                    {
                        var key = GetSingleKey();
                        if (key == "F1")
                        {
                            Music1.stop();
                            LoadLyrics(guildLyricsFilename);
                            Music1.play();
                        }
                        if (key == "0")
                            guildMenu = 0;
                        if (key == "down")
                            guildMenu = 0;
                        if (key == "1")
                        {
                            key = "";
                            if (plyr.level < guilds[guildNo].minLevel)
                            {
                                while (key == "")
                                {
                                    ClearGuildDisplay();
                                    CyText(2, "I am deeply sorry but you have not");
                                    CyText(4, "the experience to join our guild yet.");
                                    CyText(9, "( Press a key )");
                                    UpdateDisplay();
                                    key = GetSingleKey();
                                    if (key != "")
                                        guildMenu = 0;
                                }
                            }
                            if ((plyr.alignment < guilds[guildNo].minAlignment) && (plyr.level >= guilds[guildNo].minLevel))
                            {
                                while (key == "")
                                {
                                    ClearGuildDisplay();
                                    CyText(2, "I am sorry but your soul is too dark");
                                    CyText(4, "to become a member of our guild.");
                                    UpdateDisplay();
                                    key = GetSingleKey();
                                    if (key != "")
                                        guildMenu = 0;
                                }
                            }
                            if ((plyr.alignment > guilds[guildNo].maxAlignment) && (plyr.level >= guilds[guildNo].minLevel))
                            {
                                while (key == "")
                                {
                                    ClearGuildDisplay();
                                    CyText(2, "I am sorry, but you are too righteous");
                                    CyText(4, "to become a member of our guild.");
                                    UpdateDisplay();
                                    key = GetSingleKey();
                                    if (key != "")
                                        guildMenu = 0;
                                }
                            }

                            if ((plyr.level >= guilds[guildNo].minLevel) &&
                                (plyr.alignment >= guilds[guildNo].minAlignment) &&
                                (plyr.alignment <= guilds[guildNo].maxAlignment))
                            {
                                var guildFullMembership = 255;
                                for (var i = 0; i <= numberOfGuilds; i++)
                                {
                                    if (plyr.guildMemberships[i] == 255)
                                        guildFullMembership = i;
                                }
                                if (guildFullMembership == 255)
                                    guildMenu = 3;
                                if (guildFullMembership < 255)
                                    guildMenu = 2;
                            }
                        }
                    };
                }

                while (guildMenu == 2) // Associate or full guild membership?
                {
                    ClearGuildDisplay();
                    CyText(2, "You can only have full membership@privileges in but one guild.@@Do you want:");
                    BText(7, 7, "(1) Full membership or");
                    BText(7, 8, "(2) Associate membership");

                    UpdateDisplay();
                    var key = GetSingleKey();
                    if (key == "1")
                        guildMenu = 3;
                    if (key == "2")
                        guildMenu = 12;
                }

                while (guildMenu == 3) // Join guild full membership menu
                {
                    ClearGuildDisplay();
                    var str = $"Dues are {Itos(guilds[guildNo].fullDues)} silvers.";
                    CyText(2, str);
                    CyText(4, "Do you still wish to join? (Y or N)");
                    DisplaySilverCoins();
                    UpdateDisplay();
                    var key = GetSingleKey();
                    if (key == "N")
                        guildMenu = 0;
                    if (key == "Y")
                        guildMenu = 4;
                }

                while (guildMenu == 4)
                {
                    if (CheckCoins(0, (guilds[guildNo].fullDues), 0))
                    {
                        var enemyGuild = guilds[guildNo].enemyGuild;
                        ClearGuildDisplay();
                        CyText(1, "You are now part of our ancient order!");
                        CyText(2, "We give you your own Guild Ring to");
                        CyText(3, "store spell energy and identify yourself");
                        CyText(4, "to other members.  Wear it with pride!");
                        CyText(5, "You may also keep your valuables safe");
                        CyText(6, "in your guild locker. Beware of anyone");
                        var str = $"from the {guilds[enemyGuild].name}!";
                        CyText(7, str);
                        CyText(9, "<<< Press any key to continue >>>");
                        UpdateDisplay();
                        var key = GetSingleKey();
                        if (key != "")
                        {
                            // Now joined guild
                            DeductCoins(0, (guilds[guildNo].fullDues), 0);

                            // remove or replace any previous "full membership" guild records
                            for (var i = 0; i <= numberOfGuilds; i++)
                            {
                                if (plyr.guildMemberships[i] == 255)
                                    plyr.guildMemberships[i] = 240;
                            }

                            var guildRingRef = GetQuestItemRef(3);
                            if (guildRingRef == 255)
                                guildRingRef = CreateQuestItem(3);
                            itemBuffer[guildRingRef].type = 201; // type 201 just for guild ring
                            itemBuffer[guildRingRef].location = 10; // move to players inventory
                            var newGuildName = "";
                            if (guildNo == 0)
                                newGuildName = "Thieves Ring";
                            if (guildNo == 1)
                                newGuildName = "Blue Ring";
                            if (guildNo == 2)
                                newGuildName = "Light Ring";
                            if (guildNo == 3)
                                newGuildName = "Green Ring";
                            if (guildNo == 4)
                                newGuildName = "Red Ring";
                            if (guildNo == 5)
                                newGuildName = "Dark Ring";
                            if (guildNo == 6)
                                newGuildName = "Star Ring";
                            if (guildNo == 7)
                                newGuildName = "Chaos Ring";
                            if (guildNo == 8)
                                newGuildName = "Law Ring";
                            if (guildNo == 9)
                                newGuildName = "Order Ring";
                            if (guildNo == 10)
                                newGuildName = "Physicians Ring";
                            if (guildNo == 11)
                                newGuildName = "Assassins Ring";
                            if (guildNo == 12)
                                newGuildName = "Mercenaries Ring";
                            if (guildNo == 13)
                                newGuildName = "Paladins Ring";
                            questItems[(itemBuffer[guildRingRef].index)].name = newGuildName;
                            plyr.ringCharges = 99;

                            plyr.guildMemberships[guildNo] = 255; // Now full member of this guild
                            guildMenu = 5;
                        } // Throw you out of the guild
                    } else
                    {
                        // Insufficient guild fees
                        ClearGuildDisplay();
                        CyText(2, "You have not the funds!");
                        UpdateDisplay();
                        var key = GetSingleKey();
                        if (key != "")
                            guildMenu = 0;
                    }
                }

                while (guildMenu == 5)
                {
                    ClearGuildDisplay();
                    if (CheckForQuestItem(3))
                        BText(8, 1, "(1) Charge your Guild Ring");
                    else
                        BText(8, 1, "(1) Replace your Guild Ring");
                    BText(8, 2, "(2) Have curses removed");
                    BText(8, 3, "(3) Learn Guild spells");
                    BText(8, 4, "(4) Practice Guild spells");
                    BText(8, 5, "(5) Resign from the Guild");
                    BText(8, 6, "(6) Check your Guild Locker");
                    BText(8, 7, "(0) Leave");

                    UpdateDisplay();
                    var key = GetSingleKey();
                    if (key == "1")
                    {
                        if (CheckForQuestItem(3))
                            guildMenu = 10;
                        else
                            guildMenu = 13;
                    }
                    if (key == "2")
                        guildMenu = 20;
                    if (key == "3")
                        guildMenu = 16;
                    if (key == "4")
                        guildMenu = 21;
                    if (key == "5")
                        guildMenu = 8;
                    if (key == "6")
                        guildMenu = 15;
                    if (key == "0")
                        guildMenu = 7;
                }

                while (guildMenu == 6)
                {
                    ClearGuildDisplay();
                    BText(8, 1, "(1) Apply for full membership");
                    BText(8, 2, "(2) Have curses removed");
                    BText(8, 3, "(3) Learn Guild spells");
                    BText(8, 4, "(4) Practice Guild spells");
                    BText(8, 5, "(5) Resign from the Guild");
                    BText(8, 7, "(0) Leave");

                    UpdateDisplay();
                    var key = GetSingleKey();
                    if (key == "1")
                        guildMenu = 3;
                    if (key == "2")
                        guildMenu = 20;
                    if (key == "3")
                        guildMenu = 16;
                    if (key == "4")
                        guildMenu = 21;
                    if (key == "5")
                        guildMenu = 8;

                    if (key == "0")
                        guildMenu = 7;
                }

                while (guildMenu == 7)
                {
                    ClearGuildDisplay();
                    CyText(2, "Come again soon,");
                    var str = plyr.gender == 1 ? $"Brother {plyr.name}." : $"Sister {plyr.name}.";
                    CyText(4, str);
                    UpdateDisplay();
                    var key = GetSingleKey();
                    if (key != "")
                        guildMenu = 0;
                }

                while (guildMenu == 8) // resign
                {
                    ClearGuildDisplay();
                    CyText(2, "Are you sure you want to");
                    CyText(4, "terminate your membership? (Y or N)");
                    UpdateDisplay();
                    var key = GetSingleKey();
                    if (key == "Y")
                    {
                        if (plyr.guildMemberships[guildNo] == 255)
                        {
                            // only remove ring if resigning from primary guild!
                            int ringRef = GetQuestItemRef(3);
                            itemBuffer[ringRef].location = 0; // move this guild ring to the void
                            // ADD 12 lines to set plyr.lockerGold etc = 0
                            // Spells are retained by character even after guild resignation and still usable without ring
                        }
                        plyr.guildMemberships[guildNo] = 0;
                        guildMenu = 9;
                    }
                    if (key == "N")
                    {
                        if (plyr.guildMemberships[guildNo] == 255)
                            guildMenu = 5;
                        if (plyr.guildMemberships[guildNo] == 240)
                            guildMenu = 6;
                    }
                }

                while (guildMenu == 9)
                {
                    ClearGuildDisplay();
                    var str = plyr.gender == 1 ? $"Farewell Brother {plyr.name}." : $"Farewell Sister {plyr.name}.";
                    CyText(2, str);
                    UpdateDisplay();
                    var key = GetSingleKey();
                    if (key != "")
                    {
                        // All locker contents will be lost if you don't clear out your guild locker first
                        plyr.lcompasses = 0;
                        plyr.lcopper = 0;
                        plyr.lcrystals = 0;
                        plyr.lfood = 0;
                        plyr.lwater = 0;
                        plyr.lkeys = 0;
                        plyr.ltimepieces = 0;
                        plyr.lsilver = 0;
                        plyr.lgold = 0;
                        plyr.lgems = 0;
                        plyr.ljewels = 0;
                        plyr.ltorches = 0;
                        guildMenu = 0;
                    }
                }

                while (guildMenu == 10)
                {
                    ClearGuildDisplay();
                    if (plyr.ringCharges == 99)
                        CyText(2, "Your ring is fully charged!");
                    UpdateDisplay();
                    var key = GetSingleKey();
                    if (plyr.ringCharges < 99)
                        guildMenu = 100;
                    if (key != "")
                        guildMenu = 5;
                }

                while (guildMenu == 100)
                {
                    itemQuantity = InputDepositQuantity(1012);
                    if (itemQuantity == 0)
                        guildMenu = 5;
                    if (itemQuantity > 0)
                        guildMenu = 101;
                }

                while (guildMenu == 101)
                {
                    ClearGuildDisplay();
                    var str = $"That will cost {Itos(itemQuantity)} silvers.";
                    CyText(2, str);
                    CyText(4, "Are you sure? (Y or N)");
                    DisplaySilverCoins();
                    UpdateDisplay();
                    var key = GetSingleKey();
                    if (key == "N")
                        guildMenu = 5;
                    if (key == "Y")
                    {
                        if (CheckCoins(0, itemQuantity, 0))
                        {
                            DeductCoins(0, itemQuantity, 0);
                            plyr.ringCharges += itemQuantity;
                            guildMenu = 5;
                        } else
                        {
                            // Insufficient guild fees
                            ClearGuildDisplay();
                            CyText(2, "You have not the funds!");
                            UpdateDisplay();
                            key = GetSingleKey();
                            if (key != "")
                                guildMenu = 5;
                        }
                    }
                }

                while (guildMenu == 11)
                {
                    ClearGuildDisplay();
                    var str = plyr.gender == 1 ? $"Welcome Brother {plyr.name}!" : $"Welcome Sister {plyr.name}!";
                    CyText(2, str);
                    UpdateDisplay();
                    var key = GetSingleKey();
                    if (key != "")
                    {
                        if (plyr.guildMemberships[guildNo] == 255)
                            guildMenu = 5;
                        if (plyr.guildMemberships[guildNo] == 240)
                            guildMenu = 6;
                    }
                }

                while (guildMenu == 12) // Join guild associate membership menu
                {
                    ClearGuildDisplay();
                    var str = $"Dues are {Itos(guilds[guildNo].associateDues)} silvers.";
                    CyText(2, str);
                    CyText(4, "Do you still wish to join? (Y or N)");
                    DisplaySilverCoins();
                    UpdateDisplay();
                    var key = GetSingleKey();
                    if (key == "N")
                        guildMenu = 0;
                    if (key == "Y")
                    {
                        if (CheckCoins(0, (guilds[guildNo].associateDues), 0))
                        {
                            DeductCoins(0, (guilds[guildNo].associateDues), 0);
                            plyr.guildMemberships[guildNo] = 240;
                            guildMenu = 11;
                        } else
                        {
                            // Insufficient guild fees
                            ClearGuildDisplay();
                            CyText(2, "You have not the funds!");
                            UpdateDisplay();
                            key = GetSingleKey();
                            if (key != "")
                                guildMenu = 0;
                        }
                    }
                }

                while (guildMenu == 13) // Replace guild ring if missing from inventory
                {
                    ClearGuildDisplay();
                    CyText(2, "It will cost 120 silvers to");
                    CyText(4, "replace your ring.");
                    CyText(6, "Are you sure? (Y or N)");
                    DisplaySilverCoins();
                    UpdateDisplay();
                    var key = GetSingleKey();
                    if (key == "N")
                        guildMenu = 5;
                    if (key == "Y")
                    {
                        if (CheckCoins(0, 120, 0))
                        {
                            int guildRingRef = GetQuestItemRef(3);
                            itemBuffer[guildRingRef].location = 10; // move back to players inventory
                            plyr.ringCharges = 99; // Put charges back to 99
                            guildMenu = 5;
                        } else
                        {
                            guildMenu = 14; // insufficient funds
                        }
                    } // Attempt to replace ring
                }

                while (guildMenu == 14) // Replace guild ring - insufficient funds
                {
                    ClearGuildDisplay();
                    CyText(2, "You have not the funds!");
                    UpdateDisplay();
                    var key = GetSingleKey();
                    if (key != "")
                    {
                        if (plyr.guildMemberships[guildNo] == 255)
                            guildMenu = 5;
                        if (plyr.guildMemberships[guildNo] == 240)
                            guildMenu = 6;
                    }
                }

                while (guildMenu == 15) // Guild locker
                {
                    ClearGuildDisplay();
                    CyText(1, "You are at your locker.");
                    CyText(2, "What do you want to do?");
                    BText(8, 4, "(1) Make a deposit");
                    BText(8, 5, "(2) Make a withdrawal");
                    BText(8, 7, "(0) Go back to main room");
                    UpdateDisplay();
                    var key = GetSingleKey();
                    if (key == "1")
                        SelectItem(4);
                    if (key == "2")
                        SelectItem(5);
                    if (key == "0")
                    {
                        if (plyr.guildMemberships[guildNo] == 255)
                            guildMenu = 5;
                        if (plyr.guildMemberships[guildNo] == 240)
                            guildMenu = 6;
                    }
                }

                while (guildMenu == 16) // Buy spells
                {
                    // Works on the assumption that all items will be displayed and have been validated to appear in this list

                    var maxMenuItems = 6;

                    ClearGuildDisplay();
                    CyText(0, "Which spell would you like to learn? ");

                    for (var i = 0; i < maxMenuItems; i++)
                    {
                        var itemNo = menuStartItem + i;

                        BText(1, (2 + i), $"( ) {guildSpells[itemNo].name}");
                        BText(1, (2 + i), "                                 silvers");
                    }
                    DisplaySilverCoins();

                    for (var i = 0; i < maxMenuItems; i++) // Max number of item prices in this menu display
                    {
                        int itemNo = menuStartItem + i;
                        var itemCost = guildSpells[itemNo].cost;
                        var x = 0;
                        if (itemCost < 1000)
                            x = 30;
                        if (itemCost < 100)
                            x = 31;

                        var itemCostDesc = ToCurrency(itemCost);
                        BText(x, (i + 2), itemCostDesc);
                    }

                    SetFontColour(40, 96, 244, 255);
                    BText(2, 2, "1");
                    BText(2, 3, "2");
                    BText(2, 4, "3");
                    BText(2, 5, "4");
                    BText(2, 6, "5");
                    BText(2, 7, "6");
                    if (menuStartItem != 0)
                        BText(2, 1, "}");
                    if (menuStartItem != guildSpellsNo)
                        BText(2, 8, "{");
                    SetFontColour(215, 215, 215, 255);

                    UpdateDisplay();

                    var key = GetSingleKey();
                    if (key == "1")
                    {
                        itemChoice = 0;
                        guildMenu = 17;
                    }
                    if (key == "2")
                    {
                        itemChoice = 1;
                        guildMenu = 17;
                    }
                    if (key == "3")
                    {
                        itemChoice = 2;
                        guildMenu = 17;
                    }
                    if (key == "4")
                    {
                        itemChoice = 3;
                        guildMenu = 17;
                    }
                    if (key == "5")
                    {
                        itemChoice = 4;
                        guildMenu = 17;
                    }
                    if (key == "6")
                    {
                        itemChoice = 5;
                        guildMenu = 17;
                    }
                    if ((key == "up") && (menuStartItem > 0))
                        menuStartItem--;
                    if ((key == "down") && (menuStartItem < guildSpellsNo))
                        menuStartItem++;
                    if (key == "ESC")
                    {
                        if (plyr.guildMemberships[guildNo] == 255)
                            guildMenu = 5;
                        if (plyr.guildMemberships[guildNo] == 240)
                            guildMenu = 6;
                    }
                    if (key == "0")
                    {
                        if (plyr.guildMemberships[guildNo] == 255)
                            guildMenu = 5;
                        if (plyr.guildMemberships[guildNo] == 240)
                            guildMenu = 6;
                    }
                }

                while (guildMenu == 17) // Check funds to learn a spell
                {
                    var spellNo = menuStartItem + itemChoice;
                    var spellCost = guildSpells[spellNo].cost;
                    // ADD Check for maximum number of spells per level

                    for (var i = 0; i <= (plyr.spellIndex); i++)
                    {
                        if (spellBuffer[i].no == (guildSpells[spellNo].index))
                            guildMenu = 19;
                    } // Check if spell already learnt
                    if (!CheckCoins(0, spellCost, 0))
                        guildMenu = 14;
                    if ((CheckCoins(0, spellCost, 0)) && (guildMenu == 17))
                    {
                        spellBuffer[plyr.spellIndex].no = guildSpells[spellNo].index; // Add new spell to spellBuffer
                        spellBuffer[plyr.spellIndex].percentage = 40; // Starting percentage success for this spell
                        DeductCoins(0, spellCost, 0);
                        plyr.spellIndex++;

                        guildMenu = 18;
                    }
                }

                while (guildMenu == 18) // Confirmation message for learning a spell
                {
                    ClearGuildDisplay();
                    CyText(2, "It is done!");
                    UpdateDisplay();
                    var key = GetSingleKey();
                    if (key != "")
                        guildMenu = 16;
                }

                while (guildMenu == 19) // Already have the spell
                {
                    ClearGuildDisplay();
                    CyText(2, "You have already learnt that spell!");
                    UpdateDisplay();
                    var key = GetSingleKey();
                    if (key != "")
                        guildMenu = 16;
                }

                while (guildMenu == 20) // All curses are removed
                {
                    ClearGuildDisplay();
                    CyText(1, "The Guild Master performs a few@chants and gestures and then@proclaims");

                    CyText(5, "\"All curses have been removed,");
                    if (plyr.gender == 1)
                        str = $"Brother {plyr.name}.\"";
                    if (plyr.gender == 2)
                        str = $"Sister {plyr.name}.\"";
                    CyText(6, str);
                    UpdateDisplay();
                    var key = GetSingleKey();
                    if (key != "")
                    {
                        if (plyr.guildMemberships[guildNo] == 255)
                            guildMenu = 5;
                        if (plyr.guildMemberships[guildNo] == 240)
                            guildMenu = 6;
                    }
                }

                while (guildMenu == 21) // Practice spells
                {
                    ClearGuildDisplay();
                    CyText(2, "Spell casting practice takes four@hours and costs 100 silvers.");
                    CyText(5, "Is this alright? (Y or N)");
                    DisplaySilverCoins();
                    UpdateDisplay();
                    var key = GetSingleKey();
                    if (key == "N")
                    {
                        if (plyr.guildMemberships[guildNo] == 255)
                            guildMenu = 5;
                        if (plyr.guildMemberships[guildNo] == 240)
                            guildMenu = 6;
                    }
                    if (key == "Y")
                    {
                        if (CheckCoins(0, 100, 0))
                        {
                            PracticeSpells();
                            if (plyr.guildMemberships[guildNo] == 255)
                                guildMenu = 5;
                            if (plyr.guildMemberships[guildNo] == 240)
                                guildMenu = 6;
                        } else
                        {
                            guildMenu = 14; // insufficient funds for spell practice
                        }
                    }
                }
            }
            Music1.stop();
            LeaveShop();
        }

        //extern sf::Clock clock1;
        //extern int iCounter;
        //extern spellItem spellBuffer[35];
        //extern questItem questItems[4];

    }
}