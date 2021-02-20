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
    public static partial class GlobalMembers
    {
        #region Review Coins 

        //TODO: Move to Currency
        public static bool CheckCoins ( int gold, int silver, int copper )
        {
            var itemCostInCoppers = (gold * 100) + (silver * 10) + copper;
            var playerTotalCoppers = (plyr.gold * 100) + (plyr.silver * 10) + plyr.copper;

            return itemCostInCoppers <= playerTotalCoppers;
        }

        //TODO: Move to Currency
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

        //TODO: Move to Currency
        public static void DisplayCoins ()
        {
            var coinsCopper = (plyr.gold * 100) + (plyr.silver * 10) + plyr.copper;

            CyText(9, $"Your coins in copper {ToCurrency(coinsCopper)}");
        }

        //TODO: Move to Currency formatter
        public static void DisplaySilverCoins ()
        {
            var coinsSilver = (plyr.gold * 10) + plyr.silver + (plyr.copper / 10);

            CyText(9, $"Your coins in silver {ToCurrency(coinsSilver)}");
        }

        //TODO: Create format providers for currency, time, percentage, currency in copper, etc
        /// <summary>Formats with thousands separator.</summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static string ToCurrency ( int i ) => i.ToString("N0");
        #endregion

        #region Review Input

        //TODO: Move to input

        public static string GetSingleKey () => ReadKey();

        //TODO: Move to input
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

        //TODO: Move to input
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

        //TODO: Move to input
        public static bool KeyPressed () => ReadKey() != "";
        
        //TODO: Return virtual key instead        
        // misc.cpp: 121: readKey()
        public static string ReadKey ()
        {
            var keyString = "";
            var evt = App.PollEvents();
            while (evt != null && !App.IsWindowClosing)                
            {
                if (evt is KeyEventArgs keyPress)
                {
                    keyString = keyPress.KeyString();
                };

                evt = App.PollEvents();
            };

            //TODO: Figure out better way of reporting window close
            if (App.IsWindowClosing)
                return "QUIT";

            return keyString;
        }

        #endregion

        public static int Hex2Dec ( string s ) => Int32.Parse(s, NumberStyles.HexNumber);

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
        
        public static string ReadBinaryString ( byte[] buffer, int offset, byte delimiter = 0 )
        {
            var endIndex = Array.IndexOf(buffer, delimiter, offset);
            if (endIndex < 0)
                endIndex = buffer.Length - 1;

            //TODO: Check for off by one
            return Encoding.ASCII.GetString(monstersBinary, offset, endIndex - offset);
        }
        
        public static void Sleep ( TimeSpan time ) => Thread.Sleep(time);

        //TODO: This method does nothing, sorting rectangular arrays requires a reasonable amount of code and we honestly are going to remove the arrays anyway
        public static void sort ( int[,] values )
        {            
        }

        //TODO: Move to helper Dice class
        public static int RollDice ( int rolls, int dice )
        {
            var result = 0;

            for (var r = 0; r < rolls; r++)
                result += Random(1, dice);
            if (result == 0)
                Console.Write("\nDice roll 0 error!\n");
            return result;
        }                

        #region Review Data

        //TODO: Move to configuration settings
        public static DevSettings AR_DEV = new DevSettings();

        #endregion

        #region Private Members

        private static readonly Random s_random = new Random();
        #endregion
    }
}