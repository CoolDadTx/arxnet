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
        public static sf.Sprite CharImage = new sf.Sprite();        
        public static sf.Texture FontImage = new sf.Texture();
        
        public static int yBase = 0;

        public static void BText(int x, int y, string text)
        {
            int strlen = text.Length;
            for(var i = 0; i < strlen; ++i)
            {
                char current_char = text[i];
                int char_no = ((int)current_char);
                if(plyr.status == 2)
                    DrawChar(shopConsoleY, x, y, char_no);
                else
                    DrawChar(consoleY, x, y, char_no);
                x++;
            }
        }

        public static void BText(int x, int y, int number)
        {
            string text;
            var @out = new std::stringstream();
                @out << number;
            text = @out.str();

            int strlen = text.Length;
            for(var i = 0; i < strlen; ++i)
            {
                char current_char = text[i];
                int char_no = ((int)current_char);
                if(plyr.status == 2)
                    DrawChar(shopConsoleY, x, y, char_no);
                else
                    DrawChar(consoleY, x, y, char_no);
                x++;
            }
        }

        public static void CText(string str)
        {
            int string_length;
            int current_string_length;
            int x;
            int y;
            int char_count;
            string current_string;
            string current_char;

            string_length = str.Length;
            char_count = 0;
            current_string = "";
            y = 1;
            while(char_count <= string_length)
            {
                current_char = str.Substring(char_count, 1);

                if((char_count == string_length) || (current_char == "@"))
                {
                    current_string_length = current_string.Length;
                    x = ((40 - current_string_length) / 2) + 1;

                    DrawText(0, x, y, current_string);

                    y++;
                    char_count++;
                    current_string = "";
                } else
                {
                    current_string = current_string + current_char;
                    char_count++;
                }
            }
        }

        public static void CyText(int y, string str)
        {
            int string_length;
            int current_string_length;
            int x;
            int char_count;
            string current_string;
            string current_char;

            string_length = str.Length;
            char_count = 0;
            current_string = "";

            while(char_count <= string_length)
            {
                current_char = str.Substring(char_count, 1);

                if((char_count == string_length) || (current_char == "@"))
                {
                    current_string_length = current_string.Length;
                    x = ((40 - current_string_length) / 2) + 1;

                    DrawText(consoleY, x, y, current_string);

                    y++;
                    char_count++;
                    current_string = "";
                } else
                {
                    current_string = current_string + current_char;
                    char_count++;
                }
            }
        }

        public static void DrawChar(int topY, int x, int y, int initchar_no)
        {
            int char_no;
            char_no = initchar_no - 32;

            int row;
            int column;
            var charsPerRow = 16; // number of chars per row in font image containing all tiles (16 default)

            //Select 16x16 section of tile sheet for tile

            if(char_no > charsPerRow)
            {
                column = (char_no % charsPerRow); // remainder
                row = ((char_no - column) / charsPerRow);
            } else
            {
                column = char_no;
                row = 0; // = row 1 on the actual font sheet at y=0
            }

            int charX = (column) * 16; // x loc on tiles image in pixels
            int charY = ((row) * 16); // y loc on tiles image in pixels
            
            CharImage.scale(1.0f, 1.0f);

            CharImage.setTextureRect(sf.IntRect(charX, charY, 16, 16));
            
            if(char_no == 16)
                CharImage.setTextureRect(sf.IntRect(0, 16, 16, 16));
            
            CharImage.setPosition(consoleX + ((x - 1) * 16), topY + (y * 18));

			App.draw(CharImage);
        }

        public static void DrawText(int x, int y, string text)
        {
            int strlen = text.Length;
            for(var i = 0; i < strlen; ++i)
            {
                char current_char = text[i];
                int char_no = ((int)current_char);
                if(plyr.status == 2)
                    DrawChar(shopStatsY, x, y, char_no);
                else
                    DrawChar(statPanelY, x, y, char_no);
                x++;
            }
        }

        public static void DrawText(int x, int y, int number)
        {
            string text;
            var @out = new std::stringstream();
                @out << number;
            text = @out.str();

            int strlen = text.Length;
            for(var i = 0; i < strlen; ++i)
            {
                char current_char = text[i];
                int char_no = ((int)current_char);

                if(plyr.status == 2)
                    DrawChar(shopStatsY, x, y, char_no);
                else
                    DrawChar(statPanelY, x, y, char_no);
                x++;
            }
        }

        public static void DrawText(int area, int x, int y, string text)
        {
            int strlen = text.Length;
            for(var i = 0; i < strlen; ++i)
            {
                char current_char = text[i];
                int char_no = ((int)current_char);

                if(plyr.status == 2)
                    DrawChar(shopConsoleY, x, y, char_no);
                else
                    DrawChar(consoleY, x, y, char_no); // was 16
                x++;
            }
        }

        public static void InitFont()
        {
            if(plyr.fontStyle == 0)
                FontImage.loadFromFile("data/images/arfontSmooth.png");
            if(plyr.fontStyle == 1)
                FontImage.loadFromFile("data/images/arfont.png");
            CharImage.setTexture(FontImage);
        }

        public static void SetFontColour(int r, int g, int b, int a) => CharImage.setColor(sf.Color(r, g, b, a)) ;

        /* Seem to only be used in item.h */
        public static void Text(int x, int y, string text)
        {
            int strlen = text.Length;
            for(var i = 0; i < strlen; ++i)
            {
                char current_char = text[i];
                int char_no = ((int)current_char);

                DrawChar(16, x, y, char_no); // was 16
                x++;
            }
        }

        public static void Text(int x, int y, int number)
        {
            string text;
            var @out = new std::stringstream();
                @out << number;
            text = @out.str();

            int strlen = text.Length;
            for(var i = 0; i < strlen; ++i)
            {
                char current_char = text[i];
                int char_no = ((int)current_char);

                DrawChar(16, x, y, char_no); // was 0
                x++;
            }
        }

        //extern int consoleY; // y position for displaying the bottom screen info panel (in pixels???)
        //extern int consoleX; // x starting position for displaying the panel for centering
        //extern int statPanelY; // Used as top of Y for Stats panel
        //extern int shopConsoleY;
        //extern int shopStatsY;
        //extern int charYBase;
    }
}