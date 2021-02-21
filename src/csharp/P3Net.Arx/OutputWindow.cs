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
    //TODO: Should this be combined with the actual Graphics window or an interface?
    /// <summary>Manages the output window.</summary>
    public class OutputWindow
    {
        private OutputWindow ()
        {
        }

        public static OutputWindow Instance = new OutputWindow();

        public void Clear ()
        {
            // Sets all console message slots to empty
            for (var i = 0; i < _consoleMessages.Length; ++i)
                _consoleMessages[i] = "NO MESSAGE";
        }

        public string GetMessage ()
        {
            // TODO: Should this just dequeue the next message?
            return _consoleMessages[0];
        }

        public void RemoveMessage ()
        {
            // Moves messages along so index [0] contains next message to be printed (if any).            
            for (var i = 0; i < _consoleMessages.Length; ++i)
            {
                if (i == _consoleMessages.Length - 1)
                    _consoleMessages[i] = "NO MESSAGE";
                else
                    _consoleMessages[i] = _consoleMessages[i + 1];
            };
        }

        public bool Write ( string message )
        {
            for (var index = 0; index < _consoleMessages.Length; ++index)
            {
                if (_consoleMessages[index] == "NO MESSAGE")
                {
                    _consoleMessages[index] = message;                    
                    return true;
                };
            };

            // Will currently discard the message
            Console.WriteLine("ERROR: Console messages maximum exceeded!");
            return false;
        }

        //TODO: The usage seems to be as a queue so either make Queue<string> or use a fixed length buffer
        //with a a rotating "current" indicator
        private string[] _consoleMessages = new string[10];
    }
}
