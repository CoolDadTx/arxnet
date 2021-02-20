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
using System.Data.OleDb;
using System.Drawing;
using System.IO;

namespace P3Net.Arx
{
    public static partial class GlobalMembers
    {
        public static void DisplayLoadGame ()
        {
            //TODO: Base this on size of game file list
            DrawText(1, 3, "(0)");
            DrawText(1, 5, "(1)");
            DrawText(1, 7, "(2)");
            DrawText(1, 9, "(3)");
            DrawText(1, 11, "(4)");
            DrawText(1, 13, "(5)");
            DrawText(1, 15, "(6)");
            DrawText(1, 17, "(7)");
            DrawText(1, 19, "(8)");
            DrawText(1, 21, "(9)");

            DrawText(8, 23, "Select 0-9 or ESC to cancel");

            for (var a = 0; a < 10; ++a) // number of save game slots 0 - 9
            {
                var str = saveGameDescriptions[a];
                DrawText(5, ((a * 2) + 3), str);
            }
        }

        public static void DisplaySaveGame ()
        {
            plyr.status = 0;
            var savegameMenu = 255; // high level menu

            while (savegameMenu < 256)
            {
                while (savegameMenu == 255) // main menu
                {
                    ClearDisplay();
                    DisplayLoadGame();
                    DrawText(12, 0, "Save a character");
                    UpdateDisplay();

                    var key = GetSingleKey();
                    if (key == "0")
                        savegameMenu = 0;
                    if (key == "1")
                        savegameMenu = 1;
                    if (key == "2")
                        savegameMenu = 2;
                    if (key == "3")
                        savegameMenu = 3;
                    if (key == "4")
                        savegameMenu = 4;
                    if (key == "5")
                        savegameMenu = 5;
                    if (key == "6")
                        savegameMenu = 6;
                    if (key == "7")
                        savegameMenu = 7;
                    if (key == "8")
                        savegameMenu = 8;
                    if (key == "9")
                        savegameMenu = 9;
                    if (key == "ESC")
                    {
                        savegameMenu = 256;
                        plyr.status = GameStates.Explore;
                    }
                }
                while (savegameMenu < 10) // attempt to save a character
                {
                    SaveCharacter(savegameMenu);
                    plyr.status = GameStates.Explore; // for display canvas
                    savegameMenu = 256;
                }
            }
        }

        public static void InitSaveGameDescriptions ()
        {
            //TODO: Ignoring fixed saved game slots 0-9
            saveGameDescriptions = File.ReadAllLines("data/saves/saveGames.txt");
        }

        //TODO: Why is load public but save private?
        public static bool LoadCharacter ( int saveSlot )
        {
            //TODO: Use a normal struct reader
            //TODO: Ignoring fixed saved game size - saveGameSize
            character = File.ReadAllLines($"data/saves/save{saveSlot}.txt");

            plyr.status = GameStates.Explore;

            plyr.gender = Convert.ToInt32(character[0]);
            plyr.hp = Convert.ToInt32(character[1]);
            plyr.maxhp = Convert.ToInt32(character[2]);

            plyr.scenario = (Scenarios)Convert.ToInt32(character[3]);
            plyr.map = Convert.ToInt32(character[4]);
            plyr.MapSize = new Size(Convert.ToInt32(character[5]), Convert.ToInt32(character[6]));

            //Cheating the system here but we'll set position to the "old" location first and then set the real position so it is cached
            plyr.Position = new Point(Convert.ToInt32(character[8]), Convert.ToInt32(character[10]));
            plyr.Position = new Point(Convert.ToInt32(character[7]), Convert.ToInt32(character[9]));
            
            plyr.facing = (Directions)Convert.ToInt32(character[11]);

            plyr.front = Convert.ToInt32(character[12]);
            plyr.back = Convert.ToInt32(character[13]);
            plyr.left = Convert.ToInt32(character[14]);
            plyr.right = Convert.ToInt32(character[15]);
            plyr.frontheight = Convert.ToInt32(character[16]);
            plyr.leftheight = Convert.ToInt32(character[17]);
            plyr.rightheight = Convert.ToInt32(character[18]);
            plyr.floorTexture = Convert.ToInt32(character[19]);
            plyr.ceiling = Convert.ToInt32(character[20]);
            plyr.location = Convert.ToInt32(character[21]);
            plyr.special = Convert.ToInt32(character[22]);

            plyr.alive = Convert.ToInt32(character[23]) != 0;

            plyr.teleporting = Convert.ToInt32(character[24]);
            plyr.buffer_index = Convert.ToInt32(character[25]);
            plyr.infoPanel = Convert.ToInt32(character[26]);
            plyr.priWeapon = Convert.ToInt32(character[27]);
            plyr.secWeapon = Convert.ToInt32(character[28]);
            plyr.headArmour = Convert.ToInt32(character[29]);
            plyr.bodyArmour = Convert.ToInt32(character[30]);
            plyr.legsArmour = Convert.ToInt32(character[31]);
            plyr.armsArmour = Convert.ToInt32(character[32]);
            plyr.timeOfDay = Convert.ToInt32(character[33]);
            plyr.minutes = Convert.ToInt32(character[34]);
            plyr.hours = Convert.ToInt32(character[35]);
            plyr.days = Convert.ToInt32(character[36]);
            plyr.months = Convert.ToInt32(character[37]);
            plyr.years = Convert.ToInt32(character[38]);

            plyr.sta = Convert.ToInt32(character[39]);
            plyr.chr = Convert.ToInt32(character[40]);
            plyr.str = Convert.ToInt32(character[41]);
            plyr.inte = Convert.ToInt32(character[42]);
            plyr.wis = Convert.ToInt32(character[43]);
            plyr.skl = Convert.ToInt32(character[44]);
            plyr.maxhp = Convert.ToInt32(character[45]);
            plyr.hp = Convert.ToInt32(character[46]);
            plyr.xp = Convert.ToInt32(character[47]);
            plyr.level = Convert.ToInt32(character[48]); // xp level
            plyr.chrPartials = Convert.ToInt32(character[49]);
            plyr.intPartials = Convert.ToInt32(character[50]);
            plyr.strPartials = Convert.ToInt32(character[51]);
            plyr.speed = Convert.ToInt32(character[52]);
            plyr.stealth = Convert.ToInt32(character[53]);
            plyr.diagOn = Convert.ToInt32(character[54]) != 0;
            plyr.mapOn = Convert.ToInt32(character[55]) != 0;
            plyr.fpsOn = Convert.ToInt32(character[56]) != 0;
            plyr.miniMapOn = Convert.ToInt32(character[57]) != 0;
            plyr.silver = Convert.ToInt32(character[58]);
            plyr.gold = Convert.ToInt32(character[59]);
            plyr.copper = Convert.ToInt32(character[60]);
            plyr.food = Convert.ToInt32(character[61]);
            plyr.torches = Convert.ToInt32(character[62]);
            plyr.water = Convert.ToInt32(character[63]);
            plyr.timepieces = Convert.ToInt32(character[64]);
            plyr.crystals = Convert.ToInt32(character[65]);
            plyr.jewels = Convert.ToInt32(character[66]);
            plyr.gems = Convert.ToInt32(character[67]);
            plyr.compasses = Convert.ToInt32(character[68]);
            plyr.keys = Convert.ToInt32(character[69]);
            plyr.encounter_done = Convert.ToInt32(character[70]) != 0;
            plyr.game_on = Convert.ToInt32(character[71]) != 0;
            plyr.gender = Convert.ToInt32(character[72]);
            plyr.zone = Convert.ToInt32(character[73]);
            plyr.zoneSet = Convert.ToInt32(character[74]);
            plyr.current_zone = Convert.ToInt32(character[75]); // used by drawing function
            plyr.status = (GameStates)Convert.ToInt32(character[76]);
            plyr.specialwall = Convert.ToInt32(character[77]);
            plyr.fixedEncounter = Convert.ToInt32(character[80]) != 0;
            plyr.fixedEncounterRef = Convert.ToInt32(character[81]);
            plyr.thirst = Convert.ToInt32(character[82]);
            plyr.hunger = Convert.ToInt32(character[83]);
            plyr.digestion = Convert.ToInt32(character[84]);
            plyr.alcohol = Convert.ToInt32(character[85]);

            for (var y = 0; y < plyr.guildAwards.Length; ++y)
                plyr.guildAwards[y] = Convert.ToInt32(character[86 + y]) != 0;
            for (var y = 0; y < plyr.fixedEncounters.Length; ++y)
                plyr.fixedEncounters[y] = Convert.ToInt32(character[98 + y]) != 0;
            for (var y = 0; y < plyr.guildMemberships.Length; ++y)
                plyr.guildMemberships[y] = Convert.ToInt32(character[130 + y]);

            plyr.ringCharges = Convert.ToInt32(character[144]);
            plyr.alignment = Convert.ToInt32(character[145]);
            plyr.lfood = Convert.ToInt32(character[146]);
            plyr.lwater = Convert.ToInt32(character[147]);
            plyr.ltorches = Convert.ToInt32(character[148]);
            plyr.ltimepieces = Convert.ToInt32(character[149]);
            plyr.lcompasses = Convert.ToInt32(character[150]);
            plyr.lkeys = Convert.ToInt32(character[151]);
            plyr.lcrystals = Convert.ToInt32(character[152]);
            plyr.lgems = Convert.ToInt32(character[153]);
            plyr.ljewels = Convert.ToInt32(character[154]);
            plyr.lgold = Convert.ToInt32(character[155]);
            plyr.lsilver = Convert.ToInt32(character[156]);
            plyr.lcopper = Convert.ToInt32(character[157]);
            plyr.spellIndex = Convert.ToInt32(character[158]);
            plyr.effectIndex = Convert.ToInt32(character[159]);
            plyr.retreatFriendship = Convert.ToInt32(character[160]);
            plyr.damonFriendship = Convert.ToInt32(character[161]);

            //TODO: Switch to not using fixed arrays
            plyr.smithyFriendships[0] = Convert.ToInt32(character[162]);
            plyr.smithyFriendships[1] = Convert.ToInt32(character[163]);
            plyr.smithyFriendships[2] = Convert.ToInt32(character[164]);
            plyr.smithyFriendships[3] = Convert.ToInt32(character[165]);

            plyr.bankAccountStatuses[0] = Convert.ToInt32(character[166]);
            plyr.bankAccountStatuses[1] = Convert.ToInt32(character[167]);
            plyr.bankAccountStatuses[2] = Convert.ToInt32(character[168]);
            plyr.bankAccountStatuses[3] = Convert.ToInt32(character[169]);
            plyr.bankAccountStatuses[4] = Convert.ToInt32(character[170]);
            plyr.bankAccountStatuses[5] = Convert.ToInt32(character[171]);
            plyr.bankAccountStatuses[6] = Convert.ToInt32(character[172]);
            plyr.bankAccountStatuses[7] = Convert.ToInt32(character[173]);
            plyr.bankAccountStatuses[8] = Convert.ToInt32(character[174]);

            plyr.bankAccountBalances[0] = Convert.ToInt32(character[175]);
            plyr.bankAccountBalances[1] = Convert.ToInt32(character[176]);
            plyr.bankAccountBalances[2] = Convert.ToInt32(character[177]);
            plyr.bankAccountBalances[3] = Convert.ToInt32(character[178]);
            plyr.bankAccountBalances[4] = Convert.ToInt32(character[179]);
            plyr.bankAccountBalances[5] = Convert.ToInt32(character[180]);
            plyr.bankAccountBalances[6] = Convert.ToInt32(character[181]);
            plyr.bankAccountBalances[7] = Convert.ToInt32(character[182]);
            plyr.bankAccountBalances[8] = Convert.ToInt32(character[183]);

            plyr.clothing[0] = Convert.ToInt32(character[184]);
            plyr.clothing[1] = Convert.ToInt32(character[185]);
            plyr.clothing[2] = Convert.ToInt32(character[186]);
            plyr.clothing[3] = Convert.ToInt32(character[187]);

            //TODO: Consider moving these into property bag so we don't have to work with this data directly, only the interested code
            plyr.goblinsVisited = Convert.ToInt32(character[188]) != 0;
            plyr.goblinsChallenged = Convert.ToInt32(character[189]) != 0;
            plyr.goblinsDefeated = Convert.ToInt32(character[190]) != 0;
            plyr.goblinsCombat = Convert.ToInt32(character[191]) != 0;
            plyr.goblinsReforged = Convert.ToInt32(character[192]) != 0;
            plyr.trollsVisited = Convert.ToInt32(character[193]) != 0;
            plyr.trollsChallenged = Convert.ToInt32(character[194]) != 0;
            plyr.trollsDefeated = Convert.ToInt32(character[195]) != 0;
            plyr.trollsCombat = Convert.ToInt32(character[196]) != 0;
            plyr.trollsReforged = Convert.ToInt32(character[197]) != 0;

            plyr.oracleReturnTomorrow = Convert.ToInt32(character[198]) != 0;
            plyr.oracleDay = Convert.ToInt32(character[199]);
            plyr.oracleMonth = Convert.ToInt32(character[200]);
            plyr.oracleYear = Convert.ToInt32(character[201]);
            plyr.oracleQuestNo = Convert.ToInt32(character[202]);
            plyr.healerDays[0] = Convert.ToInt32(character[203]);
            plyr.healerDays[1] = Convert.ToInt32(character[204]);
            plyr.healerHours[0] = Convert.ToInt32(character[205]);
            plyr.healerHours[1] = Convert.ToInt32(character[206]);
            plyr.healerMinutes[0] = Convert.ToInt32(character[207]);
            plyr.healerMinutes[1] = Convert.ToInt32(character[208]);
            plyr.treasureFinding = Convert.ToInt32(character[209]);
            plyr.invisibility = Convert.ToInt32(character[210]);
            plyr.diseases[0] = Convert.ToInt32(character[211]);
            plyr.diseases[1] = Convert.ToInt32(character[212]);
            plyr.diseases[2] = Convert.ToInt32(character[213]);
            plyr.diseases[3] = Convert.ToInt32(character[214]);
            plyr.poison[0] = Convert.ToInt32(character[215]);

            plyr.poison[1] = Convert.ToInt32(character[216]);
            plyr.poison[2] = Convert.ToInt32(character[217]);
            plyr.poison[3] = Convert.ToInt32(character[218]);
            plyr.delusion = Convert.ToInt32(character[219]);
            for (var y = 0; y < 9; ++y)
                plyr.invulnerability[y] = Convert.ToInt32(character[220 + y]);
            plyr.noticeability = Convert.ToInt32(character[229]);
            plyr.protection1 = Convert.ToInt32(character[230]);
            plyr.protection2 = Convert.ToInt32(character[231]);

            plyr.forgeDays = Convert.ToInt32(character[232]);
            plyr.forgeType = Convert.ToInt32(character[233]);
            plyr.forgeBonus = Convert.ToInt32(character[234]);
            plyr.forgeName = character[235];
            plyr.stolenFromVault = Convert.ToInt32(character[236]);

            var loadGameIndex = 400; // start location for object buffer items
            for (var z = 0; z < itemBufferSize; ++z)
            {
                itemBuffer[z].hp = Convert.ToInt32(character[loadGameIndex]);
                itemBuffer[z].index = Convert.ToInt32(character[loadGameIndex + 1]);
                itemBuffer[z].level = Convert.ToInt32(character[loadGameIndex + 2]);
                itemBuffer[z].location = Convert.ToInt32(character[loadGameIndex + 3]);
                itemBuffer[z].type = Convert.ToInt32(character[loadGameIndex + 4]);
                itemBuffer[z].Position = new Point(Convert.ToInt32(character[loadGameIndex + 5]), Convert.ToInt32(character[loadGameIndex + 6]));

                itemBuffer[z].name = character[loadGameIndex + 7];
                itemBuffer[z].maxHP = Convert.ToInt32(character[loadGameIndex + 8]);
                itemBuffer[z].flags = Convert.ToInt32(character[loadGameIndex + 9]);
                itemBuffer[z].minStrength = Convert.ToInt32(character[loadGameIndex + 10]);
                itemBuffer[z].minDexterity = Convert.ToInt32(character[loadGameIndex + 11]);
                itemBuffer[z].useStrength = Convert.ToInt32(character[loadGameIndex + 12]);
                itemBuffer[z].blunt = Convert.ToInt32(character[loadGameIndex + 13]);
                itemBuffer[z].sharp = Convert.ToInt32(character[loadGameIndex + 14]);
                itemBuffer[z].earth = Convert.ToInt32(character[loadGameIndex + 15]);
                itemBuffer[z].air = Convert.ToInt32(character[loadGameIndex + 16]);
                itemBuffer[z].fire = Convert.ToInt32(character[loadGameIndex + 17]);
                itemBuffer[z].water = Convert.ToInt32(character[loadGameIndex + 18]);
                itemBuffer[z].power = Convert.ToInt32(character[loadGameIndex + 19]);
                itemBuffer[z].magic = Convert.ToInt32(character[loadGameIndex + 20]); // mental
                itemBuffer[z].good = Convert.ToInt32(character[loadGameIndex + 21]); // cleric
                itemBuffer[z].evil = Convert.ToInt32(character[loadGameIndex + 22]);
                itemBuffer[z].cold = Convert.ToInt32(character[loadGameIndex + 23]);
                itemBuffer[z].weight = Convert.ToInt32(character[loadGameIndex + 24]);
                itemBuffer[z].alignment = Convert.ToInt32(character[loadGameIndex + 25]);
                itemBuffer[z].melee = Convert.ToInt32(character[loadGameIndex + 26]);
                itemBuffer[z].ammo = Convert.ToInt32(character[loadGameIndex + 27]);
                itemBuffer[z].parry = Convert.ToInt32(character[loadGameIndex + 28]);

                loadGameIndex += 28;
            }

            //TODO: Move away from offsets
            // Copy spell buffer
            loadGameIndex = 7400; // start location for spell buffer items (70 bytes)
            for (var z = 0; z < spellBuffer.Length; ++z)
            {
                spellBuffer[z].no = Convert.ToInt32(character[loadGameIndex]);
                spellBuffer[z].percentage = Convert.ToInt32(character[loadGameIndex + 1]);
                loadGameIndex += 2;
            }

            //TODO: Move away from offsets
            // Copy effect buffer
            loadGameIndex = 7470; // start location for effect buffer items (200 bytes)
            for (var z = 0; z < effectBuffer.Length; ++z)
            {
                effectBuffer[z].effect = Convert.ToInt32(character[loadGameIndex]);
                effectBuffer[z].negativeValue = Convert.ToInt32(character[loadGameIndex + 1]);
                effectBuffer[z].positiveValue = Convert.ToInt32(character[loadGameIndex + 2]);
                effectBuffer[z].duration = Convert.ToInt32(character[loadGameIndex + 3]);
                loadGameIndex += 4;
            }

            //TODO: Move away from offsets
            // Smithy daily wares
            loadGameIndex = 7670;
            for (var z = 0; z < 4; ++z)
            {
                for (var x = 0; x < 10; ++x)
                {
                    smithyDailyWares[z, x] = Convert.ToInt32(character[loadGameIndex]);
                    loadGameIndex++;
                }
            }

            //TODO: Move away from offsets
            loadGameIndex = 7710;
            for (var z = 0; z < 14; ++z)
            {
                for (var x = 0; x < 6; ++x)
                {
                    tavernDailyFoods[z, x] = Convert.ToInt32(character[loadGameIndex]);
                    loadGameIndex++;
                }
            }

            //TODO: Move away from offsets
            loadGameIndex = 7794;
            for (var z = 0; z < 14; ++z)
            {
                for (var x = 0; x < 6; ++x)
                {
                    tavernDailyDrinks[z, x] = Convert.ToInt32(character[loadGameIndex]);
                    loadGameIndex++;
                }
            }

            //TODO: Move away from offsets
            loadGameIndex = 7878;
            for (var z = 0; z < 15; ++z)
            {
                for (var x = 0; x < 12; ++x)
                {
                    shopDailyWares[z, x] = Convert.ToInt32(character[loadGameIndex]);
                    loadGameIndex++;
                }
            }

            // Currently inn and tavern job openings are not part of saved game

            //TODO: Move away from offsets
            // load automap flags
            loadGameIndex = 8058; // start location for object buffer items
            for (var z = 0; z < 5; ++z)
            {
                for (var x = 0; x < 4096; ++x)
                {
                    autoMapExplored[z, x] = Convert.ToInt32(character[loadGameIndex]) != 0;
                    loadGameIndex++;
                }
            }

            plyr.name = character[28538];
            
            plyr.z_offset = Convert.ToSingle(character[28539]);

            return true;
        }

        #region Review Data

        // Each character save game file is made up of a 28540 element string array.
        // Each array element can hold a number or text string (e.g. no. of torches carried or an item name)

        // NOTES:
        // job openings not currently part of saved game

        public static string[] character = new string[saveGameSize];
        public static string[] saveGameDescriptions = new string[10];
        public static readonly int saveGameSize = 28541;
        #endregion

        #region Private Members

        private static void Initcharacter ()
        {
            for (var i = 0; i < saveGameSize; i++)
                character[i] = "<BLANK>";
        }        
        
        private static bool SaveCharacter ( int saveSlot )
        {
            //TODO: Use a regular struct system
            
            saveGameDescriptions[saveSlot] = plyr.name;
            UpdateSaveGameDescriptions();

            Initcharacter(); // Clear out string array

            // Copy character object data (except name) into the character[4096] int block
            character[0] = plyr.gender.ToString();
            character[1] = plyr.hp.ToString();
            character[2] = plyr.maxhp.ToString();
            character[3] = plyr.scenario.ToString();
            character[4] = plyr.map.ToString();

            //TODO: Save struct
            character[5] = plyr.MapSize.Width.ToString();
            character[6] = plyr.MapSize.Height.ToString();

            //TODO: Save struct
            character[7] = plyr.Position.X.ToString();
            character[9] = plyr.Position.Y.ToString();

            //TODO: Save struct
            character[8] = plyr.OldLocation.X.ToString();
            character[10] = plyr.OldLocation.Y.ToString();

            character[11] = plyr.facing.ToString();
            character[12] = plyr.front.ToString();
            character[13] = plyr.back.ToString();
            character[14] = plyr.left.ToString();
            character[15] = plyr.right.ToString();
            character[16] = plyr.frontheight.ToString();
            character[17] = plyr.leftheight.ToString();
            character[18] = plyr.rightheight.ToString();
            character[19] = plyr.floorTexture.ToString();
            character[20] = plyr.ceiling.ToString();
            character[21] = plyr.location.ToString();
            character[22] = plyr.special.ToString();
            character[23] = plyr.alive.ToString();
            character[24] = plyr.teleporting.ToString();
            character[25] = plyr.buffer_index.ToString();
            character[26] = plyr.infoPanel.ToString();
            character[27] = plyr.priWeapon.ToString();
            character[28] = plyr.secWeapon.ToString();
            character[29] = plyr.headArmour.ToString();
            character[30] = plyr.bodyArmour.ToString();
            character[31] = plyr.legsArmour.ToString();
            character[32] = plyr.armsArmour.ToString();
            character[33] = plyr.timeOfDay.ToString();
            character[34] = plyr.minutes.ToString();
            character[35] = plyr.hours.ToString();
            character[36] = plyr.days.ToString();
            character[37] = plyr.months.ToString();
            character[38] = plyr.years.ToString();

            character[39] = plyr.sta.ToString();
            character[40] = plyr.chr.ToString();
            character[41] = plyr.str.ToString();
            character[42] = plyr.inte.ToString();
            character[43] = plyr.wis.ToString();
            character[44] = plyr.skl.ToString();
            character[45] = plyr.maxhp.ToString();
            character[46] = plyr.hp.ToString();
            character[47] = plyr.xp.ToString();
            character[48] = plyr.level.ToString(); // xp level
            character[49] = plyr.chrPartials.ToString();
            character[50] = plyr.intPartials.ToString();
            character[51] = plyr.strPartials.ToString();
            character[52] = plyr.speed.ToString();
            character[53] = plyr.stealth.ToString();
            character[54] = plyr.diagOn.ToString();
            character[55] = plyr.mapOn.ToString();
            character[56] = plyr.fpsOn.ToString();
            character[57] = plyr.miniMapOn.ToString();
            character[58] = plyr.silver.ToString();
            character[59] = plyr.gold.ToString();
            character[60] = plyr.copper.ToString();
            character[61] = plyr.food.ToString();
            character[62] = plyr.torches.ToString();
            character[63] = plyr.water.ToString();
            character[64] = plyr.timepieces.ToString();
            character[65] = plyr.crystals.ToString();
            character[66] = plyr.jewels.ToString();
            character[67] = plyr.gems.ToString();
            character[68] = plyr.compasses.ToString();
            character[69] = plyr.keys.ToString();

            character[70] = plyr.encounter_done.ToString();
            character[71] = plyr.game_on.ToString();
            character[72] = plyr.gender.ToString();

            character[73] = plyr.zone.ToString();
            character[74] = plyr.zoneSet.ToString();
            character[75] = plyr.current_zone.ToString(); // used by drawing function
            character[76] = plyr.status.ToString();
            character[77] = plyr.specialwall.ToString();
            character[80] = plyr.fixedEncounter.ToString();
            character[81] = plyr.fixedEncounterRef.ToString();
            character[82] = plyr.thirst.ToString();
            character[83] = plyr.hunger.ToString();
            character[84] = plyr.digestion.ToString();
            character[85] = plyr.alcohol.ToString();

            //TODO: Do not use offsets
            for (var y = 0; y < plyr.guildAwards.Length; ++y)
                character[86 + y] = plyr.guildAwards[y].ToString();
            for (var y = 0; y < plyr.fixedEncounters.Length; ++y)
                character[98 + y] = plyr.fixedEncounters[y].ToString();
            for (var y = 0; y < plyr.guildMemberships.Length; ++y)
                character[130 + y] = plyr.guildMemberships[y].ToString();

            //TODO: Do not use offsets
            character[144] = plyr.ringCharges.ToString();
            character[145] = plyr.alignment.ToString();
            character[146] = plyr.lfood.ToString();
            character[147] = plyr.lwater.ToString();
            character[148] = plyr.ltorches.ToString();
            character[149] = plyr.ltimepieces.ToString();
            character[150] = plyr.lcompasses.ToString();
            character[151] = plyr.lkeys.ToString();
            character[152] = plyr.lcrystals.ToString();
            character[153] = plyr.lgems.ToString();
            character[154] = plyr.ljewels.ToString();
            character[155] = plyr.lgold.ToString();
            character[156] = plyr.lsilver.ToString();
            character[157] = plyr.lcopper.ToString();
            character[158] = plyr.spellIndex.ToString();
            character[159] = plyr.effectIndex.ToString();
            character[160] = plyr.retreatFriendship.ToString();
            character[161] = plyr.damonFriendship.ToString();

            //TODO: Do not use offsets
            for (var y = 0; y < plyr.smithyFriendships.Length; ++y)
                character[162 + y] = plyr.smithyFriendships[y].ToString();
            for (var y = 0; y < plyr.bankAccountStatuses.Length; ++y)
                character[166 + y] = plyr.bankAccountStatuses[y].ToString();
            for (var y = 0; y < plyr.bankAccountBalances.Length; ++y)
                character[175 + y] = plyr.bankAccountBalances[y].ToString();
            for (var y = 0; y < plyr.clothing.Length; ++y)
                character[184 + y] = plyr.clothing[y].ToString();

            //TODO: Do not use offsets
            character[188] = plyr.goblinsVisited.ToString();
            character[189] = plyr.goblinsChallenged.ToString();
            character[190] = plyr.goblinsDefeated.ToString();
            character[191] = plyr.goblinsCombat.ToString();
            character[192] = plyr.goblinsReforged.ToString();
            character[193] = plyr.trollsVisited.ToString();
            character[194] = plyr.trollsChallenged.ToString();
            character[195] = plyr.trollsDefeated.ToString();
            character[196] = plyr.trollsCombat.ToString();
            character[197] = plyr.trollsReforged.ToString();

            character[198] = plyr.oracleReturnTomorrow.ToString();
            character[199] = plyr.oracleDay.ToString();
            character[200] = plyr.oracleMonth.ToString();
            character[201] = plyr.oracleYear.ToString();
            character[202] = plyr.oracleQuestNo.ToString();

            //TODO: Do not use offsets
            //TODO: Save TimeSpan
            for (var y = 0; y < plyr.healerDays.Length; ++y)
                character[203 + y] = plyr.healerDays[y].ToString();
            for (var y = 0; y < plyr.healerHours.Length; ++y)
                character[205 + y] = plyr.healerHours[y].ToString();
            for (var y = 0; y < plyr.healerMinutes.Length; ++y)
                character[207 + y] = plyr.healerMinutes[y].ToString();
            
            character[209] = plyr.treasureFinding.ToString();
            character[210] = plyr.invisibility.ToString();

            //TODO: Do not use offsets
            for (var y = 0; y < plyr.diseases.Length; ++y)
                character[211 + y] = plyr.diseases[y].ToString();
            for (var y = 0; y < plyr.poison.Length; ++y)
                character[215 + y] = plyr.poison[y].ToString();
            character[219] = plyr.delusion.ToString();

            //TODO: Do not use offsets
            for (var y = 0; y < plyr.invulnerability.Length; ++y)
                character[220 + y] = plyr.invulnerability[y].ToString();
            character[229] = plyr.noticeability.ToString();
            character[230] = plyr.protection1.ToString();
            character[231] = plyr.protection2.ToString();

            character[232] = plyr.forgeDays.ToString();
            character[233] = plyr.forgeType.ToString();
            character[234] = plyr.forgeBonus.ToString();
            character[235] = plyr.forgeName;
            character[236] = plyr.stolenFromVault.ToString();

            character[399] = "Line 400: Item Buffer follows";

            // Copy item buffer
            //TODO: Do not use offsets
            //TODO: Save struct
            var saveGameIndex = 400; // start location for object buffer items
            for (var z = 0; z < itemBuffer.Length; ++z)
            {
                character[saveGameIndex] = itemBuffer[z].hp.ToString();
                character[saveGameIndex + 1] = itemBuffer[z].index.ToString();
                character[saveGameIndex + 2] = itemBuffer[z].level.ToString();
                character[saveGameIndex + 3] = itemBuffer[z].location.ToString();
                character[saveGameIndex + 4] = itemBuffer[z].type.ToString();
                character[saveGameIndex + 5] = itemBuffer[z].Position.X.ToString();
                character[saveGameIndex + 6] = itemBuffer[z].Position.Y.ToString();

                character[saveGameIndex + 7] = itemBuffer[z].name;
                character[saveGameIndex + 8] = itemBuffer[z].maxHP.ToString();
                character[saveGameIndex + 9] = itemBuffer[z].flags.ToString();
                character[saveGameIndex + 10] = itemBuffer[z].minStrength.ToString();
                character[saveGameIndex + 11] = itemBuffer[z].minDexterity.ToString();
                character[saveGameIndex + 12] = itemBuffer[z].useStrength.ToString();
                character[saveGameIndex + 13] = itemBuffer[z].blunt.ToString();
                character[saveGameIndex + 14] = itemBuffer[z].sharp.ToString();
                character[saveGameIndex + 15] = itemBuffer[z].earth.ToString();
                character[saveGameIndex + 16] = itemBuffer[z].air.ToString();
                character[saveGameIndex + 17] = itemBuffer[z].fire.ToString();
                character[saveGameIndex + 18] = itemBuffer[z].water.ToString();
                character[saveGameIndex + 19] = itemBuffer[z].power.ToString();
                character[saveGameIndex + 20] = itemBuffer[z].magic.ToString(); // mental
                character[saveGameIndex + 21] = itemBuffer[z].good.ToString(); // cleric
                character[saveGameIndex + 22] = itemBuffer[z].evil.ToString();
                character[saveGameIndex + 23] = itemBuffer[z].cold.ToString();
                character[saveGameIndex + 24] = itemBuffer[z].weight.ToString();
                character[saveGameIndex + 25] = itemBuffer[z].alignment.ToString();
                character[saveGameIndex + 26] = itemBuffer[z].melee.ToString();
                character[saveGameIndex + 27] = itemBuffer[z].ammo.ToString();
                character[saveGameIndex + 28] = itemBuffer[z].parry.ToString();

                saveGameIndex += 28;
            }

            // Copy spell buffer
            //TODO: Do not use offsets
            //TODO: Save struct
            saveGameIndex = 7400; // start location for spell buffer items (70 bytes)
            for (var z = 0; z < spellBuffer.Length; ++z)
            {
                character[saveGameIndex] = spellBuffer[z].no.ToString();
                character[saveGameIndex + 1] = spellBuffer[z].percentage.ToString();
                saveGameIndex += 2;
            }

            // Copy effect buffer
            //TODO: Do not use offsets
            //TODO: Save struct
            saveGameIndex = 7470; // start location for effect buffer items (200 bytes)
            for (var z = 0; z < effectBuffer.Length; ++z)
            {
                character[saveGameIndex] = effectBuffer[z].effect.ToString();
                character[saveGameIndex + 1] = effectBuffer[z].negativeValue.ToString();
                character[saveGameIndex + 2] = effectBuffer[z].positiveValue.ToString();
                character[saveGameIndex + 3] = effectBuffer[z].duration.ToString();
                saveGameIndex += 4;
            }

            //TODO: Do not use offsets
            saveGameIndex = 7670; // start location for object buffer items
            for (var z = 0; z < 4; ++z)
            {
                for (var x = 0; x < 10; ++x)
                {
                    character[saveGameIndex] = smithyDailyWares[z, x].ToString();
                    saveGameIndex++;
                }
            }

            //TODO: Do not use offsets
            saveGameIndex = 7710; // start location for object buffer items
            for (var z = 0; z < 14; ++z)
            {
                for (var x = 0; x < 6; ++x)
                {
                    character[saveGameIndex] = tavernDailyFoods[z, x].ToString();
                    saveGameIndex++;
                }
            }

            //TODO: Do not use offsets
            saveGameIndex = 7794; // start location for object buffer items
            for (var z = 0; z < 14; ++z)
            {
                for (var x = 0; x < 6; ++x)
                {
                    character[saveGameIndex] = tavernDailyDrinks[z, x].ToString();
                    saveGameIndex++;
                }
            }

            //TODO: Do not use offsets
            saveGameIndex = 7878; // start location for object buffer items
            for (var z = 0; z < 15; ++z)
            {
                for (var x = 0; x < 12; ++x)
                {
                    character[saveGameIndex] = shopDailyWares[z, x].ToString();
                    saveGameIndex++;
                }
            }

            //TODO: Do not use offsets
            // Currently inn and tavern job openings are not part of saved game
            saveGameIndex = 8058; // start location for automapexplored
            for (var z = 0; z < 5; ++z)
            {
                for (var x = 0; x < 4096; ++x)
                {
                    character[saveGameIndex] = autoMapExplored[z, x].ToString();
                    saveGameIndex++;
                }
            }

            //TODO: Do not use offsets
            character[28538] = plyr.name;
            character[28539] = plyr.z_offset.ToString();
            character[28540] = "Release 0.80";

            File.WriteAllLines($"data/saves/save{saveSlot}.txt", character);            
            return true;
        }

        //TODO: Make this part of save game data
        private static void UpdateSaveGameDescriptions ()
        {
            File.WriteAllLines("data/saves/saveGames.txt", saveGameDescriptions);
        }
        #endregion
    }
}