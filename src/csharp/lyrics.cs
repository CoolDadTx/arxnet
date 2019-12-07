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
    public class LyricElement
    {
        public string text { get; set; }

        public int x { get; set; }
    }

    public partial class GlobalMembers
    {
        public static int backInk;
        public static string backText;
        public static int backx;

        public static sf.Clock clock1 = new sf.Clock();
        public static string foreText;
        public static float fSpeedCoefficient = 0F;
        public static int iCounter;
        public static int ink;
        public static sf.Sprite lyricCharImage = new sf.Sprite();
        public static int lyricDuration;

        public static sf.Texture lyricFontImage = new sf.Texture();
        public static int lyricPointer;

        public static LyricElement[] lyrics = Arrays.InitializeWithDefaultInstances<LyricElement>(2048);
        public static int sequenceLength;

        public static bool wipe;
        public static int x;

        //============================== drawLyricChar =================================
        //
        //  Accepts as parameters an upper-y position (which is always 0), the
        //  current character's x-value (an index which is incremented...), and
        //  INITCHAR_NO, an ASCII value derived in the 'lyric' function.
        //                                                      *** CALLED BY 'LYRIC'
        //------------------------------------------------------------------------------
        public static void DrawLyricChar ( int x, int initchar_no )
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

            lyricCharImage.setTextureRect(sf.IntRect(charX, 0, 32, 16));
            lyricCharImage.setPosition(lyricX + ((x - 1) * 32), lyricY);
            App.draw(lyricCharImage);
        }

        public static void InitLyricFont ()
        {
            lyricFontImage.loadFromFile("data/images/songFont.png");
            lyricCharImage.setTexture(lyricFontImage);
        }

        //=============================== loadLyrics ===================================
        //
        //  A thick function that takes as input the lyrics (stored in .txt files at
        //  either one of two locations, depending on if we're using the 'modern' or
        //  'classic' soundtrack), which comprise one of two elements: an integer (X,
        //  which is used as either an on-screen position indicator, a delay, or a
        //  color value) and a string, which itself either indicates a lyric or a
        //  color.
        //------------------------------------------------------------------------------
        public static void LoadLyrics ( string filename )
        {
            lyricPointer = 0; // *** reset for a new set of lyrics
            lyricDuration = 0; // *** given value only if the lyricElement's current 'x' indicates a delay
            sequenceLength = 0;
            foreText = "";
            backText = "";
            var i = 0; // *** index used to fill array as sequence data is loaded

            var lyricsFilename = $"data/audio/{filename}";

            var instream = new ifstream();

            if (plyr.musicStyle == 1) // *** selects the 'modern' soundtrack
                lyricsFilename = $"data/audio/B/{filename}";

            instream.open(lyricsFilename);

            if (instream == null) // *** obligatory mis-load feedback
                cerr << "Error: lyrics file could not be loaded" << "\n";

            var line = "";
            while (line != "EOF")
            {
                getline(instream, line); // *** read first line as blank
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
            instream.close();
            sequenceLength--;
            backText = "";
            foreText = "";
            wipe = false;
            iCounter = 0;
        }

        //================================== lyric =====================================
        //
        //  Calls the 'drawLyricChar' function, passing it 0 (the upper-leftmost
        //  y-component), X (a value passed to 'lyric' that serves a number of
        //  functions [explained below]), and CHAR_NO (the ASCII value of the current
        //  letter in the string).
        //                                              *** CALLED BY 'UPDATE LYRICS'
        //------------------------------------------------------------------------------
        public static void Lyric ( int x, string text )
        {
            for (var i = 0; i < text.Length; ++i)
            {
                var current_char = text[i];
                int char_no = current_char;

                DrawLyricChar(x, char_no);
                x++;
            }
        }

        public static void LyricColour ( int r, int g, int b, int a ) => lyricCharImage.setColor(sf.Color(r, g, b, a));

        //=============================== updateLyrics =================================
        //  Interprets the lyricElement structure and updates accordingly. Assigns a
        //  'lyricDuration' value on the basis of a clock that is started whenever
        //  'updateLyrics()' is called, which measures the elapsed time between the
        //  call and its receipt; this adjusts the lyric duration so that slower
        //  computers will see a reduction in the time it takes to update the on-screen
        //  lyrics, accounting for the longer time spent in processing.
        //                                               *** CALLED BY 'TAVERN.CPP,'
        //                                                  'SMITHY.CPP,' 'SHOP.CPP'
        //------------------------------------------------------------------------------
        public static void UpdateLyrics ()
        {
            if (lyricDuration < 0)
                lyricDuration = 0;
            if (lyricPointer < sequenceLength)
            {
                if (lyricDuration > 0)
                {
                    if (backInk == 1)
                        LyricColour(59, 83, 255, 255);
                    if (backInk == 2)
                        LyricColour(54, 127, 40, 255);
                    Lyric(backx, backText);
                    if (ink == 3)
                        LyricColour(161, 238, 255, 255);
                    if (ink == 4)
                        LyricColour(192, 192, 192, 255);
                    Lyric(x, foreText);

                    sf.sleep(sf.milliseconds(100));
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
                        LyricColour(59, 83, 255, 255);
                    if (backInk == 2)
                        LyricColour(54, 127, 40, 255);
                    Lyric(backx, backText);

                    if (backText != "")
                    {
                        if (ink == 3)
                            LyricColour(161, 238, 255, 255);
                        if (ink == 4)
                            LyricColour(192, 192, 192, 255);
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
                    var timeSinceLast = clock1.getElapsedTime();
                    var timeGiven = sf.milliseconds(lyrics[lyricPointer].x);
                    var fSinceLast = timeSinceLast.asSeconds();
                    var fGiven = timeGiven.asSeconds();
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

        //extern int lyricX;
        //extern int lyricY;
        //extern Player plyr;
        //extern sf::Clock clock1;
        //extern sf::RenderWindow App;
        //extern sf::RenderTexture lyricstexture;
        //extern int charYBase;
    }
}