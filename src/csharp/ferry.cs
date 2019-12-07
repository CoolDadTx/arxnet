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
    public partial class GlobalMembers
    {        
        public static void ShopFerry()
        {
            var ferryMenu = 1; // high level menu
            
            plyr.status = 2; // shopping

            SetAutoMapFlag(plyr.map, 28, 19); // But does the map KNOW about the ferry type to colour it in?

            LoadShopImage(23);

            while(ferryMenu > 0)
            {
                ModuleMessage("You are at a river crossing.@@The river looks too swift to safely@@swim across.");

                while(ferryMenu == 1) // main menu
                {
                    ClearShopDisplay();
                    CyText(0,
                           "A ghostly figure is waiting@for you at the dock...@He slowly opens his@outstretched hand.");
                    BText(4, 5, "Do you (1) Offer something or");
                    BText(4, 6, "       (0) Leave");

                    UpdateDisplay();

                    var key = GetSingleKey();

                    if(key == "0")
                    {
                        ferryMenu = 0;
                        LeaveShop();
                    }
                    if(key == "1")
                        ferryMenu = 2;
                    if(key == "down")
                        ferryMenu = 0;
                }

                while(ferryMenu == 2) // main menu
                {
                    var offerAccepted = false;
                    var offerText = "";

                    var itemQuantity = 0;
                    var itemRef = SelectItem(3); // select an item in OFFER mode
                    if(itemRef == 9999)
                    {
                        ferryMenu = 1;
                        break;
                    } // break out of loop???

                    if(itemRef == 1011) // Copper
                    {
                        offerText = "Copper";
                        itemQuantity = InputItemQuantity(3);
                        // Check that item quantity is valid!
                        if(itemQuantity > plyr.copper)
                            ModuleMessage("You don't have enough.");
                        if((itemQuantity == 2) && (plyr.copper >= 2) && (plyr.hours == 0) && (plyr.minutes == 0))
                        {
                            plyr.copper -= 2;
                            offerAccepted = true;
                        }
                    }

                    var offerText = "";
                    if ((itemRef > 999) && (itemRef < 1011))
                    {
                        itemQuantity = InputItemQuantity(3); // Offer text is 3

                        // Check that item quantity is valid!                        
                        switch(itemRef)
                        {
                            case 1000:
                                offerText = "Food Packet(s)";
                                break;
                            case 1001:
                                offerText = "Water Flask(s)";
                                break;
                            case 1002:
                                offerText = "Torch(es)";
                                break;
                            case 1003:
                                offerText = "Timepiece(s)";
                                break;
                            case 1004:
                                offerText = "Compass(es)";
                                break;
                            case 1005:
                                offerText = "Key(s)";
                                break;
                            case 1006:
                                offerText = "Crystal(s)";
                                break;
                            case 1007:
                                offerText = "Gem(s)";
                                break;
                            case 1008:
                                offerText = "Jewel(s)";
                                break;
                            case 1010:
                                offerText = "Silver";
                                break;
                        }
                    }

                    if(itemRef < 100) // A real object in your inventory
                    {
                        offerText = GetItemDesc(itemRef);
                        MoveItem(itemRef, 0); // move the item to the void
                    }

                    // Need to remove the volume objects from the inventory!
                    if(offerAccepted)
                    {
                        // Take the player ACROSS the river to the Undead Regions
                        ModuleMessage("The mysterious boatman takes his@@fee and places it in his pouch.@@He then rows you across the@@river in his boat.");
                        plyr.x = 28;
                        plyr.y = 20;
                        // Add forced facing south?
                    } else
                    {
                        // Take the player down river if not midnight or correct offer
                        string str = $"The mysterious boatman takes@@your {offerText} and proceeds to@@take you down the river in his boat.";
                        ModuleMessage(str);
                        plyr.x = 11;
                        plyr.y = 30;
                    }

                    ferryMenu = 0;

                    //MLT: Double to float
                    plyr.z_offset = 1.6F; // position player just outside door
                    plyr.status = 1; // explore
                }
            }
        }

        // extern Player plyr;
        // extern sf::RenderWindow App;
    }
}