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

using SFML.Graphics;
using SFML.System;

namespace P3Net.Arx
{
    public static partial class GlobalMembers
    {
        public static void InitFont ()
        {
            if (plyr.fontStyle == 0)
                FontImage = new Texture("data/images/arfontSmooth.png");
            if (plyr.fontStyle == 1)
                FontImage = new Texture("data/images/arfont.png");
            CharImage.Texture = FontImage;
        }

        public static void BText ( int x, int y, string text )
        {
            for (var i = 0; i < text.Length; ++i)
            {
                var current_char = text[i];
                var char_no = ((int)current_char);
                if (plyr.status == GameStates.Module)
                    DrawChar(shopConsoleY, x, y, char_no);
                else
                    DrawChar(consoleY, x, y, char_no);
                x++;
            }
        }

        public static void BText ( int x, int y, int number )
        {
            var text = number.ToString();

            for (var i = 0; i < text.Length; ++i)
            {
                var current_char = text[i];
                var char_no = ((int)current_char);
                if (plyr.status == GameStates.Module)
                    DrawChar(shopConsoleY, x, y, char_no);
                else
                    DrawChar(consoleY, x, y, char_no);
                x++;
            }
        }

        public static void CText ( string str )
        {
            var char_count = 0;
            var current_string = "";
            var y = 1;
            while (char_count <= str.Length)
            {
                var current_char = str.Substring(char_count, 1);

                if ((char_count == str.Length) || (current_char == "@"))
                {
                    var current_string_length = current_string.Length;
                    var x = ((40 - current_string_length) / 2) + 1;

                    DrawText(0, x, y, current_string);

                    y++;
                    char_count++;
                    current_string = "";
                } else
                {
                    current_string += current_char;
                    char_count++;
                }
            }
        }

        public static void CyText ( int y, string str )
        {
            var char_count = 0;
            var current_string = "";

            while (char_count <= str.Length)
            {
                var current_char = str.Substring(char_count, 1);

                if ((char_count == str.Length) || (current_char == "@"))
                {
                    var current_string_length = current_string.Length;
                    var x = ((40 - current_string_length) / 2) + 1;

                    DrawText(consoleY, x, y, current_string);

                    y++;
                    char_count++;
                    current_string = "";
                } else
                {
                    current_string += current_char;
                    char_count++;
                }
            }
        }

        //TODO: Identical to version that doesn't accept area other than the first parameter to DrawChar, that should be the argument
        public static void DrawText ( int x, int y, string text )
        {
            for (var i = 0; i < text.Length; ++i)
            {
                var current_char = text[i];
                var char_no = ((int)current_char);
                if (plyr.status == GameStates.Module)
                    DrawChar(shopStatsY, x, y, char_no);
                else
                    DrawChar(statPanelY, x, y, char_no);
                x++;
            }
        }        

        public static void DrawText ( int x, int y, int number )
        {
            var text = number.ToString();

            for (var i = 0; i < text.Length; ++i)
            {
                var current_char = text[i];
                var char_no = ((int)current_char);

                if (plyr.status == GameStates.Module)
                    DrawChar(shopStatsY, x, y, char_no);
                else
                    DrawChar(statPanelY, x, y, char_no);
                x++;
            }
        }

        public static void SetFontColor ( int r, int g, int b, int a ) => CharImage.Color = new Color((byte)r, (byte)g, (byte)b, (byte)a);

        //TODO: Only used in Items??
        public static void Text ( int x, int y, string text )
        {
            for (var i = 0; i < text.Length; ++i)
            {
                var current_char = text[i];
                var char_no = ((int)current_char);

                DrawChar(16, x, y, char_no); // was 16
                x++;
            }
        }

        //TODO: Only used in Items??
        public static void Text ( int x, int y, int number )
        {
            var text = number.ToString();

            for (var i = 0; i < text.Length; ++i)
            {
                var current_char = text[i];
                var char_no = ((int)current_char);

                DrawChar(16, x, y, char_no); // was 0
                x++;
            }
        }

        #region Private Members

        private static void DrawChar ( int topY, int x, int y, int initchar_no )
        {
            var char_no = initchar_no - 32;

            int row;
            int column;
            var charsPerRow = 16; // number of chars per row in font image containing all tiles (16 default)

            //Select 16x16 section of tile sheet for tile

            if (char_no > charsPerRow)
            {
                column = (char_no % charsPerRow); // remainder
                row = ((char_no - column) / charsPerRow);
            } else
            {
                column = char_no;
                row = 0; // = row 1 on the actual font sheet at y=0
            }

            var charX = (column) * 16; // x loc on tiles image in pixels
            var charY = ((row) * 16); // y loc on tiles image in pixels

            CharImage.Scale = new Vector2f(1.0f, 1.0f);

            CharImage.TextureRect = new IntRect(charX, charY, 16, 16);

            if (char_no == 16)
                CharImage.TextureRect = new IntRect(0, 16, 16, 16);

            CharImage.Position = new Vector2f(consoleX + ((x - 1) * 16), topY + (y * 18));

            App.Draw(CharImage);
        }

        //TODO: Identical to version that doesn't accept area other than the first parameter to DrawChar, that should be the argument
#pragma warning disable IDE0060  //Unused parameter        
        private static void DrawText ( int area, int x, int y, string text )
        {
            for (var i = 0; i < text.Length; ++i)
            {
                var current_char = text[i];
                var char_no = ((int)current_char);

                if (plyr.status == GameStates.Module)
                    DrawChar(shopConsoleY, x, y, char_no);
                else
                    DrawChar(consoleY, x, y, char_no);
                x++;
            }
        }
#pragma warning restore IDE0060  //Unused parameter
        #endregion

        #region Review Data

        public static Sprite CharImage = new Sprite();
        public static Texture FontImage;

        public static int yBase = 0;

        #endregion
    }
}