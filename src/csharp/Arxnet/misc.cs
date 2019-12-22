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
using System.Globalization;
using System.Text;
using System.Threading;

using SFML.Window;

namespace P3Net.Arx
{
    public partial class GlobalMembers
    {
        public static bool CheckCoins ( int gold, int silver, int copper )
        {
            var itemCostInCoppers = (gold * 100) + (silver * 10) + copper;
            var playerTotalCoppers = (plyr.gold * 100) + (plyr.silver * 10) + plyr.copper;

            return itemCostInCoppers <= playerTotalCoppers;
        }

        public static void DeductCoins ( int gold, int silver, int copper )
        {
            // Assumption 1 - Goods will be paid for using copper coins if possible as they take up the most weight for least value
            // Assumption 2 - Change will be given using higher value coins
            var itemCost = (gold * 100) + (silver * 10) + copper;

            if (itemCost <= plyr.copper)
            {
                plyr.copper -= itemCost;
                return;
            } else
            {
                itemCost -= plyr.copper;
                plyr.copper = 0;
            }

            //Use silver next
            var copperChange = (itemCost % 10);
            var numberOfSilversRequired = (itemCost / 10);

            //TODO: Test this logic - !copperChange == 0
            //if (!copperChange == 0)
            if (copperChange != 0)
                numberOfSilversRequired++;
            if (numberOfSilversRequired <= plyr.silver)
            {
                plyr.silver -= numberOfSilversRequired;
                //if (!copperChange == 0)
                if (copperChange != 0)
                    plyr.copper += (10 - copperChange);
                return;
            } else
            {
                itemCost -= (plyr.silver * 10);
                plyr.silver = 0;
            }            

            copperChange = 0;
            var silverChange = 0;
            var change = (itemCost % 100);

            //TODO: Test this logic - !change == 0
            if (change != 0)
            //if (!change == 0)
            {
                copperChange = (change % 10);
                silverChange = (change / 10);
            }
            var numberOfGoldsRequired = (itemCost / 100);
            //TODO: Test this logic - !copperChange == 0
            //if (!copperChange == 0)
            if (copperChange != 0)
                silverChange++;
            //TODO: Test this logic - !silverChange == 0
            //if (!silverChange == 0)
            if (silverChange != 0)
                numberOfGoldsRequired++;

            plyr.gold -= numberOfGoldsRequired;
            //TODO: Test this logic - !copperChange == 0
            //if (!copperChange == 0)
            if (copperChange != 0)
                plyr.copper += (10 - copperChange);
            //TODO: Test this logic - !silverChange == 0
            //if (!silverChange == 0)
            if (silverChange != 0)
                plyr.silver += (10 - silverChange);
        }

        public static void DisplayCoins ()
        {
            var coinsCopper = (plyr.gold * 100) + (plyr.silver * 10) + plyr.copper;

            CyText(9, $"Your coins in copper {ToCurrency(coinsCopper)}");
        }

        public static void DisplaySilverCoins ()
        {
            var coinsSilver = (plyr.gold * 10) + plyr.silver + (plyr.copper / 10);

            CyText(9, $"Your coins in silver {ToCurrency(coinsSilver)}");
        }

        public static string GetSingleKey () => ReadKey();

        public static string GetTextChar ()
        {
            //Before this was a pollEvents call but the implementation uses eventing so we'll switch to handler approach            
            //while (App.pollEvent(evt))
            //{
            //    if (evt.Type == EventType.TextEntered)
            //    {
            //        if (evt.Text.Unicode < 128)
            //            keyString = evt.Text.Unicode.ToString();
            //        if (evt.Text.Unicode == 13)
            //            keyString = "RETURN";
            //        if (evt.Text.Unicode == 32)
            //            keyString = "SPACE";
            //        if (evt.Text.Unicode == 8)
            //            keyString = "BACKSPACE";
            //    }
            //}         

            //TODO: How to switch from polling to event-based input?
            var keyString = "";       
            EventHandler<TextEventArgs> handler = ( o, e ) => {
                //TODO: Change this to juse use raw string value
                switch (e.Unicode)
                {
                    case "\r": keyString = "RETURN"; break;
                    case " ": keyString = "SPACE"; break;
                    case "\b": keyString = "BACKSPACE"; break;

                    default: keyString = e.Unicode; break;
                };
            };

            App.TextEntered += handler;
            do
            {
                App.WaitAndDispatchEvents();
            } while (String.IsNullOrEmpty(keyString));

            App.TextEntered -= handler;

            return keyString;
        }

        public static int Hex2Dec ( string s ) => Int32.Parse(s, NumberStyles.HexNumber);

        public static int InputValue ( string message, int shopNo )
        {
            var inputText = "";
            var maxNumberSize = 6;
            var enterKeyNotPressed = true;
            while (enterKeyNotPressed)
            {
                // error below?
                ClearShopDisplay();
                CyText(0, message);
                if (shopNo == 13)
                    DisplayCoins(); // Bank
                if (shopNo == 14)
                    DisplayCoins(); // City Healer

                BText(17, 5, $">{inputText}_");
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
                if (key == "ESC")
                    enterKeyNotPressed = false;
            }

            //TODO: Does this work with RETURN, ESC
            var itemQuantity = Convert.ToInt32(inputText);

            return itemQuantity;
        }

        public static bool KeyPressed () => ReadKey() != "";

        public static void ModuleMessage ( string txt )
        {
            string key;
            do
            {
                ClearShopDisplay();
                CText(txt);
                CyText(9, "( Press SPACE to continue )");
                UpdateDisplay();
                key = GetSingleKey();
            } while (key != "SPACE");
        }

        /// <summary>Generates a random number.</summary>
        /// <param name="low">The lowest number to generate, inclusive.</param>
        /// <param name="high">The highest number to generate, inclusive.</param>
        /// <returns>A number in the given range.</returns>
        public static int Random ( int low, int high ) => s_random.Next(low, high + 1);        

        private static readonly Random s_random = new Random();

        public static string ReadBinaryString ( byte[] buffer, int offset, byte delimiter = 0 )
        {
            var endIndex = Array.IndexOf(buffer, delimiter, offset);
            if (endIndex < 0)
                endIndex = buffer.Length - 1;

            //TODO: Check for off by one
            return Encoding.ASCII.GetString(monstersBinary, offset, endIndex - offset);
        }

        public static string ReadKey ()
        {
            // Process events            
            //while (App.pollEvent(evt))
            //{
            //    if ((evt.Type == EventType.KeyPressed) && (evt.Key.Code == Keyboard.Key.Left))
            //        keyString = "left";
            //    if ((evt.Type == EventType.KeyPressed) && (evt.Key.Code == Keyboard.Key.Right))
            //        keyString = "right";
            //    if ((evt.Type == EventType.KeyPressed) && (evt.Key.Code == Keyboard.Key.Up))
            //        keyString = "up";
            //    if ((evt.Type == EventType.KeyPressed) && (evt.Key.Code == Keyboard.Key.Down))
            //        keyString = "down";

            //    if ((evt.Type == EventType.KeyPressed) && (evt.Key.Code == Keyboard.Key.F1))
            //        keyString = "F1";
            //    if ((evt.Type == EventType.KeyPressed) && (evt.Key.Code == Keyboard.Key.F2))
            //        keyString = "F2";
            //    if ((evt.Type == EventType.KeyPressed) && (evt.Key.Code == Keyboard.Key.F3))
            //        keyString = "F3";
            //    if ((evt.Type == EventType.KeyPressed) && (evt.Key.Code == Keyboard.Key.F4))
            //        keyString = "F4";
            //    if ((evt.Type == EventType.KeyPressed) && (evt.Key.Code == Keyboard.Key.F5))
            //        keyString = "F5";
            //    if ((evt.Type == EventType.KeyPressed) && (evt.Key.Code == Keyboard.Key.F6))
            //        keyString = "F6";
            //    if ((evt.Type == EventType.KeyPressed) && (evt.Key.Code == Keyboard.Key.F7))
            //        keyString = "F7";
            //    if ((evt.Type == EventType.KeyPressed) && (evt.Key.Code == Keyboard.Key.F8))
            //        keyString = "F8";
            //    if ((evt.Type == EventType.KeyPressed) && (evt.Key.Code == Keyboard.Key.F10))
            //        keyString = "F10";
            //    if ((evt.Type == EventType.KeyPressed) && (evt.Key.Code == Keyboard.Key.F11))
            //        keyString = "F11";
            //    if ((evt.Type == EventType.KeyPressed) && (evt.Key.Code == Keyboard.Key.F12))
            //        keyString = "F12";

            //    if ((evt.Type == EventType.KeyPressed) && (evt.Key.Code == Keyboard.Key.Num0))
            //        keyString = "0";
            //    if ((evt.Type == EventType.KeyPressed) && (evt.Key.Code == Keyboard.Key.Comma))
            //        keyString = ",";
            //    if ((evt.Type == EventType.KeyPressed) && (evt.Key.Code == Keyboard.Key.Period))
            //        keyString = ".";
            //    if ((evt.Type == EventType.KeyPressed) && (evt.Key.Code == Keyboard.Key.Num1))
            //        keyString = "1";
            //    if ((evt.Type == EventType.KeyPressed) && (evt.Key.Code == Keyboard.Key.Num2))
            //        keyString = "2";
            //    if ((evt.Type == EventType.KeyPressed) && (evt.Key.Code == Keyboard.Key.Num3))
            //        keyString = "3";
            //    if ((evt.Type == EventType.KeyPressed) && (evt.Key.Code == Keyboard.Key.Num4))
            //        keyString = "4";
            //    if ((evt.Type == EventType.KeyPressed) && (evt.Key.Code == Keyboard.Key.Num5))
            //        keyString = "5";
            //    if ((evt.Type == EventType.KeyPressed) && (evt.Key.Code == Keyboard.Key.Num6))
            //        keyString = "6";
            //    if ((evt.Type == EventType.KeyPressed) && (evt.Key.Code == Keyboard.Key.Num7))
            //        keyString = "7";
            //    if ((evt.Type == EventType.KeyPressed) && (evt.Key.Code == Keyboard.Key.Num8))
            //        keyString = "8";
            //    if ((evt.Type == EventType.KeyPressed) && (evt.Key.Code == Keyboard.Key.Num9))
            //        keyString = "9";

            //    if ((evt.Type == EventType.KeyPressed) && (evt.Key.Code == Keyboard.Key.A))
            //        keyString = "A";
            //    if ((evt.Type == EventType.KeyPressed) && (evt.Key.Code == Keyboard.Key.B))
            //        keyString = "B";
            //    if ((evt.Type == EventType.KeyPressed) && (evt.Key.Code == Keyboard.Key.C))
            //        keyString = "C";
            //    if ((evt.Type == EventType.KeyPressed) && (evt.Key.Code == Keyboard.Key.D))
            //        keyString = "D"; // diag info
            //    if ((evt.Type == EventType.KeyPressed) && (evt.Key.Code == Keyboard.Key.E))
            //        keyString = "E"; // force encounter
            //    if ((evt.Type == EventType.KeyPressed) && (evt.Key.Code == Keyboard.Key.F))
            //        keyString = "F";
            //    if ((evt.Type == EventType.KeyPressed) && (evt.Key.Code == Keyboard.Key.G))
            //        keyString = "G";
            //    if ((evt.Type == EventType.KeyPressed) && (evt.Key.Code == Keyboard.Key.H))
            //        keyString = "H";
            //    if ((evt.Type == EventType.KeyPressed) && (evt.Key.Code == Keyboard.Key.I))
            //        keyString = "I";
            //    if ((evt.Type == EventType.KeyPressed) && (evt.Key.Code == Keyboard.Key.J))
            //        keyString = "J";
            //    if ((evt.Type == EventType.KeyPressed) && (evt.Key.Code == Keyboard.Key.K))
            //        keyString = "K";
            //    if ((evt.Type == EventType.KeyPressed) && (evt.Key.Code == Keyboard.Key.L))
            //        keyString = "L";
            //    if ((evt.Type == EventType.KeyPressed) && (evt.Key.Code == Keyboard.Key.M))
            //        keyString = "M";
            //    if ((evt.Type == EventType.KeyPressed) && (evt.Key.Code == Keyboard.Key.N))
            //        keyString = "N";
            //    if ((evt.Type == EventType.KeyPressed) && (evt.Key.Code == Keyboard.Key.O))
            //        keyString = "O";
            //    if ((evt.Type == EventType.KeyPressed) && (evt.Key.Code == Keyboard.Key.P))
            //        keyString = "P";
            //    if ((evt.Type == EventType.KeyPressed) && (evt.Key.Code == Keyboard.Key.Q))
            //        keyString = "Q";
            //    if ((evt.Type == EventType.KeyPressed) && (evt.Key.Code == Keyboard.Key.R))
            //        keyString = "R";
            //    if ((evt.Type == EventType.KeyPressed) && (evt.Key.Code == Keyboard.Key.S))
            //        keyString = "S";
            //    if ((evt.Type == EventType.KeyPressed) && (evt.Key.Code == Keyboard.Key.T))
            //        keyString = "T";
            //    if ((evt.Type == EventType.KeyPressed) && (evt.Key.Code == Keyboard.Key.U))
            //        keyString = "U";
            //    if ((evt.Type == EventType.KeyPressed) && (evt.Key.Code == Keyboard.Key.V))
            //        keyString = "V";
            //    if ((evt.Type == EventType.KeyPressed) && (evt.Key.Code == Keyboard.Key.W))
            //        keyString = "W";
            //    if ((evt.Type == EventType.KeyPressed) && (evt.Key.Code == Keyboard.Key.X))
            //        keyString = "X";
            //    if ((evt.Type == EventType.KeyPressed) && (evt.Key.Code == Keyboard.Key.Y))
            //        keyString = "Y";
            //    if ((evt.Type == EventType.KeyPressed) && (evt.Key.Code == Keyboard.Key.Z))
            //        keyString = "Z";

            //    if ((evt.Type == EventType.KeyPressed) && (evt.Key.Code == Keyboard.Key.Space))
            //        keyString = "SPACE";
            //    if ((evt.Type == EventType.KeyPressed) && (evt.Key.Code == Keyboard.Key.Enter))
            //        keyString = "RETURN";
            //    if ((evt.Type == EventType.KeyPressed) && (evt.Key.Code == Keyboard.Key.Backspace))
            //        keyString = "BACKSPACE";

            //    // Close window : exit
            //    if (evt.Type == EventType.Closed)
            //        keyString = "QUIT";

            //    // Escape key : exit
            //    if ((evt.Type == EventType.KeyPressed) && (evt.Key.Code == Keyboard.Key.Escape))
            //        keyString = "ESC";
            //}

            //TODO: How to switch from polling to event-based input?
            var keyString = "";
            EventHandler<TextEventArgs> handler = ( o, e ) => {
                //TODO: Change this to juse use raw string value
                switch (e.Unicode)
                {                    
                    case "\r": keyString = "RETURN"; break;
                    case " ": keyString = "SPACE"; break;
                    case "\b": keyString = "BACKSPACE"; break;
                    case "ESC": keyString = "ESC"; break;

                    //TODO: Functions, capital/lowercase letters
                    default: keyString = e.Unicode; break;
                };
            };
            EventHandler closeHandler = ( o, e ) => { keyString = "QUIT"; };
            
            App.TextEntered += handler;
            App.Closed += closeHandler;
            do
            {
                App.WaitAndDispatchEvents();
            } while (String.IsNullOrEmpty(keyString));

            App.TextEntered -= handler;
            App.Closed -= closeHandler;

            return keyString;
        }

        public static void Sleep ( TimeSpan time ) => Thread.Sleep(time);

        //TODO: This method does nothing, sorting rectangular arrays requires a reasonable amount of code and we honestly are going to remove the arrays anyway
        public static void sort ( int[,] values )
        {            
        }

        public static int RollDice ( int rolls, int dice )
        {
            var result = 0;

            for (var r = 0; r < rolls; r++)
                result = result + Random(1, dice);
            if (result == 0)
                Console.Write("\nDice roll 0 error!\n");
            return result;
        }
        
        //TODO: Create format providers for currency, time, percentage, currency in copper, etc
        /// <summary>Formats with thousands separator.</summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static string ToCurrency ( int i ) => i.ToString("N0");        
    }
}