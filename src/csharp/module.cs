using System;
using System.Collections.Generic;
using System.Linq;
using System;

namespace P3Net.Arx
{
	public class MenuItem
	{
		public int objRef { get; set; }
		public string menuName { get; set; }
		public string menuPrice { get; set; }
	}

	public partial class GlobalMembers
	{
		//extern menuItem menuItems[20];

		public static void UpdateModule(int module)
		{
			if (module == (int)Modules.Rathskeller)
				RunRathskeller();
			if (module == (int)Modules.DwarvenSmithy)
				RunDwarvenSmithy();
			if (module == (int)Modules.Vault)
				RunVault();
		}
		public static void RunModule(int module)
		{
			plyr.status = GameStates.Module;
			UpdateModule(module);
			if (plyr.facing == Directions.West)
				plyr.x = plyr.oldx;
			if (plyr.facing == Directions.East)
				plyr.x = plyr.oldx;
			if (plyr.facing == Directions.North)
				plyr.y = plyr.oldy;
			if (plyr.facing == Directions.South)
				plyr.y = plyr.oldy;

			//MLT: Double to float
			plyr.z_offset = 1.6F; // position player just outside door
			plyr.status = GameStates.Explore;
		}
		public static void DisplayModuleImage(int module)
		{
			App.clear();
			App.pushGLStates();
			//App.draw(ShopSprite);
			DrawStatsPanel();
		}
		public static int InputNumber(string message)
		{
			string str;
			string key;
			string inputText = "";
			int maxNumberSize = 6;
			bool enterKeyNotPressed = true;
			while (enterKeyNotPressed)
			{
				ClearShopDisplay();
				CyText(1, message);
				str = ">" + inputText + "_";
				BText(10, 7, str);
				UpdateDisplay();
				key = GetSingleKey();
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
					  int numberLength = inputText.Length;
					  inputText = inputText.Substring(0,(numberLength - 1));
					}
				}
				if (key == "RETURN")
					enterKeyNotPressed = false;
			}
			int value = Convert.ToInt32(inputText);
			return value;
		}
		public static string InputText(string message)
		{
			string str;
			string key;
			string inputText = "";
			int maxTextSize = 26;
			bool enterKeyNotPressed = true;
			while (enterKeyNotPressed)
			{
				ClearShopDisplay();
				CyText(1, message);
				str = ">" + inputText + "_";
				BText(10, 7, str);
				UpdateDisplay();
				key = GetTextChar();
				if (!((key == "BACKSPACE") || (key == "RETURN")))
				{
					if (key == "SPACE")
						key = " ";
					int textLength = inputText.Length;
					if (textLength < maxTextSize)
						inputText = inputText + key;
				}
				if (key == "BACKSPACE")
				{
					int textLength = inputText.Length;
					if (textLength != 0)
					{
					  int textLength = inputText.Length;
					  inputText = inputText.Substring(0,(textLength - 1));
					}
				}
				if (key == "RETURN")
					enterKeyNotPressed = false;
			}
			string myText = inputText;
			return myText;
		}

	// Returns an item reference based on a multi page menu e.g. food item, weapon item
		public static int InputItemChoice(string message, int totalItems)
		{
			// totalItems = maximum number of items possible across multiple pages
			bool noMenuSelection = true;
			string key;
			string str;
			int itemRef = 255; // Selected nothing / option 0 to go back
			int currentItem = 0;
			int menuPage = 0;
			int maximumMenuPage = CalculateMaximumMenuPage(totalItems);
			int minimumMenuPage = 0;
			int[] currentItemRefs = new int[MAX_MENU_ITEMS];

			// calculate number of menu pages

			while (noMenuSelection)
			{
				ClearShopDisplay();
				CyText(0, message);

				for (int i = 0 ; i < MAX_MENU_ITEMS ; i++)
				{
					currentItem = (menuPage * 6) + i;
					if (currentItem >= totalItems)
					{
						// Menu slot without an item
						currentItemRefs[i] = 256; // No item to select
						str = "(" + Itos(i + 1) + ")";
						BText(1, (2 + i), str);
					} else
					{
						// Menu slot showing an item
						currentItemRefs[i] = menuItems[currentItem].objRef;
						str = "(" + Itos(i + 1) + ") " + menuItems[currentItem].menuName;
						BText(1, (2 + i), str);
						if (GameStates.Module == Modules.DwarvenSmithy)
							BText(27, (2 + i), menuItems[currentItem].menuPrice);
						else
							BText(27, (2 + i), menuItems[currentItem].menuPrice); // was 31
				}
				}
				int bottomOfDisplay = 1 + MAX_MENU_ITEMS + 2;
				CyText(bottomOfDisplay, "Forward, Backward or ESC to exit");
				UpdateDisplay();

				key = GetSingleKey();
				if (key == "1")
				{
					itemRef = currentItemRefs[0];
					noMenuSelection = false;
				}
				if (key == "2")
				{
					itemRef = currentItemRefs[1];
					noMenuSelection = false;
				}
				if (key == "3")
				{
					itemRef = currentItemRefs[2];
					noMenuSelection = false;
				}
				if (key == "4")
				{
					itemRef = currentItemRefs[3];
					noMenuSelection = false;
				}
				if (key == "5")
				{
					itemRef = currentItemRefs[4];
					noMenuSelection = false;
				}
				if (key == "6")
				{
					itemRef = currentItemRefs[5];
					noMenuSelection = false;
				}
				if (key == "ESC")
					noMenuSelection = false;
				if (key == "0")
					noMenuSelection = false;
				if (key == "F")
				{
					if (menuPage < maximumMenuPage)
						menuPage++;
				}
				if (key == "B")
				{
					if (menuPage > minimumMenuPage)
						menuPage--;
				}
				if (key == "down")
				{
					if (menuPage < maximumMenuPage)
						menuPage++;
				}
				if (key == "up")
				{
					if (menuPage > minimumMenuPage)
						menuPage--;
				}

				if (itemRef == 256)
				{
					itemRef = 255;
					noMenuSelection = true;
				} // Return to menu if empty menu item slot
			}
			return itemRef;
		}
		public static int CalculateMaximumMenuPage(int numberOfItems)
		{
			int maxPageNumber = (numberOfItems / MAX_MENU_ITEMS);
			if (numberOfItems % MAX_MENU_ITEMS > 0)
				maxPageNumber++;
			maxPageNumber--;
			return maxPageNumber;
		}
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//void LeaveModule();



		//MLT: Make const
		public static readonly int MAX_MENU_ITEMS = 6; // Max 6 items per menu page. Should be usable for building any type of general shop menus with a maximum of 20 multi page items
		public static MenuItem[] menuItems = Arrays.InitializeWithDefaultInstances<MenuItem>(20); // Should be usable for building any type of general shop menus with a maximum of 20 multi page items

	}
}