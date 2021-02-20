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
using System.IO;

using SFML.Graphics;
using SFML.System;

namespace P3Net.Arx
{
    public static partial class GlobalMembers
    {
        public static void InitLyricFont ()
        {
            lyricFontImage = new Texture("data/images/songFont.png");
            lyricCharImage.Texture = lyricFontImage;
        }

        /// <summary>Loads lyrics from a file.</summary>
        /// <param name="filename">The file containing the lyrics.</param>
        /// <remarks>
        /// A thick function that takes as input the lyrics (stored in .txt files at either one of two locations, 
        /// depending on if we're using the 'modern' or 'classic' soundtrack), which comprise one of two elements: an integer (X,
        /// which is used as either an on-screen position indicator, a delay, or a color value) and a string, which itself
        /// either indicates a lyric or a color.
        /// </remarks>
        public static void LoadLyrics ( string filename )
        {
            lyricPointer = 0; // *** reset for a new set of lyrics
            lyricDuration = 0; // *** given value only if the lyricElement's current 'x' indicates a delay
            sequenceLength = 0;
            foreText = "";
            backText = "";
            var i = 0; // *** index used to fill array as sequence data is loaded

            var lyricsFilename = $"data/audio/{filename}";

            if (plyr.musicStyle) // *** selects the 'modern' soundtrack
                lyricsFilename = $"data/audio/B/{filename}";

            using (var reader = new StreamReader(lyricsFilename))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine(); // *** read first line as blank
                    var idx = line.IndexOf(','); // *** yields the index in our LINE of the ','.

                    if (idx == -1) // *** no comma; assume duration value or colour change
                    {
                        lyrics[i].x = Convert.ToInt32(line);
                        lyrics[i].text = "ERROR!";

                        if (line == "CYAN")
                        {
                            lyrics[i].x = 150000;
                            lyrics[i].text = "COLOUR!";
                        }

                        if (line == "BLUE")
                        {
                            lyrics[i].x = 150001;
                            lyrics[i].text = "COLOUR!";
                        }

                        if (line == "GREEN")
                        {
                            lyrics[i].x = 150002;
                            lyrics[i].text = "COLOUR!";
                        }

                        if (line == "WHITE")
                        {
                            lyrics[i].x = 150003;
                            lyrics[i].text = "COLOUR!";
                        }
                    } else
                    {
                        var left = line.Substring(0, idx);
                        var right = line.Substring(idx + 1);
                        lyrics[i].x = Convert.ToInt32(left);
                        lyrics[i].text = right;
                    }

                    sequenceLength++;
                    i++;
                }
            };

            sequenceLength--;
            backText = "";
            foreText = "";
            wipe = false;
            iCounter = 0;
        }

        /// <summary>Interprets the lyricElement structure and updates accordingly.</summary>
        /// <remarks>
        /// Assigns a 'lyricDuration' value on the basis of a clock that is started whenever 'updateLyrics()' is called, 
        /// which measures the elapsed time between the call and its receipt; this adjusts the lyric duration so that slower
        /// computers will see a reduction in the time it takes to update the on-screen lyrics, accounting for the longer
        /// time spent in processing.
        /// </remarks>
        public static void UpdateLyrics ()
        {
            if (lyricDuration < 0)
                lyricDuration = 0;
            if (lyricPointer < sequenceLength)
            {
                if (lyricDuration > 0)
                {
                    if (backInk == 1)
                        LyricColor(59, 83, 255, 255);
                    if (backInk == 2)
                        LyricColor(54, 127, 40, 255);
                    Lyric(backx, backText);
                    if (ink == 3)
                        LyricColor(161, 238, 255, 255);
                    if (ink == 4)
                        LyricColor(192, 192, 192, 255);
                    Lyric(x, foreText);

                    Sleep(TimeSpan.FromMilliseconds(100));
                    lyricDuration -= 100;

                    if (lyricDuration <= 0)
                    {
                        wipe = true;
                        lyricDuration = 0;
                    }
                }
                if (wipe)
                {
                    foreText = "";
                    wipe = false;
                }

                //----------------------------------------------------------------------
                //                              COLOR
                //  The following 'if' statement takes care of our color. If the
                //  'lyrics' structure's X value at [n] is greater than 150,000, this
                //  means that X indicates a color value at this index.
                //----------------------------------------------------------------------
                if ((lyrics[lyricPointer].x > 14999) && (lyricDuration == 0))
                {
                    if (lyrics[lyricPointer].x == 150000)
                        ink = 3;
                    if (lyrics[lyricPointer].x == 150001)
                    {
                        ink = 1;
                        backInk = 1;
                        backText = "";
                    }
                    if (lyrics[lyricPointer].x == 150002)
                    {
                        ink = 2;
                        backInk = 2;
                        backText = "";
                    }
                    if (lyrics[lyricPointer].x == 150003)
                        ink = 4;
                    lyricPointer++;
                }

                //----------------------------------------------------------------------
                //                          ON-SCREEN POSITION
                //  The following 'if' statement takes care of the variable X
                //  when it indicates an on-screen position (its value will always
                //  be less than 20 if this is the case.)
                //----------------------------------------------------------------------
                if ((lyrics[lyricPointer].x < 20) && (lyricDuration == 0))
                {
                    foreText = "";

                    if ((ink == 1) || (ink == 2))
                    {
                        backx = lyrics[lyricPointer].x;
                        backText = lyrics[lyricPointer].text;
                    }

                    if ((ink == 3) || (ink == 4))
                    {
                        x = lyrics[lyricPointer].x;
                        foreText = lyrics[lyricPointer].text;
                    }

                    if (backInk == 1)
                        LyricColor(59, 83, 255, 255);
                    if (backInk == 2)
                        LyricColor(54, 127, 40, 255);
                    Lyric(backx, backText);

                    if (backText != "")
                    {
                        if (ink == 3)
                            LyricColor(161, 238, 255, 255);
                        if (ink == 4)
                            LyricColor(192, 192, 192, 255);
                        Lyric(x, foreText);
                    }

                    lyricPointer++;
                }

                //----------------------------------------------------------------------
                //                              DELAY
                //  The following 'if' statement takes care of our delay. It calculates
                //  the elapsed time since the last call, and produces a ratio by which
                //  to multiply the resulting lyricDuration so that differences in
                //  processor speeds are accounted for.
                //----------------------------------------------------------------------
                if ((lyrics[lyricPointer].x > 20) && (lyrics[lyricPointer].x < 150000) && (lyricDuration == 0))
                {
                    var timeSinceLast = clock1.ElapsedTime;
                    var timeGiven = Time.FromMilliseconds(lyrics[lyricPointer].x);
                    var fSinceLast = timeSinceLast.AsSeconds();
                    var fGiven = timeGiven.AsSeconds();
                    var fRatio = (fSinceLast / fGiven);
                    if ((1.0 - fRatio) > 0.50)
                        fSpeedCoefficient = 1.0F;
                    else if ((1.0 - fRatio) < 0)
                        fSpeedCoefficient = 0.1F;
                    else
                        fSpeedCoefficient = (1.0F - fRatio);

                    var fConverted = (lyrics[lyricPointer].x * (fSpeedCoefficient));
                    lyricDuration = (int)fConverted;
                    lyricPointer++;
                }
            }
        }

        #region Review Data

        public static int backInk;
        public static string backText;
        public static int backx;

        //TODO: Use regular time
        public static Clock clock1 = new Clock();
        public static string foreText;
        public static float fSpeedCoefficient = 0F;
        public static int iCounter;
        public static int ink;
        public static Sprite lyricCharImage = new Sprite();
        public static int lyricDuration;

        public static Texture lyricFontImage;
        public static int lyricPointer;

        public static LyricElement[] lyrics = Arrays.InitializeWithDefaultInstances<LyricElement>(2048);
        public static int sequenceLength;

        public static bool wipe;
        public static int x;

        #endregion

        #region Private Members

        /// <summary>?</summary>
        /// <param name="x">Character's current X-value</param>
        /// <param name="initchar_no">ASCII value derived in the lyric function</param>
        private static void DrawLyricChar ( int x, int initchar_no )
        {
            var char_no = initchar_no - 64;
            if (initchar_no == 39)
                char_no = 29;
            if (initchar_no == 32)
                char_no = 0;
            if (initchar_no == 33)
                char_no = 27;
            if (initchar_no == 63)
                char_no = 28;
            if (initchar_no == 45)
                char_no = 30;
            if (initchar_no == 46)
                char_no = 31;
            int charX = char_no * 32;

            lyricCharImage.TextureRect = new IntRect(charX, 0, 32, 16);
            lyricCharImage.Position = new Vector2f(lyricX + ((x - 1) * 32), lyricY);
            App.Draw(lyricCharImage);
        }

        /// <summary>Calls the 'drawLyricChar' function.</summary>
        /// <param name="x">Number of functions.</param>
        /// <param name="text">ASCII value of the current letter in the string.</param>        
        private static void Lyric ( int x, string text )
        {
            for (var i = 0; i < text.Length; ++i)
            {
                var current_char = text[i];
                int char_no = current_char;

                DrawLyricChar(x, char_no);
                x++;
            }
        }

        private static void LyricColor ( int r, int g, int b, int a ) => lyricCharImage.Color = new Color((byte)r, (byte)g, (byte)b, (byte)a);
        
        #endregion
    }
}