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
        public static bool CheckCoins(int gold, int silver, int copper)
        {
            var sufficientFunds = false;
            int itemCostInCoppers = (gold * 100) + (silver * 10) + copper;
            int playerTotalCoppers = (plyr.gold * 100) + (plyr.silver * 10) + plyr.copper;
            if(itemCostInCoppers <= playerTotalCoppers)
                sufficientFunds = true;
            return sufficientFunds;
        }

        public static void DeductCoins(int gold, int silver, int copper)
        {
            // Assumption 1 - Goods will be paid for using copper coins if possible as they take up the most weight for least value
            // Assumption 2 - Change will be given using higher value coins
            var deductionCompleted = false;
            int itemCost = (gold * 100) + (silver * 10) + copper;

            if(itemCost <= plyr.copper)
            {
                plyr.copper -= itemCost;
                deductionCompleted = true;
            } else
            {
                itemCost -= plyr.copper;
                plyr.copper = 0;
            }

            if(!deductionCompleted)
            {
                int copperChange = (itemCost % 10);
                int numberOfSilversRequired = (itemCost / 10);
                if(!copperChange == 0)
                    numberOfSilversRequired++;
                if(numberOfSilversRequired <= plyr.silver)
                {
                    plyr.silver -= numberOfSilversRequired;
                    if(!copperChange == 0)
                        plyr.copper += (10 - copperChange);
                    deductionCompleted = true;
                } else
                {
                    itemCost -= (plyr.silver * 10);
                    plyr.silver = 0;
                }
            }

            if(!deductionCompleted)
            {
                var copperChange = 0;
                var silverChange = 0;
                var change = 0;
                change = (itemCost % 100);
                if(!change == 0)
                {
                    copperChange = (change % 10);
                    silverChange = (change / 10);
                }
                int numberOfGoldsRequired = (itemCost / 100);
                if(!copperChange == 0)
                    silverChange++;
                if(!silverChange == 0)
                    numberOfGoldsRequired++;

                plyr.gold -= numberOfGoldsRequired;
                if(!copperChange == 0)
                    plyr.copper += (10 - copperChange);
                if(!silverChange == 0)
                    plyr.silver += (10 - silverChange);
                deductionCompleted = true;
            }
        }
        
        public static void DisplayCoins()
        {
            string str;
            int coinsCopper = (plyr.gold * 100) + (plyr.silver * 10) + plyr.copper;
            str = $"Your coins in copper {ToCurrency(coinsCopper)}";
            CyText(9, str);
        }

        public static void DisplaySilverCoins()
        {
            string str;
            int coinsSilver = (plyr.gold * 10) + plyr.silver + (plyr.copper / 10);
            str = $"Your coins in silver {ToCurrency(coinsSilver)}";
            CyText(9, str);
        }

        public static string GetSingleKey()
        {
            string key;
            key = ReadKey();

            return key;
        }

        public static string GetTextChar()
        {
            string keyString;
            var Event = new sf.Event();
            while(App.pollEvent(Event))
            {
                if(Event.type == sf.Event.TextEntered)
                {
                    if(Event.text.unicode < 128)
                        keyString = Event.text.unicode;
                    if(Event.text.unicode == 13)
                        keyString = "RETURN";
                    if(Event.text.unicode == 32)
                        keyString = "SPACE";
                    if(Event.text.unicode == 8)
                        keyString = "BACKSPACE";
                }
            }
            return (keyString);
        }

        public static int Hex2Dec(string s)
        {
            var ss = new std::stringstream(s);
            int i;
                ss >> std::hex >> i;
            return i;
        }

        public static int InputValue(string message, int shopNo)
        {
            var itemQuantity = 0;

            string str;
            string key;
            var inputText = "";
            var maxNumberSize = 6;
            var enterKeyNotPressed = true;
            while(enterKeyNotPressed)
            {
                // error below?
                ClearShopDisplay();
                CyText(0, message);
                if(shopNo == 13)
                    DisplayCoins(); // Bank
                if(shopNo == 14)
                    DisplayCoins(); // City Healer

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

        public static bool KeyPressed()
        {
            if(ReadKey() != "")
                return true;

            return false;
        }

        public static void ModuleMessage(string txt)
        {
            var key = "";
            while(key != "SPACE")
            {
                ClearShopDisplay();
                CText(txt);
                CyText(9, "( Press SPACE to continue )");
                UpdateDisplay();
                key = GetSingleKey();
            }
        }

        public static int OldRollDice(int x, int y)
        {
            var result = 0;
            if(x != 0)
            {
                var i = 1;
                while(i <= x)
                {
                    int roll = Randn(0, y);
                    result = result + roll;
                    i++;
                }
            }
            return result;
        }

        public static int Randn(int low, int high)
        {
            int result;
            
            result = RandomNumbers.NextNumber() % ((high - low) + 1) + low;
            return result;
        }

        public static string ReadKey()
        {
            var keyString = "";

            // Process events
            var Event = new sf.Event();
            while(App.pollEvent(Event))
            {
                if((Event.type == sf.Event.KeyPressed) && (Event.key.code == sf.Keyboard.Left))
                    keyString = "left";
                if((Event.type == sf.Event.KeyPressed) && (Event.key.code == sf.Keyboard.Right))
                    keyString = "right";
                if((Event.type == sf.Event.KeyPressed) && (Event.key.code == sf.Keyboard.Up))
                    keyString = "up";
                if((Event.type == sf.Event.KeyPressed) && (Event.key.code == sf.Keyboard.Down))
                    keyString = "down";

                if((Event.type == sf.Event.KeyPressed) && (Event.key.code == sf.Keyboard.F1))
                    keyString = "F1";
                if((Event.type == sf.Event.KeyPressed) && (Event.key.code == sf.Keyboard.F2))
                    keyString = "F2";
                if((Event.type == sf.Event.KeyPressed) && (Event.key.code == sf.Keyboard.F3))
                    keyString = "F3";
                if((Event.type == sf.Event.KeyPressed) && (Event.key.code == sf.Keyboard.F4))
                    keyString = "F4";
                if((Event.type == sf.Event.KeyPressed) && (Event.key.code == sf.Keyboard.F5))
                    keyString = "F5";
                if((Event.type == sf.Event.KeyPressed) && (Event.key.code == sf.Keyboard.F6))
                    keyString = "F6";
                if((Event.type == sf.Event.KeyPressed) && (Event.key.code == sf.Keyboard.F7))
                    keyString = "F7";
                if((Event.type == sf.Event.KeyPressed) && (Event.key.code == sf.Keyboard.F8))
                    keyString = "F8";
                if((Event.type == sf.Event.KeyPressed) && (Event.key.code == sf.Keyboard.F10))
                    keyString = "F10";
                if((Event.type == sf.Event.KeyPressed) && (Event.key.code == sf.Keyboard.F11))
                    keyString = "F11";
                if((Event.type == sf.Event.KeyPressed) && (Event.key.code == sf.Keyboard.F12))
                    keyString = "F12";

                if((Event.type == sf.Event.KeyPressed) && (Event.key.code == sf.Keyboard.Num0))
                    keyString = "0";
                if((Event.type == sf.Event.KeyPressed) && (Event.key.code == sf.Keyboard.Comma))
                    keyString = ",";
                if((Event.type == sf.Event.KeyPressed) && (Event.key.code == sf.Keyboard.Period))
                    keyString = ".";
                if((Event.type == sf.Event.KeyPressed) && (Event.key.code == sf.Keyboard.Num1))
                    keyString = "1";
                if((Event.type == sf.Event.KeyPressed) && (Event.key.code == sf.Keyboard.Num2))
                    keyString = "2";
                if((Event.type == sf.Event.KeyPressed) && (Event.key.code == sf.Keyboard.Num3))
                    keyString = "3";
                if((Event.type == sf.Event.KeyPressed) && (Event.key.code == sf.Keyboard.Num4))
                    keyString = "4";
                if((Event.type == sf.Event.KeyPressed) && (Event.key.code == sf.Keyboard.Num5))
                    keyString = "5";
                if((Event.type == sf.Event.KeyPressed) && (Event.key.code == sf.Keyboard.Num6))
                    keyString = "6";
                if((Event.type == sf.Event.KeyPressed) && (Event.key.code == sf.Keyboard.Num7))
                    keyString = "7";
                if((Event.type == sf.Event.KeyPressed) && (Event.key.code == sf.Keyboard.Num8))
                    keyString = "8";
                if((Event.type == sf.Event.KeyPressed) && (Event.key.code == sf.Keyboard.Num9))
                    keyString = "9";

                if((Event.type == sf.Event.KeyPressed) && (Event.key.code == sf.Keyboard.A))
                    keyString = "A";
                if((Event.type == sf.Event.KeyPressed) && (Event.key.code == sf.Keyboard.B))
                    keyString = "B";
                if((Event.type == sf.Event.KeyPressed) && (Event.key.code == sf.Keyboard.C))
                    keyString = "C";
                if((Event.type == sf.Event.KeyPressed) && (Event.key.code == sf.Keyboard.D))
                    keyString = "D"; // diag info
                if((Event.type == sf.Event.KeyPressed) && (Event.key.code == sf.Keyboard.E))
                    keyString = "E"; // force encounter
                if((Event.type == sf.Event.KeyPressed) && (Event.key.code == sf.Keyboard.F))
                    keyString = "F";
                if((Event.type == sf.Event.KeyPressed) && (Event.key.code == sf.Keyboard.G))
                    keyString = "G";
                if((Event.type == sf.Event.KeyPressed) && (Event.key.code == sf.Keyboard.H))
                    keyString = "H";
                if((Event.type == sf.Event.KeyPressed) && (Event.key.code == sf.Keyboard.I))
                    keyString = "I";
                if((Event.type == sf.Event.KeyPressed) && (Event.key.code == sf.Keyboard.J))
                    keyString = "J";
                if((Event.type == sf.Event.KeyPressed) && (Event.key.code == sf.Keyboard.K))
                    keyString = "K";
                if((Event.type == sf.Event.KeyPressed) && (Event.key.code == sf.Keyboard.L))
                    keyString = "L";
                if((Event.type == sf.Event.KeyPressed) && (Event.key.code == sf.Keyboard.M))
                    keyString = "M";
                if((Event.type == sf.Event.KeyPressed) && (Event.key.code == sf.Keyboard.N))
                    keyString = "N";
                if((Event.type == sf.Event.KeyPressed) && (Event.key.code == sf.Keyboard.O))
                    keyString = "O";
                if((Event.type == sf.Event.KeyPressed) && (Event.key.code == sf.Keyboard.P))
                    keyString = "P";
                if((Event.type == sf.Event.KeyPressed) && (Event.key.code == sf.Keyboard.Q))
                    keyString = "Q";
                if((Event.type == sf.Event.KeyPressed) && (Event.key.code == sf.Keyboard.R))
                    keyString = "R";
                if((Event.type == sf.Event.KeyPressed) && (Event.key.code == sf.Keyboard.S))
                    keyString = "S";
                if((Event.type == sf.Event.KeyPressed) && (Event.key.code == sf.Keyboard.T))
                    keyString = "T";
                if((Event.type == sf.Event.KeyPressed) && (Event.key.code == sf.Keyboard.U))
                    keyString = "U";
                if((Event.type == sf.Event.KeyPressed) && (Event.key.code == sf.Keyboard.V))
                    keyString = "V";
                if((Event.type == sf.Event.KeyPressed) && (Event.key.code == sf.Keyboard.W))
                    keyString = "W";
                if((Event.type == sf.Event.KeyPressed) && (Event.key.code == sf.Keyboard.X))
                    keyString = "X";
                if((Event.type == sf.Event.KeyPressed) && (Event.key.code == sf.Keyboard.Y))
                    keyString = "Y";
                if((Event.type == sf.Event.KeyPressed) && (Event.key.code == sf.Keyboard.Z))
                    keyString = "Z";

                if((Event.type == sf.Event.KeyPressed) && (Event.key.code == sf.Keyboard.Space))
                    keyString = "SPACE";
                if((Event.type == sf.Event.KeyPressed) && (Event.key.code == sf.Keyboard.Return))
                    keyString = "RETURN";
                if((Event.type == sf.Event.KeyPressed) && (Event.key.code == sf.Keyboard.BackSpace))
                    keyString = "BACKSPACE";

                // Close window : exit
                if(Event.type == sf.Event.Closed)
                    keyString = "QUIT";

                // Escape key : exit
                if((Event.type == sf.Event.KeyPressed) && (Event.key.code == sf.Keyboard.Escape))
                    keyString = "ESC";
            }

            return (keyString);
        }

        public static int RollDice(int rolls, int dice)
        {
            var result = 0;

            for(var r = 0; r < rolls; r++)
                result = result + Randn(1, dice);
            if(result == 0)
                Console.Write("\nDice roll 0 error!\n");
            return result;
        }

        public static string ToCurrency(int i)
        {
            string temp;
            string formatedNumber;
            string low;
            string high;
            var s = new std::stringstream();
                s << i;
            temp = s.str();
            if(i < 1000)
                formatedNumber = temp;
            if(i > 999)
            {
                int c;
                int zLength = temp.Length;
                low = temp.Substring(zLength - 3, 3);
                if(zLength == 6)
                    c = 3;
                if(zLength == 5)
                    c = 2;
                if(zLength == 4)
                    c = 1;
                high = temp.Substring(0, c);
                formatedNumber = $"{high},{low}";
            }

            return formatedNumber;
        }

        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //string Itos(int i);
        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //string Ftos(float i);
    }
}