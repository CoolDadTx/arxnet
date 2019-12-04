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
using System.Collections.Generic;
using System.Linq;

namespace P3Net.Arx
{
    public partial class GlobalMembers
    {
        // Load settings from an arx.ini configuration file		
        public static bool LoadConfig ()
        {
            std::ifstream instream = new std::ifstream();
            string filename = "arx.ini";
            instream.open(filename);
            if (instream == null)
            {
                cerr << "Error: arx.ini could not be loaded" << "\n";
                return false;
            }
            string junk;
            string line;
            string text;
            int iniSettings = 4; // number of settings in the ini file
            string.size_type idx = new string.size_type();
            getline(instream, junk); // read first line as blank

            for (int a = 0; a < iniSettings; ++a) // number of settings in the ini file
            {
                getline(instream, line);

                idx = line.IndexOf('=');
                text = line.Substring(idx + 2);

                if (a == 0)
                    windowMode = Convert.ToInt32(text);
                if (a == 1)
                    graphicMode = Convert.ToInt32(text);
                if (a == 2)
                    windowWidth = Convert.ToInt32(text);
                if (a == 3)
                    windowHeight = Convert.ToInt32(text);
            }
            instream.close();

            // Minimum window requirement is currently 640 x 480 pixels
            if ((windowWidth < 640) || (windowHeight < 480))
            {
                Console.Write("WARNING: A minimum window size of 640 x 480 pixels is required.");
                Console.Write("\n");
                Console.Write("\n");
                windowWidth = 640;
                windowHeight = 480;
            }
            return true;
        }

        //extern int windowMode, graphicMode, windowWidth, windowHeight;

        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //bool LoadConfig();
    }
}