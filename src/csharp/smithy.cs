using System;
using System.Collections.Generic;
using System.Linq;
namespace P3Net.Arx
{
	public class Smithy
	{
	   public string name { get; set; }
	   public float minimumPriceFactor { get; set; }
	   public float initialPriceFactor { get; set; }
	   public int location { get; set; } // match with location text description number
	   public int openingHour { get; set; }
	   public int closingHour { get; set; }
	}

	public class SmithyItem
	{

		public string name { get; set; }
		public int type { get; set; } // 177 - armour, 178 - weapon
		public int basePrice { get; set; }
		public int itemRef { get; set; }
	}

	public partial class GlobalMembers
	{
		public static readonly int citySmithyFileSize = 1691;
		//extern byte citySmithyBinary[citySmithyFileSize];

		public static void LoadCitySmithyBinary()
		{
			// Loads armour and weapons binary data into the "citySmithyBinary" array
			FileStream fp; // file pointer - used when reading files
			string tempString = new string(new char[100]); // temporary string
			tempString = string.Format("{0}{1}", "data/map/", "smithyItems.bin");
			fp = fopen(tempString, "rb");
			if (fp != null)
			{
				for (int i = 0;i < citySmithyFileSize;i++)
					citySmithyBinary[i] = fgetc(fp);
			}
			fclose(fp);
		}

	/*
	smithyItem smithyWares[23] =
	{
		{"a Stiletto",		178,113,	61},
		{"a Dagger",		178,129,	62},
		{"a Whip",			178,396,	63},
		{"a War Net",		178,908,	57},
		{"Padded Armor",	177,2200,	1},
		{"a Small Shield",	178,2460,	60},
		{"a Shortsword",	178,3146,	64},
		{"a Shield",		178,4290,	59},
		{"a Flail",			178,4620,	65},
		{"Leather Armor",	177,4840,	2},
		{"a Spiked Shield",	178,6160,	58},
		{"a Battle Axe",	178,16930,	66},
		{"Studded Armor",	177,7260,	3},
		{"a Sword",			178,7680,	67},
		{"a Tower Shield",	178,9488,	56},
		{"Ring Mail",		177,10010,	4},
		{"a Battle Hammer",	178,10285,	68},
		{"a Longsword",		178,11193,	69},
		{"Scale Mail",		177,14245,	5},
		{"Splint Mail",		177,18975,	6},
		{"Chain Mail",		177,24640,	7},
		{"Banded Armor",	177,32000,	8},
		{"Plate Armor",		177,41500,	9}
	};
	*/



		public static void ShopSmithy()
		{
			if (plyr.timeOfDay == 1)
				LoadShopImage(8);
			else
				LoadShopImage(9);

			int offerStatus = 0; // 0 is normal, 1 is demanding, 2 is bartering
			int offerRounds = 0;
			int itemLowestCost;
			int smithyOffer;

			if (plyr.timeOfDay == 1)
				LoadShopImage(8);
			else
				LoadShopImage(9);

			smithyNo = GetSmithyNo();
			bool musicPlaying = false;
			int smithyMenu = 1; // high level menu
			string str;
			string key;
			plyr.status = 2; // shopping
			menuStartItem = 0; // menu starts at item 0
			if ((Smithies[smithyNo].closingHour <= plyr.hours) || (Smithies[smithyNo].openingHour> plyr.hours))
				smithyMenu = 5;


			while (smithyMenu > 0)
			{
				while (smithyMenu == 5) // closed
				{
					SmithyDisplayUpdate();
					CyText(1, "Sorry, we are closed. Come back@during our working hours.");
					str = "We are open from " + Itos(Smithies[smithyNo].openingHour) + ":00 in the morning@to " + Itos(Smithies[smithyNo].closingHour) + ":00 in the evening.";
					if (Smithies[smithyNo].closingHour == 15)
						str = "We are open from " + Itos(Smithies[smithyNo].openingHour) + ":00 in the morning@to " + Itos(Smithies[smithyNo].closingHour) + ":00 in the afternoon.";
					CyText(4, str);
					CyText(9, "( Press a key )");
					UpdateDisplay();

					key = GetSingleKey();
					if ((key != "") && (key != "up"))
						smithyMenu = 0;
				}

				while (smithyMenu == 1) // main menu
				{
					SmithyDisplayUpdate();
					BText(13, 0, "Welcome Stranger!");
					BText(7, 3, "Do you wish to see our wares?");
					CyText(5, "( es or  o)");
					SetFontColour(40, 96, 244, 255);
					CyText(5, " Y      N  ");
					SetFontColour(215, 215, 215, 255);
					DisplayCoins();
					UpdateDisplay();

					if (!musicPlaying)
					{
						//int Random = randn(0, 2);
						if (plyr.musicStyle == 0)
							smithyMusic.openFromFile("data/audio/armor.ogg");
						else
							smithyMusic.openFromFile("data/audio/B/armor.ogg");
						LoadLyrics("armor.txt");
						smithyMusic.play();
						musicPlaying = true;
					}

					key = GetSingleKey();
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
					SetFontColour(40, 96, 244, 255);
					CyText(0, "                      0          ");
					SetFontColour(215, 215, 215, 255);

					smithyNo = GetSmithyNo();
					for (int i = 0 ; i < maxMenuItems ; i++)
					{
						int itemNo = smithyDailyWares[smithyNo][menuStartItem + i];
						str = ") " + smithyWares[itemNo].name;
						//if ((smithyWares[itemNo].itemRef) > 10) { str = ") " + Weapons[smithyWares[itemNo].itemRef].name; }
						BText(3, (2 + i), str); //was 4
						BText(1, (2 + i), "                                 coppers");
					}
					DisplayCoins();

					int itemCost;
					int x;
					for (int i = 0 ; i < maxMenuItems ; i++) // Max number of item prices in this menu display
					{
						string itemCostDesc;
						x = 28;
						int itemNo = smithyDailyWares[smithyNo][menuStartItem + i];

						//MLT: Downcast to int
						itemCost = (int)(Smithies[smithyNo].initialPriceFactor * smithyWares[itemNo].basePrice);
						//itemCost = smithyWares[itemNo].basePrice;
						if (itemCost < 1000)
							x = 30;
						if (itemCost > 999)
							x = 28;
						if (itemCost > 9999)
							x = 27;
						itemCostDesc = ToCurrency(itemCost);
						BText(x, (i + 2), itemCostDesc);
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
					if (menuStartItem != 4)
						BText(2, 8, "{");
					SetFontColour(215, 215, 215, 255);

					UpdateDisplay();

					key = GetSingleKey();
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
					smithyNo = GetSmithyNo();
					itemNo = smithyDailyWares[smithyNo][menuStartItem + itemChoice];
					//MLT: Downcast to int
					itemCost = (int)(Smithies[smithyNo].initialPriceFactor * smithyWares[itemNo].basePrice);
					float tempitemcost = Smithies[smithyNo].initialPriceFactor * smithyWares[itemNo].basePrice;
					float temp = (tempitemcost / 100) * 75;
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
						str = "The cost for " + smithyWares[itemNo].name;
						CyText(0, str);
						str = "is " + ToCurrency(smithyOffer) + " coppers. Agreed?";
						CyText(1, str);
					}
					if (offerStatus == 1)
					{
						str = "I demand at least " + ToCurrency(smithyOffer) + " silvers!";
						CyText(1, str);
					}
					if (offerStatus == 2)
					{
						str = "Would you consider " + ToCurrency(smithyOffer) + "?";
						CyText(1, str);
					}

					BText(11, 3, " ) Agree to price");
					BText(11, 4, " ) Make an offer");
					BText(11, 5, " ) No sale");
					BText(11, 6, " ) Leave");
					DisplayCoins();
					SetFontColour(40, 96, 244, 255);
					BText(11, 3, "1");
					BText(11, 4, "2");
					BText(11, 5, "3");
					BText(11, 6, "0");
					SetFontColour(215, 215, 215, 255);

					UpdateDisplay();

					key = GetSingleKey();
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
					int coppers = InputValue("What is your offer? (in coppers)", 9);

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
							smithyOffer = Randn(coppers, itemCost);
							itemLowestCost = coppers;
							smithyMenu = 3;
					}
					}
					if ((coppers < itemLowestCost) && (coppers > 0))
					{
						offerStatus = 1;
						offerRounds++;
						smithyOffer = itemLowestCost;
						if (offerRounds > 1)
							smithyMenu = 19;
						else
							smithyMenu = 3;
					}
				}




				while (smithyMenu == 20) // Offer accepted (subject to funds check)
				{
					SmithyDisplayUpdate();
					CText("Agreed!");
					UpdateDisplay();
					key = GetSingleKey();

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
					key = GetSingleKey();

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
					key = GetSingleKey();

					if (key != "")
						smithyMenu = 2;
				}

				while (smithyMenu == 4) // Agree to buy item and have funds
				{
					SmithyDisplayUpdate();
					CText("An excellent choice!");
					CyText(9, "( Press a key )");
					UpdateDisplay();
					key = GetSingleKey();

					if (key != "")
					{
						// Add a weight & inventory limit check prior to taking money
						DeductCoins(0, 0, smithyOffer);
						int objectNumber = smithyWares[itemNo].itemRef; // ref within Weapons array
						//int weaponNumber = smithyWares[itemNo].itemRef; // ref within Weapons array

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
			smithyMusic.stop();
			LeaveShop();
		}
		public static int GetSmithyNo()
		{
			int smithy_no;
			for (int i = 0 ; i < 4 ; i++) // Max number of smithy objects
			{
				if (Smithies[i].location == plyr.location)
						smithy_no = i; // The number of the smithy you have entered
			}
			return smithy_no;
		}
		public static void StockSmithyWares()
		{
			// Run each day to randomly pick 10 items for sale at each of the 4 smithies
			// Check for duplicates using smithyWaresCheck array of bools

			// Set bools for duplicate items check to false
			int itemNo = 0;

			for (int x = 0; x < 4; x++)
			{
				for (int y = 0; y < 23; y++)
					smithyWaresCheck[x, y] = false;
			}

			for (int smithyNo = 0 ; smithyNo < 4 ; smithyNo++)
			{
				for (int waresNo = 0 ; waresNo < 10 ; waresNo++)
				{
					// Current code may create duplicate items in each smithy
					bool uniqueItem = false;
					while (!uniqueItem)
					{
						itemNo = Randn(0, 22);

						if (!smithyWaresCheck[smithyNo, itemNo])
						{
							smithyDailyWares[smithyNo][waresNo] = itemNo; // its not a duplicate
							smithyWaresCheck[smithyNo, itemNo] = true;
							uniqueItem = true;
						}
					}
				}
			}

			// Simple sort of items in numeric order
			sort(smithyDailyWares[0], smithyDailyWares[0] + 10);
			sort(smithyDailyWares[1], smithyDailyWares[1] + 10);
			sort(smithyDailyWares[2], smithyDailyWares[2] + 10);
			sort(smithyDailyWares[3], smithyDailyWares[3] + 10);

			// Always make sure a stiletto will be available
			smithyDailyWares[0][0] = 0;
			smithyDailyWares[1][0] = 0;
			smithyDailyWares[2][0] = 0;
			smithyDailyWares[3][0] = 0;
		}
		public static void SmithyDisplayUpdate()
		{
			clock1.restart();
			ClearShopDisplay();
			UpdateLyrics();
			iCounter++;
		}

	// Take a binary offset within citySmithyBinary and create a new inventory item from the binary data (weapon or armour)
	// Item types:  0x83 - weapon, 0x84 - armour
	//TODO: MLT No return value

		//TODO: MLT No return value
		public static void CreateCitySmithyInventoryItem(int startByte)
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

			int offset = startByte;
			int itemType = citySmithyBinary[offset];
			string itemName = ReadSmithyItemString((offset + 6));

			if (itemType == 0x83)
			{
				itemType = 178; // ARX value for weapon
				index = 0; // No longer required
				useStrength = 0;
				alignment = citySmithyBinary[offset + 3];
				weight = citySmithyBinary[offset + 4];

				wAttributes = (offset + citySmithyBinary[offset + 1]) - 19; // Working out from the end of the weapon object

				melee = 0xFF; //citySmithyBinary[wAttributes+1];
				ammo = 0; //citySmithyBinary[wAttributes+2];
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
				citySmithyBinary[wAttributes + 13];
				minStrength = citySmithyBinary[wAttributes + 13];
				minDexterity = citySmithyBinary[wAttributes + 14];
				hp = 44; //citySmithyBinary[wAttributes+15];
				maxHP = 44; //citySmithyBinary[wAttributes+16];
				flags = citySmithyBinary[wAttributes + 17];
				//flags = 1;
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
				hp = 56; //citySmithyBinary[wAttributes+12];
				maxHP = 56; //255;
				flags = 0;
				parry = 0;
			}
		//cout << itemName << " " << std::hex << "HP:" << hp << " Bl:" << blunt << " " << "Sh:" << sharp << "\n";
			int newItemRef = CreateItem(itemType, index, itemName, hp, maxHP, flags, minStrength, minDexterity, useStrength, blunt, sharp, earth, air, fire, water, power, magic, good, evil, cold, weight, alignment, melee, ammo, parry);
			itemBuffer[newItemRef].location = 10; // Add to player inventory - 10
		}
		public static string ReadSmithyItemString(int stringOffset)
		{
			stringstream ss = new stringstream();
			int z = stringOffset; // current location in the binary
			int c = 0; // current byte
			string result = "";

		   while (!(citySmithyBinary[z] == 0))
		   {
				c = citySmithyBinary[z];
				ss << (char) c;
				z++;
		   }
			result = ss.str();
			return result;
		}



		public static byte[] citySmithyBinary = new byte[citySmithyFileSize];

		//extern sf::Clock clock1;
		//extern int iCounter;

		public static sf.Music smithyMusic = new sf.Music();

		public static int itemChoice;
		public static int itemNo;
		public static int itemCost;
		public static int menuStartItem;
		public static int maxMenuItems = 6;
		public static int smithyNo;

		//MLT: Double to float
		public static Smithy[] Smithies =
		{
			new Smithy() { name = "Sharp Weaponsmiths", minimumPriceFactor = 1.25F, initialPriceFactor = 1.65F, location = 55, openingHour = 4, closingHour = 20 },
			new Smithy() { name = "Occum's Weaponsmiths", minimumPriceFactor = 1.10F, initialPriceFactor = 1.35F, location = 56, openingHour = 5, closingHour = 21 },
			new Smithy() { name = "Best Armorers", minimumPriceFactor = 1.50F, initialPriceFactor = 2.40F, location = 57, openingHour = 8, closingHour = 19 },
			new Smithy() { name = "Knight's Armorers", minimumPriceFactor = 1.60F, initialPriceFactor = 2.35F, location = 58, openingHour = 11, closingHour = 15 }
		};

		public static bool[,] smithyWaresCheck = new bool[4, 23]; // markers used to check for duplicate items

		//extern int smithyDailyWares[4][10];

		public static SmithyItem[] smithyWares =
		{
			new SmithyItem() { name = "a Stiletto", type = 178, basePrice = 113, itemRef = 0xAA },
			new SmithyItem() { name = "a Dagger", type = 178, basePrice = 129, itemRef = 0xCB },
			new SmithyItem() { name = "a Whip", type = 178, basePrice = 396, itemRef = 0xE9 },
			new SmithyItem() { name = "a War Net", type = 178, basePrice = 908, itemRef = 0x24 },
			new SmithyItem() { name = "Padded Armor", type = 177, basePrice = 2200, itemRef = 1 },
			new SmithyItem() { name = "a Small Shield", type = 178, basePrice = 2460, itemRef = 0x86 },
			new SmithyItem() { name = "a Shortsword", type = 178, basePrice = 3146, itemRef = 0x105 },
			new SmithyItem() { name = "a Shield", type = 178, basePrice = 4290, itemRef = 0x68 },
			new SmithyItem() { name = "a Flail", type = 178, basePrice = 4620, itemRef = 0x128 },
			new SmithyItem() { name = "Leather Armor", type = 177, basePrice = 4840, itemRef = 2 },
			new SmithyItem() { name = "a Spiked Shield", type = 178, basePrice = 6160, itemRef = 0x43 },
			new SmithyItem() { name = "a Battle Axe", type = 178, basePrice = 16930, itemRef = 0x145 },
			new SmithyItem() { name = "Studded Armor", type = 177, basePrice = 7260, itemRef = 3 },
			new SmithyItem() { name = "a Sword", type = 178, basePrice = 7680, itemRef = 0x167 },
			new SmithyItem() { name = "a Tower Shield", type = 178, basePrice = 9488, itemRef = 0x0 },
			new SmithyItem() { name = "Ring Mail", type = 177, basePrice = 10010, itemRef = 4 },
			new SmithyItem() { name = "a Battle Hammer", type = 178, basePrice = 10285, itemRef = 0x184 },
			new SmithyItem() { name = "a Longsword", type = 178, basePrice = 11193, itemRef = 0x1A9 },
			new SmithyItem() { name = "Scale Mail", type = 177, basePrice = 14245, itemRef = 5 },
			new SmithyItem() { name = "Splint Mail", type = 177, basePrice = 18975, itemRef = 6 },
			new SmithyItem() { name = "Chain Mail", type = 177, basePrice = 24640, itemRef = 7 },
			new SmithyItem() { name = "Banded Armor", type = 177, basePrice = 32000, itemRef = 8 },
			new SmithyItem() { name = "Plate Armor", type = 177, basePrice = 41500, itemRef = 9 }
		};

	}
}