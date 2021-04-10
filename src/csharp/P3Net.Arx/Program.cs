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

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using P3Net.Arx.Graphics;

namespace P3Net.Arx
{
    /// <summary>Represents the main program.</summary>
    public class Program
    {
        public static void Main ( string[] args )
        {
            using (var host = CreateHostBuilder(args).Build())
            {
                var engine = host.Services.GetRequiredService<GameEngine>();
                engine.Run();

                try
                {
                    host.Run();
                } catch (OperationCanceledException)
                { /* Ignore */ };
            };
        }

        #region Private Members

        private static IHostBuilder CreateHostBuilder ( string[] args ) =>  Host.CreateDefaultBuilder(args)
                .ConfigureLogging(ConfigureLogging)
                .ConfigureAppConfiguration(ConfigureConfiguration)
                .ConfigureServices(ConfigureServices);       

        private static void ConfigureConfiguration ( IConfigurationBuilder builder )
        {
            //Default configuration uses: appsettings.json, appsettings.env.json, envvars, cmd line                                               
        }

        private static void ConfigureLogging ( HostBuilderContext context, ILoggingBuilder builder )
        {
            //Default configuration uses - console, debug, EventSource, EventLog            
        }

        private static void ConfigureServices ( HostBuilderContext context, IServiceCollection services )
        {
            services.AddSingleton<GameEngine>();

            //Display            
            services.Configure<DisplaySettings>(context.Configuration.GetSection("display"));
        }
        #endregion
    }
}
