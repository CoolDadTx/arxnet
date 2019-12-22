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
    public class SpellItem
    {
        public int no { get; set; }

        public int percentage { get; set; }
    }

    public class SpellRecord
    {
        public string name { get; set; }

        public int percentage { get; set; }

        public int cost { get; set; }

        public int effect { get; set; }

        public int negativeValue { get; set; }

        public int positiveValue { get; set; }

        public int duration { get; set; }

        public bool[] guilds { get; set; } = new bool[14];
    }

    public partial class GlobalMembers
    {
        public static SpellItem[] spellBuffer = Arrays.InitializeWithDefaultInstances<SpellItem>(35); // learnt spells that can be cast

        public static SpellRecord[] spells =
        {
            new SpellRecord() { name = "Bewilder", percentage = 50, cost = 42, effect = 10, negativeValue = 10, positiveValue = 0, duration = 1, guilds = new [] { true, false, false, false, false, false, false, true, false, true, false, false, true, false } },
            new SpellRecord() { name = "Blinding", percentage = 50, cost = 42, effect = 10, negativeValue = 10, positiveValue = 0, duration = 1, guilds = new [] { false, false, true, false, false, false, false, false, false, false, false, false, false, false } },
            new SpellRecord() { name = "Charisma", percentage = 50, cost = 42, effect = 10, negativeValue = 10, positiveValue = 0, duration = 1, guilds = new [] {true, false, false, false, false, true, false, true, false, false, false, false, true, false } },
            new SpellRecord() { name = "Cold Blast", percentage = 50, cost = 42, effect = 10, negativeValue = 10, positiveValue = 0, duration = 1, guilds = new [] {false, false, false, false, false, true, false, false, true, true, false, false, false, false } },
            new SpellRecord() { name = "Conjure Food", percentage = 50, cost = 42, effect = 10, negativeValue = 10, positiveValue = 0, duration = 1, guilds = new [] {false, false, false, false, false, false, false, false, false, true, false, false, true, false } },
            new SpellRecord() { name = "Conjure Key", percentage = 50, cost = 42, effect = 10, negativeValue = 10, positiveValue = 0, duration = 1, guilds = new [] {true, false, false, false, false, false, false, false, true, false, false, false, true, false } },
            new SpellRecord() { name = "Defeat Evil", percentage = 50, cost = 42, effect = 10, negativeValue = 10, positiveValue = 0, duration = 1, guilds = new [] {false, false, false, false, false, false, false, false, true, true, false, false, false, false } },
            new SpellRecord() { name = "Defeat Good", percentage = 50, cost = 42, effect = 10, negativeValue = 10, positiveValue = 0, duration = 1, guilds = new [] {false, false, false, false, false, true, false, true, false, false, false, false, false, false } },
            new SpellRecord() { name = "Dexterity", percentage = 50, cost = 42, effect = 10, negativeValue = 10, positiveValue = 0, duration = 1, guilds = new [] {true, false, true, false, false, false, false, false, false, false, false, false, false, false } },
            new SpellRecord() { name = "Fear", percentage = 50, cost = 42, effect = 10, negativeValue = 10, positiveValue = 0, duration = 1, guilds = new [] {false, false, true, false, false, true, false, true, false, true, false, false, false, false } },
            new SpellRecord() { name = "Fireballs", percentage = 50, cost = 42, effect = 10, negativeValue = 10, positiveValue = 0, duration = 1, guilds = new [] {false, false, false, false, false, false, false, true, true, false, false, false, false, false } },
            new SpellRecord() { name = "Fireblade", percentage = 50, cost = 42, effect = 10, negativeValue = 10, positiveValue = 0, duration = 1, guilds = new [] {false, false, false, false, false, false, false, true, false, false, false, false, false, true } },
            new SpellRecord() { name = "Fury", percentage = 50, cost = 42, effect = 10, negativeValue = 10, positiveValue = 0, duration = 1, guilds = new [] {false, false, false, false, false, false, false, false, false, false, false, false, false, true } },
            new SpellRecord() { name = "Healing", percentage = 50, cost = 42, effect = 10, negativeValue = 10, positiveValue = 0, duration = 1, guilds = new [] {true, true, true, true, true, true, true, true, true, true, true, true, false, true } },
            new SpellRecord() { name = "Light", percentage = 50, cost = 42, effect = 10, negativeValue = 10, positiveValue = 0, duration = 1, guilds = new [] {false, false, true, false, false, false, false, false, true, true, false, false, false, true } },
            new SpellRecord() { name = "Lightning Bolts", percentage = 50, cost = 42, effect = 10, negativeValue = 10, positiveValue = 0, duration = 1, guilds = new [] {false, false, true, false, false, true, false, false, false, false, false, false, false, false } },
            new SpellRecord() { name = "Location", percentage = 50, cost = 42, effect = 10, negativeValue = 10, positiveValue = 0, duration = 1, guilds = new [] {false, true, true, true, true, true, true, true, true, true, true, true, true, false } },
            new SpellRecord() { name = "Luck", percentage = 50, cost = 42, effect = 10, negativeValue = 10, positiveValue = 0, duration = 1, guilds = new [] {true, false, false, false, false, false, false, false, false, false, false, false, true, false } },
            new SpellRecord() { name = "Magic Darts", percentage = 50, cost = 42, effect = 10, negativeValue = 10, positiveValue = 0, duration = 1, guilds = new [] {false, false, true, false, false, true, false, false, false, false, false, false, false, false } },
            new SpellRecord() { name = "Night Vision", percentage = 50, cost = 42, effect = 10, negativeValue = 10, positiveValue = 0, duration = 1, guilds = new [] {true, false, false, false, false, true, false, true, false, false, false, false, true, false } },
            new SpellRecord() { name = "Paralysis", percentage = 50, cost = 42, effect = 10, negativeValue = 10, positiveValue = 0, duration = 1, guilds = new [] {false, false, true, false, false, false, false, true, false, false, false, false, true, false } },
            new SpellRecord() { name = "Prism", percentage = 50, cost = 42, effect = 10, negativeValue = 10, positiveValue = 0, duration = 1, guilds = new [] {false, false, true, false, false, false, false, false, false, false, false, false, false, false } },
            new SpellRecord() { name = "Protect from Evil", percentage = 50, cost = 42, effect = 10, negativeValue = 10, positiveValue = 0, duration = 1, guilds = new [] {false, false, true, false, false, false, false, false, true, true, false, false, false, false } },
            new SpellRecord() { name = "Protect from Good", percentage = 50, cost = 42, effect = 10, negativeValue = 10, positiveValue = 0, duration = 1, guilds = new [] {false, false, false, false, false, true, false, true, false, false, false, false, false, false } },
            new SpellRecord() { name = "Protection", percentage = 50, cost = 42, effect = 10, negativeValue = 10, positiveValue = 0, duration = 1, guilds = new [] {true, false, true, false, false, true, false, false, false, false, false, false, false, true } },
            new SpellRecord() { name = "Razoredge", percentage = 50, cost = 42, effect = 10, negativeValue = 10, positiveValue = 0, duration = 1, guilds = new [] {false, false, false, false, false, false, false, false, false, false, false, false, true, true } },
            new SpellRecord() { name = "Repair", percentage = 50, cost = 42, effect = 10, negativeValue = 10, positiveValue = 0, duration = 1, guilds = new [] {true, false, false, false, false, false, false, false, false, false, false, false, true, true } },
            new SpellRecord() { name = "Shadowmeld", percentage = 50, cost = 42, effect = 10, negativeValue = 10, positiveValue = 0, duration = 1, guilds = new [] {true, false, false, false, false, true, false, false, false, false, false, false, false, false } },
            new SpellRecord() { name = "Shield", percentage = 50, cost = 42, effect = 10, negativeValue = 10, positiveValue = 0, duration = 1, guilds = new [] {true, true, true, true, true, true, true, true, true, true, true, true, true, true } },
            new SpellRecord() { name = "Slay Evil", percentage = 50, cost = 42, effect = 10, negativeValue = 10, positiveValue = 0, duration = 1, guilds = new [] {false, false, false, false, false, false, false, false, false, true, false, false, false, false } },
            new SpellRecord() { name = "Slay Good", percentage = 50, cost = 42, effect = 10, negativeValue = 10, positiveValue = 0, duration = 1, guilds = new [] {false, false, false, false, false, false, false, true, false, false, false, false, false, false } },
            new SpellRecord() { name = "Speed", percentage = 50, cost = 42, effect = 10, negativeValue = 10, positiveValue = 0, duration = 1, guilds = new [] {true, false, true, false, false, false, false, false, false, false, false, false, false, false } },
            new SpellRecord() { name = "Strength", percentage = 50, cost = 42, effect = 10, negativeValue = 10, positiveValue = 0, duration = 1, guilds = new [] {false, false, false, false, false, false, false, true, true, false, false, false, false, true } },
            new SpellRecord() { name = "Super Vision",percentage = 50, cost = 42, effect = 10, negativeValue = 10, positiveValue = 0, duration = 1, guilds = new [] {true, false, true, false, false, false, false, true, false, true, false, false, true, false } },
            new SpellRecord() { name = "Vigor", percentage = 50, cost = 42, effect = 10, negativeValue = 10, positiveValue = 0, duration = 1, guilds = new [] { false, false, false, false, false, false, false, true, true, false, false, false, false, true } }
        };

        public static void AttemptSpell ( int spellRef )
        {
            // Attempt to cast the selected spell from "castSpells()"

            // spellRef = index within spellBuffer foe selected spell
            var spellNo = spellBuffer[spellRef].no;
            var spellPercentage = spellBuffer[spellRef].percentage;
            var spellDesc = spells[spellBuffer[spellRef].no].name;
            var spellPoints = Random(1, 5);

            var castProb = Random(0, 100);
            if (castProb < spellPercentage)
            {
                CastSpellMessage(spellDesc);
                plyr.ringCharges -= spellPoints;
                if (plyr.ringCharges < 0)
                    plyr.ringCharges = 0;

                // Check for specific spells and their effects
                if (spellNo == 4)
                    plyr.food++;
                if (spellNo == 5)
                    plyr.keys++;
                if (spellNo == 13)
                {
                    plyr.hp += Random(1, 10);
                    if (plyr.hp > plyr.maxhp)
                        plyr.hp = plyr.maxhp;
                }
                if (spellNo == 16)
                    DisplayLocation();
                if (spellNo == 8) // Dexterity
                {
                    effectBuffer[plyr.effectIndex].effect = 1; // Dexterity
                    effectBuffer[plyr.effectIndex].negativeValue = 0;
                    effectBuffer[plyr.effectIndex].positiveValue = 30; // +30 to plyr.dex
                    effectBuffer[plyr.effectIndex].duration = 8; // hours
                }
            } else
            {
                SpellBackfiredMessage(spellPoints);
                plyr.hp -= spellPoints;
                // NEED  NEW METHOD OF CHECKING HP REDUCTION TO CATCH DEATH!
            }
        }

        public static void CastSpellMessage ( string spellDesc )
        {
            var keynotpressed = true;
            PlaySpellSound();
            while (keynotpressed)
            {
                if (plyr.status == GameStates.Encounter)
                    DrawEncounterView();
                if (plyr.status == GameStates.Explore)
                    DispMain();

                CyText(3, $"You cast the spell of@@$ {spellDesc} $");
                UpdateDisplay();
                var key = GetSingleKey();
                if (key != "")
                    keynotpressed = false;
            }
        }

        public static void CastSpells ()
        {
            // Based on SelectItem code using "pages" of spells hence reference to "pages > 2" etc

            var itemRef = 9999; // Nothing selected           
            var selectDesc = "CAST";

            var menuitem1 = 255; // 255 is used here as nil
            var menuitem2 = 255;
            var menuitem3 = 255;
            var menuitem4 = 255;
            var selectDone = false;

            var no_items = plyr.spellIndex; // Number of spells in players inventory
            var page = 3;
            var pages = 0;

            var noPages = no_items / 4; // based on 4 on screen items per page
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
                        switch (plyr.status)
                        {
                            case GameStates.Encounter: DrawEncounterView(); break;
                            case GameStates.Explore:
                            case 0: DispMain(); break;
                        };
                        
                        CyText(1, selectDesc);
                        BText(5, 3, "(1)");
                        BText(5, 4, "(2)");
                        BText(5, 5, "(3)");
                        BText(5, 6, "(4)");
                        BText(2, 8, "Item #, Forward, Back, or ESC to exit");
                        SetFontColour(40, 96, 244, 255);
                        BText(2, 8, "     #  F        B        ESC");
                        SetFontColour(215, 215, 215, 255);

                        var page_item = 1;
                        var cur_idx = ((page - 3) * 4);
                        menuitem1 = 9999; // 9999 is used as nil
                        menuitem2 = 9999;
                        menuitem3 = 9999;
                        menuitem4 = 9999;

                        while ((cur_idx < plyr.spellIndex) && (page_item < 5))
                        {
                            var str = $"{spells[(spellBuffer[cur_idx].no)].name} {spellBuffer[cur_idx].percentage}%";
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
                } // page > 0 loop
            } // while cast not done

            if (itemRef != 9999)
                AttemptSpell(itemRef);
        }

        public static void SpellBackfiredMessage ( int spellPoints )
        {
            var keynotpressed = true;
            while (keynotpressed)
            {
                switch (plyr.status)
                {
                    case GameStates.Encounter: DrawEncounterView(); break;
                    case GameStates.Explore:
                    case (GameStates)0: DispMain(); break;
                };
                
                CyText(3, $"The spell failed@@and backfired for {spellPoints}!");
                UpdateDisplay();
                var key = GetSingleKey();
                if (key != "")
                    keynotpressed = false;
            }
            // update ring charges and hp
        }
    }
}