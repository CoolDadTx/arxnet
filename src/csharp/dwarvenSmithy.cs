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

/* DWARVEN SMITHY.CPP
		 *
		 * TODO:
		 *
		 *  Offer to reforge Goblin / Troll ring
		 *
		 *
		 */
namespace P3Net.Arx
{
    public enum DwarvenSmithyMenus
    {
        MenuLeft,
        MenuMain,
        MenuChooseSmithyItem,
        MenuPreOffer,
        MenuSelectOffer,
        MenuSmithyMakesOffer,
        MenuOfferRefused,
        MenuNoFunds,
        MenuAnythingElse,
        MenuAnythingElse2,
        MenuCustom,
        MenuCustomOrdered,
        MenuBusyForging,
        MenuCustomReady,
        MenuNoHaggle,
        MenuNoNameProvided
    }

	public partial class GlobalMembers
    {
        public static string customWeaponDesc = "";

        public static int customWeaponType;

        public static int dmenu;

        public static byte[] dwarvenBinary = new byte[dwarvenFileSize];       

        public static readonly int dwarvenFileSize = 459;
        public static int dwarvenItemOffset;
        public static int[] dwarvenItemOffsets =
        { 0x79, 0x00, 0x29, 0x50, 0xA0, 0xCB, 0x120, 0x149, 0xF7, 0x19D, 0x173 };

        public static string itemDesc = "item";

        public static string[] itemNames =
        {
            "Truesilver Morion",
            "Truesilver Coat",
            "Truesilver Gauntlets",
            "Truesilver Leggings",
            "Truesilver Sword",
            "Truesilver Hammer",
            "Thunder Hammer",
            "Truesilver Mace",
            "Truesilver Axe",
            "Crossbow [10]",
            "Thunder Quarrels [10]"
        };

        public static int[] itemPrices = { 40, 70, 50, 60, 60, 60, 30, 60, 60, 20, 5 };
        public static int itemRef = 0;
        public static int itemValue = 0;
        public static int smithyChoice = 0;

        public static void AddDwarvenSmithyToMap()
        {
            SetAutoMapFlag(plyr.map, 16, 6);
            SetAutoMapFlag(plyr.map, 17, 6);
            SetAutoMapFlag(plyr.map, 18, 6);
            SetAutoMapFlag(plyr.map, 16, 7);
            SetAutoMapFlag(plyr.map, 17, 7);
            SetAutoMapFlag(plyr.map, 18, 7);
            SetAutoMapFlag(plyr.map, 16, 8);
            SetAutoMapFlag(plyr.map, 17, 8);
            SetAutoMapFlag(plyr.map, 18, 8);
        }

        public static void BuildSmithyMenuOptions()
        {
            // Copies Dwarven Smithy items into structure for use in module.cpp
            // The Dwarven Smithy has a fixed, unchanging menu of items for purchase.
            for (var waresNo = 0; waresNo < 11; waresNo++)
            {
                menuItems[waresNo].menuName = itemNames[waresNo];
                var str = $"{Itos(itemPrices[waresNo])}  ";
                str = str.Remove(3, 7).Insert(3, "gems/jewels");
                menuItems[waresNo].menuPrice = str;
                menuItems[waresNo].objRef = waresNo;
            }
        }

        public static void CalculateForgeBonus(int additionalGemsOffered)
        {
            var bonus = additionalGemsOffered / 60;
            if(bonus < 1)
                bonus = 0;
            plyr.forgeBonus = bonus;
        }

        public static int CalculateGemsAndJewelsTotal() => plyr.gems + plyr.jewels;

        public static void CalculateSaleItemValue(int itemRef)
        {
            var damageValues = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            var results = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            damageValues[0] = itemBuffer[itemRef].blunt;
            damageValues[1] = itemBuffer[itemRef].sharp;
            damageValues[2] = itemBuffer[itemRef].earth;
            damageValues[3] = itemBuffer[itemRef].air;
            damageValues[4] = itemBuffer[itemRef].fire;
            damageValues[5] = itemBuffer[itemRef].water;
            damageValues[6] = itemBuffer[itemRef].power;
            damageValues[7] = itemBuffer[itemRef].magic;
            damageValues[8] = itemBuffer[itemRef].good;
            damageValues[9] = itemBuffer[itemRef].evil;
            damageValues[10] = itemBuffer[itemRef].cold;

            var damageIndex = 0;
            while(damageIndex < 11)
            {
                var noDice = (damageValues[damageIndex] & 0xf0) >> 4;
                var noSides = (damageValues[damageIndex] & 0x0f);
                if(noDice > 0)
                    results[damageIndex] = noDice * noSides;
                damageIndex++;
            }

            var itemValueA = (((itemBuffer[itemRef].hp) - 1) * 2);
            var itemValueB = results[0] +
                results[1] +
                results[2] +
                results[3] +
                results[4] +
                results[5] +
                results[6] +
                results[7] +
                results[8] +
                results[9] +
                results[10];
            itemValue = itemValueA + itemValueB;
        }

        // Attempt to buy a chosen Dwarven Smithy (non custom) item
        public static void ChooseDwarvenSmithyItem()
        {
            smithyChoice = InputItemChoice("What would thou like? (0 to go back)", 11);

            if(smithyChoice < 255)
            {
                var gemsCost = itemPrices[smithyChoice];
                var playerGems = CalculateGemsAndJewelsTotal();

                if(playerGems >= gemsCost)
                {
                    DeductGems(gemsCost);
                    dwarvenItemOffset = dwarvenItemOffsets[smithyChoice];
                    CreateDwarvenInventoryItem(dwarvenItemOffset);

                    var test = Randn(0, 4);
                    if(test < 3)
                        dmenu = (int)DwarvenSmithyMenus.MenuAnythingElse;
                    else
                        dmenu = (int)DwarvenSmithyMenus.MenuAnythingElse2;
                } else
                {
                    dmenu = (int)DwarvenSmithyMenus.MenuNoFunds;
                }
            } else
            {
                dmenu = (int)DwarvenSmithyMenus.MenuMain;
            } // Option 0 was selected
        }

        public static void CreateCustomWeapon()
        {
            // Routine to create weapon after 4 days have elapsed

            int index;
            int alignment;
            int weight;
            int wAttributes;
            int melee;
            int ammo;
            int blunt;
            int sharp;
            int earth;
            int air;
            int fire;
            int water;
            int power;
            int magic;
            int good;
            int evil;
            int cold;
            int minStrength;
            int minDexterity;
            int hp;
            int maxHP;
            int flags;
            int parry;
            int useStrength;

            var offset = 0x00;
            switch(plyr.forgeType)
            {
                case 1:
                    offset = 0xA0; // Truesilver sword
                    break;
                case 2:
                    offset = 0xF7; // Truesilver axe
                    break;
                case 3:
                    offset = 0x149; // Truesilver mace
                    break;
                case 4:
                    offset = 0xCB; // Truesilver hammer
                    break;
                default:
                    offset = 0xA0;
                    break;
            }

            var itemType = dwarvenBinary[offset];
            var itemName = plyr.forgeName;

            itemType = 178; // ARX value for weapon
            index = 0; // No longer required
            useStrength = 0;
            alignment = dwarvenBinary[offset + 3];
            weight = dwarvenBinary[offset + 4];

            wAttributes = (offset + dwarvenBinary[offset + 1]) - 20; // Working out from the end of the weapon object

            melee = dwarvenBinary[wAttributes + 1]; // or ammo type for Crossbow
            ammo = dwarvenBinary[wAttributes + 2]; // 0 for melee weapons / shields
            blunt = (dwarvenBinary[wAttributes + 3]) + 0x17 + plyr.forgeBonus;
            sharp = dwarvenBinary[wAttributes + 4] + 0x17 + plyr.forgeBonus;
            earth = dwarvenBinary[wAttributes + 5];
            air = dwarvenBinary[wAttributes + 6];
            fire = dwarvenBinary[wAttributes + 7];
            water = dwarvenBinary[wAttributes + 8];
            power = dwarvenBinary[wAttributes + 9];
            magic = dwarvenBinary[wAttributes + 10];
            good = dwarvenBinary[wAttributes + 11];
            evil = dwarvenBinary[wAttributes + 12];
            cold = dwarvenBinary[wAttributes + 13];
            minStrength = dwarvenBinary[wAttributes + 14];
            minDexterity = dwarvenBinary[wAttributes + 15];
            hp = 0xFF;
            maxHP = 0xFF;
            flags = 90;
            parry = (dwarvenBinary[wAttributes + 19]) * 2;

            var newItemRef = CreateItem(itemType,
                                        index,
                                        itemName,
                                        hp,
                                        maxHP,
                                        flags,
                                        minStrength,
                                        minDexterity,
                                        useStrength,
                                        blunt,
                                        sharp,
                                        earth,
                                        air,
                                        fire,
                                        water,
                                        power,
                                        magic,
                                        good,
                                        evil,
                                        cold,
                                        weight,
                                        alignment,
                                        melee,
                                        ammo,
                                        parry);
            itemBuffer[newItemRef].location = 10; // Add to player inventory - 10

            // Reset related variables once custom weapon ready
            plyr.forgeBonus = 0;
            plyr.forgeDays = 0;
            plyr.forgeType = 0;
            plyr.forgeName = "";
        }

        // Take a binary offset within dwarvenBinary and create a new inventory item from the binary data (weapon, armour or clothing)
        // Item types:  03 - weapon, 04 - armour, 02 - ammo        
        public static void CreateDwarvenInventoryItem(int startByte)
        {
            int index;
            int alignment;
            int weight;
            int wAttributes;
            int melee;
            int ammo;
            int blunt;
            int sharp;
            int earth;
            int air;
            int fire;
            int water;
            int power;
            int magic;
            int good;
            int evil;
            int cold;
            int minStrength;
            int minDexterity;
            int hp;
            int maxHP;
            int flags;
            int parry;
            int useStrength;

            var offset = startByte;
            var itemType = dwarvenBinary[offset];
            var itemName = ReadDwarvenNameString((offset + 6));

            if(itemType == 3)
            {
                itemType = 178; // ARX value for weapon
                index = 0; // No longer required
                useStrength = 0;
                alignment = dwarvenBinary[offset + 3];
                weight = dwarvenBinary[offset + 4];

                wAttributes = (offset + dwarvenBinary[offset + 1]) - 20; // Working out from the end of the weapon object

                melee = dwarvenBinary[wAttributes + 1]; // or ammo type for Crossbow
                ammo = dwarvenBinary[wAttributes + 2]; // 0 for melee weapons / shields
                blunt = dwarvenBinary[wAttributes + 3];
                sharp = dwarvenBinary[wAttributes + 4];
                earth = dwarvenBinary[wAttributes + 5];
                air = dwarvenBinary[wAttributes + 6];
                fire = dwarvenBinary[wAttributes + 7];
                water = dwarvenBinary[wAttributes + 8];
                power = dwarvenBinary[wAttributes + 9];
                magic = dwarvenBinary[wAttributes + 10];
                good = dwarvenBinary[wAttributes + 11];
                evil = dwarvenBinary[wAttributes + 12];
                cold = dwarvenBinary[wAttributes + 13];
                minStrength = dwarvenBinary[wAttributes + 14];
                minDexterity = dwarvenBinary[wAttributes + 15];
                hp = dwarvenBinary[wAttributes + 16];
                maxHP = dwarvenBinary[wAttributes + 17];
                flags = dwarvenBinary[wAttributes + 18];
                parry = dwarvenBinary[wAttributes + 19];
            }

            if(itemType == 4)
            {
                itemType = 177; // ARX value for armour
                index = 0; // No longer required
                useStrength = 0;
                alignment = dwarvenBinary[offset + 3];
                weight = dwarvenBinary[offset + 4];

                wAttributes = (offset + dwarvenBinary[offset + 1]) - 15; // Working out from the end of the weapon object

                melee = dwarvenBinary[wAttributes + 1]; // Body part
                ammo = 0; // Not used
                blunt = dwarvenBinary[wAttributes + 2];
                sharp = dwarvenBinary[wAttributes + 3];
                earth = dwarvenBinary[wAttributes + 4];
                air = dwarvenBinary[wAttributes + 5];
                fire = dwarvenBinary[wAttributes + 6];
                water = dwarvenBinary[wAttributes + 7];
                power = dwarvenBinary[wAttributes + 8];
                magic = dwarvenBinary[wAttributes + 9];
                good = dwarvenBinary[wAttributes + 10];
                evil = dwarvenBinary[wAttributes + 11];
                cold = dwarvenBinary[wAttributes + 12];
                minStrength = 0;
                minDexterity = 0;
                hp = dwarvenBinary[wAttributes + 13];
                maxHP = dwarvenBinary[wAttributes + 14];
                flags = 0;
                parry = 0;
            }

            if(itemType == 2)
            {
                itemType = 199; // ARX value for ammo????
                index = 0; // No longer required
                useStrength = 0;
                alignment = dwarvenBinary[offset + 3];
                weight = dwarvenBinary[offset + 4];

                wAttributes = (offset + dwarvenBinary[offset + 1]) - 3; // Working out from the end of the weapon object

                melee = dwarvenBinary[wAttributes + 1];
                ammo = dwarvenBinary[wAttributes + 2];
                blunt = dwarvenBinary[wAttributes + 3];
                sharp = 0; // Set to 0 for non use
                earth = 0;
                air = 0;
                fire = 0;
                water = 0;
                power = 0;
                magic = 0;
                good = 0;
                evil = 0;
                cold = 0;
                minStrength = 0;
                minDexterity = 0;
                hp = 0;
                maxHP = 0;
                flags = 0;
                parry = 0;
            }

            var newItemRef = CreateItem(itemType,
                                        index,
                                        itemName,
                                        hp,
                                        maxHP,
                                        flags,
                                        minStrength,
                                        minDexterity,
                                        useStrength,
                                        blunt,
                                        sharp,
                                        earth,
                                        air,
                                        fire,
                                        water,
                                        power,
                                        magic,
                                        good,
                                        evil,
                                        cold,
                                        weight,
                                        alignment,
                                        melee,
                                        ammo,
                                        parry);
            itemBuffer[newItemRef].location = 10; // Add to player inventory - 10
        }

        public static void DeductGems(int totalGems)
        {
            // Deducts from gems before jewels
            if (plyr.gems >= totalGems)
                plyr.gems -= totalGems;
            else
            {
                totalGems -= plyr.gems;
                plyr.gems = 0;
                plyr.jewels -= totalGems;
            }
        }

        public static void DisplayDwarvenModuleText()
        {
            if (dmenu == (int)DwarvenSmithyMenus.MenuMain)
            {
                var dgreetingText = $"Welcome to my forge, {plyr.name}!";
                CyText(1, dgreetingText);
                BText(6, 3, "(1) Examine my wares");
                BText(6, 4, "(2) Sell weapons or armor");
                BText(6, 5, "(3) Have a custom weapon made");
                BText(6, 6, "(0) Leave");
            } else if(dmenu == (int)DwarvenSmithyMenus.MenuPreOffer)
            {
                CyText(3, "What do you offer to sell?");
            } else if(dmenu == (int)DwarvenSmithyMenus.MenuSelectOffer)
            {
                itemRef = SelectItem(3);
                if(itemRef == 9999)
                    dmenu = (int)DwarvenSmithyMenus.MenuMain; // No selection made
                if((itemRef > 999) && (itemRef < 1012))
                    dmenu = (int)DwarvenSmithyMenus.MenuOfferRefused;
                if(itemRef < 101)
                {                 
                    int itemType = itemBuffer[itemRef].type;
                    itemDesc = itemBuffer[itemRef].name;
                    if((itemType == 177) || (itemType == 178))
                    {
                        CalculateSaleItemValue(itemRef);
                        dmenu = (int)DwarvenSmithyMenus.MenuSmithyMakesOffer;
                    } else
                    {
                        dmenu = (int)DwarvenSmithyMenus.MenuOfferRefused;
                    }
                }
            } else if(dmenu == (int)DwarvenSmithyMenus.MenuOfferRefused)
            {
                var str = $"@@Sorry, but I'm not interested in your@@{itemDesc}.";
                CyText(1, str);
            } else if(dmenu == (int)DwarvenSmithyMenus.MenuSmithyMakesOffer)
            {
                var str = $"@@I will give you {Itos(itemValue)} silvers for@@your {itemDesc}.@@Okay? (Y or N)";
                CyText(1, str);
            } else if(dmenu == (int)DwarvenSmithyMenus.MenuNoFunds)
            {
                var str = "@@That's more than you have.";
                CyText(1, str);
            } else if(dmenu == (int)DwarvenSmithyMenus.MenuNoHaggle)
            {
                var str = "Who do you think I am?  Omar?!@@You'll do no haggling with me!";
                CyText(1, str);
            } else if(dmenu == (int)DwarvenSmithyMenus.MenuAnythingElse)
            {
                var itemText = itemNames[smithyChoice];
                var str = $"@I'm sure that the {itemText}@will be to your liking@@@Will there be anything else?@@(Y or N)";
                CyText(1, str);
            } else if(dmenu == (int)DwarvenSmithyMenus.MenuAnythingElse2)
            {
                var itemText = itemNames[smithyChoice];
                var str = $"@Here's the {itemText}@@@@@Will there be anything else?@@(Y or N)";
                CyText(1, str);
            } else if(dmenu == (int)DwarvenSmithyMenus.MenuCustom)
            {
                CyText(1, "What type of weapon are you@interested in?");
                BText(13, 4, "(1) Sword");
                BText(13, 5, "(2) Axe");
                BText(13, 6, "(3) Mace");
                BText(13, 7, "(4) Hammer");
                BText(13, 8, "(0) Not interested");
            } else if(dmenu == (int)DwarvenSmithyMenus.MenuCustomOrdered)
            {
                var str = $"Return in four days for your {customWeaponDesc}.@@It will be forged by then.";
                CyText(1, str);
            } else if(dmenu == (int)DwarvenSmithyMenus.MenuBusyForging)
            {
                var dayText = $"{Itos(plyr.forgeDays)} days";
                if(plyr.forgeDays == 1)
                    dayText = "1 day";

                var str = $"Sorry, but I'll be busy for {dayText} yet,@@forging and inscribing your weapon.@@I shall see you then.";
                CyText(1, str);
            } else if(dmenu == (int)DwarvenSmithyMenus.MenuCustomReady)
            {
                var str = $"Welcome {plyr.name}!@@ I have your custom weapon right here!@@It is indeed a mighty weapon!";
                CyText(1, str);
            } else if(dmenu == (int)DwarvenSmithyMenus.MenuNoNameProvided)
            {
                var str = $"Very well then, I will simply call@@it the {plyr.forgeName}.";
                CyText(1, str);
            }
        }        

        public static void LoadDwarvenBinary()
        {
            // Loads armour and weapons binary data into the "dwarvenBinary" array
            var tempString = $"{"data/map/"}{"DwarvenItems.bin"}";
            var fp = fopen(tempString, "rb");
            if(fp != null)
            {
                for(var i = 0; i < dwarvenFileSize; i++)
                    dwarvenBinary[i] = fgetc(fp);
            }
            fclose(fp);
        }

        public static void MakeCustomWeaponOffer()
        {
            var custonWeaponMinimum = 0;
            if(customWeaponType == 1)
            {
                customWeaponDesc = "sword";
                custonWeaponMinimum = 180;
            }
            if(customWeaponType == 2)
            {
                customWeaponDesc = "axe";
                custonWeaponMinimum = 150;
            }
            if(customWeaponType == 3)
            {
                customWeaponDesc = "mace";
                custonWeaponMinimum = 110;
            }
            if(customWeaponType == 4)
            {
                customWeaponDesc = "hammer";
                custonWeaponMinimum = 90;
            }

            var str = $"I ask at least {Itos(custonWeaponMinimum)} gems or jewels for a@high-quality custom made {customWeaponDesc}.@How much are you prepared to offer?";
            var gemsJewelsOffer = InputNumber(str);

            var playerGems = CalculateGemsAndJewelsTotal();
            if(gemsJewelsOffer < custonWeaponMinimum)
            {
                if (gemsJewelsOffer == 0)
                    dmenu = (int)DwarvenSmithyMenus.MenuMain;
                else
                    dmenu = (int)DwarvenSmithyMenus.MenuNoHaggle;
            } else
            {
                if (playerGems >= gemsJewelsOffer)
                {
                    DeductGems(gemsJewelsOffer);

                    plyr.forgeBonus = 0;

                    var additionalGemsOffered = gemsJewelsOffer - custonWeaponMinimum;
                    CalculateForgeBonus(additionalGemsOffered);
                    
                    var str = $"@By what name do you wish your mighty@@{customWeaponDesc} to be called?";
                    plyr.forgeName = InputText(str);
                    if (plyr.forgeName == "")
                    {
                        if(customWeaponType == 1)
                            plyr.forgeName = "Dwarven Sword";
                        if(customWeaponType == 2)
                            plyr.forgeName = "Dwarven Axe";
                        if(customWeaponType == 3)
                            plyr.forgeName = "Dwarven Mace";
                        if(customWeaponType == 4)
                            plyr.forgeName = "Dwarven Hammer";
                        dmenu = (int)DwarvenSmithyMenus.MenuNoNameProvided;
                    } else
                    {
                        dmenu = (int)DwarvenSmithyMenus.MenuCustomOrdered;
                    }
                    plyr.forgeDays = 4;
                    plyr.forgeType = customWeaponType;
                } else
                {
                    dmenu = (int)DwarvenSmithyMenus.MenuNoFunds;
                }
            }
        }

        public static void ProcessDwarvenMenuInput()
        {
            var key = ReadKey();

            switch(dmenu)
            {
                case DwarvenSmithyMenus.MenuMain:
                    if(key == "0")
                        dmenu = (int)DwarvenSmithyMenus.MenuLeft;
                    if(key == "1")
                        dmenu = (int)DwarvenSmithyMenus.MenuChooseSmithyItem;
                    if(key == "2")
                        dmenu = (int)DwarvenSmithyMenus.MenuPreOffer;
                    if(key == "3")
                        dmenu = (int)DwarvenSmithyMenus.MenuCustom;
                    if(key == "down")
                        dmenu = (int)DwarvenSmithyMenus.MenuLeft;
                    break;
                case DwarvenSmithyMenus.MenuChooseSmithyItem:
                    ChooseDwarvenSmithyItem();
                    break;
                case DwarvenSmithyMenus.MenuPreOffer:
                    if(key != "")
                        dmenu = (int)DwarvenSmithyMenus.MenuSelectOffer;
                    break;
                case DwarvenSmithyMenus.MenuOfferRefused:
                    if(key != "")
                        dmenu = (int)DwarvenSmithyMenus.MenuMain;
                    break;
                case DwarvenSmithyMenus.MenuNoFunds:
                    if(key != "")
                        dmenu = (int)DwarvenSmithyMenus.MenuMain;
                    break;
                case DwarvenSmithyMenus.MenuSmithyMakesOffer:
                    if(key == "N")
                        dmenu = (int)DwarvenSmithyMenus.MenuMain;
                    if(key == "Y")
                    {
                        ProcessPayment();
                        dmenu = (int)DwarvenSmithyMenus.MenuMain;
                    }
                    if(key == "0")
                        dmenu = (int)DwarvenSmithyMenus.MenuMain;
                    break;
                case DwarvenSmithyMenus.MenuAnythingElse:
                    if(key == "N")
                        dmenu = (int)DwarvenSmithyMenus.MenuMain;
                    if(key == "Y")
                        dmenu = (int)DwarvenSmithyMenus.MenuChooseSmithyItem;
                    break;
                case DwarvenSmithyMenus.MenuAnythingElse2:
                    if(key == "N")
                        dmenu = (int)DwarvenSmithyMenus.MenuMain;
                    if(key == "Y")
                        dmenu = (int)DwarvenSmithyMenus.MenuChooseSmithyItem;
                    break;
                case DwarvenSmithyMenus.MenuCustom:
                    if(key == "0")
                        dmenu = (int)DwarvenSmithyMenus.MenuMain;
                    if(key == "1")
                    {
                        customWeaponType = 1;
                        MakeCustomWeaponOffer();
                    }
                    if(key == "2")
                    {
                        customWeaponType = 2;
                        MakeCustomWeaponOffer();
                    }
                    if(key == "3")
                    {
                        customWeaponType = 3;
                        MakeCustomWeaponOffer();
                    }
                    if(key == "4")
                    {
                        customWeaponType = 4;
                        MakeCustomWeaponOffer();
                    }
                    break;
                case DwarvenSmithyMenus.MenuCustomOrdered:
                    if(key != "")
                        dmenu = (int)DwarvenSmithyMenus.MenuLeft;
                    break;
                case DwarvenSmithyMenus.MenuBusyForging:
                    if(key == "SPACE")
                        dmenu = (int)DwarvenSmithyMenus.MenuLeft;
                    break;
                case DwarvenSmithyMenus.MenuCustomReady:
                    if(key == "SPACE")
                    {
                        CreateCustomWeapon();
                        dmenu = (int)DwarvenSmithyMenus.MenuMain;
                    }
                    break;
                case DwarvenSmithyMenus.MenuNoHaggle:
                    if(key == "SPACE")
                        dmenu = (int)DwarvenSmithyMenus.MenuMain;
                    break;
                case DwarvenSmithyMenus.MenuNoNameProvided:
                    if(key == "SPACE")
                        dmenu = (int)DwarvenSmithyMenus.MenuCustomOrdered;
                    break;
            }
        }

        public static void ProcessPayment()
        {
            // Check equipped items
            if(plyr.priWeapon == itemRef)
                plyr.priWeapon = 0;
            if(plyr.secWeapon == itemRef)
                plyr.secWeapon = 0;
            if(plyr.armsArmour == itemRef)
                plyr.armsArmour = 255;
            if(plyr.legsArmour == itemRef)
                plyr.legsArmour = 255;
            if(plyr.bodyArmour == itemRef)
                plyr.bodyArmour = 255;
            if(plyr.headArmour == itemRef)
                plyr.headArmour = 255;

            // Remove item from inventory
            MoveItem(itemRef, 0);

            // Add silvers
            plyr.silver += itemValue;
        }

        public static string ReadDwarvenNameString(int stringOffset)
        {
            var ss = new stringstream();
            var z = stringOffset; // current location in the binary

            while (!(dwarvenBinary[z] == 0))
            {
                var c = dwarvenBinary[z];
                ss << (char) c;
                z++;
            }
            
            return ss.str();
        }

        public static void RunDwarvenSmithy()
        {
            if (plyr.forgeDays > 0)
                dmenu = (int)DwarvenSmithyMenus.MenuBusyForging;
            else
                dmenu = (int)DwarvenSmithyMenus.MenuMain;
            if((plyr.forgeDays == 0) && (plyr.forgeType > 0))
                dmenu = (int)DwarvenSmithyMenus.MenuCustomReady;

            AddDwarvenSmithyToMap();
            LoadShopImage(26);

            BuildSmithyMenuOptions();

            while(dmenu != (int)DwarvenSmithyMenus.MenuLeft)
            {
                ClearShopDisplay();
                DisplayDwarvenModuleText();
                UpdateDisplay();
                ProcessDwarvenMenuInput();
            }
        }

        //extern byte dwarvenBinary[dwarvenFileSize];
    }
}