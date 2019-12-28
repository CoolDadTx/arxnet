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
    public partial class GlobalMembers
    {
        //TODO: Make a module a first class citizen with control over the game loop, available items, etc
        public static void RunModule ( Modules module )
        {
            plyr.status = GameStates.Module;
            UpdateModule(module);

            switch (plyr.facing)
            {
                case Directions.East:
                case Directions.West: plyr.Position = plyr.Position.WithX(plyr.OldLocation.X); break;

                case Directions.North:
                case Directions.South: plyr.Position = plyr.Position.WithY(plyr.OldLocation.Y); break;
            };

            //MLT: Double to float
            plyr.z_offset = 1.6F; // position player just outside door
            plyr.status = GameStates.Explore;
        }

        //TODO: Move to Input component
        // Returns an item reference based on a multi page menu e.g. food item, weapon item
        public static int InputItemChoice ( string message, int totalItems )
        {
            // totalItems = maximum number of items possible across multiple pages
            var noMenuSelection = true;

            var itemRef = 255; // Selected nothing / option 0 to go back
            var menuPage = 0;
            var maximumMenuPage = CalculateMaximumMenuPage(totalItems);
            var minimumMenuPage = 0;
            var currentItemRefs = new int[MAX_MENU_ITEMS];

            // calculate number of menu pages

            while (noMenuSelection)
            {
                ClearShopDisplay();
                CyText(0, message);

                for (var i = 0; i < MAX_MENU_ITEMS; i++)
                {
                    var currentItem = (menuPage * 6) + i;
                    if (currentItem >= totalItems)
                    {
                        // Menu slot without an item
                        currentItemRefs[i] = 256; // No item to select
                        BText(1, (2 + i), $"({i + 1})");
                    } else
                    {
                        // Menu slot showing an item
                        currentItemRefs[i] = menuItems[currentItem].objRef;
                        BText(1, (2 + i), $"({i + 1}) {menuItems[currentItem].menuName}");
                        BText(27, (2 + i), menuItems[currentItem].menuPrice);
                    };
                };

                var bottomOfDisplay = 1 + MAX_MENU_ITEMS + 2;
                CyText(bottomOfDisplay, "Forward, Backward or ESC to exit");
                UpdateDisplay();

                var key = GetSingleKey();
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

        //TODO: Move to Input component
        public static int InputNumber ( string message )
        {
            var inputText = "";
            var maxNumberSize = 6;
            var enterKeyNotPressed = true;
            while (enterKeyNotPressed)
            {
                ClearShopDisplay();
                CyText(1, message);
                BText(10, 7, $">{inputText}_");
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

            //TODO: Does this work with RETURN
            var value = Convert.ToInt32(inputText);
            return value;
        }

        //TODO: Move to Input component
        public static string InputText ( string message )
        {
            var inputText = "";
            var maxTextSize = 26;
            var enterKeyNotPressed = true;
            while (enterKeyNotPressed)
            {
                ClearShopDisplay();
                CyText(1, message);
                BText(10, 7, $">{inputText}_");
                UpdateDisplay();
                var key = GetTextChar();
                if (!((key == "BACKSPACE") || (key == "RETURN")))
                {
                    if (key == "SPACE")
                        key = " ";
                    var textLength = inputText.Length;
                    if (textLength < maxTextSize)
                        inputText = inputText + key;
                }
                if (key == "BACKSPACE")
                {
                    var textLength = inputText.Length;
                    if (textLength != 0)
                    {
                        inputText = inputText.Substring(0, (textLength - 1));
                    }
                }
                if (key == "RETURN")
                    enterKeyNotPressed = false;
            }

            return inputText;
        }

        #region Review Data

        public const int MAX_MENU_ITEMS = 6; // Max 6 items per menu page. Should be usable for building any type of general shop menus with a maximum of 20 multi page items
        public static MenuItem[] menuItems = Arrays.InitializeWithDefaultInstances<MenuItem>(20); // Should be usable for building any type of general shop menus with a maximum of 20 multi page items

        #endregion

        #region Private Members

        private static int CalculateMaximumMenuPage ( int numberOfItems )
        {
            var maxPageNumber = (numberOfItems / MAX_MENU_ITEMS);
            if (numberOfItems % MAX_MENU_ITEMS > 0)
                maxPageNumber++;
            maxPageNumber--;
            return maxPageNumber;
        }                              

        private static void UpdateModule ( Modules module )
        {
            if (module == Modules.RATHSKELLER)
                RunRathskeller();
            if (module == Modules.DwarvenSmithy)
                RunDwarvenSmithy();
            if (module == Modules.VAULT)
                RunVault();
        }
        #endregion
    }
}