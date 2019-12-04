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
    public class EffectItem
    {
        public int duration { get; set; }

        public int effect { get; set; }

        public int negativeValue { get; set; }

        public int positiveValue { get; set; }
    }

	public class DoorDetail
    {
        public int direction { get; set; }

        public int level { get; set; }

        public int x { get; set; }

        public int y { get; set; }
    }

	public class Player
    {
        public int alcohol { get; set; }

        public int alignment { get; set; }

        public bool alive { get; set; }

        public int armsArmour { get; set; }

        public int back { get; set; }

        public int[] bankAccountBalances { get; set; } = new int[9];

        public int[] bankAccountStatuses { get; set; } = new int[9];

        public int bodyArmour { get; set; }

        public int buffer_index { get; set; }

        public int ceiling { get; set; }

        public int chr { get; set; }

        public int chrPartials { get; set; }

        public int[] clothing { get; set; } = new int[4];

        public int compasses { get; set; }

        public int copper { get; set; }

        public int crystals { get; set; }

        public int current_zone { get; set; } // used by drawing function

        public int damonFriendship { get; set; }

        public int days { get; set; }

        public int delusion { get; set; }

        public bool diagOn { get; set; }

        public int digestion { get; set; }

        public int[] diseases { get; set; } = new int[4];

        public int doorDetailIndex { get; set; }

        public DoorDetail[] doorDetails { get; set; } = Arrays.InitializeWithDefaultInstances<DoorDetail>(20);

        public bool drawingBigAutomap { get; set; }

        public int effectIndex { get; set; }

        public bool encounter_done { get; set; }

        public int encounterAnimationRef { get; set; }

        public int encounterRef { get; set; }

        public int facing { get; set; }

        public bool fixedEncounter { get; set; }

        public int fixedEncounterRef { get; set; }

        public bool[] fixedEncounters { get; set; } = new bool[32];

        public bool[] fixedTreasures { get; set; } = new bool[64];

        public int floorTexture { get; set; }

        public int fontStyle { get; set; }

        public int food { get; set; }

        public int forgeBonus { get; set; }

        public int forgeDays { get; set; }

        public string forgeName { get; set; }

        public int forgeType { get; set; } // Sword, Axe, Mace or Hammer (1,2,3,4)

        public bool fpsOn { get; set; }

        public int front { get; set; }

        public int frontheight { get; set; }

        public bool game_on { get; set; }

        public int gems { get; set; }

        public int gender { get; set; }

        public bool goblinsChallenged { get; set; }

        public bool goblinsCombat { get; set; }

        public bool goblinsDefeated { get; set; }

        public bool goblinsReforged { get; set; }
        
        public bool goblinsVisited { get; set; }

        public int gold { get; set; }

        public bool[] guildAwards { get; set; } = new bool[12];

        public int[] guildMemberships { get; set; } = new int[14]; // slot 1 for full membership, other 13 for associate membership(s)

        public int headArmour { get; set; }

        public int[] healerDays { get; set; } = new int[2];

        public int[] healerHours { get; set; } = new int[2];

        public int[] healerMinutes { get; set; } = new int[2];

        public int hours { get; set; }

        public int hp { get; set; }

        public int hunger { get; set; }

        public int infoPanel { get; set; }

        public int inte { get; set; }

        public int intPartials { get; set; }

        public int invisibility { get; set; }

        public int[] invulnerability { get; set; } = new int[9];

        public int jewels { get; set; }

        public int keys { get; set; }

        public int lcompasses { get; set; }

        public int lcopper { get; set; }

        public int lcrystals { get; set; }

        public int left { get; set; }

        public int leftheight { get; set; }

        public int legsArmour { get; set; }

        public int level { get; set; } // xp level

        public int lfood { get; set; }

        public int lgems { get; set; }

        public int lgold { get; set; }

        public int ljewels { get; set; }

        public int lkeys { get; set; }

        public int location { get; set; }

        public int lsilver { get; set; }

        public int ltimepieces { get; set; }

        public int ltorches { get; set; }

        public int lwater { get; set; }

        public int map { get; set; }

        public int mapHeight { get; set; }

        public bool mapOn { get; set; }

        public int mapWidth { get; set; }

        public int maxBufferSize { get; set; }

        public int maxhp { get; set; }

        public string message { get; set; }

        public bool miniMapOn { get; set; }

        public int minutes { get; set; }

        public int months { get; set; }

        public bool movingForward { get; set; }

        public bool musicStyle { get; set; }

        public string name { get; set; }

        public int noticeability { get; set; }

        public int oldx { get; set; }

        public int oldy { get; set; }

        public int oracleDay { get; set; }

        public int oracleMonth { get; set; }

        public int oracleQuestNo { get; set; }

        public bool oracleReturnTomorrow { get; set; }

        public int oracleYear { get; set; }

        public int[] poison { get; set; } = new int[4]; // Four strengths of poison

        public int priWeapon { get; set; }

        public int protection1 { get; set; }

        public int protection2 { get; set; }

        public int rathskellerFriendship { get; set; }

        public int retreatFriendship { get; set; }

        public int right { get; set; }

        public int rightheight { get; set; }

        public int ringCharges { get; set; }

        public int scenario { get; set; }

        public int secWeapon { get; set; }

        public int[] shopFriendships { get; set; } = new int[15];

        public int silver { get; set; }

        public int skl { get; set; }

        public int[] smithyFriendships { get; set; } = new int[4];

        public int special { get; set; }

        public int specialwall { get; set; }

        public int speed { get; set; }

        public int spellIndex { get; set; }

        public int sta { get; set; }

        public int status { get; set; }

        public string status_text { get; set; }

        public int stealth { get; set; }

        public int stolenFromVault { get; set; }

        public int str { get; set; }

        public int strPartials { get; set; }

        public int[] tavernFriendships { get; set; } = new int[14];

        public int teleporting { get; set; }

        public int thirst { get; set; }

        public int timeOfDay { get; set; }

        public int timepieces { get; set; }

        public int torches { get; set; }

        public int treasureFinding { get; set; }

        public bool trollsChallenged { get; set; }

        public bool trollsCombat { get; set; }

        public bool trollsDefeated { get; set; }

        public bool trollsReforged { get; set; }

        public bool trollsVisited { get; set; }

        public bool undeadKingVisited { get; set; }

        public int water { get; set; }

        public string weapon1 { get; set; }

        public string weapon2 { get; set; }

        public int windowStyle { get; set; }

        public int wis { get; set; }

        public int x { get; set; }

        public int xp { get; set; }

        public int y { get; set; }

        public int years { get; set; }

        public float z_offset { get; set; }

        public int zone { get; set; }

        public int zoneSet { get; set; }
    }

	public partial class GlobalMembers
    {
        public static bool[,] autoMapExplored = new bool[5, 4096]; // 5 levels of 4096 on/off values

        public static EffectItem[] effectBuffer = Arrays.InitializeWithDefaultInstances<EffectItem>(50); // active time limited effects from spells, scrolls, eyes
        
		public static Player plyr = new Player();

        public static int[,] shopDailyWares = new int[15, 12]; //15 shops with 12 items each a day for sale
        public static int[,] smithyDailyWares = new int[4, 10]; // 4 smithies with 10 items each a day for sale
        public static int[,] tavernDailyDrinks = new int[14, 6]; // 14 taverns with 6 drink items each day for sale
        public static int[,] tavernDailyFoods = new int[14, 6]; // 14 taverns with 6 food items each day for sale

        public static void AddDay()
        {
            if(plyr.days == 31)
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
            if(plyr.forgeDays > 0)
                plyr.forgeDays--;
        }

        public static void AddHour()
        {
            if(plyr.hours == 23)
            {
                plyr.hours = 0; // 12 midnight important for Ferry crossing
                plyr.hunger += 2;
                plyr.thirst += 2;
                if(plyr.alcohol > 0)
                    plyr.alcohol--;
                AddDay();
            } else
            {
                plyr.hours++;
                // perform hourly actions / checks
                plyr.hunger += 2;
                plyr.thirst += 2;
                if(plyr.alcohol > 0)
                    plyr.alcohol--;
            }
            CheckActiveMagic();
            UpdateDisease();
            UpdatePoison();
            CheckBackgroundTime();
        }

        public static void AddMinute()
        {
            if(plyr.minutes == 59)
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

        public static void AddMonth()
        {
            if(plyr.months == 12)
            {
                plyr.months = 1; // first month of a new year
                AddYear();
            } else
            {
                plyr.months++;
                // perform monthly actions - are there any? Weather changes?
            }
        }

        public static void AddYear() => plyr.years++ ;

        public static void CheckActiveMagic()
        {
            if(plyr.protection1 > 0)
                plyr.protection1--;
            if(plyr.protection2 > 0)
                plyr.protection2--;
            if(plyr.invulnerability[0] > 0)
                plyr.invulnerability[0]--;
            if(plyr.invulnerability[1] > 0)
                plyr.invulnerability[1]--;
            if(plyr.invulnerability[2] > 0)
                plyr.invulnerability[2]--;
            if(plyr.invulnerability[3] > 0)
                plyr.invulnerability[3]--;
            if(plyr.invulnerability[4] > 0)
                plyr.invulnerability[4]--;
            if(plyr.invulnerability[5] > 0)
                plyr.invulnerability[5]--;
            if(plyr.invulnerability[6] > 0)
                plyr.invulnerability[6]--;
            if(plyr.invulnerability[7] > 0)
                plyr.invulnerability[7]--;
            if(plyr.invulnerability[8] > 0)
                plyr.invulnerability[8]--;
        }

        public static string CheckAlcohol()
        {
            string alcoholDesc;
            alcoholDesc = ""; // alcohol level is 0
            if((plyr.alcohol > 0) && (plyr.alcohol < plyr.sta))
                alcoholDesc = "Tipsy";
            if((plyr.alcohol >= plyr.sta) && (plyr.alcohol < (plyr.sta * 2)))
                alcoholDesc = "Drunk";
            if(plyr.alcohol >= (plyr.sta * 2))
                alcoholDesc = "Very Drunk";
            return alcoholDesc;
        }

        public static void CheckBackgroundTime()
        {
            plyr.timeOfDay = 0;

            if((plyr.hours > 18) || (plyr.hours < 4))
                plyr.timeOfDay = 1;
            if((plyr.hours == 4) && (plyr.minutes >= 0) && (plyr.minutes < 30))
                plyr.timeOfDay = 1;

            if((plyr.hours == 4) && (plyr.minutes > 29) && (plyr.minutes <= 59))
                plyr.timeOfDay = 2;
            if((plyr.hours == 5) && (plyr.minutes <= 29))
                plyr.timeOfDay = 3;
            if((plyr.hours == 5) && (plyr.minutes > 29) && (plyr.minutes <= 59))
                plyr.timeOfDay = 4;
            if((plyr.hours == 6) && (plyr.minutes <= 29))
                plyr.timeOfDay = 5;
            if((plyr.hours == 6) && (plyr.minutes > 29) && (plyr.minutes <= 59))
                plyr.timeOfDay = 6;
            if((plyr.hours == 7) && (plyr.minutes <= 29))
                plyr.timeOfDay = 7;

            if((plyr.hours == 16) && (plyr.minutes <= 29))
                plyr.timeOfDay = 7;
            if((plyr.hours == 16) && (plyr.minutes > 29) && (plyr.minutes <= 59))
                plyr.timeOfDay = 6;
            if((plyr.hours == 17) && (plyr.minutes <= 29))
                plyr.timeOfDay = 5;
            if((plyr.hours == 17) && (plyr.minutes > 29) && (plyr.minutes <= 59))
                plyr.timeOfDay = 4;
            if((plyr.hours == 18) && (plyr.minutes <= 29))
                plyr.timeOfDay = 3;
            if((plyr.hours == 18) && (plyr.minutes > 29) && (plyr.minutes <= 59))
                plyr.timeOfDay = 2;
        }

        public static string CheckDisease()
        {
            var diseaseDesc = "";
            if((plyr.diseases[0] > 0) || (plyr.diseases[0] > 0) || (plyr.diseases[0] > 0) || (plyr.diseases[0] > 0))
                diseaseDesc = "Diseased!";
            return diseaseDesc;
        }

        public static string CheckEncumbrance()
        {
            var weightDesc = "";
            int weight = ReturnCarriedWeight();
            int encumbrance = weight - (plyr.str + 224);
            if((encumbrance >= 0) && (encumbrance < 16))
                weightDesc = "Burdened";
            if((encumbrance >= 16) && (encumbrance < 33))
                weightDesc = "Encumbered";
            if(encumbrance >= 33)
                weightDesc = "Immobilized!";
            return weightDesc;
        }

        public static string CheckHunger()
        {
            string hungerDesc;
            hungerDesc = ""; // alcohol level is 0
            if((plyr.hunger > 16) && (plyr.hunger < 49))
                hungerDesc = "Hungry";
            if((plyr.hunger > 48) && (plyr.hunger < 97))
                hungerDesc = "Famished";
            if(plyr.hunger > 96)
                hungerDesc = "Starving";
            return hungerDesc;
        }

        public static string CheckPoison()
        {
            var poisonDesc = "";
            if((plyr.poison[0] > 0) || (plyr.poison[0] > 0) || (plyr.poison[0] > 0) || (plyr.poison[0] > 0))
                poisonDesc = "Poisoned!";
            return poisonDesc;
        }

        public static string CheckThirst()
        {
            string thirstDesc;
            thirstDesc = ""; // alcohol level is 0
            if((plyr.thirst > 16) && (plyr.thirst < 33))
                thirstDesc = "Thirsty";
            if((plyr.thirst > 32) && (plyr.thirst < 57))
                thirstDesc = "Very Thirsty";
            if(plyr.thirst > 56)
                thirstDesc = "Parched";
            return thirstDesc;
        }

        public static void CreateNewCharacter(int scenario)
        {
            InitStats();

            switch(scenario)
            {
                case Scenarios.City:
                    plyr.x = 35;
                    plyr.y = 36;
                    plyr.scenario = (int)Scenarios.City;
                    plyr.map = 0;
                    plyr.facing = (int)Directions.North;
                    break;
                case Scenarios.Dungeon:
                    plyr.scenario = (int)Scenarios.Dungeon;
                    plyr.map = 1;
                    plyr.x = 49;
                    plyr.y = 3;
                    plyr.facing = (int)Directions.West;
                    break;
            }

            StockSmithyWares();
            StockTavernDrinks();
            StockTavernFoods();
            StockShopWares();

            if(AR_DEV.CHARACTER_CREATION == OnOff.On)
            {
                GetPlayerName();
                switch(scenario)
                {
                    case Scenarios.City:
                        CityGate();
                        break;
                    case Scenarios.Dungeon:
                        DungeonGate();
                        break;
                }
            }
        }

        public static void IncreaseExperience(int xpIncrease)
        {
            double levelBottom;
            double levelTop;
            double basePower;
            basePower = 2;
            if(plyr.level == 0)
                levelBottom = 200;
            else
                levelBottom = 200 * Math.Pow(basePower, plyr.level);
            levelTop = levelBottom * 2;
            levelBottom--;
            plyr.xp += xpIncrease;
            if((plyr.xp > levelBottom) && (plyr.xp < levelTop))
                IncreaseLevel();
        }

        public static void IncreaseLevel()
        {
            var statBonuses = new int[7];
            int statBonus;
            plyr.level++;
            for(var x = 0; x < 7; ++x)
                statBonuses[x] = 0;

            // Increase hit points
            int hpIncrease = Randn(0, plyr.sta);
            plyr.hp += hpIncrease;
            plyr.maxhp += hpIncrease;

            // Increase stats
            for(var x = 0; x < 7; ++x)
            {
                statBonus = Randn(1, 100); // roll to see if this stat will receive a bonus
                if(statBonus > 30)
                    statBonuses[x] = Randn(0, 3);
            }

            // Add the bonuses (which might be 0)
            plyr.sta += statBonuses[0];
            plyr.chr += statBonuses[1];
            plyr.str += statBonuses[2];
            plyr.inte += statBonuses[3];
            plyr.wis += statBonuses[4];
            plyr.skl += statBonuses[5];
            plyr.speed += statBonuses[6];
        }

        public static void InitStats()
        {
            for(var i = 0; i < 14; i++)
                plyr.guildMemberships[i] = 0;
            for(var i = 0; i <= 11; i++)
                plyr.guildAwards[i] = false;
            for(var i = 0; i < 32; i++)
                plyr.fixedEncounters[i] = false;
            for(var i = 0; i < 9; i++)
                plyr.bankAccountStatuses[i] = 0;
            for(var i = 0; i < 9; i++)
                plyr.bankAccountBalances[i] = 0;
            for(var i = 0; i < 14; i++)
                plyr.tavernFriendships[i] = 2;
            for(var i = 0; i < 15; i++)
                plyr.shopFriendships[i] = 2;

            plyr.healerDays[0] = 0;
            plyr.healerHours[0] = 0;
            plyr.healerMinutes[0] = 0;
            plyr.healerDays[1] = 0;
            plyr.healerHours[1] = 0;
            plyr.healerMinutes[1] = 0;
            plyr.teleporting = 0; // counter that is used for flashing teleport sequence
            plyr.z_offset = 1.0f;
            plyr.scenario = 255; // city 0, dungeon 1 - 255 means main menu for font choice
            plyr.map = 0; // city
            plyr.x = 63; // 49
            plyr.y = 63; // 3
            plyr.facing = 2; // 2 equals north?
            plyr.sta = 16;
            plyr.chr = 13;
            plyr.str = 17;
            plyr.inte = 14;
            plyr.wis = 15;
            plyr.skl = 18;
            plyr.hp = 15;
            plyr.maxhp = 15;
            plyr.name = "Xebec";
            plyr.xp = 0;
            plyr.level = 0;
            plyr.alive = true;
            plyr.encounter_done = true;
            plyr.gender = 1;
            plyr.zone = 1;
            plyr.status = 1; // Exploring
            plyr.torches = 3;
            plyr.food = 3;
            plyr.water = 3;
            plyr.timepieces = 0;
            plyr.compasses = 0;
            plyr.gems = 0;
            plyr.jewels = 0;
            plyr.crystals = 0;
            plyr.copper = 0;
            plyr.silver = 0;
            plyr.gold = 0;
            plyr.keys = 0;

            plyr.infoPanel = 1;
            plyr.stealth = 4; //was 4
            plyr.speed = 10;
            plyr.diagOn = false;
            plyr.fpsOn = false;
            plyr.miniMapOn = false;

            plyr.chrPartials = 0;
            plyr.intPartials = 0;
            plyr.strPartials = 0;
            plyr.buffer_index = 0;
            plyr.zone = 0;
            plyr.zoneSet = 0;
            plyr.specialwall = 0;

            plyr.minutes = 0;
            plyr.hours = 12; // Atari 8bit time
            plyr.days = 30;
            plyr.months = 4;
            plyr.years = 0;
            plyr.timeOfDay = 0;

            // Quest flags
            plyr.goblinsVisited = false;
            plyr.goblinsChallenged = false;
            plyr.trollsVisited = false;
            plyr.trollsChallenged = false;
            plyr.trollsDefeated = false;
            plyr.trollsCombat = false;
            plyr.goblinsDefeated = false;
            plyr.goblinsCombat = false;
            plyr.goblinsReforged = false;
            plyr.trollsReforged = false;

            plyr.thirst = 0;
            plyr.hunger = 0;
            plyr.digestion = 0;
            plyr.alcohol = 0;

            plyr.alignment = 128; // was 128

            plyr.spellIndex = 0;

            // Create starting items and set weapon, armour and clothing slots to initial values
			// Do not set these values manually - must be set by USE command in game! They hold array references within inventory

            plyr.buffer_index = 0;

            CreateBareHands(); // Put "bare hand" into itemBuffer[0]
            plyr.priWeapon = 0; // bare hand should always be itemBuffer[0]
            plyr.secWeapon = 0; // bare hand should always be itemBuffer[0]
            plyr.headArmour = 255; // none
            plyr.bodyArmour = 255; // none
            plyr.legsArmour = 255; // none
            plyr.armsArmour = 255; // none

            plyr.clothing[0] = CreateClothing(0); // Put "Cheap Robe" into itemBuffer[1]
            plyr.clothing[1] = 255; // Slot 2 empty
            plyr.clothing[2] = 255; // Slot 3 empty
            plyr.clothing[3] = 255; // Slot 4 empty

            plyr.retreatFriendship = 5;
            plyr.damonFriendship = 2;
            plyr.smithyFriendships[0] = 2;
            plyr.smithyFriendships[1] = 2;
            plyr.smithyFriendships[2] = 2;
            plyr.smithyFriendships[3] = 2;

            for(var i = 0; i <= 35; i++)
                spellBuffer[i].no = 255;
            for(var i = 0; i < 20; i++)
            {
                plyr.doorDetails[i].direction = 0;
                plyr.doorDetails[i].x = 0;
                plyr.doorDetails[i].y = 0;
                plyr.doorDetails[i].level = 0;
            } // Initialise door buffer
            plyr.doorDetailIndex = 0;

            plyr.oracleReturnTomorrow = false;
            plyr.oracleDay = 255; // unset
            plyr.oracleMonth = 255;
            plyr.oracleYear = 255;
            plyr.oracleQuestNo = 0; // First quest from Oracle is silver key & palace prison

            plyr.treasureFinding = 15;
            plyr.invisibility = 0;
            plyr.diseases[0] = 0;
            plyr.diseases[1] = 0;
            plyr.diseases[2] = 0;
            plyr.diseases[3] = 0;
            plyr.poison[0] = 0;
            plyr.poison[1] = 0;
            plyr.poison[2] = 0;
            plyr.poison[3] = 0;
            plyr.delusion = 0;
            for(var i = 0; i < 9; i++)
                plyr.invulnerability[i] = 0;
            plyr.noticeability = 0;
            plyr.protection1 = 0;
            plyr.protection2 = 0;

            plyr.fixedEncounter = false;

            plyr.undeadKingVisited = false;
            plyr.special = 0;
            plyr.mapWidth = 64;
            plyr.mapHeight = 64;

            plyr.forgeDays = 0;
            plyr.forgeType = 0;
            plyr.forgeBonus = 0;
            plyr.forgeName = "";

            plyr.stolenFromVault = 0;
        }

        public static void UpdateDisease()
        {
            int rabiesStatus = plyr.diseases[0];
            if(rabiesStatus > 0)
            {
                // 0 - no rabies, 1-14 in incubation, 15 - active and identified
                if(rabiesStatus < 15)
                    plyr.diseases[0]++;
                else
                    plyr.hp -= 5; // temporary penalty.
            }
        }

        public static void UpdatePoison()
        {
            if(plyr.poison[0] > 0)
                plyr.hp -= 2;
            if(plyr.poison[1] > 0)
                plyr.hp -= 5;
            if(plyr.poison[2] > 0)
                plyr.hp -= 7;
            if(plyr.poison[3] > 0)
                plyr.hp -= 10;
        }

        //extern Player plyr;

        //extern effectItem effectBuffer[50]; // active time limited effects from spells, scrolls, eyes

        //extern bool autoMapExplored[5][4096]; // 5 levels of 4096 on/off values

        //extern int shopDailyWares[15][12]; //15 shops with 12 items each a day for sale
        //extern int smithyDailyWares[4][10]; // 4 smithies with 10 items each a day for sale
        //extern int tavernDailyFoods[14][6]; // 14 taverns with 6 food items each day for sale
        //extern int tavernDailyDrinks[14][6]; // 14 taverns with 6 drink items each day for sale
    }
}