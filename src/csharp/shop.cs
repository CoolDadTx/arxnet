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
    public class ShopClothingItem
    {
        public int itemRef { get; set; }

        public int price { get; set; }

        public int type { get; set; } // 180 - clothing?
    }

    public class Shop
    {
        public int closingHour { get; set; }

        public float initialPriceFactor { get; set; }

        public int location { get; set; } // match with location text description number

        public float minimumPriceFactor { get; set; }

        public string name { get; set; }

        public int openingHour { get; set; }
    }

    public partial class GlobalMembers
    {
        public static ShopClothingItem[] shopClothingWares =
        {
            new ShopClothingItem()
            { type = 180, price = 112, itemRef = 13 },
            new ShopClothingItem()
            { type = 180, price = 117, itemRef = 14 },
            new ShopClothingItem()
            { type = 180, price = 117, itemRef = 15 },
            new ShopClothingItem()
            { type = 180, price = 1252, itemRef = 16 },
            new ShopClothingItem()
            { type = 180, price = 162, itemRef = 17 },
            new ShopClothingItem()
            { type = 180, price = 112, itemRef = 18 },
            new ShopClothingItem()
            { type = 180, price = 117, itemRef = 19 },
            new ShopClothingItem()
            { type = 180, price = 122, itemRef = 20 },
            new ShopClothingItem()
            { type = 180, price = 1222, itemRef = 21 },
            new ShopClothingItem()
            { type = 180, price = 162, itemRef = 22 },
            new ShopClothingItem()
            { type = 180, price = 162, itemRef = 23 },
            new ShopClothingItem()
            { type = 180, price = 182, itemRef = 24 }
        };

        //MLT: Double to float
        public static Shop[] Shops =
        {
            new Shop()
            {
                name = "Smiley's Shop",
                minimumPriceFactor = 1.00F,
                initialPriceFactor = 1.53F,
                location = 40,
                openingHour = 8,
                closingHour = 20
            },
            new Shop()
            {
                name = "Honest Trader",
                minimumPriceFactor = 1.10F,
                initialPriceFactor = 1.75F,
                location = 41,
                openingHour = 8,
                closingHour = 21
            },
            new Shop()
            {
                name = "Adventurer's Outfitters",
                minimumPriceFactor = 0.95F,
                initialPriceFactor = 1.35F,
                location = 42,
                openingHour = 8,
                closingHour = 19
            },
            new Shop()
            {
                name = "Warrior's Supplies",
                minimumPriceFactor = 0.80F,
                initialPriceFactor = 1.10F,
                location = 43,
                openingHour = 5,
                closingHour = 17
            },
            new Shop()
            {
                name = "General Store",
                minimumPriceFactor = 0.90F,
                initialPriceFactor = 1.29F,
                location = 44,
                openingHour = 5,
                closingHour = 23
            },
            new Shop()
            {
                name = "Exclusive Outfitters",
                minimumPriceFactor = 1.10F,
                initialPriceFactor = 1.85F,
                location = 45,
                openingHour = 10,
                closingHour = 15
            },
            new Shop()
            {
                name = "Rocky's Emporium",
                minimumPriceFactor = 1.00F,
                initialPriceFactor = 1.53F,
                location = 46,
                openingHour = 9,
                closingHour = 17
            },
            new Shop()
            {
                name = "Best Bargain Store",
                minimumPriceFactor = 1.10F,
                initialPriceFactor = 1.85F,
                location = 47,
                openingHour = 9,
                closingHour = 21
            },
            new Shop()
            {
                name = "Special Imports Store",
                minimumPriceFactor = 0.90F,
                initialPriceFactor = 1.55F,
                location = 48,
                openingHour = 10,
                closingHour = 14
            },
            new Shop()
            {
                name = "Betelgeuse Sales",
                minimumPriceFactor = 0.90F,
                initialPriceFactor = 1.43F,
                location = 49,
                openingHour = 3,
                closingHour = 22
            },
            new Shop()
            {
                name = "Merchant's Grotto",
                minimumPriceFactor = 1.00F,
                initialPriceFactor = 1.64F,
                location = 50,
                openingHour = 4,
                closingHour = 19
            },
            new Shop()
            {
                name = "Sunset Market",
                minimumPriceFactor = 1.00F,
                initialPriceFactor = 1.53F,
                location = 51,
                openingHour = 9,
                closingHour = 19
            },
            new Shop()
            {
                name = "Pauline's Emporium",
                minimumPriceFactor = 0.95F,
                initialPriceFactor = 1.35F,
                location = 52,
                openingHour = 11,
                closingHour = 16
            },
            new Shop()
            {
                name = "Da Place!",
                minimumPriceFactor = 0.82F,
                initialPriceFactor = 1.12F,
                location = 53,
                openingHour = 8,
                closingHour = 17
            },
            new Shop()
            {
                name = "Trade Winds",
                minimumPriceFactor = 0.95F,
                initialPriceFactor = 1.70F,
                location = 54,
                openingHour = 8,
                closingHour = 17
            }
        };

        // There are 16 possible items that could be in stock but only 12 unique items per day per shop
        public static bool[,] shopWaresCheck = new bool[15, 16]; // markers used to check for duplicate items created during daily stocking up process

        public static int GetShopNo ()
        {
            var shop_no = 255;
            for (var i = 0; i < 15; i++) // Max number of smithy objects
            {
                if (Shops[i].location == plyr.location)
                    shop_no = i; // The number of the shop you have entered
            }
            return shop_no;
        }

        public static void ShopMessage ( string txt )
        {
            var key = "";
            do
            {
                ClearShopDisplay();
                CText(txt);
                UpdateDisplay();
                key = GetSingleKey();
            } while (key != "SPACE");
        }

        public static void ShopShop ()
        {
            var shopNo = GetShopNo();
            // copies wares into local data structure for easier display as list
            for (var i = 0; i < 12; i++)
            {
                shopClothingWares[i].itemRef = shopDailyWares[shopNo, i];
                
                var clothingRef = shopDailyWares[shopNo, i];
                shopClothingWares[i].price = clothingItems[clothingRef].quality; // quality used for price just now
            }

            var shopMenu = 1; // high level menu

            plyr.status = GameStates.Module; // shopping

            int itemChoice = 0;
            int itemLowestCost = 0;
            int shopOffer = 0;
            var menuStartItem = 0;
            var offerStatus = 0; // 0 is normal, 1 is demanding, 2 is bartering
            var offerRounds = 0;

            LoadShopImage(12);

            if ((Shops[shopNo].closingHour <= plyr.hours) || (Shops[shopNo].openingHour > plyr.hours))
                shopMenu = 50;

            while (shopMenu > 0)
            {
                while (shopMenu == 1) // main menu
                {
                    ClearShopDisplay();
                    BText(13, 0, "Welcome Stranger!");
                    BText(7, 3, "Do you wish to see our wares?");
                    CyText(5, "( es or  o)");
                    SetFontColour(40, 96, 244, 255);
                    CyText(5, " Y      N  ");
                    SetFontColour(215, 215, 215, 255);
                    DisplayCoins();
                    UpdateDisplay();

                    var key = GetSingleKey();

                    if (key == "N")
                        shopMenu = 2;
                    if (key == "Y")
                        shopMenu = 3;
                    if (key == "down")
                        shopMenu = 2;
                }

                while (shopMenu == 2) // Buy a compass?
                {
                    ClearShopDisplay();
                    CyText(1, "Well then, How about a compass?");
                    CyText(3, "I charge only 5 silvers for it.");
                    CyText(5, "( es or  o)");
                    SetFontColour(40, 96, 244, 255);
                    CyText(5, " Y      N  ");
                    SetFontColour(215, 215, 215, 255);
                    DisplayCoins();
                    UpdateDisplay();

                    var key = GetSingleKey();
                    if (key == "Y")
                    {
                        if (CheckCoins(0, 5, 0))
                        {
                            ShopMessage("Right away!");
                            plyr.compasses++;
                            DeductCoins(0, 5, 0);
                            shopMenu = 0;
                        } else
                        {
                            ShopMessage("THAT OFFENDS ME DEEPLY!@Why don't you get serious@and only agree for something@you can afford!");
                            // Adjust friendship?
                            shopMenu = 0;
                        }
                    }
                    if (key == "N")
                        shopMenu = 0;
                }

                while (shopMenu == 3)
                {
                    offerStatus = 0;
                    offerRounds = 0;
                    var maxMenuItems = 6;

                    ClearShopDisplay();

                    CyText(0, "What would you like? (  to leave)");
                    SetFontColour(40, 96, 244, 255);
                    CyText(0, "                      0          ");
                    SetFontColour(215, 215, 215, 255);

                    for (var i = 0; i < maxMenuItems; i++)
                    {
                        var itemNo = menuStartItem + i;

                        str = $"( ) {clothingItems[shopClothingWares[itemNo].itemRef].name}";

                        BText(1, (2 + i), str);
                    }
                    DisplayCoins();

                    for (var i = 0; i < maxMenuItems; i++) // Max number of item prices in this menu display
                    {
                        var itemNo = menuStartItem + i;
                        var itemCost = shopClothingWares[itemNo].price;

                        var x = 0;
                        if (itemCost < 10000)
                            x = 34;
                        if (itemCost < 1000)
                            x = 36;
                        if (itemCost < 100)
                            x = 37;

                        var itemCostDesc = ToCurrency(itemCost);
                        BText(x + 2, (i + 2), itemCostDesc);
                    }

                    SetFontColour(40, 96, 244, 255);
                    BText(2, 2, "1");
                    BText(2, 3, "2");
                    BText(2, 4, "3");
                    BText(2, 5, "4");
                    BText(2, 6, "5");
                    BText(2, 7, "6");
                    if (menuStartItem != 0)
                        BText(2, 1, "}");
                    if (menuStartItem != 6)
                        BText(2, 8, "{");
                    SetFontColour(215, 215, 215, 255);

                    UpdateDisplay();

                    var key = GetSingleKey();
                    if (key == "1")
                    {
                        itemChoice = 0;
                        shopMenu = 22;
                    }
                    if (key == "2")
                    {
                        itemChoice = 1;
                        shopMenu = 22;
                    }
                    if (key == "3")
                    {
                        itemChoice = 2;
                        shopMenu = 22;
                    }
                    if (key == "4")
                    {
                        itemChoice = 3;
                        shopMenu = 22;
                    }
                    if (key == "5")
                    {
                        itemChoice = 4;
                        shopMenu = 22;
                    }
                    if (key == "6")
                    {
                        itemChoice = 5;
                        shopMenu = 22;
                    }
                    if ((key == "up") && (menuStartItem > 0))
                        menuStartItem--;
                    if ((key == "down") && (menuStartItem < 6))
                        menuStartItem++;
                    if (key == "ESC")
                        shopMenu = 0;
                    if (key == "0")
                        shopMenu = 2;
                }

                while (shopMenu == 22) // buy item?
                {
                    var itemNo = menuStartItem + itemChoice;
                    var itemCost = shopClothingWares[itemNo].price;

                    //MLT: Double to float
                    var tempitemcost = (float)shopClothingWares[itemNo].price;
                    var temp = (tempitemcost / 100F) * 75;

                    //MLT: Downcast to int
                    itemLowestCost = (int)temp;
                    shopOffer = itemCost;
                    shopMenu = 23;
                }

                while (shopMenu == 23) // buy item?
                {
                    ClearShopDisplay();
                    if (offerStatus == 0)
                    {
                        CyText(0, $"The cost for {clothingItems[shopClothingWares[itemNo].itemRef].name}");
                        CyText(1, $"is {ToCurrency(shopOffer)} coppers. Agreed?");
                    }
                    if (offerStatus == 1)
                    {
                        CyText(1, $"I demand at least {ToCurrency(shopOffer)} coppers!");
                    }
                    if (offerStatus == 2)
                    {
                        CyText(1, $"Would you consider {ToCurrency(shopOffer)}?");
                    }

                    BText(11, 3, " ) Agree to price");
                    BText(11, 4, " ) Make an offer");
                    BText(11, 5, " ) Select other apparel");
                    BText(11, 6, " ) Buy something else");
                    DisplayCoins();
                    SetFontColour(40, 96, 244, 255);
                    BText(11, 3, "1");
                    BText(11, 4, "2");
                    BText(11, 5, "3");
                    BText(11, 6, "0");
                    SetFontColour(215, 215, 215, 255);

                    UpdateDisplay();

                    var key = GetSingleKey();
                    if (key == "1")
                    {
                        if (!CheckCoins(0, 0, shopOffer))
                            shopMenu = 25;
                        else
                            shopMenu = 24;
                    }
                    if (key == "2")
                        shopMenu = 26;
                    if (key == "3")
                        shopMenu = 21;
                    if (key == "0")
                        shopMenu = 1;
                }

                while (shopMenu == 24) // Agree to buy item and have funds
                {
                    var itemNo = menuStartItem + itemChoice;
                    ClearShopDisplay();
                    CText("Excellent decision");
                    UpdateDisplay();
                    var key = GetSingleKey();

                    if (key != "")

                    {
                        // Add a weight & inventory limit check prior to taking money

                        DeductCoins(0, 0, shopOffer);
                        var objectNumber = shopClothingWares[itemNo].itemRef; // ref within Weapons array
                        var itemHandle = CreateClothing(objectNumber); // create a new weapon or armour item(s)
                        itemBuffer[itemHandle].location = 10; // Add to player inventory - 10
                        shopMenu = 3; // back to purchases
                    }
                }

                while (shopMenu == 25) // insufficient funds!
                {
                    ShopMessage("Thou would be wise to check thy funds@@BEFORE purchasing!");
                    shopMenu = 3; // back to clothing purchases
                }

                while (shopMenu == 26) // what is your offer
                {
                    var coppers = InputValue("How many coppers do you offer?", 3);

                    // check offer
                    if (coppers == 0)
                        shopMenu = 22;

                    if (coppers >= itemCost)
                    {
                        shopOffer = coppers; // accepted the players offer
                        offerStatus = 2;
                        shopMenu = 27;
                    }
                    if ((coppers >= itemLowestCost) && (coppers < itemCost))
                    {
                        offerStatus = 2;
                        offerRounds++;
                        if (offerRounds > 2)
                        {
                            shopOffer = coppers;
                            shopMenu = 27;
                        } else
                        {
                            shopOffer = Randn(coppers, itemCost);
                            itemLowestCost = coppers;
                            shopMenu = 23;
                        }
                    }
                    if ((coppers < itemLowestCost) && (coppers > 0))
                    {
                        offerStatus = 1;
                        offerRounds++;
                        shopOffer = itemLowestCost;
                        shopMenu = (offerRounds > 1) ? 19 : 23;
                    }
                }

                while (shopMenu == 27) // Offer accepted (subject to funds check) for clothing
                {
                    ClearShopDisplay();
                    CText("I'll take it!");
                    UpdateDisplay();
                    var key = GetSingleKey();

                    if (key != "")
                    {
                        if (!CheckCoins(0, 0, shopOffer))
                            shopMenu = 25;
                        else
                        {
                            plyr.shopFriendships[shopNo]++;
                            if (plyr.shopFriendships[shopNo] > 4)
                                plyr.shopFriendships[shopNo] = 4;
                            shopMenu = 24;
                        }
                    }
                }

                while (shopMenu == 50) // closed
                {
                    var openingText = "";
                    var closingText = "";
                    var openHour = Shops[shopNo].openingHour;
                    var closeHour = Shops[shopNo].closingHour;
                    if ((openHour >= 0) && (openHour < 12))
                        openingText = "morning";
                    if ((openHour >= 12) && (openHour < 18))
                        openingText = "afternoon";
                    if ((openHour >= 18) && (openHour <= 23))
                        openingText = "evening";
                    if ((closeHour >= 0) && (closeHour < 12))
                        closingText = "morning";
                    if ((closeHour >= 12) && (closeHour < 18))
                        closingText = "afternoon";
                    if ((closeHour >= 18) && (closeHour <= 23))
                        closingText = "evening";
                    ClearShopDisplay();
                    CyText(1, "Sorry, we are closed. Come back@during our working hours.");
                    
                    CyText(4, $"We are open from {Shops[shopNo].openingHour}:00 in the {openingText}@to {Shops[shopNo].closingHour}:00 in the {closingText}.");
                    CyText(9, "( Press a key )");
                    UpdateDisplay();
                    var key = GetSingleKey();
                    if (key == "SPACE")
                        shopMenu = 0;
                }
            }
            LeaveShop();
        }

        public static void StockShopWares ()
        {
            // Run each day to randomly pick 12 items for sale at each of the 15 shops
            // Check for duplicates using smithyWaresCheck array of bools

            // Set bools for duplicate items check to false

            for (var x = 0; x < 15; x++)
            {
                for (var y = 0; y < 16; y++)
                    shopWaresCheck[x, y] = false;
            }

            for (var shopNo = 0; shopNo < 15; shopNo++)
            {
                for (var waresNo = 0; waresNo < 12; waresNo++)
                {
                    var uniqueItem = false;
                    while (!uniqueItem)
                    {
                        var itemNo = Randn(0, 15); // to exclude damon clothing items
                        var itemIndex = 12 + itemNo; // to exclude the Damon items
                        if (itemIndex == 12)
                            itemIndex = 13;
                        if (!shopWaresCheck[shopNo, itemNo])
                        {
                            // problem

                            // problem duplicates
                            shopDailyWares[shopNo, waresNo] = itemIndex; // its not a duplicate
                            shopWaresCheck[shopNo, itemNo] = true;
                            uniqueItem = true;
                        }
                    }
                }
            }

            // Simple sort of items in cost numeric order
            // Requires items arranged in the array in ascending price order to work!                        
            sort(shopDailyWares);
        }

        // extern Player plyr;
        // extern sf::RenderWindow App;
        //extern clothingItem clothingItems[12];

        //extern int shopDailyWares[15][12]; //15 shops with 12 items each a day for sale
    }
}