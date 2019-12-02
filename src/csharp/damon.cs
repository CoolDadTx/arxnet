using System;
using System.Collections.Generic;
using System.Linq;
namespace P3Net.Arx
{
	public class DamonBattleGearItem
	{

		//string name;
		public int type { get; set; } // 177 - armour, 178 - weapon
		public int price { get; set; }
		public int itemRef { get; set; }
	}

	public class DamonClothingItem
	{

		//string name;
		public int type { get; set; } // 180 - clothing?
		public int price { get; set; }
		public int itemRef { get; set; }
	}

	public partial class GlobalMembers
	{
		public static readonly int damonFileSize = 916;
		//extern byte damonBinary[damonFileSize];

		public static void Message(string txt)
		{
			string key = "";
			while (key != "SPACE")
			{
					ClearShopDisplay();
					CText(txt);
					UpdateDisplay();
					key = GetSingleKey();
			}
		}
		public static void ShopDamon()
		{
			int itemChoice;
			int itemCost;
			int itemLowestCost;
			int damonOffer;
			int itemNo;
			int damonMenu = 1; // high level menu
			string str;
			string key;
			plyr.status = 2; // shopping
			int menuStartItem = 0;
			int offerStatus = 0; // 0 is normal, 1 is demanding, 2 is bartering
			int offerRounds = 0;

			StockDamon(); // Calculate provisions stock level - torches, food packets etc

			SetAutoMapFlag(plyr.map, 45, 3);
			SetAutoMapFlag(plyr.map, 46, 3);
			SetAutoMapFlag(plyr.map, 45, 4);
			SetAutoMapFlag(plyr.map, 46, 4);
			SetAutoMapFlag(plyr.map, 45, 5);
			SetAutoMapFlag(plyr.map, 46, 5);

			LoadShopImage(3);

			while (damonMenu > 0)
			{
				while (damonMenu == 1) // main menu
				{
					ClearShopDisplay();

					if (plyr.damonFriendship > 1)
					{
					BText(1, 1, "Welcome to our shoppe. I am Omar, Jeff's");
					BText(4, 2, "twin brother.  How can I help you?");
					}
					if (plyr.damonFriendship < 2)
						CyText(1, "What are you here for, churl?");
					BText(6, 4, "(1) Stocking up on provisions");
					BText(6, 5, "(2) Selecting battle gear");
					BText(6, 6, "(3) Choosing some apparel");
					BText(6, 7, "(4) Exchanging currency");
					BText(6, 8, "(0) Leave");
					UpdateDisplay();

					key = GetSingleKey();
					if (key == "0")
						damonMenu = 0;
					if (key == "down")
						damonMenu = 0;
					if (key == "1")
						damonMenu = 30;
					if (key == "2")
						damonMenu = 3;
					if (key == "3")
						damonMenu = 21;
					if (key == "4")
						damonMenu = 7;
				}


		while (damonMenu == 30) // main menu
		{
			ClearShopDisplay();
			BText(1, 1, "What provisions are you interested in?");
			BText(6, 3, "(1) Nourishing food packets");
			BText(6, 4, "(2) Delicious water flasks");
			BText(6, 5, "(3) Bright torches");
			BText(6, 6, "(4) Reliable compasses");
			BText(6, 7, "(5) Accurate timepieces");
			BText(6, 8, "(0) Buy something else");
			UpdateDisplay();

			key = GetSingleKey();
			if (key == "0")
				damonMenu = 1;
			if (key == "1")
			{
				provisionDescription = "food packets";
				damonMenu = 31;
			}
			if (key == "2")
			{
				provisionDescription = "water flasks";
				damonMenu = 31;
			}
			if (key == "3")
			{
				provisionDescription = "torches";
				damonMenu = 31;
			}
			if (key == "4")
			{
				provisionDescription = "compasses";
				damonMenu = 31;
			}
			if (key == "5")
			{
				provisionDescription = "timepieces";
				damonMenu = 31;
			}
		}

		while (damonMenu == 31) //Buy a specific provision item(s)
		{
			if ((plyr.days == 30) && (provisionDescription == "compasses"))
			{
				Message("I'm sorry, but I seem to be out of@@stock in that particular item.  I@@expect my next shipment tomorrow.");
				damonMenu = 30;
				break;
			}
			if ((plyr.days == 30) && (provisionDescription == "timepieces"))
			{
				Message("I'm sorry, but I seem to be out of@@stock in that particular item.  I@@expect my next shipment tomorrow.");
				damonMenu = 30;
				break;
			}
			if ((damonStock[0] == 0) && (provisionDescription == "food packets"))
			{
				Message("I'm sorry, but I seem to be out of@@stock in that particular item.  I@@expect my next shipment tomorrow.");
				damonMenu = 30;
				break;
			}
			if ((damonStock[1] == 0) && (provisionDescription == "water flasks"))
			{
				Message("I'm sorry, but I seem to be out of@@stock in that particular item.  I@@expect my next shipment tomorrow.");
				damonMenu = 30;
				break;
			}
			if ((damonStock[2] == 0) && (provisionDescription == "torches"))
			{
				Message("I'm sorry, but I seem to be out of@@stock in that particular item.  I@@expect my next shipment tomorrow.");
				damonMenu = 30;
				break;
			}
			if ((damonStock[3] == 0) && (provisionDescription == "timepieces"))
			{
				Message("I'm sorry, but I seem to be out of@@stock in that particular item.  I@@expect my next shipment tomorrow.");
				damonMenu = 30;
				break;
			}
			if ((damonStock[4] == 0) && (provisionDescription == "compasses"))
			{
				Message("I'm sorry, but I seem to be out of@@stock in that particular item.  I@@expect my next shipment tomorrow.");
				damonMenu = 30;
				break;
			}

			// check for stock in other items
			int provisionCost = 5;
			int provisionStock = 0;
			if (provisionDescription == "food packets")
			{
				provisionCost = 6;
				provisionStock = damonStock[0];
			}
			if (provisionDescription == "water flasks")
			{
				provisionCost = 4;
				provisionStock = damonStock[1];
			}
			if (provisionDescription == "torches")
			{
				provisionCost = 4;
				provisionStock = damonStock[2];
			}
			if (provisionDescription == "timepieces")
			{
				provisionCost = 15;
				provisionStock = damonStock[3];
			}
			if (provisionDescription == "compasses")
			{
				provisionCost = 15;
				provisionStock = damonStock[4];
			}

			// add look up prov cost here
			provMessage = "How many " + provisionDescription + " do you want@@at " + Itos(provisionCost) + " silvers each?";
			string atHandMessage = "I only have " + Itos(provisionStock) + " to hand.";
			quantity = InputNumber(provMessage);
			if (quantity > provisionStock)
			{
				Message(atHandMessage);
				break;
			}

			if (quantity == 0)
			{
				damonMenu = 30;
				break;
			}
			total = quantity * provisionCost;
			if (quantity == 1)
				provMessage = "That will be " + Itos(provisionCost) + " silvers for one of@my fine " + provisionDescription + ".";
			if (quantity > 1)
				provMessage = "Let's see..." + Itos(quantity) + " " + provisionDescription + " at@" + Itos(provisionCost) + " silvers each comes to a total@of " + Itos(total) + " silvers.";
			bool keyNotPressed = true;
			while (keyNotPressed)
			{
				ClearShopDisplay();
				CyText(1, provMessage);
				BText(12, 6, "(1) Agree to sale");
				BText(12, 7, "(2) Forget it");
				UpdateDisplay();
				key = GetSingleKey();
				if (key == "1")
				{
					keyNotPressed = false;
					damonMenu = 32;
				}
				if (key == "2")
				{
					keyNotPressed = false;
					damonMenu = 30;
				}
			}
		}


		while (damonMenu == 32)
		{
			bool fundsAvailable = CheckCoins(0, total, 0);
			if (!fundsAvailable)
				Message("Thou would be wise to check thy funds@@BEFORE purchasing!");
			else
			{
				Message("A thousand blessings.");
				DeductCoins(0, total, 0);
				if (provisionDescription == "food packets")
				{
					plyr.food += quantity;
					damonStock[0] -= quantity;
				}
				if (provisionDescription == "water flasks")
				{
					plyr.water += quantity;
					damonStock[1] -= quantity;
				}
				if (provisionDescription == "torches")
				{
					plyr.torches += quantity;
					damonStock[2] -= quantity;
				}
				if (provisionDescription == "timepieces")
				{
					plyr.timepieces += quantity;
					damonStock[3] -= quantity;
				}
				if (provisionDescription == "compasses")
				{
					plyr.compasses += quantity;
					damonStock[4] -= quantity;
				}
				// Add a weight check
			}
			damonMenu = 30;
		}





		while (damonMenu == 21)
		{
			offerStatus = 0;
			offerRounds = 0;
			int maxMenuItems = 6;


			ClearShopDisplay();

			CyText(0, "What would you like? (  to go back)");
			SetFontColour(40, 96, 244, 255);
			CyText(0, "                      0            ");
			SetFontColour(215, 215, 215, 255);

			for (int i = 0 ; i < maxMenuItems ; i++)
			{

				int itemNo = menuStartItem + i;
				itemNameOffset = (damonClothingWares[itemNo].itemRef) + 6;
				str = "( ) " + ReadNameString(itemNameOffset);
				BText(1, (2 + i), str); //was 4
				BText(1, (2 + i), "                                 silvers");
			}
			DisplaySilverCoins();

			int itemCost;
			int x;
			for (int i = 0 ; i < maxMenuItems ; i++) // Max number of item prices in this menu display
			{
				string itemCostDesc;

				int itemNo = menuStartItem + i;
				itemCost = damonClothingWares[itemNo].price;

				if (itemCost < 1000)
					x = 30;
				if (itemCost < 100)
					x = 31;

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
			if (menuStartItem != 6)
				BText(2, 8, "{");
			SetFontColour(215, 215, 215, 255);

			UpdateDisplay();

			key = GetSingleKey();
			if (key == "1")
			{
				itemChoice = 0;
				damonMenu = 22;
			}
			if (key == "2")
			{
				itemChoice = 1;
				damonMenu = 22;
			}
			if (key == "3")
			{
				itemChoice = 2;
				damonMenu = 22;
			}
			if (key == "4")
			{
				itemChoice = 3;
				damonMenu = 22;
			}
			if (key == "5")
			{
				itemChoice = 4;
				damonMenu = 22;
			}
			if (key == "6")
			{
				itemChoice = 5;
				damonMenu = 22;
			}
			if ((key == "up") && (menuStartItem > 0))
				menuStartItem--;
			if ((key == "down") && (menuStartItem < 6))
				menuStartItem++;
			if (key == "ESC")
				damonMenu = 0;
			if (key == "0")
				damonMenu = 1;

		}

		while (damonMenu == 22) // buy item?
		{
			itemNo = menuStartItem + itemChoice;
			itemCost = damonClothingWares[itemNo].price;
			float tempitemcost = damonClothingWares[itemNo].price;
			float temp = (tempitemcost / 100) * 75;

			//MLT: Downcast
			itemLowestCost = (int)temp;
			damonOffer = itemCost;
			damonMenu = 23;
		}


				while (damonMenu == 23) // buy item?
				{
					ClearShopDisplay();
					if (offerStatus == 0)
					{
						itemNameOffset = (damonClothingWares[itemNo].itemRef) + 6;
						str = "The cost for " + ReadNameString(itemNameOffset);
						CyText(0, str);
						str = "is " + ToCurrency(damonOffer) + " silvers. Agreed?";
						CyText(1, str);
					}
					if (offerStatus == 1)
					{
						str = "I demand at least " + ToCurrency(damonOffer) + " silvers!";
						CyText(1, str);
					}
					if (offerStatus == 2)
					{
						str = "Would you consider " + ToCurrency(damonOffer) + "?";
						CyText(1, str);
					}

					BText(11, 3, " ) Agree to price");
					BText(11, 4, " ) Make an offer");
					BText(11, 5, " ) Select other apparel");
					BText(11, 6, " ) Buy something else");
					DisplaySilverCoins();
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
						if (!CheckCoins(0, damonOffer, 0))
							damonMenu = 25;
						else
							damonMenu = 24;
					}
					if (key == "2")
						damonMenu = 26;
					if (key == "3")
						damonMenu = 21;
					if (key == "0")
						damonMenu = 1;

				}


				while (damonMenu == 24) // Agree to buy item and have funds
				{
					int itemNo = menuStartItem + itemChoice;
					ClearShopDisplay();
					CText("Excellent decision");
					UpdateDisplay();
					key = GetSingleKey();


					if (key != "")

					{
						// Add a weight & inventory limit check prior to taking money

						//plyr.silver-=itemCost;
						DeductCoins(0, damonOffer, 0);
						int objectNumber = damonClothingWares[itemNo].itemRef; // ref within Weapons array
						CreateInventoryItem(objectNumber);
						damonMenu = 21; // back to purchases
					}

				}




				while (damonMenu == 25) // insufficient funds!
				{
					Message("Thou would be wise to check thy funds@@BEFORE purchasing!");
					damonMenu = 21; // back to clothing purchases
				}




				while (damonMenu == 26) // what is your offer
				{
					int silvers = InputValue("How many silvers do you offer?", 3);

					// check offer
					if (silvers == 0)
						damonMenu = 22;

					if (silvers >= itemCost)
					{
						damonOffer = silvers; // accepted the players offer
						offerStatus = 2;
						damonMenu = 27;
					}
					if ((silvers >= itemLowestCost) && (silvers < itemCost))
					{

						offerStatus = 2;
						offerRounds++;
						if (offerRounds > 2)
						{
							damonOffer = silvers;
							damonMenu = 27;
						} else
						{
							damonOffer = Randn(silvers, itemCost);
							itemLowestCost = silvers;
							damonMenu = 23;
					}
					}
					if ((silvers < itemLowestCost) && (silvers > 0))
					{
						offerStatus = 1;
						offerRounds++;
						damonOffer = itemLowestCost;
						if (offerRounds > 1)
							damonMenu = 19;
						else
							damonMenu = 23;
					}
				}

				while (damonMenu == 27) // Offer accepted (subject to funds check) for clothing
				{
					ClearShopDisplay();
					CText("I'll take it!");
					UpdateDisplay();
					key = GetSingleKey();

					if (key != "")
					{
						if (!CheckCoins(0, damonOffer, 0))
							damonMenu = 25;
						else
						{
							plyr.damonFriendship++;
							if (plyr.damonFriendship > 4)
								plyr.damonFriendship = 4;
							damonMenu = 24;
						}
					}
				}





				while (damonMenu == 3)
				{
					offerStatus = 0;
					offerRounds = 0;
					int maxMenuItems = 6;

					ClearShopDisplay();

					CyText(0, "What would you like? (  to go back)");
					SetFontColour(40, 96, 244, 255);
					CyText(0, "                      0            ");
					SetFontColour(215, 215, 215, 255);

					for (int i = 0 ; i < maxMenuItems ; i++)
					{
						int itemNo = menuStartItem + i;

						itemNameOffset = (damonBattleGearWares[itemNo].itemRef) + 6;
						str = "( ) " + ReadNameString(itemNameOffset);
						BText(1, (2 + i), str); //was 4
						BText(1, (2 + i), "                                 silvers");
					}
					DisplaySilverCoins();

					int itemCost;
					int x;
					for (int i = 0 ; i < maxMenuItems ; i++) // Max number of item prices in this menu display
					{
						string itemCostDesc;

						int itemNo = menuStartItem + i;
						itemCost = damonBattleGearWares[itemNo].price;

						if (itemCost < 1000)
							x = 30;
						if (itemCost < 100)
							x = 31;

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
					if (menuStartItem != 6)
						BText(2, 8, "{");
					SetFontColour(215, 215, 215, 255);

					UpdateDisplay();

					key = GetSingleKey();
					if (key == "1")
					{
						itemChoice = 0;
						damonMenu = 4;
					}
					if (key == "2")
					{
						itemChoice = 1;
						damonMenu = 4;
					}
					if (key == "3")
					{
						itemChoice = 2;
						damonMenu = 4;
					}
					if (key == "4")
					{
						itemChoice = 3;
						damonMenu = 4;
					}
					if (key == "5")
					{
						itemChoice = 4;
						damonMenu = 4;
					}
					if (key == "6")
					{
						itemChoice = 5;
						damonMenu = 4;
					}
					if ((key == "up") && (menuStartItem > 0))
						menuStartItem--;
					if ((key == "down") && (menuStartItem < 6))
						menuStartItem++;
					if (key == "ESC")
						damonMenu = 0;
					if (key == "0")
						damonMenu = 1;

				}




				while (damonMenu == 4) // buy item?
				{
					itemNo = menuStartItem + itemChoice;
					itemCost = damonBattleGearWares[itemNo].price;
					float tempitemcost = damonBattleGearWares[itemNo].price;
					float temp = (tempitemcost / 100) * 75;

					//MLT: Downcast to int
					itemLowestCost = (int)temp;
					damonOffer = itemCost;
					damonMenu = 18;
				}


				while (damonMenu == 18) // buy item?
				{
					ClearShopDisplay();
					if (offerStatus == 0)
					{
						itemNameOffset = (damonBattleGearWares[itemNo].itemRef) + 6;
						str = "The cost for " + ReadNameString(itemNameOffset);

						CyText(0, str);
						str = "is " + ToCurrency(damonOffer) + " silvers. Agreed?";
						CyText(1, str);
					}
					if (offerStatus == 1)
					{
						str = "I demand at least " + ToCurrency(damonOffer) + " silvers!";
						CyText(1, str);
					}
					if (offerStatus == 2)
					{
						str = "Would you consider " + ToCurrency(damonOffer) + "?";
						CyText(1, str);
					}

					BText(11, 3, " ) Agree to price");
					BText(11, 4, " ) Make an offer");
					BText(11, 5, " ) Select other battle gear");
					BText(11, 6, " ) Buy something else");
					DisplaySilverCoins();
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
						if (!CheckCoins(0, damonOffer, 0))
							damonMenu = 6;
						else
							damonMenu = 5;
					}
					if (key == "2")
						damonMenu = 16;
					if (key == "3")
						damonMenu = 3;
					if (key == "0")
						damonMenu = 1;

				}




				while (damonMenu == 16) // what is your offer
				{
					int silvers = InputValue("How many silvers do you offer?", 3);

					// check offer
					if (silvers == 0)
						damonMenu = 4;

					if (silvers >= itemCost)
					{
						damonOffer = silvers; // accepted the players offer
						offerStatus = 2;
						damonMenu = 20;
					}
					if ((silvers >= itemLowestCost) && (silvers < itemCost))
					{

						offerStatus = 2;
						offerRounds++;
						if (offerRounds > 2)
						{
							damonOffer = silvers;
							damonMenu = 20;
						} else
						{
							damonOffer = Randn(silvers, itemCost);
							itemLowestCost = silvers;
							damonMenu = 18;
					}
					}
					if ((silvers < itemLowestCost) && (silvers > 0))
					{
						offerStatus = 1;
						offerRounds++;
						damonOffer = itemLowestCost;
						if (offerRounds > 1)
							damonMenu = 19;
						else
							damonMenu = 18;
					}
				}


				while (damonMenu == 19) // Leave my shop
				{
					ClearShopDisplay();
					CText("Leave my shoppe and don't return@@until you are ready to make a decent@@offer!");
					UpdateDisplay();
					key = GetSingleKey();

					if (key != "")
					{
						plyr.damonFriendship--;
						if (plyr.damonFriendship < 0)
							plyr.damonFriendship = 0;
						damonMenu = 0;
					} // Thrown out
				}

				while (damonMenu == 20) // Offer accepted (subject to funds check)
				{
					ClearShopDisplay();
					CText("I'll take it!");
					UpdateDisplay();
					key = GetSingleKey();

					if (key != "")
					{
						if (!CheckCoins(0, damonOffer, 0))
							damonMenu = 6;
						else
						{
							plyr.damonFriendship++;
							if (plyr.damonFriendship > 4)
								plyr.damonFriendship = 4;
							damonMenu = 5;
						}
					}
				}






				while (damonMenu == 5) // Agree to buy item and have funds
				{
					int itemNo = menuStartItem + itemChoice;
					ClearShopDisplay();
					CText("It will serve you well!");
					CyText(9, "( Press a key )");
					UpdateDisplay();
					key = GetSingleKey();

					if (key != "")

					{
						// Add a weight & inventory limit check prior to taking money

						DeductCoins(0, damonOffer, 0);
						int objectNumber = damonBattleGearWares[itemNo].itemRef; // ref within Weapons array

						if (damonBattleGearWares[itemNo].type == 178)
						{
							// Weapon item
							int weaponOffset = damonBattleGearWares[itemNo].itemRef;
							CreateInventoryItem(weaponOffset);
						}

						if (damonBattleGearWares[itemNo].type == 177)
						{
							// Armour item
							int armourOffset = damonBattleGearWares[itemNo].itemRef;
							CreateInventoryItem(armourOffset);
						}

						damonMenu = 3; // back to purchases
					}
				}




				while (damonMenu == 6) // insufficient funds!
				{
					ClearShopDisplay();
					CText("Thine eyes are bigger than thy purse!");
					CyText(9, "( Press a key )");
					UpdateDisplay();
					key = GetSingleKey();

					if (key != "")
						damonMenu = 3;
				}



				while (damonMenu == 7) // exchange currency menu
				{

					ClearShopDisplay();
					CyText(1, "What would you like to exchange?");

					BText(6, 4, "(1) Gems for coins");
					BText(6, 5, "(2) Jewels for coins");
					BText(6, 6, "(3) Silver & copper coins");
					BText(6, 8, "(0) Done");
					UpdateDisplay();

					key = GetSingleKey();
					if (key == "1")
						damonMenu = 8;
					if (key == "2")
						damonMenu = 11;
					if (key == "3")
						damonMenu = 14;
					if (key == "0")
						damonMenu = 1;
				}

				while (damonMenu == 8) // gems exchange currency menu
				{
					string str = "I will give you 22 silvers@for each gem, big or small.@@How many would you like to exchange?";
					gemsToSell = InputNumber(str);
					//clearShopDisplay();

					//updateDisplay();

					//key = pressKey();
					if (gemsToSell == 0)
						damonMenu = 7;
					if (gemsToSell > plyr.gems)
						damonMenu = 9;
					if ((gemsToSell > 0) && (gemsToSell <= plyr.gems))
						damonMenu = 10;
				}

				while (damonMenu == 9) // Insufficient gems!
				{


					str = "You have only " + Itos(plyr.gems) + "!";
					ClearShopDisplay();
					CyText(2, str);
					UpdateDisplay();
					key = GetSingleKey();
					if (key == "SPACE")
						damonMenu = 7;

				}

				while (damonMenu == 10) // Gems sold!
				{
					int totalSilvers = 22 * gemsToSell;
					int goldFromGems = 0;
					int silverFromGems = 0;
					silverFromGems = totalSilvers % 10;
					goldFromGems = (totalSilvers - silverFromGems) / 10;

					str = "Here is " + Itos(goldFromGems) + " gold and " + Itos(silverFromGems) + " silver coins.";
					ClearShopDisplay();
					CyText(2, str);
					UpdateDisplay();
					key = GetSingleKey();
					if (key == "SPACE")
					{
						plyr.gems -= gemsToSell;
						plyr.gold += goldFromGems;
						plyr.silver += silverFromGems;
						damonMenu = 7;
					}
				}

				while (damonMenu == 11) // jewels exchange currency menu
				{
					string str = "I can't tell one jewel from another@so I pay a flat rate of 32 per jewel.@@How many do you wish to sell?";
					jewelsToSell = InputNumber(str);
					//clearShopDisplay();

					//updateDisplay();

					//key = pressKey();
					if (jewelsToSell == 0)
						damonMenu = 7;
					if (jewelsToSell > plyr.jewels)
						damonMenu = 12;
					if ((jewelsToSell > 0) && (jewelsToSell <= plyr.jewels))
						damonMenu = 13;
				}

				while (damonMenu == 12) // Insufficient jewels!
				{


					str = "You have only " + Itos(plyr.jewels) + "!";
					ClearShopDisplay();
					CyText(2, str);
					UpdateDisplay();
					key = GetSingleKey();
					if (key == "SPACE")
						damonMenu = 7;

				}

				while (damonMenu == 13) // jewels sold!
				{
					int totalSilvers = 32 * jewelsToSell;
					int goldFromjewels = 0;
					int silverFromjewels = 0;
					silverFromjewels = totalSilvers % 10;
					goldFromjewels = (totalSilvers - silverFromjewels) / 10;
				//str = itos(totalSilvers);
					str = "Here is " + Itos(goldFromjewels) + " gold and " + Itos(silverFromjewels) + " silver coins.";
					ClearShopDisplay();
					CyText(2, str);
					UpdateDisplay();
					key = GetSingleKey();
					if (key == "SPACE")
					{
						plyr.jewels -= jewelsToSell;
						plyr.gold += goldFromjewels;
						plyr.silver += silverFromjewels;
						damonMenu = 7;
					}
				}


				while (damonMenu == 14) // exchange silver & copper for gold
				{

					ClearShopDisplay();
					CyText(1, "For 11 silvers I will exchange all@of your silver & copper coins for gold.@This will lessen your load.@@ Would you like this? (Y or N)");

					UpdateDisplay();

					key = GetSingleKey();
					if (key == "Y")
					{
						totalSilver = plyr.silver + (plyr.gold * 10) + ((plyr.copper - (plyr.copper % 10)) / 10);
						damonMenu = 15;
					}
					if (key == "N")
						damonMenu = 7;
				}

				while (damonMenu == 15) // exchange silver & copper for gold
				{

					ClearShopDisplay();
					if (totalSilver < 11)
						str = "HA!  You haven't even the silver@to pay me!";
					else
						str = "It's done.";

					CyText(1, str);

					UpdateDisplay();

					key = GetSingleKey();
					if (key == "SPACE")
					{
						if (totalSilver > 10)
						{
							DeductCoins(0, 11, 0);
							totalSilver = plyr.silver + ((plyr.copper - (plyr.copper % 10)) / 10);
							plyr.gold += (totalSilver - totalSilver % 10) / 10;
							DeductCoins(0, (totalSilver - totalSilver % 10), 0);

						}
						damonMenu = 7;
					}
				}









			}
			LeaveShop();
		}
		public static void StockDamon()
		{
			for (int i = 0 ; i < 5 ; i++)
				damonStock[i] = Randn(1, 5) + 8;
		}
		public static void LoadDamonBinary()
		{
			// Loads armour,weapons and clothing binary data into the "damonBinary" array
			FileStream fp; // file pointer - used when reading files
			string tempString = new string(new char[100]); // temporary string
			tempString = string.Format("{0}{1}", "data/map/", "DamonItems.bin");
			fp = fopen(tempString, "rb");
			if (fp != null)
			{
				for (int i = 0;i < damonFileSize;i++)
					damonBinary[i] = fgetc(fp);
			}
			fclose(fp);
		}

	// Take a binary offset within damonBinary and create a new inventory item from the binary data (weapon, armour or clothing)
	// Item types:  03 - weapon, 04 - armour, 05 - clothing
	//MLT: No return value

		//MLT: No return value
		public static void CreateInventoryItem(int startByte)
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
			int itemType = damonBinary[offset];
			string itemName = ReadNameString((offset + 6));

			if (itemType == 3)
			{
				itemType = 178; // ARX value for weapon
				index = 0; // No longer required
				useStrength = 0;
				alignment = damonBinary[offset + 3];
				weight = damonBinary[offset + 4];

				wAttributes = (offset + damonBinary[offset + 1]) - 20; // Working out from the end of the weapon object

				melee = damonBinary[wAttributes + 1];
				ammo = damonBinary[wAttributes + 2];
				blunt = damonBinary[wAttributes + 3];
				sharp = damonBinary[wAttributes + 4];
				earth = damonBinary[wAttributes + 5];
				air = damonBinary[wAttributes + 6];
				fire = damonBinary[wAttributes + 7];
				water = damonBinary[wAttributes + 8];
				power = damonBinary[wAttributes + 9];
				magic = damonBinary[wAttributes + 10];
				good = damonBinary[wAttributes + 11];
				evil = damonBinary[wAttributes + 12];
				cold = damonBinary[wAttributes + 13];
				minStrength = damonBinary[wAttributes + 14];
				minDexterity = damonBinary[wAttributes + 15];
				hp = damonBinary[wAttributes + 16];
				maxHP = damonBinary[wAttributes + 17];
				flags = damonBinary[wAttributes + 18];
				parry = damonBinary[wAttributes + 19];
			}

			if (itemType == 4)
			{
				itemType = 177; // ARX value for armour
				index = 0; // No longer required
				useStrength = 0;
				alignment = damonBinary[offset + 3];
				weight = damonBinary[offset + 4];

				wAttributes = (offset + damonBinary[offset + 1]) - 15; // Working out from the end of the weapon object

				melee = damonBinary[wAttributes + 1]; // Body part
				ammo = 0; // Not used
				blunt = damonBinary[wAttributes + 2]; // ERROR ONWARDS
				sharp = damonBinary[wAttributes + 3];
				earth = damonBinary[wAttributes + 4];
				air = damonBinary[wAttributes + 5];
				fire = damonBinary[wAttributes + 6];
				water = damonBinary[wAttributes + 7];
				power = damonBinary[wAttributes + 8];
				magic = damonBinary[wAttributes + 9];
				good = damonBinary[wAttributes + 10];
				evil = damonBinary[wAttributes + 11];
				cold = damonBinary[wAttributes + 12];
				minStrength = 0;
				minDexterity = 0;
				hp = damonBinary[wAttributes + 13];
				maxHP = damonBinary[wAttributes + 14];
				flags = 0;
				parry = 0;
			}

			if (itemType == 5)
			{
				itemType = 180; // ARX value for clothing
				index = 0; // No longer required
				useStrength = 0;
				alignment = damonBinary[offset + 3];
				weight = damonBinary[offset + 4];

				wAttributes = (offset + damonBinary[offset + 1]) - 3; // Working out from the end of the weapon object

				melee = damonBinary[wAttributes + 1]; // clothing attribute
				ammo = damonBinary[wAttributes + 2]; // clothing attribute
				blunt = damonBinary[wAttributes + 3]; // clothing attribute
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

			int newItemRef = CreateItem(itemType, index, itemName, hp, maxHP, flags, minStrength, minDexterity, useStrength, blunt, sharp, earth, air, fire, water, power, magic, good, evil, cold, weight, alignment, melee, ammo, parry);
			itemBuffer[newItemRef].location = 10; // Add to player inventory - 10
		}
		public static string ReadNameString(int stringOffset)
		{
			stringstream ss = new stringstream();
			int z = stringOffset; // current location in the binary
			int c = 0; // current byte
			string result = "";

		   while (!(damonBinary[z] == 0))
		   {
				c = damonBinary[z];
				ss << (char) c;
				z++;
		   }
			result = ss.str();
			return result;
		}




		public static byte[] damonBinary = new byte[damonFileSize];
		public static int itemNameOffset;

		//extern buffer_item itemBuffer[100];


		public static int gemsToSell = 0;
		public static int jewelsToSell = 0;
		public static int totalSilver = 0;

		// provision variables
		public static string provisionDescription;
		public static string provMessage;
		public static int quantity;
		public static int total;
		public static int[] damonStock = new int[5];

		public static DamonBattleGearItem[] damonBattleGearWares =
		{
			new DamonBattleGearItem() { type = 177, price = 127, itemRef = 0x00 },
			new DamonBattleGearItem() { type = 177, price = 52, itemRef = 0x29 },
			new DamonBattleGearItem() { type = 177, price = 52, itemRef = 0x50 },
			new DamonBattleGearItem() { type = 177, price = 27, itemRef = 0x76 },
			new DamonBattleGearItem() { type = 178, price = 22, itemRef = 0x98 },
			new DamonBattleGearItem() { type = 178, price = 27, itemRef = 0xBB },
			new DamonBattleGearItem() { type = 178, price = 47, itemRef = 0x102 },
			new DamonBattleGearItem() { type = 178, price = 77, itemRef = 0x167 },
			new DamonBattleGearItem() { type = 178, price = 102, itemRef = 0x18C },
			new DamonBattleGearItem() { type = 178, price = 127, itemRef = 0x1B2 },
			new DamonBattleGearItem() { type = 178, price = 82, itemRef = 0x1D7 },
			new DamonBattleGearItem() { type = 178, price = 102, itemRef = 0x1FE }
		};

		public static DamonClothingItem[] damonClothingWares =
		{
			new DamonClothingItem() { type = 180, price = 12, itemRef = 0x225 },
			new DamonClothingItem() { type = 180, price = 17, itemRef = 0x245 },
			new DamonClothingItem() { type = 180, price = 17, itemRef = 0x261 },
			new DamonClothingItem() { type = 180, price = 52, itemRef = 0x27A },
			new DamonClothingItem() { type = 180, price = 62, itemRef = 0x296 },
			new DamonClothingItem() { type = 180, price = 12, itemRef = 0x2C9 },
			new DamonClothingItem() { type = 180, price = 17, itemRef = 0x2E4 },
			new DamonClothingItem() { type = 180, price = 22, itemRef = 0x300 },
			new DamonClothingItem() { type = 180, price = 22, itemRef = 0x31F },
			new DamonClothingItem() { type = 180, price = 62, itemRef = 0x33B },
			new DamonClothingItem() { type = 180, price = 62, itemRef = 0x358 },
			new DamonClothingItem() { type = 180, price = 82, itemRef = 0x375 }
		};




	}
}