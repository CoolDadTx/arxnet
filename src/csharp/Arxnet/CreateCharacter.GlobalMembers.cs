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

using SFML.Audio;

namespace P3Net.Arx
{
    public partial class GlobalMembers
    {
        //TODO: Move to Scenario
        public static void DungeonGate ()
        {
            var gateNotDone = true;
            plyr.scenario = Scenarios.Dungeon;
            LoadCounterImages();

            for (var i = 0; i < 8; i++) // copy dungeon counter data
                counters[i] = dungeonCounters[i];

            InitDungeonGateSounds();
            PlayDungeonGateSound1(); // play looped sound while counters spin

            while (gateNotDone)
            {
                ClearDisplay();

                DisplayCounters();
                DisplayDungeonGateImage();
                UpdateDisplay();

                var key = GetSingleKey();
                if (key != "")
                    gateNotDone = false;

                var counter = 0;
                while (counter < 8) // number of counters should be 8
                {
                    if (counters[counter].speed == 0)
                    {
                        counters[counter].speed = counters[counter].speed_initial;
                        counters[counter].y--;
                        if (counters[counter].y == 82) // 32
                        {
                            counters[counter].value1 = counters[counter].value2;
                            counters[counter].value2 = Random(0, 13) + 10;
                            counters[counter].y = 98; // 40
                        }
                        if ((counter == 7) && (counters[counter].y == 320)) // silver
                        {
                            counters[counter].value1 = counters[counter].value2;
                            counters[counter].value2 = Random(0, 30) + 50; // silver is 40 + random number between 1 and 30
                            counters[counter].y = 336;
                        }
                    } else
                    {
                        counters[counter].speed--;
                    }
                    counter++;

                }
            }

            // key pressed

            StopDungeonGateSound1(); // stop looping gate sound
            PlayDungeonGateSound2(); // start longer gate sound

            // display chosen digits
            {
                var counter = 0;
                while (counter < 7) // number of counters should be 8
                {
                    if (counters[counter].y < 90)
                        counters[counter].value1 = counters[counter].value2;
                    counters[counter].y = 96; //was 40
                    counter++;
                }
            };

            if (counters[7].y < 344)
                counters[7].value1 = counters[7].value2;
            counters[7].y = 336;

            ClearDisplay();
            DisplayCounters();
            DisplayDungeonGateImage();
            UpdateDisplay();

            // pause
            for (var i = 0; i < 3; i++)
                Sleep(TimeSpan.FromSeconds(1));

            // display "Joined" message

            for (var i = 0; i < 6; i++) 
            {
                Sleep(TimeSpan.FromSeconds(1));
                ClearDisplay();
                DrawText(2, 11, "You are now joined.  Prepare to enter");
                DrawText(6, 13, "Alternate Reality, The Dungeon.");
                UpdateDisplay();

            }

            // assign chosen values to stats
            plyr.sta = counters[0].value1;
            plyr.chr = counters[1].value1;
            plyr.str = counters[2].value1;
            plyr.inte = counters[3].value1;
            plyr.wis = counters[4].value1;
            plyr.skl = counters[5].value1;
            plyr.hp = counters[6].value1;
            plyr.maxhp = counters[6].value1;
            plyr.silver = counters[7].value1;
        }

        //TODO: Move to Scenario
        public static void CityGate ()
        {
            var gateNotDone = true;
            
            plyr.scenario = 0;

            LoadCounterImages();
            for (var i = 0; i < 8; i++) // copy city counter data
                counters[i] = cityCounters[i];

            InitCityGateSounds();
            PlayCityGateSound1(); // play sound once only

            while (gateNotDone)
            {

                ClearDisplay();
                DisplayCounters();
                DisplayCityGateImage();
                UpdateDisplay();

                var key = GetSingleKey();
                if (key != "")
                    gateNotDone = false;

                var counter = 0;
                while (counter < 8) // number of counters should be 8
                {

                    if (counters[counter].speed == 0)
                    {
                        counters[counter].speed = counters[counter].speed_initial;
                        counters[counter].y--;
                        if (counters[counter].y == 82) // 32
                        {
                            counters[counter].value1 = counters[counter].value2;
                            counters[counter].value2 = Random(0, 12) + 10;
                            counters[counter].y = 96; // 40
                        }
                        if ((counter == 7) && (counters[counter].y == 356)) // copper
                        {
                            counters[counter].value1 = counters[counter].value2;
                            counters[counter].value2 = Random(0, 50) + 49; // copper is 0-50 + 49 (giving max of 99 coppers)
                            counters[counter].y = 370;
                        }
                    } else
                    {
                        counters[counter].speed--;
                    }
                    counter++;

                }
            }

            // key pressed

            StopCityGateSound1(); // stop looping gate sound
            PlayCityGateSound2(); // start longer gate sound

            // display chosen digits
            {
                var counter = 0;
                while (counter < 7) // number of counters should be 8
                {
                    if (counters[counter].y < 88)
                        counters[counter].value1 = counters[counter].value2;
                    counters[counter].y = 96; // Set vertical position of STR CHA DEX etc
                    counter++;
                }
                if (counters[7].y < 362)
                    counters[7].value1 = counters[7].value2;
                counters[7].y = 370; // Set vertical position of copper counter when key pressed 354
            };

            // Loop while the sound is playing
            while (cityGate2Sound.Status == SoundStatus.Playing)
            {
                ClearDisplay();
                DisplayCounters();
                DisplayCityGateImage();
                UpdateDisplay();
            }

            PlayCityGateSound3(); // start final gate sound
            while (cityGate3Sound.Status == SoundStatus.Playing)
            {
                ClearDisplay();
                DrawText(2, 11, "You are now joined.  Prepare to enter");
                DrawText(7, 13, "Alternate Reality, The City.");
                UpdateDisplay();
            }

            // assign chosen values to stats
            plyr.sta = counters[0].value1;
            plyr.chr = counters[1].value1;
            plyr.str = counters[2].value1;
            plyr.inte = counters[3].value1;
            plyr.wis = counters[4].value1;
            plyr.skl = counters[5].value1;
            plyr.hp = counters[6].value1;
            plyr.maxhp = counters[6].value1;
            plyr.copper = (counters[7].value1) + 120;
        }

        public static void GetPlayerName ()
        {
            var details_confirmed = false;
            var name_completed = false;
            string typed_name;

            while (!details_confirmed)
            {
                plyr.fixedEncounter = false;
                var player_chosen = false;
                plyr.gender = 0;
                plyr.name = " ";
                typed_name = "";
                DrawPlayerDetails(typed_name);

                while (!name_completed)
                {
                    var single_key = GetTextChar();
                    if (single_key == "SPACE")
                        single_key = " ";
                    if (single_key == "ESC")
                        single_key = "";
                    if ((single_key == "RETURN") && (typed_name != ""))
                    {
                        single_key = "";
                        name_completed = true;
                    }

                    var name_length = typed_name.Length;
                    if (single_key == "BACKSPACE")
                    {
                        if (name_length != 0)
                        {
                            typed_name = typed_name.Substring(0, (name_length - 1));
                        }
                    } else
                    {
                        if (single_key != "RETURN")
                        {
                            if (name_length != 24) // check for limit of name length
                                typed_name += single_key;
                        }
                    }
                    DrawPlayerDetails(typed_name);
                }

                plyr.name = typed_name;

                DrawPlayerDetails(typed_name);

                while (plyr.gender == 0)
                {
                    var single_key = GetSingleKey();
                    if (single_key == "M")
                    {
                        plyr.gender = 1;
                    }
                    if (single_key == "F")
                    {
                        plyr.gender = 2;
                    }
                }

                DrawPlayerDetails(typed_name);
                while (!player_chosen)
                {
                    var single_key = GetSingleKey();
                    if (single_key == "Y")
                    {
                        details_confirmed = true;
                        player_chosen = true;
                    }
                    if (single_key == "N")
                    {
                        details_confirmed = false;
                        player_chosen = true;
                        plyr.gender = 0;
                        plyr.name = " ";
                        name_completed = false;

                    }
                }

                DrawPlayerDetails(typed_name);
            }
        }

        #region Private Members

        private static void DisplayCounters ()
        {
            var digit1 = "0";
            var digit2 = "0";
            var counter = 0;
            while (counter < 8) // number of counters should be 8
            {
                // Draw the first layer number (upper)
                var counter_x = counters[counter].x;
                var counter_y = counters[counter].y;

                var digits = counters[counter].value1.ToString();
                var digits_number = counters[counter].value1;
                if (digits_number < 10)
                {
                    digit1 = "0";
                    digit2 = digits.Substring(0, 1);
                } else
                {
                    digit1 = digits.Substring(0, 1);
                    digit2 = digits.Substring(1, 1);
                }

                DrawImage(digit1, counter_x, counter_y);
                DrawImage(digit2, counter_x + 32, counter_y);

                // Draw the second layer number (lower)
                digits = counters[counter].value2.ToString();
                digits_number = counters[counter].value2;
                if (digits_number < 10)
                {
                    digit1 = "0";
                    digit2 = digits.Substring(0, 1);
                } else
                {
                    digit1 = digits.Substring(0, 1);
                    digit2 = digits.Substring(1, 1);
                }

                DrawImage(digit1, counter_x, counter_y + 16);
                DrawImage(digit2, counter_x + 32, counter_y + 16);

                counter++;
            }
        }        

        private static void DrawPlayerDetails ( string typed_name )
        {
            ClearDisplay();
            DrawText(2, 2, "Create a new character");
            DrawText(2, 5, $"Enter name: {typed_name}_");

            if (!(plyr.name == " "))
                DrawText(2, 16, "Art thou male or female ? (M or F)");

            if (!(plyr.gender == 0))
            {
                DrawText(2, 18, $"Thy name is {typed_name}");
                if (plyr.gender == 1)
                    DrawText(12, 20, "Thy sex is male.");
                else
                    DrawText(11, 20, "Thy sex is female.");
                DrawText(8, 22, "Is this correct? (Y or N)");
            }
            UpdateDisplay();
        }
        #endregion

        #region Review Data

        public static CreateCharacterCounter[] counters =
        {
              new CreateCharacterCounter() { value1 = 10, value2 = 16, x = 64 - 32, y = 96, speed = 11, speed_initial = 11 },
              new CreateCharacterCounter() { value1 = 13, value2 = 21, x = 144 - 32, y = 96, speed = 9, speed_initial = 9 },
              new CreateCharacterCounter() { value1 = 15, value2 = 10, x = 224 - 32, y = 96, speed = 8, speed_initial = 8 },
              new CreateCharacterCounter() { value1 = 10, value2 = 10, x = 304 - 32, y = 96, speed = 13, speed_initial = 13 },
              new CreateCharacterCounter() { value1 = 18, value2 = 11, x = 384 - 32, y = 96, speed = 10, speed_initial = 10 },
              new CreateCharacterCounter() { value1 = 10, value2 = 12, x = 460 - 32, y = 96, speed = 8, speed_initial = 8 },
              new CreateCharacterCounter() { value1 = 23, value2 = 15, x = 540 - 32, y = 96, speed = 14, speed_initial = 14 },
              new CreateCharacterCounter() { value1 = 54, value2 = 47, x = 560 - 32, y = 336, speed = 11, speed_initial = 11 }
        };

        //TODO: Move to Scenario
        public static CreateCharacterCounter[] dungeonCounters =
        {
              new CreateCharacterCounter() { value1 = 10, value2 = 16, x = 48, y = 96, speed = 2, speed_initial = 2 },
              new CreateCharacterCounter() { value1 = 13, value2 = 21, x = 128, y = 96, speed = 0, speed_initial = 0 },
              new CreateCharacterCounter() { value1 = 15, value2 = 10, x = 208, y = 96, speed = 2, speed_initial = 2 },
              new CreateCharacterCounter() { value1 = 10, value2 = 10, x = 288, y = 96, speed = 3, speed_initial = 3 },
              new CreateCharacterCounter() { value1 = 18, value2 = 11, x = 368, y = 96, speed = 4, speed_initial = 4 },
              new CreateCharacterCounter() { value1 = 10, value2 = 12, x = 444, y = 96, speed = 1, speed_initial = 1 },
              new CreateCharacterCounter() { value1 = 23, value2 = 15, x = 524, y = 96, speed = 3, speed_initial = 3 },
              new CreateCharacterCounter() { value1 = 54, value2 = 47, x = 544, y = 336, speed = 5, speed_initial = 5 }
        };

        //TODO: Move to Scenario
        public static CreateCharacterCounter[] cityCounters =
        {
              new CreateCharacterCounter() { value1 = 8, value2 = 16, x = 48, y = 96, speed = 2, speed_initial = 2 },
              new CreateCharacterCounter() { value1 = 13, value2 = 21, x = 128, y = 96, speed = 0, speed_initial = 0 },
              new CreateCharacterCounter() { value1 = 15, value2 = 10, x = 208, y = 96, speed = 2, speed_initial = 2 },
              new CreateCharacterCounter() { value1 = 10, value2 = 10, x = 288, y = 96, speed = 4, speed_initial = 4 },
              new CreateCharacterCounter() { value1 = 18, value2 = 11, x = 368, y = 96, speed = 5, speed_initial = 5 },
              new CreateCharacterCounter() { value1 = 10, value2 = 12, x = 444, y = 96, speed = 1, speed_initial = 1 },
              new CreateCharacterCounter() { value1 = 23, value2 = 15, x = 528, y = 96, speed = 4, speed_initial = 4 },
              new CreateCharacterCounter() { value1 = 54, value2 = 47, x = 514, y = 370, speed = 5, speed_initial = 5 }
        };
        #endregion
    }
}