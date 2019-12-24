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
using System.Drawing;

namespace P3Net.Arx
{
    public class Player
    {
        //TODO: Why are we defaulting the name?
        public string name { get; set; } = "Xebec";

        //TODO: Make an enum
        public int gender { get; set; } = 1;

        //TODO: Make a range
        public int alignment { get; set; } = 128;

        //TODO: Should this be a calculated value based upon XP?
        /// <summary>Gets or sets the XP level.</summary>        
        public int level { get; set; }
        public int xp { get; set; }

        // Attributes
        //TODO: Consider grouping together with other attributes
        public int str { get; set; } = 17;
        public int sta { get; set; } = 16;
        public int chr { get; set; } = 13;
        public int inte { get; set; } = 14;
        public int wis { get; set; } = 15;
        public int skl { get; set; } = 18;

        //TODO: Are these "current" values?
        public int chrPartials { get; set; }
        public int intPartials { get; set; }
        public int strPartials { get; set; }

        // Secondary attributes
        public int speed { get; set; } = 10;
        public int stealth { get; set; } = 4;
        
        // Health
        public int hp { get; set; } = 15;
        public int maxhp { get; set; } = 15;

        // Effects
        //TODO: Group status effects or consider a list of effects so we can add later
        public bool alive { get; set; } = true;
        public int alcohol { get; set; }
        public int hunger { get; set; }
        public int thirst { get; set; }
        public int digestion { get; set; }
        public int delusion { get; set; }
        public int[] diseases { get; set; } = new int[4];
        public int invisibility { get; set; }
        public int[] invulnerability { get; set; } = new int[9];
        public int treasureFinding { get; set; } = 15;
        public int[] poison { get; set; } = new int[4]; // Four strengths of poison
        public int noticeability { get; set; }
        public int protection1 { get; set; }
        public int protection2 { get; set; }        

        // Location
        //TODO: Should location information be stored elsewhere or grouped together?
        public Directions facing { get; set; } = Directions.North;

        //TODO: Should be attribute of map, not player
        //TODO: Are map and scenario related?
        public int map { get; set; }
        public Scenarios scenario { get; set; } = Scenarios.Unknown;
        public int zone { get; set; } = 0; // Inited at 1 but reset to 0 later in original code
        public int zoneSet { get; set; }

        [Obsolete("Use MapSize")]
        public int mapWidth
        {
            get => _mapSize.Width;
            set => _mapSize.Width = value;
        }

        [Obsolete("Use MapSize")]
        public int mapHeight
        {
            get => _mapSize.Height;
            set => _mapSize.Height = value;
        }

        public Size MapSize
        {
            //TODO: Use auto property once obsolete removed
            get => _mapSize;
            set => _mapSize = value;
        }

        // State
        //TODO: Group state management
        public GameStates status { get; set; } = GameStates.Explore;

        //TODO: Do we really need this?
        public string status_text { get; set; }

        //TODO: These should be stored elsewhere
        public bool diagOn { get; set; }
        public bool fpsOn { get; set; }
        public bool miniMapOn { get; set; }

        // Time of day
        //TODO: Store TOD elsewhere
        public int days { get; set; } = 30;
        public int months { get; set; } = 4;
        public int years { get; set; }

        public int hours { get; set; } = 12;
        public int minutes { get; set; }

        //TODO: Make TimeSpan
        public int timeOfDay { get; set; }

        // Equipped items (index into itemBuffer)
        public int priWeapon { get; set; }
        public int secWeapon { get; set; }
        public int armsArmour { get; set; } = 255;  //None
        public int bodyArmour { get; set; } = 255;
        public int headArmour { get; set; } = 255;
        public int legsArmour { get; set; } = 255;
        
        // Inventory
        //TODO: Why are these separate instead of part of inventory?
        public int compasses { get; set; }
        public int food { get; set; } = 3;
        public int water { get; set; } = 3;
        public int torches { get; set; } = 3;
        public int timepieces { get; set; }

        public int[] clothing { get; set; } = Arrays.CreateAndInitialize(4, 255);  //Empty

        // Money
        public int copper { get; set; }
        public int crystals { get; set; }
        public int gems { get; set; }
        public int gold { get; set; }
        public int jewels { get; set; }
        public int keys { get; set; }
        public int silver { get; set; }

        // Quest status
        //TODO: Convert these to a property bag so we can support arbitrary quests
        public bool goblinsChallenged { get; set; }
        public bool goblinsCombat { get; set; }
        public bool goblinsDefeated { get; set; }
        public bool goblinsReforged { get; set; }
        public bool goblinsVisited { get; set; }

        public bool trollsChallenged { get; set; }
        public bool trollsCombat { get; set; }
        public bool trollsDefeated { get; set; }
        public bool trollsReforged { get; set; }
        public bool trollsVisited { get; set; }

        public bool undeadKingVisited { get; set; }

        public int stolenFromVault { get; set; }

        // Faction status 
        //TODO: Make this a property bag so we can add factions later
        public int damonFriendship { get; set; } = 2;
        public int retreatFriendship { get; set; } = 5;
        public int[] smithyFriendships { get; set; } = Arrays.CreateAndInitialize(4, 2);
        public int rathskellerFriendship { get; set; }
        public int[] shopFriendships { get; set; } = Arrays.CreateAndInitialize<int>(15, 2);
        public int[] tavernFriendships { get; set; } = Arrays.CreateAndInitialize<int>(14, 2);

        public bool[] guildAwards { get; set; } = new bool[12];

        public int[] guildMemberships { get; set; } = new int[14]; // slot 1 for full membership, other 13 for associate membership(s)


        // Forge stuff
        //TODO: Does this belong here - forge?
        public int forgeBonus { get; set; }
        public int forgeDays { get; set; }
        public string forgeName { get; set; } = "";
        public int forgeType { get; set; } // Sword, Axe, Mace or Hammer (1,2,3,4)

        // Oracle stuff
        //TODO: Does this need to be here - Oracle
        //TODO: Convert to TimeSpan - Oracle
        public int oracleDay { get; set; } = 255;  //Unset
        public int oracleMonth { get; set; } = 255;
        public int oracleYear { get; set; } = 255;

        //TODO: Track using quest property bag
        public int oracleQuestNo { get; set; }
        public bool oracleReturnTomorrow { get; set; }
        
        // Other        
        public int back { get; set; }
        public int front { get; set; }
        public int frontheight { get; set; }

        public int left { get; set; }
        public int leftheight { get; set; }

        public int right { get; set; }
        public int rightheight { get; set; }                

        public int[] bankAccountBalances { get; set; } = new int[9];
        public int[] bankAccountStatuses { get; set; } = new int[9];        

        public int buffer_index { get; set; }
        public int ceiling { get; set; }                                                
        public int current_zone { get; set; } // used by drawing function
                        
        public int doorDetailIndex { get; set; }
        public DoorDetail[] doorDetails { get; set; } = Arrays.InitializeWithDefaultInstances<DoorDetail>(20);

        public bool drawingBigAutomap { get; set; }

        public int effectIndex { get; set; }

        // Encounter stuff
        public bool encounter_done { get; set; } = true;

        public int encounterAnimationRef { get; set; }

        public Encounters encounterRef { get; set; }             

        public bool fixedEncounter { get; set; }

        public int fixedEncounterRef { get; set; }

        public bool[] fixedEncounters { get; set; } = new bool[32];

        public bool[] fixedTreasures { get; set; } = new bool[64];

        public int floorTexture { get; set; }

        //TODO: Make boolean
        public int fontStyle { get; set; } = 1;

        public bool game_on { get; set; }        
                
        //TODO: Healer - to Time?
        public int[] healerDays { get; set; } = new int[2];
        public int[] healerHours { get; set; } = new int[2];
        public int[] healerMinutes { get; set; } = new int[2];
                        
        public int infoPanel { get; set; } = 1;        
        
        public int lcompasses { get; set; }

        public int lcopper { get; set; }

        public int lcrystals { get; set; }                
        
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

        public bool mapOn { get; set; }
        
        public bool movingForward { get; set; }

        public bool musicStyle { get; set; }

        [Obsolete("Use OldLocation")]
        public int oldx
        {
            get => _oldLocation.X;
            set => _oldLocation.X = value;
        }

        [Obsolete("Use OldLocation")]
        public int oldy
        {
            get => _oldLocation.Y;
            set => _oldLocation.Y = value;
        }

        public Point OldLocation
        {
            //TODO: Make auto property once obsolete removed
            get => _oldLocation;
            set => _oldLocation = value;
        }

        public int ringCharges { get; set; }
        
        public int special { get; set; }

        public int specialwall { get; set; }
        
        public int spellIndex { get; set; }        
        
        // counter that is used for flashing teleport sequence
        public int teleporting { get; set; }                        

        [Obsolete("Use Location")]
        public int x
        {
            get => _location.X;
            set => _location.X = value;
        }

        [Obsolete("Use Location")]
        public int y
        {
            get => _location.Y;
            set => _location.Y = value;
        }

        public Point Location        
        {
            //TODO: Make auto property once obsolete removed
            get => _location;
            set => _location = value;
        }

        public float z_offset { get; set; } = 1;           
        
        public void IncreaseExperience ( int value )
        {
            //TODO: Move to a data table for easier maintenance - xp
            var basePower = 2.0;
            var levelBottom = (level == 0) ? 200.0 : 200 * Math.Pow(basePower, level);
            var levelTop = levelBottom * 2;

            levelBottom--;
            xp += value;
            if ((xp > levelBottom) && (xp < levelTop))
                IncreaseLevel();
        }

        #region Private Members

        private void IncreaseLevel ()
        {
            var statBonuses = new int[7];

            level++;

            // Increase hit points
            var hpIncrease = GlobalMembers.Random(0, sta);
            hp += hpIncrease;
            maxhp += hpIncrease;

            // Increase stats
            for (var x = 0; x < statBonuses.Length; ++x)
            {
                var statBonus = GlobalMembers.Random(1, 100); // roll to see if this stat will receive a bonus
                if (statBonus > 30)
                    statBonuses[x] = GlobalMembers.Random(0, 3);
            }

            // Add the bonuses (which might be 0)
            sta += statBonuses[0];
            chr += statBonuses[1];
            str += statBonuses[2];
            inte += statBonuses[3];
            wis += statBonuses[4];
            skl += statBonuses[5];
            speed += statBonuses[6];
        }

        private Size _mapSize = new Size(64, 64);
        private Point _location = new Point(63, 63);
        private Point _oldLocation;
        #endregion
    }
}