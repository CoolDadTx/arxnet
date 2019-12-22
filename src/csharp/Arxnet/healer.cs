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
        public static int alcoholPrice = 0;
        public static int clarityPrice = 0;
        public static int diagnosePrice = 0;
        public static int diseasesPrice = 0;

        // Base prices
        public static int healPrice = 0;
        public static int poisonsPrice = 0;

        public static int GetHealerNo ()
        {
            var healer_no = 0;
            if (plyr.location == 74)
                healer_no = 1; // One Way Soothers
            if (plyr.location == 75)
                healer_no = 2; // Alpha Omega Healers
            return healer_no;
        }

        public static void HealerShopHealWounds ()
        {
            var str = $"Cure how many hits at@@ {Itos(healPrice)} coppers each?";
            var hpToHealInput = InputValue(str, 14);

            var hpToHeal = hpToHealInput * healPrice;
            if (hpToHealInput > 0)
            {
                if (!CheckCoins(0, 0, hpToHeal))
                    // Insufficient funds
                    ModuleMessage("I'm sorry... You have not the funds.");
                else
                {
                    // Sufficient funds
                    ModuleMessage("IT SHALL BE DONE!");
                    DeductCoins(0, 0, hpToHeal);
                    plyr.hp = plyr.hp + hpToHealInput;
                    if (plyr.hp > plyr.maxhp)
                        plyr.hp = plyr.maxhp;
                    HealerUpdateLastServiceTime();
                    HealerUpdatePrices();
                }
            }
        }

        public static void HealerUpdateLastServiceTime ()
        {
            var healerNo = GetHealerNo();
            healerNo--;
            plyr.healerDays[healerNo] = plyr.days;
            plyr.healerHours[healerNo] = plyr.hours;
            plyr.healerMinutes[healerNo] = plyr.minutes;
        }

        public static void HealerUpdatePrices ()
        {
            var healerNo = GetHealerNo();
            healerNo--;
            var days = plyr.healerDays[healerNo];
            var hours = plyr.healerHours[healerNo];
            var minutes = plyr.healerMinutes[healerNo];
            var n = 1;
            if (days == plyr.days)
                n = 2;
            if ((days == plyr.days) && (hours == plyr.hours))
                n = 4;
            if ((days == plyr.days) && (hours == plyr.hours) && (minutes == plyr.minutes))
                n = 8;
            if ((days == 0) && (hours == 0) && (minutes == 0))
                n = 1; // First time visitor

            healPrice = 10 * n;
            poisonsPrice = 100 * n;
            diseasesPrice = 200 * n;
            alcoholPrice = 20 * n;
            clarityPrice = 200 * n;
            diagnosePrice = 10 * n;
        }

        public static void ShopHealer ()
        {
            var healerMenu = 1; // high level menu

            plyr.status = GameStates.Module; // shopping

            // Both healers have the same base prices
            HealerUpdatePrices();

            var healerNo = GetHealerNo();
            if (healerNo == 1) // One Way
            {
                SetAutoMapFlag(plyr.map, 28, 34);
                SetAutoMapFlag(plyr.map, 29, 34);
                SetAutoMapFlag(plyr.map, 30, 34);
                SetAutoMapFlag(plyr.map, 28, 35);
                SetAutoMapFlag(plyr.map, 29, 35);
                SetAutoMapFlag(plyr.map, 30, 35);
                SetAutoMapFlag(plyr.map, 28, 36);
                SetAutoMapFlag(plyr.map, 29, 36);
                SetAutoMapFlag(plyr.map, 30, 36);
            }
            if (healerNo == 2) // Alphe Omega
            {
                // TODO Add automap co-ordinates for 2nd healer!
                SetAutoMapFlag(plyr.map, 32, 44);
                SetAutoMapFlag(plyr.map, 31, 44);
                SetAutoMapFlag(plyr.map, 33, 44);
                SetAutoMapFlag(plyr.map, 31, 43);
            }

            // determine odd or even hour and if this healer is open
            var oddHour = false;
            var h = plyr.hours;
            if ((h == 1) ||
                (h == 3) ||
                (h == 5) ||
                (h == 7) ||
                (h == 9) ||
                (h == 11) ||
                (h == 13) ||
                (h == 15) ||
                (h == 17) ||
                (h == 19) ||
                (h == 21) ||
                (h == 23))
                oddHour = true;
            var healerOpen = false;
            if ((healerNo == 1) && (oddHour))
                healerOpen = true;
            if ((healerNo == 2) && (!oddHour))
                healerOpen = true;

            if (healerOpen)
            {
                LoadShopImage(15);
                healerMenu = 1;
            } else
            {
                LoadShopImage(21);
                healerMenu = 2;
            }

            while (healerMenu > 0)
            {
                while (healerMenu == 1) // main menu
                {
                    ClearShopDisplay();

                    CyText(0, "Welcome Stranger!");
                    CyText(1, "How may I serve you?");
                    BText(11, 3, "(1) Heal");
                    BText(11, 4, "(2) Cleanse");
                    BText(11, 5, "(3) Remove delusions");
                    BText(11, 6, "(4) Diagnose");
                    BText(11, 7, "(0) Leave");

                    DisplayCoins();
                    UpdateDisplay();

                    var key = GetSingleKey();

                    if (key == "0")
                        healerMenu = 0;
                    if (key == "down")
                        healerMenu = 0;
                    if (key == "1")
                        HealerShopHealWounds();
                    if (key == "2")
                        healerMenu = 3;
                    if (key == "3")
                        healerMenu = 4;
                    if (key == "4")
                        healerMenu = 5;
                }

                while (healerMenu == 2) // at bar, table or booth menu
                {
                    ModuleMessage("It looks as though the@@Healer is not here.");
                    healerMenu = 0;
                }

                while (healerMenu == 3) // Cleanse menu
                {
                    ClearShopDisplay();

                    CyText(1, "Remove which illnesses?");
                    BText(1, 3, "(1) Cleanse Poisons");
                    BText(1, 4, "(2) Cure Diseases");
                    BText(1, 5, "(3) Remove Alcohol");
                    BText(1, 6, "(0) None");

                    BText(24, 3, $"{Itos(poisonsPrice)} coppers");
                    BText(24, 4, $"{Itos(diseasesPrice)} coppers");
                    BText(24, 5, $"{Itos(alcoholPrice)} coppers");

                    DisplayCoins();
                    UpdateDisplay();

                    var key = GetSingleKey();

                    if (key == "0")
                        healerMenu = 1;
                    if (key == "2")
                    {
                        if (!CheckCoins(0, 0, diseasesPrice))
                            ModuleMessage("I'm sorry... You have not the funds.");
                        else
                        {
                            ModuleMessage("IT SHALL BE DONE!");
                            DeductCoins(0, 0, diseasesPrice);
                            plyr.diseases[0] = 0;
                            plyr.diseases[1] = 0;
                            plyr.diseases[2] = 0;
                            plyr.diseases[3] = 0;
                            // CLEAR ANY RELATED EFFECTS
                            HealerUpdateLastServiceTime();
                            HealerUpdatePrices();
                        }
                        healerMenu = 1;
                    }
                    if (key == "1")
                    {
                        if (!CheckCoins(0, 0, poisonsPrice))
                            ModuleMessage("I'm sorry... You have not the funds.");
                        else
                        {
                            ModuleMessage("IT SHALL BE DONE!");
                            DeductCoins(0, 0, poisonsPrice);
                            // CLEAR ANY RELATED EFFECTS
                            plyr.poison[0] = 0;
                            plyr.poison[1] = 0;
                            plyr.poison[2] = 0;
                            plyr.poison[3] = 0;
                            HealerUpdateLastServiceTime();
                            HealerUpdatePrices();
                        }
                        healerMenu = 1;
                    }
                    if (key == "3")
                    {
                        if (!CheckCoins(0, 0, alcoholPrice))
                            ModuleMessage("I'm sorry... You have not the funds.");
                        else
                        {
                            ModuleMessage("IT SHALL BE DONE!");
                            DeductCoins(0, 0, alcoholPrice);
                            plyr.alcohol = 0;
                            HealerUpdateLastServiceTime();
                            HealerUpdatePrices();
                        }
                        healerMenu = 1;
                    }
                }

                while (healerMenu == 4) // Remove delusions
                {
                    ClearShopDisplay();

                    CyText(1, "Shall I remove your delusions");
                    CyText(2, $"for {Itos(clarityPrice)} coppers?");
                    CyText(5, "(Yes or No)");
                    DisplayCoins();
                    UpdateDisplay();

                    var key = GetSingleKey();

                    if (key == "N")
                        healerMenu = 1;
                    if (key == "Y")
                    {
                        if (!CheckCoins(0, 0, clarityPrice))
                            ModuleMessage("I'm sorry... You have not the funds.");
                        else
                        {
                            ModuleMessage("IT SHALL BE DONE!");
                            DeductCoins(0, 0, clarityPrice);
                            plyr.delusion = 0;
                            HealerUpdateLastServiceTime();
                            HealerUpdatePrices();
                        }
                        healerMenu = 1;
                    }
                }

                while (healerMenu == 5) // Diagnose
                {
                    ClearShopDisplay();

                    CyText(1, "Shall I check for and diagnose disease");
                    CyText(2, $"for {Itos(diagnosePrice)} coppers?");
                    CyText(5, "(Yes or No)");
                    DisplayCoins();
                    UpdateDisplay();

                    var key = GetSingleKey();

                    if (key == "N")
                        healerMenu = 1;
                    if (key == "Y")
                    {
                        if (!CheckCoins(0, 0, diagnosePrice))
                            ModuleMessage("I'm sorry... You have not the funds.");
                        else
                        {
                            ModuleMessage("IT SHALL BE DONE!");
                            DeductCoins(0, 0, diagnosePrice);
                            // Make diseases visible
                            if ((plyr.diseases[0] > 0) && (plyr.diseases[0] < 15))
                                plyr.diseases[0] = 15;
                            HealerUpdateLastServiceTime();
                            HealerUpdatePrices();
                        }
                        healerMenu = 1;
                    }
                }
            }
            LeaveShop();
        }

        // extern Player plyr;
        // extern sf::RenderWindow App;
    }
}