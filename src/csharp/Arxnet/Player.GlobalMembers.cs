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
    public partial class GlobalMembers
    {
        //TODO: Move to a game clock
        public static void AddDay ()
        {
            if (plyr.days == 31)
            {
                plyr.days = 1; // first day of a new month
                AddMonth();
            } else
            {
                plyr.days++;
            }

            // perform daily actions / checks / shop changes
            StockSmithyWares();
            StockShopWares();
            StockTavernDrinks();
            StockTavernFoods();
            CheckDailybankInterest();
            if (plyr.forgeDays > 0)
                plyr.forgeDays--;
        }

        //TODO: Move to a game clock
        public static void AddHour ()
        {
            if (plyr.hours == 23)
            {
                plyr.hours = 0; // 12 midnight important for Ferry crossing
                plyr.hunger += 2;
                plyr.thirst += 2;
                if (plyr.alcohol > 0)
                    plyr.alcohol--;
                AddDay();
            } else
            {
                plyr.hours++;
                // perform hourly actions / checks
                plyr.hunger += 2;
                plyr.thirst += 2;
                if (plyr.alcohol > 0)
                    plyr.alcohol--;
            }
            CheckActiveMagic();
            UpdateDisease();
            UpdatePoison();
            CheckBackgroundTime();
        }

        //TODO: Move to a game clock
        public static void AddMinute ()
        {
            if (plyr.minutes == 59)
            {
                plyr.minutes = 0;
                AddHour();
            } else
            {
                plyr.minutes++;
            }
            // perform minute actions - disease, hp point loss, check for weather change etc
            CheckBackgroundTime();
        }

        //TODO: Move to a game clock
        public static void AddMonth ()
        {
            if (plyr.months == 12)
            {
                plyr.months = 1; // first month of a new year
                AddYear();
            } else
            {
                plyr.months++;
                // perform monthly actions - are there any? Weather changes?
            }
        }

        //TODO: Move to a game clock
        public static void AddYear () => plyr.years++;


        //TODO: Move to status effect
        public static string CheckAlcohol ()
        {
            var alcoholDesc = ""; // alcohol level is 0
            if ((plyr.alcohol > 0) && (plyr.alcohol < plyr.sta))
                alcoholDesc = "Tipsy";
            if ((plyr.alcohol >= plyr.sta) && (plyr.alcohol < (plyr.sta * 2)))
                alcoholDesc = "Drunk";
            if (plyr.alcohol >= (plyr.sta * 2))
                alcoholDesc = "Very Drunk";
            return alcoholDesc;
        }

        //TODO: Move to a game time class
        public static void CheckBackgroundTime ()
        {
            plyr.timeOfDay = 0;

            if ((plyr.hours > 18) || (plyr.hours < 4))
                plyr.timeOfDay = 1;
            if ((plyr.hours == 4) && (plyr.minutes >= 0) && (plyr.minutes < 30))
                plyr.timeOfDay = 1;

            if ((plyr.hours == 4) && (plyr.minutes > 29) && (plyr.minutes <= 59))
                plyr.timeOfDay = 2;
            if ((plyr.hours == 5) && (plyr.minutes <= 29))
                plyr.timeOfDay = 3;
            if ((plyr.hours == 5) && (plyr.minutes > 29) && (plyr.minutes <= 59))
                plyr.timeOfDay = 4;
            if ((plyr.hours == 6) && (plyr.minutes <= 29))
                plyr.timeOfDay = 5;
            if ((plyr.hours == 6) && (plyr.minutes > 29) && (plyr.minutes <= 59))
                plyr.timeOfDay = 6;
            if ((plyr.hours == 7) && (plyr.minutes <= 29))
                plyr.timeOfDay = 7;

            if ((plyr.hours == 16) && (plyr.minutes <= 29))
                plyr.timeOfDay = 7;
            if ((plyr.hours == 16) && (plyr.minutes > 29) && (plyr.minutes <= 59))
                plyr.timeOfDay = 6;
            if ((plyr.hours == 17) && (plyr.minutes <= 29))
                plyr.timeOfDay = 5;
            if ((plyr.hours == 17) && (plyr.minutes > 29) && (plyr.minutes <= 59))
                plyr.timeOfDay = 4;
            if ((plyr.hours == 18) && (plyr.minutes <= 29))
                plyr.timeOfDay = 3;
            if ((plyr.hours == 18) && (plyr.minutes > 29) && (plyr.minutes <= 59))
                plyr.timeOfDay = 2;
        }

        //TODO: Move to status effect
        public static string CheckDisease ()
        {
            var diseaseDesc = "";
            if ((plyr.diseases[0] > 0) || (plyr.diseases[0] > 0) || (plyr.diseases[0] > 0) || (plyr.diseases[0] > 0))
                diseaseDesc = "Diseased!";
            return diseaseDesc;
        }

        //TODO: Move to status effect
        public static string CheckEncumbrance ()
        {
            var weightDesc = "";
            var weight = ReturnCarriedWeight();
            var encumbrance = weight - (plyr.str + 224);
            if ((encumbrance >= 0) && (encumbrance < 16))
                weightDesc = "Burdened";
            if ((encumbrance >= 16) && (encumbrance < 33))
                weightDesc = "Encumbered";
            if (encumbrance >= 33)
                weightDesc = "Immobilized!";
            return weightDesc;
        }

        //TODO: Move to status effect
        public static string CheckHunger ()
        {
            var hungerDesc = ""; // alcohol level is 0
            if ((plyr.hunger > 16) && (plyr.hunger < 49))
                hungerDesc = "Hungry";
            if ((plyr.hunger > 48) && (plyr.hunger < 97))
                hungerDesc = "Famished";
            if (plyr.hunger > 96)
                hungerDesc = "Starving";
            return hungerDesc;
        }

        //TODO: Move to status effect
        public static string CheckPoison ()
        {
            var poisonDesc = "";
            if ((plyr.poison[0] > 0) || (plyr.poison[0] > 0) || (plyr.poison[0] > 0) || (plyr.poison[0] > 0))
                poisonDesc = "Poisoned!";
            return poisonDesc;
        }

        //TODO: Move to status effect
        public static string CheckThirst ()
        {
            var thirstDesc = ""; // alcohol level is 0
            if ((plyr.thirst > 16) && (plyr.thirst < 33))
                thirstDesc = "Thirsty";
            if ((plyr.thirst > 32) && (plyr.thirst < 57))
                thirstDesc = "Very Thirsty";
            if (plyr.thirst > 56)
                thirstDesc = "Parched";
            return thirstDesc;
        }

        public static void CreateNewCharacter ( Scenarios scenario )
        {
            InitStats();

            switch (scenario)
            {
                case Scenarios.City:
                {
                    plyr.Position = new System.Drawing.Point(35, 36);                    
                    plyr.scenario = Scenarios.City;
                    plyr.map = 0;
                    plyr.facing = Directions.North;
                    break;
                }
                case Scenarios.Dungeon:
                {
                    plyr.scenario = Scenarios.Dungeon;
                    plyr.map = 1;
                    plyr.Position = new System.Drawing.Point(49, 3);                    
                    plyr.facing = Directions.West;
                    break;
                };

                default: throw new NotSupportedException();
            }

            StockSmithyWares();
            StockTavernDrinks();
            StockTavernFoods();
            StockShopWares();

            if (AR_DEV.EnableCharacterCreation)
            {
                GetPlayerName();
                switch (scenario)
                {
                    case Scenarios.City: CityGate(); break;
                    case Scenarios.Dungeon: DungeonGate(); break;
                }
            }
        }

        #region Review Data

        public static bool[,] autoMapExplored = new bool[5, 4096]; // 5 levels of 4096 on/off values

        //TODO: Load effects from data file
        public static EffectItem[] effectBuffer = Arrays.InitializeWithDefaultInstances<EffectItem>(50); // active time limited effects from spells, scrolls, eyes

        public static Player plyr = new Player();

        //TODO: Replace with structured lists of items
        public static int[,] shopDailyWares = new int[15, 12]; //15 shops with 12 items each a day for sale
        public static int[,] smithyDailyWares = new int[4, 10]; // 4 smithies with 10 items each a day for sale
        public static int[,] tavernDailyDrinks = new int[14, 6]; // 14 taverns with 6 drink items each day for sale
        public static int[,] tavernDailyFoods = new int[14, 6]; // 14 taverns with 6 food items each day for sale

        #endregion

        #region Private Members
                
        //TODO: Move to status effect
        private static void CheckActiveMagic ()
        {
            if (plyr.protection1 > 0)
                plyr.protection1--;
            if (plyr.protection2 > 0)
                plyr.protection2--;
            if (plyr.invulnerability[0] > 0)
                plyr.invulnerability[0]--;
            if (plyr.invulnerability[1] > 0)
                plyr.invulnerability[1]--;
            if (plyr.invulnerability[2] > 0)
                plyr.invulnerability[2]--;
            if (plyr.invulnerability[3] > 0)
                plyr.invulnerability[3]--;
            if (plyr.invulnerability[4] > 0)
                plyr.invulnerability[4]--;
            if (plyr.invulnerability[5] > 0)
                plyr.invulnerability[5]--;
            if (plyr.invulnerability[6] > 0)
                plyr.invulnerability[6]--;
            if (plyr.invulnerability[7] > 0)
                plyr.invulnerability[7]--;
            if (plyr.invulnerability[8] > 0)
                plyr.invulnerability[8]--;
        }
                       
        //TODO: Need more descriptive name
        private static void InitStats ()
        {            
            CreateBareHands(); // Put "bare hand" into itemBuffer[0]            
            plyr.clothing[0] = CreateClothing(0); // Put "Cheap Robe" into itemBuffer[1]                                    
        }

        //TODO: Move to status effect class
        private static void UpdateDisease ()
        {
            var rabiesStatus = plyr.diseases[0];
            if (rabiesStatus > 0)
            {
                // 0 - no rabies, 1-14 in incubation, 15 - active and identified
                if (rabiesStatus < 15)
                    plyr.diseases[0]++;
                else
                    plyr.hp -= 5; // temporary penalty.
            }
        }

        //TODO: Move to status effect class
        private static void UpdatePoison ()
        {
            if (plyr.poison[0] > 0)
                plyr.hp -= 2;
            if (plyr.poison[1] > 0)
                plyr.hp -= 5;
            if (plyr.poison[2] > 0)
                plyr.hp -= 7;
            if (plyr.poison[3] > 0)
                plyr.hp -= 10;
        }
        #endregion
    }
}