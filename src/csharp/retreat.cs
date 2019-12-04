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
        public static string Concat(int n, string str)
        {
            var ss = new std::ostringstream();
                ss << n;
                ss << str;
            return ss.str();
        }

        public static int InputValue()
        {
            var itemQuantity = 0;

            string str;
            string key;
            var inputText = "";
            var maxNumberSize = 6;
            var enterKeyNotPressed = true;
            while(enterKeyNotPressed)
            {
                ClearShopDisplay();
                CyText(0,
                       "Thou mayest have a spot on@the floor for a small donation.@@How many coppers woulds't thou give?");

                str = $">{inputText}_";
                BText(17, 5, str);
                UpdateDisplay();
                key = GetSingleKey();
                if((key == "0") ||
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
                    int numberLength = inputText.Length;
                    if(numberLength < maxNumberSize)
                        inputText = inputText + key;
                }
                if(key == "BACKSPACE")
                {
                    int numberLength = inputText.Length;
                    if(numberLength != 0)
                    {
                        int numberLength = inputText.Length;
                        inputText = inputText.Substring(0, (numberLength - 1));
                    }
                }
                if(key == "RETURN")
                    enterKeyNotPressed = false;
                if(key == "ESC")
                {
                    itemQuantity = 0;
                    enterKeyNotPressed = false;
                }
            }
            itemQuantity = Convert.ToInt32(inputText);

            return itemQuantity;
        }

        public static void ShopRetreat()
        {
            var retreatMenu = 1; // high level menu
            var hoursSlept = 0; // number of hours slept
            var roomChoice = 0; // spot by door, drafty spot or by fire
            string str;
            string key;
            plyr.status = 2; // shopping

            SetAutoMapFlag(plyr.map, 55, 3);
            SetAutoMapFlag(plyr.map, 56, 3);
            SetAutoMapFlag(plyr.map, 55, 2);
            SetAutoMapFlag(plyr.map, 56, 2);

            LoadShopImage(1);

            while(retreatMenu > 0)
            {
                var firstVisit = false;
                if(plyr.retreatFriendship == 5)
                    firstVisit = true;

                var coppers = 0;
                while(retreatMenu == 1) // main menu
                {
                    ClearShopDisplay();
                    if(firstVisit)
                        str = "Welcome, Stranger, to our meager hovel.";
                    else
                    {
                        if(plyr.retreatFriendship > 1)
                            str = "Greetings, Explorer. Welcome back!";
                        if(plyr.retreatFriendship < 2)
                            str = "Art thou back again?";
                    }
                    CyText(1, str);
                    CyText(3, "Woulds't thou like to");
                    BText(15, 5, "(1) Sleep");
                    BText(15, 6, "(0) Leave");
                    UpdateDisplay();

                    key = GetSingleKey();

                    if(key == "1")
                        retreatMenu = 2;
                    if(key == "0")
                        retreatMenu = 0;
                }

                while(retreatMenu == 2) // how many coppers?
                {
                    coppers = InputValue();

                    if(coppers > 0)
                    {
                        if(CheckCoins(0, 0, coppers))
                        {
                            DeductCoins(0, 0, coppers);
                            retreatMenu = 4;
                        } else
                        {
                            retreatMenu = 3; // insufficient funds
                        }
                    }

                    if(coppers == 0)
                        retreatMenu = 1;
                }

                while(retreatMenu == 3) // insufficient funds
                {
                    ClearShopDisplay();
                    CyText(2, "Thou has offered more@@than thou hast!");
                    UpdateDisplay();
                    key = GetSingleKey();
                    if(key != "")
                        retreatMenu = 1;
                }

                while(retreatMenu == 4) // coppers offering message
                {
                    ClearShopDisplay();
                    if(coppers < 15)
                    {
                        str = "That's not enough to@@cover the cost of delousing!@@Take the drafty spot near the door!";
                        roomChoice = 0;
                        plyr.retreatFriendship--;
                        if(plyr.retreatFriendship < 0)
                            plyr.retreatFriendship = 0;
                    }
                    if((coppers > 14) && (coppers < 31))
                    {
                        str = "Pinching pennies, eh?@@Well, lie down in that corner!";
                        roomChoice = 1;
                    }
                    if(coppers > 30)
                    {
                        str = "Thou art generous to a fault!@@Take this place next to the fire!";
                        roomChoice = 2;
                        plyr.retreatFriendship++;
                        if(plyr.retreatFriendship > 4)
                            plyr.retreatFriendship = 4;
                    }
                    CyText(2, str);
                    UpdateDisplay();
                    key = GetSingleKey();
                    if(key != "")
                        retreatMenu = 5;
                }

                while(retreatMenu == 5) // Sleeping
                {
                    key = "";
                    while((key == "") && (hoursSlept < 8))
                    {
                        ClearShopDisplay();
                        CyText(3, "Thou sleepest.");
                        CyText(6, "(Press SPACE when ready to awaken.)");
                        UpdateDisplay();
                        key = GetSingleKey();

                        var roomProb = 0;
                        if(roomChoice == 0)
                            roomProb = 40;
                        if(roomChoice == 1)
                            roomProb = 60;
                        if(roomChoice == 2)
                            roomProb = 80;
                        int actualSleepProb = Randn(0, 100);
                        if(actualSleepProb <= roomProb)
                        {
                            plyr.hp = plyr.hp + Randn(1, 5);
                            if(plyr.hp > plyr.maxhp)
                                plyr.hp = plyr.maxhp;
                        }
                        AddHour();
                        hoursSlept++;
                        sf.sleep(sf.seconds(1));
                    }
                    retreatMenu = 6;
                }

                while(retreatMenu == 6) // Dreams troubled...
                {
                    ClearShopDisplay();
                    if(plyr.alignment < 129)
                        str = "Thy sleep is troubled by evil dreams.@@Thou wakest in a cold sweat!";
                    if(plyr.alignment > 128)
                        str = "Thy dreams are warm and bright.@@Thou wakest rested and refreshed.";
                    CyText(2, str);
                    UpdateDisplay();
                    key = GetSingleKey();
                    if(key != "")
                        retreatMenu = 7;
                }

                while(retreatMenu == 7) // You slept for...
                {
                    string monthDesc;
                    switch(plyr.months)
                    {
                        case 1:
                            monthDesc = "Rebirth";
                            break;
                        case 2:
                            monthDesc = "Awakening";
                            break;
                        case 3:
                            monthDesc = "Winds";
                            break;
                        case 4:
                            monthDesc = "Rains";
                            break;
                        case 5:
                            monthDesc = "Sowings";
                            break;
                        case 6:
                            monthDesc = "First Fruits";
                            break;
                        case 7:
                            monthDesc = "Harvest";
                            break;
                        case 8:
                            monthDesc = "Final Reaping";
                            break;
                        case 9:
                            monthDesc = "The Fall";
                            break;
                        case 10:
                            monthDesc = "Darkness";
                            break;
                        case 11:
                            monthDesc = "Cold Winds";
                            break;
                        case 12:
                            monthDesc = "Lights";
                            break;
                    }

                    ClearShopDisplay();
                    str = $"Thou hast slept for {Itos(hoursSlept)} hours.";
                    CyText(1, str);
                    str = $"It is day {Itos(plyr.days)}";
                    CyText(3, str);
                    str = $"in the month of {monthDesc}";
                    CyText(4, str);
                    str = $"year {Itos(plyr.years)} since abduction.";
                    CyText(5, str);
                    UpdateDisplay();
                    key = GetSingleKey();
                    if(key != "")
                        retreatMenu = 0;
                }
            }
            if(plyr.retreatFriendship == 5)
                plyr.retreatFriendship = 2;
            LeaveShop();
        }

        // extern Player plyr;
        // extern sf::RenderWindow App;
    }
}