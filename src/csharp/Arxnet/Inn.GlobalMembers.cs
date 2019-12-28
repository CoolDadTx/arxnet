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
        public static void CheckDailyInnJobOpenings ()
        {
            // Run at the start of each new day
            for (var i = 0; i < 7; i++)
            {
                var jobOpeningProbability = Random(0, 255);
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

        public static void ShopInn ()
        {
            var InnNo = GetInnNo();
            switch (InnNo)
            {
                case 0:
                {
                    SetAutoMapFlag(plyr.map, 8, 44);
                    SetAutoMapFlag(plyr.map, 9, 44);
                    SetAutoMapFlag(plyr.map, 10, 44);
                    SetAutoMapFlag(plyr.map, 8, 45);
                    break;
                }

                case 3:
                {
                    SetAutoMapFlag(plyr.map, 31, 38);
                    SetAutoMapFlag(plyr.map, 32, 39);
                    SetAutoMapFlag(plyr.map, 31, 39);
                    break;
                }
                case 4:
                {
                    SetAutoMapFlag(plyr.map, 31, 40);
                    SetAutoMapFlag(plyr.map, 32, 40);
                    SetAutoMapFlag(plyr.map, 33, 40);
                    SetAutoMapFlag(plyr.map, 31, 41);
                    break;
                }
            };               

            var InnMenu = 1; // high level menu
            var roomChoice = 0;
            var sleepingHours = 1;
            var workingHours = 0;
            var jobIncome = 0;

            LoadShopImage(11);

            plyr.status = GameStates.Module; // shopping

            //TODO: Convert to actual menu code
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
                    SetFontColor(40, 96, 244, 255);
                    BText(12, 4, "1");
                    BText(12, 5, "2");
                    BText(12, 6, "3");
                    BText(12, 7, "0");
                    SetFontColor(215, 215, 215, 255);
                    UpdateDisplay();

                    switch (GetSingleKey())
                    {
                        case "1": InnMenu = 2; break;
                        case "2": InnMenu = 7; break;
                        case "3": InnMenu = 8; break;
                        case "0": 
                        case "down": InnMenu = 0; break;
                    }
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

                    SetFontColor(40, 96, 244, 255);
                    BText(1, 2, "1");
                    BText(1, 3, "2");
                    BText(1, 4, "3");
                    BText(1, 5, "4");
                    BText(1, 6, "5");
                    BText(1, 7, "6");
                    SetFontColor(215, 215, 215, 255);

                    UpdateDisplay();

                    InnMenu = 4;  //Moved outside switch so may behave differently
                    switch (GetSingleKey())
                    {
                        case "1": roomChoice = 0; break;
                        case "2": roomChoice = 1; break;
                        case "3": roomChoice = 2; break; //Original code did not set InnMenu = 4
                        case "4": roomChoice = 3; break;
                        case "5": roomChoice = 4; break;
                        case "6": InnMenu = 3; break;
                        case "0": InnMenu = 0; break;
                    }
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

                    SetFontColor(40, 96, 244, 255);
                    BText(1, 2, "1");
                    BText(1, 3, "2");
                    BText(1, 4, "3");
                    BText(1, 5, "4");
                    BText(1, 6, "5");
                    BText(1, 7, "6");
                    SetFontColor(215, 215, 215, 255);

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

                    InnMenu = 4;
                    switch(GetSingleKey())
                    {
                        case "1": roomChoice = 5; break;
                        case "2": roomChoice = 6; break;
                        case "3": roomChoice = 7; break;
                        case "4": roomChoice = 8; break;
                        case "5": roomChoice = 9; break;
                        case "6": InnMenu = 2; break;
                        case "0": InnMenu = 0; break;                        
                    };
                }

                while (InnMenu == 4) // Confirm room choice and rate
                {
                    ClearShopDisplay();
                    CyText(0, "Our rate for sleeping in");

                    //MLT: Downcast to int
                    CyText(1, $"{Rooms[roomChoice].name} is {ToCurrency((int)(Rooms[roomChoice].baseCost * Inns[InnNo].costMultiplier))} coppers.");
                    CyText(3, "Do you wish to sign in?");
                    CyText(5, "( es or  o)");
                    DisplayCoins();
                    SetFontColor(40, 96, 244, 255);
                    CyText(5, " Y      N  ");
                    SetFontColor(215, 215, 215, 255);

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
                        if (GetSingleKey() != "")
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
                        SetFontColor(40, 96, 244, 255);
                        BText(3, 4, "1           2           3");
                        BText(3, 5, "4           5           6");
                        BText(3, 6, "7           8           9");
                        BText(3, 7, "A           B           C");
                        SetFontColor(215, 215, 215, 255);
                        UpdateDisplay();

                        InnMenu = 6;
                        switch (GetSingleKey())
                        {
                            case "1": sleepingHours = 1; break;
                            case "2": sleepingHours = 2; break;
                            case "3": sleepingHours = 3; break;
                            case "4": sleepingHours = 4; break;
                            case "5": sleepingHours = 5; break;
                            case "6": sleepingHours = 6; break;
                            case "7": sleepingHours = 7; break;
                            case "8": sleepingHours = 8; break;
                            case "9": sleepingHours = 9; break;
                            case "A": sleepingHours = 10; break;
                            case "B": sleepingHours = 11; break;
                            case "C": sleepingHours = 12; break;                            
                        }
                    }
                }

                while (InnMenu == 6) // Display sleeping message and wake up message
                {
                    while (sleepingHours > 0)
                    {
                        ClearShopDisplay();
                        CyText(2, "You are sleeping in");
                        CyText(3, $"{Rooms[roomChoice].name}.");
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
                            plyr.hp += 6;
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
                    if (GetSingleKey() != "")
                    {
                        DeductCoins(0, 0, roomCost);
                        InnMenu = 1;
                    }
                }

                while (InnMenu == 7) // Check time menu
                {
                    ClearShopDisplay();
                    BText(7, 2, $"Hour {plyr.hours} of day {plyr.days}");

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
                    BText(7, 4, $"In the month of {monthDesc}");
                    BText(7, 6, $"In year {plyr.years} since abduction");
                    CyText(9, "( Press a key )");
                    UpdateDisplay();

                    if (GetSingleKey() != "")
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

                        if (GetSingleKey() != "")
                            InnMenu = 1;
                    } else
                    {
                        CyText(0, $"We have an opening for a {innJobs[jobNumber].name}");
                        CyText(1, $"for {innJobOpenings[InnNo].JobHoursRequired} hours at {innJobOpenings[InnNo].jobHourlyIncome} coppers per hour.");
                        CyText(3, "Would you like to apply?");
                        CyText(5, "( es or  o)");
                        SetFontColor(40, 96, 244, 255);
                        CyText(5, " Y      N  ");
                        SetFontColor(215, 215, 215, 255);
                        UpdateDisplay();

                        switch (GetSingleKey())
                        {
                            case "Y": InnMenu = 9; break;
                            case "N": InnMenu = 1; break;
                        };
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

                        if (GetSingleKey() != "")
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
                    if (GetSingleKey() != "")
                    {
                        plyr.copper += jobIncome;
                        innJobOpenings[InnNo].jobNumber = 255;
                        InnMenu = 1;
                    }
                }
            }
            LeaveShop();
        }        
        
        #region Private Members

        //TODO: Return Inn
        private static int GetInnNo ()
        {
            var inn = 0;
            for (var i = 0; i <= Inns.Length; i++) // Max number of Inn objects
            {
                if (Inns[i].Position == plyr.Position)
                    inn = i; // The number of the Inn you have entered
            }
            if ((plyr.Position.X == 31) && (plyr.Position.Y == 38))
                inn = 3;

            return inn;
        }

        private static InnJobOpening[] innJobOpenings = Arrays.InitializeWithDefaultInstances<InnJobOpening>(7);

        //TODO: Load as data for map
        //MLT: Double to float        
        private static InnJob[] innJobs =
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

        //TODO: Load as data for map
        private static Inn[] Inns =
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

        //TODO: Load as data for map
        private static InnRoom[] Rooms =
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
        #endregion
    }
}