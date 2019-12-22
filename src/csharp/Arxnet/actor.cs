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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace P3Net.Arx
{
    public class MonsterFramePair
    {
        public int startFrame { get; set; }
        public int endFrame { get; set; }
    }
    
    public class Monster
    {
        public int type { get; set; }
        public string name { get; set; }
        public string pluName { get; set; }
        public string armorText { get; set; }
        public int behaviour { get; set; } // evil, bad, neutral, good
        public int alignment { get; set; }
        public int stealth { get; set; }
        public int randomStrength { get; set; }
        public int randomSkill { get; set; }
        public int randomIntelligence { get; set; }
        public int randomSpeed { get; set; }
        public int hp { get; set; }
        public int randomHP { get; set; }
        public int maxHP { get; set; }
        public int image { get; set; } // becomes first frame in animations array
        public int image2 { get; set; } // becomes last frame in animations array

        public int sta { get; set; }
        public int cha { get; set; }
        public int str { get; set; }
        public int inte { get; set; }
        public int wis { get; set; }
        public int skl { get; set; }
        public int spd { get; set; }

        public int aBlunt { get; set; }
        public int aSharp { get; set; }
        public int aEarth { get; set; }
        public int aAir { get; set; }
        public int aFire { get; set; }
        public int aWater { get; set; }
        public int aPower { get; set; }
        public int aMagic { get; set; }
        public int aGood { get; set; }
        public int aEvil { get; set; }
        public int aCold { get; set; }

        public int tPotions { get; set; }
        public int tEquipment { get; set; }
        public int tFood { get; set; }
        public int tWater { get; set; }
        public int tTorches { get; set; }
        public int tTimepieces { get; set; }
        public int tCompasses { get; set; }
        public int tKeys { get; set; }
        public int tCrystals { get; set; }
        public int tCopper { get; set; }
        public int tSilver { get; set; }
        public int tGold { get; set; }
        public int tGems { get; set; }
        public int tJewels { get; set; }

        public int w1 { get; set; }
        public int w2 { get; set; }
        public int w3 { get; set; }
        public int w4 { get; set; }
        public int w5 { get; set; }
        public int w6 { get; set; }
        public int c1 { get; set; }
        public int c2 { get; set; }
        public int c3 { get; set; }
        public int c4 { get; set; }
        public int c5 { get; set; }
        public int c6 { get; set; }

        //TODO: Separate the monster definition from the current encounter so we don't have to copy this data each time
        public Monster CopyFrom ( Monster source ) => MemberwiseClone() as Monster;
    }

    public class Weapon
    {
        public string article { get; set; }
        public string name { get; set; }
        public string desc { get; set; }
        public string descMon { get; set; }
        public int type { get; set; }

        public int flags { get; set; }
        public int minStrength { get; set; }
        public int minDexterity { get; set; }
        public int useStrength { get; set; }
        public int blunt { get; set; }
        public int sharp { get; set; }
        public int earth { get; set; }
        public int air { get; set; }
        public int fire { get; set; }
        public int water { get; set; }
        public int power { get; set; }
        public int magic { get; set; } // mental
        public int good { get; set; } // cleric
        public int evil { get; set; }
        public int cold { get; set; }
        public int weight { get; set; }
        public int hp { get; set; }
        public int maxHP { get; set; }
        public int alignment { get; set; }
        public int melee { get; set; }
        public int ammo { get; set; }
        public int parry { get; set; }
    }

    public partial class GlobalMembers
    {
        public const int noOfEncounters = 84;
        public const int noOfMonsterWeapons = 250;
               
        public const int noOfWeapons = 111;
        public const int monstersFileSize = 45056;        

        public static void LoadEncounters ()
        {
            //TODO: Read as structural data
            using (var reader = new StreamReader("data/map/encounters.txt"))
            {                 
                for (var i = 0; i < noOfEncounters; ++i) // number of monsters
                {
                    var attributes = 48; // number of lines making up each record
                    
                    // read first line as blank
                    reader.ReadLine();
                    
                    for (var a = 0; a < attributes; ++a) // number of attributes per record
                    {
                        var line = reader.ReadLine();                        
                        var idx = line.IndexOf(':');
                        var text = line.Substring(idx + 1);

                        if (a == 0)
                            Monsters[i].name = text;
                        if (a == 1)
                            Monsters[i].pluName = text;
                        if (a == 2)
                            Monsters[i].armorText = text;
                        if (a == 3)
                            Monsters[i].hp = Convert.ToInt32(text);
                        if (a == 4)
                            Monsters[i].alignment = Convert.ToInt32(text);
                        if (a == 5)
                            Monsters[i].image = Convert.ToInt32(text);
                        if (a == 6)
                            Monsters[i].sta = Convert.ToInt32(text);
                        if (a == 7)
                            Monsters[i].cha = Convert.ToInt32(text);
                        if (a == 8)
                            Monsters[i].str = Convert.ToInt32(text);
                        if (a == 9)
                            Monsters[i].inte = Convert.ToInt32(text);
                        if (a == 10)
                            Monsters[i].wis = Convert.ToInt32(text);
                        if (a == 11)
                            Monsters[i].skl = Convert.ToInt32(text);
                        if (a == 12)
                            Monsters[i].spd = Convert.ToInt32(text);

                        if (a == 13)
                            Monsters[i].tFood = Convert.ToInt32(text);
                        if (a == 14)
                            Monsters[i].tWater = Convert.ToInt32(text);
                        if (a == 15)
                            Monsters[i].tTorches = Convert.ToInt32(text);
                        if (a == 16)
                            Monsters[i].tTimepieces = Convert.ToInt32(text);
                        if (a == 17)
                            Monsters[i].tCompasses = Convert.ToInt32(text);
                        if (a == 18)
                            Monsters[i].tKeys = Convert.ToInt32(text);
                        if (a == 19)
                            Monsters[i].tCrystals = Convert.ToInt32(text);
                        if (a == 20)
                            Monsters[i].tGems = Convert.ToInt32(text);
                        if (a == 21)
                            Monsters[i].tJewels = Convert.ToInt32(text);
                        if (a == 22)
                            Monsters[i].tGold = Convert.ToInt32(text);
                        if (a == 23)
                            Monsters[i].tSilver = Convert.ToInt32(text);
                        if (a == 24)
                            Monsters[i].tCopper = Convert.ToInt32(text);

                        if (a == 25)
                            Monsters[i].aBlunt = Hex2Dec(text);
                        if (a == 26)
                            Monsters[i].aSharp = Hex2Dec(text);
                        if (a == 27)
                            Monsters[i].aEarth = Hex2Dec(text);
                        if (a == 28)
                            Monsters[i].aAir = Hex2Dec(text);
                        if (a == 29)
                            Monsters[i].aFire = Hex2Dec(text);
                        if (a == 30)
                            Monsters[i].aWater = Hex2Dec(text);
                        if (a == 31)
                            Monsters[i].aPower = Hex2Dec(text);
                        if (a == 32)
                            Monsters[i].aMagic = Hex2Dec(text);
                        if (a == 33)
                            Monsters[i].aGood = Hex2Dec(text);
                        if (a == 34)
                            Monsters[i].aEvil = Hex2Dec(text);
                        if (a == 35)
                            Monsters[i].aCold = Hex2Dec(text);

                        if (a == 36)
                            Monsters[i].w1 = Convert.ToInt32(text);
                        if (a == 37)
                            Monsters[i].w2 = Convert.ToInt32(text);
                        if (a == 38)
                            Monsters[i].w3 = Convert.ToInt32(text);
                        if (a == 39)
                            Monsters[i].w4 = Convert.ToInt32(text);
                        if (a == 40)
                            Monsters[i].w5 = Convert.ToInt32(text);
                        if (a == 41)
                            Monsters[i].w6 = Convert.ToInt32(text);

                        if (a == 42)
                            Monsters[i].c1 = Convert.ToInt32(text);
                        if (a == 43)
                            Monsters[i].c2 = Convert.ToInt32(text);
                        if (a == 44)
                            Monsters[i].c3 = Convert.ToInt32(text);
                        if (a == 45)
                            Monsters[i].c4 = Convert.ToInt32(text);
                        if (a == 46)
                            Monsters[i].c5 = Convert.ToInt32(text);
                        if (a == 47)
                            Monsters[i].c6 = Convert.ToInt32(text);

                    }
                }
            };
        }
        public static void LoadMonstersBinary ()
        {
            //TODO: Read as structured data

            // Loads 42kb of monster binary data into the "monstersBinary" array
            var data = File.ReadAllBytes("data/map/monsters.bin");

            //TODO: Ignoring the upper limit on monster array right now
            monstersBinary = data;
        }

        public static void InitialiseMonsterOffsets ()
        {
            // Temp routine to fill all values with FBI Agent until they are added below
            for (var i = 0; i < noOfEncounters; i++)
                monsterOffsets[i] = 0x1D1D; // Novice

            monsterOffsets[0] = 0x00; // Devourer
            monsterOffsets[1] = 0x246; // "Thief" class
            monsterOffsets[2] = 0x763; // Assassin 0x763
            monsterOffsets[3] = 0x875; // Goblin
            monsterOffsets[4] = 0xA22; // Troll
            monsterOffsets[5] = 0xB58; // Lich
            monsterOffsets[6] = 0xBF7; // Undead Knight
            monsterOffsets[7] = 0xCEE; // "Guard" class
            monsterOffsets[8] = 0x10E7; // FBI Agent
            monsterOffsets[9] = 0x117F; // Dark Knight
            monsterOffsets[10] = 0x124F; // Champion
            monsterOffsets[11] = 0x1364; // Healer
            monsterOffsets[12] = 0x169B; // Knight
            monsterOffsets[13] = 0x184F; // Pauper
            monsterOffsets[14] = 0x1A21; // Nobleman
            monsterOffsets[15] = 0x1B6C; // Alien sentry
            monsterOffsets[16] = 0x1C25; // robot
            monsterOffsets[17] = 0x1D1D; // novice
            monsterOffsets[18] = 0x1DE1; // apprentice
            monsterOffsets[19] = 0x1EA1; // mage
            monsterOffsets[20] = 0x1F58; // wizard / archmage
            monsterOffsets[21] = 0x20D7; // acolyte
            monsterOffsets[22] = 0x21C6; // sage
            monsterOffsets[23] = 0x230C; // orc
            monsterOffsets[24] = 0x23C5; // gnome
            monsterOffsets[25] = 0x2461; // dwarf
            monsterOffsets[26] = 0x2526; // slime
            monsterOffsets[27] = 0x2664; // mold
            monsterOffsets[28] = 0x2748; // homunculus
            monsterOffsets[29] = 0x27DE; // phoenix
            monsterOffsets[30] = 0x2898; // sorceress
            monsterOffsets[31] = 0x2981; // whirlwind
            monsterOffsets[32] = 0x2A82; // giant rat
            monsterOffsets[33] = 0x2B53; // small dragon
            monsterOffsets[34] = 0x2C14; // skeleton
            monsterOffsets[35] = 0x2CD2; // zombie
            monsterOffsets[36] = 0x2D8E; // ghoul
            monsterOffsets[37] = 0x2E92; // ghost
            monsterOffsets[38] = 0x2F3D; // spectre
            monsterOffsets[39] = 0x2FE2; // wraith
            monsterOffsets[40] = 0x30ED; // vampire
            monsterOffsets[41] = 0x3205; // great bat
            monsterOffsets[42] = 0x32C8; // hellhound
            monsterOffsets[43] = 0x339E; // harpy
            monsterOffsets[44] = 0x3479; // gremlin
            monsterOffsets[45] = 0x3537; // imp
            monsterOffsets[46] = 0x35FD; // flame demon
            monsterOffsets[47] = 0x3781; // storm devil
            monsterOffsets[48] = 0x3833; // giant wolf
            monsterOffsets[49] = 0x38BD; // werewolf
            monsterOffsets[50] = 0x39C8; // warrior
            monsterOffsets[51] = 0x3A88; // weapon master
            monsterOffsets[52] = 0x3B55; // valkyrie
            monsterOffsets[53] = 0x3BD9; // gladiator
            monsterOffsets[54] = 0x3C60; // mercenary
            monsterOffsets[55] = 0x3CEC; // doppleganger
            monsterOffsets[56] = 0x3DCE; // adventurer
            monsterOffsets[57] = 0x3E77; // water sprite
            monsterOffsets[58] = 0x3EED; // night stalker
            monsterOffsets[59] = 0x3FAC; // salamander
            monsterOffsets[60] = 0x4068; // ronin
            monsterOffsets[61] = 0x416C; // serpentman
            monsterOffsets[62] = 0x4227; // big snake
            monsterOffsets[63] = 0x42CD; // Great Naga
            monsterOffsets[64] = 0x4350; // berserker
            monsterOffsets[65] = 0x4423; // basilisk
            monsterOffsets[66] = 0x4631; // Great Wyrm
            monsterOffsets[67] = 0x4708; // Goblin Lord
            monsterOffsets[68] = 0x47E3; // Troll Tyrant
            monsterOffsets[69] = 0x48C8; // ice demon
            monsterOffsets[70] = 0x494A; // horned devil
        }
        public static void ConvertMonstersBinary ()
        {
            // Reads through the binary block and creates entry in Monsters[]
            // Bytes 2, 4, 6, 8 and 10 appear to always have a value of 0xAA

            currentWeapon = 0; // Index for next weapon or attack to be added to monsterWeapons[]

            for (var i = 0; i < noOfEncounters; i++)
            {
                var idx = monsterOffsets[i]; // Sets current monster start address in binary block

                maxNumberEncountered = monstersBinary[idx];

                var animationNumber = monstersBinary[idx + 0x1D];
                Monsters[i].image = animations[animationNumber].startFrame;
                Monsters[i].image2 = animations[animationNumber].endFrame;

                var nameTextOffset = idx + monstersBinary[(idx + 1)];
                ReadMonsterNameText(i, nameTextOffset);

                // Note: idx+3 might be offset for plural name - if one exists
                if (maxNumberEncountered > 1)
                    ReadMonsterPluralNameText(i, pluralNameOffset);

                var deathTextOffset = idx + 66;
                ReadMonsterDeathText(i, deathTextOffset);

                Monsters[i].hp = monstersBinary[idx + 0x23];
                Monsters[i].maxHP = monstersBinary[idx + 0x23];

                // Special cases - thief
                if (i == (int)Encounters.Thief)
                {
                    Monsters[i].hp = 5;
                    Monsters[i].maxHP = 5;
                }
                if (i == (int)Encounters.FbiAgent)
                {
                    Monsters[i].hp = 0;
                    Monsters[i].maxHP = 0;
                }

                Monsters[i].alignment = monstersBinary[idx + 12];

                Monsters[i].sta = monstersBinary[idx + 0x24];
                Monsters[i].cha = monstersBinary[idx + 0x25];
                Monsters[i].str = monstersBinary[idx + 0x26];
                Monsters[i].inte = monstersBinary[idx + 0x27];
                Monsters[i].wis = monstersBinary[idx + 0x28];
                Monsters[i].skl = monstersBinary[idx + 0x29];
                Monsters[i].spd = monstersBinary[idx + 0x2A];

                Monsters[i].tFood = monstersBinary[idx + 0x2B];
                Monsters[i].tWater = monstersBinary[idx + 0x2C];
                Monsters[i].tTorches = monstersBinary[idx + 0x2D];
                Monsters[i].tTimepieces = monstersBinary[idx + 0x2E];
                Monsters[i].tCompasses = monstersBinary[idx + 0x2F];
                Monsters[i].tKeys = monstersBinary[idx + 0x30];
                Monsters[i].tCrystals = monstersBinary[idx + 0x31];
                Monsters[i].tGems = monstersBinary[idx + 0x32];
                Monsters[i].tJewels = monstersBinary[idx + 0x33];
                Monsters[i].tGold = monstersBinary[idx + 0x34];
                Monsters[i].tSilver = monstersBinary[idx + 0x35];
                Monsters[i].tCopper = monstersBinary[idx + 0x36];

                Monsters[i].aBlunt = monstersBinary[idx + 0x37];
                Monsters[i].aSharp = monstersBinary[idx + 0x38];
                Monsters[i].aEarth = monstersBinary[idx + 0x39];
                Monsters[i].aAir = monstersBinary[idx + 0x3A];
                Monsters[i].aFire = monstersBinary[idx + 0x3B];
                Monsters[i].aWater = monstersBinary[idx + 0x3C];
                Monsters[i].aPower = monstersBinary[idx + 0x3D];
                Monsters[i].aMagic = monstersBinary[idx + 0x3E];
                Monsters[i].aGood = monstersBinary[idx + 0x3F];
                Monsters[i].aEvil = monstersBinary[idx + 0x40];
                Monsters[i].aCold = monstersBinary[idx + 0x41];

                // Temporary weapon values based on existing ARX weapon handling
                Monsters[i].w1 = 1;
                Monsters[i].w2 = 0;
                Monsters[i].w3 = 0;
                Monsters[i].w4 = 0;
                Monsters[i].w5 = 0;
                Monsters[i].w6 = 0;
                Monsters[i].c1 = 100;
                Monsters[i].c2 = 0;
                Monsters[i].c3 = 0;
                Monsters[i].c4 = 0;
                Monsters[i].c5 = 0;
                Monsters[i].c6 = 0;

                // Weapon / attack reading

                var weapon1 = idx + monstersBinary[idx + 5];
                var weapon2 = idx + monstersBinary[idx + 7];
                var weapon3 = idx + monstersBinary[idx + 9];

                if (i == (int)Encounters.Thief)
                {
                    weapon1 = 0x32F;
                }

                CreateMonsterWeapon(currentWeapon, weapon1);
                Monsters[i].w1 = currentWeapon;
                currentWeapon++; // Increment each time a new weapon or attack is created

                if (!(weapon1 == weapon2))
                {
                    CreateMonsterWeapon(currentWeapon, weapon2);
                    Monsters[i].w2 = currentWeapon;
                    currentWeapon++; // Increment each time a new weapon or attack is created
                }

                if ((!(weapon1 == weapon3)) && (!(weapon2 == weapon3)))
                {
                    CreateMonsterWeapon(currentWeapon, weapon3);
                    Monsters[i].w3 = currentWeapon;
                    currentWeapon++; // Increment each time a new weapon or attack is created
                }
            }
        }

        public static void ReadMonsterNameText ( int monsterNo, int nameOffset )
        {
            // Special Case - "thief" class has value 0
            if (monsterNo == 1)
                nameOffset = 0x2AA;
                        
            var name = ReadBinaryString(monstersBinary, nameOffset);

            // Some special monster classes have multiple entries here
            if (maxNumberEncountered > 1)
                pluralNameOffset = name.Length + 1;

            Monsters[monsterNo].name = name;
        }

        public static void ReadMonsterPluralNameText ( int monsterNo, int pluralNameOffset )
        {
            // Special Case - "thief" class has value 0
            if (monsterNo == 1)
                pluralNameOffset = 0x2CF;

            // Loop through until 0 found
            // Some special monster classes have multiple entries here
            Monsters[monsterNo].pluName = ReadBinaryString(monstersBinary, pluralNameOffset);
        }

        public static void ReadMonsterDeathText ( int monsterNo, int deathOffset )
        {
            // Change variable name
            if (monsterNo == (int)Encounters.Devourer)
                deathOffset = 0x42;

            var name = ReadBinaryString(monstersBinary, deathOffset, 0xAE);

            //Convert 0xA5 to 0x40
            name = name.Replace(Char.ConvertFromUtf32(0xA5), Char.ConvertFromUtf32(0x40));

            // Some special monster classes have multiple entries here
            Monsters[monsterNo].armorText = name;
        }

        public static void CreateMonsterWeapon ( int currentWeapon, int weaponOffset )
        {
            var weaponNameOffset = weaponOffset + 6;
            monsterWeapons[currentWeapon].name = ReadBinaryString(monstersBinary, weaponNameOffset);

            // byte 2 is object length, byte 3 is unknown
            monsterWeapons[currentWeapon].type = monstersBinary[weaponOffset + 0];
            monsterWeapons[currentWeapon].alignment = monstersBinary[weaponOffset + 3];
            monsterWeapons[currentWeapon].weight = monstersBinary[weaponOffset + 4];

            var wAttributes = (weaponOffset + monstersBinary[weaponOffset + 1]) - 20; // Working out from the end of the weapon object

            monsterWeapons[currentWeapon].melee = monstersBinary[wAttributes + 1];
            monsterWeapons[currentWeapon].ammo = monstersBinary[wAttributes + 2];
            monsterWeapons[currentWeapon].blunt = monstersBinary[wAttributes + 3];
            monsterWeapons[currentWeapon].sharp = monstersBinary[wAttributes + 4];
            monsterWeapons[currentWeapon].earth = monstersBinary[wAttributes + 5];
            monsterWeapons[currentWeapon].air = monstersBinary[wAttributes + 6];
            monsterWeapons[currentWeapon].fire = monstersBinary[wAttributes + 7];
            monsterWeapons[currentWeapon].water = monstersBinary[wAttributes + 8];
            monsterWeapons[currentWeapon].power = monstersBinary[wAttributes + 9];
            monsterWeapons[currentWeapon].magic = monstersBinary[wAttributes + 10];
            monsterWeapons[currentWeapon].good = monstersBinary[wAttributes + 11];
            monsterWeapons[currentWeapon].evil = monstersBinary[wAttributes + 12];
            monsterWeapons[currentWeapon].cold = monstersBinary[wAttributes + 13];
            monsterWeapons[currentWeapon].minStrength = monstersBinary[wAttributes + 14];
            monsterWeapons[currentWeapon].minDexterity = monstersBinary[wAttributes + 15];
            monsterWeapons[currentWeapon].hp = monstersBinary[wAttributes + 16];
            monsterWeapons[currentWeapon].maxHP = monstersBinary[wAttributes + 17];
            monsterWeapons[currentWeapon].flags = monstersBinary[wAttributes + 18];
            monsterWeapons[currentWeapon].parry = monstersBinary[wAttributes + 19];            
        }

        public static MonsterFramePair[] animations =
        {
                new MonsterFramePair () { startFrame = 0, endFrame = 2 },
                new MonsterFramePair () { startFrame = 27, endFrame = 28 },
                new MonsterFramePair () { startFrame = 72, endFrame = 73 },
                new MonsterFramePair () { startFrame = 98, endFrame = 99 },
                new MonsterFramePair () { startFrame = 109, endFrame = 110 },
                new MonsterFramePair () { startFrame = 111, endFrame = 112 },
                new MonsterFramePair () { startFrame = 113, endFrame = 114 },
                new MonsterFramePair () { startFrame = 115, endFrame = 116 },
                new MonsterFramePair () { startFrame = 115, endFrame = 116 },
                new MonsterFramePair () { startFrame = 117, endFrame = 122 },
                new MonsterFramePair () { startFrame = 3, endFrame = 4 },
                new MonsterFramePair () { startFrame = 5, endFrame = 6 },
                new MonsterFramePair () { startFrame = 7, endFrame = 7 },
                new MonsterFramePair () { startFrame = 8, endFrame = 9 },
                new MonsterFramePair () { startFrame = 10, endFrame = 14 },
                new MonsterFramePair () { startFrame = 15, endFrame = 15 },
                new MonsterFramePair () { startFrame = 16, endFrame = 18 },
                new MonsterFramePair () { startFrame = 19, endFrame = 19 },
                new MonsterFramePair () { startFrame = 20, endFrame = 22 },
                new MonsterFramePair () { startFrame = 23, endFrame = 26 },
                new MonsterFramePair () { startFrame = 29, endFrame = 36 },
                new MonsterFramePair () { startFrame = 37, endFrame = 44 },
                new MonsterFramePair () { startFrame = 45, endFrame = 46 },
                new MonsterFramePair () { startFrame = 47, endFrame = 51 },
                new MonsterFramePair () { startFrame = 52, endFrame = 53 },
                new MonsterFramePair () { startFrame = 54, endFrame = 57 },
                new MonsterFramePair () { startFrame = 58, endFrame = 60 },
                new MonsterFramePair () { startFrame = 61, endFrame = 65 },
                new MonsterFramePair () { startFrame = 66, endFrame = 69 },
                new MonsterFramePair () { startFrame = 70, endFrame = 71 },
                new MonsterFramePair () { startFrame = 74, endFrame = 75 },
                new MonsterFramePair () { startFrame = 76, endFrame = 77 },
                new MonsterFramePair () { startFrame = 78, endFrame = 83 },
                new MonsterFramePair () { startFrame = 84, endFrame = 85 },
                new MonsterFramePair () { startFrame = 86, endFrame = 87 },
                new MonsterFramePair () { startFrame = 108, endFrame = 108 },
                new MonsterFramePair () { startFrame = 89, endFrame = 91 },
                new MonsterFramePair () { startFrame = 92, endFrame = 93 },
                new MonsterFramePair () { startFrame = 94, endFrame = 95 },
                new MonsterFramePair () { startFrame = 96, endFrame = 97 },
                new MonsterFramePair () { startFrame = 100, endFrame = 103 },
                new MonsterFramePair () { startFrame = 104, endFrame = 105 },
                new MonsterFramePair () { startFrame = 106, endFrame = 107 },
                new MonsterFramePair () { startFrame = 100, endFrame = 103 }
            };

        public static Monster[] Monsters = Arrays.InitializeWithDefaultInstances<Monster>(noOfEncounters); // City and Dungeon monsters combined
        public static Weapon[] monsterWeapons = Arrays.InitializeWithDefaultInstances<Weapon>(noOfMonsterWeapons); // Weapons & attacks which are part of the Dungeon monsters data
        public static byte[] monstersBinary = new byte[monstersFileSize];

        public static int[] monsterOffsets = new int[noOfEncounters];
        public static int pluralNameOffset;
        public static int maxNumberEncountered;
        public static int currentWeapon;

        //extern monster Monsters[noOfEncounters];
        //extern weapon monsterWeapons[noOfMonsterWeapons];
        //extern byte monstersBinary[monstersFileSize];
    }
}