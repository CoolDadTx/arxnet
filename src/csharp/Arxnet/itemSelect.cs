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

namespace P3Net.Arx
{
    public class ItemMenuEntry
    {
        public string menuName { get; set; }

        public int objRef { get; set; }
    }

    public partial class GlobalMembers
    {
        public static ItemMenuEntry[] itemSelectEntries = Arrays.InitializeWithDefaultInstances<ItemMenuEntry>(255); // Should be usable for building item menus with a maximum of 255 multi page items

        public const int MAX_MENU_ENTRIES = 4; // Max 4 entries per menu page.

        public static int CalculateLastMenuPage ( int numberOfItems )
        {
            var maxPageNumber = (numberOfItems / MAX_MENU_ENTRIES);
            if (numberOfItems % MAX_MENU_ENTRIES > 0)
                maxPageNumber++;
            maxPageNumber--;
            return maxPageNumber;
        }

        // Returns an item reference based on a multi page menu e.g. food item, weapon item
        public static int InputItemRef ( string message )
        {
            var noMenuSelection = true;
            var itemRef = 255;
            var currentItem = 0;
            var totalItems = 20; // needs to be calculated separately to total up items
            var menuPage = 0;
            var maximumMenuPage = CalculateLastMenuPage(totalItems);
            var minimumMenuPage = 0;
            var currentItemRefs = new int[MAX_MENU_ENTRIES];

            // calculate number of menu pages

            while (noMenuSelection)
            {
                CyText(0, message);

                for (var i = 0; i < MAX_MENU_ENTRIES; i++)
                {
                    currentItem = (menuPage * 6) + i;
                    if (currentItem >= totalItems)
                    {
                        // Menu slot without an item
                        currentItemRefs[i] = 256; // No item to select
                        BText(1, (2 + i), $"({Itos(i + 1)})");
                    } else
                    {
                        // Menu slot showing an item
                        currentItemRefs[i] = itemSelectEntries[currentItem].objRef;
                        BText(1, (2 + i), $"({Itos(i + 1)}) {itemSelectEntries[currentItem].menuName}");
                    }
                }
                var bottomOfDisplay = 1 + MAX_MENU_ENTRIES + 2;
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

        //extern itemMenuEntry itemSelectEntries[255];
    }
}