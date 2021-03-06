﻿/*
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
    public static partial class GlobalMembers
    {
        public static void CheckDailybankJobOpenings ()
        {
            // Run at the start of each new day
            for (var i = 0; i < bankJobOpenings.Length; i++) 
            {
                var jobOpeningProbability = Random(0, 255);
                if (jobOpeningProbability <= banks[i].jobProbability)
                {
                    // Create a new job entry for the day
                    var newJobNumber = Random(0, 2);
                    bankJobOpenings[i].jobNumber = newJobNumber;
                    bankJobOpenings[i].jobHoursRequired = Random(0, 5) + 3;
                    bankJobOpenings[i].jobHourlyIncome = Random(bankJobs[newJobNumber].minIncome, bankJobs[newJobNumber].maxIncome);
                } else
                {
                    // No job available today
                    bankJobOpenings[i].jobNumber = 255; // 255 for none
                }
            }
        }

        public static void ShopBank ()
        {
            int workingHours = 0;
            int hourlyRate;
            int jobIncome = 0;
            bankNo = GetBankNo();
            if (bankNo == 0)
                accountRef = 0;
            if (bankNo == 1)
                accountRef = 3;
            if (bankNo == 2)
                accountRef = 6;

            var bankMenu = 1; // high level menu
            
            plyr.status = GameStates.Module; // shopping

            LoadShopImage(13);

            while (bankMenu > 0)
            {

                //TODO: Add check for bank opening and closing hours
                while (bankMenu == 1) // main menu
                {
                    ClearShopDisplay();

                    CyText(0, "How may I be of service?");
                    BText(2, 2, "1) Open, or 2) Close an account");
                    BText(2, 3, "3) Make a deposit or 4) Withdrawl");
                    BText(2, 4, "5) Look at your balances");
                    BText(2, 5, "6) Examine Bank's books");
                    BText(2, 6, "7) Sell Gems or Jewelry");
                    BText(2, 7, "8) Apply for a job");
                    BText(2, 8, "0) Leave");

                    SetFontColor(40, 96, 244, 255);
                    BText(2, 2, "1");
                    BText(14, 2, "2");
                    BText(2, 3, "3");
                    BText(23, 3, "4");
                    BText(2, 4, "5");
                    BText(2, 5, "6");
                    BText(2, 6, "7");
                    BText(2, 7, "8");
                    BText(2, 8, "0");
                    SetFontColor(215, 215, 215, 255);

                    DisplayCoins();
                    UpdateDisplay();

                    var key = GetSingleKey();

                    if (key == "0")
                        bankMenu = 0;
                    if (key == "1")
                        bankMenu = 2;
                    if (key == "2")
                        bankMenu = 8;
                    if (key == "3")
                        bankMenu = 16;
                    if (key == "4")
                        bankMenu = 19;
                    if (key == "5")
                        bankMenu = 15;
                    if (key == "6")
                        bankMenu = 14;
                    if (key == "7")
                        bankMenu = 21;
                    if (key == "down")
                        bankMenu = 0;
                    if (key == "8")
                        bankMenu = 11;
                }

                while (bankMenu == 2) // Open an account & make an initial deposit
                {
                    ClearShopDisplay();

                    CyText(0, "What account would you like to open?");

                    var str = "(1) Low Risk Account    ";
                    if (plyr.bankAccountStatuses[accountRef] == 1)
                        str = "(1) Low Risk Account    : Already open";
                    BText(2, 2, str);
                    str = "(2) Medium Risk Account";
                    if (plyr.bankAccountStatuses[accountRef + 1] == 1)
                        str = "(2) Medium Risk Account : Already open";
                    BText(2, 3, str);
                    str = "(3) High Risk Account";
                    if (plyr.bankAccountStatuses[accountRef + 2] == 1)
                        str = "(3) High Risk Account   : Already open";
                    BText(2, 4, str);
                    BText(2, 6, "(0) Other banking options");

                    UpdateDisplay();

                    key = GetSingleKey();
                    if (key == "1")
                    {
                        accountType = 0;
                        bankMenu = 3;
                        if (plyr.bankAccountStatuses[accountRef] == 1)
                            bankMenu = 4;
                    }
                    if (key == "2")
                    {
                        accountType = 1;
                        bankMenu = 3;
                        if (plyr.bankAccountStatuses[accountRef + 1] == 1)
                            bankMenu = 4;
                    }
                    if (key == "3")
                    {
                        accountType = 2;
                        bankMenu = 3;
                        if (plyr.bankAccountStatuses[accountRef + 2] == 1)
                            bankMenu = 4;
                    }
                    if (key == "0")
                        bankMenu = 1;
                }

                while (bankMenu == 3) // Open an account - display interest and failure rates
                {
                    ClearShopDisplay();

                    var str = "";
                    if (accountType == 0)
                        str = "Bank Policy - Low Risk Account";
                    if (accountType == 1)
                        str = "Bank Policy - Medium Risk Account";
                    if (accountType == 2)
                        str = "Bank Policy - High Risk Account";
                    BText(2, 0, str);

                    str = $"1. Average interest {banks[bankNo].accounts[accountType].interest}% per day.";
                    BText(2, 3, str);
                    str = "2. Has a mean failure rate of";
                    BText(2, 5, str);
                    str = $"approximately {banks[bankNo].accounts[accountType].failProb}% per day.";
                    BText(5, 6, str);

                    UpdateDisplay();

                    var key = GetSingleKey();
                    if (key == "SPACE")
                        bankMenu = 5;
                }

                while (bankMenu == 4) // Open an account - You already have one
                {
                    ClearShopDisplay();
                    CyText(2, "You already have one.");
                    UpdateDisplay();
                    var key = GetSingleKey();
                    if (key == "SPACE")
                        bankMenu = 1;
                }

                while (bankMenu == 5) // Open an account - Initial deposit
                {
                    var coppers = InputValue("Deposit how much (in coppers)?", 13);

                    if (coppers > 0)
                    {
                        if (CheckCoins(0, 0, coppers))
                        {
                            currentAccount = accountRef + accountType;
                            plyr.bankAccountStatuses[currentAccount] = 1;
                            DeductCoins(0, 0, coppers);
                            plyr.bankAccountBalances[currentAccount] += coppers;
                            bankMenu = 6;
                        } else
                        {
                            // Insufficient funds
                            bankMenu = 7;
                        }
                    }

                    if (coppers == 0)
                        bankMenu = 1;
                }

                while (bankMenu == 6) // Open an account - success
                {
                    ClearShopDisplay();
                    CyText(2, "We are glad to be of service.");
                    UpdateDisplay();
                    var key = GetSingleKey();
                    if (key == "SPACE")
                        bankMenu = 1;
                }

                while (bankMenu == 7) // Open an account - offered more than you have or less than withdraw amount in account
                {
                    ClearShopDisplay();
                    CyText(2, "You have insufficient funds.");
                    UpdateDisplay();
                    var key = GetSingleKey();
                    if (key == "SPACE")
                        bankMenu = 1;
                }

                while (bankMenu == 8) // Close an account
                {
                    ClearShopDisplay();

                    CyText(0, "What account would you like to close?");

                    var str = "(1) Low Risk Account    ";
                    if (plyr.bankAccountStatuses[accountRef] == 0)
                        str = "(1) Low Risk Account    : Already closed";
                    BText(2, 2, str);
                    str = "(2) Medium Risk Account";
                    if (plyr.bankAccountStatuses[accountRef + 1] == 0)
                        str = "(2) Medium Risk Account : Already closed";
                    BText(2, 3, str);
                    str = "(3) High Risk Account";
                    if (plyr.bankAccountStatuses[accountRef + 2] == 0)
                        str = "(3) High Risk Account   : Already closed";
                    BText(2, 4, str);
                    BText(2, 6, "(0) Other banking options");

                    UpdateDisplay();

                    var key = GetSingleKey();
                    if (key == "1")
                    {
                        accountType = 0;
                        bankMenu = 9;
                        if (plyr.bankAccountStatuses[accountRef] == 0)
                            bankMenu = 10;
                    }
                    if (key == "2")
                    {
                        accountType = 1;
                        bankMenu = 9;
                        if (plyr.bankAccountStatuses[accountRef + 1] == 0)
                            bankMenu = 10;
                    }
                    if (key == "3")
                    {
                        accountType = 2;
                        bankMenu = 9;
                        if (plyr.bankAccountStatuses[accountRef + 2] == 0)
                            bankMenu = 10;
                    }
                    if (key == "0")
                        bankMenu = 1;
                }

                while (bankMenu == 9) // Close an account
                {
                    ClearShopDisplay();
                    CyText(2, "This account is now closed.");
                    UpdateDisplay();

                    var key = GetSingleKey();
                    if (key == "SPACE")
                    {
                        currentAccount = accountRef + accountType;
                        plyr.bankAccountStatuses[currentAccount] = 0;
                        int coppersToWithdraw = plyr.bankAccountBalances[currentAccount];
                        plyr.bankAccountBalances[currentAccount] = 0;
                        WithdrawCoppers(coppersToWithdraw);
                        bankMenu = 1;
                    }
                }

                while (bankMenu == 10) // Already closed
                {
                    ClearShopDisplay();
                    CyText(2, "Already closed.");
                    UpdateDisplay();
                    var key = GetSingleKey();
                    if (key == "SPACE")
                        bankMenu = 1;
                }

                while (bankMenu == 11) // apply for job
                {
                    var jobNumber = bankJobOpenings[bankNo].jobNumber;

                    ClearShopDisplay();
                    if (jobNumber == 255)
                    {
                        BText(7, 0, "I'm sorry but there are no");
                        CyText(2, "job openings at the moment.");
                        CyText(9, "( Press a key )");
                        UpdateDisplay();

                        var key = GetSingleKey();
                        if (key != "")
                            bankMenu = 1;
                    } else
                    {
                        var str = "We have an opening for a " + bankJobs[jobNumber].name;
                        CyText(0, str);
                        str = $"for {bankJobOpenings[bankNo].jobHoursRequired} hours at {bankJobOpenings[bankNo].jobHourlyIncome} coppers per hour.";
                        CyText(1, str);
                        CyText(3, "Would you like to apply?");
                        CyText(5, "( es or  o)");
                        SetFontColor(40, 96, 244, 255);
                        CyText(5, " Y      N  ");
                        SetFontColor(215, 215, 215, 255);
                        UpdateDisplay();

                        var key = GetSingleKey();
                        if (key == "Y")
                            bankMenu = 12;
                        if (key == "N")
                            bankMenu = 1;
                    }
                }

                while (bankMenu == 12) // Check job stat requirements
                {
                    var jobNumber = bankJobOpenings[bankNo].jobNumber;
                    var statRequirementName = bankJobs[jobNumber].statRequirementName;
                    var statRequirement = bankJobs[jobNumber].statRequirementValue;
                    var jobStatMet = false;

                    // Check stat requirement met
                    if ((statRequirementName == "Strength") && (statRequirement <= plyr.str))
                        jobStatMet = true;
                    if ((statRequirementName == "Intelligence") && (statRequirement <= plyr.inte))
                        jobStatMet = true;
                    if ((statRequirementName == "Alignment") && (statRequirement <= plyr.alignment))
                        jobStatMet = true;

                    if (!jobStatMet)
                    {
                        ClearShopDisplay();
                        str = "You will need more " + statRequirementName;
                        if ((statRequirementName == "Alignment"))
                            str = "You are not righteous enough";
                        CyText(0, str);
                        CyText(1, "to get the job.");
                        CyText(9, "( Press a key )");
                        UpdateDisplay();

                        var key = GetSingleKey();
                        if (key == "SPACE")
                            bankMenu = 1;

                    } else
                    {
                        workingHours = bankJobOpenings[bankNo].jobHoursRequired;
                        hourlyRate = bankJobOpenings[bankNo].jobHourlyIncome;
                        jobIncome = workingHours * hourlyRate;
                        bankMenu = 13;
                    }
                }

                while (bankMenu == 13) // Display working message
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
                    var str = $"You have earned {jobIncome} coppers.";
                    CyText(3, str);
                    CyText(9, "( Press a key )");
                    UpdateDisplay();
                    var key = GetSingleKey();
                    if (key == "SPACE")
                    {
                        plyr.copper += jobIncome;
                        bankJobOpenings[bankNo].jobNumber = 255;
                        bankMenu = 1;
                    }
                }

                while (bankMenu == 14) // Examine banks books
                {
                    ClearShopDisplay();

                    BText(2, 0, "Account failures at our bank");
                    var str = $"Low Risk Account Failures:    {banks[bankNo].accounts[0].failures}";
                    BText(2, 2, str);
                    str = $"Medium Risk Account Failures: {banks[bankNo].accounts[1].failures}";
                    BText(2, 4, str);
                    str = $"High Risk Account Failures:   {banks[bankNo].accounts[2].failures}";
                    BText(2, 6, str);
                    CyText(9, "(Press SPACE to continue)");

                    UpdateDisplay();

                    var key = GetSingleKey();
                    if (key == "SPACE")
                        bankMenu = 1;
                }

                while (bankMenu == 15) // Examine my accounts
                {
                    ClearShopDisplay();

                    BText(2, 0, "Account Balances");

                    var str = $"Low Risk Account:    {plyr.bankAccountBalances[accountRef]}";
                    if (plyr.bankAccountStatuses[accountRef] == 0)
                        str = "Low Risk Account:    Closed";
                    BText(2, 2, str);
                    str = $"Medium Risk Account: {plyr.bankAccountBalances[accountRef + 1]}";
                    if (plyr.bankAccountStatuses[accountRef + 1] == 0)
                        str = "Medium Risk Account: Closed";
                    BText(2, 4, str);
                    str = $"High Risk Account:   {plyr.bankAccountBalances[accountRef + 2]}";
                    if (plyr.bankAccountStatuses[accountRef + 2] == 0)
                        str = "High Risk Account:   Closed";
                    BText(2, 6, str);
                    CyText(9, "(Press SPACE to continue)");

                    UpdateDisplay();

                    var key = GetSingleKey();
                    if (key == "SPACE")
                        bankMenu = 1;
                }

                while (bankMenu == 16) // Make a deposit - select account & show balances
                {
                    ClearShopDisplay();

                    CyText(0, "Deposit in which account?");

                    var str = $"(1) Low Risk Account    : {plyr.bankAccountBalances[accountRef]}";
                    if (plyr.bankAccountStatuses[accountRef] == 0)
                        str = "(1) Low Risk Account    : No account";
                    BText(2, 2, str);
                    str = $"(2) Medium Risk Account : {plyr.bankAccountBalances[accountRef + 1]}";
                    if (plyr.bankAccountStatuses[accountRef + 1] == 0)
                        str = "(2) Medium Risk Account : No account";
                    BText(2, 3, str);
                    str = $"(3) High Risk Account   : {plyr.bankAccountBalances[accountRef + 2]}";
                    if (plyr.bankAccountStatuses[accountRef + 2] == 0)
                        str = "(3) High Risk Account   : No account";
                    BText(2, 4, str);
                    BText(2, 6, "(0) Other banking options");

                    UpdateDisplay();

                    var key = GetSingleKey();
                    if (key == "1")
                    {
                        accountType = 0;
                        bankMenu = 18;
                        if (plyr.bankAccountStatuses[accountRef] == 0)
                            bankMenu = 17;
                    }
                    if (key == "2")
                    {
                        accountType = 1;
                        bankMenu = 18;
                        if (plyr.bankAccountStatuses[accountRef + 1] == 0)
                            bankMenu = 17;
                    }
                    if (key == "3")
                    {
                        accountType = 2;
                        bankMenu = 18;
                        if (plyr.bankAccountStatuses[accountRef + 2] == 0)
                            bankMenu = 17;
                    }
                    if (key == "0")
                        bankMenu = 1;
                }

                while (bankMenu == 17) // Open an account - You already have one
                {
                    ClearShopDisplay();
                    CyText(2, "You don't have an account of that type.");
                    UpdateDisplay();
                    var key = GetSingleKey();
                    if (key == "SPACE")
                        bankMenu = 1;
                }

                while (bankMenu == 18) // Deposit - Initial deposit
                {
                    var coppers = InputValue("Deposit how much (in coppers)?", 13);

                    if (coppers > 0)
                    {
                        if (CheckCoins(0, 0, coppers))
                        {
                            currentAccount = accountRef + accountType;
                            plyr.bankAccountStatuses[currentAccount] = 1;
                            DeductCoins(0, 0, coppers);
                            plyr.bankAccountBalances[currentAccount] += coppers;
                            bankMenu = 6;
                        } else
                        {
                            // Insufficient funds
                            bankMenu = 7;
                        }
                    }

                    if (coppers == 0)
                        bankMenu = 1;
                }

                while (bankMenu == 19) // Make a withdrawal - select account & show balances
                {
                    ClearShopDisplay();

                    CyText(0, "Withdraw from which account?");

                    var str = $"(1) Low Risk Account    : {plyr.bankAccountBalances[accountRef]}";
                    if (plyr.bankAccountStatuses[accountRef] == 0)
                        str = "(1) Low Risk Account    : No account";
                    BText(2, 2, str);
                    str = $"(2) Medium Risk Account : {plyr.bankAccountBalances[accountRef + 1]}";
                    if (plyr.bankAccountStatuses[accountRef + 1] == 0)
                        str = "(2) Medium Risk Account : No account";
                    BText(2, 3, str);
                    str = $"(3) High Risk Account   : {plyr.bankAccountBalances[accountRef + 2]}";
                    if (plyr.bankAccountStatuses[accountRef + 2] == 0)
                        str = "(3) High Risk Account   :  No account";
                    BText(2, 4, str);
                    BText(2, 6, "(0) Other banking options");

                    UpdateDisplay();

                    var key = GetSingleKey();
                    if (key == "1")
                    {
                        accountType = 0;
                        bankMenu = 20;
                        if (plyr.bankAccountStatuses[accountRef] == 0)
                            bankMenu = 17;
                    }
                    if (key == "2")
                    {
                        accountType = 1;
                        bankMenu = 20;
                        if (plyr.bankAccountStatuses[accountRef + 1] == 0)
                            bankMenu = 17;
                    }
                    if (key == "3")
                    {
                        accountType = 2;
                        bankMenu = 20;
                        if (plyr.bankAccountStatuses[accountRef + 2] == 0)
                            bankMenu = 17;
                    }
                    if (key == "0")
                        bankMenu = 1;
                }

                while (bankMenu == 20) // Withdraw - Amount
                {
                    var coppersToWithdraw = InputValue("Withdraw how much (in coppers)?", 13);

                    if (coppersToWithdraw > 0)
                    {
                        currentAccount = accountRef + accountType;
                        if (coppersToWithdraw <= plyr.bankAccountBalances[currentAccount])
                        {
                            plyr.bankAccountBalances[currentAccount] -= coppersToWithdraw;
                            WithdrawCoppers(coppersToWithdraw);
                            bankMenu = 6; // custom message required
                        } else
                        {
                            // Insufficient funds - needs a custom message
                            bankMenu = 7;
                        }
                    }

                    if (coppersToWithdraw == 0)
                        bankMenu = 1; // 0 means changed mind about withdrawal
                }

                while (bankMenu == 21) // Sells gems or jewels
                {
                    ClearShopDisplay();

                    CyText(0, "WELCOME FRIEND TO OUR");
                    CyText(1, "GEM AND JEWELRY APPRAISAL DEPARTMENT");
                    BText(2, 3, "Do you wish our experts to appraise your:");
                    BText(2, 5, "(1) Fine gems");
                    BText(2, 6, "(2) Magnificent jewelry");
                    UpdateDisplay();

                    var key = GetSingleKey();
                    if (key == "1")
                    {
                        bankMenu = 22;
                        if (plyr.gems > 0)
                            bankMenu = 26;
                    }
                    if (key == "2")
                    {
                        bankMenu = 22;
                        if (plyr.jewels > 0)
                            bankMenu = 24;
                    }
                }

                while (bankMenu == 22) // Sells gems or jewels - no gems or jewels
                {
                    ClearShopDisplay();

                    BText(2, 0, "The expert waits patiently...");
                    BText(2, 2, "Then informs you that he will be");
                    BText(2, 4, "glad to give you an appraisal");
                    BText(2, 6, "when you have something to");
                    BText(2, 8, "appraise.");
                    UpdateDisplay();

                    var key = GetSingleKey();
                    if (key == "SPACE")
                        bankMenu = 1;
                }

                while (bankMenu == 23) // gem or jewel success
                {
                    ClearShopDisplay();

                    BText(2, 0, "The expert carefully examines");
                    BText(2, 2, "your find and informs you that");
                    var str = $"it is worth {findValue} in gold pieces.";
                    BText(2, 4, str);
                    UpdateDisplay();

                    var key = GetSingleKey();
                    if (key == "SPACE")
                    {
                        plyr.gold += findValue;
                        findValue = 0;
                        bankMenu = 1;
                    }
                }

                while (bankMenu == 24) // Sell jewel success
                {
                    plyr.jewels--;
                    var worthless = Random(0, 20);
                    if (worthless < 2)
                    {
                        findValue = 0;
                        bankMenu = 25;
                    } else
                    {
                        findValue = (Random(1, 23)) + 16;
                        bankMenu = 23;
                    }
                }

                while (bankMenu == 25) // gem or jewel worthless message
                {
                    ClearShopDisplay();

                    BText(2, 0, "The expert carefully examines");
                    BText(2, 2, "your find and informs you that");
                    BText(2, 4, "it is completely worthless!");
                    UpdateDisplay();

                    var key = GetSingleKey();
                    if (key == "SPACE")
                        bankMenu = 1;
                }

                while (bankMenu == 26) // calculate gem value
                {
                    plyr.gems--;
                    var  worthless = Random(0, 20);
                    if (worthless < 2)
                    {
                        findValue = 0;
                        bankMenu = 25;
                    } else
                    {
                        findValue = Random(1, 13);
                        bankMenu = 23;
                    }
                }
            }
            LeaveShop();
        }

        //TODO: Move to clock
        public static void CheckDailybankInterest ()
        {
            // Run at the start of each new day

            for (var i = 0; i < plyr.bankAccountBalances.Length; i++)
            {
                //TODO: Fix this logic so account info is kept per bank, not per account so we can use a foreach
                if (plyr.bankAccountBalances[i] > 0)
                {
                    //TODO: Verify math
                    //0-2 = 0, 3-5 = 1, 6-8 = 2
                    var bankNo = i / banks.Length;

                    //0, 3, 6 = 0; 1, 4, 7 = 1; 2, 5, 8 = 2
                    var accountNo = i % 3;

                    var account = banks[bankNo].accounts[accountNo];

                    var newInterest = (((float)plyr.bankAccountBalances[i]) / 100) * account.interest;
                    plyr.bankAccountBalances[i] += ((int)newInterest);
                }
            }
        }

        #region Private Members

        //TODO: Return bank
        private static int GetBankNo ()
        {
            int bank_no = -1;
            for (var  i = 0; i < banks.Length; i++) 
            {
                if (banks[i].location == plyr.location)
                    bank_no = i; // The number of the bank you have entered
            }

            return bank_no;
        }        

        private static void WithdrawCoppers ( int coppersToWithdraw )
        {
            // Adds an amount of coppers to plyr in gold, silver and copper coins

            var silverFromWithdrawal = 0;
            var copperFromWithdrawal = 0;
            var remainder = coppersToWithdraw % 100;
            var goldFromWithdrawal = (coppersToWithdraw - remainder) / 100;
            coppersToWithdraw -= (goldFromWithdrawal * 100);
            if (remainder > 0)
            {
                remainder = coppersToWithdraw % 10;
                silverFromWithdrawal = (coppersToWithdraw - remainder) / 10;
                coppersToWithdraw -= (silverFromWithdrawal * 10);
                if (remainder > 0)
                    copperFromWithdrawal = remainder;
            }

            plyr.copper += copperFromWithdrawal;
            plyr.gold += goldFromWithdrawal;
            plyr.silver += silverFromWithdrawal;
        }        
        #endregion

        #region Review Data

        public static int bankNo;
        public static int accountType;
        public static int accountRef;
        public static int currentAccount;
        public static int findValue;

        //TODO: Move to map
        //MLT: Fix double to float conversion
        public static Bank[] banks =
        {
            new Bank() {
                name = "First City Bank", 
                accounts = new [] {
                            new BankAccount() { interest = 0.5F, failProb = 0.3F, minBalance = 1, failures = 0 },
                            new BankAccount() { interest = 0.9F, failProb = 0.9F, minBalance = 1, failures = 0 },
                            new BankAccount() { interest = 2.6F, failProb = 3.0F, minBalance = 1, failures = 0 } 
                            }, 
                location = 37, 
                openingHour = 0, 
                closingHour = 23,
                jobProbability = 51, 
                gemCost = 0, 
                jewelCost = 0 
            },
            new Bank() { 
                name = "Granite Bank", 
                accounts = new [] {
                    new BankAccount() { interest = 0.5F, failProb = 0.2F, minBalance = 1, failures = 0 },
                    new BankAccount() { interest = 1.0F, failProb = 1.0F, minBalance = 1, failures = 0 },
                    new BankAccount() { interest = 2.7F, failProb = 3.1F, minBalance = 1, failures = 0 }
                    }, 
                location = 38, 
                openingHour = 11, 
                closingHour = 2, 
                jobProbability = 51, 
                gemCost = 3000, 
                jewelCost = 1 
            },
            new Bank() { 
                name = "Gram's Gold Exchange", 
                accounts = new [] {
                    new BankAccount() { interest = 0.6F, failProb = 0.1F, minBalance = 1, failures = 0 },
                    new BankAccount() { interest = 1.3F, failProb = 1.0F, minBalance = 1, failures = 0 },
                    new BankAccount() { interest = 3.2F, failProb = 3.3F, minBalance = 1, failures = 0 }
                   },
                location = 39, 
                openingHour = 0, 
                closingHour = 23, 
                jobProbability = 46, 
                gemCost = 0, 
                jewelCost = 0 
            }
        };

        public static BankJobOpening[] bankJobOpenings = Arrays.InitializeWithDefaultInstances<BankJobOpening>(3);

        //TODO: Move to map
        //MLT: Double to float
        public static BankJob[] bankJobs =
        {
            new BankJob() { name = "Guard", minIncome = 30, maxIncome = 36, statRequirementName = "Strength", statRequirementValue = 15, fatigueRate = 0.6875F, minorWoundProbability = 15.95F, majorWoundProbability = 3.97F },
            new BankJob() { name = "File Clerk", minIncome = 50, maxIncome = 56, statRequirementName = "Intelligence", statRequirementValue = 20, fatigueRate = 0.5625F, minorWoundProbability = 2.29F, majorWoundProbability = 0.05F },
            new BankJob() { name = "Coin Roller", minIncome = 22, maxIncome = 28, statRequirementName = "Alignment", statRequirementValue = 144, fatigueRate = 0.59375F, minorWoundProbability = 2.29F, majorWoundProbability = 0.05F }
        };

        #endregion
    }
}