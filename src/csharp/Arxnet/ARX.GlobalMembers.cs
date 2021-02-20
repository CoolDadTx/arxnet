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
    //TODO: Move the game menu logic to its own helper class
    public static partial class GlobalMembers
    {                   
        public static bool ConfirmQuit ()
        {
            return true;
        }

        //TODO: Move to City module
        public static void CreateCityCharacter ()
        {
            CreateNewCharacter(Scenarios.City);
            StartGame();
        }

        //TODO: Move to Dungeon module
        public static void CreateDungeonCharacter ()
        {
            CreateNewCharacter(Scenarios.Dungeon);
            StartGame();
        }

        public static void LoadCharacter ()
        {
            var done = false;
            while (!done)
            {
                ClearDisplay();
                DisplayLoadGame();
                DrawText(12, 0, "Load a character");
                UpdateDisplay();

                //TODO: Modify to allow loading any # of characters
                var key = GetSingleKey();
                switch (key)
                {
                    case "0":
                    case "1":
                    case "2":
                    case "3":
                    case "4":
                    case "5":
                    case "6":
                    case "7":
                    case "8":
                    case "9":
                    {
                        LoadCharacter(Int32.Parse(key));
                        StartGame();
                        done = true;
                        break;
                    };

                    case "ESC": done = true; break;
                };
            }
        }

        public static void StartGame ()
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

            InitializeNewGame(); // Only sound and graphic resources
            plyr.status = GameStates.Explore;

            LoadDungeonItems(); // Sets up the Dungeon items char array

            GameLoop(); // Enter the main game loop
        }

        public static void ToggleAndInitializeFont ()
        {
            if (plyr.fontStyle == 0)
                plyr.fontStyle = 1;
            else
                plyr.fontStyle = 0;
            InitFont();
        }

        public static void ToggleMusic () => plyr.musicStyle = !plyr.musicStyle;        
    }
}