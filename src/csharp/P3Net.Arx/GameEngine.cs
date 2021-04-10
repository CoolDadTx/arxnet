/*
 * Copyright © Michael Taylor (P3Net)
 * All Rights Reserved
 *
 * http://www.michaeltaylorp3.net
 */
using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

using P3Net.Arx.Graphics;

namespace P3Net.Arx
{
    public class GameEngine
    {
        #region Construction

        public GameEngine ( IHostApplicationLifetime hostLifetime, IOptions<DisplaySettings> displaySettings )
        {
            _hostLifetime = hostLifetime;
            _displaySettings = displaySettings;
        }
        #endregion

        public void Run ()
        {
            try
            {
                Initialize();
                RunCore();                
            } finally
            {
                _hostLifetime.StopApplication();
            };
        }

        #region Private Members

        private void Initialize ()
        {
            //TODO: Move to DI 
            GlobalMembers.LoadConfig(_displaySettings.Value);

            GlobalMembers.CreateGameWindow();

            GlobalMembers.DispInit();
            GlobalMembers.InitFont();
            GlobalMembers.LoadLogoImage();

            GlobalMembers.InitSaveGameDescriptions();
        }

        private void RunCore ()
        {
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

                    case "0": done = GlobalMembers.ConfirmQuit(); break;
                    case "QUIT": done = true; break;
                };
            };
        }

        private readonly IHostApplicationLifetime _hostLifetime;
        private readonly IOptions<DisplaySettings> _displaySettings;
        #endregion
    }
}
