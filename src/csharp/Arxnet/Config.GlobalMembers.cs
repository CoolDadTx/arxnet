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
using System.Drawing;
using System.IO;

namespace P3Net.Arx
{
    public static partial class GlobalMembers
    {
        // Load settings from an arx.ini configuration file		
        public static bool LoadConfig ()
        {
            using (var reader = new StreamReader("arx.ini"))
            {
                // read first line as blank
                reader.ReadLine();

                var iniSettings = 4; // number of settings in the ini file

                var windowSize = new Size();

                for (var a = 0; a < iniSettings; ++a) // number of settings in the ini file
                {
                    var line = reader.ReadLine();

                    var idx = line.IndexOf('=');
                    var text = line.Substring(idx + 2);

                    if (a == 0)
                        windowMode = (WindowMode)Convert.ToInt32(text);
                    if (a == 1)
                        graphicMode = (DisplayOptions)Convert.ToInt32(text);
                    if (a == 2)
                        windowSize.Width = Convert.ToInt32(text);
                    if (a == 3)
                        windowSize.Height = Convert.ToInt32(text);
                };

                WindowSize = windowSize;
            };

            // Minimum window requirement is currently 640 x 480 pixels
            if (WindowSize.Width < 640 || WindowSize.Height < 480)
            {
                Console.Write("WARNING: A minimum window size of 640 x 480 pixels is required.");
                Console.Write("\n");
                Console.Write("\n");
                WindowSize = new System.Drawing.Size(640, 480);
            }
            return true;
        }
    }
}