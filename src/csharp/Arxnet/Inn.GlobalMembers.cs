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
        public static InnJobOpening[] innJobOpenings = Arrays.InitializeWithDefaultInstances<InnJobOpening>(7);

        //MLT: Double to float
        public static InnJob[] innJobs =
        {
            new InnJob()
            {
                name = "Bellhop",
                minIncome = 10,
                maxIncome = 24,
                statRequirementName = "Strength",
                statRequirementValue = 18,
                fatigueRate = 0.6875F,
                minorWoundProbability = 15.95F,
                majorWoundProbability = 3.97F
            },
            new InnJob()
            {
                name = "Desk Clerk",
                minIncome = 18,
                maxIncome = 20,
                statRequirementName = "Charm",
                statRequirementValue = 16,
                fatigueRate = 0.5625F,
                minorWoundProbability = 3.75F,
                majorWoundProbability = 0.15F
            },
            new InnJob()
            {
                name = "Janitor",
                minIncome = 12,
                maxIncome = 15,
                statRequirementName = "Stamina",
                statRequirementValue = 17,
                fatigueRate = 0.6250F,
                minorWoundProbability = 8.50F,
                majorWoundProbability = 0.88F
            }
        };

        public static Inn[] Inns =
        {
            new Inn()
            { name = "Green Boar Inn", costMultiplier = 1, jobProbability = 64, x = 9, y = 44 },
            new Inn()
            { name = "Lazy Griffin Inn", costMultiplier = 2, jobProbability = 154, x = 33, y = 11 },
            new Inn()
            { name = "Sleeping Dragon Inn", costMultiplier = 0.5F, jobProbability = 64, x = 60, y = 57 },
            new Inn()
            { name = "Traveller's Inn", costMultiplier = 6, jobProbability = 179, x = 32, y = 39 },
            new Inn()
            { name = "Midnight Inn", costMultiplier = 3, jobProbability = 56, x = 32, y = 40 },
            new Inn()
            { name = "Warrior's Retreat", costMultiplier = 0.75F, jobProbability = 77, x = 28, y = 9 },
            new Inn()
            { name = "Royal Resort Inn", costMultiplier = 8, jobProbability = 102, x = 31, y = 60 }
        };

        public static InnRoom[] Rooms =
        {
            new InnRoom()
            { name = "the common area floor", baseCost = 10, fatigueRecoveryProbability = 30 },
            new InnRoom()
            { name = "a bed with no bath", baseCost = 20, fatigueRecoveryProbability = 45 },
            new InnRoom()
            { name = "a bed with common bath", baseCost = 30, fatigueRecoveryProbability = 60 },
            new InnRoom()
            { name = "a room with common bath", baseCost = 40, fatigueRecoveryProbability = 85 },
            new InnRoom()
            { name = "a room with bath", baseCost = 50, fatigueRecoveryProbability = 100 },
            new InnRoom()
            { name = "a Premium Room", baseCost = 100, fatigueRecoveryProbability = 140 },
            new InnRoom()
            { name = "a Deluxe Room", baseCost = 200, fatigueRecoveryProbability = 170 },
            new InnRoom()
            { name = "a Small Suite", baseCost = 400, fatigueRecoveryProbability = 200 },
            new InnRoom()
            { name = "a Suite", baseCost = 800, fatigueRecoveryProbability = 240 },
            new InnRoom()
            { name = "our BEST Suite", baseCost = 1600, fatigueRecoveryProbability = 255 }
        };

        public static void CheckDailyInnJobOpenings ()
        {
            // Run at the start of each new day
            var jobOpeningProbability = 0;
            for (var i = 0; i < 7; i++)
            {
                jobOpeningProbability = Random(0, 255);
                if (jobOpeningProbability <= Inns[i].jobProbability)
                {
                    // Create a new job entry for the day
                    int newJobNumber = Random(0, 2);
                    innJobOpenings[i].jobNumber = newJobNumber;
                    innJobOpenings[i].JobHoursRequired = Random(0, 5) + 3;
                    innJobOpenings[i].jobHourlyIncome = Random(innJobs[newJobNumber].minIncome,
                                                              innJobs[newJobNumber].maxIncome);
                } else
                {
                    // No job available today
                    innJobOpenings[i].jobNumber = 255;
                }
            }
        }

        public static int GetInnNo ()
        {
            var Inn_no = 0;
            for (var i = 0; i <= 6; i++) // Max number of Inn objects
            {
                if ((Inns[i].x == plyr.x) && (Inns[i].y == plyr.y))
                    Inn_no = i; // The number of the Inn you have entered
            }
            if ((plyr.x == 31) && (plyr.y == 38))
                Inn_no = 3;
            return Inn_no;
        }

        public static void ShopInn ()
        {
            var InnNo = GetInnNo();
            if (InnNo == 0)
            {
                SetAutoMapFlag(plyr.map, 8, 44);
                SetAutoMapFlag(plyr.map, 9, 44);
                SetAutoMapFlag(plyr.map, 10, 44);
                SetAutoMapFlag(plyr.map, 8, 45);
            }
            if (InnNo == 3)
            {
                SetAutoMapFlag(plyr.map, 31, 38);
                SetAutoMapFlag(plyr.map, 32, 39);
                SetAutoMapFlag(plyr.map, 31, 39);
            }
            if (InnNo == 4)
            {
                SetAutoMapFlag(plyr.map, 31, 40);
                SetAutoMapFlag(plyr.map, 32, 40);
                SetAutoMapFlag(plyr.map, 33, 40);
                SetAutoMapFlag(plyr.map, 31, 41);
            }

            var InnMenu = 1; // high level menu
            var roomChoice = 0;
            var sleepingHours = 1;
            var workingHours = 0;
            var jobIncome = 0;

            LoadShopImage(11);

            plyr.status = GameStates.Module; // shopping

            while (InnMenu > 0)
            {
                while (InnMenu == 1) // main menu
                {
                    ClearShopDisplay();
                    BText(6, 0, "You are at the Inn's counter");
                    BText(10, 2, "Do you wish to");
                    BText(12, 4, " ) Stay the night");
                    BText(12, 5, " ) Check the time");
                    BText(12, 6, " ) Apply for a job");
                    BText(12, 7, " ) Leave");
                    DisplayCoins();
                    SetFontColour(40, 96, 244, 255);
                    BText(12, 4, "1");
                    BText(12, 5, "2");
                    BText(12, 6, "3");
                    BText(12, 7, "0");
                    SetFontColour(215, 215, 215, 255);
                    UpdateDisplay();

                    var key = GetSingleKey();

                    if (key == "1")
                        InnMenu = 2;
                    if (key == "2")
                        InnMenu = 7;
                    if (key == "3")
                        InnMenu = 8;
                    if (key == "0")
                        InnMenu = 0;
                    if (key == "down")
                        InnMenu = 0;
                }

                while (InnMenu == 2) // first room booking menu
                {
                    ClearShopDisplay();
                    BText(8, 0, "Would you like to sleep in");
                    BText(1, 2, " ) the common area floor         coppers");
                    BText(1, 3, " ) a Bed with no bath            coppers");
                    BText(1, 4, " ) a Bed with common Bath        coppers");
                    BText(1, 5, " ) a Room, with common Bath      coppers");
                    BText(1, 6, " ) a Room with Bath              coppers");
                    BText(1, 7, " ) Something better");
                    DisplayCoins();

                    for (var i = 0; i < 5; i++) // Max number of room prices in this menu display
                    {
                        var x = 30;

                        //MLT: Downcast to int
                        var room_cost = (int)(Rooms[i].baseCost * Inns[InnNo].costMultiplier);
                        if (room_cost < 100)
                            x = 31;
                        if (room_cost > 999)
                            x = 29;
                        BText(x, (i + 2), room_cost);
                    }

                    SetFontColour(40, 96, 244, 255);
                    BText(1, 2, "1");
                    BText(1, 3, "2");
                    BText(1, 4, "3");
                    BText(1, 5, "4");
                    BText(1, 6, "5");
                    BText(1, 7, "6");
                    SetFontColour(215, 215, 215, 255);

                    UpdateDisplay();

                    var key = GetSingleKey();
                    if (key == "1")
                    {
                        roomChoice = 0;
                        InnMenu = 4;
                    }
                    if (key == "2")
                    {
                        roomChoice = 1;
                        InnMenu = 4;
                    }
                    if (key == "3")
                    {
                        roomChoice = 2;
                        InnMenu = 4;
                    }
                    if (key == "4")
                    {
                        roomChoice = 3;
                        InnMenu = 4;
                    }
                    if (key == "5")
                    {
                        roomChoice = 4;
                        InnMenu = 4;
                    }
                    if (key == "6")
                        InnMenu = 3;
                    if (key == "0")
                        InnMenu = 0;
                }

                while (InnMenu == 3) // Join Inn menu
                {
                    ClearShopDisplay();
                    BText(8, 0, "Would you like to sleep in");
                    BText(1, 2, " ) a Premium Room");
                    BText(1, 3, " ) a Deluxe Room");
                    BText(1, 4, " ) a Small Suite");
                    BText(1, 5, " ) a Suite");
                    BText(1, 6, " ) Our BEST Suite");
                    BText(1, 7, " ) Something cheaper");
                    DisplayCoins();

                    SetFontColour(40, 96, 244, 255);
                    BText(1, 2, "1");
                    BText(1, 3, "2");
                    BText(1, 4, "3");
                    BText(1, 5, "4");
                    BText(1, 6, "5");
                    BText(1, 7, "6");
                    SetFontColour(215, 215, 215, 255);

                    for (var i = 5; i < 10; i++) // Max number of room prices in this menu display
                    {
                        var x = 28;
                        //MLT: Downcast to int
                        var room_cost = (int)(Rooms[i].baseCost * Inns[InnNo].costMultiplier);
                        if (room_cost < 1000)
                            x = 30;
                        BText(x, (i - 3), ToCurrency(room_cost));
                        BText(34, (i - 3), "coppers");
                    }

                    UpdateDisplay();

                    var key = GetSingleKey();
                    if (key == "6")
                        InnMenu = 2;
                    if (key == "1")
                    {
                        roomChoice = 5;
                        InnMenu = 4;
                    }
                    if (key == "2")
                    {
                        roomChoice = 6;
                        InnMenu = 4;
                    }
                    if (key == "3")
                    {
                        roomChoice = 7;
                        InnMenu = 4;
                    }
                    if (key == "4")
                    {
                        roomChoice = 8;
                        InnMenu = 4;
                    }
                    if (key == "5")
                    {
                        roomChoice = 9;
                        InnMenu = 4;
                    }
                    if (key == "0")
                        InnMenu = 0;
                }

                while (InnMenu == 4) // Confirm room choice and rate
                {
                    ClearShopDisplay();
                    CyText(0, "Our rate for sleeping in");

                    //MLT: Downcast to int
                    var str = $"{Rooms[roomChoice].name} is {ToCurrency((int)(Rooms[roomChoice].baseCost * Inns[InnNo].costMultiplier))} coppers.";
                    CyText(1, str);
                    CyText(3, "Do you wish to sign in?");
                    CyText(5, "( es or  o)");
                    DisplayCoins();
                    SetFontColour(40, 96, 244, 255);
                    CyText(5, " Y      N  ");
                    SetFontColour(215, 215, 215, 255);

                    UpdateDisplay();

                    var key = GetSingleKey();
                    if (key == "Y")
                        InnMenu = 5;
                    if (key == "N")
                        InnMenu = 1;
                }

                var roomCost = 0;
                while (InnMenu == 5) // Check funds and display sleeping time choices
                {
                    ClearShopDisplay();

                    //MLT: Downcast to int
                    roomCost = (int)(Rooms[roomChoice].baseCost * Inns[InnNo].costMultiplier);
                    if (!CheckCoins(0, 0, roomCost))
                    {
                        CText("I'm sorry, you have not the funds.");
                        CyText(9, "( Press a key )");
                        UpdateDisplay();
                        var key = GetSingleKey();
                        if (key != "")
                            InnMenu = 1;
                    } else
                    {
                        CyText(0, "How many hours from now do you wish");
                        CyText(1, "your wake-up call to be set for");
                        CyText(2, "(Maximum of 12 hours)?");
                        BText(3, 4, " ) 1 hour    ) 2 hours   ) 3 hours");
                        BText(3, 5, " ) 4 hours   ) 5 hours   ) 6 hours");
                        BText(3, 6, " ) 7 hours   ) 8 hours   ) 9 hours");
                        BText(3, 7, " ) 10 hours  ) 11 hours  ) 12 hours");
                        SetFontColour(40, 96, 244, 255);
                        BText(3, 4, "1           2           3");
                        BText(3, 5, "4           5           6");
                        BText(3, 6, "7           8           9");
                        BText(3, 7, "A           B           C");
                        SetFontColour(215, 215, 215, 255);
                        UpdateDisplay();
                        var key = GetSingleKey();

                        if (key == "1")
                        {
                            sleepingHours = 1;
                            InnMenu = 6;
                        }
                        if (key == "2")
                        {
                            sleepingHours = 2;
                            InnMenu = 6;
                        }
                        if (key == "3")
                        {
                            sleepingHours = 3;
                            InnMenu = 6;
                        }
                        if (key == "4")
                        {
                            sleepingHours = 4;
                            InnMenu = 6;
                        }
                        if (key == "5")
                        {
                            sleepingHours = 5;
                            InnMenu = 6;
                        }
                        if (key == "6")
                        {
                            sleepingHours = 6;
                            InnMenu = 6;
                        }
                        if (key == "7")
                        {
                            sleepingHours = 7;
                            InnMenu = 6;
                        }
                        if (key == "8")
                        {
                            sleepingHours = 8;
                            InnMenu = 6;
                        }
                        if (key == "9")
                        {
                            sleepingHours = 9;
                            InnMenu = 6;
                        }
                        if (key == "A")
                        {
                            sleepingHours = 10;
                            InnMenu = 6;
                        }
                        if (key == "B")
                        {
                            sleepingHours = 11;
                            InnMenu = 6;
                        }
                        if (key == "C")
                        {
                            sleepingHours = 12;
                            InnMenu = 6;
                        }
                    }
                }

                while (InnMenu == 6) // Display sleeping message and wake up message
                {
                    while (sleepingHours > 0)
                    {
                        ClearShopDisplay();
                        CyText(2, "You are sleeping in");
                        var str = $"{Rooms[roomChoice].name}.";
                        CyText(3, str);
                        UpdateDisplay();
                        Sleep(TimeSpan.FromSeconds(1));
                        // check for diseases
                        // modify fatigue

                        // sleepProbabability for each room determines whether 6 hp per hour will be recovered
                        var roomProb = 0;
                        if (roomChoice == 0)
                            roomProb = 30;
                        if (roomChoice == 1)
                            roomProb = 45;
                        if (roomChoice == 2)
                            roomProb = 60;
                        if (roomChoice == 3)
                            roomProb = 85;
                        if (roomChoice == 4)
                            roomProb = 100;
                        if (roomChoice == 5)
                            roomProb = 140;
                        if (roomChoice == 6)
                            roomProb = 170;
                        if (roomChoice == 7)
                            roomProb = 200;
                        if (roomChoice == 8)
                            roomProb = 240;
                        if (roomChoice == 9)
                            roomProb = 255;
                        var actualSleepProb = Random(0, 255);
                        if (actualSleepProb <= roomProb)
                        {
                            plyr.hp = plyr.hp + 6;
                            if (plyr.hp > plyr.maxhp)
                                plyr.hp = plyr.maxhp;
                        }

                        AddHour();
                        sleepingHours--;
                    }

                    ClearShopDisplay();
                    CyText(2, "Brave adventurer...");
                    CyText(3, "It is time to wake up.");
                    CyText(4, "I hope you rested well.");
                    CyText(9, "( Press a key )");
                    UpdateDisplay();
                    var key = GetSingleKey();
                    if (key != "")
                    {
                        DeductCoins(0, 0, roomCost);
                        InnMenu = 1;
                    }
                }

                while (InnMenu == 7) // Check time menu
                {
                    ClearShopDisplay();
                    var str = $"Hour {plyr.hours} of day {plyr.days}";
                    BText(7, 2, str);

                    var monthDesc = "";
                    switch (plyr.months)
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
                    str = $"In the month of {monthDesc}";
                    BText(7, 4, str);
                    str = $"In year {plyr.years} since abduction";
                    BText(7, 6, str);
                    CyText(9, "( Press a key )");
                    UpdateDisplay();

                    var key = GetSingleKey();
                    if (key != "")
                        InnMenu = 1;
                }

                while (InnMenu == 8) // apply for job
                {
                    var jobNumber = innJobOpenings[InnNo].jobNumber;

                    ClearShopDisplay();
                    if (jobNumber == 255)
                    {
                        BText(7, 0, "I'm sorry but there are no");
                        CyText(1, "job openings at the moment.");
                        CyText(9, "( Press a key )");
                        UpdateDisplay();

                        var key = GetSingleKey();
                        if (key != "")
                            InnMenu = 1;
                    } else
                    {
                        CyText(0, $"We have an opening for a {innJobs[jobNumber].name}");
                        CyText(1, $"for {innJobOpenings[InnNo].JobHoursRequired} hours at {innJobOpenings[InnNo].jobHourlyIncome} coppers per hour.");
                        CyText(3, "Would you like to apply?");
                        CyText(5, "( es or  o)");
                        SetFontColour(40, 96, 244, 255);
                        CyText(5, " Y      N  ");
                        SetFontColour(215, 215, 215, 255);
                        UpdateDisplay();

                        var key = GetSingleKey();
                        if (key == "Y")
                            InnMenu = 9;
                        if (key == "N")
                            InnMenu = 1;
                    }
                }

                while (InnMenu == 9) // Check job stat requirements
                {
                    var jobNumber = innJobOpenings[InnNo].jobNumber;
                    var statRequirementName = innJobs[jobNumber].statRequirementName;
                    var statRequirement = innJobs[jobNumber].statRequirementValue;
                    var jobStatMet = false;

                    // Check stat requirement met
                    if ((statRequirementName == "Strength") && (statRequirement <= plyr.str))
                        jobStatMet = true;
                    if ((statRequirementName == "Charm") && (statRequirement <= plyr.chr))
                        jobStatMet = true;
                    if ((statRequirementName == "Stamina") && (statRequirement <= plyr.sta))
                        jobStatMet = true;

                    if (!jobStatMet)
                    {
                        ClearShopDisplay();
                        str = $"You will need more {statRequirementName}";
                        CyText(0, str);
                        CyText(1, "to get the job.");
                        CyText(9, "( Press a key )");
                        UpdateDisplay();

                        var key = GetSingleKey();

                        if (key != "")
                            InnMenu = 1;
                    } else
                    {
                        workingHours = innJobOpenings[InnNo].JobHoursRequired;
                        var hourlyRate = innJobOpenings[InnNo].jobHourlyIncome;
                        jobIncome = workingHours * hourlyRate;
                        InnMenu = 10;
                    }
                }

                while (InnMenu == 10) // Display sleeping message and wake up message
                {
                    while (workingHours > 0)
                    {
                        ClearShopDisplay();
                        CyText(2, "WORKING");
                        UpdateDisplay();
                        Sleep(TimeSpan.FromSeconds(1));
                        for (var i = 0; i < 60; i++) // 60 minutes
                        {
                            // check for diseases
                            // modify fatigue
                            // modify hitpoints
                            // modify temporary magic bonuses
                        }
                        AddHour();
                        workingHours--;
                    }

                    ClearShopDisplay();
                    // CHECK FOR INJURY
                    CyText(2, "The job is completed.");
                    CyText(3, $"You have earned {jobIncome} coppers.");
                    CyText(9, "( Press a key )");
                    UpdateDisplay();
                    var key = GetSingleKey();
                    if (key != "")
                    {
                        plyr.copper += jobIncome;
                        innJobOpenings[InnNo].jobNumber = 255;
                        InnMenu = 1;
                    }
                }
            }
            LeaveShop();
        }
    }
}