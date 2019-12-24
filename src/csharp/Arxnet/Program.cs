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
    /// <summary>Represents the main program.</summary>
    public static class Program
    {
        public static void Main ()
        {
            //TODO: Init game layer
            if (!GlobalMembers.LoadConfig())
                return; // load screen resolution from arx.ini

            GlobalMembers.CreateGameWindow();

            GlobalMembers.DispInit();
            GlobalMembers.InitFont();
            GlobalMembers.LoadLogoImage();

            GlobalMembers.InitSaveGameDescriptions();

            //TODO: Move to menu handler class
            var done = false;
            while (!done)
            {
                GlobalMembers.ClearDisplay();
                GlobalMembers.DisplayMainMenu();
                GlobalMembers.UpdateDisplay();

                switch (GlobalMembers.GetSingleKey())
                {
                    case "1": GlobalMembers.CreateCityCharacter(); break;
                    case "2": GlobalMembers.CreateDungeonCharacter(); break;
                    case "3": GlobalMembers.LoadCharacter(); break;
                    case "4": GlobalMembers.DisplayAcknowledgements(); break;
                    case "6": GlobalMembers.ToggleMusic(); break;
                    case "7": GlobalMembers.ToggleAndInitializeFont(); break;
                    
                    case "0":
                    case "QUIT": done = true; break;
                };
            };
        }
    }
}
