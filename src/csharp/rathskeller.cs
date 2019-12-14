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
using SFML.Audio;

namespace P3Net.Arx
{
    /* RATHSKELLER.CPP
		 *
		 * TODO:
		 * carrying corpse message once corpses implemented, dragon/wyrm corpse gold offer,
		 * enclosed booth???
		 * all music and on screen lyrics
		 *
		 */
    public enum RathSkellerMenus
    {
        MenuLeft,
        MenuMain,
        MenuSeated,
        MenuOrder,
        MenuRound,
        MenuLeaveATip,
        MenuTransact,
        MenuThanks,
        MenuNoFunds,
        MenuNoTippingFunds,
        MenuGetTipValue,
        MenuGetItemChoice,
        MenuRightAway,
        MenuOrderAnythingElse,
        MenuLeavingAlready,
        MenuNpcEnters,
        MenuNpcOpener,
        MenuNpcMeal,
        MenuNpcDrink,
        MenuNpcTransact,
        MenuNpcRumour
    }

    public class FoodDrinkItem
    {
        public int alcoholValue { get; set; }

        public int basePrice { get; set; } // Multiplied by 2 to get cost in silvers

        public int hungerValue { get; set; }

        public string name { get; set; }

        public int thirstValue { get; set; }
    }

    public partial class GlobalMembers
    {
        public static bool bar;
        public static string eatingDescription = "";
        public static string greetingText;

        public static RathSkellerMenus menu;
        public static string npcDescription;

        public static string[] npcDescriptions =
        {
            "A sly looking stranger sits down.",
            "A sloppy guy stumbles in.",
            "A dwarf wearing a raincoat walks over.",
            "A human wearing a fur-lined outfit@@says \"Hello Adventurer!\"",
            "A large, burly human approaches."
        };
        public static int npcDrinkCost;
        public static int npcMealCost;
        public static bool npcNotPresent;
        public static string npcOpener;

        public static string[] npcOpeners =
        {
            "It's too bad they beefed up@@the security in the Dungeon.@@It used to be an easy life stealing@@from others.",
            "It's been..@@Why, I can't remember the@@last time I've had a drink.",
            "I'm tired. I've just finished@@a show in The City. I hate@@dancing, but it's a living.",
            "I've just lost a third of@@what I own. You sure can't@@trust the banks around here.",
            "Greetings Adventurer.@@I'm known as Salin Wauthra."
        };
        public static string npcRumour;

        public static string[] npcRumours =
        {
            "A wise oracle dwells beneath@the Floating Gate.",
            "Bank vault basements can be found@on the first level.",
            "Acrinimiril's Tomb is haunted.",
            "The Chapel dispenses@pragmatic salvation.",
            "A fountain that heals wounds@is on the first level.",
            "The Troll King eats@punks like you for breakfast.",
            "The Goblin King@is an underhanded fink.",
            "There is no honor among Thieves.",
            "All magic has a price.",
            "The Guilds of the undercity@war upon each other.",
            "The river leads to@lands of the Undead.",
            "There is a fountain that cures disease@on the second level.",
            "You can always trust the guards.",
            "Step lightly in the crystal caverns.",
            "Lucky's got the brew for you.",
            "You can trust what you hear@from the Horse's mouth.",
            "Never trust a demon.",
            "The dwarf on the second level is@interested in old weapons and armor.",
            "A special fountain on the third level@can enliven your day.",
            "There is a dreaded dragon@on the third level.",
            "Beware the gauntlet of doom@on the third level.",
            "There is a very special door@hidden on the third level.",
            "Evil magic takes a special toll.",
            "Seek the ways of the Wizards of Law.",
            "We're being watched all the time.",
            "The music heard in the tavern@comes from beyond this world.",
            "The temptations of evil are strong.",
            "Fruit juice is.�very invigorating.",
            "Many great treasures@are carefully guarded.",
            "Be sure your friends@are not your foes.",
            "Always leave a tip@for services rendered.",
            "The pure in heart know@how to show mercy.",
            "The Oracle always tells the truth.",
            "Never fight fire with fire.",
            "The truly brave are not@afraid to make peace.",
            "In the Rooms of Confusion,@seek out a secret door.",
            "Fine clothing attracts more friends.",
            "Gluttony is hazardous to your health.",
            "Some treasures are better left alone.",
            "Cross the river at midnight.",
            "One magic ring@can cause a lot of trouble.",
            "Answering riddles brings great reward.",
            "Give a generous donation at@the Retreat.",
            "Be careful when dealing with@\"Honest\" Omar and his brother Jeff.",
            "The Dwarf loves sparkling gems.",
            "Greedy adventurers are often visited by@by the Devourer!.",
            "Read the Guidebook thoroughly!.",
            "Sermons are good for your soul.",
            "About half the rumors you hear@in the Dungeon are false.",
            "Beware the screamer that@walks on the wind.",
            "Locked doors require skill to pass.",
            "Muggers can make powerful allies.",
            "Occum gets the best weapons for his shop@by capturing them in a clever trap.",
            "Keep an eye on your purse@if you sleep at the Sanctuary.",
            "There are magical baths@hidden on the fourth level.",
            "All goblins have a hidden zipper.",
            "Seek the leg of Jeerbeef.",
            "Be kind to zombies.",
            "The third level of the dungeon@is patrolled by elephants.",
            "The Troglodytes are valuable friends.",
            "Don't drink from the fountains.",
            "The Troll Tyrant is fair@and honorable.",
            "The Goblin Lord is a decent fellow.",
            "Black Magic is more powerful@than White Magic.",
            "The Thieves' Guild is a@Trustworthy organization.",
            "My dog told me to watch out for you.",
            "Seek magical knowledge@at the Green Wizards Academy.",
            "Never eat anything@you didn't kill yourself.",
            "The Red Wizards teach the Black Arts.",
            "Beware the Clown that@laughs by the bedside.",
            "The Guilds conspire together@against the King.",
            "Those about to die dwell beneath@the Arena on the third level.",
            "They need a new jester at the Palace.",
            "The Palace Prison is full of@convicts and madmen.",
            "Beware the boatman on the second level.",
            "There is a fountain on the fourth level@that makes you invulnerable.",
            "Mutilation has its good points.",
            "Beware of the poisoner named Lucky.",
            "The Clothes Horse will let you ride him@if you bring him a nice hat.",
            "Demons are great guys.",
            "Seek the Dwarf by the Wharf.",
            "The fountain on the third level@flows directly from Hell.",
            "The Great Wyrm likes to bluff.",
            "The third level is actually@safer than the first.",
            "If you push something hard enough,@it will fall over.",
            "Two and two does not always equal four@on the fourth level.",
            "Seek the teleporters@to rapidly travel The Dungeon.",
            "Look out for the guy@with the high arches in his feet.",
            "Exploring the third level@requires high stamina.",
            "The one-winged dog flies tonight.",
            "You can get rich quick@on the fourth level.",
            "Armor takes skill to use well.",
            "An unspeakable horror dwells@at the center of time.",
            "Avoid the elf in the raincoat.",
            "Quest for the man@in the purple trousers.",
            "Never allow an enemy to surrender,@They have nothing true to say.",
            "Walk backwards through@the Hall of Mirrors.",
            "There's money to be made@in the blacksmithing trade."
        };
        public static bool rathMusicPlaying;

        public static int[] rathskellerFoodDrink = new int[18];
        public static bool[] rathskellerFoodDrinkCheck = new bool[40];

        public static FoodDrinkItem[] rathskellerItems =
        {
            new FoodDrinkItem()
            { basePrice = 0x05, hungerValue = 0x12, thirstValue = 0x02, alcoholValue = 0x00, name = "Rack of Lamb" },
            new FoodDrinkItem()
            { basePrice = 0x06, hungerValue = 0x18, thirstValue = 0x04, alcoholValue = 0x00, name = "Roast Beef" },
            new FoodDrinkItem()
            { basePrice = 0x04, hungerValue = 0x11, thirstValue = 0x02, alcoholValue = 0x00, name = "Roast Fowl" },
            new FoodDrinkItem()
            { basePrice = 0x1E, hungerValue = 0x20, thirstValue = 0x04, alcoholValue = 0x00, name = "Roast Dragon" },
            new FoodDrinkItem()
            { basePrice = 0x04, hungerValue = 0x0D, thirstValue = 0x01, alcoholValue = 0x00, name = "Spare Ribs" },
            new FoodDrinkItem()
            { basePrice = 0x07, hungerValue = 0x14, thirstValue = 0x02, alcoholValue = 0x00, name = "Roast Mutton" },
            new FoodDrinkItem()
            { basePrice = 0x0A, hungerValue = 0x16, thirstValue = 0x03, alcoholValue = 0x00, name = "Leg of Dragon" },
            new FoodDrinkItem()
            { basePrice = 0x28, hungerValue = 0x14, thirstValue = 0x04, alcoholValue = 0x00, name = "a Burger" },
            new FoodDrinkItem()
            { basePrice = 0x06, hungerValue = 0x0E, thirstValue = 0x02, alcoholValue = 0x00, name = "Roast Pig" },
            new FoodDrinkItem()
            { basePrice = 0x06, hungerValue = 0x10, thirstValue = 0x02, alcoholValue = 0x00, name = "Sausages" },
            new FoodDrinkItem()
            { basePrice = 0x0A, hungerValue = 0x14, thirstValue = 0x02, alcoholValue = 0x00, name = "Fillet of Beef" },
            new FoodDrinkItem()
            { basePrice = 0x07, hungerValue = 0x10, thirstValue = 0x04, alcoholValue = 0x00, name = "Ragout of Beef" },
            new FoodDrinkItem()
            { basePrice = 0x0A, hungerValue = 0x18, thirstValue = 0x04, alcoholValue = 0x00, name = "Ragout of Dragon" },
            new FoodDrinkItem()
            { basePrice = 0x07, hungerValue = 0x10, thirstValue = 0x02, alcoholValue = 0x00, name = "Smoked Fish" },
            new FoodDrinkItem()
            { basePrice = 0x07, hungerValue = 0x0E, thirstValue = 0x02, alcoholValue = 0x00, name = "Crayfish" },
            new FoodDrinkItem()
            { basePrice = 0x09, hungerValue = 0x12, thirstValue = 0x02, alcoholValue = 0x00, name = "Lobster" },
            new FoodDrinkItem()
            { basePrice = 0x03, hungerValue = 0x08, thirstValue = 0x04, alcoholValue = 0x00, name = "a Bowl of Fruit" },
            new FoodDrinkItem()
            {
                basePrice = 0x03,
                hungerValue = 0x05,
                thirstValue = 0x03,
                alcoholValue = 0x00,
                name = "a Plate of Greens"
            },
            new FoodDrinkItem()
            { basePrice = 0x02, hungerValue = 0x04, thirstValue = 0x00, alcoholValue = 0x00, name = "a Loaf of Bread" },
            new FoodDrinkItem()
            {
                basePrice = 0x03,
                hungerValue = 0x08,
                thirstValue = 0x00,
                alcoholValue = 0x00,
                name = "a Block of Cheese"
            },
            new FoodDrinkItem()
            { basePrice = 0x04, hungerValue = 0x0A, thirstValue = 0x04, alcoholValue = 0x00, name = "a Bowl of Chili" },
            new FoodDrinkItem()
            { basePrice = 0x03, hungerValue = 0x07, thirstValue = 0x04, alcoholValue = 0x00, name = "Pasta" },
            new FoodDrinkItem()
            { basePrice = 0x04, hungerValue = 0x09, thirstValue = 0x04, alcoholValue = 0x00, name = "Lasagna" },
            new FoodDrinkItem()
            { basePrice = 0x03, hungerValue = 0x08, thirstValue = 0x00, alcoholValue = 0x00, name = "a Sandwich" },
            new FoodDrinkItem()
            { basePrice = 0x03, hungerValue = 0x07, thirstValue = 0x0A, alcoholValue = 0x00, name = "Lentil Soup" },
            new FoodDrinkItem()
            { basePrice = 0x04, hungerValue = 0x0C, thirstValue = 0x00, alcoholValue = 0x00, name = "Pemmican" },
            new FoodDrinkItem()
            { basePrice = 0x03, hungerValue = 0x0A, thirstValue = 0x08, alcoholValue = 0x00, name = "Gruel" },
            new FoodDrinkItem()
            { basePrice = 0x02, hungerValue = 0x06, thirstValue = 0x00, alcoholValue = 0x00, name = "Sweet Meats" },
            new FoodDrinkItem()
            { basePrice = 0x02, hungerValue = 0x06, thirstValue = 0x02, alcoholValue = 0x00, name = "Blood Pudding" },
            new FoodDrinkItem()
            { basePrice = 0x02, hungerValue = 0x08, thirstValue = 0x00, alcoholValue = 0x00, name = "Haggis" },
            new FoodDrinkItem()
            { basePrice = 0x02, hungerValue = 0x02, thirstValue = 0x0A, alcoholValue = 0x05, name = "Spirits" },
            new FoodDrinkItem()
            { basePrice = 0x03, hungerValue = 0x03, thirstValue = 0x0C, alcoholValue = 0x04, name = "Mead" },
            new FoodDrinkItem()
            { basePrice = 0x02, hungerValue = 0x02, thirstValue = 0x0A, alcoholValue = 0x03, name = "Beer" },
            new FoodDrinkItem()
            { basePrice = 0x02, hungerValue = 0x03, thirstValue = 0x0A, alcoholValue = 0x04, name = "Ale" },
            new FoodDrinkItem()
            { basePrice = 0x03, hungerValue = 0x03, thirstValue = 0x0A, alcoholValue = 0x03, name = "Wine" },
            new FoodDrinkItem()
            { basePrice = 0x02, hungerValue = 0x02, thirstValue = 0x0A, alcoholValue = 0x04, name = "Grog" },
            new FoodDrinkItem()
            { basePrice = 0x02, hungerValue = 0x04, thirstValue = 0x10, alcoholValue = 0x00, name = "Sarsaparilla" },
            new FoodDrinkItem()
            { basePrice = 0x02, hungerValue = 0x04, thirstValue = 0x10, alcoholValue = 0x00, name = "Milk" },
            new FoodDrinkItem()
            { basePrice = 0x02, hungerValue = 0x06, thirstValue = 0x0C, alcoholValue = 0x00, name = "Grape Juice" },
            new FoodDrinkItem()
            { basePrice = 0x01, hungerValue = 0x02, thirstValue = 0x10, alcoholValue = 0x00, name = "Mineral Water" },
            new FoodDrinkItem()
            { basePrice = 0x02, hungerValue = 0x06, thirstValue = 0x0C, alcoholValue = 0x00, name = "Orange Juice" }
        };

        public static Music rathskellerMusic;
        public static int roundCost;
        public static int stillEating;
        public static int value;

        public static void AddRathskellerToMap ()
        {
            SetAutoMapFlag(plyr.map, 58, 3);
            SetAutoMapFlag(plyr.map, 62, 3);
            SetAutoMapFlag(plyr.map, 58, 2);
            SetAutoMapFlag(plyr.map, 59, 2);
            SetAutoMapFlag(plyr.map, 60, 2);
            SetAutoMapFlag(plyr.map, 61, 2);
            SetAutoMapFlag(plyr.map, 62, 2);
            SetAutoMapFlag(plyr.map, 58, 1);
            SetAutoMapFlag(plyr.map, 59, 1);
            SetAutoMapFlag(plyr.map, 60, 1);
            SetAutoMapFlag(plyr.map, 61, 1);
            SetAutoMapFlag(plyr.map, 62, 1);
        }

        public static void BuildFoodDrinkMenuOptions ()
        {
            // Run daily to randomly pick 19 unique items
            // Check for duplicates using Check array of bools
            // Set bools for duplicate items check to false
            for (var y = 0; y < 40; y++)
                rathskellerFoodDrinkCheck[y] = false;

            for (var waresNo = 0; waresNo < 20; waresNo++)
            {
                // Current code may create duplicate items in each tavern
                var uniqueItem = false;
                while (!uniqueItem)
                {
                    var itemNo = Randn(0, 40); // was 12

                    if (!rathskellerFoodDrinkCheck[itemNo])
                    {
                        menuItems[waresNo].menuName = rathskellerItems[itemNo].name; // its not a duplicate
                        var str = $"{rathskellerItems[itemNo].basePrice * 2}           "; // its not a duplicate
                        str = str.Remove(3, 7).Insert(3, "silvers");
                        menuItems[waresNo].menuPrice = str;
                        menuItems[waresNo].objRef = itemNo; // its not a duplicate
                        rathskellerFoodDrinkCheck[itemNo] = true;
                        uniqueItem = true;
                    }
                }
            }
        }

        public static void CheckForNPC ()
        {
            if ((npcNotPresent) && (menu == RathSkellerMenus.MenuSeated))
            {
                var npcCheck = Randn(1, 1000);
                if (npcCheck == 1)
                {
                    npcDescription = npcDescriptions[Randn(1, 4)];
                    npcOpener = npcOpeners[Randn(1, 4)];
                    npcRumour = npcRumours[Randn(1, 98)];
                    npcDrinkCost = Randn(1, 8);
                    npcMealCost = Randn(1, 18);
                    menu = RathSkellerMenus.MenuNpcEnters;
                    npcNotPresent = false;
                }
            }
        }

        public static void ChooseFoodDrinkMenuItem ()
        {
            var foodDrinkChoice = InputItemChoice("What would thou like? (0 to go back)", 20);
            if (foodDrinkChoice < 255)
            {
                var itemCost = rathskellerItems[foodDrinkChoice].basePrice * 2;

                if (CheckCoins(0, itemCost, 0))
                {
                    DeductCoins(0, itemCost, 0);
                    ConsumeFoodDrinkItem(foodDrinkChoice);
                    menu = RathSkellerMenus.MenuOrderAnythingElse;
                } else
                {
                    plyr.rathskellerFriendship--;
                    menu = RathSkellerMenus.MenuNoFunds;
                }
            } else
            {
                menu = RathSkellerMenus.MenuSeated;
            }
        }

        public static void ConsumeFoodDrinkItem ( int foodDrinkItem )
        {
            // Unlike the City taverns Rathskeller items appear to have both food and drink values

            plyr.hunger -= rathskellerItems[foodDrinkItem].hungerValue;
            if (plyr.hunger < 0)
                plyr.hunger = 0;
            plyr.digestion += (rathskellerItems[foodDrinkItem].hungerValue) * 2;

            if (rathskellerItems[foodDrinkItem].hungerValue > 9)
            {
                eatingDescription = rathskellerItems[foodDrinkItem].name;
                stillEating = rathskellerItems[foodDrinkItem].hungerValue;
            }

            plyr.thirst -= rathskellerItems[foodDrinkItem].thirstValue;
            if (plyr.thirst < 0)
                plyr.thirst = 0;
            plyr.alcohol += rathskellerItems[foodDrinkItem].alcoholValue;
        }

        public static void DisplayModuleText ()
        {
            if (menu == RathSkellerMenus.MenuMain)
            {
                CyText(0, greetingText);
                CyText(2, "Where dost thou wish to sit?");
                BText(15, 4, "(1) At the bar");
                BText(15, 5, "(2) At a table");
                BText(15, 7, "(0) Leave");
            } else if (menu == RathSkellerMenus.MenuSeated)
            {
                var str = bar ? "Thou art sitting at the bar." : "Thou art sitting at a table.";
                if (stillEating > 0)
                    str = $"Thou art eating {eatingDescription}.";
                CyText(0, str);
                CyText(2, "What dost thou wish?");
                BText(6, 4, "(1) Order something");
                BText(6, 5, "(2) Buy a round for the house");
                BText(6, 7, "(0) Leave");
            } else if (menu == RathSkellerMenus.MenuLeaveATip)
            {
                BText(11, 2, "(1) Say goodbye");
                BText(11, 4, "(2) Leave a tip");
                BText(11, 7, "(0) Leave quietly");
            } else if (menu == RathSkellerMenus.MenuTransact)
            {
                CyText(0, "Dost thou wish to:");
                BText(10, 2, "(1) Buy him a drink");
                BText(10, 3, "(2) Buy him a meal");
                BText(10, 4, "(3) Transact");
                BText(10, 6, "(0) Ignore him");
            } else if (menu == RathSkellerMenus.MenuRound)
            {
                CyText(1, "A round for the house will cost:");
                CyText(3, $"{roundCost} silvers.");
                CyText(6, "Dost thou still wish to buy? (Y or N)");
            } else if (menu == RathSkellerMenus.MenuNpcDrink)
            {
                CyText(1, $"The drink will cost you {npcDrinkCost} silvers.");
                CyText(3, "OK (Y or N)");
            } else if (menu == RathSkellerMenus.MenuNpcMeal)
            {
                CyText(1, $"The meal will cost you {npcMealCost} silvers.");
                CyText(3, "OK (Y or N)");
            } else if (menu == RathSkellerMenus.MenuNpcEnters)
                CyText(1, npcDescription);
            else if (menu == RathSkellerMenus.MenuNpcOpener)
                CyText(1, npcOpener);
            else if (menu == RathSkellerMenus.MenuNpcRumour)
                CyText(1, npcRumour);
            else if (menu == RathSkellerMenus.MenuThanks)
                CyText(1, "Thank you!  Please come again.");
            else if (menu == RathSkellerMenus.MenuRightAway)
                CyText(1, "Coming right up!");
            else if (menu == RathSkellerMenus.MenuNoFunds)
                CyText(2, "I'm sorry, you have not the funds.");
            else if (menu == RathSkellerMenus.MenuNoTippingFunds)
                CyText(1, "Your generosity is greatly appreciated.@@However, your humor is not.");
            else if (menu == RathSkellerMenus.MenuOrderAnythingElse)
                CyText(1, "Coming up!@@Is there anything else@@I can get for you? (Y or N)");
            else if (menu == RathSkellerMenus.MenuLeavingAlready)
                CyText(1, "Leaving already?  You haven't@@finished your meal.  I'll wrap it@@in a packet for you.");            
        }

        public static void LeaveTip ()
        {
            var tip = InputNumber("How many silvers@dost thou wish to leave?");
            if (CheckCoins(0, tip, 0))
            {
                DeductCoins(0, tip, 0);
                menu = RathSkellerMenus.MenuThanks;
                plyr.rathskellerFriendship++;
                plyr.rathskellerFriendship++;
            } else
            {
                plyr.rathskellerFriendship--;
                menu = RathSkellerMenus.MenuNoTippingFunds;
            }
        }

        public static void PlayRathskellerMusic ()
        {
            if (!rathMusicPlaying)
            {
                var randomSong = Randn(0, 2);
                if (randomSong == 1)
                    rathskellerMusic = new Music("data/audio/rathskeller.ogg");
                else
                    rathskellerMusic = new Music("data/audio/rathskeller2.ogg");
                rathskellerMusic.Play();
                rathMusicPlaying = true;
            }
        }

        public static void ProcessMenuInput ()
        {
            var key = ReadKey();

            switch (menu)
            {
                case RathSkellerMenus.MenuMain:
                if (key == "1")
                    menu = RathSkellerMenus.MenuSeated;
                else if (key == "2")
                {
                    bar = false;
                    menu = RathSkellerMenus.MenuSeated;
                } else if (key == "0")
                    menu = RathSkellerMenus.MenuLeft;
                else if (key == "down")
                    menu = RathSkellerMenus.MenuLeft;
                break;
                case RathSkellerMenus.MenuSeated:
                if (key == "1")
                    menu = RathSkellerMenus.MenuGetItemChoice;
                else if (key == "2")
                    menu = RathSkellerMenus.MenuRound;
                else if (key == "0")
                {
                    if (stillEating > 0)
                        menu = RathSkellerMenus.MenuLeavingAlready;
                    else
                        menu = RathSkellerMenus.MenuLeaveATip;
                }
                break;
                case RathSkellerMenus.MenuLeaveATip:
                if (key == "1")
                {
                    menu = RathSkellerMenus.MenuThanks;
                    plyr.rathskellerFriendship++;
                } else if (key == "2")
                    menu = RathSkellerMenus.MenuGetTipValue;
                else if (key == "0")
                    menu = RathSkellerMenus.MenuLeft;
                break;
                case RathSkellerMenus.MenuTransact:
                if (key == "1")
                    menu = RathSkellerMenus.MenuNpcDrink;
                else if (key == "2")
                    menu = RathSkellerMenus.MenuNpcMeal;
                else if (key == "3")
                    menu = RathSkellerMenus.MenuNpcOpener;
                else if (key == "0")
                {
                    npcNotPresent = true;
                    menu = RathSkellerMenus.MenuSeated;
                }
                break;
                case RathSkellerMenus.MenuRound:
                if (key == "Y")
                {
                    if (CheckCoins(0, roundCost, 0))
                    {
                        DeductCoins(0, roundCost, 0);
                        menu = RathSkellerMenus.MenuSeated;
                        plyr.rathskellerFriendship++;
                    } else
                    {
                        plyr.rathskellerFriendship--;
                        menu = RathSkellerMenus.MenuNoFunds;
                    }
                } else if (key == "N")
                    menu = RathSkellerMenus.MenuSeated;
                break;
                case RathSkellerMenus.MenuNoFunds:
                if (key != "")
                    menu = RathSkellerMenus.MenuSeated;
                break;
                case RathSkellerMenus.MenuThanks:
                if (key != "")
                    menu = RathSkellerMenus.MenuLeft;
                break;
                case RathSkellerMenus.MenuRightAway:
                if (key != "")
                    menu = RathSkellerMenus.MenuSeated;
                break;
                case RathSkellerMenus.MenuNoTippingFunds:
                if (key != "")
                    menu = RathSkellerMenus.MenuGetTipValue;
                break;
                case RathSkellerMenus.MenuGetTipValue:
                LeaveTip();
                break;
                case RathSkellerMenus.MenuGetItemChoice:
                ChooseFoodDrinkMenuItem();
                break;
                case RathSkellerMenus.MenuOrderAnythingElse:
                if (key == "Y")
                    menu = RathSkellerMenus.MenuGetItemChoice;
                else if (key == "N")
                    menu = RathSkellerMenus.MenuSeated;
                break;
                case RathSkellerMenus.MenuNpcEnters:
                if (key != "")
                    menu = RathSkellerMenus.MenuTransact;
                break;
                case RathSkellerMenus.MenuNpcDrink:
                if (key == "Y")
                {
                    if (CheckCoins(0, npcDrinkCost, 0))
                    {
                        DeductCoins(0, npcDrinkCost, 0);
                        menu = RathSkellerMenus.MenuNpcRumour;
                    } else
                    {
                        menu = RathSkellerMenus.MenuNoFunds;
                    }
                } else if (key == "N")
                    menu = RathSkellerMenus.MenuTransact;
                break;
                case RathSkellerMenus.MenuNpcMeal:
                if (key == "Y")
                {
                    if (CheckCoins(0, npcMealCost, 0))
                    {
                        DeductCoins(0, npcMealCost, 0);
                        menu = RathSkellerMenus.MenuNpcRumour;
                    } else
                    {
                        menu = RathSkellerMenus.MenuNoFunds;
                    }
                } else if (key == "N")
                    menu = RathSkellerMenus.MenuTransact;
                break;
                case RathSkellerMenus.MenuNpcRumour:
                if (key != "")
                    menu = RathSkellerMenus.MenuSeated;
                break;
                case RathSkellerMenus.MenuNpcOpener:
                if (key != "")
                    menu = RathSkellerMenus.MenuTransact;
                break;
                case RathSkellerMenus.MenuLeavingAlready:
                if (key != "")
                {
                    plyr.food++;
                    menu = RathSkellerMenus.MenuLeaveATip;
                }
                break;
            }
        }

        public static void RunRathskeller ()
        {
            menu = RathSkellerMenus.MenuMain;
            LoadShopImage(2);
            stillEating = 0;
            bar = true;
            npcNotPresent = true;
            roundCost = Randn(5, 20);
            rathMusicPlaying = false;

            BuildFoodDrinkMenuOptions(); // Each visit currently
            AddRathskellerToMap();
            SetGreetingText();
            PlayRathskellerMusic();

            while (menu != (int)RathSkellerMenus.MenuLeft)
            {
                UpdateFoodConsumption();
                CheckForNPC();
                ClearShopDisplay();
                DisplayModuleText();
                UpdateDisplay();
                ProcessMenuInput();
            }

            rathskellerMusic.Stop();
        }

        public static void SetGreetingText ()
        {
            if (plyr.rathskellerFriendship == -6)
                greetingText = "Slimy thing, must thee darken my door?";
            if (plyr.rathskellerFriendship == -5)
                greetingText = "Thou fewmet, why art thou here?";
            if (plyr.rathskellerFriendship == -4)
                greetingText = "What now, filthy Cheapskate?";
            if (plyr.rathskellerFriendship == -3)
                greetingText = "What dost thou here, insolent one!";
            if (plyr.rathskellerFriendship == -2)
                greetingText = "Hast thou brought enough cash?";
            if (plyr.rathskellerFriendship == -1)
                greetingText = "Thy welcome is wearing thin.";
            if (plyr.rathskellerFriendship == 0)
                greetingText = "Hello, Stranger!";
            if (plyr.rathskellerFriendship == 1)
                greetingText = $"Hello, {plyr.name}!";
            if (plyr.rathskellerFriendship >= 2)
                greetingText = $"Well met, {plyr.name} is welcome here!";
        }

        public static void UpdateFoodConsumption ()
        {
            // Check for time 30 seconds?
        }

        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //void OrderFoodDrink();
    }
}