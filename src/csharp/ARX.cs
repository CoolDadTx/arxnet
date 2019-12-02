using System;
using System.Collections.Generic;
using System.Linq;
namespace P3Net.Arx
{
	public partial class GlobalMembers
	{

        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //void InitialiseGame();

        public static void StartGame()
		{
			DisplayLoading(); // display loading message on screen
			LoadResources(); // load up images for textures and backdrops once the graphic style has been selected
			InitEncounterThemes();
			InitMaps();
			LoadMapData(plyr.map);
			LoadDescriptions(plyr.map);
			LoadZoneData(plyr.map);
			LoadMessages(plyr.map);
			LoadEncounters();

			// Load monsters
			LoadMonstersBinary();
			InitialiseMonsterOffsets();
			ConvertMonstersBinary();

			// Load shop item binary files

			LoadDamonBinary(); // Load weapons, armour and clothing for the Damon & Pythias
			LoadCitySmithyBinary(); // Load weapons and armour for the City Smithies
			LoadDwarvenBinary(); // Load weapons and armour for the Dwarven Smithy

			CheckBackgroundTime(); // determine background graphics based on time of day

			CheckDailyInnJobOpenings();
			CheckDailyTavernJobOpenings();
			CheckDailybankJobOpenings();

			InitialiseNewGame(); // Only sound and graphic resources
			plyr.status = GameStates.Explore;

			//clearInventory();           // Set the inventory char array to zeros

			// Not in use yet - dungeonItems()
			LoadDungeonItems(); // Sets up the Dungeon items char array

			GameLoop(); // Enter the main game loop
		}

		public static bool programRunning = true;
		public static int loadgameMenu = 0;

		static int Main()
		{
			string key;

			if (!LoadConfig())
				return 0; // load screen resolution from arx.ini
			//FreeConsole(); // Hides the console window - make ARX.ini option
			SetDeveloperFlags();
			CreateGameWindow();
			//setScreenValues();
			plyr.fontStyle = 1;
			DispInit();
			InitFont();
			LoadLogoImage();

			InitSaveGameDescriptions();

			while (programRunning)
			{
				ClearDisplay();
				DisplayMainMenu();
				UpdateDisplay();

				key = GetSingleKey();

				if (key == "1")
				{
					CreateNewCharacter(Scenarios.City);
					StartGame();
				}

				if (key == "2")
				{
					CreateNewCharacter(Scenarios.Dungeon);
					StartGame();
				}


				if (key == "3")
				{
					loadgameMenu = 255; // high level menu
					while (loadgameMenu < 256)
					{
							ClearDisplay();
							DisplayLoadGame();
							DrawText(12, 0, "Load a character");
							UpdateDisplay();

							key = GetSingleKey();
							if (key == "0")
							{
								LoadCharacter(0);
								StartGame();
								loadgameMenu = 256;
							}
							if (key == "1")
							{
								LoadCharacter(1);
								StartGame();
								loadgameMenu = 256;
							}
							if (key == "2")
							{
								LoadCharacter(2);
								StartGame();
								loadgameMenu = 256;
							}
							if (key == "3")
							{
								LoadCharacter(3);
								StartGame();
								loadgameMenu = 256;
							}
							if (key == "4")
							{
								LoadCharacter(4);
								StartGame();
								loadgameMenu = 256;
							}
							if (key == "5")
							{
								LoadCharacter(5);
								StartGame();
								loadgameMenu = 256;
							}
							if (key == "6")
							{
								LoadCharacter(6);
								StartGame();
								loadgameMenu = 256;
							}
							if (key == "7")
							{
								LoadCharacter(7);
								StartGame();
								loadgameMenu = 256;
							}
							if (key == "8")
							{
								LoadCharacter(8);
								StartGame();
								loadgameMenu = 256;
							}
							if (key == "9")
							{
								LoadCharacter(9);
								StartGame();
								loadgameMenu = 256;
							}
							if (key == "ESC")
								loadgameMenu = 256;
					}
					key = "";
				}



				if (key == "4")
					DisplayAcknowledgements();

		/*
				if ( key=="5" )
				{
					if (graphicMode==0) { graphicMode=1; dispInit();}
					else
					if (graphicMode==1) { graphicMode=2; createGameWindow();dispInit();}
					else
					if (graphicMode==2) { graphicMode=0; createGameWindow();dispInit();}
		
				}
		*/
				if (key == "6")
				{
					if (plyr.musicStyle == 0)
						plyr.musicStyle = 1;
					else
						plyr.musicStyle = 0;
				}


				if (key == "7")
				{
					if (plyr.fontStyle == 0)
						plyr.fontStyle = 1;
					else
						plyr.fontStyle = 0;
					InitFont();
				}

				if (key == "0")
					programRunning = false;
				if (key == "QUIT")
					programRunning = false;
			}

			return EXIT_SUCCESS;
		}



	}
}