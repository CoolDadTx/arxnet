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
using Drawing = System.Drawing;

using SFML.Graphics;
using SFML.System;
using SFML.Window;

using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;

using Arxnet.OpenTK.Compatibility;

namespace P3Net.Arx
{
    public static partial class GlobalMembers
    {
        public static void CreateGameWindow ()
        {
            var title = "Alternate Reality X " + version;

            var styles = windowMode == WindowMode.Window ? Styles.Close : Styles.Fullscreen;

            App = new InputRenderWindow(new VideoMode((uint)WindowSize.Width, (uint)WindowSize.Height), title, styles);

            // Print OpenGL settings to game console for information
            var settings = App.Settings;
            Console.WriteLine($"Welcome to Alternate Reality X {version}");
            Console.WriteLine(" ...");
            Console.WriteLine();
            Console.WriteLine("OpenGL Settings:");
            Console.WriteLine();
            Console.WriteLine($"Depth bits:           {settings.DepthBits}");
            Console.WriteLine($"Stencil bits:         {settings.StencilBits}");
            Console.WriteLine($"Anti-aliasing level:  {settings.AntialiasingLevel}");
            Console.WriteLine($"OpenGL Version:       {settings.MajorVersion}{settings.MinorVersion}");
            Console.WriteLine();
            Console.WriteLine($"Window Size:          {WindowSize.Width} x {WindowSize.Height}");
            Console.WriteLine();
            Console.WriteLine();

            // Limit the framerate to 60 frames per second (this step is optional)
            App.SetFramerateLimit(60);

            //Initialize OpenTK
            OpenTKContext.Initialize(App.SystemHandle);
        }

        // drawAtariAnimation - draws single frame and updates counter
        public static void DrawAtariAnimation ()
        {
            if (animationNotStarted)
            {
                currentFrame = firstFrame;
                Offset = encounterAnim[currentFrame].Offset;
                animImage = encounterAnim[currentFrame].image;
                animDuration = encounterAnim[currentFrame].duration;
                animationNotStarted = false;
            }

            if (animDuration == 0)
            {
                currentFrame++;
                if (currentFrame == (lastFrame+1))
                    currentFrame = firstFrame;
                Offset = encounterAnim[currentFrame].Offset;
                animImage = encounterAnim[currentFrame].image;
                animDuration = encounterAnim[currentFrame].duration;
            }

            animDuration--;

            // Alternate and set animation frame as required
            encImage.Texture = encImageSheet;

            // Original Atari 8bit image at original size
            SetTileImage(animImage);

            //MLT: Downcast to int
            // Calculate new image width and height based on viewport size
            //var encWidth = (int)(viewWidth / 4.5);
            //var encHeight = (int)(viewHeight / 1.125);
            
            /* SET POSITION OF RESIZED IMAGE ON SCREEN */
            var encX = (WindowSize.Width / 2) - 32;
            var encY = (viewPortY + ViewSize.Height) - 130;

            encImage.Position = Offset.IsEmpty ? new Vector2f(encX, encY) : new Vector2f(viewPortX - 32 + Offset.X, viewPortY + (Offset.Y * 2));

            // DRAW DISPLAY AND FINAL ENCOUNTER IMAGE
            DispMain();

            if (graphicMode == DisplayOptions.AtariSmall)
                App.Draw(encImage);
        }

        public static void LoadResources ()
        {
            LoadBackgroundNames();
            LoadTextureNames();
            InitTextures();
            InitLyricFont();

            LoadCounterImages();

            compassN = new Texture("data/images/compass_n.png");
            compassS = new Texture("data/images/compass_s.png");
            compassW = new Texture("data/images/compass_w.png");
            compassE = new Texture("data/images/compass_e.png");

            // Create a sprite for the stat banner
            BannerImageCity = new Texture("data/images/cityBanner.png");
            Banner.Texture = BannerImageCity;
            BannerImageStrip = new Texture("data/images/cityBannerStatusLine.png");
            BannerStrip.Texture = BannerImageStrip;

            // Load Atari 8 bit encounter images sheet
            encImageSheet = new Texture("data/images/encounters/encounters.png"); // Atari 8bit
        }

        //TODO: Move to main menu
        public static void DisplayQuitMenu ()
        {
            DrawText(6, 11, " Are you sure you want to quit?");
            DrawText(15, 13, " ( es or  o)");
            SetFontColor(40, 96, 244, 255);
            DrawText(15, 13, "  Y      N");
            SetFontColor(215, 215, 215, 255);
        }

        //TODO: Move to main menu
        public static void DisplayOptionsMenu ()
        {
            DrawText(17, 0, "Options");
            DrawText(8, 2, "( ) Save current character");
            DrawText(8, 4, "( ) Quit to main menu");

            DrawText(1, 8, "Keys");
            DrawText(1, 10, "F1-F7 Hotkeys for information screens");
            DrawText(1, 11, ", .   Toggle through information screens");
            DrawText(1, 12, "I K   Move forward and backward");
            DrawText(1, 13, "J L   Turn left and right");
            DrawText(1, 14, "      (Arrow keys may also be used)");
            DrawText(1, 15, "U     Use or equip items");
            DrawText(1, 16, "0-9   Select options from menus");
            DrawText(1, 17, "A     Display mini map");
            DrawText(1, 18, "M     Display full screen map");
            DrawText(1, 19, "W     Wait for an encounter");
            DrawText(1, 20, "G     Get a weapon or item");
            DrawText(1, 21, "D     Drop a weapon or item");
            DrawText(1, 22, "C     Cast a spell");
            DrawText(1, 23, "ESC   Display this screen");


            SetFontColor(40, 96, 244, 255);
            DrawText(8, 2, " S");
            DrawText(8, 4, " Q");

            SetFontColor(215, 215, 215, 255);

            DrawText(13, 7, "Or ESC to cancel");
            SetFontColor(40, 96, 244, 255);
            DrawText(13, 7, "   ESC");
            SetFontColor(215, 215, 215, 255);
        }

        //TODO: Move to create character
        public static void DisplayDungeonGateImage ()
        {
            dungeonGate.Texture = imgDungeonGate;
            dungeonGate.Position = new Vector2f(gateX, gateY + 64);
            App.Draw(dungeonGate);
        }

        //TODO: Move to create character
        public static void DisplayCityGateImage ()
        {
            cityGate.Texture = imgCityGate;
            cityGate.Position = new Vector2f(gateX, gateY + 78);
            App.Draw(cityGate);
        }

        //TODO: Move to create character
        public static void LoadCounterImages ()
        {
            // Dungeon gate counters
            img0 = new Texture("data/images/0.png");
            img1 = new Texture("data/images/1.png");
            img2 = new Texture("data/images/2.png");
            img3 = new Texture("data/images/3.png");
            img4 = new Texture("data/images/4.png");
            img5 = new Texture("data/images/5.png");
            img6 = new Texture("data/images/6.png");
            img7 = new Texture("data/images/7.png");
            img8 = new Texture("data/images/8.png");
            img9 = new Texture("data/images/9.png");

            //City gate counters
            imgc0 = new Texture("data/images/c0.png");
            imgc1 = new Texture("data/images/c1.png");
            imgc2 = new Texture("data/images/c2.png");
            imgc3 = new Texture("data/images/c3.png");
            imgc4 = new Texture("data/images/c4.png");
            imgc5 = new Texture("data/images/c5.png");
            imgc6 = new Texture("data/images/c6.png");
            imgc7 = new Texture("data/images/c7.png");
            imgc8 = new Texture("data/images/c8.png");
            imgc9 = new Texture("data/images/c9.png");

            imgDungeonGate = new Texture("data/images/locations2/gate3.png");
            imgDungeonGate.Smooth = false;
            imgCityGate = new Texture("data/images/cityGate.png");
            imgCityGate.Smooth = false;
        }

        public static void DispInit ()
        {
            SetScreenValues();
            /* Set up window based on choice of display option NOT screen resolution */
            GL.Enable(EnableCap.Texture2D);         // enable texture mapping
            GL.ShadeModel(ShadingModel.Smooth);     // Enable Smooth Shading
            GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);

            // Set color and depth clear value
            GL.ClearDepth(1);

            // Enable Z-buffer read and write
            GL.Enable(EnableCap.DepthTest);
            GL.DepthMask(true);

            // Setup a perspective projection
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();

            /* Original small 3D view */
            Glu.Perspective(45, (float)(ViewSize.Width / ViewSize.Height), 0.1, 100);
            if (graphicMode != DisplayOptions.AlternateLarge)
            {
                var z = WindowSize.Height - (viewPortY + ViewSize.Height);

                GL.Viewport(viewPortX, z, ViewSize.Width, ViewSize.Height);
                GL.Translate(0.0, 0.0, -1.0);
            } else
            {
                GL.Translate(0.0, 0.0, -1.2);
            }

            GL.MatrixMode(MatrixMode.Modelview);
        }

        public static void DrawConsoleBackground ()
        {
            /* Draws a transparent box with yellow border around the console window whilst exploring and in large 3D view mode */
            if ((plyr.status != GameStates.Module) && (graphicMode == DisplayOptions.AlternateLarge)) // Whilst not shopping
            {
                var rectangle = new RectangleShape() {
                    Size = new Vector2f(670, 182),
                    OutlineColor = Color.Yellow,
                    FillColor = new Color(0, 0, 0, 128),
                    OutlineThickness = 1,
                    Position = new Vector2f(consoleX - 16, consoleY) // Offset to give a 16 pixel border to text
                };
                App.Draw(rectangle);
            }
        }

        //TODO: Move to main
        public static void DisplayLoading ()
        {
            ClearDisplay();
            DrawText(loadingX, loadingY, "Loading...");
            UpdateDisplay();
        }

        //TODO: Move to main menu
        public static void DisplayMainMenu ()
        {
            DrawLogo();

            var z = (240) / 18;
            var x = 2;

            DrawText(x + 3, z, "(1) Create a new City character");
            DrawText(x + 3, z + 1, "(2) Create a new Dungeon character");
            DrawText(x + 3, z + 2, "(3) Resume a character");
            DrawText(x + 3, z + 3, "(4) Acknowledgements");
            DrawText(x + 3, z + 6, "(6) Modify audio:");
            DrawText(x + 3, z + 7, "(7) Modify font:");
            DrawText(x + 3, z + 9, "(0) Leave the game");

            if (!plyr.musicStyle)
                DrawText(x + 21, z + 6, "Atari 8bit");
            else
                DrawText(x + 21, z + 6, "Alternate");
            if (plyr.fontStyle == 0)
                DrawText(x + 21, z + 7, "Smooth");
            else
                DrawText(x + 21, z + 7, "Atari 8bit");
        }

        //TODO: Move to main menu
        public static void DisplayAcknowledgements ()
        {
            var acknowledgements = true;

            while (acknowledgements)
            {
                ClearDisplay();
                SetFontColor(40, 96, 244, 255);
                DrawText(1, 3, "Alternate Reality X       New Music");
                DrawText(1, 6, "Alternate Reality Copyright and Concept");
                DrawText(1, 9, "Alternate Reality: The Dungeon");
                DrawText(1, 12, "Original AR Music");
                DrawText(1, 15, "Original AR Artwork          New Art");
                DrawText(1, 19, "Disassembly Genius");
                DrawText(1, 22, "Additional Programming");

                SetFontColor(215, 215, 215, 255);
                DrawText(13, 0, "Acknowledgements");
                DrawText(1, 4, "acrin1 AT gmail.com       Furious");
                DrawText(1, 7, "Philip Price");
                DrawText(1, 10, "Dan Pinal, Ken Jordan");
                DrawText(1, 13, "Gary Gilbertson");
                DrawText(1, 16, "Craig Skinner, Bonita Reid,  Ted Cox");
                DrawText(1, 17, "Steve Hofmann                Wisecat");
                DrawText(1, 20, "Jim Norris, Kroah & Brian Herlihy");
                DrawText(1, 23, "M Scott Adams");
                UpdateDisplay();
                if (KeyPressed())
                    acknowledgements = false;
            }

            acknowledgements = true;

            while (acknowledgements)
            {
                ClearDisplay();
                SetFontColor(40, 96, 244, 255);
                DrawText(1, 3, "   Special thanks for supporting the");
                DrawText(1, 5, "    development of release 0.82 to:");

                SetFontColor(215, 215, 215, 255);
                DrawText(13, 0, "Acknowledgements");
                DrawText(1, 8, "   Brian Herlihy       Dennis Hughes");
                DrawText(1, 10, "   Danny Belvin        Marco Fraolini");
                DrawText(1, 12, "   Allan van Leeuwen   Cliff Friedel");
                DrawText(1, 14, "   GameBanshee.com     Matthew Zagacki");
                DrawText(1, 16, "   Eric Koh            Stephen Mahoney");
                DrawText(1, 18, "   Tim Georgic");
                UpdateDisplay();
                if (KeyPressed())
                    acknowledgements = false;
            }

            acknowledgements = true;

            while (acknowledgements)
            {
                ClearDisplay();
                SetFontColor(40, 96, 244, 255);
                DrawText(1, 3, "    Special thanks for their support");

                SetFontColor(215, 215, 215, 255);
                DrawText(13, 0, "Acknowledgements");
                DrawText(4, 5, "Eric Koh           Brian Herlihy    ");
                DrawText(4, 6, "Maxzius            Stefano Peracchi ");
                DrawText(4, 7, "BelriX             Thomas Eibl     ");
                DrawText(4, 8, "James Denson       Aria             ");
                DrawText(4, 9, "Dramon Glover      Dalimar          ");
                DrawText(4, 10, "Paul Moore         Marco Fraolini   ");
                DrawText(4, 11, "Allan van Leeuwen  Jerry Stabell");
                DrawText(4, 12, "Stephen Mahoney    Goodman Gear");
                DrawText(4, 13, "Steven Kovach      Kara Cordner");
                DrawText(4, 14, "Andrew Hancock     Michael McCloskey");
                DrawText(4, 15, "Stefano Peracchi   Timothy Jones");
                DrawText(4, 16, "Stephen Latz       Chris Larson");
                DrawText(4, 17, "Matthew Zagacki    David Parslow");
                DrawText(4, 18, "Ross Lemke         Dennis Hughes");
                DrawText(4, 19, "Danny Belvin       Rudolf Kraus");
                DrawText(4, 20, "Jack Webb          Richard Milks");
                DrawText(4, 21, "Jaime Soltys");

                UpdateDisplay();
                if (KeyPressed())
                    acknowledgements = false;
            }
        }

        public static void ClearDisplay ()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            App.PushGLStates();
        }

        public static void UpdateDisplay ()
        {
            App.PopGLStates();
            App.Display();
        }

        //TODO: Move to program
        public static void DispMain ()
        {
            // All non OpenGL drawing should be performed between the pushGLStates and popGLStates commands
            // Need to determine here if 3D view or shop image

            Draw3DView();
            DrawStatsPanel();
            DrawCompass();
            DrawAutomap();
            if ((graphicMode == DisplayOptions.AlternateLarge) && (plyr.status != GameStates.Encounter))
                DrawConsoleBackground();
        }

        //TODO: Move to create character
        public static void DrawImage ( string imagename, Drawing.Point pos ) => DrawImage(imagename, pos.X, pos.Y);
            
        public static void DrawImage ( string imagename, int x, int y )
        {
            // Counter images for Dungeon gate character creation
            Texture texture;
            var useC = plyr.scenario != Scenarios.Dungeon;
            switch (imagename)
            {
                case "0":
                texture = useC ? imgc0 : img0;
                break;
                case "1":
                texture = useC ? imgc1 : img1;
                break;
                case "2":
                texture = useC ? imgc2 : img2;
                break;
                case "3":
                texture = useC ? imgc3 : img3;
                break;
                case "4":
                texture = useC ? imgc4 : img4;
                break;
                case "5":
                texture = useC ? imgc5 : img5;
                break;
                case "6":
                texture = useC ? imgc6 : img6;
                break;
                case "7":
                texture = useC ? imgc7 : img7;
                break;
                case "8":
                texture = useC ? imgc8 : img8;
                break;
                case "9":
                texture = useC ? imgc9 : img9;
                break;

                default:
                throw new InvalidOperationException("Unknown image");
            };

            counterImage.Texture = texture;
            counterImage.Position = new Vector2f(gateX + x, gateY + y);
            App.Draw(counterImage);
        }

        //TODO: Move to Program
        public static void LoadLogoImage ()
        {
            LogoImage = new Texture("data/images/logo640x240.png");
            LogoSprite.Texture = LogoImage;
            var x = (WindowSize.Width - 640) / 2;
            var y = (WindowSize.Height - (180 + 240)) / 2;
            LogoSprite.Position = new Vector2f(x, y);
        }

        //TODO: Move to Main Window
        public static void DrawInfoPanels ()
        {
            if (plyr.status != GameStates.Encounter)
            {
                if (plyr.infoPanel == 1)
                {

                    if ((plyr.special >= 0xc0) && (plyr.special <= 0xff) && (plyr.scenario == Scenarios.Dungeon)) // 224 - 255 was d2
                    {
                        var ind = (plyr.special - 0xc0);
                        var str = roomMessages[ind];
                        CText(str);
                    } else
                    {
                        BText(2, 1, "Food Packets    Torches   Water Flasks"); //was 3
                        BText(7, 2, plyr.food);
                        BText(20, 2, plyr.torches);
                        BText(33, 2, plyr.water);
                        if (plyr.scenario == 0)
                        {
                            BText(11, 5, "You are in the city");
                            BText(12, 6, "of Xebec's Demise");
                        }
                        if (plyr.scenario == Scenarios.Dungeon)
                        {
                            if (plyr.map == 1)
                                BText(12, 5, "You are on level 1");
                            if (plyr.map == 2)
                                BText(12, 5, "You are on level 2");
                            if (plyr.map == 3)
                                BText(12, 5, "You are on level 3");
                            if (plyr.map == 4)
                                BText(12, 5, "You are on level 4");
                            BText(14, 6, "of the Dungeon");
                        }
                        if (plyr.scenario == Scenarios.Arena)
                        {
                            BText(11, 5, "You are in the Arena");
                            BText(12, 6, "of Xebec's Demise");
                        }

                        BText(1, 7, CheckThirst());
                        BText(1, 8, CheckHunger());
                        BText(1, 9, CheckAlcohol());
                        BText(30, 7, CheckEncumbrance());
                        BText(31, 8, CheckPoison());
                        BText(31, 9, CheckDisease());
                    }
                }

                if (plyr.infoPanel == 2)
                {
                    BText(1, 1, "Gold Coins   Silver Coins   Copper Coins");
                    BText(3, 2, plyr.gold);
                    BText(17, 2, plyr.silver);
                    BText(32, 2, plyr.copper);

                    BText(1, 4, "            Other Possessions");

                    BText(8, 6, "Gems:");
                    BText(6, 7, "Jewels:");
                    BText(4, 8, "Crystals:");
                    BText(29, 6, "Keys:");
                    BText(24, 7, "Compasses:");
                    BText(23, 8, "Timepieces:");

                    BText(13, 6, plyr.gems);
                    BText(13, 7, plyr.jewels);
                    BText(13, 8, plyr.crystals);
                    BText(34, 6, plyr.keys);
                    BText(34, 7, plyr.compasses);
                    BText(34, 8, plyr.timepieces);

                }

                if (plyr.infoPanel == 3)
                {
                    BText(18, 1, "Weapons");
                    var str = "Primary: Bare hand";
                    if (plyr.priWeapon != 255)
                        str = "Primary: " + itemBuffer[plyr.priWeapon].name;
                    BText(1, 2, str);
                    str = "Secondary: Bare hand";
                    if (plyr.secWeapon != 255)
                        str = "Secondary: " + itemBuffer[plyr.secWeapon].name;
                    BText(1, 3, str);

                    // plyr.headArmor MUST be set by USE command in game not manually!
                    BText(19, 5, "Armour");
                    BText(1, 6, "Head:");
                    BText(1, 7, "Body:");
                    BText(1, 8, "Arms:");
                    BText(1, 9, "Legs:");

                    str = "None";
                    if (plyr.headArmour != 255)
                        str = itemBuffer[plyr.headArmour].name;
                    BText(7, 6, str);
                    str = "None";
                    if (plyr.bodyArmour != 255)
                        str = itemBuffer[plyr.bodyArmour].name;
                    BText(7, 7, str);
                    str = "None";
                    if (plyr.armsArmour != 255)
                        str = itemBuffer[plyr.armsArmour].name;
                    BText(7, 8, str);
                    str = "None";
                    if (plyr.legsArmour != 255)
                    {
                        str = itemBuffer[plyr.legsArmour].name;
                    }
                    BText(7, 9, str);
                }

                if (plyr.infoPanel == 4)
                {
                    CyText(1, "Apparel");
                    if ((plyr.clothing[0] == 255) && (plyr.clothing[1] == 255) && (plyr.clothing[2] == 255) && (plyr.clothing[3] == 255))
                        CyText(3, "Birthday suit");
                    var y = 3;
                    if (plyr.clothing[0] != 255)
                    {
                        CyText(y, itemBuffer[plyr.clothing[0]].name);
                        y++;
                    }
                    if (plyr.clothing[1] != 255)
                    {
                        CyText(y, itemBuffer[plyr.clothing[1]].name);
                        y++;
                    }
                    if (plyr.clothing[2] != 255)
                    {
                        CyText(y, itemBuffer[plyr.clothing[2]].name);
                        y++;
                    }
                    if (plyr.clothing[3] != 255)
                        CyText(y, itemBuffer[plyr.clothing[3]].name);
                }

                if (plyr.infoPanel == 5)
                {
                    CyText(1, "Active Magic");
                    var y = 3; // starting value for displaying items
                    if (plyr.protection1 != 0)
                    {
                        CyText(y, "Protection +1");
                        y++;
                    }
                    if (plyr.protection2 != 0)
                    {
                        CyText(y, "Protection +2");
                        y++;
                    }
                    if (plyr.invulnerability[0] != 0)
                    {
                        CyText(y, "Invulnerability Blunt");
                        y++;
                    }
                    if (plyr.invulnerability[1] != 0)
                    {
                        CyText(y, "Invulnerability Sharp");
                        y++;
                    }
                    if (plyr.invulnerability[2] != 0)
                    {
                        CyText(y, "Invulnerability Earth");
                        y++;
                    }
                    if (plyr.invulnerability[3] != 0)
                    {
                        CyText(y, "Invulnerability Air");
                        y++;
                    }
                    if (plyr.invulnerability[4] != 0)
                    {
                        CyText(y, "Invulnerability Fire");
                        y++;
                    }
                    if (plyr.invulnerability[5] != 0)
                    {
                        CyText(y, "Invulnerability Water");
                        y++;
                    }
                    if (plyr.invulnerability[6] != 0)
                    {
                        CyText(y, "Invulnerability Power");
                        y++;
                    }
                    if (plyr.invulnerability[7] != 0)
                    {
                        CyText(y, "Invulnerability Mental");
                        y++;
                    }
                    if (plyr.invulnerability[8] != 0)
                    {
                        CyText(y, "Invulnerability Cleric");
                        y++;
                    }
                }

                if (plyr.infoPanel == 6)
                {
                    CyText(1, "Known Diseases");
                    var y = 3; // starting value for displaying items
                    if (plyr.diseases[0] > 14)
                    {
                        CyText(y, "Rabies");
                        y++;
                    }
                }

                if (plyr.infoPanel == 7)
                    CyText(1, "Curses");

                if (plyr.infoPanel == 8)
                    CyText(1, "Titles");
            }
        }

        //TODO: Move to Main Window
        public static void DrawStatsPanel ()
        {
            //TODO: What is state 5?
            if ((graphicMode == DisplayOptions.AlternateLarge) && (plyr.status != (GameStates)5) && (plyr.status != GameStates.Module)) // not shopping
            {
                var rectangle = new RectangleShape() {
                    Size = new Vector2f(640, 110), // 640, 110
                    OutlineColor = Color.Yellow,
                    OutlineThickness = 1,
                    Position = new Vector2f(statPanelX, statPanelY - 1)
                };
                App.Draw(rectangle);
            }

            Banner.Position = new Vector2f(statPanelX, statPanelY - 1);
            if (plyr.status == GameStates.Module)
            {
                var statsX = (WindowSize.Width - 640) / 2;
                //var statsY = ((windowHeight - 144) / 2) - 126; // 144 pixels for picture + 16 space + stats height
                Banner.Position = new Vector2f(statsX, shopStatsY - 1);
            }
            App.Draw(Banner);
            if ((plyr.status == GameStates.Explore) || (plyr.status == GameStates.Encounter))
            {
                BannerStrip.Position = new Vector2f(statPanelX, (statPanelY + 89));
                App.Draw(BannerStrip);
            }
            if (plyr.status != GameStates.Module)
                App.Draw(BannerStrip);
            if (!plyr.diagOn)
            {
                SetFontColor(162, 114, 64, 255);
                DrawText(2, 0, plyr.name);
                DrawText(32, 0, "Level:");
                DrawText(38, 0, plyr.level);
                SetFontColor(147, 69, 130, 255);
                DrawText(2, 1, "Stats:  STA  CHR  STR  INT  WIS  SKL");
                SetFontColor(138, 68, 158, 255);
                DrawText(11, 2, plyr.sta);
                DrawText(16, 2, plyr.chr);
                DrawText(21, 2, plyr.str);
                DrawText(26, 2, plyr.inte);
                DrawText(31, 2, plyr.wis);
                DrawText(36, 2, plyr.skl);

                SetFontColor(62, 106, 162, 255);
                DrawText(2, 3, "Experience:");
                DrawText(14, 3, plyr.xp);
                if (plyr.hp < 0)
                    DrawText(24, 3, "Hit Points: !!!!!");
                if (plyr.hp == plyr.maxhp)
                {
                    DrawText(25, 3, "Hit Points=");
                    DrawText(36, 3, plyr.hp);
                }
                if ((plyr.hp < plyr.maxhp) && (plyr.hp > -1))
                {
                    DrawText(25, 3, "Hit Points:");
                    DrawText(36, 3, plyr.hp);
                }

                var str = "You are " + descriptions[plyr.location];

                // Draw status line text
                CheckForItemsHere();

                SetFontColor(102, 149, 40, 255);

                if ((plyr.status_text != " ") && (plyr.status != GameStates.Encounter) && plyr.alive)
                    DrawText(2, 5, plyr.status_text);

                SetFontColor(102, 149, 40, 255);
                if (plyr.alive)
                    DrawText(2, 4, str);
                else
                    DrawText(12, 4, "$ Where are you? $");

                SetFontColor(215, 215, 215, 255); // set text colour to white for all other text
            }

            // Diag Text

            if (plyr.diagOn)
            {
                DrawText(2, 0, $"X:{plyr.Position.X} Y:{plyr.Position.Y} Special:{plyr.special}  Zone:{plyr.zone} Set:{plyr.zoneSet}");
                DrawText(2, 1, $"Front:{plyr.front}  Left:{plyr.left} Right:{plyr.right}  Back:{plyr.back}");

                DrawText(2, 5, $"Offset: {plyr.z_offset}");
                DrawText(2, 2, $"Floor:{zones[plyr.zoneSet].floor}  Ceiling:{zones[plyr.zoneSet].ceiling}");
                DrawText(2, 3, $"Position:{plyr.location}");

                var e = ReturnCarriedWeight();
                DrawText(2, 4, $"Encumbrance:{e}");
                DrawText(30, 4, $"T: {plyr.hours}:{plyr.minutes}");
            }
        }

        //TODO: Move to Main Window
        public static void DrawCompass ()
        {
            if (plyr.compasses > 0)
            {
                if ((plyr.status != GameStates.Module) && (graphicMode == DisplayOptions.AlternateLarge)) // if exploring and full screen draw a background
                {
                    var x = 16;
                    var y = (WindowSize.Height - 128) / 2;

                    var rectangle = new RectangleShape() {
                        Size = new Vector2f(130, 130),
                        OutlineColor = Color.Yellow,
                        FillColor = new Color(0, 0, 0, 128),
                        OutlineThickness = 1,
                        Position = new Vector2f(x, y) // Offset to give a 16 pixel border to text
                    };
                    App.Draw(rectangle);
                }

                Texture texture;
                switch (plyr.facing)
                {
                    case Directions.West:
                    texture = compassW;
                    break;
                    case Directions.North:
                    texture = compassN;
                    break;
                    case Directions.East:
                    texture = compassE;
                    break;
                    case Directions.South:
                    texture = compassS;
                    break;

                    default:
                    throw new InvalidOperationException("Unknown player facing");
                };

                compass.Texture = texture;

                if (graphicMode == DisplayOptions.AlternateLarge)
                {
                    var x = 16;
                    var y = (WindowSize.Height - 128) / 2;
                    compass.Position = new Vector2f(x, y);
                } else
                {
                    /* Normal Small 3D view mode */
                    var x = (viewPortX - 128) / 2;
                    var y = viewPortY + ((ViewSize.Height - 128) / 2);
                    compass.Position = new Vector2f(x, y);
                }
                App.Draw(compass);
            }
        }

        //TODO: Move to Shop
        public static void ClearShopDisplay ()
        {
            App.Clear();
            App.PushGLStates();
            App.Draw(ShopSprite);
            DrawStatsPanel();
        }

        //TODO: Move to Shop
        public static void LoadShopImage ( int imageno )
        {
            var useGraphics2 = graphicMode.UseAlternateTextures();
            var texturePath = useGraphics2 ? "data/images/locations2/" : "data/images/locations";

            //TODO: Consolidate image names to eliminate the file checks
            switch (imageno)
            {
                case 1:
                texturePath += useGraphics2 ? "inn.png" : "retreat.png";
                break;
                case 2:
                texturePath += useGraphics2 ? "rathskeller.png" : "rathskeller.png";
                break;
                case 3:
                texturePath += useGraphics2 ? "shop.png" : "oDamon.png";
                break;
                case 4:
                texturePath += useGraphics2 ? "guild.png" : "evilGuild.png";
                break;
                case 5:
                texturePath += useGraphics2 ? "guild.png" : "goodGuild.png";
                break;
                case 6:
                texturePath += useGraphics2 ? "stairwayUp.png" : "stairwayUp.png";
                break;
                case 7:
                texturePath += useGraphics2 ? "stairwayDown.png" : "stairwayDown.png";
                break;
                case 8:
                texturePath += useGraphics2 ? "citySmithyNight.png" : "citySmithyNight.png";
                break;
                case 9:
                texturePath += useGraphics2 ? "smithy.png" : "imgCitySmithy.png";
                break;
                case 10:
                texturePath += useGraphics2 ? "tavern.png" : "imgCityTavern.png";
                break;
                case 11:
                texturePath += useGraphics2 ? "inn.png" : "imgCityInn.png";
                break;
                case 12:
                texturePath += useGraphics2 ? "shop.png" : "imgCityShop.png";
                break;
                case 13:
                texturePath += useGraphics2 ? "bank.png" : "imgCityBank.png";
                break;
                case 14:
                texturePath += useGraphics2 ? "guild.png" : "imgCityGuild.png";
                break;
                case 15:
                texturePath += useGraphics2 ? "healer1.png" : "imgCityHealer.png";
                break;
                case 16:
                texturePath += useGraphics2 ? "trolls.png" : "trolls.png";
                break;
                case 17:
                texturePath += useGraphics2 ? "goblins.png" : "goblins.png";
                break;
                case 18:
                texturePath += useGraphics2 ? "chapel.png" : "chapel.png";
                break;
                case 19:
                texturePath += useGraphics2 ? "fountain.png" : "fountain.png";
                break;
                case 20:
                texturePath += useGraphics2 ? "oracle.png" : "oracle.png";
                break;
                case 21:
                texturePath += useGraphics2 ? "healer2.png" : "imgCityHealer.png";
                break;
                case 22:
                texturePath += useGraphics2 ? "lift.png" : "lift.png";
                break;
                case 23:
                texturePath += useGraphics2 ? "river.png" : "ferry.png";
                break;
                case 24:
                texturePath += useGraphics2 ? "undead.png" : "undead.png";
                break;
                case 25:
                texturePath += useGraphics2 ? "arena.png" : "arena.png";
                break;
                case 26:
                texturePath += useGraphics2 ? "dwarvenSmithy.png" : "dwarvenSmithy.png";
                break;
                case 27:
                texturePath += useGraphics2 ? "6.png" : "6.png";
                break;

                default:
                throw new InvalidOperationException("Unknown image");
            };

            ShopSprite.Texture = new Texture(texturePath);
            ShopSprite.Scale = new Vector2f(2.0F, 2.0F);
            ShopSprite.Position = new Vector2f((WindowSize.Width - 640) / 2, shopPictureY);
        }

        public static int CheckCityDoors () => 0;

        #region Private Members

        private static void SetTileImage ( int tile_no )
        {
            int row;
            int column;
            var tilesPerRow = 8; // number of tiles per row in source image containing all tiles (8 default)
                                 //int tilesPerColumn = 8; // number of tiles per column in source image (8 default)

            //Select 64x128 section of tile sheet for tile
            if (tile_no >= tilesPerRow)
            {
                column = (tile_no % tilesPerRow); // remainder
                row = ((tile_no - column) / tilesPerRow);
            } else
            {
                column = tile_no;
                row = 0; // = row 1 on the actual tile sheet at y=0
            }

            var tileX = (column) * 64; // x loc on tiles image in pixels
            var tileY = (row) * 128; // y loc on tiles image in pixels

            encImage.TextureRect = new IntRect(tileX, tileY, 64, 128);
        }
        
        //TODO: Clean this up
        private static void SetScreenValues ()
        {
            // Determines screen element locations based on window dimensions
            gateX = (WindowSize.Width - 640) / 2;
            gateY = (WindowSize.Height - 384) / 2 - 78;
            loadingX = 16;
            loadingY = 11;

            var spacer = 0; // spacer value between screen elements - adjust here
            var consoleHeight = (16 + 2) * 10; // How tall is 10 lines of console text (16 pixels with 2 pixel space between lines)
            statPanelX = (WindowSize.Width - 640) / 2; // Center in middle of window width
            consoleX = (WindowSize.Width - 640) / 2; // Center in middle of window width

            /* Original small 3D view */
            if (graphicMode.UseOriginalSize())            
            {
                ViewSize = new Drawing.Size(288, 144);
                viewPortX = (WindowSize.Width - ViewSize.Width) / 2;
                var spacing = 110 + spacer + ViewSize.Height + spacer + consoleHeight;
                statPanelY = (WindowSize.Height - spacing) / 2;
                viewPortY = statPanelY + 110 + spacer;
                consoleY = viewPortY + ViewSize.Height + 4;
                miniMapX = WindowSize.Width - (((((WindowSize.Width - ViewSize.Width) / 2) - 144) / 2) + 144);
                miniMapY = spacer + viewPortY + ((ViewSize.Height - 144) / 2);
            } else  /* New large 3D view */
            {
                ViewSize = new Drawing.Size(WindowSize.Width, WindowSize.Height);
                viewPortX = 0;
                viewPortY = WindowSize.Height - ViewSize.Height;
                statPanelY = 16;
                consoleY = (WindowSize.Height - 32) - 144; // 16 x 10 not quite right
                miniMapX = (WindowSize.Width - 16) - 176;
                miniMapY = (WindowSize.Height - 176) / 2;
            }

            /* Shop positions are the same regardless of small / large 3D view choice */
            var temp = 110 + spacer + 144 + 16 + consoleHeight;
            shopStatsY = ((WindowSize.Height - temp) / 2);
            shopStatsY = statPanelY;
            shopPictureY = shopStatsY + 110 + spacer;
            shopConsoleY = shopPictureY + 144 + spacer; // 16 to put space between image and console text

            /* Misc assignments for positioning */

            lyricX = (WindowSize.Width - 640) / 2;
            lyricY = shopPictureY - 18;

        }        
        
        private static void DrawLogo () => App.Draw(LogoSprite);

        #endregion 

        #region Review Data

        public static Sprite encImage { get; set; } = new Sprite();

        public const string version = "0.82";

        public static WindowMode windowMode { get; set; }

        public static DisplayOptions graphicMode { get; set; }

        public static Drawing.Size WindowSize { get; set; }

        public static Drawing.Size ViewSize { get; set; }

        public static int viewPortX { get; set; }
        public static int viewPortY { get; set; }
        public static int statPanelX { get; set; } // x starting position for displaying the stats banner for centering
        public static int statPanelY { get; set; }
        public static int consoleY { get; set; } // y position for displaying the bottom screen info panel
        public static int consoleX { get; set; } // x starting position for displaying the panel for centering
        public static int miniMapY { get; set; } // y position for displaying the bottom screen info panel
        public static int miniMapX { get; set; } // x starting position for displaying the panel for centering
        public static int lyricX { get; set; }
        public static int lyricY { get; set; }
        public static int loadingX { get; set; }
        public static int loadingY { get; set; }
        public static int gateX { get; set; }
        public static int gateY { get; set; }
        public static int shopStatsY { get; set; } // y position for stats in shops
        public static int shopConsoleY { get; set; } // y position for console in shops
        public static int shopPictureY { get; set; }

        // Main window
        public static InputRenderWindow App { get; set; }

        public static bool mainMenuQuit { get; set; }

        //TODO: Put in a texture cache to lazy load and release this
        //TODO: Make this array or something so we can more quickly update it
        public static Texture img0 { get; set; }
        public static Texture img1 { get; set; }
        public static Texture img2 { get; set; }
        public static Texture img3 { get; set; }
        public static Texture img4 { get; set; }
        public static Texture img5 { get; set; }
        public static Texture img6 { get; set; }
        public static Texture img7 { get; set; }
        public static Texture img8 { get; set; }
        public static Texture img9 { get; set; }
        public static Texture imgDungeonGate { get; set; }
        public static Texture imgCityGate { get; set; }
        public static Texture imgc0 { get; set; }
        public static Texture imgc1 { get; set; }
        public static Texture imgc2 { get; set; }
        public static Texture imgc3 { get; set; }
        public static Texture imgc4 { get; set; }
        public static Texture imgc5 { get; set; }
        public static Texture imgc6 { get; set; }
        public static Texture imgc7 { get; set; }
        public static Texture imgc8 { get; set; }
        public static Texture imgc9 { get; set; }
        public static Texture consoleImage { get; set; }
        public static Texture BannerImageCity { get; set; }
        public static Texture BannerImageStrip { get; set; }
        public static Texture compassN { get; set; }
        public static Texture compassS { get; set; }
        public static Texture compassW { get; set; }
        public static Texture compassE { get; set; }
        public static Texture ShopImage { get; set; }
        public static Texture LogoImage { get; set; }

        //TODO: Use a sprite cache to lazy load, release these
        public static Sprite Banner { get; set; } = new Sprite();
        public static Sprite BannerStrip { get; set; } = new Sprite();
        public static Sprite counterImage { get; set; } = new Sprite();
        public static Sprite dungeonGate { get; set; } = new Sprite();
        public static Sprite cityGate { get; set; } = new Sprite();
        public static Sprite compass { get; set; } = new Sprite();
        public static Sprite ShopSprite { get; set; } = new Sprite();
        public static Sprite LogoSprite { get; set; } = new Sprite();
        
        public static string olddrawText { get; set; } // text string used for setting and passing strings to the print routine

        // If image2 == 255 then just display image1 rather than use animations below
        //Dungeon Monster Animation Scripts

        //TODO: Move to data file
        public static AnimFrame[] encounterAnim { get; set; } =
        {
            new AnimFrame() { image = 0, duration = 12 },
            new AnimFrame() { image = 1, duration = 12 },
            new AnimFrame() { image = 2, duration = 12 },
            new AnimFrame() { image = 3, duration = 111 },
            new AnimFrame() { image = 3, duration = 111 },
            new AnimFrame() { image = 6, duration = 100 },
            new AnimFrame() { image = 7, duration = 36 },
            new AnimFrame() { image = 8, duration = 36 },
            new AnimFrame() { image = 9, duration = 48 },
            new AnimFrame() { image = 10, duration = 48 },
            new AnimFrame() { image = 11, duration = 32 },
            new AnimFrame() { image = 12, duration = 16 },
            new AnimFrame() { image = 13, duration = 16 },
            new AnimFrame() { image = 14, duration = 16 },
            new AnimFrame() { image = 15, duration = 16 },
            new AnimFrame() { image = 16, duration = 36 },
            new AnimFrame() { image = 17, duration = 100 },
            new AnimFrame() { image = 18, duration = 50 },
            new AnimFrame() { image = 19, duration = 50 },
            new AnimFrame() { image = 20, duration = 144 },
            new AnimFrame() { image = 21, duration = 144 },
            new AnimFrame() { image = 22, duration = 144 },
            new AnimFrame() { image = 23, duration = 144 },
            new AnimFrame() { image = 24, duration = 20 },
            new AnimFrame() { image = 25, duration = 18 },
            new AnimFrame() { image = 26, duration = 18 },
            new AnimFrame() { image = 27, duration = 18 },
            new AnimFrame() { image = 28, duration = 88 },
            new AnimFrame() { image = 29, duration = 88 },
            new AnimFrame() { Offset = new Drawing.Point(154, 9), image = 30, duration = 20 },
            new AnimFrame() { Offset = new Drawing.Point(150, 11), image = 31, duration = 24 },
            new AnimFrame() { Offset = new Drawing.Point(153, 13), image = 30, duration = 20 },
            new AnimFrame() { Offset = new Drawing.Point(156, 11), image = 31, duration = 24 },
            new AnimFrame() { Offset = new Drawing.Point(157, 10), image = 30, duration = 20 },
            new AnimFrame() { Offset = new Drawing.Point(160, 9), image = 31, duration = 24 },
            new AnimFrame() { Offset = new Drawing.Point(159, 11), image = 30, duration = 20 },
            new AnimFrame() { Offset = new Drawing.Point(157, 12), image = 31, duration = 24 },
            new AnimFrame() { Offset = new Drawing.Point(154, 9), image = 32, duration = 20 },
            new AnimFrame() { Offset = new Drawing.Point(150, 11), image = 33, duration = 24 },
            new AnimFrame() { Offset = new Drawing.Point(153, 13), image = 32, duration = 20 },
            new AnimFrame() { Offset = new Drawing.Point(156, 11), image = 33, duration = 24 },
            new AnimFrame() { Offset = new Drawing.Point(157, 10), image = 32, duration = 20 },
            new AnimFrame() { Offset = new Drawing.Point(160, 9), image = 33, duration = 24 },
            new AnimFrame() { Offset = new Drawing.Point(159, 11), image = 32, duration = 20 },
            new AnimFrame() { Offset = new Drawing.Point(157, 12), image = 33, duration = 24 },
            new AnimFrame() { image = 34, duration = 88 },
            new AnimFrame() { image = 35, duration = 88 },
            new AnimFrame() { image = 36, duration = 16 },
            new AnimFrame() { image = 37, duration = 16 },
            new AnimFrame() { image = 38, duration = 16 },
            new AnimFrame() { image = 39, duration = 16 },
            new AnimFrame() { image = 38, duration = 16 },
            new AnimFrame() { image = 40, duration = 42 },
            new AnimFrame() { image = 41, duration = 42 },
            new AnimFrame() { image = 43, duration = 20 },
            new AnimFrame() { image = 44, duration = 18 },
            new AnimFrame() { image = 45, duration = 18 },
            new AnimFrame() { image = 42, duration = 18 },
            new AnimFrame() { image = 46, duration = 35 },
            new AnimFrame() { image = 47, duration = 35 },
            new AnimFrame() { image = 48, duration = 35 },
            new AnimFrame() { Offset = new Drawing.Point(156, 11), image = 49, duration = 42 },
            new AnimFrame() { Offset = new Drawing.Point(155, 10), image = 49, duration = 42 },
            new AnimFrame() { Offset = new Drawing.Point(155, 9), image = 49, duration = 42 },
            new AnimFrame() { Offset = new Drawing.Point(156, 10), image = 49, duration = 42 },
            new AnimFrame() { Offset = new Drawing.Point(157, 11), image = 49, duration = 42 },
            new AnimFrame() { image = 50, duration = 22 },
            new AnimFrame() { image = 51, duration = 22 },
            new AnimFrame() { image = 52, duration = 22 },
            new AnimFrame() { image = 53, duration = 22 },
            new AnimFrame() { image = 54, duration = 40 },
            new AnimFrame() { image = 55, duration = 10 },
            new AnimFrame() { image = 56, duration = 56 },
            new AnimFrame() { image = 57, duration = 28 },
            new AnimFrame() { image = 58, duration = 33 },
            new AnimFrame() { image = 59, duration = 33 },
            new AnimFrame() { image = 60, duration = 64 },
            new AnimFrame() { image = 61, duration = 64 },
            new AnimFrame() { image = 62, duration = 10 },
            new AnimFrame() { image = 63, duration = 10 },
            new AnimFrame() { image = 64, duration = 10 },
            new AnimFrame() { image = 65, duration = 10 },
            new AnimFrame() { image = 66, duration = 10 },
            new AnimFrame() { image = 67, duration = 10 },
            new AnimFrame() { image = 68, duration = 56 },
            new AnimFrame() { image = 69, duration = 28 },
            new AnimFrame() { image = 70, duration = 40 },
            new AnimFrame() { image = 71, duration = 40 },
            new AnimFrame() { image = 72, duration = 40 },
            new AnimFrame() { Offset = new Drawing.Point(157, 8), image = 73, duration = 18 },
            new AnimFrame() { Offset = new Drawing.Point(157, 8), image = 74, duration = 19 },
            new AnimFrame() { Offset = new Drawing.Point(157, 8), image = 75, duration = 19 },
            new AnimFrame() { image = 76, duration = 100 },
            new AnimFrame() { image = 77, duration = 100 },
            new AnimFrame() { image = 78, duration = 32 },
            new AnimFrame() { image = 79, duration = 32 },
            new AnimFrame() { image = 80, duration = 32 },
            new AnimFrame() { image = 81, duration = 32 },
            new AnimFrame() { image = 82, duration = 32 },
            new AnimFrame() { image = 83, duration = 32 },
            new AnimFrame() { image = 84, duration = 18 },
            new AnimFrame() { image = 85, duration = 18 },
            new AnimFrame() { image = 86, duration = 10 },
            new AnimFrame() { image = 87, duration = 14 },
            new AnimFrame() { image = 88, duration = 32 },
            new AnimFrame() { image = 89, duration = 32 },
            new AnimFrame() { image = 90, duration = 32 },
            new AnimFrame() { image = 91, duration = 32 },
            new AnimFrame() { image = 92, duration = 32 },
            new AnimFrame() { image = 93, duration = 32 },
            new AnimFrame() { image = 94, duration = 32 },
            new AnimFrame() { image = 95, duration = 32 },
            new AnimFrame() { image = 96, duration = 32 },
            new AnimFrame() { image = 97, duration = 32 },
            new AnimFrame() { image = 98, duration = 32 },
            new AnimFrame() { image = 99, duration = 32 },
            new AnimFrame() { image = 100, duration = 32 },
            new AnimFrame() { image = 101, duration = 48 },
            new AnimFrame() { image = 102, duration = 48 },
            new AnimFrame() { image = 101, duration = 48 },
            new AnimFrame() { image = 102, duration = 48 },
            new AnimFrame() { image = 103, duration = 48 },
            new AnimFrame() { image = 102, duration = 48 }
        };
        // end of animation sequences excluding city images        

        public static bool animationNotStarted { get; set; }
        public static Texture encImageSheet { get; set; }
        public static int firstFrame { get; set; }
        public static int lastFrame { get; set; }
        public static int currentFrame { get; set; } // within encounterAnim 0-7

        public static Drawing.Point Offset { get; set; }
        
        public static int animImage { get; set; }
        public static int animDuration { get; set; }
 
         #endregion
    }
}