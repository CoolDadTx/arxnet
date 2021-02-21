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

using SFML.Audio;

namespace P3Net.Arx
{
    public static partial class GlobalMembers
    {        
        //TODO: Combine with core load and move to data file
        public static void LoadCitySmithyBinary ()
        {
            //TODO: Ignoring fixed length - citySmithyFileSize
            // Loads armour and weapons binary data into the "citySmithyBinary" array
            citySmithyBinary = File.ReadAllBytes($"data/map/smithyItems.bin");
        }
        
        public static void StockSmithyWares ()
        {
            // Run each day to randomly pick 10 items for sale at each of the 4 smithies
            // Check for duplicates using smithyWaresCheck array of bools

            // Set bools for duplicate items check to false
            var itemNo = 0;

            for (var x = 0; x < 4; x++)
            {
                for (var y = 0; y < 23; y++)
                    smithyWaresCheck[x, y] = false;
            }

            for (var smithyNo = 0; smithyNo < 4; smithyNo++)
            {
                for (var waresNo = 0; waresNo < 10; waresNo++)
                {
                    // Current code may create duplicate items in each smithy
                    var uniqueItem = false;
                    while (!uniqueItem)
                    {
                        itemNo = Random(0, 22);

                        if (!smithyWaresCheck[smithyNo, itemNo])
                        {
                            smithyDailyWares[smithyNo, waresNo] = itemNo; // its not a duplicate
                            smithyWaresCheck[smithyNo, itemNo] = true;
                            uniqueItem = true;
                        }
                    }
                }
            }

            // Simple sort of items in numeric order
            sort(smithyDailyWares);

            // Always make sure a stiletto will be available
            smithyDailyWares[0, 0] = 0;
            smithyDailyWares[1, 0] = 0;
            smithyDailyWares[2, 0] = 0;
            smithyDailyWares[3, 0] = 0;
        }

        public static void ShopSmithy ()
        {
            if (plyr.timeOfDay == 1)
                LoadShopImage(8);
            else
                LoadShopImage(9);

            var offerStatus = 0; // 0 is normal, 1 is demanding, 2 is bartering
            var offerRounds = 0;
            var itemLowestCost = 0;
            var smithyOffer = 0;

            if (plyr.timeOfDay == 1)
                LoadShopImage(8);
            else
                LoadShopImage(9);

            smithyNo = GetSmithyNo();
            var musicPlaying = false;
            var smithyMenu = 1; // high level menu

            plyr.status = GameStates.Module; // shopping
            menuStartItem = 0; // menu starts at item 0
            if ((Smithies[smithyNo].closingHour <= plyr.hours) || (Smithies[smithyNo].openingHour > plyr.hours))
                smithyMenu = 5;

            while (smithyMenu > 0)
            {
                while (smithyMenu == 5) // closed
                {
                    SmithyDisplayUpdate();
                    CyText(1, "Sorry, we are closed. Come back@during our working hours.");
                    var str = $"We are open from {Smithies[smithyNo].openingHour}:00 in the morning@to {Smithies[smithyNo].closingHour}:00 in the evening.";
                    if (Smithies[smithyNo].closingHour == 15)
                        str = $"We are open from {Smithies[smithyNo].openingHour}:00 in the morning@to {Smithies[smithyNo].closingHour}:00 in the afternoon.";
                    CyText(4, str);
                    CyText(9, "( Press a key )");
                    UpdateDisplay();

                    var key = GetSingleKey();
                    if ((key != "") && (key != "up"))
                        smithyMenu = 0;
                }

                while (smithyMenu == 1) // main menu
                {
                    SmithyDisplayUpdate();
                    BText(13, 0, "Welcome Stranger!");
                    BText(7, 3, "Do you wish to see our wares?");
                    CyText(5, "( es or  o)");
                    SetFontColor(40, 96, 244, 255);
                    CyText(5, " Y      N  ");
                    SetFontColor(215, 215, 215, 255);
                    DisplayCoins();
                    UpdateDisplay();

                    if (!musicPlaying)
                    {
                        var file = plyr.musicStyle ? "data/audio/B/armor.ogg" : "data/audio/armor.ogg";

                        smithyMusic = new Music(file);
                        LoadLyrics("armor.txt");
                        smithyMusic.Play();
                        musicPlaying = true;
                    }

                    var key = GetSingleKey();
                    if (key == "Y")
                        smithyMenu = 2;
                    if (key == "N")
                        smithyMenu = 0;
                    if (key == "down")
                        smithyMenu = 0;
                }

                while (smithyMenu == 2)
                {
                    offerStatus = 0;
                    offerRounds = 0;
                    SmithyDisplayUpdate();
                    CyText(0, "What would you like? (  to leave)");
                    SetFontColor(40, 96, 244, 255);
                    CyText(0, "                      0          ");
                    SetFontColor(215, 215, 215, 255);

                    smithyNo = GetSmithyNo();
                    for (var i = 0; i < maxMenuItems; i++)
                    {
                        var itemNo = smithyDailyWares[smithyNo, menuStartItem + i];
                        BText(3, (2 + i), $") {smithyWares[itemNo].name}");
                        BText(1, (2 + i), "                                 coppers");
                    }
                    DisplayCoins();

                    for (var i = 0; i < maxMenuItems; i++) // Max number of item prices in this menu display
                    {
                        var x = 28;
                        var itemNo = smithyDailyWares[smithyNo, menuStartItem + i];

                        //MLT: Downcast to int
                        var itemCost = (int)(Smithies[smithyNo].initialPriceFactor * smithyWares[itemNo].basePrice);

                        if (itemCost < 1000)
                            x = 30;
                        if (itemCost > 999)
                            x = 28;
                        if (itemCost > 9999)
                            x = 27;
                        var itemCostDesc = ToCurrency(itemCost);
                        BText(x, (i + 2), itemCostDesc);
                    }

                    SetFontColor(40, 96, 244, 255);
                    BText(2, 2, "1");
                    BText(2, 3, "2");
                    BText(2, 4, "3");
                    BText(2, 5, "4");
                    BText(2, 6, "5");
                    BText(2, 7, "6");
                    if (menuStartItem != 0)
                        BText(2, 1, "}");
                    if (menuStartItem != 4)
                        BText(2, 8, "{");
                    SetFontColor(215, 215, 215, 255);

                    UpdateDisplay();

                    var key = GetSingleKey();
                    if (key == "1")
                    {
                        itemChoice = 0;
                        smithyMenu = 20;
                    }
                    if (key == "2")
                    {
                        itemChoice = 1;
                        smithyMenu = 20;
                    }
                    if (key == "3")
                    {
                        itemChoice = 2;
                        smithyMenu = 20;
                    }
                    if (key == "4")
                    {
                        itemChoice = 3;
                        smithyMenu = 20;
                    }
                    if (key == "5")
                    {
                        itemChoice = 4;
                        smithyMenu = 20;
                    }
                    if (key == "6")
                    {
                        itemChoice = 5;
                        smithyMenu = 20;
                    }
                    if ((key == "up") && (menuStartItem > 0))
                        menuStartItem--;
                    if ((key == "down") && (menuStartItem < 4))
                        menuStartItem++;
                    if (key == "ESC")
                        smithyMenu = 0;
                    if (key == "0")
                        smithyMenu = 0;
                }

                while (smithyMenu == 20) // buy item?
                {
                    var smithyNo = GetSmithyNo();
                    var itemNo = smithyDailyWares[smithyNo, menuStartItem + itemChoice];

                    //MLT: Downcast to int
                    itemCost = (int)(Smithies[smithyNo].initialPriceFactor * smithyWares[itemNo].basePrice);
                    var tempitemcost = Smithies[smithyNo].initialPriceFactor * smithyWares[itemNo].basePrice;
                    var temp = (tempitemcost / 100) * 75;

                    //MLT: Downcast to int
                    itemLowestCost = (int)temp;
                    smithyOffer = itemCost;
                    smithyMenu = 3;
                }

                while (smithyMenu == 3) // buy item
                {
                    SmithyDisplayUpdate();
                    if (offerStatus == 0)
                    {
                        CyText(0, $"The cost for {smithyWares[itemNo].name}");
                        CyText(1, $"is {ToCurrency(smithyOffer)} coppers. Agreed?");
                    }
                    if (offerStatus == 1)
                    {
                        CyText(1, $"I demand at least {ToCurrency(smithyOffer)} silvers!");
                    }
                    if (offerStatus == 2)
                    {
                        CyText(1, $"Would you consider {ToCurrency(smithyOffer)}?");
                    }

                    BText(11, 3, " ) Agree to price");
                    BText(11, 4, " ) Make an offer");
                    BText(11, 5, " ) No sale");
                    BText(11, 6, " ) Leave");
                    DisplayCoins();
                    SetFontColor(40, 96, 244, 255);
                    BText(11, 3, "1");
                    BText(11, 4, "2");
                    BText(11, 5, "3");
                    BText(11, 6, "0");
                    SetFontColor(215, 215, 215, 255);

                    UpdateDisplay();

                    var key = GetSingleKey();
                    if (key == "1")
                    {
                        if (!CheckCoins(0, 0, smithyOffer))
                            smithyMenu = 5;
                        else
                            smithyMenu = 4;
                    }

                    if (key == "2")
                        smithyMenu = 16;
                    if (key == "3")
                        smithyMenu = 2;
                    if (key == "0")
                        smithyMenu = 0;
                }

                while (smithyMenu == 16) // what is your offer
                {
                    var coppers = InputValue("What is your offer? (in coppers)", 9);

                    // check offer
                    if (coppers == 0)
                        smithyMenu = 2;

                    if (coppers >= itemCost)
                    {
                        smithyOffer = coppers; // accepted the players offer
                        offerStatus = 2;
                        smithyMenu = 20;
                    }
                    if ((coppers >= itemLowestCost) && (coppers < itemCost))
                    {
                        offerStatus = 2;
                        offerRounds++;
                        if (offerRounds > 2)
                        {
                            smithyOffer = coppers;
                            smithyMenu = 20;
                        } else
                        {
                            smithyOffer = Random(coppers, itemCost);
                            itemLowestCost = coppers;
                            smithyMenu = 3;
                        }
                    }
                    if ((coppers < itemLowestCost) && (coppers > 0))
                    {
                        offerStatus = 1;
                        offerRounds++;
                        smithyOffer = itemLowestCost;
                        smithyMenu = (offerRounds > 1) ? 19 : 3;
                    }
                }

                while (smithyMenu == 20) // Offer accepted (subject to funds check)
                {
                    SmithyDisplayUpdate();
                    CText("Agreed!");
                    UpdateDisplay();
                    var key = GetSingleKey();

                    if (key != "")
                    {
                        if (!CheckCoins(0, 0, smithyOffer))
                            smithyMenu = 5;
                        else
                        {
                            plyr.smithyFriendships[smithyNo]++;
                            if (plyr.smithyFriendships[smithyNo] > 4)
                                plyr.smithyFriendships[smithyNo] = 4;
                            smithyMenu = 4;
                        }
                    }
                }

                while (smithyMenu == 19) // Leave my shop
                {
                    SmithyDisplayUpdate();
                    CText("Leave my shoppe and don't return@@until you are ready to make a decent@@offer!");
                    UpdateDisplay();
                    var key = GetSingleKey();

                    if (key != "")
                    {
                        plyr.smithyFriendships[smithyNo]--;
                        if (plyr.smithyFriendships[smithyNo] < 0)
                            plyr.smithyFriendships[smithyNo] = 0;
                        smithyMenu = 0;
                    } // Thrown out
                }

                while (smithyMenu == 5) // insufficient funds!
                {
                    SmithyDisplayUpdate();
                    CText("THAT OFFENDS ME DEEPLY!@Why don't you get serious and only@agree to something that you can afford!");
                    CyText(9, "( Press a key )");
                    UpdateDisplay();
                    var key = GetSingleKey();

                    if (key != "")
                        smithyMenu = 2;
                }

                while (smithyMenu == 4) // Agree to buy item and have funds
                {
                    SmithyDisplayUpdate();
                    CText("An excellent choice!");
                    CyText(9, "( Press a key )");
                    UpdateDisplay();
                    var key = GetSingleKey();

                    if (key != "")
                    {
                        // Add a weight & inventory limit check prior to taking money
                        DeductCoins(0, 0, smithyOffer);
                        var objectNumber = smithyWares[itemNo].itemRef; // ref within Weapons array

                        if ((objectNumber > 10) || (objectNumber == 0))
                            // Weapon item
                            CreateCitySmithyInventoryItem(objectNumber);

                        // Create an armour set
                        if (objectNumber == 1)
                        {
                            // Padded armor set - buying group of items
                            CreateCitySmithyInventoryItem(0x1CA);
                            CreateCitySmithyInventoryItem(0x1EE);
                            CreateCitySmithyInventoryItem(0x217);
                            CreateCitySmithyInventoryItem(0x23E);
                        }
                        if (objectNumber == 2)
                        {
                            // Leather armor set - buying group of items
                            CreateCitySmithyInventoryItem(0x263);
                            CreateCitySmithyInventoryItem(0x288);
                            CreateCitySmithyInventoryItem(0x2B2);
                            CreateCitySmithyInventoryItem(0x2DA);
                        }
                        if (objectNumber == 3)
                        {
                            // Studded armor set - buying group of items
                            CreateCitySmithyInventoryItem(0x300);
                            CreateCitySmithyInventoryItem(0x325);
                            CreateCitySmithyInventoryItem(0x34F);
                            CreateCitySmithyInventoryItem(0x377);
                        }
                        if (objectNumber == 4)
                        {
                            // Ring mail set - buying group of items
                            CreateCitySmithyInventoryItem(0x39D);
                            CreateCitySmithyInventoryItem(0x3C1);
                            CreateCitySmithyInventoryItem(0x3E5);
                        }
                        if (objectNumber == 5)
                        {
                            // Scale mail set - buying group of items
                            CreateCitySmithyInventoryItem(0x40D);
                            CreateCitySmithyInventoryItem(0x432);
                            CreateCitySmithyInventoryItem(0x457);
                        }
                        if (objectNumber == 6)
                        {
                            // Splint mail set - buying group of items
                            CreateCitySmithyInventoryItem(0x480);
                            CreateCitySmithyInventoryItem(0x4A6);
                            CreateCitySmithyInventoryItem(0x4CC);
                        }
                        if (objectNumber == 7)
                        {
                            // Chain mail set - buying group of items
                            CreateCitySmithyInventoryItem(0x4F6);
                            CreateCitySmithyInventoryItem(0x51B);
                            CreateCitySmithyInventoryItem(0x540);
                        }
                        if (objectNumber == 8)
                        {
                            // Banded mail set - buying group of items
                            CreateCitySmithyInventoryItem(0x569);
                            CreateCitySmithyInventoryItem(0x58D);
                            CreateCitySmithyInventoryItem(0x5B6);
                            CreateCitySmithyInventoryItem(0x5DD);
                        }
                        if (objectNumber == 9)
                        {
                            // Plate mail set - buying group of items
                            CreateCitySmithyInventoryItem(0x602);
                            CreateCitySmithyInventoryItem(0x626);
                            CreateCitySmithyInventoryItem(0x64F);
                            CreateCitySmithyInventoryItem(0x676);
                        }

                        smithyMenu = 2; // back to purchases
                    }
                }
            }
            smithyMusic.Stop();
            LeaveShop();
        }

        #region Review Data

        public static byte[] citySmithyBinary = new byte[citySmithyFileSize];
        public static readonly int citySmithyFileSize = 1691;

        public static int itemChoice;
        public static int itemCost;
        public static int itemNo;
        public static int maxMenuItems = 6;
        public static int menuStartItem;

        //TODO: Move to a data file
        //MLT: Double to float
        public static Smithy[] Smithies =
        {
            new Smithy()
            {
                name = "Sharp Weaponsmiths",
                minimumPriceFactor = 1.25F,
                initialPriceFactor = 1.65F,
                location = 55,
                openingHour = 4,
                closingHour = 20
            },
            new Smithy()
            {
                name = "Occum's Weaponsmiths",
                minimumPriceFactor = 1.10F,
                initialPriceFactor = 1.35F,
                location = 56,
                openingHour = 5,
                closingHour = 21
            },
            new Smithy()
            {
                name = "Best Armorers",
                minimumPriceFactor = 1.50F,
                initialPriceFactor = 2.40F,
                location = 57,
                openingHour = 8,
                closingHour = 19
            },
            new Smithy()
            {
                name = "Knight's Armorers",
                minimumPriceFactor = 1.60F,
                initialPriceFactor = 2.35F,
                location = 58,
                openingHour = 11,
                closingHour = 15
            }
        };

        public static Music smithyMusic;
        public static int smithyNo;

        //TODO: Move to a data file
        public static SmithyItem[] smithyWares =
        {
            new SmithyItem()
            { name = "a Stiletto", type = 178, basePrice = 113, itemRef = 0xAA },
            new SmithyItem()
            { name = "a Dagger", type = 178, basePrice = 129, itemRef = 0xCB },
            new SmithyItem()
            { name = "a Whip", type = 178, basePrice = 396, itemRef = 0xE9 },
            new SmithyItem()
            { name = "a War Net", type = 178, basePrice = 908, itemRef = 0x24 },
            new SmithyItem()
            { name = "Padded Armor", type = 177, basePrice = 2200, itemRef = 1 },
            new SmithyItem()
            { name = "a Small Shield", type = 178, basePrice = 2460, itemRef = 0x86 },
            new SmithyItem()
            { name = "a Shortsword", type = 178, basePrice = 3146, itemRef = 0x105 },
            new SmithyItem()
            { name = "a Shield", type = 178, basePrice = 4290, itemRef = 0x68 },
            new SmithyItem()
            { name = "a Flail", type = 178, basePrice = 4620, itemRef = 0x128 },
            new SmithyItem()
            { name = "Leather Armor", type = 177, basePrice = 4840, itemRef = 2 },
            new SmithyItem()
            { name = "a Spiked Shield", type = 178, basePrice = 6160, itemRef = 0x43 },
            new SmithyItem()
            { name = "a Battle Axe", type = 178, basePrice = 16930, itemRef = 0x145 },
            new SmithyItem()
            { name = "Studded Armor", type = 177, basePrice = 7260, itemRef = 3 },
            new SmithyItem()
            { name = "a Sword", type = 178, basePrice = 7680, itemRef = 0x167 },
            new SmithyItem()
            { name = "a Tower Shield", type = 178, basePrice = 9488, itemRef = 0x0 },
            new SmithyItem()
            { name = "Ring Mail", type = 177, basePrice = 10010, itemRef = 4 },
            new SmithyItem()
            { name = "a Battle Hammer", type = 178, basePrice = 10285, itemRef = 0x184 },
            new SmithyItem()
            { name = "a Longsword", type = 178, basePrice = 11193, itemRef = 0x1A9 },
            new SmithyItem()
            { name = "Scale Mail", type = 177, basePrice = 14245, itemRef = 5 },
            new SmithyItem()
            { name = "Splint Mail", type = 177, basePrice = 18975, itemRef = 6 },
            new SmithyItem()
            { name = "Chain Mail", type = 177, basePrice = 24640, itemRef = 7 },
            new SmithyItem()
            { name = "Banded Armor", type = 177, basePrice = 32000, itemRef = 8 },
            new SmithyItem()
            { name = "Plate Armor", type = 177, basePrice = 41500, itemRef = 9 }
        };

        public static bool[,] smithyWaresCheck = new bool[4, 23]; // markers used to check for duplicate items

        #endregion 

        #region Private Members

        //TODO: Move to a data file
        private static void CreateCitySmithyInventoryItem ( int startByte )
        {
            // Take a binary offset within citySmithyBinary and create a new inventory item from the binary data (weapon or armour)
            // Item types:  0x83 - weapon, 0x84 - armour

            int index = 0;
            int alignment = 0;
            int weight = 0;
            int wAttributes = 0;
            int melee = 0;
            int ammo = 0;
            int blunt = 0;
            int sharp = 0;
            int earth = 0;
            int air = 0;
            int fire = 0;
            int water = 0;
            int power = 0;
            int magic = 0;
            int good = 0;
            int evil = 0;
            int cold = 0;
            int minStrength = 0;
            int minDexterity = 0;
            int hp = 0;
            int maxHP = 0;
            int flags = 0;
            int parry = 0;
            int useStrength = 0;

            var offset = startByte;
            var itemType = citySmithyBinary[offset];
            var itemName = ReadSmithyItemString((offset + 6));

            if (itemType == 0x83)
            {
                itemType = 178; // ARX value for weapon
                index = 0; // No longer required
                useStrength = 0;
                alignment = citySmithyBinary[offset + 3];
                weight = citySmithyBinary[offset + 4];

                wAttributes = (offset + citySmithyBinary[offset + 1]) - 19; // Working out from the end of the weapon object

                melee = 0xFF;
                ammo = 0;
                blunt = citySmithyBinary[wAttributes + 3];
                sharp = citySmithyBinary[wAttributes + 4];
                earth = citySmithyBinary[wAttributes + 5];
                air = citySmithyBinary[wAttributes + 6];
                fire = citySmithyBinary[wAttributes + 7];
                water = citySmithyBinary[wAttributes + 8];
                power = citySmithyBinary[wAttributes + 9];
                magic = citySmithyBinary[wAttributes + 10];
                good = citySmithyBinary[wAttributes + 11];
                evil = citySmithyBinary[wAttributes + 12];
                cold = 0; // No cold damage for City items

                //TODO: What should this be for?
                //citySmithyBinary[wAttributes + 13];
                minStrength = citySmithyBinary[wAttributes + 13];
                minDexterity = citySmithyBinary[wAttributes + 14];
                hp = 44;
                maxHP = 44;
                flags = citySmithyBinary[wAttributes + 17];
                parry = citySmithyBinary[wAttributes + 18];
            }

            if (itemType == 0x84)
            {
                itemType = 177; // ARX value for armour
                index = 0; // No longer required
                useStrength = 0;
                alignment = citySmithyBinary[offset + 3];
                weight = citySmithyBinary[offset + 4];

                wAttributes = (offset + citySmithyBinary[offset + 1]) - 17; // Working out from the end of the weapon object

                melee = citySmithyBinary[wAttributes + 13]; // Body part
                if (melee == 1)
                    melee = 0;
                if (melee == 2)
                    melee = 1;
                if (melee == 4)
                    melee = 2;
                if (melee == 8)
                    melee = 3;
                if (melee == 6)
                    melee = 1;

                ammo = 0; // Not used
                blunt = citySmithyBinary[wAttributes + 2]; // ERROR ONWARDS
                sharp = citySmithyBinary[wAttributes + 3];
                earth = citySmithyBinary[wAttributes + 4];
                air = citySmithyBinary[wAttributes + 5];
                fire = citySmithyBinary[wAttributes + 6];
                water = citySmithyBinary[wAttributes + 7];
                power = citySmithyBinary[wAttributes + 8];
                magic = citySmithyBinary[wAttributes + 9];
                good = citySmithyBinary[wAttributes + 10];
                evil = citySmithyBinary[wAttributes + 11];
                cold = 0;
                minStrength = 0;
                minDexterity = 0;
                hp = 56;
                maxHP = 56;
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

        //TODO: Return Smithy instead
        private static int GetSmithyNo ()
        {
            var smithy_no = 0;
            for (var i = 0; i < Smithies.Length; i++) 
            {
                if (Smithies[i].location == plyr.location)
                    smithy_no = i; // The number of the smithy you have entered
            }
            return smithy_no;
        }

        
        private static string ReadSmithyItemString ( int stringOffset ) => ReadBinaryString(citySmithyBinary, stringOffset);        

        private static void SmithyDisplayUpdate ()
        {
            clock1.Restart();
            ClearShopDisplay();
            UpdateLyrics();
            iCounter++;
        }

        #endregion
    }
}