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

            //TODO: This doesn't actually work correctly since we don't clone the objects but it is probably harmless
            counters = dungeonCounters;
            //for (var i = 0; i < counters.Length; i++) // copy dungeon counter data
             //   counters[i] = dungeonCounters[i];

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

                for (var counter = 0; counter < counters.Length; ++counter)
                {
                    if (counters[counter].speed == 0)
                    {
                        counters[counter].speed = counters[counter].speed_initial;
                        var pos = counters[counter].Position;
                        pos.Y--;
                        if (pos.Y == 82) // 32
                        {
                            counters[counter].value1 = counters[counter].value2;
                            counters[counter].value2 = Random(0, 13) + 10;
                            pos.Y = 98; // 40
                        }
                        if ((counter == 7) && (pos.Y == 320)) // silver
                        {
                            counters[counter].value1 = counters[counter].value2;
                            counters[counter].value2 = Random(0, 30) + 50; // silver is 40 + random number between 1 and 30
                            pos.Y = 336;
                        };
                        counters[counter].Position = pos;
                    } else
                    {
                        counters[counter].speed--;
                    };
                }
            }

            // key pressed

            StopDungeonGateSound1(); // stop looping gate sound
            PlayDungeonGateSound2(); // start longer gate sound

            // display chosen digits
            for (var counter = 0; counter < counters.Length; ++counter)
            {
                var pos = counters[counter].Position;
                if (counter != 7)
                {                     
                    if (pos.Y < 90)
                        counters[counter].value1 = counters[counter].value2;
                    pos.Y = 96;
                } else
                {
                    if (pos.Y < 344)
                        counters[7].value1 = counters[7].value2;
                    pos.Y = 336;
                };
                counters[counter].Position = pos;
            };
            
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

            //TODO: This doesn't actually work correctly since we don't clone the objects but it is probably harmless
            //for (var i = 0; i < 8; i++) // copy city counter data
            //    counters[i] = cityCounters[i];
            counters = cityCounters;

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

                for (var counter = 0; counter < counters.Length; ++counter)
                {
                    if (counters[counter].speed == 0)
                    {
                        var pos = counters[counter].Position;

                        counters[counter].speed = counters[counter].speed_initial;
                        pos.Y--;
                        if (pos.Y == 82) 
                        {
                            counters[counter].value1 = counters[counter].value2;
                            counters[counter].value2 = Random(0, 12) + 10;
                            pos.Y = 96; 
                        }
                        if ((counter == 7) && (pos.Y == 356)) // copper
                        {
                            counters[counter].value1 = counters[counter].value2;
                            counters[counter].value2 = Random(0, 50) + 49; // copper is 0-50 + 49 (giving max of 99 coppers)
                            pos.Y = 370;
                        };

                        counters[counter].Position = pos;
                    } else
                    {
                        counters[counter].speed--;
                    };
                }
            }

            // key pressed

            StopCityGateSound1(); // stop looping gate sound
            PlayCityGateSound2(); // start longer gate sound

            // display chosen digits
            for (var counter = 0; counter < counters.Length; ++counter)
            {
                var pos = counters[counter].Position;
                if (counter != 7)
                {
                    if (pos.Y < 88)
                        counters[counter].value1 = counters[counter].value2;
                    pos.Y = 96; // Set vertical position of STR CHA DEX etc
                } else
                {
                    if (pos.Y < 362)
                        counters[7].value1 = counters[7].value2;
                    pos.Y = 370; // Set vertical position of copper counter when key pressed 354
                };
                counters[counter].Position = pos;
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
                    switch (GetSingleKey())
                    {
                        case "M": plyr.gender = 1; break;
                        case "F": plyr.gender = 2; break;
                    };
                }

                DrawPlayerDetails(typed_name);
                while (!player_chosen)
                {
                    switch (GetSingleKey())
                    {
                        case "Y":
                        {
                            details_confirmed = true;
                            player_chosen = true;
                            break;
                        }
                        case "N":
                        {
                            details_confirmed = false;
                            player_chosen = true;
                            plyr.gender = 0;
                            plyr.name = " ";
                            name_completed = false;
                            break;
                        };
                    };
                }

                DrawPlayerDetails(typed_name);
            }
        }

        #region Private Members

        private static void DisplayCounters ()
        {
            var digit1 = "0";
            var digit2 = "0";
            for (var counter = 0; counter < counters.Length; ++counter)
            {
                // Draw the first layer number (upper)
                var pos = counters[counter].Position;
                
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

                DrawImage(digit1, pos);
                DrawImage(digit2, pos.X + 32, pos.Y);

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

                DrawImage(digit1, pos.X, pos.Y + 16);
                DrawImage(digit2, pos.X + 32, pos.Y + 16);
            }
        }        

        private static void DrawPlayerDetails ( string typed_name )
        {
            ClearDisplay();
            DrawText(2, 2, "Create a new character");
            DrawText(2, 5, $"Enter name: {typed_name}_");

            //TODO: What are we doing here?
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
              new CreateCharacterCounter() { value1 = 10, value2 = 16, Position = new System.Drawing.Point(64 - 32, 96), speed = 11, speed_initial = 11 },
              new CreateCharacterCounter() { value1 = 13, value2 = 21, Position = new System.Drawing.Point(144 - 32, 96), speed = 9, speed_initial = 9 },
              new CreateCharacterCounter() { value1 = 15, value2 = 10, Position = new System.Drawing.Point(224 - 32, 96), speed = 8, speed_initial = 8 },
              new CreateCharacterCounter() { value1 = 10, value2 = 10, Position = new System.Drawing.Point(304 - 32, 96), speed = 13, speed_initial = 13 },
              new CreateCharacterCounter() { value1 = 18, value2 = 11, Position = new System.Drawing.Point(384 - 32, 96), speed = 10, speed_initial = 10 },
              new CreateCharacterCounter() { value1 = 10, value2 = 12, Position = new System.Drawing.Point(460 - 32, 96), speed = 8, speed_initial = 8 },
              new CreateCharacterCounter() { value1 = 23, value2 = 15, Position = new System.Drawing.Point(540 - 32, 96), speed = 14, speed_initial = 14 },
              new CreateCharacterCounter() { value1 = 54, value2 = 47, Position = new System.Drawing.Point(560 - 32, 336), speed = 11, speed_initial = 11 }
        };

        //TODO: Move to Scenario
        public static CreateCharacterCounter[] dungeonCounters =
        {
              new CreateCharacterCounter() { value1 = 10, value2 = 16, Position = new System.Drawing.Point(48, 96), speed = 2, speed_initial = 2 },
              new CreateCharacterCounter() { value1 = 13, value2 = 21, Position = new System.Drawing.Point(128, 96), speed = 0, speed_initial = 0 },
              new CreateCharacterCounter() { value1 = 15, value2 = 10, Position = new System.Drawing.Point(208, 96), speed = 2, speed_initial = 2 },
              new CreateCharacterCounter() { value1 = 10, value2 = 10, Position = new System.Drawing.Point(288, 96), speed = 3, speed_initial = 3 },
              new CreateCharacterCounter() { value1 = 18, value2 = 11, Position = new System.Drawing.Point(368, 96), speed = 4, speed_initial = 4 },
              new CreateCharacterCounter() { value1 = 10, value2 = 12, Position = new System.Drawing.Point(444, 96), speed = 1, speed_initial = 1 },
              new CreateCharacterCounter() { value1 = 23, value2 = 15, Position = new System.Drawing.Point(524, 96), speed = 3, speed_initial = 3 },
              new CreateCharacterCounter() { value1 = 54, value2 = 47, Position = new System.Drawing.Point(544, 336), speed = 5, speed_initial = 5 }
        };

        //TODO: Move to Scenario
        public static CreateCharacterCounter[] cityCounters =
        {
              new CreateCharacterCounter() { value1 = 8, value2 = 16, Position = new System.Drawing.Point(48, 96), speed = 2, speed_initial = 2 },
              new CreateCharacterCounter() { value1 = 13, value2 = 21, Position = new System.Drawing.Point(128, 96), speed = 0, speed_initial = 0 },
              new CreateCharacterCounter() { value1 = 15, value2 = 10, Position = new System.Drawing.Point(208, 96), speed = 2, speed_initial = 2 },
              new CreateCharacterCounter() { value1 = 10, value2 = 10, Position = new System.Drawing.Point(288, 96), speed = 4, speed_initial = 4 },
              new CreateCharacterCounter() { value1 = 18, value2 = 11, Position = new System.Drawing.Point(368, 96), speed = 5, speed_initial = 5 },
              new CreateCharacterCounter() { value1 = 10, value2 = 12, Position = new System.Drawing.Point(444, 96), speed = 1, speed_initial = 1 },
              new CreateCharacterCounter() { value1 = 23, value2 = 15, Position = new System.Drawing.Point(528, 96), speed = 4, speed_initial = 4 },
              new CreateCharacterCounter() { value1 = 54, value2 = 47, Position = new System.Drawing.Point(514, 370), speed = 5, speed_initial = 5 }
        };
        #endregion
    }
}