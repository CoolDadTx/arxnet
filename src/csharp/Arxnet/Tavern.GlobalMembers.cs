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
using SFML.Audio;

namespace P3Net.Arx
{
    public partial class GlobalMembers
    {        
        public static void CheckDailyTavernJobOpenings ()
        {
            // Run at the start of each new day
            for (var i = 0; i < 14; i++) // 14 taverns in total
            {
                var jobOpeningProbability = Random(0, 255);
                if (jobOpeningProbability <= Taverns[i].jobProbability)
                {
                    // Create a new job entry for the day
                    var newJobNumber = Random(0, 2);
                    tavernJobOpenings[i].jobNumber = newJobNumber;
                    tavernJobOpenings[i].JobHoursRequired = Random(0, 5) + 3;
                    tavernJobOpenings[i].jobHourlyIncome = Random(tavernJobs[newJobNumber].minIncome,
                                                                 tavernJobs[newJobNumber].maxIncome);
                } else
                {
                    // No job available today
                    tavernJobOpenings[i].jobNumber = 255; // 255 for none
                }
            }
        }
        
        public static void ShopTavern ()
        {
            var tavernNo = GetTavernNo();

            LoadShopImage(10);

            if (tavernNo == 3)
            {
                SetAutoMapFlag(plyr.map, 7, 39);
                SetAutoMapFlag(plyr.map, 8, 39);
                SetAutoMapFlag(plyr.map, 9, 39);
                SetAutoMapFlag(plyr.map, 10, 39);
            }
            if (tavernNo == 6)
            {
                SetAutoMapFlag(plyr.map, 32, 44);
                SetAutoMapFlag(plyr.map, 31, 44);
                SetAutoMapFlag(plyr.map, 33, 44);
                SetAutoMapFlag(plyr.map, 31, 43);
            }
            if (tavernNo == 9)
            {
                SetAutoMapFlag(plyr.map, 39, 34);
                SetAutoMapFlag(plyr.map, 39, 33);
            }
            if (tavernNo == 12)
            {
                SetAutoMapFlag(plyr.map, 5, 28);
                SetAutoMapFlag(plyr.map, 6, 28);
            }

            int workingHours = 0;
            int hourlyRate;
            int jobIncome = 0;
            descriptionPointer = 0;
            eatDrinkDescriptions[0] = "";
            eatDrinkDescriptions[1] = "";
            eatDrinkDescriptions[2] = "";
            eatDrinkDescriptions[3] = "";

            tavernNo = GetTavernNo();

            tavernMusic.Loop = false;
            var musicPlaying = false;
            var tavernMenu = 1; // high level menu
            var tavernLoc = 0; // bar, table or booth

            plyr.status = GameStates.Module; // shopping

            if ((Taverns[tavernNo].closingHour <= plyr.hours) || (Taverns[tavernNo].openingHour > plyr.hours))
                tavernMenu = 20;
            if (Taverns[tavernNo].membershipFee > 0)
                tavernMenu = 21;

            while (tavernMenu > 0)
            {
                while (tavernMenu == 20) // closed
                {
                    var openHour = Taverns[tavernNo].openingHour;
                    var closeHour = Taverns[tavernNo].closingHour;
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

                    TavernDisplayUpdate();
                    CyText(1, "Sorry, we are closed. Come back@during our working hours.");
                    CyText(4, $"We are open from {Taverns[tavernNo].openingHour}:00 in the {openingText}@to {Taverns[tavernNo].closingHour}:00 in the {closingText}.");
                    CyText(9, "( Press a key )");
                    UpdateDisplay();

                    var key = GetSingleKey();
                    if (key == "SPACE")
                        tavernMenu = 0;
                }

                while (tavernMenu == 21) // membership fee required
                {
                    TavernDisplayUpdate();
                    CyText(1, "To enter you must become a member.");
                    CyText(3, $"Dues are {ToCurrency(Taverns[tavernNo].membershipFee)} copper coins.");
                    CyText(5, "( es or  o)");
                    SetFontColor(40, 96, 244, 255);
                    CyText(5, " Y      N  ");
                    SetFontColor(215, 215, 215, 255);
                    DisplayCoins();
                    UpdateDisplay();

                    var key = GetSingleKey();
                    if (key == "N")
                        tavernMenu = 0;
                    if (key == "Y")
                        tavernMenu = 22;
                }

                while (tavernMenu == 22) // Attempt to buy a club membership
                {
                    tavernNo = GetTavernNo();
                    var membershipCost = Taverns[tavernNo].membershipFee;
                    if (!CheckCoins(0, 0, membershipCost))
                        tavernMenu = 23;
                    else
                    {
                        DeductCoins(0, 0, membershipCost);
                        Taverns[tavernNo].membershipFee = 0; // needs reseting on new game!
                        tavernMenu = 1;
                    }
                }

                while (tavernMenu == 23) // Insufficient funds for club membership
                {
                    TavernDisplayUpdate();
                    CyText(3, "I'm sorry... You have not the funds.");
                    UpdateDisplay();
                    var key = GetSingleKey();
                    if (key == "SPACE")
                        tavernMenu = 0;
                }

                while (tavernMenu == 1) // main menu
                {
                    // check whether player requires food and drink based on tavern friendship level
                    var countedCoppers = (plyr.gold * 100) + (plyr.silver * 10) + plyr.copper;
                    if ((plyr.tavernFriendships[tavernNo] >= 4) &&
                        (plyr.water == 0) &&
                        (countedCoppers == 0) &&
                        (plyr.thirst > 56))
                    {
                        plyr.thirst = 0;
                        plyr.water = 1;
                        TavernMessage("Friend, you thirst. Let me get you a drink.");
                    }
                    if ((plyr.tavernFriendships[tavernNo] >= 4) &&
                        (plyr.food == 0) &&
                        (countedCoppers == 0) &&
                        (plyr.hunger > 96))
                    {
                        plyr.hunger = 0;
                        plyr.food = 1;
                        TavernMessage("Friend, you hunger. Let me get you some food.");
                    }

                    TavernDisplayUpdate();

                    BText(7, 0, "Welcome Stranger! You are at ");
                    BText(7, 1, "the entrance. Do you wish to");
                    BText(9, 3, " ) Go to the bar");
                    BText(9, 4, " ) Get a table");
                    BText(9, 5, " ) Sit in a private booth");
                    BText(9, 6, " ) Apply for a job");
                    BText(9, 7, " ) Leave");
                    SetFontColor(40, 96, 244, 255);
                    BText(9, 3, "1");
                    BText(9, 4, "2");
                    BText(9, 5, "3");
                    BText(9, 6, "4");
                    BText(9, 7, "0");
                    SetFontColor(215, 215, 215, 255);
                    DisplayCoins();

                    UpdateDisplay();

                    if (!musicPlaying)
                    {
                        var newMusic = plyr.musicStyle;


                        //TODO: Normalize file system
                        var musicPath = newMusic ? "data/audio/B/" : "data/audio";
                        string musicFile;

                        switch (Random(1, 5))
                        {
                            case 1:
                            musicFile = "dwarfdance.ogg";
                            break;
                            case 2:
                            musicFile = "thoreandan.ogg";
                            break;
                            case 3:
                            musicFile = "waves.ogg";
                            break;
                            case 4:
                            {
                                //TODO: Fix the differences
                                musicPath = "data/audio/B/";
                                musicFile = newMusic ? "LetInTheLight.ogg" : "moments.ogg";
                                break;
                            };
                            case 5:
                            musicFile = "TheNightstalker.ogg";
                            break;

                            default:
                            throw new InvalidOperationException("Unknown music");
                        };

                        var lyricsFilename = System.IO.Path.ChangeExtension(musicFile, ".txt");

                        tavernMusic = new Music(musicPath + musicFile);
                        LoadLyrics(lyricsFilename);

                        tavernMusic.Play();
                        musicPlaying = true;
                    }

                    var key = GetSingleKey();

                    if (key == "1")
                    {
                        tavernMenu = 2;
                        tavernLoc = 1;
                    }
                    if (key == "2")
                    {
                        tavernMenu = 2;
                        tavernLoc = 2;
                    }
                    if (key == "3")
                    {
                        tavernMenu = 2;
                        tavernLoc = 3;
                    }
                    if (key == "4")
                        tavernMenu = 11;
                    if (key == "0")
                        tavernMenu = 0;
                    if (key == "down")
                        tavernMenu = 0;
                    if (key == "F1")
                    {
                        tavernMusic.Stop();
                        LoadLyrics(lyricsFilename);
                        tavernMusic.Play();
                    }
                }

                while (tavernMenu == 2) // at bar, table or booth menu
                {
                    TavernDisplayUpdate();
                    if (tavernLoc == 1)
                    {
                        BText(7, 0, "You are sitting at the bar.");
                        BText(23, 3, "A few nuts");
                    }
                    if (tavernLoc == 2)
                    {
                        BText(7, 0, "You are at your table.");
                        BText(26, 3, "Popcorn");
                    }
                    if (tavernLoc == 3)
                    {
                        BText(7, 0, "You are in a private booth.");
                        BText(7, 3, "A smokey torch  A few nuts");
                        tavernMusic.Stop();
                        musicPlaying = false;
                    }
                    BText(7, 4, " ) Hail the Barkeeper");
                    BText(7, 5, " ) Hail the Waitress");
                    BText(7, 6, " ) Buy a round for the house");
                    BText(7, 7, " ) Leave");
                    SetFontColor(40, 96, 244, 255);
                    BText(7, 4, "1");
                    BText(7, 5, "2");
                    BText(7, 6, "3");
                    BText(7, 7, "0");
                    SetFontColor(215, 215, 215, 255);
                    DisplayCoins();

                    SetFontColor(208, 178, 2, 255);
                    BText(7, 1, (eatDrinkDescriptions[0]));
                    BText(23, 1, (eatDrinkDescriptions[1]));
                    BText(7, 2, (eatDrinkDescriptions[2]));
                    BText(23, 2, (eatDrinkDescriptions[3]));
                    SetFontColor(215, 215, 215, 255);

                    UpdateDisplay();

                    var key = GetSingleKey();
                    if (key == "0")
                        tavernMenu = 0;
                    if (key == "1")
                        tavernMenu = 3;
                    if (key == "2")
                        tavernMenu = 7;
                    if (key == "3")
                        tavernMenu = 30;
                }

                while (tavernMenu == 3) // Order a drink
                {
                    TavernDisplayUpdate();
                    CyText(0, "What would you like? (  to go back)");
                    SetFontColor(40, 96, 244, 255);
                    CyText(0, "                      0            ");
                    SetFontColor(215, 215, 215, 255);

                    tavernNo = GetTavernNo();
                    for (var i = 0; i < 6; i++)
                    {
                        var itemNo = tavernDailyDrinks[tavernNo, i];
                        str = $") {tavernDrinks[itemNo].name}";
                        BText(3, (2 + i), str); //was 4
                        BText(1, (2 + i), "                                 coppers");
                    }
                    DisplayCoins();

                    for (var i = 0; i < 6; i++) // Max 6 drink items on menu each day
                    {
                        var x = 33;
                        int itemNo = tavernDailyDrinks[tavernNo, i];

                        //MLT: Downcast to int
                        var itemCost = (int)(Taverns[tavernNo].priceFactor * tavernDrinks[itemNo].basePrice);

                        if (itemCost < 10)
                            x = 34;
                        if ((itemCost > 9) && (itemCost < 100))
                            x = 32;
                        if (itemCost < 1000)
                            x = 30;
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
                    SetFontColor(215, 215, 215, 255);

                    UpdateDisplay();

                    var key = GetSingleKey();
                    if (key == "1")
                    {
                        drinkChoice = 0;
                        tavernMenu = 4;
                    }
                    if (key == "2")
                    {
                        drinkChoice = 1;
                        tavernMenu = 4;
                    }
                    if (key == "3")
                    {
                        drinkChoice = 2;
                        tavernMenu = 4;
                    }
                    if (key == "4")
                    {
                        drinkChoice = 3;
                        tavernMenu = 4;
                    }
                    if (key == "5")
                    {
                        drinkChoice = 4;
                        tavernMenu = 4;
                    }
                    if (key == "6")
                    {
                        drinkChoice = 5;
                        tavernMenu = 4;
                    }
                    if (key == "ESC")
                        tavernMenu = 0;
                    if (key == "0")
                        tavernMenu = 2;
                }

                while (tavernMenu == 4) // Attempt to buy a drink
                {
                    tavernNo = GetTavernNo();
                    drinkNo = tavernDailyDrinks[tavernNo, drinkChoice];

                    //MLT: Downcast to int
                    drinkCost = (int)(Taverns[tavernNo].priceFactor * tavernDrinks[drinkNo].basePrice);
                    if (!CheckCoins(0, 0, drinkCost))
                        tavernMenu = 5;
                    else
                        tavernMenu = 6;
                }

                while (tavernMenu == 5) // Insufficient funds
                {
                    TavernDisplayUpdate();
                    CyText(3, "I'm sorry... You have not the funds.");
                    UpdateDisplay();
                    var key = GetSingleKey();
                    if (key == "SPACE")
                        tavernMenu = 3;
                }

                while (tavernMenu == 6) // Successful purchase
                {
                    TavernDisplayUpdate();
                    CyText(3, "Right away!");
                    UpdateDisplay();
                    var key = GetSingleKey();
                    if (key == "SPACE")
                    {
                        tavernMenu = 2;
                        DeductCoins(0, 0, drinkCost);
                        plyr.thirst -= tavernDrinks[drinkNo].thirstRemoved;
                        if (plyr.thirst < 0)
                            plyr.thirst = 0;
                        plyr.alcohol += tavernDrinks[drinkNo].alcoholAdded;
                        plyr.water += tavernDrinks[drinkNo].waterFlaskAdded;
                        eatDrinkDescriptions[descriptionPointer] = tavernDrinks[drinkNo].name;
                        if (descriptionPointer == 3)
                            descriptionPointer = 0;
                        else
                            descriptionPointer++;
                    }
                }

                while (tavernMenu == 7) // Order food
                {
                    TavernDisplayUpdate();
                    CyText(0, "What would you like? (  to go back)");
                    SetFontColor(40, 96, 244, 255);
                    CyText(0, "                      0            ");
                    SetFontColor(215, 215, 215, 255);

                    tavernNo = GetTavernNo();
                    for (var i = 0; i < 6; i++)
                    {
                        var itemNo = tavernDailyFoods[tavernNo, i];
                        BText(3, (2 + i), $") {tavernFoods[itemNo].name}");
                        BText(1, (2 + i), "                                 coppers");
                    }
                    DisplayCoins();

                    for (var i = 0; i < 6; i++) // Max 6 drink items on menu each day
                    {
                        var x = 33;
                        var itemNo = tavernDailyFoods[tavernNo, i];

                        //MLT: Downcast to int
                        var itemCost = (int)(Taverns[tavernNo].priceFactor * tavernFoods[itemNo].basePrice);

                        if (itemCost < 10)
                            x = 37;
                        if ((itemCost > 9) && (itemCost < 100))
                            x = 34;
                        if (itemCost < 1000)
                            x = 30;

                        BText(x, (i + 2), ToCurrency(itemCost));
                    }

                    SetFontColor(40, 96, 244, 255);
                    BText(2, 2, "1");
                    BText(2, 3, "2");
                    BText(2, 4, "3");
                    BText(2, 5, "4");
                    BText(2, 6, "5");
                    BText(2, 7, "6");
                    SetFontColor(215, 215, 215, 255);

                    UpdateDisplay();

                    var key = GetSingleKey();
                    if (key == "1")
                    {
                        foodChoice = 0;
                        tavernMenu = 8;
                    }
                    if (key == "2")
                    {
                        foodChoice = 1;
                        tavernMenu = 8;
                    }
                    if (key == "3")
                    {
                        foodChoice = 2;
                        tavernMenu = 8;
                    }
                    if (key == "4")
                    {
                        foodChoice = 3;
                        tavernMenu = 8;
                    }
                    if (key == "5")
                    {
                        foodChoice = 4;
                        tavernMenu = 8;
                    }
                    if (key == "6")
                    {
                        foodChoice = 5;
                        tavernMenu = 8;
                    }

                    if (key == "ESC")
                        tavernMenu = 0;
                    if (key == "0")
                        tavernMenu = 2;
                }

                while (tavernMenu == 8) // Attempt to buy food
                {
                    tavernNo = GetTavernNo();
                    foodNo = tavernDailyFoods[tavernNo, foodChoice];

                    //MLT: Downcast to int
                    foodCost = (int)(Taverns[tavernNo].priceFactor * tavernFoods[foodNo].basePrice);
                    tavernMenu = !CheckCoins(0, 0, foodCost) ? 9 : 10;
                }

                while (tavernMenu == 9) // Insufficient funds
                {
                    TavernDisplayUpdate();
                    CyText(3, "I'm sorry... You have not the funds.");
                    UpdateDisplay();
                    var key = GetSingleKey();
                    if (key == "SPACE")
                        tavernMenu = 7;
                }

                while (tavernMenu == 10) // Successful purchase
                {
                    TavernDisplayUpdate();
                    CyText(3, "Right away!");
                    UpdateDisplay();
                    var key = GetSingleKey();
                    if (key == "SPACE")
                    {
                        tavernMenu = 2;
                        DeductCoins(0, 0, foodCost);
                        plyr.hunger -= tavernFoods[foodNo].hungerRemoved;
                        if (plyr.hunger < 0)
                            plyr.hunger = 0;
                        plyr.digestion += (tavernFoods[foodNo].hungerRemoved) * 2;
                        plyr.food += tavernFoods[foodNo].foodPacketAdded;
                        eatDrinkDescriptions[descriptionPointer] = tavernFoods[foodNo].name;
                        if (descriptionPointer == 3)
                            descriptionPointer = 0;
                        else
                            descriptionPointer++;
                    }
                }

                while (tavernMenu == 11) // apply for job
                {
                    var jobNumber = tavernJobOpenings[tavernNo].jobNumber;

                    TavernDisplayUpdate();
                    if (jobNumber == 255)
                    {
                        BText(7, 0, "I'm sorry but there are no");
                        CyText(1, "job openings at the moment.");
                        CyText(9, "( Press a key )");
                        UpdateDisplay();

                        var key = GetSingleKey();
                        if (key != "")
                            tavernMenu = 1;
                    } else
                    {
                        CyText(0, $"We have an opening for a {tavernJobs[jobNumber].name}");
                        var str = $"for {tavernJobOpenings[tavernNo].JobHoursRequired} hours at {tavernJobOpenings[tavernNo].jobHourlyIncome} coppers per hour.";
                        CyText(1, str);
                        CyText(3, "Would you like to apply?");
                        CyText(5, "( es or  o)");
                        SetFontColor(40, 96, 244, 255);
                        CyText(5, " Y      N  ");
                        SetFontColor(215, 215, 215, 255);
                        UpdateDisplay();

                        var key = GetSingleKey();
                        if (key == "Y")
                            tavernMenu = 12;
                        if (key == "N")
                            tavernMenu = 1;
                    }
                }

                while (tavernMenu == 12) // Check job stat requirements
                {
                    var jobNumber = tavernJobOpenings[tavernNo].jobNumber;
                    var statRequirementName = tavernJobs[jobNumber].statRequirementName;
                    var statRequirement = tavernJobs[jobNumber].statRequirementValue;
                    var jobStatMet = false;

                    //TODO: Don't use magic strings
                    // Check stat requirement met
                    if ((statRequirementName == "Strength") && (statRequirement <= plyr.str))
                        jobStatMet = true;
                    if ((statRequirementName == "Charm") && (statRequirement <= plyr.chr))
                        jobStatMet = true;
                    if ((statRequirementName == "Skill") && (statRequirement <= plyr.skl))
                        jobStatMet = true;

                    if (!jobStatMet)
                    {
                        TavernDisplayUpdate();
                        str = $"You will need more {statRequirementName}";
                        CyText(0, str);
                        CyText(1, "to get the job.");
                        CyText(9, "( Press a key )");
                        UpdateDisplay();

                        var key = GetSingleKey();

                        if (key == "SPACE")
                            tavernMenu = 1;
                    } else
                    {
                        workingHours = tavernJobOpenings[tavernNo].JobHoursRequired;
                        hourlyRate = tavernJobOpenings[tavernNo].jobHourlyIncome;
                        jobIncome = workingHours * hourlyRate;
                        tavernMenu = 13;
                    }
                }

                while (tavernMenu == 13) // Display working message
                {
                    while (workingHours > 0)
                    {
                        TavernDisplayUpdate();
                        CyText(2, "WORKING");
                        UpdateDisplay();
                        Sleep(TimeSpan.FromSeconds(1));
                        for (var i = 0; i < 60; i++) // 60 minutes
                        {
                            //sf::sleep(0.01f);
                            // check for diseases
                            // modify fatigue
                            // modify hitpoints
                            // modify temporary magic bonuses
                        }
                        AddHour();

                        workingHours--;
                    }

                    TavernDisplayUpdate();

                    // CHECK FOR INJURY
                    CyText(2, "The job is completed.");
                    CyText(3, $"You have earned {jobIncome} coppers.");
                    CyText(9, "( Press a key )");
                    UpdateDisplay();
                    var key = GetSingleKey();
                    if (key == "SPACE")
                    {
                        plyr.copper += jobIncome;
                        tavernJobOpenings[tavernNo].jobNumber = 255;
                        tavernMenu = 1;
                    }
                }

                while (tavernMenu == 30) // Buy a round
                {
                    //MLT: Downcast to int
                    var roundCost = (int)(80 * Taverns[tavernNo].priceFactor);
                    TavernDisplayUpdate();
                    CyText(0, $"A round for the house will cost@@{roundCost} coppers.@@@Dost thou still wish to buy? (Y or N)");
                    UpdateDisplay();
                    var key = GetSingleKey();
                    if (key == "Y")
                    {
                        bool affordRound = CheckCoins(0, 0, roundCost);
                        if (affordRound)
                        {
                            DeductCoins(0, 0, roundCost);
                            plyr.tavernFriendships[tavernNo] += 1;
                            tavernMenu = 31;
                        } else
                        {
                            plyr.tavernFriendships[tavernNo] -= 1;
                            TavernMessage("I'm sorry you have not the funds.");
                            tavernMenu = 2;
                        }
                    }
                    if (key == "N")
                        tavernMenu = 2;
                }

                while (tavernMenu == 31) // Round successfully bought messages
                {
                    var tf = plyr.tavernFriendships[tavernNo];
                    var str = "The patrons go up to the bar.";
                    if ((tf >= 0) && (tf < 3))
                        str = "A few people take up your offer.";
                    if ((tf > 3) && (tf < 6))
                        str = "You have won yourself some friends, Adventurer.";
                    if ((tf > 5) && (tf < 7))
                        str = "All the patrons applaud your generosity!";
                    if (tf > 6)
                        str = $"A toast to our friend, {plyr.name}.";
                    TavernMessage(str);
                    tavernMenu = 2;
                }
            }
            if (musicPlaying)
                tavernMusic.Stop();
            LeaveShop();
        }

        public static void StockTavernDrinks ()
        {
            // Run each hour to randomly pick 10 items for sale at each of the 4 smithies
            // Check for duplicates using smithyWaresCheck array of bools

            // Set bools for duplicate items check to false            
            for (var x = 0; x < 14; x++)
            {
                for (var y = 0; y < 12; y++)
                    tavernDrinksCheck[x, y] = false;
            }

            for (var tavernNo = 0; tavernNo < 14; tavernNo++)
            {
                for (var waresNo = 0; waresNo < 6; waresNo++)
                {
                    // Current code may create duplicate items in each tavern
                    var uniqueItem = false;
                    while (!uniqueItem)
                    {
                        var itemNo = Random(0, 11); // was 12

                        if (!tavernDrinksCheck[tavernNo, itemNo])
                        {
                            tavernDailyDrinks[tavernNo, waresNo] = itemNo; // its not a duplicate
                            tavernDrinksCheck[tavernNo, itemNo] = true;
                            uniqueItem = true;
                        }
                    }
                }
            }
        }

        public static void StockTavernFoods ()
        {
            // Run each hour to randomly pick 10 items for sale at each of the 4 smithies
            // Check for duplicates using smithyWaresCheck array of bools

            // Set bools for duplicate items check to false
            for (var x = 0; x < 14; x++)
            {
                for (var y = 0; y < 12; y++)
                    tavernFoodsCheck[x, y] = false;
            }

            for (var tavernNo = 0; tavernNo < 14; tavernNo++)
            {
                for (var waresNo = 0; waresNo < 6; waresNo++)
                {
                    // Current code may create duplicate items in each tavern
                    var uniqueItem = false;
                    while (!uniqueItem)
                    {
                        var itemNo = Random(0, 31); // was 32

                        if (!tavernFoodsCheck[tavernNo, itemNo])
                        {
                            tavernDailyFoods[tavernNo, waresNo] = itemNo; // its not a duplicate
                            tavernFoodsCheck[tavernNo, itemNo] = true;
                            uniqueItem = true;
                        }
                    }
                }
            }
        }

        #region Review Data

        public static string closingText;
        public static int descriptionPointer = 0;
        public static int drinkChoice;
        public static int drinkCost;
        public static int drinkNo;

        public static string[] eatDrinkDescriptions = new string[4];
        public static int foodChoice;
        public static int foodCost;
        public static int foodNo;
        public static string lyricsFilename;
        public static string openingText;

        //TODO: Move to data files
        public static TavernDrinkItem[] tavernDrinks =
        {
            new TavernDrinkItem()
            { name = "Water Flask", basePrice = 5, alcoholAdded = 0, thirstRemoved = 0, waterFlaskAdded = 1 },
            new TavernDrinkItem()
            { name = "Milk", basePrice = 2, alcoholAdded = 0, thirstRemoved = 6, waterFlaskAdded = 0 },
            new TavernDrinkItem()
            { name = "Beer", basePrice = 2, alcoholAdded = 1, thirstRemoved = 2, waterFlaskAdded = 0 },
            new TavernDrinkItem()
            { name = "Ale", basePrice = 3, alcoholAdded = 1, thirstRemoved = 3, waterFlaskAdded = 0 },
            new TavernDrinkItem()
            { name = "Wine", basePrice = 4, alcoholAdded = 2, thirstRemoved = 2, waterFlaskAdded = 0 },
            new TavernDrinkItem()
            { name = "Grog", basePrice = 2, alcoholAdded = 2, thirstRemoved = 2, waterFlaskAdded = 0 },
            new TavernDrinkItem()
            { name = "Spirits", basePrice = 5, alcoholAdded = 2, thirstRemoved = 2, waterFlaskAdded = 0 },
            new TavernDrinkItem()
            { name = "Water", basePrice = 1, alcoholAdded = 0, thirstRemoved = 8, waterFlaskAdded = 0 },
            new TavernDrinkItem()
            { name = "Sarsaparila", basePrice = 3, alcoholAdded = 0, thirstRemoved = 5, waterFlaskAdded = 0 },
            new TavernDrinkItem()
            { name = "Orange Juice", basePrice = 3, alcoholAdded = 0, thirstRemoved = 5, waterFlaskAdded = 0 },
            new TavernDrinkItem()
            { name = "Grape Juice", basePrice = 3, alcoholAdded = 0, thirstRemoved = 5, waterFlaskAdded = 0 },
            new TavernDrinkItem()
            { name = "Mineral Water", basePrice = 2, alcoholAdded = 0, thirstRemoved = 8, waterFlaskAdded = 0 }
        };
        public static bool[,] tavernDrinksCheck = new bool[14, 12]; // markers used to check for duplicate items - 14 taverns, 12 potential drinks

        //TODO: Move to data files
        public static TavernFoodItem[] tavernFoods =
        {
            new TavernFoodItem()
            { name = "Rack of Lamb", basePrice = 40, hungerRemoved = 16, foodPacketAdded = 0 },
            new TavernFoodItem()
            { name = "Roast Beef", basePrice = 50, hungerRemoved = 18, foodPacketAdded = 0 },
            new TavernFoodItem()
            { name = "Roast Chicken", basePrice = 25, hungerRemoved = 16, foodPacketAdded = 0 },
            new TavernFoodItem()
            { name = "Roast Dragon", basePrice = 150, hungerRemoved = 22, foodPacketAdded = 1 },
            new TavernFoodItem()
            { name = "Pork Ribs", basePrice = 30, hungerRemoved = 12, foodPacketAdded = 0 },
            new TavernFoodItem()
            { name = "Leg of Lamb", basePrice = 80, hungerRemoved = 19, foodPacketAdded = 0 },
            new TavernFoodItem()
            { name = "Fried Chicken", basePrice = 50, hungerRemoved = 15, foodPacketAdded = 0 },
            new TavernFoodItem()
            { name = "Leg of Dragon", basePrice = 300, hungerRemoved = 22, foodPacketAdded = 2 },
            new TavernFoodItem()
            { name = "Ham", basePrice = 60, hungerRemoved = 14, foodPacketAdded = 0 },
            new TavernFoodItem()
            { name = "Lamb", basePrice = 56, hungerRemoved = 17, foodPacketAdded = 0 },
            new TavernFoodItem()
            { name = "Filet of Beef", basePrice = 70, hungerRemoved = 19, foodPacketAdded = 0 },
            new TavernFoodItem()
            { name = "Vegetable Soup", basePrice = 5, hungerRemoved = 8, foodPacketAdded = 0 },
            new TavernFoodItem()
            { name = "Ragout of Beef", basePrice = 33, hungerRemoved = 10, foodPacketAdded = 0 },
            new TavernFoodItem()
            { name = "Ragout of Dragon", basePrice = 50, hungerRemoved = 11, foodPacketAdded = 1 },
            new TavernFoodItem()
            { name = "Bowl of Fruit", basePrice = 25, hungerRemoved = 7, foodPacketAdded = 0 },
            new TavernFoodItem()
            { name = "Plate of Greens", basePrice = 18, hungerRemoved = 6, foodPacketAdded = 0 },
            new TavernFoodItem()
            { name = "Smoked Fish", basePrice = 30, hungerRemoved = 12, foodPacketAdded = 0 },
            new TavernFoodItem()
            { name = "Apple Pie", basePrice = 12, hungerRemoved = 4, foodPacketAdded = 0 },
            new TavernFoodItem()
            { name = "Lemon Pie", basePrice = 12, hungerRemoved = 4, foodPacketAdded = 0 },
            new TavernFoodItem()
            { name = "Chocolate Cake", basePrice = 10, hungerRemoved = 4, foodPacketAdded = 0 },
            new TavernFoodItem()
            { name = "Loaf of Bread", basePrice = 8, hungerRemoved = 6, foodPacketAdded = 0 },
            new TavernFoodItem()
            { name = "Block of Cheese", basePrice = 15, hungerRemoved = 9, foodPacketAdded = 0 },
            new TavernFoodItem()
            { name = "Food Packet", basePrice = 25, hungerRemoved = 0, foodPacketAdded = 1 },
            new TavernFoodItem()
            { name = "Gruel", basePrice = 4, hungerRemoved = 8, foodPacketAdded = 0 },
            new TavernFoodItem()
            { name = "Bagel", basePrice = 6, hungerRemoved = 5, foodPacketAdded = 0 },
            new TavernFoodItem()
            { name = "Pemmican", basePrice = 20, hungerRemoved = 4, foodPacketAdded = 1 },
            new TavernFoodItem()
            { name = "Bowl of Chili", basePrice = 10, hungerRemoved = 6, foodPacketAdded = 0 },
            new TavernFoodItem()
            { name = "Pasta", basePrice = 30, hungerRemoved = 10, foodPacketAdded = 0 },
            new TavernFoodItem()
            { name = "Lasagna", basePrice = 30, hungerRemoved = 10, foodPacketAdded = 0 },
            new TavernFoodItem()
            { name = "Crayfish", basePrice = 40, hungerRemoved = 9, foodPacketAdded = 0 },
            new TavernFoodItem()
            { name = "Lobster", basePrice = 100, hungerRemoved = 14, foodPacketAdded = 0 },
            new TavernFoodItem()
            { name = "Sandwich", basePrice = 10, hungerRemoved = 8, foodPacketAdded = 0 }
        };
        public static bool[,] tavernFoodsCheck = new bool[14, 32]; // markers used to check for duplicate items

        public static TavernJobOpening[] tavernJobOpenings = Arrays.InitializeWithDefaultInstances<TavernJobOpening>(14);

        //TODO: Move to data files
        public static TavernJob[] tavernJobs =
        {
            new TavernJob()
            {
                name = "Bouncer",
                minIncome = 40,
                maxIncome = 44,
                statRequirementName = "Strength",
                statRequirementValue = 22,
                fatigueRate = 0.75F,
                minorWoundProbability = 19.69F,
                majorWoundProbability = 7.26F
            },
            new TavernJob()
            {
                name = "Host",
                minIncome = 20,
                maxIncome = 24,
                statRequirementName = "Charm",
                statRequirementValue = 12,
                fatigueRate = 0.5625F,
                minorWoundProbability = 0.77F,
                majorWoundProbability = 0.01F
            },
            new TavernJob()
            {
                name = "Dish Washer",
                minIncome = 8,
                maxIncome = 12,
                statRequirementName = "Skill",
                statRequirementValue = 9,
                fatigueRate = 0.5625F,
                minorWoundProbability = 12.64F,
                majorWoundProbability = 2.20F
            }
        };

        public static Music tavernMusic;
        public static int tavernNo;

        //TODO: Move to data files
        //MLT: Double to float, int to boolean
        public static Tavern[] Taverns =
        {
            new Tavern()
            {
                name = "Flaming Dragon",
                priceFactor = 1.2F,
                location = 23,
                openingHour = 0,
                closingHour = 23,
                jobProbability = 51,
                membershipFee = 0,
                classy = false
            },
            new Tavern()
            {
                name = "Misty Mountain",
                priceFactor = 3.5F,
                location = 24,
                openingHour = 11,
                closingHour = 2,
                jobProbability = 51,
                membershipFee = 3000,
                classy = true
            },
            new Tavern()
            {
                name = "Screaming Siren Bar",
                priceFactor = 1.2F,
                location = 25,
                openingHour = 16,
                closingHour = 3,
                jobProbability = 64,
                membershipFee = 0,
                classy = false
            },
            new Tavern()
            {
                name = "Happy Hunter Rest Stop",
                priceFactor = 1.4F,
                location = 26,
                openingHour = 0,
                closingHour = 7,
                jobProbability = 77,
                membershipFee = 0,
                classy = false
            },
            new Tavern()
            {
                name = "Dancing Nymph",
                priceFactor = 1.6F,
                location = 27,
                openingHour = 16,
                closingHour = 7,
                jobProbability = 72,
                membershipFee = 0,
                classy = false
            },
            new Tavern()
            {
                name = "Club",
                priceFactor = 3.5F,
                location = 28,
                openingHour = 18,
                closingHour = 22,
                jobProbability = 182,
                membershipFee = 1500,
                classy = true
            },
            new Tavern()
            {
                name = "Black Devil",
                priceFactor = 1.4F,
                location = 29,
                openingHour = 0,
                closingHour = 5,
                jobProbability = 64,
                membershipFee = 0,
                classy = true
            },
            new Tavern()
            {
                name = "Lost Oasis",
                priceFactor = 0.8F,
                location = 30,
                openingHour = 0,
                closingHour = 23,
                jobProbability = 26,
                membershipFee = 0,
                classy = true
            },
            new Tavern()
            {
                name = "Last Stop",
                priceFactor = 1.2F,
                location = 31,
                openingHour = 0,
                closingHour = 23,
                jobProbability = 38,
                membershipFee = 0,
                classy = false
            },
            new Tavern()
            {
                name = "Tail of the Dog",
                priceFactor = 2.0F,
                location = 32,
                openingHour = 0,
                closingHour = 23,
                jobProbability = 192,
                membershipFee = 0,
                classy = false
            },
            new Tavern()
            {
                name = "Club Babylon",
                priceFactor = 5.0F,
                location = 33,
                openingHour = 10,
                closingHour = 4,
                jobProbability = 77,
                membershipFee = 50000,
                classy = true
            },
            new Tavern()
            {
                name = "Lost Tears",
                priceFactor = 1.8F,
                location = 34,
                openingHour = 0,
                closingHour = 23,
                jobProbability = 56,
                membershipFee = 0,
                classy = true
            },
            new Tavern()
            {
                name = "Mom's Bar",
                priceFactor = 1.6F,
                location = 35,
                openingHour = 0,
                closingHour = 23,
                jobProbability = 128,
                membershipFee = 0,
                classy = false
            },
            new Tavern()
            {
                name = "Lusty Lloyds",
                priceFactor = 1.0F,
                location = 36,
                openingHour = 0,
                closingHour = 23,
                jobProbability = 46,
                membershipFee = 0,
                classy = false
            }
        };
        #endregion

        #region Private Members

        //TODO: Return the Tavern itself
        private static int GetTavernNo ()
        {
            var tavern_no = 0;
            for (var i = 0; i < Taverns.Length; i++)
            {
                if (Taverns[i].location == plyr.location)
                    tavern_no = i; // The number of the tavern you have entered
            }
            return tavern_no;
        }

        private static void TavernDisplayUpdate ()
        {
            clock1.Restart();
            ClearShopDisplay();
            UpdateLyrics();
            iCounter++;
        }

        private static void TavernMessage ( string txt )
        {
            string key;
            do
            {
                ClearShopDisplay();
                CText(txt);
                UpdateDisplay();
                key = GetSingleKey();
            } while (key != "SPACE");
        }
        #endregion
    }
}