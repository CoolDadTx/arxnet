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
using System.IO;

namespace P3Net.Arx
{
    public partial class GlobalMembers
    {
        public static void LoadDungeonItems ()
        {
            //TODO: Ignoring dungeonItemsSize
            // Load items into binary char array
            dungeonItems = File.ReadAllBytes("data/map/items.bin");
        }

        //TODO: Not used but for symmetry leave it here and move to Item class
        public static int CreateArmor ( int armor_no )
        {
            // location options:
            // 0 - limbo, 1 - floor, 10 - carried, 11 - primary, 12 - secondary,
            // 13 - worn, 14 - head, 15 - arms, 16 - legs, 17 - body
            TidyObjectBuffer();

            var new_item = new BufferItem {
                type = 177, // temporary object type to indicate armor
                index = armor_no,
                location = 10, // carried in inventory but not in use
                x = plyr.x,
                y = plyr.y,
                level = plyr.map,
                hp = 12 // temp value to act as breakable value
            };
            itemBuffer[plyr.buffer_index] = new_item;

            //TODO: Just use x++ right?
            var new_item_ref = plyr.buffer_index;
            plyr.buffer_index++;
            return new_item_ref; // what was the new items index in the object buffer
        }

        public static void CreateBareHands ()
        {
            var itemRef = CreateItem(178, 0, "bare hand", 255, 255, 6, 0, 0, 1, 0x15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 255, 0, 0);
            itemBuffer[itemRef].location = 99; // body part of player - 99 so it doesn't show up in the inventory
                                               //plyr.priWeapon = itemRef;
                                               //plyr.secWeapon = itemRef;
        }

        //TODO: Add to Item class
        public static int CreateGenericItem ( int type, int value )
        {
            // generic item type = 1 - food, 2 - water, 3 - torches, 4 - timepieces, 5 - compasses
            // location options:
            // 0 - limbo, 1 - floor, 10 - carried, 11 - primary, 12 - secondary,
            // 13 - worn, 14 - head, 15 - arms, 16 - legs, 17 - body
            //int weapon_no = encounters[monster_no].weapon_no;
            TidyObjectBuffer();

            var new_item = new BufferItem {
                //new_item.index = 0; // should be weapon_no?
                type = type, // gold, crystals, keys, gems
                             //new_item.index = weapon_no;
                location = 1, // the floor
                x = plyr.x,
                y = plyr.y,
                level = plyr.map,

                hp = value // for generic items sets number e.g. 4 food packets, 3 gold
            };
            itemBuffer[plyr.buffer_index] = new_item;

            //TODO: x++ right?
            var new_item_ref = plyr.buffer_index;
            plyr.buffer_index++;
            return new_item_ref; // what was the new items index in the object buffer
        }

        //TODO: Add to Item class
        public static int CreateItem ( int type, int index, string name, int hp, int maxHP, int flags, int minStrength, int minDexterity, int useStrength, int blunt, int sharp, int earth, int air, int fire, int water, int power, int magic, int good, int evil, int cold, int weight, int alignment, int melee, int ammo, int parry )
        {
            // Create a new item in itemBuffer[]

            // 0 - limbo, 1 - floor, 10 - carried, 11 - primary, 12 - secondary,
            // 13 - worn, 14 - head, 15 - arms, 16 - legs, 17 - body

            // Clean up itemBuffer[] before adding a new item
            TidyObjectBuffer();

            // Create a new item
            var new_item = new BufferItem() {

                // Set item attributes
                type = type,
                index = index,
                name = name,
                hp = hp,
                maxHP = maxHP,
                flags = flags,
                minStrength = minStrength,
                minDexterity = minDexterity,
                useStrength = useStrength,
                blunt = blunt,
                sharp = sharp,
                earth = earth,
                air = air,
                fire = fire,
                water = water,
                power = power,
                magic = magic,
                good = good,
                evil = evil,
                cold = cold,
                weight = weight,
                alignment = alignment,
                melee = melee,
                ammo = ammo,
                parry = parry,

                // Set location attributes
                location = 1, // the floor
                x = plyr.x,
                y = plyr.y,
                level = plyr.map
            };

            // Update buffer and buffer references
            itemBuffer[plyr.buffer_index] = new_item;

            //TODO: Isn't this equivalent to plyr.Buffer_index++     
            var new_item_ref = plyr.buffer_index;
            plyr.buffer_index++;
            return new_item_ref; // what was the new items index in the object buffer
        }

        //TODO: Add to Item class
        public static int CreatePotion ( int potion_no )
        {
            // location options:
            // 0 - limbo, 1 - floor, 10 - carried, 11 - primary, 12 - secondary,
            // 13 - worn, 14 - head, 15 - arms, 16 - legs, 17 - body
            TidyObjectBuffer();

            var new_item = new BufferItem {
                type = 176, // object type to indicate potion
                index = potion_no, // Index will define which of 42 potion types this is
                location = 1, // On floor after encounter
                x = plyr.x,
                y = plyr.y,
                level = plyr.map,
                hp = 0 // For potions 0 indicates unidentified
            };
            itemBuffer[plyr.buffer_index] = new_item;

            //TODO: Just use x++ right?
            var new_item_ref = plyr.buffer_index;
            plyr.buffer_index++;
            return new_item_ref; // what was the new items index in the object buffer
        }

        //TODO: Add to Item class
        public static int CreateQuestItem ( int questItemNo )
        {
            // location options:
            // 0 - limbo, 1 - floor, 10 - carried, 11 - primary, 12 - secondary,
            // 13 - worn, 14 - head, 15 - arms, 16 - legs, 17 - body
            //int weapon_no = encounters[monster_no].weapon_no;
            TidyObjectBuffer();

            var new_item = new BufferItem() {
                type = 200, // type for quest items e.g ring halves, silver key etc
                index = questItemNo, // for quest items index is used to identify the object
                location = 1, // the floor
                x = plyr.x,
                y = plyr.y,
                level = plyr.map,
            };
            itemBuffer[plyr.buffer_index] = new_item;

            //TODO: Just use x++ right?
            var new_item_ref = plyr.buffer_index;
            plyr.buffer_index++;
            return new_item_ref; // what was the new items index in the object buffer
        }

        //TODO: Add to Item class
        public static int CreateWeapon ( int weapon_no )
        {
            // Create a new monster weapon on the floor

            // 0 - limbo, 1 - floor, 10 - carried, 11 - primary, 12 - secondary,
            // 13 - worn, 14 - head, 15 - arms, 16 - legs, 17 - body

            TidyObjectBuffer();

            var new_item = new BufferItem() {

                //TODO: Hard coded type, why?
                // Set weapon type
                //type = monsterWeapons[weapon_no].type,
                type = 178, // type for a weapon that can be wielded (e.g not claws)
                index = weapon_no, // Reference to monsterWeapons - currently index not a binary offset

                // Copy weapon attributes
                name = monsterWeapons[weapon_no].name,
                hp = monsterWeapons[weapon_no].hp,
                maxHP = monsterWeapons[weapon_no].maxHP,
                flags = monsterWeapons[weapon_no].flags,
                minStrength = monsterWeapons[weapon_no].minStrength,
                minDexterity = monsterWeapons[weapon_no].minDexterity,
                useStrength = monsterWeapons[weapon_no].useStrength,
                blunt = monsterWeapons[weapon_no].blunt,
                sharp = monsterWeapons[weapon_no].sharp,
                earth = monsterWeapons[weapon_no].earth,
                air = monsterWeapons[weapon_no].air,
                fire = monsterWeapons[weapon_no].fire,
                water = monsterWeapons[weapon_no].water,
                power = monsterWeapons[weapon_no].power,
                magic = monsterWeapons[weapon_no].magic,
                good = monsterWeapons[weapon_no].good,
                evil = monsterWeapons[weapon_no].evil,
                cold = monsterWeapons[weapon_no].cold,
                weight = monsterWeapons[weapon_no].weight,
                alignment = monsterWeapons[weapon_no].alignment,
                melee = monsterWeapons[weapon_no].melee, // Don't think needed
                ammo = monsterWeapons[weapon_no].ammo, // Don't think needed
                parry = monsterWeapons[weapon_no].parry,

                // Set weapon location
                location = 1, // the floor
                x = plyr.x,
                y = plyr.y,
                level = plyr.map
            };

            // Update buffer and buffer references
            itemBuffer[plyr.buffer_index] = new_item;

            //TODO: Just use x++ right?
            var new_item_ref = plyr.buffer_index;
            plyr.buffer_index++;
            return new_item_ref; // what was the new items index in the object buffer
        }

        //TODO: Add to Item class
        public static int CreateClothing ( int clothing_no )
        {
            TidyObjectBuffer();

            var new_item = new BufferItem() {
                name = clothingItems[clothing_no].name,
                type = 180, // clothing type
                index = clothing_no,
                location = 10, // carried in inventory but not in use
                x = plyr.x,
                y = plyr.y,
                level = plyr.map,
                hp = 12, // temp value to act as breakable value
            };
            itemBuffer[plyr.buffer_index] = new_item;

            //TODO: Just use x++ right?
            var new_item_ref = plyr.buffer_index;
            plyr.buffer_index++;
            return new_item_ref; // what was the new items index in the object buffer
        }

        public static void DisplayLocation ()
        {
            var levelDesc = "";
            var keynotpressed = true;
            var squaresNorth = 63 - plyr.y;
            while (keynotpressed)
            {
                switch (plyr.map)
                {
                    case 0:
                    levelDesc = "the City";
                    break;
                    case 1:
                    levelDesc = "level 1";
                    break;
                    case 2:
                    levelDesc = "level 2";
                    squaresNorth = 31 - plyr.y;
                    break;
                    case 3:
                    levelDesc = "level 3";
                    squaresNorth = 15 - plyr.y;
                    break;
                    case 4:
                    levelDesc = "level 4";
                    squaresNorth = 7 - plyr.y;
                    break;
                };

                if (plyr.status == GameStates.Encounter)
                    DrawEncounterView();
                else if (plyr.status == GameStates.Explore)
                    DispMain();
                else if (plyr.status == (GameStates)0)
                    DispMain();
                CyText(3, $"You are {squaresNorth} squares North@and {plyr.x} squares East from the SouthWest@corner of {levelDesc}.");
                CyText(8, "<<< Press any key to continue >>>");
                UpdateDisplay();
                var key = GetSingleKey();
                if (key != "")
                    keynotpressed = false;
            }
        }

        //TODO: Add to Item class
        public static string GetItemDesc ( int itemRef )
        {
            var itemDesc = "ERROR";
            if (itemBuffer[itemRef].type == 176)
                itemDesc = "Potion";
            if (itemBuffer[itemRef].type == 177)
                itemDesc = itemBuffer[itemRef].name;
            if (itemBuffer[itemRef].type == 178)
                itemDesc = itemBuffer[itemRef].name;
            if (itemBuffer[itemRef].type == 180)
                itemDesc = itemBuffer[itemRef].name;
            if (itemBuffer[itemRef].type == 199)
                itemDesc = itemBuffer[itemRef].name;
            if (itemBuffer[itemRef].type == 200)
                itemDesc = questItems[(itemBuffer[itemRef].index)].name;
            if (itemBuffer[itemRef].type == 201)
                itemDesc = questItems[(itemBuffer[itemRef].index)].name;
            return itemDesc;
        }

        //TODO: Add to Player class
        public static void MoveItem ( int itemRef, int newLocation )
        {
            itemBuffer[itemRef].location = newLocation;
            if (plyr.priWeapon == itemRef)
                plyr.priWeapon = 255;
            if (plyr.secWeapon == itemRef)
                plyr.secWeapon = 255;
            if (plyr.armsArmour == itemRef)
                plyr.armsArmour = 255;
            if (plyr.legsArmour == itemRef)
                plyr.legsArmour = 255;
            if (plyr.headArmour == itemRef)
                plyr.headArmour = 255;
            if (plyr.bodyArmour == itemRef)
                plyr.bodyArmour = 255;
            if (plyr.clothing[0] == itemRef)
                plyr.clothing[0] = 255;
            if (plyr.clothing[1] == itemRef)
                plyr.clothing[1] = 255;
            if (plyr.clothing[2] == itemRef)
                plyr.clothing[2] = 255;
            if (plyr.clothing[3] == itemRef)
                plyr.clothing[3] = 255;
        }

        public static bool CheckForQuestItem ( int itemNo )
        {
            // checks through item buffer for carried quest items of type 200
            var response = false;
            var cur_idx = 0;
            while (cur_idx < plyr.buffer_index)
            {
                if ((itemBuffer[cur_idx].location == 10) && (itemBuffer[cur_idx].type == 200) && (itemBuffer[cur_idx].index == itemNo))
                    response = true;
                if ((itemBuffer[cur_idx].location == 10) && (itemBuffer[cur_idx].type == 201) && (itemBuffer[cur_idx].index == itemNo))
                    response = true;
                cur_idx++;
            }
            return response;
        }

        //TODO: Add to Items class
        public static int GetQuestItemRef ( int itemNo )
        {
            // checks through item buffer for carried quest items of type 200
            var response = 255; // 255 indicates no match
            var cur_idx = 0;
            while (cur_idx < plyr.buffer_index)
            {
                if ((itemBuffer[cur_idx].location == 10) && (itemBuffer[cur_idx].type == 200) && (itemBuffer[cur_idx].index == itemNo))
                    response = cur_idx;
                if ((itemBuffer[cur_idx].location == 10) && (itemBuffer[cur_idx].type == 201) && (itemBuffer[cur_idx].index == itemNo))
                    response = cur_idx;
                cur_idx++;
            }
            return response;
        }

        public static void GetItems ()
        {
            // types = 1 - food, 2 - water, 3 - torches, 4 - timepieces, 5 - compasses
            var cur_idx = 0;
            var noGetQuit = true;
            while ((cur_idx <= plyr.buffer_index) && (noGetQuit))
            {
                if ((itemBuffer[cur_idx].x == plyr.x) && (itemBuffer[cur_idx].y == plyr.y) && (itemBuffer[cur_idx].level == plyr.map) && (itemBuffer[cur_idx].location == 1))
                {
                    var keypressed = false;

                    while (!keypressed)
                    {
                        DispMain();
                        DrawConsoleBackground();
                        CyText(1, "GET?");

                        var str = "";
                        switch (itemBuffer[cur_idx].type)
                        {
                            case 1:
                            str = $"{itemBuffer[cur_idx].hp} Food Packet(s)";
                            break;
                            case 2:
                            str = $"{itemBuffer[cur_idx].hp} Water Flask(s)";
                            break;
                            case 3:
                            str = $"{itemBuffer[cur_idx].hp} Torch(es)";
                            break;
                            case 4:
                            str = $"{itemBuffer[cur_idx].hp} Timepiece(s)";
                            break;
                            case 5:
                            str = $"{itemBuffer[cur_idx].hp} Compass(es)";
                            break;
                            case 6:
                            str = $"{itemBuffer[cur_idx].hp} Key(s)";
                            break;
                            case 7:
                            str = $"{itemBuffer[cur_idx].hp} Crystal(s)";
                            break;
                            case 8:
                            str = $"{itemBuffer[cur_idx].hp} Gem(s)";
                            break;
                            case 9:
                            str = $"{itemBuffer[cur_idx].hp} Jewel(s)";
                            break;
                            case 10:
                            str = $"{itemBuffer[cur_idx].hp} Gold";
                            break;
                            case 11:
                            str = $"{itemBuffer[cur_idx].hp} Silver";
                            break;
                            case 12:
                            str = $"{itemBuffer[cur_idx].hp} Copper";
                            break;
                            case 176:
                            {
                                if (itemBuffer[cur_idx].hp == 0)
                                    str = "Potion";
                                else if (itemBuffer[cur_idx].hp == 1)
                                    str = Potions[(itemBuffer[cur_idx].index)].name;
                                break;
                            }
                            case 177:
                            str = itemBuffer[cur_idx].name;
                            break;
                            case 178:
                            str = itemBuffer[cur_idx].name;
                            break;
                            case 199:
                            str = itemBuffer[cur_idx].name;
                            break;
                            case 180:
                            str = itemBuffer[cur_idx].name;
                            break;
                            case 200:
                            str = questItems[(itemBuffer[cur_idx].index)].name;
                            break;
                        };
                        CyText(4, str);
                        CyText(7, "Yes, No or ESC.");
                        UpdateDisplay();

                        var key_value = GetSingleKey();

                        if (key_value == "Y")
                        {
                            var encText = CheckEncumbrance();
                            if (encText == "Immobilized!")
                                CannotCarryMessage();
                            else
                            {
                                // get food packets
                                if (itemBuffer[cur_idx].type < 13)
                                {
                                    switch (itemBuffer[cur_idx].type)
                                    {
                                        case 1:
                                        plyr.food += (itemBuffer[cur_idx].hp);
                                        break;
                                        case 2:
                                        plyr.water += (itemBuffer[cur_idx].hp);
                                        break;
                                        case 3:
                                        plyr.torches += (itemBuffer[cur_idx].hp);
                                        break;
                                        case 4:
                                        plyr.timepieces += (itemBuffer[cur_idx].hp);
                                        break;
                                        case 5:
                                        plyr.compasses += (itemBuffer[cur_idx].hp);
                                        break;
                                        case 6:
                                        plyr.keys += (itemBuffer[cur_idx].hp);
                                        break;
                                        case 7:
                                        plyr.crystals += (itemBuffer[cur_idx].hp);
                                        break;
                                        case 8:
                                        plyr.gems += (itemBuffer[cur_idx].hp);
                                        break;
                                        case 9:
                                        plyr.jewels += (itemBuffer[cur_idx].hp);
                                        break;
                                        case 10:
                                        plyr.gold += (itemBuffer[cur_idx].hp);
                                        break;
                                        case 11:
                                        plyr.silver += (itemBuffer[cur_idx].hp);
                                        break;
                                        case 12:
                                        plyr.copper += (itemBuffer[cur_idx].hp);
                                        break;
                                    };

                                    itemBuffer[cur_idx].location = 0; // remove this item to the void
                                } else if (itemBuffer[cur_idx].type > 150)
                                    itemBuffer[cur_idx].location = 10; // moved to player inventory
                            }
                            keypressed = true;
                        }

                        if (key_value == "N")
                            keypressed = true;
                        else if (key_value == "ESC")
                        {
                            keypressed = true;
                            noGetQuit = false;
                        }
                    }
                }
                cur_idx++;
            }
        }

        //TODO: Add to Items class
        public static int InputItemQuantity ( int selectItemMode )
        {
            var itemQuantity = 0;
            var inputText = "";
            var maxNumberSize = 6;
            var enterKeyNotPressed = true;
            while (enterKeyNotPressed)
            {
                switch (plyr.status)
                {
                    case GameStates.Module:
                    ClearShopDisplay();
                    break;
                    case GameStates.Encounter:
                    DrawEncounterView();
                    break;

                    default:
                    DispMain();
                    DrawConsoleBackground();
                    break;
                };

                switch (selectItemMode)
                {
                    case 2:
                    CyText(2, "Drop how many?");
                    break;
                    case 3:
                    CyText(2, "Offer how many?");
                    break;
                    case 4:
                    CyText(2, "Deposit how many?");
                    break;
                };

                BText(10, 5, $">{inputText}_");
                BText(10, 9, "Enter amount or press ESC.");
                UpdateDisplay();
                var key = GetSingleKey();
                if ((key == "0") || (key == "1") || (key == "2") || (key == "3") || (key == "4") || (key == "5") || (key == "6") || (key == "7") || (key == "8") || (key == "9"))
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
                if (key == "ESC")
                {
                    itemQuantity = 0;
                    enterKeyNotPressed = false;
                }
            }
            //TODO: What happens on RETURN/ESC
            itemQuantity = Convert.ToInt32(inputText);

            return itemQuantity;
        }

        //TODO: Item class?
        public static int SelectItem ( SelectStates state ) => SelectItem((int)state);

        //TODO: Item class?
        public static int SelectItem ( int selectItemMode )
        {
            // 1 - USE, 2 - DROP, 3 - OFFER, 4 - Deposit
            // item types : 1-weapon 177-armour
            var itemRef = 9999; // Nothing selected

            var selectDesc = "";
            if (selectItemMode == 1)
                selectDesc = "USE";
            if (selectItemMode == 2)
                selectDesc = "DROP";
            if (selectItemMode == 3)
                selectDesc = "OFFER";
            if (selectItemMode == 4)
                selectDesc = "Deposit";
            if (selectItemMode == 5)
                selectDesc = "Withdrawal";

            var selectDone = false;

            var no_items = 0;
            var cur_idx = 0;
            var page = 0;

            while (cur_idx < plyr.buffer_index)
            {
                if (itemBuffer[cur_idx].location == 10)
                    no_items++;
                cur_idx++;
            }

            var noPages = no_items / 4; // based on 4 on screen items per page
            var pages = noPages;
            var tempRemainder = no_items % 4;
            if (tempRemainder != 0)
                pages++;

            while (!selectDone)
            {
                if (page == 0)
                {
                    // this is effectively page 0 in terms of using items
                    switch (plyr.status)
                    {
                        case GameStates.Module:
                        ClearGuildDisplay();
                        break;
                        case GameStates.Encounter:
                        DrawEncounterView();
                        break;
                        case GameStates.Explore:
                        DispMain();
                        DrawConsoleBackground();
                        break;

                        //TODO: What state is this?
                        case (GameStates)0:
                        DispMain();
                        DrawConsoleBackground();
                        break;
                    };

                    CyText(1, selectDesc);
                    var str = $"(1) Food Packets: {plyr.food}";
                    if (selectItemMode == 5)
                        str = $"(1) Food Packets: {plyr.lfood}";
                    BText(5, 3, str);

                    str = $"(2) Water Flasks: {plyr.water}";
                    if (selectItemMode == 5)
                        str = $"(2) Water Flasks: {plyr.lwater}";
                    BText(5, 4, str);

                    str = $"(3) Unlit Torches: {plyr.torches}";
                    if (selectItemMode == 5)
                        str = $"(3) Unlit Torches: {plyr.ltorches}";
                    BText(5, 5, str);

                    str = $"(4) Timepieces: {plyr.timepieces}";
                    if (selectItemMode == 5)
                        str = $"(4) Timepieces: {plyr.ltimepieces}";
                    BText(5, 6, str);

                    BText(2, 8, "Item #, Forward, Back, or ESC to exit");
                    SetFontColor(40, 96, 244, 255);
                    BText(2, 8, "     #  F        B        ESC");
                    SetFontColor(215, 215, 215, 255);

                    UpdateDisplay();

                    var key_value = GetSingleKey();
                    switch (key_value)
                    {
                        case "1":
                        {
                            itemRef = 1000;
                            selectDone = true;
                            break;
                        };
                        case "2":
                        {
                            itemRef = 1001;
                            selectDone = true;
                            break;
                        };
                        case "3":
                        {
                            itemRef = 1002;
                            selectDone = true;
                            break;
                        };
                        case "4":
                        {
                            itemRef = 1003;
                            selectDone = true;
                            break;
                        }
                        case "E":
                        selectDone = true;
                        break;
                        case "ESC":
                        selectDone = true;
                        break;

                        case "F":
                        case "down":
                        {
                            if ((selectItemMode == 1) && (pages > 0))
                                page = 3;
                            if ((selectItemMode == 2) || (selectItemMode == 3) || (selectItemMode == 4) || (selectItemMode == 5))
                                page = 1;
                            break;
                        };
                    };
                } else if (page == 1)
                {
                    // this is effectively page 1 in terms of using items
                    switch (plyr.status)
                    {
                        case GameStates.Module:
                        ClearGuildDisplay();
                        break;
                        case GameStates.Encounter:
                        DrawEncounterView();
                        break;
                        case GameStates.Explore:
                        DispMain();
                        DrawConsoleBackground();
                        break;

                        //TODO: What state is this?
                        case (GameStates)0:
                        DispMain();
                        DrawConsoleBackground();
                        break;
                    };

                    CyText(1, selectDesc);
                    var str = $"(1) Compasses: {plyr.compasses}";
                    if (selectItemMode == 5)
                        str = $"(1) Compasses: {plyr.lcompasses}";
                    BText(5, 3, str);
                    str = $"(2) Keys: {plyr.keys}";
                    if (selectItemMode == 5)
                        str = $"(2) Keys: {plyr.lkeys}";
                    BText(5, 4, str);
                    str = $"(3) Crystals: {plyr.crystals}";
                    if (selectItemMode == 5)
                        str = $"(3) Crystals: {plyr.lcrystals}";
                    BText(5, 5, str);
                    str = $"(4) Gems: {plyr.gems}";
                    if (selectItemMode == 5)
                        str = $"(4) Gems: {plyr.lgems}";
                    BText(5, 6, str);
                    BText(2, 8, "Item #, Forward, Back, or ESC to exit");
                    SetFontColor(40, 96, 244, 255);
                    BText(2, 8, "     #  F        B        ESC");
                    SetFontColor(215, 215, 215, 255);

                    UpdateDisplay();

                    var key_value = GetSingleKey();
                    switch (key_value)
                    {
                        case "1":
                        {
                            itemRef = 1004;
                            selectDone = true;
                            break;
                        };
                        case "2":
                        {
                            itemRef = 1005;
                            selectDone = true;
                            break;
                        };
                        case "3":
                        {
                            itemRef = 1006;
                            selectDone = true;
                            break;
                        };
                        case "4":
                        {
                            itemRef = 1007;
                            selectDone = true;
                            break;
                        };
                        case "E":
                        case "ESC":
                        selectDone = true;
                        break;

                        case "F":
                        case "down":
                        page = 2;
                        break;

                        case "B":
                        case "up":
                        page = 0;
                        break;
                    };
                } else if (page == 2)
                {
                    // this is effectively page 2 in terms of using items
                    switch (plyr.status)
                    {
                        case GameStates.Module:
                        ClearGuildDisplay();
                        break;
                        case GameStates.Encounter:
                        DrawEncounterView();
                        break;
                        case GameStates.Explore:
                        DispMain();
                        DrawConsoleBackground();
                        break;

                        //TODO: What state is this?
                        case (GameStates)0:
                        DispMain();
                        DrawConsoleBackground();
                        break;
                    }

                    CyText(1, selectDesc);
                    var str = $"(1) Jewels: {plyr.jewels}";
                    if (selectItemMode == 5)
                        str = $"(1) Jewels: {plyr.ljewels}";
                    BText(5, 3, str);
                    str = $"(2) Gold: {plyr.gold}";
                    if (selectItemMode == 5)
                        str = $"(2) Gold: {plyr.lgold}";
                    BText(5, 4, str);
                    str = $"(3) Silver: {plyr.silver}";
                    if (selectItemMode == 5)
                        str = $"(3) Silver: {plyr.lsilver}";
                    BText(5, 5, str);
                    str = $"(4) Copper: {plyr.copper}";
                    if (selectItemMode == 5)
                        str = $"(4) Copper: {plyr.lcopper}";
                    BText(5, 6, str);
                    BText(2, 8, "Item #, Forward, Back, or ESC to exit");
                    SetFontColor(40, 96, 244, 255);
                    BText(2, 8, "     #  F        B        ESC");
                    SetFontColor(215, 215, 215, 255);
                    UpdateDisplay();

                    switch (GetSingleKey())
                    {
                        case "1":
                        {
                            itemRef = 1008;
                            selectDone = true;
                            break;
                        };
                        case "2":
                        {
                            itemRef = 1009;
                            selectDone = true;
                            break;
                        };
                        case "3":
                        {
                            itemRef = 1010;
                            selectDone = true;
                            break;
                        };
                        case "4":
                        {
                            itemRef = 1011;
                            selectDone = true;
                            break;
                        };

                        case "E":
                        case "ESC":
                        selectDone = true;
                        break;

                        case "F":
                        case "down":
                        {
                            if ((pages > 0) && (selectItemMode < 4))
                                page = 3;
                            break;
                        };

                        case "B":
                        case "up":
                        page = 1;
                        break;
                    };
                } else if (page > 2) // Variable items
                {
                    var keypressed = false;
                    while (!keypressed)
                    {
                        switch (plyr.status)
                        {
                            case GameStates.Module:
                            ClearGuildDisplay();
                            break;
                            case GameStates.Encounter:
                            DrawEncounterView();
                            break;
                            case GameStates.Explore:
                            DispMain();
                            DrawConsoleBackground();
                            break;

                            //TODO: What state is this?
                            case (GameStates)0:
                            DispMain();
                            DrawConsoleBackground();
                            break;
                        };

                        CyText(1, selectDesc);
                        BText(5, 3, "(1)");
                        BText(5, 4, "(2)");
                        BText(5, 5, "(3)");
                        BText(5, 6, "(4)");
                        BText(2, 8, "Item #, Forward, Back, or ESC to exit");
                        SetFontColor(40, 96, 244, 255);
                        BText(2, 8, "     #  F        B        ESC");
                        SetFontColor(215, 215, 215, 255);

                        // Identify starting value for cur_idx for page x of carried objects excluding items not carried
                        var mypage = page-3; // should be 0 for page 0 (first page with items displayed on it)
                        cur_idx = 0;
                        if (mypage > 0)
                        {
                            var idx = 0;

                            var tempPage = 0;
                            var pageItems = 0;
                            while ((mypage > tempPage) && (idx <= plyr.buffer_index))
                            {
                                if (itemBuffer[idx].location == 10)
                                {
                                    cur_idx++;
                                    pageItems++;
                                    if (pageItems == 4)
                                    {
                                        tempPage++;
                                        pageItems = 0;
                                    }
                                }
                                if (itemBuffer[idx].location != 10)
                                    cur_idx++;
                                idx++;
                            }
                        }

                        var page_item = 1;
                        var menuitem1 = 9999; // 9999 is used as nil
                        var menuitem2 = 9999;
                        var menuitem3 = 9999;
                        var menuitem4 = 9999;

                        while ((cur_idx < plyr.buffer_index) && (page_item < 5))
                        {
                            var str = "";
                            if ((itemBuffer[cur_idx].location == 10))
                            {
                                // Display a Potion item
                                if (itemBuffer[cur_idx].type == 176) // armour
                                {
                                    var potion = itemBuffer[cur_idx].index;
                                    if (itemBuffer[cur_idx].hp == 0)
                                        str = "Potion";
                                    if (itemBuffer[cur_idx].hp == 1)
                                        str = Potions[potion].name;
                                }

                                // Display an armour, weapon or clothing item name
                                if (itemBuffer[cur_idx].type == 177)
                                    str = itemBuffer[cur_idx].name;
                                if (itemBuffer[cur_idx].type == 178)
                                    str = itemBuffer[cur_idx].name;
                                if (itemBuffer[cur_idx].type == 180)
                                    str = itemBuffer[cur_idx].name;
                                if (itemBuffer[cur_idx].type == 199)
                                    str = itemBuffer[cur_idx].name;

                                // Display quest items in inventory
                                if (itemBuffer[cur_idx].type == 200)
                                    str = questItems[(itemBuffer[cur_idx].index)].name;

                                // Display guild ring name and charges
                                if (itemBuffer[cur_idx].type == 201)
                                    //str = questItems[(itemBuffer[cur_idx].index)].name;
                                    str = (questItems[(itemBuffer[cur_idx].index)].name) + $" [{plyr.ringCharges}]";

                                // Indicate items worn or in use as primary or secondary weapons
                                if (cur_idx == plyr.priWeapon)
                                    BText(4, (page_item + 2), "*");
                                if (cur_idx == plyr.secWeapon)
                                    BText(4, (page_item + 2), "*");
                                if (cur_idx == plyr.headArmour)
                                    BText(4, (page_item + 2), "*");
                                if (cur_idx == plyr.bodyArmour)
                                    BText(4, (page_item + 2), "*");
                                if (cur_idx == plyr.armsArmour)
                                    BText(4, (page_item + 2), "*");
                                if (cur_idx == plyr.legsArmour)
                                    BText(4, (page_item + 2), "*");
                                if (cur_idx == plyr.clothing[0])
                                    BText(4, (page_item + 2), "*");
                                if (cur_idx == plyr.clothing[1])
                                    BText(4, (page_item + 2), "*");
                                if (cur_idx == plyr.clothing[2])
                                    BText(4, (page_item + 2), "*");
                                if (cur_idx == plyr.clothing[3])
                                    BText(4, (page_item + 2), "*");

                                BText(9, (page_item + 2), str);
                                switch (page_item)
                                {
                                    case 1:
                                    menuitem1 = cur_idx;
                                    break;
                                    case 2:
                                    menuitem2 = cur_idx;
                                    break;
                                    case 3:
                                    menuitem3 = cur_idx;
                                    break;
                                    case 4:
                                    menuitem4 = cur_idx;
                                    break;
                                }
                                page_item++;
                            }
                            cur_idx++;
                        }
                        UpdateDisplay();

                        switch (GetSingleKey())
                        {
                            case "1":
                            {
                                if (menuitem1 != 9999)
                                {
                                    itemRef = menuitem1;
                                    keypressed = true;
                                    selectDone = true;
                                };
                                break;
                            };
                            case "2":
                            {
                                if (menuitem2 != 9999)
                                {
                                    itemRef = menuitem2;
                                    keypressed = true;
                                    selectDone = true;
                                };
                                break;
                            };
                            case "3":
                            {
                                if (menuitem3 != 9999)
                                {
                                    itemRef = menuitem3;
                                    keypressed = true;
                                    selectDone = true;
                                };
                                break;
                            };
                            case "4":
                            {
                                if (menuitem4 != 9999)
                                {
                                    itemRef = menuitem4;
                                    keypressed = true;
                                    selectDone = true;
                                };
                                break;
                            };

                            case "ESC":
                            {
                                keypressed = true;
                                selectDone = true;
                                break;
                            };

                            case "B":
                            {
                                if ((selectItemMode == 1) && (page == 3))
                                {
                                    keypressed = true;
                                    page = 0;
                                } else if ((selectItemMode == 2) && (page == 3))
                                {
                                    keypressed = true;
                                    page = 2;
                                } else if ((selectItemMode == 3) && (page == 3))
                                {
                                    keypressed = true;
                                    page = 2;
                                } else if (page > 3)
                                {
                                    keypressed = true;
                                    page--;
                                };
                                break;
                            };
                            case "up":
                            {
                                if (page > 3)
                                {
                                    keypressed = true;
                                    page--;
                                };
                                break;
                            };
                            case "F":
                            case "down":
                            {
                                if (pages > (page-2))
                                {
                                    keypressed = true;
                                    page++;
                                };
                                break;
                            };
                        };
                    }
                }
            }

            // END - SELECT DONE LOOP

            if ((itemRef != 9999) && (selectItemMode != 3))
                DetermineItemAction(selectItemMode, itemRef); // Pass on mode and index for buffer items only if something selected
            if ((itemRef != 9999) && (selectItemMode == 3))
                return itemRef;
            if ((itemRef == 9999) && (selectItemMode == 3))
                return 9999;

            //MLT: What to return?
            return 9999;
        }

        public static void CheckForItemsHere ()
        {
            // counts number of objects on a map square - equal to 1
            var no_items = 0;
            var cur_idx = 0;
            while (cur_idx < plyr.buffer_index)
            {
                if ((itemBuffer[cur_idx].x == plyr.x) && (itemBuffer[cur_idx].y == plyr.y) && (itemBuffer[cur_idx].level == plyr.map) && (itemBuffer[cur_idx].location == 1))
                    no_items++;
                cur_idx++;
            }

            if (no_items == 1)
                plyr.status_text = "There is something here.";
            if (no_items > 1)
                plyr.status_text = "There are several things here.";
        }

        public static int InputDepositQuantity ( int itemRef )
        {
            var itemQuantity = 0;
            var inputText = "";
            var maxNumberSize = 6;
            var enterKeyNotPressed = true;
            while (enterKeyNotPressed)
            {
                CyText(2, "It costs a silver per unit. How many?");

                var str = "";
                ClearGuildDisplay();
                if (itemRef == 1000)
                    CyText(2, "Deposit how many Food Packets?");
                if (itemRef == 1001)
                    CyText(2, "Deposit how many Water Flasks?");
                if (itemRef == 1002)
                    CyText(2, "Deposit how many Unlit Torches?");
                if (itemRef == 1003)
                    CyText(2, "Deposit how many Timepieces?");
                if (itemRef == 1004)
                    CyText(2, "Deposit how many Compasses?");
                if (itemRef == 1005)
                    CyText(2, "Deposit how many Keys?");
                if (itemRef == 1006)
                    CyText(2, "Deposit how many Crystals?");
                if (itemRef == 1007)
                    CyText(2, "Deposit how many Gems?");
                if (itemRef == 1008)
                    CyText(2, "Deposit how many Jewels?");
                if (itemRef == 1009)
                    CyText(2, "Deposit how much Gold?");
                if (itemRef == 1010)
                    CyText(2, "Deposit how much Silver?");
                if (itemRef == 1011)
                    CyText(2, "Deposit how much Copper?");
                if (itemRef == 1012)
                    CyText(2, "It costs a silver per unit. How many?");
                if (itemRef == 1000)
                    str = $"(up to {plyr.food})";
                if (itemRef == 1001)
                    str = $"(up to {plyr.water})";
                if (itemRef == 1002)
                    str = $"(up to {plyr.torches})";
                if (itemRef == 1003)
                    str = $"(up to {plyr.timepieces})";
                if (itemRef == 1004)
                    str = $"(up to {plyr.compasses})";
                if (itemRef == 1005)
                    str = $"(up to {plyr.keys})";
                if (itemRef == 1006)
                    str = $"(up to {plyr.crystals})";
                if (itemRef == 1007)
                    str = $"(up to {plyr.gems})";
                if (itemRef == 1008)
                    str = $"(up to {plyr.jewels})";
                if (itemRef == 1009)
                    str = $"(up to {plyr.gold})";
                if (itemRef == 1010)
                    str = $"(up to {plyr.silver})";
                if (itemRef == 1011)
                    str = $"(up to {plyr.copper})";
                if (itemRef == 1012)
                    str = $"(up to {99 - plyr.ringCharges})";
                CyText(3, str);

                str = ">" + inputText + "_";
                BText(10, 5, str);
                BText(10, 9, "Enter amount or press ESC.");
                UpdateDisplay();
                var key = GetSingleKey();
                if ((key == "0") || (key == "1") || (key == "2") || (key == "3") || (key == "4") || (key == "5") || (key == "6") || (key == "7") || (key == "8") || (key == "9"))
                {
                    int numberLength = inputText.Length;
                    if (numberLength < maxNumberSize)
                        inputText = inputText + key;
                }
                if (key == "BACKSPACE")
                {
                    int numberLength = inputText.Length;
                    if (numberLength != 0)
                    {
                        inputText = inputText.Substring(0, (numberLength - 1));
                    }
                }
                if (key == "RETURN")
                    enterKeyNotPressed = false;
                if (key == "ESC")
                {
                    itemQuantity = 0;
                    enterKeyNotPressed = false;
                }
            }

            //TODO: Does this work with RETURN or ESC?
            itemQuantity = Convert.ToInt32(inputText);

            return itemQuantity;
        }
        public static int InputWithdrawalQuantity ( int itemRef )
        {
            var itemQuantity = 0;

            var inputText = "";
            var maxNumberSize = 6;
            var enterKeyNotPressed = true;
            while (enterKeyNotPressed)
            {
                var str = "";

                ClearGuildDisplay();
                if (itemRef == 1000)
                    CyText(2, "Withdraw how many Food Packets?");
                if (itemRef == 1001)
                    CyText(2, "Withdraw how many Water Flasks?");
                if (itemRef == 1002)
                    CyText(2, "Withdraw how many Unlit Torches?");
                if (itemRef == 1003)
                    CyText(2, "Withdraw how many Timepieces?");
                if (itemRef == 1004)
                    CyText(2, "Withdraw how many Compasses?");
                if (itemRef == 1005)
                    CyText(2, "Withdraw how many Keys?");
                if (itemRef == 1006)
                    CyText(2, "Withdraw how many Crystals?");
                if (itemRef == 1007)
                    CyText(2, "Withdraw how many Gems?");
                if (itemRef == 1008)
                    CyText(2, "Withdraw how many Jewels?");
                if (itemRef == 1009)
                    CyText(2, "Withdraw how much Gold?");
                if (itemRef == 1010)
                    CyText(2, "Withdraw how much Silver?");
                if (itemRef == 1011)
                    CyText(2, "Withdraw how much Copper?");
                if (itemRef == 1000)
                    str = $"(up to {plyr.lfood})";
                if (itemRef == 1001)
                    str = $"(up to {plyr.lwater})";
                if (itemRef == 1002)
                    str = $"(up to {plyr.ltorches})";
                if (itemRef == 1003)
                    str = $"(up to {plyr.ltimepieces})";
                if (itemRef == 1004)
                    str = $"(up to {plyr.lcompasses})";
                if (itemRef == 1005)
                    str = $"(up to {plyr.lkeys})";
                if (itemRef == 1006)
                    str = $"(up to {plyr.lcrystals})";
                if (itemRef == 1007)
                    str = $"(up to {plyr.lgems})";
                if (itemRef == 1008)
                    str = $"(up to {plyr.ljewels})";
                if (itemRef == 1009)
                    str = $"(up to {plyr.lgold})";
                if (itemRef == 1010)
                    str = $"(up to {plyr.lsilver})";
                if (itemRef == 1011)
                    str = $"(up to {plyr.lcopper})";
                CyText(3, str);

                str = $">{inputText}_";
                BText(10, 5, str);
                BText(10, 9, "Enter amount or press ESC.");
                UpdateDisplay();
                var key = GetSingleKey();
                if ((key == "0") || (key == "1") || (key == "2") || (key == "3") || (key == "4") || (key == "5") || (key == "6") || (key == "7") || (key == "8") || (key == "9"))
                {
                    int numberLength = inputText.Length;
                    if (numberLength < maxNumberSize)
                        inputText = inputText + key;
                }
                if (key == "BACKSPACE")
                {
                    int numberLength = inputText.Length;
                    if (numberLength != 0)
                    {
                        inputText = inputText.Substring(0, (numberLength - 1));
                    }
                }
                if (key == "RETURN")
                    enterKeyNotPressed = false;
                if (key == "ESC")
                {
                    itemQuantity = 0;
                    enterKeyNotPressed = false;
                }
            }

            //TODO: Does this work with RETURN or ESC?
            itemQuantity = Convert.ToInt32(inputText);

            return itemQuantity;
        }

        public static void DisplayObjectBuffer ()
        {
            var keypressed = false;
            var oldStatus = plyr.status;

            //TODO: What does state 255 meanu?
            plyr.status = (GameStates)255; // Diag screen being displayed

            while (!keypressed)
            {
                ClearDisplay();

                Text(0, 2, "No");
                Text(4, 2, "Typ ");
                Text(8, 2, "Loc");
                Text(12, 2, "X");
                Text(15, 2, "Y");
                Text(18, 2, "L");
                Text(20, 2, "Item");

                Text(0, 0, $"Buffer Index {plyr.buffer_index} of {itemBufferSize}");

                var cur_idx = 0;
                while (cur_idx < plyr.buffer_index)
                {
                    var str = "";

                    Text(0, (cur_idx + 3), cur_idx);
                    Text(4, (cur_idx + 3), itemBuffer[cur_idx].type);
                    Text(8, (cur_idx + 3), itemBuffer[cur_idx].location);
                    Text(12, (cur_idx + 3), itemBuffer[cur_idx].x);
                    Text(15, (cur_idx + 3), itemBuffer[cur_idx].y);
                    Text(18, (cur_idx + 3), itemBuffer[cur_idx].level);
                    if (itemBuffer[cur_idx].type < 20)
                        str = "Volume item";
                    if (itemBuffer[cur_idx].type == 178)
                        str = itemBuffer[cur_idx].name;
                    if (itemBuffer[cur_idx].type == 177)
                        str = itemBuffer[cur_idx].name;
                    if ((itemBuffer[cur_idx].type == 176) && (itemBuffer[cur_idx].hp == 0))
                        str = "Potion";
                    if ((itemBuffer[cur_idx].type == 176) && (itemBuffer[cur_idx].hp == 1))
                        str = Potions[(itemBuffer[cur_idx].index)].name;
                    if (itemBuffer[cur_idx].type == 180)
                        str = itemBuffer[cur_idx].name;
                    if (itemBuffer[cur_idx].type == 199)
                        str = itemBuffer[cur_idx].name;
                    if (itemBuffer[cur_idx].type == 200)
                        str = questItems[(itemBuffer[cur_idx].index)].name;
                    if (itemBuffer[cur_idx].type == 201)
                        str = questItems[(itemBuffer[cur_idx].index)].name;
                    Text(20, (cur_idx + 3), str);

                    cur_idx++;
                }
                Text(0, (cur_idx + 4), "(Press SPACE to continue)");
                UpdateDisplay();

                var key_value = GetSingleKey();

                if ((key_value == "SPACE") || (key_value == "B"))
                    keypressed = true;
            }
            plyr.status = oldStatus;

        }

        public static void TidyObjectBuffer ()
        {
            // copy itemBuffer[250] to tempBuffer[250] before starting to reorganise entries
            // Maximum of 250 objects in play at the same time
            // 0 - limbo, 1 - floor, 10 - carried, 11 - primary, 12 - secondary,
            // 13 - worn, 14 - head, 15 - arms, 16 - legs, 17 - body

            var bufferSafetyCheckIndex = (itemBufferSize-3);

            if (plyr.buffer_index > bufferSafetyCheckIndex)
            {
                var idx = plyr.buffer_index; // Number of items in play +1
                var newIndex = 0;
                for (var i = 0; i < idx; i++)
                    tempBuffer[i] = itemBuffer[i];

                for (var i = 0; i < idx; i++)
                {
                    if (tempBuffer[i].location == 10)
                    {
                        itemBuffer[newIndex] = tempBuffer[i];
                        if (plyr.priWeapon == i)
                            plyr.priWeapon = newIndex;
                        if (plyr.secWeapon == i)
                            plyr.secWeapon = newIndex;
                        if (plyr.headArmour == i)
                            plyr.headArmour = newIndex;
                        if (plyr.bodyArmour == i)
                            plyr.bodyArmour = newIndex;
                        if (plyr.armsArmour == i)
                            plyr.armsArmour = newIndex;
                        if (plyr.legsArmour == i)
                            plyr.legsArmour = newIndex;
                        // worn clothing?
                        newIndex++;
                    }

                }

                // Reset the buffer index value
                plyr.buffer_index = newIndex;
            }
        }

        public static int ReturnCarriedWeight ()
        {
            //TODO: This does nothing
            var itemWeight = 0;

            var gold = plyr.gold / 16;
            var silver = plyr.silver / 16;
            var copper = plyr.copper / 16;
            var torches = plyr.torches / 16;
            var flasks = plyr.water / 16;
            var food = plyr.food / 16;
            var crystals = plyr.crystals / 16;
            var keys = plyr.keys / 16;
            var gems = plyr.gems / 16;
            var jewels = plyr.jewels / 16;
            var timepieces = plyr.timepieces / 16;
            var compasses = plyr.compasses / 16;
            var carriedWeight = gold + silver + copper + torches + flasks + food + crystals + keys + gems + jewels + timepieces + compasses;
            var idx = plyr.buffer_index;
            for (var i = 0; i < idx; i++)
            {
                if (itemBuffer[i].location == 10)
                {
                    if (itemBuffer[i].type == 177)
                        itemWeight = itemBuffer[i].weight;
                    if (itemBuffer[i].type == 178)
                        itemWeight = itemBuffer[i].weight;
                    if (itemBuffer[i].type == 180)
                        itemWeight = itemBuffer[i].weight;
                    itemWeight = 0;
                }
            }
            return carriedWeight;
        }

        #region Review Data

        public static readonly int itemBufferSize = 250;

        public static readonly int dungeonItemsSize = 10496;

        public static BufferItem[] itemBuffer = Arrays.InitializeWithDefaultInstances<BufferItem>(itemBufferSize); // Items on floor,carried and in void - now 250

        public static byte[] dungeonItems = new byte[dungeonItemsSize];

        public static int[] itemOffsets = new int[141] {
            742, // THE_STAR_CARD
            782, // THE_FOOL_CARD
            854, // THE_HEIROPHANT_CARD
            900, // DEATH_CARD
            937, // ACE_OF_CUPS
            973, // THE_CHARIOT_CARD
            1016, // STRENGTH_CARD
            1056, // ACE_OF_WANDS
            1109, // TEMPERANCE_CARD
            1151, // KING_OF_WANDS
            1221, // PAGE_OF_CUPS_CARD
            1281, // ACE_OF_PENTACLES
            1322, // HIGH_PRIESTESS_CARD
            1400, // COLD_WAND
            1433, // FIRE_WAND
            1466, // PARALYSIS_WAND
            1504, // EYE_OF_VULNERABILTY
            1548, // LIGHT_WAND
            1614, // HEALING_WAND
            1650, // FROSTBLADE_SCROLL
            1724, // FIREBLADE_SCROLL
            1797, // CLOUT_SCROLL
            1866, // RENEW_SCROLL
            1890, // REMOVE_CURSE_SCROLL
            1921, // WIZARD_EYE_SCROLL
            2027, // RUBY_EYE
            2060, // EMERALD_EYE
            2096, // SAPPHIRE_EYE
            2133, // AMBER_EYE
            2167, // WIZARD'S_EYE
            2236, // HYPNOTIC_EYE
            2273, // TOME_OF_KNOWLEDGE
            2315, // TOME_OF_UNDERSTANDING
            2361, // TOME_OF_LEADERSHIP
            2404, // BRONZE_HORN
            2440, // SILVER_HORN
            2476, // GOLD_HORN
            2510, // GOLD_HORN
            2544, // POTION_OF_FLEETNESS
            2619, // POTION_OF_STRENGTH
            2693, // POTION_OF_INTELLIGENCE
            2771, // POTION_OF_CHARISMA
            2845, // POTION_OF_ENDURANCE
            2888, // POTION_OF_INV._BLUNT
            2964, // POTION_OF_INV._SHARP
            3040, // POTION_OF_INV._EARTH
            3116, // POTION_OF_INV._AIR
            3190, // POTION_OF_INV._FIRE
            3265, // POTION_OF_INV._WATER
            3341, // POTION_OF_REGENERATION
            3403, // POTION_OF_INV._MENTAL
            3480, // POTION_OF_INV._COLD
            3555, // POTION_OF_FRUIT_JUICE
            3600, // POTION_OF_WIZARD_EYE
            3708, // POTION_OF_DEXTERITY
            3783, // POTION_OF_INFRA-VISION
            3861, // POTION_OF_CLEANSING
            3889, // POTION_OF_ANTIDOTE
            3916, // POTION_OF_RESTORATION
            3961, // POTION_OF_HEALING
            4002, // POTION_OF_HEMLOCK
            4059, // POTION_OF_INEBRIATION
            4104, // CRYSTAL_SHIELD
            4177, // SHIELD_OF_GALAHAD
            4269, // SPIKED_SHIELD
            4309, // SHIELD_OF_MORDRED
            4401, // SPIRIT_SHIELD
            4473, // IRONWOOD_BOKEN
            4546, // IRON_FAN
            4581, // TOWER_SHIELD
            4620, // CROSSBOW_[03]
            4660, // QUARRELS_[10]
            4694, // CHAOS_CLUB
            4747, // SHORT_SWORD
            4785, // HOLY_HAND_GRENADE
            4859, // PIKE
            4890, // DIRK
            4921, // PANTHER_GLOVES
            4989, // HELM_OF_LIGHT
            5056, // DRAGONSKIN_HAUBERK
            5096, // GOLDEN_GREAVES
            5164, // PLATE_HELM
            5196, // PLATE_GAUNTLETS
            5233, // PLATE_LEGGINGS
            5269, // PLATE_ARMOR
            5302, // SCALE_ARMOR
            5335, // TRUESILVER_HELM
            5372, // TRUESILVER_COAT
            5409, // TRUESILVER_GUANTLETS
            5451, // TRUESILVER_LEGGINGS
            5492, // CUIRBOUILLI_HELM
            5530, // BRONZE_BREASTPLATE
            5570, // BRONZE_BRACERS
            5606, // WHITE_LINEN_SHIRT
            5633, // BLACK_SILK_KIMONO
            5660, // CHEAP_ROBE
            5680, // ELVEN_CLOAK
            5765, // ELVEN_BOOTS
            5818, // CRYSTAL_BELT
            5872, // BLUE_SUEDE_SHOES
            5898, // BLACK_WOOLEN_BREECHES
            5929, // SILVER_BROCADED_BODICE
            5961, // RED_PLAID_KILT
            5985, // GOLD_SILK_PANTALOONS
            6015, // LEATHER_JERKIN
            6039, // FLOPPY_LEATHER_HAT
            6067, // BLACK_COTTON_PARTLET
            6097, // SILVER_SASH
            6150, // STEALTH_SUIT
            6204, // SILVER_KEY
            6224, // GOBLIN_RING_HALF
            6250, // TROLL_RING_HALF
            6275, // STAFF_PIECE
            6296, // PAC_CARD
            6330, // MIRRORED_SHIELD
            6404, // REFORGED_RING
            6475, // BLOODSTONE
            6511, // WINGED_SANDALS
            6567, // MORGANA'S_TIARA
            6608, // CLOAK_OF_LEVITATION
            6669, // CRYSTAL_BREASTPLATE
            6710, // JUNAI'S_SWORD
            6766, // LOADSTONE
            6799, // IRON_PALM_SALVE
            6839, // SWORD_OF_THE_ADEPT
            6884, // RAZOR_ICE
            6920, // WHETSTONE
            6941, // SAURIAN_BRANDY
            6996, // BLUE_PEARL_DAGGER
            7040, // SIX_PACK_[6]
            7093, // MELVIN'S_HELM
            7128, // AMETHYST_ROD
            7182, // MAP_STONE
            7203, // FLAME_QUARRELS_[10]
            7243, // THUNDER_QUARRELS_[10]
            7285, // STAFF_OF_AMBER
            7326, // ROBIN'S_HOOD
            7380, // GOLDEN_APPLE
            7434, // GAUSS_RIFLE_[95]
            7477, // SOLAR_SUIT
            7541  // BEAM_WEAPON_[25]
        }; // Dungeon items only

        public static BufferItem[] tempBuffer = Arrays.InitializeWithDefaultInstances<BufferItem>(itemBufferSize); // Temp buffer for rebuilding new object buffer when bufferIndex reaches 99

        public static ClothingItem[] clothingItems =
        {
            new ClothingItem() { name = "Cheap Robe", quality = 0, colour = 0, fabric = 0, type = 0, weight = 4 },
            new ClothingItem() { name = "Green Cap with Feather", quality = 0, colour = 0, fabric = 0, type = 0, weight = 4 },
            new ClothingItem() { name = "Floppy Leather Hat", quality = 0, colour = 0, fabric = 0, type = 0, weight = 4 },
            new ClothingItem() { name = "Leather Sandals", quality = 0, colour = 0, fabric = 0, type = 0, weight = 4 },
            new ClothingItem() { name = "High Leather Boots", quality = 0, colour = 0, fabric = 0, type = 0, weight = 4 },
            new ClothingItem() { name = "Showshoes", quality = 0, colour = 0, fabric = 0, type = 0, weight = 4 },
            new ClothingItem() { name = "White Cotton Robe", quality = 0, colour = 0, fabric = 0, type = 0, weight = 4 },
            new ClothingItem() { name = "White Cotton Tunic", quality = 0, colour = 0, fabric = 0, type = 0, weight = 4 },
            new ClothingItem() { name = "Brown Cotton Breeches", quality = 0, colour = 0, fabric = 0, type = 0, weight = 4 },
            new ClothingItem() { name = "Green Cotton Skirt", quality = 0, colour = 0, fabric = 0, type = 0, weight = 4 },
            new ClothingItem() { name = "Purple Flowing Cape", quality = 0, colour = 0, fabric = 0, type = 0, weight = 4 },
            new ClothingItem() { name = "Blue Woolen Sweater", quality = 0, colour = 0, fabric = 0, type = 0, weight = 4 },
            new ClothingItem() { name = "Heavy Leather Jacket", quality = 0, colour = 0, fabric = 0, type = 0, weight = 6 },
            new ClothingItem() { name = "Fine Yellow Wool Pants", quality = 283, colour = 0, fabric = 0, type = 0, weight = 4 },
            new ClothingItem() { name = "Simple Violet Jerkin", quality = 196, colour = 0, fabric = 0, type = 0, weight = 4 },
            new ClothingItem() { name = "Simple White Wool Robe", quality = 872, colour = 0, fabric = 0, type = 0, weight = 4 },
            new ClothingItem() { name = "Simple Red Jerkin", quality = 161, colour = 0, fabric = 0, type = 0, weight = 4 },
            new ClothingItem() { name = "Simple Purple Fur-Lined Toga", quality = 457, colour = 0, fabric = 0, type = 0, weight = 4 },
            new ClothingItem() { name = "Simple Gold Jerkin", quality = 327, colour = 0, fabric = 0, type = 0, weight = 4 },
            new ClothingItem() { name = "Cheap Gray Silk Hat", quality = 248, colour = 0, fabric = 0, type = 0, weight = 4 },
            new ClothingItem() { name = "Cheap Gold Wool Toga", quality = 139, colour = 0, fabric = 0, type = 0, weight = 4 },
            new ClothingItem() { name = "Fine Silver Jerkin", quality = 357, colour = 0, fabric = 0, type = 0, weight = 4 },
            new ClothingItem() { name = "Simple Gray Cloak", quality = 170, colour = 0, fabric = 0, type = 0, weight = 4 },
            new ClothingItem() { name = "Simple White Fur-Lined Robe", quality = 4260, colour = 0, fabric = 0, type = 0, weight = 4 },
            new ClothingItem() { name = "Cheap Black Jerkin", quality = 221, colour = 0, fabric = 0, type = 0, weight = 4 },
            new ClothingItem() { name = "Fine Purple Fur-Lined Shirt", quality = 979, colour = 0, fabric = 0, type = 0, weight = 6 },
            new ClothingItem() { name = "Simple Purple Dragonskin Blouse", quality = 2364, colour = 0, fabric = 0, type = 0, weight = 4 },
            new ClothingItem() { name = "Simple Orange Silk Vest", quality = 1107, colour = 0, fabric = 0, type = 0, weight = 4 },
            new ClothingItem() { name = "Cheap Gold Silk Skirt", quality = 924, colour = 0, fabric = 0, type = 0, weight = 6 }
        };

        public static QuestItem[] questItems =
        {
            new QuestItem() { name = "Troll Ring Half", weight = 2 },
            new QuestItem() { name = "Goblin Ring Half", weight = 2 },
            new QuestItem() { name = "Reforged Ring", weight = 2 },
            new QuestItem() { name = "Guild Ring", weight = 2 },
            new QuestItem() { name = "Map Stone", weight = 2 },
            new QuestItem() { name = "Amethyst Rod", weight = 2 },
            new QuestItem() { name = "Staff Piece", weight = 2 }
        };

        public static PotionItem[] Potions =
        {
            new PotionItem() { name = "Potion of Water", color = "clear", taste = "plain", sip = "safe" },
            new PotionItem() { name = "Potion of Wine", color = "red", taste = "dry", sip = "caution" },
            new PotionItem() { name = "Potion of Spirits", color = "amber", taste = "sour", sip = "caution" },
            new PotionItem() { name = "Potion of Milk", color = "white", taste = "alkaline", sip = "safe" },
            new PotionItem() { name = "Potion of Fruit Juice", color = "red", taste = "sweet", sip = "safe" },
            new PotionItem() { name = "Potion of Mineral Water", color = "clear", taste = "dry", sip = "safe" },
            new PotionItem() { name = "Potion of Saltwater", color = "clear", taste = "salty", sip = "caution" },
            new PotionItem() { name = "Potion of Invisibility", color = "clear", taste = "dry", sip = "safe" },
            new PotionItem() { name = "Potion of Vinegar", color = "red", taste = "acidic", sip = "caution" },
            new PotionItem() { name = "Potion of ACID!", color = "clear", taste = "acidic", sip = "dangerous" },
            new PotionItem() { name = "Potion of Weak Poison", color = "silver", taste = "bitter", sip = "dangerous" },
            new PotionItem() { name = "Potion of Poison!", color = "white", taste = "alkaline", sip = "dangerous" },
            new PotionItem() { name = "Potion of Strong Poison!", color = "black", taste = "sour", sip = "dangerous" },
            new PotionItem() { name = "Potion of DEADLY POISON!", color = "red", taste = "sweet", sip = "dangerous" },
            new PotionItem() { name = "Potion of Heal Minor Wounds", color = "green", taste = "sour", sip = "safe" },
            new PotionItem() { name = "Potion of Heal Wounds", color = "yellow", taste = "plain", sip = "safe" },
            new PotionItem() { name = "Potion of Heal Major Wounds", color = "silver", taste = "plain", sip = "safe" },
            new PotionItem() { name = "Potion of Heal All Wounds", color = "white", taste = "salty", sip = "safe" },
            new PotionItem() { name = "Potion of Curing Poison", color = "black", taste = "bitter", sip = "caution" },
            new PotionItem() { name = "Potion of Cleansing", color = "black", taste = "bitter", sip = "caution" },
            new PotionItem() { name = "Potion of Delusion", color = "black", taste = "bitter", sip = "caution" },
            new PotionItem() { name = "Potion of Invulnerability Blunt", color = "black", taste = "sweet", sip = "safe" },
            new PotionItem() { name = "Potion of Invulnerability Sharp", color = "black", taste = "plain", sip = "safe" },
            new PotionItem() { name = "Potion of Invulnerability Earth", color = "black", taste = "sour", sip = "safe" },
            new PotionItem() { name = "Potion of Invulnerability Air", color = "black", taste = "salty", sip = "safe" },
            new PotionItem() { name = "Potion of Invulnerability Fire", color = "black", taste = "acidic", sip = "safe" },
            new PotionItem() { name = "Potion of Invulnerability Water", color = "black", taste = "alkaline", sip = "safe" },
            new PotionItem() { name = "Potion of Invulnerability Power", color = "black", taste = "dry", sip = "safe" },
            new PotionItem() { name = "Potion of Invulnerability Mental", color = "black", taste = "plain", sip = "safe" },
            new PotionItem() { name = "Potion of Invulnerability Cleric", color = "black", taste = "sweet", sip = "safe" },
            new PotionItem() { name = "Potion of Noticeability", color = "yellow", taste = "bitter", sip = "dangerous" },
            new PotionItem() { name = "Potion of Inebriation", color = "orange", taste = "plain", sip = "caution" },
            new PotionItem() { name = "Potion of Strength", color = "red", taste = "bitter", sip = "safe" },
            new PotionItem() { name = "Potion of Intelligence", color = "silver", taste = "bitter", sip = "safe" },
            new PotionItem() { name = "Potion of Charisma", color = "silver", taste = "sweet", sip = "safe" },
            new PotionItem() { name = "Potion of Ugliness", color = "green", taste = "sweet", sip = "dangerous" },
            new PotionItem() { name = "Potion of Weakness", color = "yellow", taste = "dry", sip = "dangerous" },
            new PotionItem() { name = "Potion of Dumbness", color = "orange", taste = "sweet", sip = "dangerous" },
            new PotionItem() { name = "Potion of Fleetness", color = "black", taste = "plain", sip = "safe" },
            new PotionItem() { name = "Potion of Slowness", color = "white", taste = "bitter", sip = "dangerous" },
            new PotionItem() { name = "Potion of Protection +1", color = "orange", taste = "sweet", sip = "safe" },
            new PotionItem() { name = "Potion of Protection +2", color = "orange", taste = "sour", sip = "safe" },
            new PotionItem() { name = "Potion of TREASURE FINDING", color = "red", taste = "sweet", sip = "safe" },
            new PotionItem() { name = "Potion of Unnoticeability", color = "clear", taste = "bitter", sip = "safe" }
        };
        #endregion

        #region Private Members

        private static void CannotCarryMessage ()
        {
            string key;
            do
            {
                DispMain();
                CText("@You cannot carry any more!@@@@(Press SPACE to continue)");
                UpdateDisplay();
                key = GetSingleKey();
            } while (key != "SPACE");
        }        
                        
        private static void SwapClothing ( int object_ref )
        {
            bool keypressed = false;

            while (!keypressed)
            {
                switch (plyr.status)
                {
                    case GameStates.Module:
                    ClearGuildDisplay();
                    break;
                    case GameStates.Encounter:
                    DrawEncounterView();
                    break;
                    case GameStates.Explore:
                    DispMain();
                    break;
                    case (GameStates)0:
                    DispMain();
                    break;
                };

                CyText(1, "Wear instead of:");
                BText(5, 3, $"(1) {clothingItems[itemBuffer[plyr.clothing[0]].index].name}");
                BText(5, 4, $"(2) {clothingItems[itemBuffer[plyr.clothing[1]].index].name}");
                BText(5, 5, $"(3) {clothingItems[itemBuffer[plyr.clothing[2]].index].name}");
                BText(5, 6, $"(4) {clothingItems[itemBuffer[plyr.clothing[3]].index].name}");
                BText(2, 8, "Item # or ESC to exit");
                SetFontColor(40, 96, 244, 255);
                BText(2, 8, "     #    ESC");
                SetFontColor(215, 215, 215, 255);

                UpdateDisplay();

                var key = GetSingleKey();
                if (key == "1")
                {
                    plyr.clothing[0] = object_ref;
                    keypressed = true;
                }
                if (key == "2")
                {
                    plyr.clothing[1] = object_ref;
                    keypressed = true;
                }
                if (key == "3")
                {
                    plyr.clothing[2] = object_ref;
                    keypressed = true;
                }
                if (key == "4")
                {
                    plyr.clothing[3] = object_ref;
                    keypressed = true;
                }
                if (key == "ESC")
                    keypressed = true;
            }
        }
               
        //TODO: Add to Items class
        private static void DetermineItemAction ( int selectItemMode, int itemRef )
        {
            if (selectItemMode == 1) // Use
            {
                if (itemRef < 1000)
                    UseObject(itemRef);
                if (itemRef == 1000)
                    UseFood();
                if (itemRef == 1001)
                    UseWater();
                if (itemRef == 1002)
                    UseTorch();
                if (itemRef == 1003)
                    UseTimepiece();
            }

            if (selectItemMode == 2) // Drop
            {
                if (itemRef < 1000)
                    DropObject(itemRef);
                if (itemRef > 999)
                    DropVolumeObject(selectItemMode, itemRef);
            }

            if (selectItemMode == 4)
                DepositObject(itemRef);
            if (selectItemMode == 5)
                WithdrawalObject(itemRef);
        }
                
        private static void DropVolumeObject ( int selectItemMode, int object_ref )
        {
            var itemQuantity = InputItemQuantity(selectItemMode);

            switch (object_ref)
            {
                case 1000:
                {
                    if ((plyr.food > 0) && (itemQuantity > 0))
                    {
                        if (itemQuantity > plyr.food)
                            itemQuantity = plyr.food;
                        var existingItem = CheckForGenericItemsHere(1);
                        if (existingItem == 9999)
                            CreateGenericItem(1, itemQuantity);
                        else
                            itemBuffer[existingItem].hp += itemQuantity;
                        plyr.food -= itemQuantity;
                    };
                    break;
                };
                case 1001:
                {
                    if ((plyr.water > 0) && (itemQuantity > 0))
                    {
                        if (itemQuantity > plyr.water)
                            itemQuantity = plyr.water;
                        var existingItem = CheckForGenericItemsHere(2);
                        if (existingItem == 9999)
                            CreateGenericItem(2, itemQuantity);
                        else
                            itemBuffer[existingItem].hp += itemQuantity;
                        plyr.water -= itemQuantity;
                    };
                    break;
                };
                case 1002:
                {
                    if ((plyr.torches > 0) && (itemQuantity > 0))
                    {
                        if (itemQuantity > plyr.torches)
                            itemQuantity = plyr.torches;
                        var existingItem = CheckForGenericItemsHere(3);
                        if (existingItem == 9999)
                            CreateGenericItem(3, itemQuantity);
                        else
                            itemBuffer[existingItem].hp += itemQuantity;
                        plyr.torches -= itemQuantity;
                    };
                    break;
                };
                case 1003:
                {
                    if ((plyr.timepieces > 0) && (itemQuantity > 0))
                    {
                        if (itemQuantity > plyr.timepieces)
                            itemQuantity = plyr.timepieces;
                        var existingItem = CheckForGenericItemsHere(4);
                        if (existingItem == 9999)
                            CreateGenericItem(4, itemQuantity);
                        else
                            itemBuffer[existingItem].hp += itemQuantity;
                        plyr.timepieces -= itemQuantity;
                    };
                    break;
                };
                case 1004:
                {
                    if ((plyr.compasses > 0) && (itemQuantity > 0))
                    {
                        if (itemQuantity > plyr.compasses)
                            itemQuantity = plyr.compasses;
                        var existingItem = CheckForGenericItemsHere(5);
                        if (existingItem == 9999)
                            CreateGenericItem(5, itemQuantity);
                        else
                            itemBuffer[existingItem].hp += itemQuantity;
                        plyr.compasses -= itemQuantity;
                    };
                    break;
                };
                case 1005:
                {
                    if ((plyr.keys > 0) && (itemQuantity > 0))
                    {
                        if (itemQuantity > plyr.keys)
                            itemQuantity = plyr.keys;

                        var existingItem = CheckForGenericItemsHere(6);
                        if (existingItem == 9999)
                            CreateGenericItem(6, itemQuantity);
                        else
                            itemBuffer[existingItem].hp += itemQuantity;
                        plyr.keys -= itemQuantity;
                    };
                    break;
                };
                case 1006:
                {
                    if ((plyr.crystals > 0) && (itemQuantity > 0))
                    {
                        if (itemQuantity > plyr.crystals)
                            itemQuantity = plyr.crystals;
                        var existingItem = CheckForGenericItemsHere(7);
                        if (existingItem == 9999)
                            CreateGenericItem(7, itemQuantity);
                        else
                            itemBuffer[existingItem].hp += itemQuantity;
                        plyr.crystals -= itemQuantity;
                    };
                    break;
                };
                case 1007:
                {
                    if ((plyr.gems > 0) && (itemQuantity > 0))
                    {
                        if (itemQuantity > plyr.gems)
                            itemQuantity = plyr.gems;
                        var existingItem = CheckForGenericItemsHere(8);
                        if (existingItem == 9999)
                            CreateGenericItem(8, itemQuantity);
                        else
                            itemBuffer[existingItem].hp += itemQuantity;
                        plyr.gems -= itemQuantity;
                    };
                    break;
                };
                case 1008:
                {
                    if ((plyr.jewels > 0) && (itemQuantity > 0))
                    {
                        if (itemQuantity > plyr.jewels)
                            itemQuantity = plyr.jewels;
                        var existingItem = CheckForGenericItemsHere(9);
                        if (existingItem == 9999)
                            CreateGenericItem(9, itemQuantity);
                        else
                            itemBuffer[existingItem].hp += itemQuantity;
                        plyr.jewels -= itemQuantity;
                    };
                    break;
                };
                case 1009:
                {
                    if ((plyr.gold > 0) && (itemQuantity > 0))
                    {
                        if (itemQuantity > plyr.gold)
                            itemQuantity = plyr.gold;
                        var existingItem = CheckForGenericItemsHere(10);
                        if (existingItem == 9999)
                            CreateGenericItem(10, itemQuantity);
                        else
                            itemBuffer[existingItem].hp += itemQuantity;
                        plyr.gold -= itemQuantity;
                    };
                    break;
                };
                case 1010:
                {
                    if ((plyr.silver > 0) && (itemQuantity > 0))
                    {
                        if (itemQuantity > plyr.silver)
                            itemQuantity = plyr.silver;
                        var existingItem = CheckForGenericItemsHere(11);
                        if (existingItem == 9999)
                            CreateGenericItem(11, itemQuantity);
                        else
                            itemBuffer[existingItem].hp += itemQuantity;
                        plyr.silver -= itemQuantity;
                    };
                    break;
                };
                case 1011:
                {
                    if ((plyr.copper > 0) && (itemQuantity > 0))
                    {
                        if (itemQuantity > plyr.copper)
                            itemQuantity = plyr.copper;
                        var existingItem = CheckForGenericItemsHere(12);
                        if (existingItem == 9999)
                            CreateGenericItem(12, itemQuantity);
                        else
                            itemBuffer[existingItem].hp += itemQuantity;
                        plyr.copper -= itemQuantity;
                    };
                    break;
                };
            };
        }        

        private static int CheckForGenericItemsHere ( int type )
        {
            // counts number of objects on a map square - equal to 1
            var value = 9999; // null return
            var cur_idx = 0;
            while (cur_idx < plyr.buffer_index)
            {
                if ((itemBuffer[cur_idx].type == type) && (itemBuffer[cur_idx].x == plyr.x) && (itemBuffer[cur_idx].y == plyr.y) && (itemBuffer[cur_idx].level == plyr.map) && (itemBuffer[cur_idx].location == 1))
                    value = cur_idx;
                cur_idx++;

            }
            return value;
        }

        //TODO: Move to Items class
        private static void UseFood ()
        {
            if (plyr.food > 0)
            {
                plyr.hunger -= 16;
                if (plyr.hunger < 0)
                    plyr.hunger = 0;
                plyr.food--;
            }
        }

        //TODO: Move to Items class
        private static void UseWater ()
        {
            if (plyr.water > 0)
            {
                plyr.thirst -= 15;
                if (plyr.thirst < 0)
                    plyr.thirst = 0;
                plyr.water--;
            }
        }

        //TODO: Move to Items class
        private static void UseTorch ()
        {
            // Header - 8B 24 00 00 02 10
            // Text -   4C 69 74 20 54 6F 72 63 68 00
            // Ammo -   00 FF 00
            // Damage - 13 00 00 00 13 00 00 00 00 00 00 (blunt & fire)
            // 04 01 16 16 82 03

            var keypressed = false;
            if (plyr.torches > 0)
            {

                while (!keypressed)
                {
                    DispMain();
                    BText(17, 1, "Use as:");
                    BText(12, 4, "1 Primary weapon");
                    BText(12, 5, "2 Secondary weapon");
                    BText(9, 8, "Press number or ESC to exit");
                    UpdateDisplay();

                    var key_value = GetSingleKey();

                    // Header - 8B 24 00 00 02 10
                    // Text -   4C 69 74 20 54 6F 72 63 68 00
                    // Ammo -   00 FF 00
                    // Damage - 13 00 00 00 13 00 00 00 00 00 00 (blunt & fire)
                    // 04 01 16 16 82 03

                    if (key_value == "1")
                    {
                        plyr.priWeapon = CreateItem(178, 0x0, "Lit Torch", 0x16, 0x16, 0x82, 0x04, 0x01, 0x0, 0x13, 0, 0, 0, 0x13, 0, 0, 0, 0, 0, 0, 0x02, 0, 0xFF, 0, 0x03);
                        itemBuffer[plyr.priWeapon].location = 10;
                        plyr.torches--;
                        // remove old primary ref if exists
                        keypressed = true;
                    }
                    if (key_value == "2")
                    {
                        plyr.secWeapon = CreateItem(178, 0x0, "Lit Torch", 0x16, 0x16, 0x82, 0x04, 0x01, 0x0, 0x13, 0, 0, 0, 0x13, 0, 0, 0, 0, 0, 0, 0x02, 0, 0xFF, 0, 0x03);
                        itemBuffer[plyr.secWeapon].location = 10;
                        plyr.torches--;
                        keypressed = true;
                        // remove old secondary ref if exists
                    }
                    if (key_value == "ESC")
                        keypressed = true;
                }                
            } else
            {
                while (!keypressed)
                {
                    DispMain();
                    CyText(2, "You have none.");
                    CyText(9, "( Press a key )");
                    UpdateDisplay();
                    var key_value = GetSingleKey();
                    if (key_value != "")
                        keypressed = true;
                }
            }

        }

        //TODO: Move to Items class
        private static void UseTimepiece ()
        {
            var hourtext = "th";
            if ((plyr.hours == 1) || (plyr.hours == 21))
                hourtext = "st";
            if ((plyr.hours == 2) || (plyr.hours == 22))
                hourtext = "nd";
            if ((plyr.hours == 3) || (plyr.hours == 23))
                hourtext = "rd";

            var str = $"It is {plyr.minutes} minutes@past the {plyr.hours} {hourtext} hour.@@@@(Press SPACE to continue)";
            var key = "";
            if (plyr.timepieces > 0)
            {
                do
                {
                    DispMain();
                    CText(str);
                    UpdateDisplay();
                    key = GetSingleKey();
                } while (key != "SPACE");
            } else
            {
                do
                {
                    DispMain();
                    CText("You have none.");
                    UpdateDisplay();
                    key = GetSingleKey();
                } while (key != "SPACE");
            }
        }

        //TODO: Move to Items class
        private static void UsePotion ( int object_ref )
        {
            // Need to add POOF!
            // Need to add option of identifying potion in response to SIP

            var keypressed = false;
            var potionType = itemBuffer[object_ref].index;

            while (!keypressed)
            {
                if (plyr.status == GameStates.Encounter)
                    DrawEncounterView();
                else
                    DispMain();

                CyText(1, "POTION");
                BText(16, 3, "(1) Taste");
                BText(16, 4, "(2) Sip");
                BText(16, 5, "(3) Examine");
                BText(16, 6, "(4) Quaff");
                CyText(8, "Press number or ESC to exit");
                UpdateDisplay();

                var key_value = GetSingleKey();
                if (key_value == "1")
                {
                    ItemMessage("The potion tastes " + Potions[potionType].taste + ".");
                }
                if (key_value == "2")
                {
                    var str = "You take a sip of the potion,@and feel it is " + Potions[potionType].sip + ".";
                    if (Potions[potionType].sip == "caution")
                        str = "You take a sip of the potion,@and feel that you@should show caution.";
                    ItemMessage(str);
                }
                if (key_value == "3")
                {
                    ItemMessage("You open the potion and@see a potion that is " + Potions[potionType].color + "@in colour.");
                }
                if (key_value == "4")
                {
                    QuaffPotion(object_ref);
                    keypressed = true;
                }
                if (key_value == "ESC")
                    keypressed = true;
            }
        }

        //TODO: Move to Items class
        private static void QuaffPotion ( int object_ref )
        {
            var potionType = itemBuffer[object_ref].index;
            var potionName = Potions[potionType].name;

            ItemMessage($"You drink a@@{potionName}");

            // Implement potion effect
            if (potionType == 0)
            {
                plyr.thirst -= 2;
                if (plyr.thirst < 0)
                    plyr.thirst = 0;
            }
            if (potionType == 1)
                plyr.alcohol += 3;
            if (potionType == 2)
                plyr.alcohol += 4;
            if (potionType == 3)
            {
                plyr.thirst -= 3;
                if (plyr.thirst < 0)
                    plyr.thirst = 0;
            }
            if (potionType == 4)
            {
                plyr.thirst -= 3;
                if (plyr.thirst < 0)
                    plyr.thirst = 0;
            }
            if (potionType == 5)
            {
                plyr.thirst -= 2;
                if (plyr.thirst < 0)
                    plyr.thirst = 0;
            }
            if (potionType == 6)
                plyr.thirst += 3;
            if (potionType == 7)
                plyr.invisibility = 1;
            if (potionType == 8)
                plyr.thirst += 2;
            if (potionType == 9)
                plyr.hp -= 5;
            if (potionType == 10)
                plyr.poison[0] = 1;
            if (potionType == 11)
                plyr.poison[1] = 1;
            if (potionType == 12)
                plyr.poison[2] = 1;
            if (potionType == 13)
                plyr.poison[3] = 1;
            if (potionType == 14)
            {
                plyr.hp += 5;
                if (plyr.hp > plyr.maxhp)
                    plyr.hp = plyr.maxhp;
            }
            if (potionType == 15)
            {
                plyr.hp += 8;
                if (plyr.hp > plyr.maxhp)
                    plyr.hp = plyr.maxhp;
            }
            if (potionType == 16)
            {
                plyr.hp += 11;
                if (plyr.hp > plyr.maxhp)
                    plyr.hp = plyr.maxhp;
            }
            if (potionType == 17)
                plyr.hp = plyr.maxhp;
            if (potionType == 18)
            {
                plyr.poison[0] = 0;
                plyr.poison[1] = 0;
                plyr.poison[2] = 0;
                plyr.poison[3] = 0;
            }
            if (potionType == 19)
            {
                plyr.diseases[0] = 0;
                plyr.diseases[1] = 0;
                plyr.diseases[2] = 0;
                plyr.diseases[3] = 0;
            }
            if (potionType == 20)
                plyr.delusion = 1;
            if (potionType == 21)
                plyr.invulnerability[0] += 8;
            if (potionType == 22)
                plyr.invulnerability[1] += 8;
            if (potionType == 23)
                plyr.invulnerability[2] += 8;
            if (potionType == 24)
                plyr.invulnerability[3] += 8;
            if (potionType == 25)
                plyr.invulnerability[4] += 8;
            if (potionType == 26)
                plyr.invulnerability[5] += 8;
            if (potionType == 27)
                plyr.invulnerability[6] += 8;
            if (potionType == 28)
                plyr.invulnerability[7] += 8;
            if (potionType == 29)
                plyr.invulnerability[8] += 8;
            if (potionType == 30)
                plyr.noticeability += 2;
            if (potionType == 31)
                plyr.alcohol += 65;
            if (potionType == 32)
                plyr.str += 1;
            if (potionType == 33)
                plyr.inte += 1;
            if (potionType == 34)
                plyr.chr += 1;
            if (potionType == 35)
                plyr.chr -= 2;
            if (potionType == 36)
                plyr.str -= 2;
            if (potionType == 37)
                plyr.inte -= 2;
            if (potionType == 38)
                plyr.speed -= 2;
            if (potionType == 39)
                plyr.speed -= 2;
            if (potionType == 40)
                plyr.protection1 += 2;
            if (potionType == 41)
                plyr.protection2 += 2;
            if (potionType == 42)
                plyr.treasureFinding += 5;
            if (potionType == 43)
            {
                plyr.noticeability -= 2;
                if (plyr.noticeability < 0)
                    plyr.noticeability = 0;
            }

            itemBuffer[object_ref].location = 0; // Move used potion to void
            TidyObjectBuffer();
        }

        //TODO: Move to Items class
        private static void UseWeapon ( int object_ref )
        {
            var keypressed = false;

            while (!keypressed)
            {
                if (plyr.status == GameStates.Encounter)
                    DrawEncounterView();
                else
                    DispMain();

                BText(17, 1, "Use as:");
                BText(12, 4, "1 Primary weapon");
                BText(12, 5, "2 Secondary weapon");
                BText(9, 8, "Press number or ESC to exit");
                UpdateDisplay();

                var key_value = GetSingleKey();

                if (key_value == "1")
                {
                    plyr.priWeapon = object_ref;
                    if (object_ref == plyr.secWeapon)
                        plyr.secWeapon = 255;
                    itemBuffer[plyr.priWeapon].location = 10; // primary was 11
                                                              // remove old primary ref if exists
                    keypressed = true;
                }
                if (key_value == "2")
                {
                    plyr.secWeapon = object_ref;
                    if (object_ref == plyr.priWeapon)
                        plyr.priWeapon = 255;
                    itemBuffer[plyr.secWeapon].location = 10; // secondary was 12
                    plyr.torches--; // ??????
                    keypressed = true;
                    // remove old secondary ref if exists
                }
                if (key_value == "ESC")
                    keypressed = true;
            }
        }

        //TODO: Move to Items class
        private static void UseClothing ( int object_ref )
        {
            var allocated = false;
            if (plyr.clothing[0] == 255)
            {
                plyr.clothing[0] = object_ref;
                allocated = true;
            }
            if ((plyr.clothing[1] == 255) && !allocated)
            {
                plyr.clothing[1] = object_ref;
                allocated = true;
            }
            if ((plyr.clothing[2] == 255) && !allocated)
            {
                plyr.clothing[2] = object_ref;
                allocated = true;
            }
            if ((plyr.clothing[3] == 255) && !allocated)
            {
                plyr.clothing[3] = object_ref;
                allocated = true;
            }

            if ((plyr.clothing[3] != 255) && !allocated)
                SwapClothing(object_ref);
        }

        //TODO: Move to Items class
        private static void UseObject ( int object_ref )
        {
            // Determine object type and pass object_ref to appropriate function
            if (itemBuffer[object_ref].type == 178)
                UseWeapon(object_ref);
            if (itemBuffer[object_ref].type == 177)
                UseArmor(object_ref);
            if ((itemBuffer[object_ref].type == 176) && (itemBuffer[object_ref].hp == 0))
                UsePotion(object_ref);
            if ((itemBuffer[object_ref].type == 176) && (itemBuffer[object_ref].hp == 1))
                QuaffPotion(object_ref);
            if (itemBuffer[object_ref].type == 180)
            {
                if (!((plyr.clothing[0] == object_ref) || (plyr.clothing[1] == object_ref) || (plyr.clothing[2] == object_ref) || (plyr.clothing[3] == object_ref)))
                    UseClothing(object_ref);
            }
            if (itemBuffer[object_ref].type == 199)
                UseAmmoItem(object_ref);
            if (itemBuffer[object_ref].type == 200)
                UseQuestItem(object_ref);
            if (itemBuffer[object_ref].type == 201)
                UseQuestItem(object_ref);
        }

        //TODO: Move to Items/Players class
        private static void DropObject ( int object_ref )
        {
            // Turn lit torch to stick when dropped
            itemBuffer[object_ref].location = 1;
            itemBuffer[object_ref].x = plyr.x;
            itemBuffer[object_ref].y = plyr.y;
            itemBuffer[object_ref].level = plyr.map;
            if (plyr.headArmour == object_ref)
                plyr.headArmour = 255;
            if (plyr.bodyArmour == object_ref)
                plyr.bodyArmour = 255;
            if (plyr.armsArmour == object_ref)
                plyr.armsArmour = 255;
            if (plyr.legsArmour == object_ref)
                plyr.legsArmour = 255;
            if (plyr.priWeapon == object_ref)
                plyr.priWeapon = 0; // Set bufferItem[0] - bare hand
            if (plyr.secWeapon == object_ref)
                plyr.secWeapon = 0; // Set bufferItem[0] - bare hand
            if (plyr.clothing[0] == object_ref)
                plyr.clothing[0] = 255;
            if (plyr.clothing[1] == object_ref)
                plyr.clothing[1] = 255;
            if (plyr.clothing[2] == object_ref)
                plyr.clothing[2] = 255;
            if (plyr.clothing[3] == object_ref)
                plyr.clothing[3] = 255;
        }

        private static void ItemMessage ( string message )
        {
            var keynotpressed = true;
            while (keynotpressed)
            {
                if (plyr.status == GameStates.Encounter)
                    DrawEncounterView();
                else if (plyr.status == GameStates.Explore)
                    DispMain();

                CText(message);
                CyText(8, "( Press SPACE to continue )");
                UpdateDisplay();
                var key = GetSingleKey();
                if (key != "")
                    keynotpressed = false;
            }
        }

        //TODO: Move to Players class
        private static void DepositObject ( int itemRef )
        {
            var itemQuantity = InputDepositQuantity(itemRef);
            if (itemQuantity > 0)
            {
                if ((itemRef == 1000) && (plyr.food > 0))
                {
                    if (itemQuantity > plyr.food)
                        itemQuantity = plyr.food;
                    plyr.food -= itemQuantity;
                    plyr.lfood += itemQuantity;
                }
                if ((itemRef == 1001) && (plyr.water > 0))
                {
                    if (itemQuantity > plyr.water)
                        itemQuantity = plyr.water;
                    plyr.water -= itemQuantity;
                    plyr.lwater += itemQuantity;
                }
                if ((itemRef == 1002) && (plyr.torches > 0))
                {
                    if (itemQuantity > plyr.torches)
                        itemQuantity = plyr.torches;
                    plyr.torches -= itemQuantity;
                    plyr.ltorches += itemQuantity;
                }
                if ((itemRef == 1003) && (plyr.timepieces > 0))
                {
                    if (itemQuantity > plyr.timepieces)
                        itemQuantity = plyr.timepieces;
                    plyr.timepieces -= itemQuantity;
                    plyr.ltimepieces += itemQuantity;
                }
                if ((itemRef == 1004) && (plyr.compasses > 0))
                {
                    if (itemQuantity > plyr.compasses)
                        itemQuantity = plyr.compasses;
                    plyr.compasses -= itemQuantity;
                    plyr.lcompasses += itemQuantity;
                }
                if ((itemRef == 1005) && (plyr.keys > 0))
                {
                    if (itemQuantity > plyr.keys)
                        itemQuantity = plyr.keys;
                    plyr.keys -= itemQuantity;
                    plyr.lkeys += itemQuantity;
                }
                if ((itemRef == 1006) && (plyr.crystals > 0))
                {
                    if (itemQuantity > plyr.crystals)
                        itemQuantity = plyr.crystals;
                    plyr.crystals -= itemQuantity;
                    plyr.lcrystals += itemQuantity;
                }
                if ((itemRef == 1007) && (plyr.gems > 0))
                {
                    if (itemQuantity > plyr.gems)
                        itemQuantity = plyr.gems;
                    plyr.gems -= itemQuantity;
                    plyr.lgems += itemQuantity;
                }
                if ((itemRef == 1008) && (plyr.jewels > 0))
                {
                    if (itemQuantity > plyr.jewels)
                        itemQuantity = plyr.jewels;
                    plyr.jewels -= itemQuantity;
                    plyr.ljewels += itemQuantity;
                }
                if ((itemRef == 1009) && (plyr.gold > 0))
                {
                    if (itemQuantity > plyr.gold)
                        itemQuantity = plyr.gold;
                    plyr.gold -= itemQuantity;
                    plyr.lgold += itemQuantity;
                }
                if ((itemRef == 1010) && (plyr.silver > 0))
                {
                    if (itemQuantity > plyr.silver)
                        itemQuantity = plyr.silver;
                    plyr.silver -= itemQuantity;
                    plyr.lsilver += itemQuantity;
                }
                if ((itemRef == 1011) && (plyr.copper > 0))
                {
                    if (itemQuantity > plyr.copper)
                        itemQuantity = plyr.copper;
                    plyr.copper -= itemQuantity;
                    plyr.lcopper += itemQuantity;
                }

            }
        }

        //TODO: Move to Players class
        private static void WithdrawalObject ( int itemRef )
        {
            var itemQuantity = InputWithdrawalQuantity(itemRef);
            if (itemQuantity > 0)
            {
                if ((itemRef == 1000) && (plyr.lfood > 0))
                {
                    if (itemQuantity > plyr.lfood)
                        itemQuantity = plyr.lfood;
                    plyr.food += itemQuantity;
                    plyr.lfood -= itemQuantity;
                }
                if ((itemRef == 1001) && (plyr.lwater > 0))
                {
                    if (itemQuantity > plyr.lwater)
                        itemQuantity = plyr.lwater;
                    plyr.water += itemQuantity;
                    plyr.lwater -= itemQuantity;
                }
                if ((itemRef == 1002) && (plyr.ltorches > 0))
                {
                    if (itemQuantity > plyr.ltorches)
                        itemQuantity = plyr.ltorches;
                    plyr.torches += itemQuantity;
                    plyr.ltorches -= itemQuantity;
                }
                if ((itemRef == 1003) && (plyr.ltimepieces > 0))
                {
                    if (itemQuantity > plyr.ltimepieces)
                        itemQuantity = plyr.ltimepieces;
                    plyr.timepieces += itemQuantity;
                    plyr.ltimepieces -= itemQuantity;
                }
                if ((itemRef == 1004) && (plyr.lcompasses > 0))
                {
                    if (itemQuantity > plyr.lcompasses)
                        itemQuantity = plyr.lcompasses;
                    plyr.compasses += itemQuantity;
                    plyr.lcompasses -= itemQuantity;
                }
                if ((itemRef == 1005) && (plyr.lkeys > 0))
                {
                    if (itemQuantity > plyr.lkeys)
                        itemQuantity = plyr.lkeys;
                    plyr.keys += itemQuantity;
                    plyr.lkeys -= itemQuantity;
                }
                if ((itemRef == 1006) && (plyr.lcrystals > 0))
                {
                    if (itemQuantity > plyr.lcrystals)
                        itemQuantity = plyr.lcrystals;
                    plyr.crystals += itemQuantity;
                    plyr.lcrystals -= itemQuantity;
                }
                if ((itemRef == 1007) && (plyr.lgems > 0))
                {
                    if (itemQuantity > plyr.lgems)
                        itemQuantity = plyr.lgems;
                    plyr.gems += itemQuantity;
                    plyr.lgems -= itemQuantity;
                }
                if ((itemRef == 1008) && (plyr.ljewels > 0))
                {
                    if (itemQuantity > plyr.ljewels)
                        itemQuantity = plyr.ljewels;
                    plyr.jewels += itemQuantity;
                    plyr.ljewels -= itemQuantity;
                }
                if ((itemRef == 1009) && (plyr.lgold > 0))
                {
                    if (itemQuantity > plyr.lgold)
                        itemQuantity = plyr.lgold;
                    plyr.gold += itemQuantity;
                    plyr.lgold -= itemQuantity;
                }
                if ((itemRef == 1010) && (plyr.lsilver > 0))
                {
                    if (itemQuantity > plyr.lsilver)
                        itemQuantity = plyr.lsilver;
                    plyr.silver += itemQuantity;
                    plyr.lsilver -= itemQuantity;
                }
                if ((itemRef == 1011) && (plyr.lcopper > 0))
                {
                    if (itemQuantity > plyr.lcopper)
                        itemQuantity = plyr.lcopper;
                    plyr.copper += itemQuantity;
                    plyr.lcopper -= itemQuantity;
                }
            }
        }
        
        //TODO: Move to Items class
        private static void UseArmor ( int object_ref )
        {
            // The "melee" attribute is used for bodypart in armour items
            if (itemBuffer[object_ref].melee == 0)
                plyr.headArmour = object_ref;
            if (itemBuffer[object_ref].melee == 1)
                plyr.bodyArmour = object_ref;
            if (itemBuffer[object_ref].melee == 2)
                plyr.armsArmour = object_ref;
            if (itemBuffer[object_ref].melee == 3)
                plyr.legsArmour = object_ref;
            Console.Write(itemBuffer[object_ref].melee);
            Console.Write("\n");                
        }

        //TODO: Move to Items class
        private static void UseQuestItem ( int object_ref )
        {
            if (itemBuffer[object_ref].index == 4)
                DisplayLocation();
        }

        //TODO: Move to Items class
        private  static void UseAmmoItem ( int object_ref )
        {
            // Assume Thunder quarrels for now + leave out ammo type check
            if (itemBuffer[plyr.priWeapon].melee != 0xff)
            {
                itemBuffer[plyr.priWeapon].name = "Crossbow [10]";
                itemBuffer[plyr.priWeapon].ammo = 10;
                itemBuffer[plyr.priWeapon].power = 0x18;
                itemBuffer[object_ref].location = 0; // Destroy the ammo following a reload
            }
        }        
        #endregion
    }
}