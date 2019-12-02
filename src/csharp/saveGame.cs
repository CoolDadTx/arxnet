using System;
using System.Collections.Generic;
using System.Linq;
namespace P3Net.Arx
{
	public partial class GlobalMembers
	{
		//#include <SFML/Graphics.hpp>




		public static readonly int saveGameSize = 28541;




		public static void DisplayLoadGame()
		{
			string str;
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

			for (int a = 0; a < 10; ++a) // number of save game slots 0 - 9
			{
				str = saveGameDescriptions[a];
				DrawText(5, ((a * 2) + 3), str);
			}

		}
		public static void DisplaySaveGame()
		{
			string key;
			plyr.status = 0;
			{
					int savegameMenu = 255; // high level menu



					while (savegameMenu < 256)
					{
						while (savegameMenu == 255) // main menu
						{
							ClearDisplay();
							DisplayLoadGame();
							DrawText(12, 0, "Save a character");
							UpdateDisplay();

							key = GetSingleKey();
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
								plyr.status = 1;
							}
						}
						while (savegameMenu < 10) // attempt to save a character
						{
							SaveCharacter(savegameMenu);
							plyr.status = 1; // for display canvas
							savegameMenu = 256;
						}
					}

			}

		}

		public static void InitSaveGameDescriptions()
		{
			std::ifstream instream = new std::ifstream();
			instream.open("data/saves/saveGames.txt");
			string text;

			for (int a = 0; a < 10; a++) // number of save game slots 0 - 9
			{
				getline(instream, text);
				saveGameDescriptions[a] = text;
			}
			instream.close();
		}
		public static void UpdateSaveGameDescriptions()
		{
			ofstream outdata = new ofstream(); // outdata is like cin
			outdata.open("data/saves/saveGames.txt"); // opens the file

			for (int y = 0; y < 10; ++y)
				outdata << saveGameDescriptions[y] << "\n";
			outdata.close();
		}

		public static void Initcharacter()
		{
			for (int i = 0 ; i < saveGameSize ; i++)
				character[i] = "<BLANK>";
		}

		public static bool SaveCharacter(int saveSlot)
		{
			ofstream outdata = new ofstream();

			saveGameDescriptions[saveSlot] = plyr.name;
			UpdateSaveGameDescriptions();

			Initcharacter(); // Clear out string array

			// Copy character object data (except name) into the character[4096] int block
			character[0] = Itos(plyr.gender);
			character[1] = Itos(plyr.hp);
			character[2] = Itos(plyr.maxhp);
			character[3] = Itos(plyr.scenario);
			character[4] = Itos(plyr.map);
			character[5] = Itos(plyr.mapWidth);
			character[6] = Itos(plyr.mapHeight);
			character[7] = Itos(plyr.x);
			character[8] = Itos(plyr.oldx);
			character[9] = Itos(plyr.y);
			character[10] = Itos(plyr.oldy);
			character[11] = Itos(plyr.facing);
			character[12] = Itos(plyr.front);
			character[13] = Itos(plyr.back);
			character[14] = Itos(plyr.left);
			character[15] = Itos(plyr.right);
			character[16] = Itos(plyr.frontheight);
			character[17] = Itos(plyr.leftheight);
			character[18] = Itos(plyr.rightheight);
			character[19] = Itos(plyr.floorTexture);
			character[20] = Itos(plyr.ceiling);
			character[21] = Itos(plyr.location);
			character[22] = Itos(plyr.special);
			character[23] = Itos(plyr.alive);
			character[24] = Itos(plyr.teleporting);
			character[25] = Itos(plyr.buffer_index);
			character[26] = Itos(plyr.infoPanel);
			character[27] = Itos(plyr.priWeapon);
			character[28] = Itos(plyr.secWeapon);
			character[29] = Itos(plyr.headArmour);
			character[30] = Itos(plyr.bodyArmour);
			character[31] = Itos(plyr.legsArmour);
			character[32] = Itos(plyr.armsArmour);
			character[33] = Itos(plyr.timeOfDay);
			character[34] = Itos(plyr.minutes);
			character[35] = Itos(plyr.hours);
			character[36] = Itos(plyr.days);
			character[37] = Itos(plyr.months);
			character[38] = Itos(plyr.years);

			character[39] = Itos(plyr.sta);
			  character[40] = Itos(plyr.chr);
			  character[41] = Itos(plyr.str);
			  character[42] = Itos(plyr.inte);
			  character[43] = Itos(plyr.wis);
			  character[44] = Itos(plyr.skl);
			  character[45] = Itos(plyr.maxhp);
			  character[46] = Itos(plyr.hp);
			  character[47] = Itos(plyr.xp);
			  character[48] = Itos(plyr.level); // xp level
			  character[49] = Itos(plyr.chrPartials);
			  character[50] = Itos(plyr.intPartials);
			  character[51] = Itos(plyr.strPartials);
			  character[52] = Itos(plyr.speed);
			 character[53] = Itos(plyr.stealth);
			  character[54] = Itos(plyr.diagOn);
			  character[55] = Itos(plyr.mapOn);
			 character[56] = Itos(plyr.fpsOn);
			  character[57] = Itos(plyr.miniMapOn);
			  character[58] = Itos(plyr.silver);
			  character[59] = Itos(plyr.gold);
			  character[60] = Itos(plyr.copper);
			  character[61] = Itos(plyr.food);
			  character[62] = Itos(plyr.torches);
			  character[63] = Itos(plyr.water);
			  character[64] = Itos(plyr.timepieces);
			  character[65] = Itos(plyr.crystals);
			  character[66] = Itos(plyr.jewels);
			 character[67] = Itos(plyr.gems);
			 character[68] = Itos(plyr.compasses);
			  character[69] = Itos(plyr.keys);

			  character[70] = Itos(plyr.encounter_done);
			  character[71] = Itos(plyr.game_on);
			  character[72] = Itos(plyr.gender);

			  character[73] = Itos(plyr.zone);
			  character[74] = Itos(plyr.zoneSet);
			  character[75] = Itos(plyr.current_zone); // used by drawing function
			  character[76] = Itos(plyr.status);
			  character[77] = Itos(plyr.specialwall);
		//					  character[78] = itos( plyr.windowSize);
		//					  character[79] = itos( plyr.graphicStyle);
			  character[80] = Itos(plyr.fixedEncounter);
			  character[81] = Itos(plyr.fixedEncounterRef);
			  character[82] = Itos(plyr.thirst);
			  character[83] = Itos(plyr.hunger);
			 character[84] = Itos(plyr.digestion);
			  character[85] = Itos(plyr.alcohol);

			for (int y = 0; y < 12; ++y)
				character[86 + y] = Itos(plyr.guildAwards[y]);
			for (int y = 0; y < 32; ++y)
				character[98 + y] = Itos(plyr.fixedEncounters[y]);
			for (int y = 0; y < 14; ++y)
				character[130 + y] = Itos(plyr.guildMemberships[y]);

			  character[144] = Itos(plyr.ringCharges);
			  character[145] = Itos(plyr.alignment);
			  character[146] = Itos(plyr.lfood);
			  character[147] = Itos(plyr.lwater);
			  character[148] = Itos(plyr.ltorches);
			  character[149] = Itos(plyr.ltimepieces);
			  character[150] = Itos(plyr.lcompasses);
			  character[151] = Itos(plyr.lkeys);
			  character[152] = Itos(plyr.lcrystals);
			  character[153] = Itos(plyr.lgems);
			  character[154] = Itos(plyr.ljewels);
			  character[155] = Itos(plyr.lgold);
			  character[156] = Itos(plyr.lsilver);
			  character[157] = Itos(plyr.lcopper);
			  character[158] = Itos(plyr.spellIndex);
			  character[159] = Itos(plyr.effectIndex);
			  character[160] = Itos(plyr.retreatFriendship);
			  character[161] = Itos(plyr.damonFriendship);

			  character[162] = Itos(plyr.smithyFriendships[0]);
			  character[163] = Itos(plyr.smithyFriendships[1]);
			  character[164] = Itos(plyr.smithyFriendships[2]);
			  character[165] = Itos(plyr.smithyFriendships[3]);

				character[166] = Itos(plyr.bankAccountStatuses[0]);
				character[167] = Itos(plyr.bankAccountStatuses[1]);
				character[168] = Itos(plyr.bankAccountStatuses[2]);
				character[169] = Itos(plyr.bankAccountStatuses[3]);
				character[170] = Itos(plyr.bankAccountStatuses[4]);
				character[171] = Itos(plyr.bankAccountStatuses[5]);
				character[172] = Itos(plyr.bankAccountStatuses[6]);
				character[173] = Itos(plyr.bankAccountStatuses[7]);
				character[174] = Itos(plyr.bankAccountStatuses[8]);

				character[175] = Itos(plyr.bankAccountBalances[0]);
				character[176] = Itos(plyr.bankAccountBalances[1]);
				character[177] = Itos(plyr.bankAccountBalances[2]);
				character[178] = Itos(plyr.bankAccountBalances[3]);
				character[179] = Itos(plyr.bankAccountBalances[4]);
				character[180] = Itos(plyr.bankAccountBalances[5]);
				character[181] = Itos(plyr.bankAccountBalances[6]);
				character[182] = Itos(plyr.bankAccountBalances[7]);
				character[183] = Itos(plyr.bankAccountBalances[8]);

				character[184] = Itos(plyr.clothing[0]);
				character[185] = Itos(plyr.clothing[1]);
				character[186] = Itos(plyr.clothing[2]);
				character[187] = Itos(plyr.clothing[3]);

				character[188] = Itos(plyr.goblinsVisited);
				character[189] = Itos(plyr.goblinsChallenged);
				character[190] = Itos(plyr.goblinsDefeated);
				character[191] = Itos(plyr.goblinsCombat);
				character[192] = Itos(plyr.goblinsReforged);
				character[193] = Itos(plyr.trollsVisited);
				character[194] = Itos(plyr.trollsChallenged);
				character[195] = Itos(plyr.trollsDefeated);
				character[196] = Itos(plyr.trollsCombat);
				character[197] = Itos(plyr.trollsReforged);

				character[198] = Itos(plyr.oracleReturnTomorrow);
				character[199] = Itos(plyr.oracleDay);
				character[200] = Itos(plyr.oracleMonth);
				character[201] = Itos(plyr.oracleYear);
				character[202] = Itos(plyr.oracleQuestNo);
				character[203] = Itos(plyr.healerDays[0]);
				character[204] = Itos(plyr.healerDays[1]);
				character[205] = Itos(plyr.healerHours[0]);
				character[206] = Itos(plyr.healerHours[1]);
				character[207] = Itos(plyr.healerMinutes[0]);
				character[208] = Itos(plyr.healerMinutes[1]);
				character[209] = Itos(plyr.treasureFinding);
				character[210] = Itos(plyr.invisibility);
				character[211] = Itos(plyr.diseases[0]);
				character[212] = Itos(plyr.diseases[1]);
				character[213] = Itos(plyr.diseases[2]);
				character[214] = Itos(plyr.diseases[3]);
				character[215] = Itos(plyr.poison[0]);
				character[216] = Itos(plyr.poison[1]);
				character[217] = Itos(plyr.poison[2]);
				character[218] = Itos(plyr.poison[3]);
				character[219] = Itos(plyr.delusion);

				for (int y = 0; y < 9; ++y)
					character[220 + y] = Itos(plyr.invulnerability[y]);
				character[229] = Itos(plyr.noticeability);
				character[230] = Itos(plyr.protection1);
				character[231] = Itos(plyr.protection2);

				character[232] = Itos(plyr.forgeDays);
				character[233] = Itos(plyr.forgeType);
				character[234] = Itos(plyr.forgeBonus);
				character[235] = plyr.forgeName;
				character[236] = Itos(plyr.stolenFromVault);

				character[399] = "Line 400: Item Buffer follows";

		 // Copy item buffer

			int saveGameIndex = 400; // start location for object buffer items
			for (int z = 0; z < itemBufferSize; ++z)
			{
				character[saveGameIndex] = Itos(itemBuffer[z].hp);
				character[saveGameIndex + 1] = Itos(itemBuffer[z].index);
				character[saveGameIndex + 2] = Itos(itemBuffer[z].level);
				character[saveGameIndex + 3] = Itos(itemBuffer[z].location);
				character[saveGameIndex + 4] = Itos(itemBuffer[z].type);
				character[saveGameIndex + 5] = Itos(itemBuffer[z].x);
				character[saveGameIndex + 6] = Itos(itemBuffer[z].y);

				character[saveGameIndex + 7] = itemBuffer[z].name;
				character[saveGameIndex + 8] = Itos(itemBuffer[z].maxHP);
				character[saveGameIndex + 9] = Itos(itemBuffer[z].flags);
				character[saveGameIndex + 10] = Itos(itemBuffer[z].minStrength);
				character[saveGameIndex + 11] = Itos(itemBuffer[z].minDexterity);
				character[saveGameIndex + 12] = Itos(itemBuffer[z].useStrength);
				character[saveGameIndex + 13] = Itos(itemBuffer[z].blunt);
				character[saveGameIndex + 14] = Itos(itemBuffer[z].sharp);
				character[saveGameIndex + 15] = Itos(itemBuffer[z].earth);
				character[saveGameIndex + 16] = Itos(itemBuffer[z].air);
				character[saveGameIndex + 17] = Itos(itemBuffer[z].fire);
				character[saveGameIndex + 18] = Itos(itemBuffer[z].water);
				character[saveGameIndex + 19] = Itos(itemBuffer[z].power);
				character[saveGameIndex + 20] = Itos(itemBuffer[z].magic); // mental
				character[saveGameIndex + 21] = Itos(itemBuffer[z].good); // cleric
				character[saveGameIndex + 22] = Itos(itemBuffer[z].evil);
				character[saveGameIndex + 23] = Itos(itemBuffer[z].cold);
				character[saveGameIndex + 24] = Itos(itemBuffer[z].weight);
				character[saveGameIndex + 25] = Itos(itemBuffer[z].alignment);
				character[saveGameIndex + 26] = Itos(itemBuffer[z].melee);
				character[saveGameIndex + 27] = Itos(itemBuffer[z].ammo);
				character[saveGameIndex + 28] = Itos(itemBuffer[z].parry);

				saveGameIndex = saveGameIndex + 28;
			}
		//cout << saveGameIndex << "\n";


			// Copy spell buffer
			saveGameIndex = 7400; // start location for spell buffer items (70 bytes)
			for (int z = 0; z < 35; ++z)
			{
				character[saveGameIndex] = Itos(spellBuffer[z].no);
				character[saveGameIndex + 1] = Itos(spellBuffer[z].percentage);
				saveGameIndex = saveGameIndex + 2;
			}
		//cout << saveGameIndex << "\n";
			// Copy effect buffer
			saveGameIndex = 7470; // start location for effect buffer items (200 bytes)
			for (int z = 0; z < 50; ++z)
			{
				character[saveGameIndex] = Itos(effectBuffer[z].effect);
				character[saveGameIndex + 1] = Itos(effectBuffer[z].negativeValue);
				character[saveGameIndex + 2] = Itos(effectBuffer[z].positiveValue);
				character[saveGameIndex + 3] = Itos(effectBuffer[z].duration);
				saveGameIndex = saveGameIndex + 4;
			}
		//cout << saveGameIndex << "\n";
			// Smithy daily wares - int smithyDailyWares[4][10];
			saveGameIndex = 7670; // start location for object buffer items
			for (int z = 0; z < 4; ++z)
			{
				for (int x = 0; x < 10; ++x)
				{
					character[saveGameIndex] = Itos(smithyDailyWares[z][x]);
					saveGameIndex++;
				}
			}
		//cout << saveGameIndex << "\n";
			//int tavernDailyFoods[14][6];
			saveGameIndex = 7710; // start location for object buffer items
			for (int z = 0; z < 14; ++z)
			{
				for (int x = 0; x < 6; ++x)
				{
					character[saveGameIndex] = Itos(tavernDailyFoods[z][x]);
					saveGameIndex++;
				}
			}
		//cout << saveGameIndex << "\n";
			//int tavernDailyDrinks[14][6];
			saveGameIndex = 7794; // start location for object buffer items
			for (int z = 0; z < 14; ++z)
			{
				for (int x = 0; x < 6; ++x)
				{
					character[saveGameIndex] = Itos(tavernDailyDrinks[z][x]);
					saveGameIndex++;
				}
			}
		//cout << saveGameIndex << "\n";
			// int shopDailyWares[15][12];
			saveGameIndex = 7878; // start location for object buffer items
			for (int z = 0; z < 15; ++z)
			{
				for (int x = 0; x < 12; ++x)
				{
					character[saveGameIndex] = Itos(shopDailyWares[z][x]);
					saveGameIndex++;
				}
			}

			// Currently inn and tavern job openings are not part of saved game
		//cout << saveGameIndex << "\n";
			//bool autoMapExplored[5][4096];
			saveGameIndex = 8058; // start location for automapexplored
			for (int z = 0; z < 5; ++z)
			{
				for (int x = 0; x < 4096; ++x)
				{
					character[saveGameIndex] = Itos(autoMapExplored[z][x]);
					saveGameIndex++;
				}
			}
		//cout << saveGameIndex << "\n";

		character[28538] = plyr.name;
		character[28539] = Ftos(plyr.z_offset);
		character[28540] = "Release 0.80";

			string filename = "data/saves/save" + Itos(saveSlot) + ".txt";
			outdata.open(filename); // opens the file
			if (outdata == null)
				cerr << "Error: character file could not be saved" << "\n";


			for (int y = 0; y < saveGameSize; ++y)
				outdata << character[y] << "\n";


		   // outdata << plyr.name << endl;
		   // outdata << plyr.z_offset << endl;

			outdata.close();
			return (1); // Added by me
		}
		public static bool LoadCharacter(int saveSlot)
		{

			std::ifstream instream = new std::ifstream();
			string junk;
			string junk2;
			string data;

			string filename = "data/saves/save" + Itos(saveSlot) + ".txt";
			instream.open(filename); // opens the file
			//instream.open("data/saves/save0.txt");
			if (instream == null)
			{
				cerr << "No save game in this slot" << "\n";
				return false;
			}
			string text;

			for (int a = 0; a < saveGameSize; ++a)
			{
				getline(instream, text);
				character[a] = text;
				//atoi(character[a].c_str()) = atoi(text.c_str());
			}


			//getline(instream, text); // read first line from the file for the character name
			//plyr.name = text;
			//getline(instream, text); // read second line from the file for the z_offset
			//plyr.z_offset = atof(text.c_str());

			instream.close();

			plyr.status = GameStates.Explore;
			// Copy atoi(character[24576].c_str()) int block values back into the atoi(character object
			//cout << "loaded gender = " << plyr.gender << "\n";

			plyr.gender = Convert.ToInt32(character[0]);
			plyr.hp = Convert.ToInt32(character[1]);
			plyr.maxhp = Convert.ToInt32(character[2]);

			plyr.scenario = Convert.ToInt32(character[3]);
			plyr.map = Convert.ToInt32(character[4]);
			plyr.mapWidth = Convert.ToInt32(character[5]);
			plyr.mapHeight = Convert.ToInt32(character[6]);
			plyr.x = Convert.ToInt32(character[7]);
			plyr.oldx = Convert.ToInt32(character[8]);
			plyr.y = Convert.ToInt32(character[9]);
			plyr.oldy = Convert.ToInt32(character[10]);
			plyr.facing = Convert.ToInt32(character[11]);

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

			plyr.alive = Convert.ToInt32(character[23]);

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
		  plyr.diagOn = Convert.ToInt32(character[54]);
		  plyr.mapOn = Convert.ToInt32(character[55]);
		  plyr.fpsOn = Convert.ToInt32(character[56]);
		  plyr.miniMapOn = Convert.ToInt32(character[57]);
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
		  plyr.encounter_done = Convert.ToInt32(character[70]);
		  plyr.game_on = Convert.ToInt32(character[71]);
		  plyr.gender = Convert.ToInt32(character[72]);
		  plyr.zone = Convert.ToInt32(character[73]);
		  plyr.zoneSet = Convert.ToInt32(character[74]);
		  plyr.current_zone = Convert.ToInt32(character[75]); // used by drawing function
		  plyr.status = Convert.ToInt32(character[76]);
		  plyr.specialwall = Convert.ToInt32(character[77]);
		//  plyr.windowSize = atoi(character[78].c_str());
		  //plyr.graphicStyle = atoi(character[79].c_str());
		  plyr.fixedEncounter = Convert.ToInt32(character[80]);
		  plyr.fixedEncounterRef = Convert.ToInt32(character[81]);
		  plyr.thirst = Convert.ToInt32(character[82]);
		  plyr.hunger = Convert.ToInt32(character[83]);
		  plyr.digestion = Convert.ToInt32(character[84]);
		  plyr.alcohol = Convert.ToInt32(character[85]);


		 for (int y = 0; y < 12; ++y)
			 plyr.guildAwards[y] = Convert.ToInt32(character[86 + y]);
		 for (int y = 0; y < 32; ++y)
			 plyr.fixedEncounters[y] = Convert.ToInt32(character[98 + y]);
		 for (int y = 0; y < 14; ++y)
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

		plyr.goblinsVisited = Convert.ToInt32(character[188]);
		plyr.goblinsChallenged = Convert.ToInt32(character[189]);
		plyr.goblinsDefeated = Convert.ToInt32(character[190]);
		plyr.goblinsCombat = Convert.ToInt32(character[191]);
		plyr.goblinsReforged = Convert.ToInt32(character[192]);
		plyr.trollsVisited = Convert.ToInt32(character[193]);
		plyr.trollsChallenged = Convert.ToInt32(character[194]);
		plyr.trollsDefeated = Convert.ToInt32(character[195]);
		plyr.trollsCombat = Convert.ToInt32(character[196]);
		plyr.trollsReforged = Convert.ToInt32(character[197]);

		plyr.oracleReturnTomorrow = Convert.ToInt32(character[198]);
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
		for (int y = 0; y < 9; ++y)
			plyr.invulnerability[y] = Convert.ToInt32(character[220 + y]);
		plyr.noticeability = Convert.ToInt32(character[229]);
		plyr.protection1 = Convert.ToInt32(character[230]);
		plyr.protection2 = Convert.ToInt32(character[231]);

		plyr.forgeDays = Convert.ToInt32(character[232]);
		plyr.forgeType = Convert.ToInt32(character[233]);
		plyr.forgeBonus = Convert.ToInt32(character[234]);
		plyr.forgeName = character[235];
		plyr.stolenFromVault = Convert.ToInt32(character[236]);

		int loadGameIndex = 400; // start location for object buffer items
		for (int z = 0; z < itemBufferSize; ++z)
		{
			itemBuffer[z].hp = Convert.ToInt32(character[loadGameIndex]);
			itemBuffer[z].index = Convert.ToInt32(character[loadGameIndex + 1]);
			itemBuffer[z].level = Convert.ToInt32(character[loadGameIndex + 2]);
			itemBuffer[z].location = Convert.ToInt32(character[loadGameIndex + 3]);
			itemBuffer[z].type = Convert.ToInt32(character[loadGameIndex + 4]);
			itemBuffer[z].x = Convert.ToInt32(character[loadGameIndex + 5]);
			itemBuffer[z].y = Convert.ToInt32(character[loadGameIndex + 6]);


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

			loadGameIndex = loadGameIndex + 28;
		}


		// Copy spell buffer
		loadGameIndex = 7400; // start location for spell buffer items (70 bytes)
		for (int z = 0; z < 35; ++z)
		{
			spellBuffer[z].no = Convert.ToInt32(character[loadGameIndex]);
			spellBuffer[z].percentage = Convert.ToInt32(character[loadGameIndex + 1]);
			loadGameIndex = loadGameIndex + 2;
		}

		// Copy effect buffer
		loadGameIndex = 7470; // start location for effect buffer items (200 bytes)
		for (int z = 0; z < 50; ++z)
		{
			effectBuffer[z].effect = Convert.ToInt32(character[loadGameIndex]);
			effectBuffer[z].negativeValue = Convert.ToInt32(character[loadGameIndex + 1]);
			effectBuffer[z].positiveValue = Convert.ToInt32(character[loadGameIndex + 2]);
			effectBuffer[z].duration = Convert.ToInt32(character[loadGameIndex + 3]);
			loadGameIndex = loadGameIndex + 4;
		}

		// Smithy daily wares - int smithyDailyWares[4][10];
		loadGameIndex = 7670;
		for (int z = 0; z < 4; ++z)
		{
			for (int x = 0; x < 10; ++x)
			{
				smithyDailyWares[z][x] = Convert.ToInt32(character[loadGameIndex]);
				loadGameIndex++;
			}
		}

		//int tavernDailyFoods[14][6];
		loadGameIndex = 7710;
		for (int z = 0; z < 14; ++z)
		{
			for (int x = 0; x < 6; ++x)
			{
				tavernDailyFoods[z][x] = Convert.ToInt32(character[loadGameIndex]);
				loadGameIndex++;
			}
		}

		//int tavernDailyDrinks[14][6];
		loadGameIndex = 7794;
		for (int z = 0; z < 14; ++z)
		{
			for (int x = 0; x < 6; ++x)
			{
				tavernDailyDrinks[z][x] = Convert.ToInt32(character[loadGameIndex]);
				loadGameIndex++;
			}
		}

		// int shopDailyWares[15][12];
		loadGameIndex = 7878;
		for (int z = 0; z < 15; ++z)
		{
			for (int x = 0; x < 12; ++x)
			{
				shopDailyWares[z][x] = Convert.ToInt32(character[loadGameIndex]);
				loadGameIndex++;
			}
		}

		// Currently inn and tavern job openings are not part of saved game

		// load automap flags
		loadGameIndex = 8058; // start location for object buffer items
		for (int z = 0; z < 5; ++z)
		{
			for (int x = 0; x < 4096; ++x)
			{
				autoMapExplored[z][x] = Convert.ToInt32(character[loadGameIndex]);
				loadGameIndex++;
			}
		}

		plyr.name = character[28538];

		//MLT: Double to float
		plyr.z_offset = (float)Convert.ToDouble(character[28539]);
		string saveGameReleaseNo = character[28540];

		  return true;
		}


		public static string Ftos(float i)
		{
			std::stringstream s = new std::stringstream();
			s << i;
			return s.str();
		}
		public static string Itos(int i)
		{
			std::stringstream s = new std::stringstream();
			s << i;
			return s.str();
		}
		//int str2int (const string &str)







		//using namespace std;

		// Each character save game file is made up of a 28540 element string array.
		// Each array element can hold a number or text string (e.g. no. of torches carried or an item name)

		// NOTES:
		// job openings not currently part of saved game

		public static string[] character = new string[saveGameSize];
		public static string[] saveGameDescriptions = new string[10];




	}
}