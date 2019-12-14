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
using System.Xml.Schema;

namespace P3Net.Arx
{
    public enum DamageImpacts
    {
        INVULNERABLE = 0xF0,
        VULNERABLE = 0x0F
    }

    public class EncRecord
    {
        public int encProb { get; set; }

        public Encounters encType { get; set; }
    }

    public partial class GlobalMembers
    {
        public static bool checkForTreasure;
        public static string[] consoleMessages = new string[MAX_CONSOLE_MESSAGES];
        public static int curOpponent; // 0-7

        public static EncRecord[] dayEncTable =
        {
            new EncRecord()
            { encProb = 41, encType = Encounters.Guard },
            new EncRecord()
            { encProb = 19, encType = Encounters.Commoner },
            new EncRecord()
            { encProb = 19, encType = Encounters.Merchant },
            new EncRecord()
            { encProb = 18, encType = Encounters.Apprentice },
            new EncRecord()
            { encProb = 18, encType = Encounters.Acolyte },
            new EncRecord()
            { encProb = 17, encType = Encounters.Archmage },
            new EncRecord()
            { encProb = 16, encType = Encounters.Courier },
            new EncRecord()
            { encProb = 12, encType = Encounters.Knight },
            new EncRecord()
            { encProb = 10, encType = Encounters.Novice },
            new EncRecord()
            { encProb = 10, encType = Encounters.Mugger },
            new EncRecord()
            { encProb = 8, encType = Encounters.Wizard },
            new EncRecord()
            { encProb = 8, encType = Encounters.Champion },
            new EncRecord()
            { encProb = 7, encType = Encounters.Fighter },
            new EncRecord()
            { encProb = 6, encType = Encounters.Swordsman },
            new EncRecord()
            { encProb = 6, encType = Encounters.Thief },
            new EncRecord()
            { encProb = 6, encType = Encounters.Warrior },
            new EncRecord()
            { encProb = 5, encType = Encounters.Thief },
            new EncRecord()
            { encProb = 5, encType = Encounters.Nobleman },
            new EncRecord()
            { encProb = 4, encType = Encounters.Pauper },
            new EncRecord()
            { encProb = 4, encType = Encounters.Gladiator },
            new EncRecord()
            { encProb = 3, encType = Encounters.Guard },
            new EncRecord()
            { encProb = 3, encType = Encounters.Knight },
            new EncRecord()
            { encProb = 3, encType = Encounters.Thief },
            new EncRecord()
            { encProb = 1, encType = Encounters.Robber },
            new EncRecord()
            { encProb = 1, encType = Encounters.Mage },
            new EncRecord()
            { encProb = 1, encType = Encounters.Assassin },
            new EncRecord()
            { encProb = 1, encType = Encounters.Thief },
            new EncRecord()
            { encProb = 1, encType = Encounters.Weaponmaster },
            new EncRecord()
            { encProb = 2, encType = Encounters.Noblewoman }
        };

        public static readonly int DUNGEON_TABLE_ENCOUNTERS = 64;

        public static EncRecord[] dungeonTable =
        {
            new EncRecord()
            { encProb = 8, encType = Encounters.Thief },
            new EncRecord()
            { encProb = 6, encType = Encounters.Thief },
            new EncRecord()
            { encProb = 3, encType = Encounters.Thief },
            new EncRecord()
            { encProb = 5, encType = Encounters.Thief },
            new EncRecord()
            { encProb = 3, encType = Encounters.Goblin },
            new EncRecord()
            { encProb = 3, encType = Encounters.Troll },
            new EncRecord()
            { encProb = 3, encType = Encounters.Lich },
            new EncRecord()
            { encProb = 6, encType = Encounters.Guard },
            new EncRecord()
            { encProb = 3, encType = Encounters.DarkKnight },
            new EncRecord()
            { encProb = 3, encType = Encounters.Champion },
            new EncRecord()
            { encProb = 13, encType = Encounters.Healer },
            new EncRecord()
            { encProb = 8, encType = Encounters.Knight },
            new EncRecord()
            { encProb = 8, encType = Encounters.Pauper },
            new EncRecord()
            { encProb = 8, encType = Encounters.Nobleman },
            new EncRecord()
            { encProb = 8, encType = Encounters.Novice },
            new EncRecord()
            { encProb = 3, encType = Encounters.Apprentice },
            new EncRecord()
            { encProb = 3, encType = Encounters.Mage },
            new EncRecord()
            { encProb = 3, encType = Encounters.Wizard },
            new EncRecord()
            { encProb = 3, encType = Encounters.Acolyte },
            new EncRecord()
            { encProb = 3, encType = Encounters.Sage },
            new EncRecord()
            { encProb = 3, encType = Encounters.Orc },
            new EncRecord()
            { encProb = 3, encType = Encounters.Gnome },
            new EncRecord()
            { encProb = 3, encType = Encounters.Dwarf },
            new EncRecord()
            { encProb = 8, encType = Encounters.Slime },
            new EncRecord()
            { encProb = 8, encType = Encounters.Mold },
            new EncRecord()
            { encProb = 3, encType = Encounters.Homunculus },
            new EncRecord()
            { encProb = 3, encType = Encounters.Phoenix },
            new EncRecord()
            { encProb = 3, encType = Encounters.Sorceress },
            new EncRecord()
            { encProb = 3, encType = Encounters.Whirlwind },
            new EncRecord()
            { encProb = 8, encType = Encounters.GiantRat },
            new EncRecord()
            { encProb = 3, encType = Encounters.SmallDragon },
            new EncRecord()
            { encProb = 3, encType = Encounters.Skeleton },
            new EncRecord()
            { encProb = 3, encType = Encounters.Zombie },
            new EncRecord()
            { encProb = 3, encType = Encounters.Ghoul },
            new EncRecord()
            { encProb = 3, encType = Encounters.Ghost },
            new EncRecord()
            { encProb = 3, encType = Encounters.Spectre },
            new EncRecord()
            { encProb = 3, encType = Encounters.Wraith },
            new EncRecord()
            { encProb = 3, encType = Encounters.Vampire },
            new EncRecord()
            { encProb = 8, encType = Encounters.GreatBat },
            new EncRecord()
            { encProb = 3, encType = Encounters.Hellhound },
            new EncRecord()
            { encProb = 3, encType = Encounters.Harpy },
            new EncRecord()
            { encProb = 3, encType = Encounters.Gremlin },
            new EncRecord()
            { encProb = 3, encType = Encounters.Imp },
            new EncRecord()
            { encProb = 3, encType = Encounters.FlameDemon },
            new EncRecord()
            { encProb = 3, encType = Encounters.StormDevil },
            new EncRecord()
            { encProb = 3, encType = Encounters.GiantWolf },
            new EncRecord()
            { encProb = 3, encType = Encounters.Werewolf },
            new EncRecord()
            { encProb = 3, encType = Encounters.Warrior },
            new EncRecord()
            { encProb = 3, encType = Encounters.Weaponmaster },
            new EncRecord()
            { encProb = 3, encType = Encounters.Valkyrie },
            new EncRecord()
            { encProb = 3, encType = Encounters.Gladiator },
            new EncRecord()
            { encProb = 3, encType = Encounters.Mercenary },
            new EncRecord()
            { encProb = 3, encType = Encounters.Doppleganger },
            new EncRecord()
            { encProb = 3, encType = Encounters.Adventurer },
            new EncRecord()
            { encProb = 3, encType = Encounters.Watersprite },
            new EncRecord()
            { encProb = 3, encType = Encounters.Nightstalker },
            new EncRecord()
            { encProb = 3, encType = Encounters.Salamander },
            new EncRecord()
            { encProb = 3, encType = Encounters.Ronin },
            new EncRecord()
            { encProb = 3, encType = Encounters.Serpentman },
            new EncRecord()
            { encProb = 3, encType = Encounters.BigSnake },
            new EncRecord()
            { encProb = 3, encType = Encounters.GreatNaga },
            new EncRecord()
            { encProb = 3, encType = Encounters.Berserker },
            new EncRecord()
            { encProb = 3, encType = Encounters.IceDemon },
            new EncRecord()
            { encProb = 3, encType = Encounters.HornedDevil }
        };
        public static int encAnimation;
        public static int encounterMenu;
        public static bool encounterNotHostile;

        public static int encounterQuantity;
        public static bool encounterRunning;
        public static int encounterTurns;
        public static int flashCount;
        public static bool flashTextOn;
        public static int groundTurnsRemaining;
        public static string key;
        public static readonly int MAX_CONSOLE_MESSAGES = 10;

        public static readonly int MAX_OPPONENTS = 8;

        public static EncRecord[] nightEncTable =
        {
            new EncRecord()
            { encProb = 19, encType = Encounters.Ghost },
            new EncRecord()
            { encProb = 17, encType = Encounters.Mugger },
            new EncRecord()
            { encProb = 17, encType = Encounters.GiantRat },
            new EncRecord()
            { encProb = 17, encType = Encounters.Skeleton },
            new EncRecord()
            { encProb = 16, encType = Encounters.Zombie },
            new EncRecord()
            { encProb = 15, encType = Encounters.Gremlin },
            new EncRecord()
            { encProb = 12, encType = Encounters.Mold },
            new EncRecord()
            { encProb = 10, encType = Encounters.Thief },
            new EncRecord()
            { encProb = 9, encType = Encounters.Thief },
            new EncRecord()
            { encProb = 9, encType = Encounters.Giant },
            new EncRecord()
            { encProb = 8, encType = Encounters.Slime },
            new EncRecord()
            { encProb = 7, encType = Encounters.GiantWolf },
            new EncRecord()
            { encProb = 7, encType = Encounters.Champion },
            new EncRecord()
            { encProb = 7, encType = Encounters.Imp },
            new EncRecord()
            { encProb = 7, encType = Encounters.Ghoul },
            new EncRecord()
            { encProb = 6, encType = Encounters.Fighter },
            new EncRecord()
            { encProb = 5, encType = Encounters.Swordsman },
            new EncRecord()
            { encProb = 5, encType = Encounters.Warrior },
            new EncRecord()
            { encProb = 4, encType = Encounters.Gnoll },
            new EncRecord()
            { encProb = 4, encType = Encounters.Thief },
            new EncRecord()
            { encProb = 3, encType = Encounters.SmallGreenDragon },
            new EncRecord()
            { encProb = 3, encType = Encounters.Gladiator },
            new EncRecord()
            { encProb = 3, encType = Encounters.Goblin },
            new EncRecord()
            { encProb = 3, encType = Encounters.Hobbit },
            new EncRecord()
            { encProb = 3, encType = Encounters.Orc },
            new EncRecord()
            { encProb = 3, encType = Encounters.Dwarf },
            new EncRecord()
            { encProb = 2, encType = Encounters.Wraith },
            new EncRecord()
            { encProb = 2, encType = Encounters.Apprentice },
            new EncRecord()
            { encProb = 2, encType = Encounters.Knight },
            new EncRecord()
            { encProb = 2, encType = Encounters.Acolyte },
            new EncRecord()
            { encProb = 2, encType = Encounters.Troll },
            new EncRecord()
            { encProb = 2, encType = Encounters.Archmage },
            new EncRecord()
            { encProb = 2, encType = Encounters.Robber },
            new EncRecord()
            { encProb = 2, encType = Encounters.Guard },
            new EncRecord()
            { encProb = 2, encType = Encounters.Thief },
            new EncRecord()
            { encProb = 1, encType = Encounters.Novice },
            new EncRecord()
            { encProb = 1, encType = Encounters.Nightstalker },
            new EncRecord()
            { encProb = 1, encType = Encounters.Spectre },
            new EncRecord()
            { encProb = 1, encType = Encounters.Knight },
            new EncRecord()
            { encProb = 1, encType = Encounters.Wizard },
            new EncRecord()
            { encProb = 1, encType = Encounters.Commoner },
            new EncRecord()
            { encProb = 1, encType = Encounters.Merchant },
            new EncRecord()
            { encProb = 1, encType = Encounters.Courier },
            new EncRecord()
            { encProb = 1, encType = Encounters.Weaponmaster },
            new EncRecord()
            { encProb = 1, encType = Encounters.Nobleman },
            new EncRecord()
            { encProb = 1, encType = Encounters.Pauper },
            new EncRecord()
            { encProb = 1, encType = Encounters.Mage },
            new EncRecord()
            { encProb = 1, encType = Encounters.Assassin }
        };

        public static Monster opponent = new Monster();

        public static int opponentNoAttacking;

        public static Monster[] Opponents = Arrays.InitializeWithDefaultInstances<Monster>(8); // max 8 monsters against you

        public static bool opponentSurprised;
        public static Encounters opponentType;
        public static bool playerOnGround;
        public static bool playerRunsAway = false;
        public static bool playerStunned;
        public static bool playerSurprised;
        public static bool playerTurn;
        public static string prefix;

        public static string str; // for message text
        public static string str2;
        public static bool waitingForAnyKey;
        public static bool waitingForSpaceKey;

        public static int[] weaponProbabilities = { 0, 0, 0, 0, 0, 0 };
        public static int[] weaponReferences = { 0, 0, 0, 0, 0, 0 };

        public static EncRecord[] wellLitEncTable =
        {
            new EncRecord()
            { encProb = 30, encType = Encounters.Guard },
            new EncRecord()
            { encProb = 30, encType = Encounters.Thief },
            new EncRecord()
            { encProb = 30, encType = Encounters.GiantRat },
            new EncRecord()
            { encProb = 30, encType = Encounters.Nobleman },
            new EncRecord()
            { encProb = 15, encType = Encounters.Pauper },
            new EncRecord()
            { encProb = 30, encType = Encounters.Healer },
            new EncRecord()
            { encProb = 20, encType = Encounters.Knight },
            new EncRecord()
            { encProb = 20, encType = Encounters.GreatBat },
            new EncRecord()
            { encProb = 20, encType = Encounters.Slime },
            new EncRecord()
            { encProb = 15, encType = Encounters.Adventurer },
            new EncRecord()
            { encProb = 15, encType = Encounters.Acolyte }
        };

        public static void AwardExperience ( Encounters opponentNo )
        {
            // x2 is default experience multiplier value for defeating an opponent in the Dungeon
            var experienceMultiplier = 2;

            //TODO: Move this into Encounters type
            if (opponentNo == Encounters.Ghost)
                experienceMultiplier = 8;
            if (opponentNo == Encounters.Doppleganger)
                experienceMultiplier = 7;
            if (opponentNo == Encounters.Mold)
                experienceMultiplier = 3;

            //TODO: This doesn't look right, shouldn't it be based upon maxHP of actual opponent, not first 
            var opponentXP = Opponents[0].maxHP * experienceMultiplier;
            IncreaseExperience(opponentXP);
        }

        public static int CalcOpponentWeaponDamage ( int weaponNo, float attackFactor, int attacker )
        {
            // CALCULATE MONSTER WEAPON / ATTACK DAMAGE

            // attacker - 1 = monster
            var weaponDamageValues = new int[11];
            weaponDamageValues[0] = monsterWeapons[weaponNo].blunt;
            weaponDamageValues[1] = monsterWeapons[weaponNo].sharp;
            weaponDamageValues[2] = monsterWeapons[weaponNo].earth;
            weaponDamageValues[3] = monsterWeapons[weaponNo].air;
            weaponDamageValues[4] = monsterWeapons[weaponNo].fire;
            weaponDamageValues[5] = monsterWeapons[weaponNo].water;
            weaponDamageValues[6] = monsterWeapons[weaponNo].power;
            weaponDamageValues[7] = monsterWeapons[weaponNo].magic;
            weaponDamageValues[8] = monsterWeapons[weaponNo].good;
            weaponDamageValues[9] = monsterWeapons[weaponNo].evil;
            weaponDamageValues[10] = monsterWeapons[weaponNo].cold;

            if (opponentType == Encounters.Doppleganger)
            {
                weaponDamageValues[0] = itemBuffer[weaponNo].blunt;
                weaponDamageValues[1] = itemBuffer[weaponNo].sharp;
                weaponDamageValues[2] = itemBuffer[weaponNo].earth;
                weaponDamageValues[3] = itemBuffer[weaponNo].air;
                weaponDamageValues[4] = itemBuffer[weaponNo].fire;
                weaponDamageValues[5] = itemBuffer[weaponNo].water;
                weaponDamageValues[6] = itemBuffer[weaponNo].power;
                weaponDamageValues[7] = itemBuffer[weaponNo].magic;
                weaponDamageValues[8] = itemBuffer[weaponNo].good;
                weaponDamageValues[9] = itemBuffer[weaponNo].evil;
                weaponDamageValues[10] = itemBuffer[weaponNo].cold;
            }

            var armorValues = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            // Need to add modifier for player armor values & armor body parts

            var armors = new int[11]; // holds results of rolling for armor protection
            if (attacker == 0) // Player attacking
            {
                armorValues[0] = Opponents[0].aBlunt;
                armorValues[1] = Opponents[0].aSharp;
                armorValues[2] = Opponents[0].aEarth;
                armorValues[3] = Opponents[0].aAir;
                armorValues[4] = Opponents[0].aFire;
                armorValues[5] = Opponents[0].aWater;
                armorValues[6] = Opponents[0].aPower;
                armorValues[7] = Opponents[0].aMagic;
                armorValues[8] = Opponents[0].aGood;
                armorValues[9] = Opponents[0].aEvil;
                armorValues[10] = Opponents[0].aCold;

                var armorIndex = 0;
                while (armorIndex < 11)
                {
                    int noDice = (armorValues[armorIndex] & 0xf0) >> 4;
                    int noSides = (armorValues[armorIndex] & 0x0f);
                    armors[armorIndex] = RollDice(noDice, noSides);
                    armorIndex++;
                }
            }

            var damages = new[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }; // holds results of rolling for damage

            var damageIndex = 0; // 0 is blunt, 1 is sharp, 11 is cold - 11 damage types in total

            while (damageIndex < 11)
            {
                var noDice = (weaponDamageValues[damageIndex] & 0xf0) >> 4;
                var noSides = (weaponDamageValues[damageIndex] & 0x0f);

                if (noDice > 0)
                    damages[damageIndex] = RollDice(noDice, noSides);
                damageIndex++;
            }

            // Compare weapon damages against ancounter armour values inc. vulnerabilities and invulnerabilities
            // 0xff = invulnerable.
            // 0xf0 = absorbs power from this damage type.
            // 0x0f = takes double damage from this damage type.            

            var totalDamage = damages[0] +
                damages[1] +
                damages[2] +
                damages[3] +
                damages[4] +
                damages[5] +
                damages[6] +
                damages[7] +
                damages[8] +
                damages[9] +
                damages[10];
            return totalDamage;
        }

        public static int CalcPlayerWeaponDamage ( int weaponNo, float attackFactor, int attacker )
        {
            // CALCULATE PLAYER WEAPON / ATTACK DAMAGE

            // attacker - 0 = player
            var weaponDamageValues = new int[11];
            weaponDamageValues[0] = itemBuffer[weaponNo].blunt;
            weaponDamageValues[1] = itemBuffer[weaponNo].sharp;
            weaponDamageValues[2] = itemBuffer[weaponNo].earth;
            weaponDamageValues[3] = itemBuffer[weaponNo].air;
            weaponDamageValues[4] = itemBuffer[weaponNo].fire;
            weaponDamageValues[5] = itemBuffer[weaponNo].water;
            weaponDamageValues[6] = itemBuffer[weaponNo].power;
            weaponDamageValues[7] = itemBuffer[weaponNo].magic;
            weaponDamageValues[8] = itemBuffer[weaponNo].good;
            weaponDamageValues[9] = itemBuffer[weaponNo].evil;
            weaponDamageValues[10] = itemBuffer[weaponNo].cold;

            var armorValues = new[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            var armors = new[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            var damages = new[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }; // holds results of rolling for damage

            var damageIndex = 0; // 0 is blunt, 1 is sharp, 11 is cold - 11 damage types in total

            while (damageIndex < 11)
            {
                var noDice = (weaponDamageValues[damageIndex] & 0xf0) >> 4;
                var noSides = (weaponDamageValues[damageIndex] & 0x0f);

                if (noDice > 0)
                {
                    damages[damageIndex] = RollDice(noDice, noSides);
                    if (armorValues[damageIndex] == 0xff)
                        damages[damageIndex] = 0;
                    if (armorValues[damageIndex] == 0xf0)
                        damages[damageIndex] = damages[damageIndex] * -1;
                    if (armorValues[damageIndex] == 0xff)
                        damages[damageIndex] = damages[damageIndex] * 2;
                }
                damageIndex++;
            }

            // Compare weapon damages against encounter armour values inc. vulnerabilities and invulnerabilities
            // 0xff = invulnerable.
            // 0xf0 = absorbs power from this damage type.
            // 0x0f = takes double damage from this damage type.
            if (attacker == 0) // Player attacking
            {
                for (var i = 0; i < 11; ++i) // number of damage slots to compare against armour slots
                {
                    damages[i] -= armors[i];
                    if (damages[i] < 0)
                        damages[i] = 0;
                }
            }

            var totalDamage = damages[0] +
                damages[1] +
                damages[2] +
                damages[3] +
                damages[4] +
                damages[5] +
                damages[6] +
                damages[7] +
                damages[8] +
                damages[9] +
                damages[10];
            return totalDamage;
        }

        public static void CheckEncounter ()
        {
            if (EncounterThemeNotPlaying())
            {
                var encounter_check = Randn(0, plyr.stealth);
                if (encounter_check == 0)
                    ChooseEncounter();
            }
        }

        public static void CheckFixedEncounters ()
        {
            // Check for guards outside bank vaults
            if (plyr.stolenFromVault == 2)
                // Check for Palace Elite as well
                EncounterLoop(Encounters.Guard, 3);

            switch (plyr.special)
            {
                case 1001: // troll tyrant & gang of 8 trolls
                if ((plyr.trollsDefeated == false) && (plyr.trollsCombat))
                {
                    plyr.trollsCombat = false;
                    plyr.status = GameStates.Encounter;
                    EncounterLoop(Encounters.TrollTyrant, 1);
                    if (plyr.trollsDefeated)
                    {
                        plyr.status = GameStates.Encounter;
                        EncounterLoop(Encounters.Troll, 8);
                    }
                }
                if ((plyr.trollsDefeated) && (plyr.trollsCombat))
                {
                    plyr.trollsCombat = false;
                    plyr.status = GameStates.Encounter;
                    EncounterLoop(Encounters.Troll, 8);
                }
                break;

                case 1002: // goblin lord & gang of 8 goblins
                if ((plyr.goblinsDefeated == false) && (plyr.goblinsCombat))
                {
                    plyr.goblinsCombat = false;
                    plyr.status = GameStates.Encounter;
                    EncounterLoop(Encounters.GoblinLord, 1);
                    if (plyr.goblinsDefeated)
                    {
                        plyr.status = GameStates.Encounter;
                        EncounterLoop(Encounters.Goblin, 8);
                    }
                }
                if ((plyr.goblinsDefeated) && (plyr.goblinsCombat))
                {
                    plyr.goblinsCombat = false;
                    plyr.status = GameStates.Encounter;
                    EncounterLoop(Encounters.Goblin, 8);
                }
                break;
            }

            if ((plyr.special > 0x79) && (plyr.special < 0xA0)) // Dungeon only just now
            {
                plyr.fixedEncounterRef = plyr.special - 128;
                if (plyr.fixedEncounters[plyr.fixedEncounterRef] == false)
                {
                    plyr.fixedEncounter = true;
                    switch (plyr.special)
                    {
                        case 0x80:
                        EncounterLoop(Encounters.UndeadKnight, 1);
                        break;
                        case 0x81:
                        EncounterLoop(Encounters.UndeadKnight, 1);
                        break;
                        case 0x82:
                        EncounterLoop(Encounters.UndeadKnight, 1);
                        break;
                        case 0x83:
                        EncounterLoop(Encounters.UndeadKnight, 1);
                        break;
                        case 0x84:
                        EncounterLoop(Encounters.UndeadKnight, 1);
                        break;
                        case 0x85:
                        EncounterLoop(Encounters.UndeadKnight, 1);
                        break;
                        case 0x86:
                        EncounterLoop(Encounters.UndeadKnight, 1);
                        break;
                        case 0x87:
                        EncounterLoop(Encounters.Basilisk, 1);
                        break;
                        case 0x88:
                        EncounterLoop(Encounters.Doppleganger, 1);
                        break;
                        case 0x89:
                        EncounterLoop(Encounters.Lich, 1);
                        break;
                        case 0x8A:
                        EncounterLoop(Encounters.Valkyrie, 1);
                        break;
                        case 0x8B:
                        EncounterLoop(Encounters.GreatNaga, 1);
                        break;
                        case 0x8C:
                        EncounterLoop(Encounters.Wraith, 3);
                        break;
                        case 0x8D:
                        EncounterLoop(Encounters.FlameDemon, 4);
                        break;
                        case 0x8E:
                        EncounterLoop(Encounters.Dwarf, 7);
                        break;
                        case 0x8F:
                        EncounterLoop(Encounters.Vampire, 8);
                        break;
                        case 0x90:
                        EncounterLoop(Encounters.Whirlwind, 1);
                        break;
                        case 0x91:
                        EncounterLoop(Encounters.SmallDragon, 1);
                        break;
                        case 0x92:
                        EncounterLoop(Encounters.GiantWolf, 3);
                        break;
                        case 0x93:
                        EncounterLoop(Encounters.Doppleganger, 1);
                        break;

                        //TODO: What is encounters 37?
                        case 0x94:
                        EncounterLoop((Encounters)37, 8);
                        break;
                        case 0x95:
                        EncounterLoop(Encounters.Homunculus, 8);
                        break;
                        case 0x96:
                        EncounterLoop(Encounters.Skeleton, 8);
                        break;
                        case 0x97:
                        EncounterLoop(Encounters.Phoenix, 1);
                        break;
                        case 0x98:
                        EncounterLoop(Encounters.Ghost, 1);
                        break;
                        case 0x99:
                        EncounterLoop(Encounters.FlameDemon, 4);
                        break;
                        case 0x9A:
                        EncounterLoop(Encounters.Valkyrie, 1);
                        break;
                        case 0x9B:
                        EncounterLoop(Encounters.HornedDevil, 1);
                        break;
                        case 0x9C:
                        EncounterLoop(Encounters.Vampire, 1);
                        break;
                        case 0x9D:
                        EncounterLoop(Encounters.SmallDragon, 1);
                        break;
                        case 0x9E:
                        EncounterLoop(Encounters.Doppleganger, 8);
                        break;
                        case 0x9F:
                        EncounterLoop(Encounters.Devourer, 1);
                        break;
                    }
                }
            }
        }

        public static void CheckForActiveOpponents ()
        {
            // Check first encounter slot
            if (Opponents[0].hp == 0)
                encounterRunning = false;
        }

        public static void CheckHostility ()
        {
            encounterNotHostile = true;

            // Check alignment - Evil
            if (Opponents[0].alignment < 128)
                encounterNotHostile = false;

            // Good but hostile to humans
            if ((opponentType == Encounters.Phoenix) ||
                (opponentType == Encounters.Valkyrie) ||
                (opponentType == Encounters.Dwarf))
                encounterNotHostile = false;

            // Check for neutral encounters without intelligence or wisdom - e.g. giant rat
            if ((Opponents[0].inte == 0) && (Opponents[0].wis == 0))
                encounterNotHostile = false;

            // Check anti-guild status - only applies to humans
        }

        public static void CheckSurprise ()
        {
            // Determines whether the player or opponent have element of surprise
            // If player then go to encounterMenu 3

            var playerStealth = Randn(1, plyr.stealth);
            var opponentStealth = Randn(0, Opponents[0].stealth);

            var surpriseValue = -opponentStealth + playerStealth;

            if (surpriseValue < -5)
                playerSurprised = true;
            if (surpriseValue > 5)
                opponentSurprised = true;
        }

        public static void CheckTreasure ()
        {
            var foundTreasure = false;
            
            // Check to see if the opponent was carrying a weapon (as opposed to claws or teeth)
            // Only type 0x03 weapons can be dropped - type 0xFF refers to natural weapons such as bites, tails, claws, spells

            Opponents[0] = Monsters[(int)opponentType];

            var weapon = Opponents[0].w1; // Modify

            if (monsterWeapons[weapon].type == 0x03)
            {
                CreateWeapon(weapon); // Create a new instance of this weapon type on the floor
                foundTreasure = true; // need to ensure that only weapons get created!
            }

            var upperRange = 75;

            var no_found = Randn(0, Opponents[0].tFood);
            var found = Randn(0, upperRange); // Adjusted from upperRange to reduce volume item drops
            if ((no_found > 0) && (found <= plyr.treasureFinding))
            {
                CreateGenericItem(1, no_found);
                foundTreasure = true;
            }

            no_found = Randn(0, Opponents[0].tWater);
            found = Randn(0, upperRange);
            if ((no_found > 0) && (found <= plyr.treasureFinding))
            {
                CreateGenericItem(2, no_found);
                foundTreasure = true;
            }

            no_found = Randn(0, Opponents[0].tTorches);
            found = Randn(0, upperRange);
            if ((no_found > 0) && (found <= plyr.treasureFinding))
            {
                CreateGenericItem(3, no_found);
                foundTreasure = true;
            }

            no_found = Randn(0, Opponents[0].tTimepieces);
            found = Randn(0, upperRange);
            if ((no_found > 0) && (found <= plyr.treasureFinding))
            {
                CreateGenericItem(4, no_found);
                foundTreasure = true;
            }

            no_found = Randn(0, Opponents[0].tCompasses);
            found = Randn(0, upperRange);
            if ((no_found > 0) && (found <= plyr.treasureFinding))
            {
                CreateGenericItem(5, no_found);
                foundTreasure = true;
            }

            no_found = Randn(0, Opponents[0].tKeys);
            found = Randn(0, upperRange);
            if ((no_found > 0) && (found <= plyr.treasureFinding))
            {
                CreateGenericItem(6, no_found);
                foundTreasure = true;
            }

            no_found = Randn(0, Opponents[0].tCrystals);
            found = Randn(0, upperRange);
            if ((no_found > 0) && (found <= plyr.treasureFinding))
            {
                CreateGenericItem(7, no_found);
                foundTreasure = true;
            }

            no_found = Randn(0, Opponents[0].tGems);
            found = Randn(0, upperRange);
            if ((no_found > 0) && (found <= plyr.treasureFinding))
            {
                CreateGenericItem(8, no_found);
                foundTreasure = true;
            }
            no_found = Randn(0, Opponents[0].tJewels);
            found = Randn(0, upperRange);
            if ((no_found > 0) && (found <= plyr.treasureFinding))
            {
                CreateGenericItem(9, no_found);
                foundTreasure = true;
            }
            no_found = Randn(0, Opponents[0].tGold);
            found = Randn(0, upperRange);
            if ((no_found > 0) && (found <= plyr.treasureFinding))
            {
                CreateGenericItem(10, no_found);
                foundTreasure = true;
            }
            no_found = Randn(0, Opponents[0].tSilver);
            found = Randn(0, upperRange);
            if ((no_found > 0) && (found <= plyr.treasureFinding))
            {
                CreateGenericItem(11, no_found);
                foundTreasure = true;
            }
            no_found = Randn(0, Opponents[0].tCopper);
            found = Randn(0, upperRange);
            if ((no_found > 0) && (found <= plyr.treasureFinding))
            {
                CreateGenericItem(12, no_found);
                foundTreasure = true;
            }

            found = Randn(0, 20);
            if (found > 17)
            {
                // No checking for suitability of encounter to be carrying potions yet
                foundTreasure = true;
                var potionType = Randn(0, 43);
                var potionKnown = Randn(0, 100);
                var potionRef = CreatePotion(potionType);
                if (potionKnown > 90)
                    itemBuffer[potionRef].hp = 1; // Potion identified or labelled
            }

            if (opponentType == Encounters.UndeadKnight)
            {
                switch (plyr.special) // Stat bonus for defeating The Seven Undead Knights
                {
                    case 0x80:
                    plyr.sta++;
                    break;
                    case 0x81:
                    plyr.chr++;
                    break;
                    case 0x82:
                    plyr.str++;
                    break;
                    case 0x83:
                    plyr.inte++;
                    break;
                    case 0x84:
                    plyr.wis++;
                    break;
                    case 0x85:
                    plyr.skl++;
                    break;
                    case 0x86:
                    plyr.str++;
                    break;
                }
            }

            if (opponentType == Encounters.TrollTyrant) // troll tyrant killed
            {
                if (plyr.trollsReforged == false)
                {
                    plyr.trollsDefeated = true;
                    CreateQuestItem(0);
                } else
                {
                    plyr.trollsDefeated = true;
                    CreateQuestItem(2);
                }
                foundTreasure = false;
            }

            if (opponentType == Encounters.GoblinLord) // goblin lord killed
            {
                if (plyr.goblinsReforged == false)
                {
                    plyr.goblinsDefeated = true;
                    CreateQuestItem(1);
                } else
                {
                    plyr.goblinsDefeated = true;
                    CreateQuestItem(2);
                }
                foundTreasure = false;
            }

            if (foundTreasure)
                GetItems();
        }

        public static void ChooseEncounter ()
        {
            Encounters monsterNo = 0;
            plyr.status = GameStates.Encounter;

            // CITY - Day
            if ((plyr.timeOfDay != 1) && (plyr.scenario == Scenarios.City))
            {
                var encCount = 0;
                var monsterProb = Randn(0, 255);
                for (var i = 0; i < 29; ++i) // 28???
                {
                    if ((monsterProb >= encCount) && (monsterProb < dayEncTable[i].encProb + encCount))
                        monsterNo = dayEncTable[i].encType;
                    encCount += dayEncTable[i].encProb;
                }
            }

            // CITY - Night
            if ((plyr.timeOfDay == 1) && (plyr.scenario == Scenarios.City))
            {
                var encCount = 0;
                var monsterProb = Randn(0, 255);
                for (var i = 0; i < 48; ++i)
                {
                    if ((monsterProb >= encCount) && (monsterProb < nightEncTable[i].encProb + encCount))
                        monsterNo = nightEncTable[i].encType;
                    encCount += nightEncTable[i].encProb;
                }
            }

            if ((plyr.scenario == Scenarios.Dungeon) && ((plyr.zone == 17) || (plyr.zone == 16))) // Dungeon - Well Lit Area
            {
                var encCount = 0;
                var monsterProb = Randn(0, 255);
                for (var i = 0; i < 11; ++i)
                {
                    if ((monsterProb >= encCount) && (monsterProb < wellLitEncTable[i].encProb + encCount))
                        monsterNo = wellLitEncTable[i].encType;
                    encCount += wellLitEncTable[i].encProb;
                }
                if (monsterNo == 0)
                {
                    //TODO: Why are we erroring on this?
                    monsterNo = Encounters.Thief;
                    Console.Write("Error: Monster 0 rolled!\n");
                }
            }

            if ((plyr.scenario == Scenarios.Dungeon) && !((plyr.zone == 17) || (plyr.zone == 16)))
            {
                var encCount = 0;
                var monsterProb = Randn(0, 255);
                for (var i = 0; i < DUNGEON_TABLE_ENCOUNTERS; ++i)
                {
                    if ((monsterProb >= encCount) && (monsterProb < dungeonTable[i].encProb + encCount))
                        monsterNo = dungeonTable[i].encType;
                    encCount += dungeonTable[i].encProb;
                }
                if (monsterNo == 0)
                {
                    //TODO: Why are we erroring on this?
                    monsterNo = Encounters.Thief;
                    Console.Write("Error: Monster 0 rolled!\n");
                }
            }

            if ((plyr.scenario == Scenarios.Dungeon) && (plyr.map == 4))
                monsterNo = Encounters.Mage;
            
            plyr.fixedEncounter = false;
            EncounterLoop(monsterNo, 1); // Only one currently except for fixed encounters

            //if (checkForTreasure) checkTreasure();
            // FBI Agent and Basilisk images missing
        }

        public static void ClearConsoleMessages ()
        {
            // Sets all console message slots to empty
            for (var i = 0; i < MAX_CONSOLE_MESSAGES; ++i)
                consoleMessages[i] = "NO MESSAGE";
        }

        public static void ConsoleMessage ( string messageText )
        {
            var messageNotAddedToQueue = true;
            var messagesIndex = 0;
            while (messageNotAddedToQueue)
            {
                if (consoleMessages[messagesIndex] == "NO MESSAGE")
                {
                    consoleMessages[messagesIndex] = messageText;
                    waitingForSpaceKey = true;
                    messageNotAddedToQueue = false;
                }
                messagesIndex++;
                if (messagesIndex > MAX_CONSOLE_MESSAGES)
                {
                    // Will currently discard the message
                    Console.Write("ERROR: Console messages maximum exceeded!\n");
                    messageNotAddedToQueue = false; // To break out of loop after reporting error
                }
            }
        }

        public static void DetermineOpponentOpeningMessage ()
        {
            // Determines the following:
            // 1) Does the opponent have an opening message (e.g. thief, pauper, knight)

            switch (plyr.encounterRef)
            {
                case Encounters.Thief:
                encounterMenu = 4;
                break;
                case Encounters.Knight:
                encounterMenu = 6;
                break;
                case Encounters.Pauper:
                encounterMenu = 7;
                break;
                case Encounters.Guard:
                if (plyr.stolenFromVault == 2)
                {
                    plyr.stolenFromVault = 1;
                    encounterMenu = 12;
                }
                break;
            }
        }

        public static void DrawEncounterView ()
        {
            DrawAtariAnimation();
            UpdateEncounterStatusText();
            if (waitingForSpaceKey)
                CyText(3, consoleMessages[0]);

            if (graphicMode == (int)DisplayOptions.AlternateLarge)
                DrawConsoleBackground();
        }

        public static void EncounterLoop ( Encounters encounterType, int opponentQuantity )
        {
            opponentType = encounterType;
            checkForTreasure = false;
            animationNotStarted = true;
            firstFrame = Monsters[(int)opponentType].image;
            lastFrame = Monsters[(int)opponentType].image2;
            encounterRunning = true;
            encounterTurns = 0;
            playerTurn = true;
            playerStunned = false;
            playerOnGround = false;
            playerSurprised = false;
            opponentSurprised = false;
            waitingForAnyKey = false;
            waitingForSpaceKey = false;
            encounterMenu = 1;
            groundTurnsRemaining = 0;
            plyr.status_text = "                                        ";
            playerRunsAway = false;

            // Move to display!
            //MLT: Double to float
            if (graphicMode == (int)DisplayOptions.AlternateLarge)
                plyr.z_offset = 0.3F;
            else
                plyr.z_offset = 1.5F;

            ClearConsoleMessages();
            InitialiseOpponents(opponentType, opponentQuantity);

            DetermineOpponentOpeningMessage();
            CheckHostility(); // Check if opponent is hostile
            //checkSurprise();  // Check if player or opponent surprised

            SelectEncounterTheme();

            while ((encounterRunning) || (waitingForSpaceKey))
            {
                DrawEncounterView();
                if (!waitingForSpaceKey)
                {
                    if (playerTurn)
                        ProcessPlayerAction();
                    else
                        ProcessOpponentAction();
                }

                UpdateDisplay();
                var key = GetSingleKey();

                // Check if player died this turn
                if (plyr.hp < 0)
                    encounterRunning = false;

                // Handle dismissed encounter messages
                if (key == "SPACE")
                {
                    UpdateConsoleMessages(); // Checks for further messages to be printed.
                    if (consoleMessages[0] == "NO MESSAGE")
                        waitingForSpaceKey = false; // player pressed space to acknowledge last message read
                }

                // ...and see if any opponents still active
                UpdateOpponents();
                CheckForActiveOpponents();
            }

            // If opponent killed, charmed or tricked then player might find treasure afterwards

            if (plyr.fixedEncounter)
            {
                plyr.fixedEncounter = false;
                if (playerRunsAway)
                {
                    switch (plyr.facing)
                    {
                        case Directions.West:
                        case Directions.East: plyr.x = plyr.oldx; break;

                        case Directions.North:
                        case Directions.South: plyr.y = plyr.oldy; break;
                    }
                    plyr.z_offset = 1.0F;
                } else
                {
                    // Fixed encounter killed
                    plyr.fixedEncounters[plyr.fixedEncounterRef] = true;
                }
            }

            plyr.status = GameStates.Explore;

            if ((checkForTreasure) && (plyr.hp >= 0))
                CheckTreasure();
        }

        public static string GetAttackDesc ( int chosenWeapon, int damage )
        {
            /*
			    0x00 - hack/slash
			    0x01 - poke/stab
			    0x02 - bash/wallop
			    0x03 - spear/impale
			    0x04 - whip/lash
			    0x05 - blast
			    0x06 - punch/whomp
			    0x07 - hit
			*/

            /*
			    claws
			    kicks
			    bites
			    tears
			    chokes
			    lashes
			    burns
			    stings
			    rips
			    stomps
			    gnaws
			    rends
			    asphyxiates
			    wallops
			    fries
			    stings
			*/

            var result = "hits";
            var value = (monsterWeapons[chosenWeapon].flags) & 7;

            if (monsterWeapons[chosenWeapon].type == 0xFF)
            {
                if (value == 0)
                    result = "claws at";
                if (value == 1)
                    result = "kicks at";
                if (value == 2)
                    result = "bites";
                if (value == 3)
                    result = "tears at";
                if (value == 4)
                    result = "chokes";
                if (value == 5)
                    result = "lashes at";
                if (value == 6)
                    result = "burns";
                if (value == 7)
                    result = "stings";
                if (value > 7)
                    result = "greater than 7";
            }

            if (monsterWeapons[chosenWeapon].type == 0x03)
            {
                if (damage > 3)
                {
                    if (value == 0)
                        result = "slashes at";
                    if (value == 1)
                        result = "stabs at";
                    if (value == 2)
                        result = "wallops";
                    if (value == 3)
                        result = "impales";
                    if (value == 4)
                        result = "lashes at";
                    if (value == 5)
                        result = "blasts";
                    if (value == 6)
                        result = "whomps";
                    if (value == 7)
                        result = "hits";
                    if (value > 7)
                        result = "greater than 7";
                } else
                {
                    if (value == 0)
                        result = "hacks at";
                    if (value == 1)
                        result = "pokes at";
                    if (value == 2)
                        result = "bashes";
                    if (value == 3)
                        result = "spears";
                    if (value == 4)
                        result = "whips at";
                    if (value == 5)
                        result = "blasts";
                    if (value == 6)
                        result = "punches";
                    if (value == 7)
                        result = "hits";
                    if (value > 7)
                        result = "greater than 7";
                }
            }

            return result;
        }

        public static string GetPlayerAttackDesc ( int damage )
        {
            /*
			    0x00 - hack/slash
			    0x01 - poke/stab
			    0x02 - bash/wallop
			    0x03 - spear/impale
			    0x04 - whip/lash
			    0x05 - blast
			    0x06 - punch/whomp
			    0x07 - hit
			*/

            var result = "hit";
            var value = (itemBuffer[plyr.priWeapon].flags) & 7;

            if (damage < 4)
            {
                if (value == 0)
                    result = "hack";
                if (value == 1)
                    result = "poke";
                if (value == 2)
                    result = "bash";
                if (value == 3)
                    result = "spear";
                if (value == 4)
                    result = "whip";
                if (value == 5)
                    result = "blast";
                if (value == 6)
                    result = "punch";
                if (value == 7)
                    result = "hit";
                if (value > 7)
                    result = "greater than 7";
            } else
            {
                if (value == 0)
                    result = "slash";
                if (value == 1)
                    result = "stab";
                if (value == 2)
                    result = "wallop";
                if (value == 3)
                    result = "impale";
                if (value == 4)
                    result = "lash";
                if (value == 5)
                    result = "blast";
                if (value == 6)
                    result = "whomp";
                if (value == 7)
                    result = "hit";
                if (value > 7)
                    result = "greater than 7";
            }

            return result;
        }

        public static void HealerCureDiseases ()
        {
            // Clear all the disease flags
            if (!CheckCoins(0, 100, 0))
            {
                // Insufficient funds
                plyr.gold = 0;
                plyr.silver = 0;
                plyr.copper = 0;
                ConsoleMessage("The Healer grumbles:@@Thy purse is too small, however...");
            } else
            {
                DeductCoins(0, 100, 0);
            }
            ConsoleMessage("The Healer lays his@hands upon you.");
            plyr.diseases[0] = 0;
            OpponentLeaves();
        }

        public static void HealerCurePoisons ()
        {
            // Clear all the poison flags
            if (!CheckCoins(0, 50, 0))
            {
                // Insufficient funds
                plyr.gold = 0;
                plyr.silver = 0;
                plyr.copper = 0;
                ConsoleMessage("The Healer grumbles:@@Thy purse is too small, however...");
            } else
            {
                DeductCoins(0, 50, 0);
            }
            ConsoleMessage("The Healer lays his@hands upon you.");
            plyr.poison[0] = 0;
            plyr.poison[1] = 0;
            plyr.poison[2] = 0;
            plyr.poison[3] = 0;
            OpponentLeaves();
        }

        public static void HealerHealWounds ()
        {
            var hpToHeal = InputNumber();

            if (hpToHeal > 0)
            {
                if (!CheckCoins(0, hpToHeal, 0))
                {
                    // Insufficient funds
                    plyr.gold = 0;
                    plyr.silver = 0;
                    plyr.copper = 0;
                    ConsoleMessage("The Healer grumbles:@@Thy purse is too small, however...");
                } else
                {
                    DeductCoins(0, hpToHeal, 0);
                }
                ConsoleMessage("The Healer lays his@hands upon you.");
                plyr.hp = plyr.hp + hpToHeal;
                if (plyr.hp > plyr.maxhp)
                    plyr.hp = plyr.maxhp;
                OpponentLeaves();
            }
        }

        public static void InitialiseOpponents ( Encounters opponentType, int opponentQuantity )
        {
            // Clean out all 8 opponent slots with an empty monster object (using the unused FBI Agent for this)
            for (var i = 0; i < MAX_OPPONENTS; ++i)
                Opponents[i] = Monsters[(int)Encounters.FbiAgent];
            for (var i = 0; i < opponentQuantity; ++i)
            {
                Opponents[i] = Monsters[(int)opponentType];
                if (opponentType == Encounters.Doppleganger)
                {
                    // Doppleganger
                    Opponents[i].hp = plyr.hp;
                    Opponents[i].maxHP = plyr.hp;
                    Opponents[i].str = plyr.str;
                    Opponents[i].skl = plyr.skl;
                    Opponents[i].inte = plyr.inte;
                    Opponents[i].spd = plyr.speed;
                    Opponents[i].w1 = plyr.priWeapon;
                    Opponents[i].c1 = 100;
                }
            }

            plyr.encounterAnimationRef = Opponents[0].image;
            plyr.encounterRef = opponentType;
            encounterQuantity = opponentQuantity;
        }

        public static void InitMonster ( int monsterNo )
        {
            // Initialise a SINGLE monster - legacy City logic

            /*
			Opponents[a] = Monsters[monsterNo]; // copy monster details to current Opponents[a] object
			Opponents[a].hp += randn(0,Monsters[monsterNo].randomHP);
			Opponents[a].str += randn(0,Monsters[monsterNo].randomStrength);
			Opponents[a].skl += randn(0,Monsters[monsterNo].randomSkill);
			Opponents[a].inte += randn(0,Monsters[monsterNo].randomIntelligence);
			Opponents[a].spd += randn(0,Monsters[monsterNo].randomSpeed);
			Opponents[a].maxHP = Opponents[a].hp;
		
			if (monsterNo==DOPPLEGANGER)
			{
			    // Doppelganger
			    Opponents[a].hp = plyr.hp;
			    Opponents[a].maxHP = plyr.hp;
			    Opponents[a].str = plyr.str;
			    Opponents[a].skl = plyr.skl;
			    Opponents[a].inte = plyr.inte;
			    Opponents[a].spd = plyr.speed;
			    Opponents[a].w1 = itemBuffer[plyr.priWeapon].index;
			    if (plyr.priWeapon==255) Opponents[a].w1=0; // bare hands
			    Opponents[a].c1 = 100;
			}
			*/
        }

        public static int InputNumber ()
        {
            UpdateDisplay(); // Messy visual fix before the display loop below - probably break animation later

            var inputText = "";
            var maxNumberSize = 6;
            var enterKeyNotPressed = true;
            while (enterKeyNotPressed)
            {
                DrawEncounterView();
                CyText(3, "Cure how many hits at@@ 1 silver each?");
                var str = $">{inputText}_";
                BText(10, 7, str);
                UpdateDisplay();
                var key = GetSingleKey();
                if ((key == "0") ||
                    (key == "1") ||
                    (key == "2") ||
                    (key == "3") ||
                    (key == "4") ||
                    (key == "5") ||
                    (key == "6") ||
                    (key == "7") ||
                    (key == "8") ||
                    (key == "9"))
                {
                    var numberLength = inputText.Length;
                    if (numberLength < maxNumberSize)
                        inputText = inputText + key;
                }
                if (key == "BACKSPACE")
                {
                    var numberLength = inputText.Length;
                    if (numberLength != 0)
                    {
                        inputText = inputText.Substring(0, (numberLength - 1));
                    }
                }
                if (key == "RETURN")
                    enterKeyNotPressed = false;
            }
            var value = Convert.ToInt32(inputText);
            return value;
        }

        public static void OpponentAttack ()
        {            
            var bPart = Randn(0, 5);
            var bPartText = "you";
            var weaponName = "ERROR";
            var attackDesc = "ERROR";
            if (bPart == 3)
                bPartText = "your head";
            if (bPart == 4)
                bPartText = "your arm";
            if (bPart == 5)
                bPartText = "your legs";

            opponentNoAttacking = curOpponent;

            if (opponentNoAttacking == 0)
                prefix = "The ";
            if (opponentNoAttacking == 1)
                prefix = "2nd ";
            if (opponentNoAttacking == 2)
                prefix = "3rd ";
            if (opponentNoAttacking == 3)
                prefix = "4th ";
            if (opponentNoAttacking == 4)
                prefix = "5th ";
            if (opponentNoAttacking == 5)
                prefix = "6th ";
            if (opponentNoAttacking == 6)
                prefix = "7th ";
            if (opponentNoAttacking == 7)
                prefix = "8th ";

            var chosenWeapon = Opponents[0].w1;

            // Hit probability
            var skillDifference = Opponents[0].skl - plyr.skl;
            int hitProbability = 0;

            if (skillDifference <= -128)
                hitProbability = 1;
            if ((skillDifference >= -128) && (skillDifference < -64))
                hitProbability = 19;
            if ((skillDifference >= -64) && (skillDifference < 0))
                hitProbability = 50;
            if ((skillDifference >= 0) && (skillDifference < 64))
                hitProbability = 63;
            if ((skillDifference >= 64) && (skillDifference < 128))
                hitProbability = 69;
            if ((skillDifference >= 128) && (skillDifference < 192))
                hitProbability = 76;
            if (skillDifference >= 192)
                hitProbability = 100;

            var str = "";
            var hitRoll = Randn(1, 100);
            var hitSuccess = hitRoll <= hitProbability;

            if (!hitSuccess)
                str = $"{prefix}{Opponents[0].name} misses.";
            // Add additional player dodges type messages here

            if (hitSuccess)
            {
                if (opponentType == Encounters.GiantRat)
                    plyr.diseases[0] = 1;
                var attackFactor = 1.0F; // change!
                var damage = CalcOpponentWeaponDamage(chosenWeapon, attackFactor, 1);

                if (opponentType == Encounters.Doppleganger)
                {
                    weaponName = itemBuffer[(Opponents[0].w1)].name;
                    attackDesc = "hits";
                } else
                {
                    weaponName = monsterWeapons[(Opponents[0].w1)].name;
                    var weaponIndex = Opponents[0].w1;
                    attackDesc = GetAttackDesc(weaponIndex, damage);
                }

                if (damage != 1000)
                    plyr.hp -= damage;

                str = $"{prefix}{Opponents[0].name} {attackDesc}@{bPartText} with {weaponName}@for {damage}.";
                if (damage == 0)
                    str = $"{prefix}{Opponents[0].name} {attackDesc}@{bPartText} with {weaponName}@which has no effect!";
            }

            ConsoleMessage(str);

            // Check if player is knocked down and PLAYER STILL ALIVE
            if ((hitSuccess) && (!(playerOnGround)) && (plyr.hp >= 0))
            {
                var grounded = Randn(0, 12);
                if (grounded < 1)
                {
                    playerOnGround = true;
                    groundTurnsRemaining = 2;
                    ConsoleMessage("You have been knocked down.");
                }
            }

            // Check if player can rise from ground
            if ((playerOnGround) && (groundTurnsRemaining == 0) && (plyr.hp >= 0))
            {
                ConsoleMessage("You rise from the ground.");
                playerOnGround = false;
            } else
            {
                groundTurnsRemaining--;
            }
        }

        public static int OpponentChooseWeapon ()
        {
            weaponProbabilities[0] = Opponents[0].c1;
            weaponProbabilities[1] = Opponents[0].c2;
            weaponProbabilities[2] = Opponents[0].c3;
            weaponProbabilities[3] = Opponents[0].c4;
            weaponProbabilities[4] = Opponents[0].c5;
            weaponProbabilities[5] = Opponents[0].c6;

            weaponReferences[0] = Opponents[0].w1;
            weaponReferences[1] = Opponents[0].w2;
            weaponReferences[2] = Opponents[0].w3;
            weaponReferences[3] = Opponents[0].w4;
            weaponReferences[4] = Opponents[0].w5;
            weaponReferences[5] = Opponents[0].w6;

            var chosenWeaponNo = 255;
            var weaponProbability = Randn(1, 100);
            var weaponProbabilityTotal = 0;
            var weaponIndex = 0;
            while (chosenWeaponNo == 255)
            {
                weaponProbabilityTotal += weaponProbabilities[weaponIndex];
                if (weaponProbability <= weaponProbabilityTotal)
                    chosenWeaponNo = weaponReferences[weaponIndex];
                if (weaponIndex == 6)
                    chosenWeaponNo = 0;
                weaponIndex++;
            }
            return chosenWeaponNo;
        }

        public static void OpponentDeath ()
        {
            var str = $"The {Opponents[0].name} {Opponents[0].armorText}.";
            ConsoleMessage(str);
            AwardExperience(opponentType);
            RemoveOpponent();
            checkForTreasure = true;
        }

        public static void OpponentLeaves ()
        {
            var str = $"The {Opponents[0].name} leaves.";
            ConsoleMessage(str);
            encounterRunning = false;
        }

        public static void PauseEncounter () => ConsoleMessage("(Paused)@@@@@(Press SPACE to continue)");

        public static void PlayerAttack ( int attackType, float attackFactorBonus )
        {
            var str = "";
            var missileWeapon = false;
            var missileAmmoAvailable = false;
            encounterNotHostile = false; // Opponent now hostile as they have been attacked
            var attackFactor = 1.00F;
            var weapon = plyr.priWeapon;
            var hitAttempt = true;

            var weaponDesc = itemBuffer[plyr.priWeapon].name;

            if (attackType == 3)
            {
                if (Randn(1, 5) > 3)
                {
                    str = "You wait for an opening.";
                    hitAttempt = false;
                }
            }

            // Check for missile weapon (e.g Crossbow)
            if ((itemBuffer[plyr.priWeapon].melee != 0xFF) && (hitAttempt))
            {
                missileWeapon = true;
                if (itemBuffer[plyr.priWeapon].ammo > 0)
                {
                    missileAmmoAvailable = true;
                    itemBuffer[plyr.priWeapon].ammo--;
                    var remainingAmmo = itemBuffer[plyr.priWeapon].ammo;
                    if (remainingAmmo < 10)
                        itemBuffer[plyr.priWeapon].name = $"Crossbow [0{remainingAmmo}]";
                    else
                        itemBuffer[plyr.priWeapon].name = $"Crossbow [{remainingAmmo}]";
                }
                weaponDesc = itemBuffer[plyr.priWeapon].name;
            }

            if ((itemBuffer[weapon].useStrength == 1))
            {
                //Calculate attack factor bonus if weapon uses Strength
                int strengthDifference = plyr.str - Opponents[0].str;
                if (strengthDifference <= -128)
                    attackFactorBonus += 0.0625F;
                if ((strengthDifference >= -128) && (strengthDifference < -64))
                    attackFactorBonus += 0.125F;
                if ((strengthDifference >= -64) && (strengthDifference < 0))
                    attackFactorBonus += 0.5F;
                if ((strengthDifference >= 0) && (strengthDifference < 64))
                    attackFactorBonus += 0.75F;
                if ((strengthDifference >= 64) && (strengthDifference < 128))
                    attackFactorBonus += 0.875F;
                if ((strengthDifference >= 128) && (strengthDifference < 192))
                    attackFactorBonus += 1;
                if (strengthDifference >= 192)
                    attackFactorBonus += 1;
            }
            attackFactor += attackFactorBonus;

            // Hit probability
            var skillDifference = plyr.skl - Opponents[0].skl;
            var hitProbability = 0;
            if (skillDifference <= -128)
                hitProbability = 1;
            if ((skillDifference >= -128) && (skillDifference < -64))
                hitProbability = 19;
            if ((skillDifference >= -64) && (skillDifference < 0))
                hitProbability = 50;
            if ((skillDifference >= 0) && (skillDifference < 64))
                hitProbability = 63;
            if ((skillDifference >= 64) && (skillDifference < 128))
                hitProbability = 69;
            if ((skillDifference >= 128) && (skillDifference < 192))
                hitProbability = 76;
            if (skillDifference >= 192)
                hitProbability = 100;
            if (attackType == 3)
                hitProbability -= 5;
            
            var hitRoll = Randn(1, 100);
            var hitSuccess = hitRoll <= hitProbability;

            if ((!hitSuccess) && (hitAttempt))
                str = "You miss.";

            if ((missileWeapon) && (!missileAmmoAvailable))
            {
                str = itemBuffer[plyr.priWeapon].name + " not loaded.";
                hitSuccess = false;
            }

            if ((hitSuccess) && (hitAttempt))
            {
                plyr.strPartials++; // Each successful hit contributes towards an extra strength point
                if (plyr.strPartials == 255)
                {
                    plyr.str++;
                    plyr.strPartials = 0;
                }
                int damage = CalcPlayerWeaponDamage(weapon, attackFactor, 0);

                var attackDesc = GetPlayerAttackDesc(damage);

                if (damage != 1000)
                {
                    Opponents[0].hp -= damage;
                    IncreaseExperience(damage / 2);
                }
                //damage [type] = Random [0; Round (base weapon damage [type] * attack factor)]

                str = $"You {attackDesc} the {Opponents[0].name}@with your {weaponDesc}@for {damage}.";
                if (damage == 0)
                    str = $"You {attackDesc} the {Opponents[0].name}@with your {weaponDesc}@which has no effect!";
                if ((plyr.scenario == Scenarios.City) && (damage == 1000))
                    str = $"You attack the {Opponents[0].name}@with your {weaponDesc}@which is stopped by its {Opponents[0].armorText}.";
                if ((plyr.scenario == Scenarios.Dungeon) && (damage == 1000))
                    str = $"You attack the {Opponents[0].name}@with your {weaponDesc}@which is stopped by its armour.";
            }

            ConsoleMessage(str);

            if (Opponents[0].hp < 1)
                OpponentDeath();

            playerTurn = false;
        }

        public static void PlayerCharm ()
        {
            UpdateDisplay(); // sloppy!

            //C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
            opponent.CopyFrom(Opponents[0]);
            encounterNotHostile = false;
            var charmSuccess = false;
            if ((opponent.inte > 5) && (plyr.chr != 0))
            {
                // Check for a successful Charm attempt
                var chrIntDifference = plyr.chr - opponent.inte;
                var charmProbability = 0;
                if (chrIntDifference <= -128)
                    charmProbability = 1;
                if ((chrIntDifference >= -128) && (chrIntDifference < -64))
                    charmProbability = 2;
                if ((chrIntDifference >= -64) && (chrIntDifference < 0))
                    charmProbability = 7;
                if ((chrIntDifference >= 0) && (chrIntDifference < 64))
                    charmProbability = 25;
                if ((chrIntDifference >= 64) && (chrIntDifference < 128))
                    charmProbability = 50;
                if ((chrIntDifference >= 128) && (chrIntDifference < 192))
                    charmProbability = 70;
                if (chrIntDifference >= 192)
                    charmProbability = 80;

                var hitRoll = Randn(1, 100);
                if (hitRoll <= charmProbability)
                    charmSuccess = true;
            }

            var str = charmSuccess ? "You charmed it!" : "You failed to charm it!";
            ConsoleMessage(str);

            if (charmSuccess)
            {
                checkForTreasure = true;
                Opponents[curOpponent].hp = 0;
                AwardExperience(opponentType);
                plyr.chrPartials++;
                if (plyr.chrPartials == 255)
                {
                    plyr.chr++;
                    plyr.chrPartials = 0;
                }
                encounterRunning = false;
            }

            // should be managed in processOpponent() turn            
            if (!charmSuccess)
            {
                encounterNotHostile = false;
                encounterMenu = 1;
                playerTurn = false;
            }
        }

        public static void PlayerHail ()
        {
            var str = "";

            var response = Randn(1, 15);
            if (response == 1)
                str = "\"Run! The Devourer comes!\"";
            if (response == 2)
                str = $"The {Opponents[0].name} mumbles@@something unintelligible.";
            if (response == 3)
                str = "\"Beware of false alarms.\"";
            if (response == 4)
                str = "\"No good deed ever goes unrewarded.\"";
            if (response == 5)
                str = "\"Don't tell the peasants how good@@the pears are with the cheese!\"";
            if (response == 6)
                str = "\"Prepare, the Apocalypse is soon.\"";
            if (response == 7)
                str = "\"Greetings adventurer!\"";
            if (response == 8)
                str = "\"Beware of Arena slavers!\"";
            if (response == 9)
                str = "\"They say that the ghost of the dead@@king lurks in the Palace gardens.\"";
            if (response == 10)
                str = "\"Seek out the guilds for learning.\"";
            if (response == 11)
                str = "\"Few have survived the Trial of Kings\"";
            if (response == 12)
                str = "\"The dead of the Arena haunt the@@ruined Five Blades tavern.\"";
            if (response == 13)
                str = "\"The House of Shadows is the most@@mysterious of all the noble houses.\"";
            if (response == 14)
                str = "\"Only those who have fought in@@the Great Arena can drink@@in the Block and Parry.\"";
            if (response == 15)
                str = "\"A flaming torch can make@@an effective weapon.\"";

            if ((plyr.encounterRef == Encounters.Nobleman))
            {
                str = "The nobleman tosses you a coin@and says:@@\"Away knave! Get thyself a bath!\"";
                if (plyr.gender == 2)
                    str = "The nobleman tosses you a coin@and says@\"Away scullion! Get thyself a bath!\"";
                plyr.gold++;
            }

            if ((plyr.encounterRef == Encounters.Acolyte))
            {
                var items = new [] { "food packets", "water flasks", "torches", "timepieces", "compasses", "keys", "crystals" };
                var opponentText = Opponents[0].name;
                var itemText = "";
                var genderText = "brother";
                var itemRequired = true;

                if (plyr.gender == 2)
                    genderText = "sister";

                // Check for friendship

                for (var i = 0; i < 7; i++)
                {
                    if ((i == 0) && (plyr.food == 0))
                    {
                        itemRequired = false;
                        itemText = items[i];
                        plyr.food++;
                    }
                    if ((i == 1) && (plyr.water == 0) && (itemRequired))
                    {
                        itemRequired = false;
                        itemText = items[i];
                        plyr.water++;
                    }
                    if ((i == 2) && (plyr.torches == 0) && (itemRequired))
                    {
                        itemRequired = false;
                        itemText = items[i];
                        plyr.torches++;
                    }
                    if ((i == 3) && (plyr.timepieces == 0) && (itemRequired))
                    {
                        itemRequired = false;
                        itemText = items[i];
                        plyr.timepieces++;
                    }
                    if ((i == 4) && (plyr.compasses == 0) && (itemRequired))
                    {
                        itemRequired = false;
                        itemText = items[i];
                        plyr.compasses++;
                    }
                    if ((i == 5) && (plyr.keys == 0) && (itemRequired))
                    {
                        itemRequired = false;
                        itemText = items[i];
                        plyr.keys++;
                    }
                    if ((i == 6) && (plyr.crystals == 0) && (itemRequired))
                    {
                        itemRequired = false;
                        itemText = items[i];
                        plyr.crystals++;
                    }
                }

                if (itemRequired)
                    str = "Greetings!";
                else
                    str = $"The {opponentText} sees you have no@{itemText} and tosses you one saying:@@\"For the cause {genderText}.\"";
            }
            ConsoleMessage(str);

            str = $"The {Opponents[0].name} leaves.";
            ConsoleMessage(str);
            encounterRunning = false;
        }

        public static void PlayerOffer ()
        {
            UpdateDisplay(); // temporary - won't work when rain is added or for an animated character
            var offermade = false;
            var itemQuantity = 0;
            var itemRef = SelectItem(SelectStates.Offer); // select an item in OFFER mode
            if ((itemRef > 999) && (itemRef != 9999))
                itemQuantity = InputItemQuantity(3);
            if ((itemRef > 999) && (itemRef != 9999))
            {
                if ((itemRef == 1000) && (plyr.food > 0))
                {
                    if (itemQuantity > plyr.food)
                        itemQuantity = plyr.food;
                    plyr.food -= itemQuantity;
                    offermade = true;
                }
                if ((itemRef == 1001) && (plyr.water > 0))
                {
                    if (itemQuantity > plyr.water)
                        itemQuantity = plyr.water;
                    plyr.water -= itemQuantity;
                    offermade = true;
                }
                if ((itemRef == 1002) && (plyr.torches > 0))
                {
                    if (itemQuantity > plyr.torches)
                        itemQuantity = plyr.torches;
                    plyr.torches -= itemQuantity;
                    offermade = true;
                }
                if ((itemRef == 1003) && (plyr.timepieces > 0))
                {
                    if (itemQuantity > plyr.timepieces)
                        itemQuantity = plyr.timepieces;
                    plyr.timepieces -= itemQuantity;
                    offermade = true;
                }
                if ((itemRef == 1004) && (plyr.compasses > 0))
                {
                    if (itemQuantity > plyr.compasses)
                        itemQuantity = plyr.compasses;
                    plyr.compasses -= itemQuantity;
                    offermade = true;
                }
                if ((itemRef == 1005) && (plyr.keys > 0))
                {
                    if (itemQuantity > plyr.keys)
                        itemQuantity = plyr.keys;
                    plyr.keys -= itemQuantity;
                    offermade = true;
                }
                if ((itemRef == 1006) && (plyr.crystals > 0))
                {
                    if (itemQuantity > plyr.crystals)
                        itemQuantity = plyr.crystals;
                    plyr.crystals -= itemQuantity;
                    offermade = true;
                }
                if ((itemRef == 1007) && (plyr.gems > 0))
                {
                    if (itemQuantity > plyr.gems)
                        itemQuantity = plyr.gems;
                    plyr.gems -= itemQuantity;
                    offermade = true;
                }
                if ((itemRef == 1008) && (plyr.jewels > 0))
                {
                    if (itemQuantity > plyr.jewels)
                        itemQuantity = plyr.jewels;
                    plyr.jewels -= itemQuantity;
                    offermade = true;
                }
                if ((itemRef == 1009) && (plyr.gold > 0))
                {
                    if (itemQuantity > plyr.gold)
                        itemQuantity = plyr.gold;
                    plyr.gold -= itemQuantity;
                    offermade = true;
                }
                if ((itemRef == 1010) && (plyr.silver > 0))
                {
                    if (itemQuantity > plyr.silver)
                        itemQuantity = plyr.silver;
                    plyr.silver -= itemQuantity;
                    offermade = true;
                }
                if ((itemRef == 1011) && (plyr.copper > 0))
                {
                    if (itemQuantity > plyr.copper)
                        itemQuantity = plyr.copper;
                    plyr.copper -= itemQuantity;
                    offermade = true;
                }
            }

            if (itemRef < 100)
                // Remember corpses for undead!
                MoveItem(itemRef, 0); // move the inventory item to the void

            var str = "";
            if ((offermade) && (plyr.encounterRef == Encounters.Pauper))
            {
                // Paupers see items 1000 - 1011 as postive offers
                var pauperAcceptance = Randn(0, 6);
                if (pauperAcceptance > 1)
                {
                    if (plyr.gender == 1)
                        str = "\"Bless you, kind sir.\"";
                    if (plyr.gender == 2)
                        str = "\"Bless you, kind madam.\"";
                    plyr.alignment += 1;
                }
                if (pauperAcceptance < 2)
                    str = "\"I was hoping for pears and cheese.\"";
            } else
            {
                str = $"The {Opponents[0].name} leaves.";
            }
            ConsoleMessage(str);
            encounterRunning = false;
        }

        public static void PlayerSnatch () { }

        public static void PlayerSwitchWeapons ()
        {
            var str = "Switching to " + itemBuffer[plyr.secWeapon].name + ".";
            var oldPrimary = plyr.priWeapon;
            plyr.priWeapon = plyr.secWeapon;
            plyr.secWeapon = oldPrimary;
            ConsoleMessage(str);
        }

        public static void PlayerTransact ()
        {
            if ((Opponents[0].alignment > 127) && (encounterNotHostile))
                encounterMenu = 2;

            if ((Opponents[0].alignment < 128) || (!encounterNotHostile))
            {
                var str = $"The {Opponents[0].name} seems uninterested.";
                ConsoleMessage(str);
                playerTurn = false;
                //checkSurprise
            }
        }

        public static void PlayerTrick ()
        {
            UpdateDisplay();
            encounterNotHostile = false;

            //C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
            //ORIGINAL LINE: opponent = Opponents[0];
            opponent.CopyFrom(Opponents[0]);

            var trickSuccess = false;
            if ((opponent.inte > 3) && (plyr.inte != 0))
            {
                // Check for a successful trick attempt
                var intelligenceDifference = plyr.inte - opponent.inte;
                var trickProbability = 0;
                if (intelligenceDifference <= -128)
                    trickProbability = 1;
                if ((intelligenceDifference >= -128) && (intelligenceDifference < -64))
                    trickProbability = 1;
                if ((intelligenceDifference >= -64) && (intelligenceDifference < 0))
                    trickProbability = 7;
                if ((intelligenceDifference >= 0) && (intelligenceDifference < 64))
                    trickProbability = 19;
                if ((intelligenceDifference >= 64) && (intelligenceDifference < 128))
                    trickProbability = 47;
                if ((intelligenceDifference >= 128) && (intelligenceDifference < 192))
                    trickProbability = 75;
                if (intelligenceDifference >= 192)
                    trickProbability = 90;

                var hitRoll = Randn(1, 100);
                if (hitRoll <= trickProbability)
                    trickSuccess = true;
            }
            // TODO: 50% test that future trick attempts always fail during this encounter
            var str = "";
            if (trickSuccess)
            {
                plyr.intPartials++;
                str = "You tricked it!";
            } else
            {
                str = "You failed to trick it!";
            }
            if (plyr.intPartials == 255)
            {
                plyr.inte++;
                plyr.intPartials = 0;
            }
            ConsoleMessage(str);
            if (trickSuccess)
            {
                checkForTreasure = true;
                Opponents[curOpponent].hp = 0;
                AwardExperience(opponentType);
                encounterMenu = 0;
            }

            // Move to opponent turn
            if (!trickSuccess)
            {
                encounterNotHostile = false;
                encounterMenu = 1;
                playerTurn = false;
            }
        }

        public static void PlayerWaylay () { }

        public static void ProcessOpponentAction ()
        {
            if (encounterNotHostile)
            {
                if (encounterTurns == 3)
                    OpponentLeaves();
                else
                    playerTurn = true;
            } else
            {
                OpponentAttack();
            }
            encounterTurns++;

            // If last opponent then switch to player turn
            if (curOpponent == (encounterQuantity - 1))
            {
                playerTurn = true;
                curOpponent = 0;
            } else
            {
                curOpponent++;
            }
        }

        public static void ProcessPlayerAction ()
        {
            if (opponentSurprised)
                encounterMenu = 3;
            if (playerSurprised)
                encounterMenu = 10;
            if (playerStunned)
                encounterMenu = 11;

            if (encounterMenu == 1)
            {
                CyText(1, "Battle Options");
                BText(12, 3, "(1) Attack");
                BText(12, 4, "(2) Charge");
                BText(12, 5, "(3) Aimed Attack");
                BText(12, 6, "(4) Transact");
                BText(12, 7, "(5) Switch weapon");
                BText(12, 8, "(0) Turn and run!");

                if (key == "0")
                {
                    playerRunsAway = true;
                    encounterRunning = false;
                } // "You didn't escape."
                if (key == "P")
                    PauseEncounter();
                if (key == "1")
                    PlayerAttack(1, 0.00F); // Attack (No attack bonus)
                if (key == "2")
                    PlayerAttack(2, 0.50F); // Charge
                if (key == "3")
                    PlayerAttack(3, 0.50F); // Aimed attack
                if (key == "4")
                    PlayerTransact();
                if (key == "5")
                    PlayerSwitchWeapons();
                if (key == "U")
                {
                    UpdateDisplay();
                    SelectItem(SelectStates.Use);
                }
                if (key == "D")
                {
                    UpdateDisplay();
                    SelectItem(SelectStates.Drop);
                }
            } else if (encounterMenu == 2)
            {
                CyText(2, "Transact Options");
                BText(16, 4, "(1) Offer");
                BText(16, 5, "(2) Charm");
                BText(16, 6, "(3) Trick");
                BText(16, 7, "(4) Hail");
                BText(16, 8, "(0) Leave");

                if (key == "1")
                    PlayerOffer();
                if (key == "2")
                    PlayerCharm();
                if (key == "3")
                    PlayerTrick();
                if (key == "4")
                {
                    if ((plyr.encounterRef == Encounters.Healer) && (plyr.scenario == Scenarios.Dungeon))
                        encounterMenu = 5;
                    else
                        PlayerHail();
                }
                if (key == "0")
                    encounterRunning = false;
            } else if (encounterMenu == 3)
            {
                var str = $"You surprise the {Opponents[0].name}.";
                CyText(2, str);
                CyText(3, "Attempt to");
                str = $"(1) waylay the {Opponents[0].name}.";

                BText(12, 4, str);
                BText(12, 5, "(2) snatch something.");
                BText(12, 6, "(3) none of these.");
                BText(12, 8, "(0) Leave.");

                if (key == "1")
                    PlayerWaylay();
                if (key == "2")
                    PlayerSnatch();
                if (key == "3")
                {
                    opponentSurprised = false;
                    encounterMenu = 1;
                }
                if (key == "0")
                    encounterRunning = false;
            } else if (encounterMenu == 10)
            {
                CyText(2, "SURPRISED@@No options");
                if (key == "SPACE")
                {
                    encounterMenu = 1;
                    DetermineOpponentOpeningMessage();
                    playerSurprised = false;
                }
            } else if (encounterMenu == 11)
            {
                CyText(2, "STUNNED@@No options");
                if (key == "SPACE")
                {
                    encounterMenu = 1;
                    playerStunned = false;
                }
            } else if (encounterMenu == 4) // thief grouping
            {
                CyText(2, $"The {Opponents[0].name} demands:");
                CyText(4, "\"Stand and deliver.");
                BText(9, 5, "Thy money or thy life!\"");
                BText(8, 7, "Dost thou yield? (Y or N)");
                if (key == "Y")
                    ThiefYield();
                if (key == "N")
                {
                    encounterNotHostile = false;
                    encounterMenu = 1;
                }
            } else if (encounterMenu == 6) // Knight
            {
                CyText(2, "The Knight demands:");
                CyText(4, "\"Yield the passage or face cold steel!\"");
                BText(8, 7, "Dost thou yield? (Y or N)");
                if (key == "Y")
                {
                    plyr.alignment++;
                    if (plyr.alignment > 255)
                        plyr.alignment = 255;
                    encounterRunning = false;
                }
                if (key == "N")
                {
                    ConsoleMessage("Have at you!");
                    plyr.alignment--;
                    if (plyr.alignment < 0)
                        plyr.alignment = 0;
                    encounterNotHostile = false;
                    encounterMenu = 1;
                }

                // Knight should yield to you in time!
            } else if (encounterMenu == 7) // Pauper
            {
                CyText(2, "The pauper pleads:@@\"Please help a fellow human@trapped in this foul world.\"");
                if (key == "SPACE")
                    encounterMenu = 1;
            } else if (encounterMenu == 5) // healer transact
            {
                CyText(2, "Would you like the healer to:");
                BText(8, 4, "(1) Heal wounds (1 silver each)");
                BText(8, 5, "(2) Cure diseases (100 silvers)");
                BText(8, 6, "(3) Cure poisons (50 silvers)");
                BText(8, 8, "(0) Nothing at all");

                if (key == "1")
                    HealerHealWounds();
                if (key == "2")
                    HealerCureDiseases();
                if (key == "3")
                    HealerCurePoisons();
                if (key == "0")
                    OpponentLeaves();
            } else if (encounterMenu == 12) // Guard after vault raid
            {
                CyText(2, "The guard demands:");
                CyText(4, "\"Surrender in the name of his majesty@the King! Thou art under arrest varlet!\"");
                CyText(7, "Dost thou yield? (Y or N)");
                if (key == "Y")
                    SurrenderToGuard();
                if (key == "N")
                {
                    encounterNotHostile = false;
                    encounterMenu = 1;
                    playerTurn = false;
                }
            }
        }

        public static void RemoveOpponent ()
        {
            // Removes Opponents[0], shuffles the other 7 and adds an Empty

            for (var i = 0; i < (MAX_OPPONENTS); ++i)
                Opponents[i] = Opponents[i + 1];
            // Add an empty slot to the end of the array (the unused FBI Agent is used for this)
            Opponents[(MAX_OPPONENTS - 1)] = Monsters[(int)Encounters.FbiAgent];
        }

        public static void SelectEncounterTheme ()
        {            
            switch (plyr.scenario)
            {
                case Scenarios.City:
                {
                    var theme = (Opponents[0].alignment > 127) ? 0 : 1;
                    PlayEncounterTheme(theme);
                    break;
                };
                case Scenarios.Dungeon:
                {
                    var theme = (Opponents[0].alignment > 128) ? 4 : ((Opponents[0].alignment > 1298) ? 2 : 3);
                    PlayEncounterTheme(theme);
                    break;
                };
            };
        }

        public static void SurrenderToGuard ()
        {
            ConsoleMessage("Enough of thy antics,@to the pokey with thee!");
            if (plyr.guildMemberships[0] > 0)
            {
                ConsoleMessage("Fellow Thieves Guild members rescue@you from prison before your trial.");
                encounterRunning = false;
                plyr.x = 43;
                plyr.y = 30;
            } else
            {
                ConsoleMessage("You receive a swift trial,@and a slow execution.");
                plyr.hp = -1;
            }
        }

        public static void Text ( string str )
        {
            var keynotpressed = true;
            while (keynotpressed)
            {
                DrawEncounterView();
                CyText(4, str);
                UpdateDisplay();
                var key = GetSingleKey();
                if (key != "")
                    keynotpressed = false;
            }
        }

        public static void ThiefYield ()
        {
            var str = "";
            if ((plyr.gold == 0) && (plyr.silver == 0) && (plyr.copper == 0) && (plyr.gems == 0) && (plyr.jewels == 0))
            {
                str = "\"Thou pitiful fool! Have a copper.\"";
                plyr.copper++;
            } else
            {
                str = "\"Farewell pidgeon.\"";
                plyr.gold = 0;
                plyr.silver = 0;
                plyr.copper = 0;
                plyr.gems = 0;
                plyr.jewels = 0;
            }
            ConsoleMessage(str);
            encounterRunning = false;
        }

        public static void UpdateConsoleMessages ()
        {
            // Moves messages along so index [0] contains next message to be printed (if any).
            for (var i = 0; i < MAX_CONSOLE_MESSAGES; ++i)
            {
                if (i == (MAX_CONSOLE_MESSAGES - 1))
                    consoleMessages[i] = "NO MESSAGE";
                else
                    consoleMessages[i] = consoleMessages[i + 1];
            }
        }

        public static void UpdateEncounterStatusText ()
        {
            // Draw status line text
            var text = "";
            SetFontColour(111, 159, 6, 255);

            var turnText = (encounterTurns == 0) ? "encounter" : "face";

            if (encounterQuantity == 1)
            {
                if ((Opponents[0].name.Substring(0, 1) == "A") ||
                    (Opponents[0].name.Substring(0, 1) == "O") ||
                    (Opponents[0].name.Substring(0, 1) == "I") ||
                    (Opponents[0].name.Substring(0, 1) == "o") ||
                    (Opponents[0].name.Substring(0, 1) == "U") ||
                    (Opponents[0].name.Substring(0, 1) == "a") ||
                    (Opponents[0].name.Substring(0, 1) == "i"))
                {
                    text = $"You encounter an {Opponents[0].name}.";
                    ;
                } else
                {
                    text = $"You {turnText} a {Opponents[0].name}.";
                }
            }
            if (encounterQuantity > 1)
                text = $"You {turnText} {encounterQuantity} {Opponents[0].pluName}.";
            var length = text.Length;
            var xpos = (40 - length) / 2;

            DrawText(xpos, 5, text);
            SetFontColour(215, 215, 215, 255);
        }

        public static void UpdateOpponents ()
        {
            // Count total remaining opponents
            encounterQuantity = 0;
            for (var i = 0; i < 8; ++i)
            {
                if (Opponents[i].hp > 0)
                    encounterQuantity++;
            }
        }

        // Level 1 - Fixed Encounters
        //51,4,88,
        //54,5,95,
        //60,5,98,
        //58,7,96,
        //41,19,8d,
        //44,19,8c,
        //47,19,8a,
        //41,25,8e,
        //44,25,8b,
        //47,25,89,
        //25,32,91,
        //18,39,91,
        //21,41,92,
        //6,43,91,
        //18,43,92,
        //19,45,92,
        //7,48,91,
        //36,57,97,

        // Level 2
        //17,1,8f
        //28,5,93
        //28,12,93
        //17,21,82
        //19,21,81
        //21,21,80
        //17,23,83
        //13,25,86
        //15,25,85
        //17,25,84

        // Level 3
        //11,7,99 4 flame demons
        //10,8,9e
        //12,8,9a
        //10,9,9d
        //11,9,9c
        //12,10,9b
        //14,13,87 basilisk

        //extern int statPanelY;
        //extern bool musicPlaying;
    }
}