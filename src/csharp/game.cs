using System;
using System.Collections.Generic;
using System.Linq;
namespace P3Net.Arx
{
	public partial class GlobalMembers
	{
		public static void InitialiseNewGame()
		{
			// COPY FROM ARX.CPP - Prepare shop stock etc...

			doorCityBuffer.loadFromFile("data/audio/cityDoor.wav");
			secretCityBuffer.loadFromFile("data/audio/citySecretDoor.wav");
			citySecretSound.setBuffer(secretCityBuffer);
			cityDoorSound.setBuffer(doorCityBuffer);

			doorDungeonBuffer.loadFromFile("data/audio/dungeonDoor.wav");
			secretDungeonBuffer.loadFromFile("data/audio/dungeonSecretDoor.wav");
			dungeonSecretSound.setBuffer(secretDungeonBuffer);
			dungeonDoorSound.setBuffer(doorDungeonBuffer);

			smithyBuffer.loadFromFile("data/audio/smithyHammer3.wav");
			smithySound.setBuffer(smithyBuffer);
			smithySound.setLoop(true);

			teleBlack.loadFromFile("data/images/teleport_black.png");
			teleGold.loadFromFile("data/images/teleport_gold.png");

			InitMap();

			//loadMonstersBinary(); duplicate
		}

	// Main game loop (excluding main menu)

		public static void GameLoop()
		{
			Running = true;
			gameQuit = false;

			bool smithyPlaying = false;
			int timer = 0;

			while (Running)
			{
				while ((Running) && (plyr.alive))
				{
					dt = myclock.restart();

				if (plyr.scenario == 1)
					CheckTeleport();

				if (plyr.hp < 0)
					plyr.alive = false;

				CheckShop();

				plyr.status_text = " ";

				/* Update player loc details */

				int ind = GetMapIndex(plyr.x, plyr.y);
				autoMapExplored[plyr.map][ind] = true;
				TransMapIndex(ind);
				plyr.special = levelmap[ind].special;
				plyr.location = levelmap[ind].location;
				SetCurrentZone();

				if (plyr.scenario == 1)
					CheckFixedEncounters();
				if (plyr.scenario == 1)
					CheckFixedTreasures();
				CheckForItemsHere();


				DispMain();
				DrawInfoPanels(); // Displayed here so as not to overwrite text from USE or an ENCOUNTER
				UpdateDisplay();

					encounterCheckTime += dt;
					if (encounterCheckTime >= sf.seconds(4.8f)) // was 0.8f
					{
					  CheckEncounter();
					  encounterCheckTime = sf.Time.Zero;
					  AddMinute();
					}


				if ((plyr.special == 1000) && (!smithyPlaying))
				{
					smithySound.play();
					smithyPlaying = true;
				}
				if ((smithyPlaying) && (plyr.special != 1000))
				{
					smithySound.stop();
					smithyPlaying = false;
				}


				string key = ReadKey();
				if (key == "left")
					TurnLeft();
				if (key == "right")
					TurnRight();
				if (key == "up")
					MoveForward();
				if (key == "down")
					MoveBack();
				if (key == "J")
					TurnLeft();
				if (key == "L")
					TurnRight();
				if (key == "I")
					MoveForward();
				if (key == "K")
					MoveBack();
				if (key == "U")
					SelectItem(1);
				if (key == "D")
					SelectItem(2);
				if (key == "C")
					CastSpells();
				if (key == "B")
					DisplayObjectBuffer();
				if (key == "G")
					GetItems();
				if (key == "P")
					PauseGame();
				if (key == ",")
					TogglePanelsBackward();
				if (key == ".")
					TogglePanelsForward();
				if (key == "F12")
				{
					if (plyr.diagOn)
						plyr.diagOn = false;
					else
						plyr.diagOn = true;
				}
				if (key == "F11")
					TidyObjectBuffer();
				if (key == "F1")
					plyr.infoPanel = 1;
				if (key == "F2")
					plyr.infoPanel = 2;
				if (key == "F3")
					plyr.infoPanel = 3;
				if (key == "F4")
					plyr.infoPanel = 4;
				if (key == "F5")
					plyr.infoPanel = 5;
				if (key == "F6")
					plyr.infoPanel = 6;
				if (key == "F7")
					plyr.infoPanel = 7;
				if (key == "F8")
					plyr.infoPanel = 8;

				if (key == "F")
				{
					if (plyr.fpsOn)
						plyr.fpsOn = false;
					else
						plyr.fpsOn = true;
				}
				if (key == "A")
				{
					if (plyr.miniMapOn)
						plyr.miniMapOn = false;
					else
						plyr.miniMapOn = true;
				}
				if (key == "M")
					Automap();

				if (key == "W")
					ChooseEncounter();

				if (key == "T")
				{
					if (AR_DEV.TELEPORT_OPTION)
						Teleport();
				}

				if (key == "ESC")
					OptionsMenu();
				if (key == "QUIT")
					OptionsMenu();
				}
				// Check smithy sounds and encounter music not playing
				if (!gameQuit)
				{
					smithySound.stop();
					PlayerDies();
				}
				Running = false;

			}
		}

		public static void QuitMenu()
		{
				string str;
				string key_value;
				bool keypressed = false;

				while (!keypressed)
				{
					ClearDisplay();
					DisplayQuitMenu();
					UpdateDisplay();

					key_value = GetSingleKey();

					if (key_value == "N")
						keypressed = true;
					if (key_value == "Y")
					{
						keypressed = true;
						Running = false;
						gameQuit = true;
					}
					//if (key_value == "Y") { keypressed=true; smithySound.stop(); Running=false; gameQuit=true; }
				}

		}
		public static void OptionsMenu()
		{
				string str;
				string key_value;
				bool keypressed = false;

				while ((!keypressed) && (Running))
				{
					ClearDisplay();
					DisplayOptionsMenu();
					UpdateDisplay();

					key_value = GetSingleKey();

					if (key_value == "ESC")
					{
						keypressed = true;
						plyr.status = 1;
					}
					if (key_value == "S")
					{
						keypressed = true;
						DisplaySaveGame();
					} // Needs updating to reflect slot
					if (key_value == "Q")
						QuitMenu();
				}
		}
		public static void Teleport()
		{
			string typed_coords;
			bool teleportComplete = false;
			int name_length;
			while (!teleportComplete)
			{
				string str;
				string single_key;
				ClearDisplay();
				DrawText(2, 2, "To XX YY (followed by Enter)");
				str = "To: " + typed_coords + "_";
				DrawText(2, 5, str);
				UpdateDisplay();


				 single_key = GetSingleKey();
				 if (single_key == "SPACE")
					 single_key = " ";
				 if ((single_key == "RETURN") && (typed_coords != ""))
				 {
					 single_key = "";
					 teleportComplete = true;
				 }
				 if (single_key == "BACKSPACE")
				 {
						  name_length = typed_coords.Length;
						  if (name_length != 0)
						  {
							  int name_length = typed_coords.Length;
							  typed_coords = typed_coords.Substring(0,(name_length - 1));
						  }
						  single_key = "";
				 } else
				 {
						  if (single_key != "RETURN")
						  {
							  name_length = typed_coords.Length;
							  if (name_length != 5) // check for limit of name length
									 typed_coords = typed_coords + single_key;
						  }
			 }

			}
			string xtxt;
			string ytxt;
			string map;
			xtxt = typed_coords.Substring(0,2);
			ytxt = typed_coords.Substring(3,2);
			//map=typed_coords.substr(6,1);
			// if moving to new map then data files need to be loaded first
			int x = 0;
			int y = 0;
			x = Convert.ToInt32(xtxt);
			y = Convert.ToInt32(ytxt);

			if (!((x >= 0) && (x <= 63)))
				x = 0;
			if (!((y >= 0) && (y <= 63)))
				y = 0;
			plyr.x = x;
			plyr.y = y;
			//plyr.map=atoi(map.c_str());
			// load new content?
			// teleport sound
			// validate!
		}

		public static void MoveWest()
		{
			if (plyr.facing == Directions.West) // west (forward)
			{

				if (plyr.z_offset > 1.9f)
				{
					plyr.z_offset = 0.0f;
					plyr.oldx = plyr.x;
					plyr.x--;
				} else
				{
					plyr.z_offset = plyr.z_offset + 0.1f;
			}

			}

			if (plyr.facing == Directions.East) // east (back)
			{
				if (plyr.z_offset < 0.1f)
				{
					plyr.z_offset = 1.9f;
					plyr.oldx = plyr.x;
					plyr.x--;
				} else
				{
					plyr.z_offset = plyr.z_offset - 0.1f;
			}
			}

		}
		public static void MoveEast()
		{
			if (plyr.facing == Directions.East) // east (forward)
			{
				if (plyr.z_offset > 1.9f)
				{
					plyr.z_offset = 0.0f;
					plyr.oldx = plyr.x;
					plyr.x++;
				} else
				{
					plyr.z_offset = plyr.z_offset + 0.1f;
			}

			}

			if (plyr.facing == Directions.West) // west (back)
			{
				if (plyr.z_offset < 0.1f)
				{
					plyr.z_offset = 1.9f;
					plyr.oldx = plyr.x;
					plyr.x++;
				} else
				{
					plyr.z_offset = plyr.z_offset - 0.1f;
			}
			}

		}
		public static void MoveNorth()
		{
			if (plyr.facing == Directions.North) // north (forward)
			{
				if (plyr.z_offset > 1.9f)
				{
					plyr.z_offset = 0.0f;
					plyr.oldy = plyr.y;
					plyr.y--;
				} else
				{
					plyr.z_offset = plyr.z_offset + 0.1f;
			}
			}

			if (plyr.facing == Directions.South) // south (back)
			{
				if (plyr.z_offset < 0.1f)
				{
					plyr.z_offset = 1.9f;
					plyr.oldy = plyr.y;
					plyr.y--;
				} else
				{
					plyr.z_offset = plyr.z_offset - 0.1f;
			}
			}

		}
		public static void MoveSouth()
		{
			if (plyr.facing == Directions.South)
			{
				if (plyr.z_offset > 1.9f)
				{
					plyr.z_offset = 0.0f;
					plyr.oldy = plyr.y;
					plyr.y++;
				} else
				{
					plyr.z_offset = plyr.z_offset + 0.1f;
			}
			}

			if (plyr.facing == Directions.North)
			{
				if (plyr.z_offset < 0.1f)
				{
					plyr.z_offset = 1.9f;
					plyr.oldy = plyr.y;
					plyr.y++;
				} else
				{
					plyr.z_offset = plyr.z_offset - 0.1f;
			}
			}

		}
		public static void MoveForward()
		{
			plyr.movingForward = true;
			string encText = CheckEncumbrance();
			if (encText == "Encumbered")
				sf.sleep(sf.milliseconds(50));
			if (encText == "Immobilized!")
				sf.sleep(sf.milliseconds(200));

			if (((plyr.front != 13) && (plyr.front != 14) && (plyr.front != 37)) || (plyr.z_offset < 1.7))
			{
				switch (plyr.facing)
				{
				case 1: // w
					MoveWest();
					break;

				case 2: // n
					MoveNorth();
					break;

				case 3: // e
					MoveEast();
					break;

				case 4: // s
					MoveSouth();
					break;
				}


				// Barred door
				if (((plyr.front == 8) || (plyr.front == 9) || (plyr.front == 10)) && (plyr.z_offset > 1.7)) // barred door
				{
					//MLT: Double to float
					plyr.z_offset -= 0.1F;
					bool doorAlreadyOpened = false;
					doorAlreadyOpened = CheckBarredDoor();
					if (doorAlreadyOpened)
					{
						DoorMessage("@@@The door opens!@@@@<<< Press any key to continue >>>");
						MoveThroughBarredDoor();
					} else
					{
						BarredDoor();
				}
				}

				if (((plyr.front == 5) || (plyr.front == 6)) && (plyr.z_offset > 1.8)) // secret door
				{
					plyr.z_offset = 0.3f;


					switch (plyr.facing)
					{
						case 1: // w
							plyr.oldx = plyr.x;
							plyr.x--;
							break;

						case 2: // n
							plyr.oldy = plyr.y;
							plyr.y--;
							break;

						case 3: // e
							plyr.oldx = plyr.x;
							plyr.x++;
							break;
						case 4:
							plyr.oldy = plyr.y;
							plyr.y++; // s
							break;
					}

					if (plyr.scenario == 0)
						citySecretSound.play();
					else
						dungeonSecretSound.play();
				}

				if (((plyr.front == 3) || (plyr.front == 4)) && (plyr.z_offset > 1.8)) // door
				{
					plyr.z_offset = 0.3f;

					switch (plyr.facing)
					{
						case 1: // w
							plyr.oldx = plyr.x;
							plyr.x--;
							break;

						case 2: // n
							plyr.oldy = plyr.y;
							plyr.y--;
							break;

						case 3: // e
							plyr.oldx = plyr.x;
							plyr.x++;
							break;

						case 4:
							plyr.oldy = plyr.y;
							plyr.y++; // s
							break;
					}
					if (plyr.scenario == 0)
						cityDoorSound.play();
					else
						dungeonDoorSound.play();
				}

			 if ((plyr.front > 25) && (plyr.front < 50) && (plyr.z_offset > 1.8)) // City doors with signs
			 {
					plyr.z_offset = 0.3f;

					switch (plyr.facing)
					{
						case 1: // w
							plyr.oldx = plyr.x;
							plyr.x--;
							break;

						case 2: // n
							plyr.oldy = plyr.y;
							plyr.y--;
							break;

						case 3: // e
							plyr.oldx = plyr.x;
							plyr.x++;
							break;

						case 4:
							plyr.oldy = plyr.y;
							plyr.y++; // s
							break;

					}
					if (plyr.scenario == 0)
						cityDoorSound.play();
					else
						dungeonDoorSound.play();
			 }

			}
			/*
			if (plyr.x == -1) { plyr.x = 63; } // city wrap around
			if (plyr.x == 64) { plyr.x = 0; } // city wrap around
			if (plyr.y == -1) { plyr.y = 63; } // city wrap around
			if (plyr.y == 64) { plyr.y = 0; } // city wrap around
			*/
			if ((plyr.x == -1) || (plyr.x == 64) || (plyr.y == -1) || (plyr.y == 64))
				ScenarioEntrance(300);


		}
		public static void MoveBack()
		{
			plyr.movingForward = false;
			string encText = CheckEncumbrance();
			if (encText == "Encumbered")
				sf.sleep(sf.milliseconds(50));
			if (encText == "Immobilized!")
				sf.sleep(sf.milliseconds(200));

			if (((plyr.back != 13) && (plyr.back != 14) && (plyr.back != 37)) || (plyr.z_offset > 0.2))
			{

			switch (plyr.facing)
			{
				case 1: // facing west
					MoveEast(); // opposite of west
					break;

				case 2: // facing north
					MoveSouth(); // opposite of north
					break;

				case 3: // facing east
					MoveWest();
					break;

				case 4: // facing south
					MoveNorth();
					break;
			}


					// Barred door
				if (((plyr.back == 8) || (plyr.back == 9) || (plyr.back == 10)) && (plyr.z_offset < 0.3)) // barred door
				{
					//MLT: Double to float
					plyr.z_offset += 0.1F;
					bool doorAlreadyOpened = false;
					doorAlreadyOpened = CheckBarredDoor();
					if (doorAlreadyOpened)
					{
						DoorMessage("@@@The door opens!@@@@<<< Press any key to continue >>>");
						MoveThroughBarredDoor();
					} else
					{
						BarredDoor();
				}
				}


		 if (((plyr.back == 5) || (plyr.back == 6)) && (plyr.z_offset < 0.3)) // secret door
		 {
					plyr.z_offset = 1.7f;


					switch (plyr.facing)
					{
						case 1: // w
							plyr.oldx = plyr.x;
							plyr.x++;
							break;

						case 2: // n
							plyr.oldy = plyr.y;
							plyr.y++;
							break;

						case 3: // e
							plyr.oldx = plyr.x;
							plyr.x--;
							break;
						case 4:
							plyr.oldy = plyr.y;
							plyr.y--; // s
							break;
					}

					if (plyr.scenario == 0)
						citySecretSound.play();
					else
						dungeonSecretSound.play();


		 }

				if (((plyr.back == 3) || (plyr.back == 4)) && (plyr.z_offset < 0.3)) // door
				{
					plyr.z_offset = 1.7f;

					switch (plyr.facing)
					{
						case 1: // w
							plyr.oldx = plyr.x;
							plyr.x++;
							break;

						case 2: // n
							plyr.oldy = plyr.y;
							plyr.y++;
							break;

						case 3: // e
							plyr.oldx = plyr.x;
							plyr.x--;
							break;

						case 4:
							plyr.oldy = plyr.y;
							plyr.y--; // s
							break;
					}
					if (plyr.scenario == 0)
						cityDoorSound.play();
					else
						dungeonDoorSound.play();
				}

			 if ((plyr.back > 25) && (plyr.back < 50) && (plyr.z_offset < 0.3)) // City doors with signs
			 {
					plyr.z_offset = 1.7f;

					switch (plyr.facing)
					{
						case 1: // w
							plyr.oldx = plyr.x;
							plyr.x++;
							break;

						case 2: // n
							plyr.oldy = plyr.y;
							plyr.y++;
							break;

						case 3: // e
							plyr.oldx = plyr.x;
							plyr.x--;
							break;

						case 4:
							plyr.oldy = plyr.y;
							plyr.y--; // s
							break;

					}
					if (plyr.scenario == 0)
						cityDoorSound.play();
					else
						dungeonDoorSound.play();
			 }

			}

		}
		public static void TurnLeft()
		{
			switch (plyr.facing)
			{
			case 2: // n

				plyr.facing = Directions.West;
				plyr.z_offset = 1.0f;
				break;

			case 1: //  facing w before turning

				plyr.facing = Directions.South;
				plyr.z_offset = 1.0f;
				break;

			case 3: // e

				plyr.facing = Directions.North;
				plyr.z_offset = 1.0f;
				break;

			case 4: // s

				plyr.facing = Directions.East;
				plyr.z_offset = 1.0f;
				break;
			}
		}
		public static void TurnRight()
		{
			switch (plyr.facing)
			{
			case 2: // n

				plyr.facing = Directions.East;
				plyr.z_offset = 1.0f;
				break;

			case 1: // w

				plyr.facing = Directions.North;
				plyr.z_offset = 1.0f;
				break;

			case 3: // e

				plyr.facing = Directions.South;
				plyr.z_offset = 1.0f;
				break;

			case 4: // s
				plyr.facing = Directions.West;
				plyr.z_offset = 1.0f;
				break;
			}
		}
		public static void CheckTeleport()
		{

			if ((plyr.special >= 0xE0) && (plyr.special <= 0xFF)) // 224 - 255
			{
			  for (int i = 0 ; i <= 31 ; i++) // Max number of teleport objects
			  {
				 //int ind = ((plyr.special)-0xEF);
				if (plyr.special == teleports[i].@ref)
				{
					if (teleports[i].new_map == 0)
					{
						plyr.x = (teleports[i].new_x);
						plyr.y = (teleports[i].new_y);
					}
					if (teleports[i].new_map == 1)
					{
						plyr.x = (teleports[i].new_x) + 32;
						plyr.y = (teleports[i].new_y);
					}
					if (teleports[i].new_map == 2)
					{
						plyr.x = (teleports[i].new_x);
						plyr.y = (teleports[i].new_y) + 32;
					}
					if (teleports[i].new_map == 3)
					{
						plyr.x = (teleports[i].new_x) + 32;
						plyr.y = (teleports[i].new_y) + 32;
					}
					int new_map;
					// Change map level to new_map value accounting for level 1 now being single 64x64 map
					if (teleports[i].new_map == 0)
						new_map = 1;
					if (teleports[i].new_map == 1)
						new_map = 1;
					if (teleports[i].new_map == 2)
						new_map = 1;
					if (teleports[i].new_map == 3)
						new_map = 1;
					if (teleports[i].new_map == 4)
						new_map = 2;
					if (teleports[i].new_map == 5)
						new_map = 3;

					if (plyr.map != new_map)
					{
						plyr.map = new_map;
						MoveMapLevelTeleport();
					}

					// Display flashing sequence for teleport.
					plyr.teleporting = 20;

					//plyr.x++;
					//plyr.y++;

					int location_index = GetMapIndex(plyr.x, plyr.y);
					TransMapIndex(location_index);
				}
			  }
			}

		}
		//void checkFixedEncounters();
		public static void CheckFixedTreasures()
		{
			int treasureRef;
			switch (plyr.special)
			{
				case 0xAE: // Sword of the Adept
					treasureRef = plyr.special - 128;
					if (plyr.fixedTreasures[treasureRef] == false)
					{
						TreasureMessage("@A sword protrudes from a slab@of black marble.@@@");
						CreateWeapon(82);
						plyr.fixedTreasures[treasureRef] = true;
						GetItems();
					}
					break;

				case 0xAF: // Razor Ice
					treasureRef = plyr.special - 128;
					if (plyr.fixedTreasures[treasureRef] == false)
					{
						TreasureMessage("A katana in a ribbed black lacquered@scabbard rests against a wall.@@A pattern of snowflakes has been@honed along its single edge.");
						CreateWeapon(81);
						plyr.fixedTreasures[treasureRef] = true;
						GetItems();
					}
					break;

				case 0xB7: // Map Stone
					treasureRef = plyr.special - 128;
					if (plyr.fixedTreasures[treasureRef] == false)
					{
						string gender = "man";
						if (plyr.gender == 2)
							gender = "woman";
						string key = "";
						while (key != "SPACE")
						{
								DispMain();
								CyText(1, "A note reads:");
								string str = plyr.name + ",";
								BText(1, 2, str);
								str = "Congratulations my good " + gender + " on";
								BText(4, 4, str);
								BText(1, 5, "getting this far.");
								BText(33, 7, "A friend");
								CyText(9, "<<< Press SPACE to continue >>>");
								UpdateDisplay();
								key = GetSingleKey();
						}
						CreateQuestItem(4);
						plyr.fixedTreasures[treasureRef] = true;
						GetItems();
					}
					break;

				case 0xB6: // Amethyst Rod
					treasureRef = plyr.special - 128;
					if (plyr.fixedTreasures[treasureRef] == false)
					{
						TreasureMessage("@A blue-violet quartz rod lies@on a purple dias.@@@");
						CreateQuestItem(5);
						plyr.fixedTreasures[treasureRef] = true;
						GetItems();
					}
					break;
			}

		}
		public static void TreasureMessage(string str)
		{
			string txt;
			string key = "";
			while (key != "SPACE")
			{
					DispMain();
					txt = str + "@@<<< Press SPACE to continue >>>";
					CyText(1, txt);
					UpdateDisplay();
					key = GetSingleKey();
			}
		}

		public static void PlayerDies()
		{
			bool deathLooping = true;
			bool musicPlaying = false;
			plyr.fixedEncounter = false;
			plyr.miniMapOn = false;
			plyr.status = 5; // Dead

			if (plyr.musicStyle == 0)
				deathMusic.openFromFile("data/audio/death.ogg");
			else
				deathMusic.openFromFile("data/audio/B/death.ogg");
			if (!musicPlaying)
			{
				deathMusic.play();
				musicPlaying = true;
			}
		//lyricstexture.clear(sf::Color::Green);
			LoadLyrics("death.txt");
			while (deathLooping)
			{
				clock1.restart();

				ClearDisplay();
				DrawStatsPanel();

				UpdateLyrics();
				iCounter++;

				CyText(3, "You're DEAD.");
				CyText(8, "(Press SPACE to continue)");
				UpdateDisplay();
				string key = GetSingleKey();
				if (key == "SPACE")
					deathLooping = false;
				if (key == "F1")
				{
					deathMusic.stop();
					LoadLyrics("death.txt");
					deathMusic.play();
				}
			}
			if (musicPlaying)
				deathMusic.stop();
		}
		public static void CheckShop()
		{
			 switch (plyr.special)
			 {
				case 0x0D: // damon
					ShopDamon();
					break;
				case 0x0C: // guild
					ShopGuild();
					break;
				case 0x0F: // retreat
					ShopRetreat();
					break;
				case 11: // Palace Prison
					// Check for completed code already
					break;
				case 19: // Ferryman
					ShopFerry();
					break;
				case 28: // lift
					ShopLift();
					break;
				case 87: // king
					ShopUndeadKing();
					break;
				case 0x1D: // rathskeller
					RunModule(0x1D);
					break;
				case 21: // dwarvenSmithy
					RunModule(21);
					break;
				case 0x50: // City Bank
					if (plyr.scenario == 0)
						ShopBank();
					break;
				case 0x90: // City Smithy
					if (plyr.scenario == 0)
						ShopSmithy();
					break;
				case 0x70: // City Shop
					if (plyr.scenario == 0)
						ShopShop();
					break;
				case 0x10: // City Inn
					if (plyr.scenario == 0)
						ShopInn();
					if (plyr.scenario == 1)
						ShopOracle();
					break;
				case 0x30: // City Tavern
					if (plyr.scenario == 0)
						ShopTavern();
					break;
				case 0xF0: // City Guild
					if (plyr.scenario == 0)
						ShopGuild();
					break;
				case 208: // City Healer
					if (plyr.scenario == 0)
						ShopHealer();
					break;
				//case 176: // Closed by order of the palace
				//	if (plyr.scenario==0) { shopClosed(); }
				//	break;
				case 3:
					ShopFountain();
					break;
				case 35:
					ShopFountain();
					break;
				case 5:
					Staircase();
					break;
				case 6:
					RunModule(0x06); // Vault
					break;
				case 7:
					if ((plyr.x == 2) && (plyr.y == 14) && (plyr.map == 1))
						ShopGoblins();
					if ((plyr.x == 56) && (plyr.y == 57) && (plyr.map == 1))
						ShopTrolls();
					break;
				case 10:
					ShopChapel();
					break;
				case 4: // entrance to wilderness
					ScenarioEntrance(300);
					break;
				case 300: // entrance to wilderness
					ScenarioEntrance(300);
					break;
				case 301: // entrance to palace
					ScenarioEntrance(301);
					break;
				case 302: // Entrance to arena
					ArenaSouthernEntrance();
					break;
				case 303: // Entrance to arena
					ArenaNorthernEntrance();
					break;
				case 304: // Entrance to arena
					ArenaWesternEntrance();

					break;
			 }
		}
		public static void LeaveShop()
		{
			//lyricstexture.clear(sf::Color::Black); // wipe the lyric strip
			if (plyr.facing == Directions.West)
				plyr.x = plyr.oldx;
			if (plyr.facing == Directions.East)
				plyr.x = plyr.oldx;
			if (plyr.facing == Directions.North)
				plyr.y = plyr.oldy;
			if (plyr.facing == Directions.South)
				plyr.y = plyr.oldy;

			//MLT: Double to float
			plyr.z_offset = 1.6F; // position player just outside door
			plyr.status = GameStates.Explore; // explore
		}
		public static void ShopClosed()
		{
			bool keynotpressed = true;
			while (keynotpressed) // closed
			{
				ClearDisplay();
				//glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
				string str;
				string key;
				plyr.status = 2; // shopping
				//App.pushGLStates();
				DrawStatsPanel();
				CyText(0, "Closed by@@Order of the Palace@@@@@@( Press space to continue )");
				UpdateDisplay();
				key = GetSingleKey();
				if (key != "")
					keynotpressed = false;
				if (key == "up")
					keynotpressed = true;
			}
			LeaveShop();
		}
		public static void ScenarioEntrance(int scenarioNumber)
		{
			bool keynotpressed = true;
			while (keynotpressed) // closed
			{
				ClearDisplay();
				//glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
				string str;
				string key;
				plyr.status = 2; // shopping
				//App.pushGLStates();
				switch (scenarioNumber)
				{
					case 300:
						str = "the Wilderness";
						break;
					case 301:
						str = "the Palace";
						break;
				}

				DrawStatsPanel();

				CyText(0, "You are at an entrance to");
				CyText(3, "Are you sure you want to enter this@scenario? (Y or N)");
				CyText(1, str);
				UpdateDisplay();
				key = GetSingleKey();

				if (key != "")
					keynotpressed = false;
				if (key == "up")
					keynotpressed = true;
			}
			LeaveShop();
		}
		//void shopDamon();
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//void ShopRetreat();
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//void ShopRathskeller();
		public static void TogglePanelsForward()
		{
			 plyr.infoPanel++;
			 if (plyr.infoPanel == 9)
				 plyr.infoPanel = 1;
		}
		public static void TogglePanelsBackward()
		{
			 plyr.infoPanel--;
			 if (plyr.infoPanel == 0)
				 plyr.infoPanel = 8;
		}

		public static void PauseGame()
		{
			string txt;
			string key = "";
			while (key != "SPACE")
			{
					DispMain();
					txt = "(Paused)@@@@@(Press SPACE to continue)";
					CText(txt);
					UpdateDisplay();
					key = GetSingleKey();
			}
		}
		public static void BarredDoor()
		{
			//bool keynotpressed = true;
			string str;
			string key;
			str = "";
			int doorType;
			if (plyr.movingForward)
				doorType = plyr.front;
			else
				doorType = plyr.back;
			int doorMenu = 0;
			bool doorNotExamined = true;
			while (doorMenu < 255) // closed
			{
				while (doorMenu == 0)
				{
					int useRef;
					doorNotExamined = true;
					DispMain();
					DrawConsoleBackground();
					CyText(1, "The door won't open. You may:");
					BText(9, 3, "(1) Examine the door");
					BText(9, 4, "(2) Force the door");
					BText(9, 5, "(3) Use a key");
					BText(9, 6, "(4) Break an enchantment");
					BText(9, 8, "(0) Leave it");
					UpdateDisplay();
					key = GetSingleKey();
					if (key == "0")
						doorMenu = 255;
					if (key == "1")
						doorMenu = 1;
					if (key == "2")
						doorMenu = 2;
					if (key == "3")
						doorMenu = 3;
					if (key == "4")
						doorMenu = 4;
					if (key == "U")
					{
						useRef = SelectItem(1);
						if (useRef == 666)
						{
							DoorMessage("@@@The door opens!@@@@<<< Press any key to continue >>>");
							UpdateDoorDetails();
							doorMenu = 255;
							MoveThroughBarredDoor();
						}
					}
				}
				while (doorMenu == 1)
				{
					if (doorNotExamined)
					{
						doorNotExamined = false;
						int doorIdentificationSuccess = RollDice(4, 6); // roll 3D6
						DoorTimedMessage("@@@Examining...");
						str = "@You can't discern what bars the door.";
						if (doorIdentificationSuccess < plyr.wis)
						{
							if (doorType == 8)
								str = "@The door appears to need a key.";
							if (doorType == 9)
								str = "@The door appears to be bolted.";
							if (doorType == 10)
								str = "@The door appears to be enchanted.";
						}
					}
					DispMain();
					DrawConsoleBackground();
					CText(str);
					UpdateDisplay();
					key = GetSingleKey();
					if (key == "SPACE")
						doorMenu = 0;
				}

				while (doorMenu == 2)
				{
					DoorMessage("@@@Wham!");
					int playerDamage = RollDice(1, 4);
					plyr.hp -= playerDamage;
					int doorRoll = RollDice(7, 6);
					bool doorOpenSuccess = false;
					if ((doorRoll <= plyr.str) && (doorType == 9))
						doorOpenSuccess = true;
					if ((doorOpenSuccess) && (plyr.hp > -1))
					{
						DoorMessage("@@@The door opens!@@@@<<< Press any key to continue >>>");
						UpdateDoorDetails();
						doorMenu = 255;
						MoveThroughBarredDoor();
					}
					if ((!doorOpenSuccess) && (plyr.hp > -1))
					{
						DoorMessage("@@@The door remains shut.");
						doorMenu = 0;
					}
					if (plyr.hp < 0)
						doorMenu = 255;
				}


				while (doorMenu == 3)
				{
					if (plyr.keys == 0)
					{
						DoorMessage("@@@You have none.");
						doorMenu = 0;
					} else
					{
						if (doorType == 9)
						{
							DoorMessage("@@@The door remains shut.");
							plyr.keys--;
							doorMenu = 0;
						}
						if (doorType == 10)
						{
							DoorMessage("@@@The door remains shut.");
							plyr.keys--;
							doorMenu = 0;
						}
						if (doorType == 8)
						{
							DoorMessage("@@@The door opens!@@@@<<< Press any key to continue >>>");
							plyr.keys--;
							UpdateDoorDetails();
							doorMenu = 255;
							MoveThroughBarredDoor();
						}
				}

				}

				while (doorMenu == 4)
				{
					DoorTimedMessage("@@@Concentrating...");

					int doorRoll = RollDice(7, 6);
					// Add to fatigue?
					bool doorOpenSuccess = false;
					if ((doorRoll <= plyr.inte) && (doorType == 10))
						doorOpenSuccess = true;
					if (doorOpenSuccess)
					{
						DoorMessage("@@@The door opens!@@@@<<< Press any key to continue >>>");
						UpdateDoorDetails();
						doorMenu = 255;
						MoveThroughBarredDoor();
					}
					if (!doorOpenSuccess)
					{
						DoorMessage("@@@The door remains shut.");
						doorMenu = 0;
					}
				}




			}
		}
		public static void DoorMessage(string str)
		{

			string key;
			bool keyNotPressed = true;
			while (keyNotPressed)
			{
				DispMain();
				DrawConsoleBackground();
				CyText(1, str);
				UpdateDisplay();
				key = GetSingleKey();
				if (key == "SPACE")
					keyNotPressed = false;
			}
		}
		public static void DoorTimedMessage(string str)
		{
			string key;
			DispMain();
			DrawConsoleBackground();
			CyText(1, str);
			UpdateDisplay();
			sf.sleep(sf.milliseconds(4000));
		}
		public static void UpdateDoorDetails()
		{
			// Adds an entry about a door that has been successfully opened. The 1st entry is overwritten after 20 door openings.

			if (plyr.movingForward)
				plyr.z_offset = 2.0f;
			else
				plyr.z_offset = 0.0f;

			if (plyr.doorDetailIndex == 19)
				plyr.doorDetailIndex = 0;
			plyr.doorDetails[plyr.doorDetailIndex].x = plyr.x;
			plyr.doorDetails[plyr.doorDetailIndex].y = plyr.y;
			plyr.doorDetails[plyr.doorDetailIndex].level = plyr.map;
			//int direction;
			if (plyr.movingForward)
				plyr.doorDetails[plyr.doorDetailIndex].direction = plyr.facing;
			if (!plyr.movingForward)
			{
				if (plyr.facing == Directions.West)
					plyr.doorDetails[plyr.doorDetailIndex].direction = 3; // actually moving east
				if (plyr.facing == Directions.North)
					plyr.doorDetails[plyr.doorDetailIndex].direction = 4; // actually moving south
				if (plyr.facing == Directions.East)
					plyr.doorDetails[plyr.doorDetailIndex].direction = 1; // actually moving west
				if (plyr.facing == Directions.South)
					plyr.doorDetails[plyr.doorDetailIndex].direction = 2; // actually moving north
			}
			plyr.doorDetailIndex++;
		}
		public static void MoveThroughBarredDoor()
		{
			if (plyr.movingForward)
				plyr.z_offset = 2.0;
			else
				plyr.z_offset = 0.0;
			if ((plyr.facing == Directions.West) && (plyr.movingForward))
				MoveWest();
			if ((plyr.facing == Directions.North) && (plyr.movingForward))
				MoveNorth();
			if ((plyr.facing == Directions.East) && (plyr.movingForward))
				MoveEast();
			if ((plyr.facing == Directions.South) && (plyr.movingForward))
				MoveSouth();
			if ((plyr.facing == Directions.West) && (!plyr.movingForward))
				MoveEast();
			if ((plyr.facing == Directions.North) && (!plyr.movingForward))
				MoveSouth();
			if ((plyr.facing == Directions.East) && (!plyr.movingForward))
				MoveWest();
			if ((plyr.facing == Directions.South) && (!plyr.movingForward))
				MoveNorth();
		}
		public static bool CheckBarredDoor()
		{
			// Assume moving forward for now
			bool doorAlreadyOpened = false;
			int doorFacing;
			if (plyr.movingForward)
				doorFacing = plyr.facing;
			else
			{
				if (plyr.facing == Directions.West)
					doorFacing = 3; // actually moving backwards east
				if (plyr.facing == Directions.North)
					doorFacing = 4; // actually moving backwards south
				if (plyr.facing == Directions.East)
					doorFacing = 1; // actually moving backwards west
				if (plyr.facing == Directions.South)
					doorFacing = 2; // actually moving backwards north
			}
			for (int i = 0 ; i < 20 ; i++)
			{
				if ((plyr.doorDetails[i].x == plyr.x) && (plyr.doorDetails[i].y == plyr.y) && (plyr.doorDetails[i].level == plyr.map) && (plyr.doorDetails[i].direction == doorFacing))
					doorAlreadyOpened = true;
			}
			return doorAlreadyOpened;
		}




		public static sf.Time dt = new sf.Time();
		public static sf.Time encounterCheckTime = new sf.Time();

		//extern sf::Clock clock1;
		//extern int iCounter;

		public static sf.Clock myclock = new sf.Clock();
		public static float Framerate;

		//extern bool autoMapExplored[5][4096];
		//extern string mess[25];
		//extern string room_descriptions[203];
		//extern Mapcell levelmap[4096]; // 12288

		//extern Teleport teleports[32];

		public static sf.SoundBuffer doorCityBuffer = new sf.SoundBuffer();
		public static sf.SoundBuffer secretCityBuffer = new sf.SoundBuffer();
		public static sf.SoundBuffer secretDungeonBuffer = new sf.SoundBuffer();
		public static sf.SoundBuffer doorDungeonBuffer = new sf.SoundBuffer();

		public static sf.Sound cityDoorSound = new sf.Sound();
		public static sf.Sound citySecretSound = new sf.Sound();
		public static sf.Sound dungeonDoorSound = new sf.Sound();
		public static sf.Sound dungeonSecretSound = new sf.Sound();

		public static sf.SoundBuffer smithyBuffer = new sf.SoundBuffer();
		public static sf.Sound smithySound = new sf.Sound();
		public static bool smithyPlaying;
		public static sf.Music deathMusic = new sf.Music();
		public static bool gameQuit;

		public static int teleColour = 1;
		public static sf.Texture teleBlack = new sf.Texture();
		public static sf.Texture teleGold = new sf.Texture();
		public static sf.Sprite tBackground = new sf.Sprite();

		public static bool Running;

	}
}