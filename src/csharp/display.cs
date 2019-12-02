using System;
using System.Collections.Generic;
using System.Linq;
using sf;
using System;

namespace P3Net.Arx
{
	public class AnimFrame
	{
		public int xOffset { get; set; } // 0 for most animations
		public int yOffset { get; set; } // 0 for most animations
		public int image { get; set; }
		public int duration { get; set; }
	}

	public partial class GlobalMembers
	{
		public static void CreateGameWindow()
		{
			string title = "Alternate Reality X " + version;

			if (windowMode == 0)
				App.create(sf.VideoMode(windowWidth, windowHeight), title,sf.Style.Close);
			else
				App.create(sf.VideoMode(windowWidth, windowHeight), title, sf.Style.Fullscreen);

			// Print OpenGL settings to game console for information
			sf.ContextSettings settings = App.getSettings();
			Console.Write("Welcome to Alternate Reality X ");
			Console.Write(version);
			Console.Write(" ...");
			Console.Write("\n");
			Console.Write("\n");
			Console.Write("OpenGL Settings:\n\n");
			Console.Write("Depth bits:           ");
			Console.Write(settings.depthBits);
			Console.Write("\n");
			Console.Write("Stencil bits:         ");
			Console.Write(settings.stencilBits);
			Console.Write("\n");
			Console.Write("Anti-aliasing level:  ");
			Console.Write(settings.antialiasingLevel);
			Console.Write("\n");
			Console.Write("OpenGL Version:       ");
			Console.Write(settings.majorVersion);
			Console.Write(".");
			Console.Write(settings.minorVersion);
			Console.Write("\n");
			Console.Write("\n");
			Console.Write("Window Size:          ");
			Console.Write(windowWidth);
			Console.Write(" x ");
			Console.Write(windowHeight);
			Console.Write("\n");
			Console.Write("\n");
			Console.Write("\n");

			// Limit the framerate to 60 frames per second (this step is optional)
			App.setFramerateLimit(60);
		}

		public static void SetTileImage(int tile_no)
		{

			int row;
			int column;
			int tilesPerRow = 8; // number of tiles per row in source image containing all tiles (8 default)
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

			int tileX = (column) * 64; // x loc on tiles image in pixels
			int tileY = (row) * 128; // y loc on tiles image in pixels

			encImage.setTextureRect(sf.IntRect(tileX, tileY, 64, 128));
			//cout << "Tile X:" << tileX << "  ,  " << "Tile Y:" << tileY << "\n";

			// Nightstalker & Dragon exceptions due to larger image size
			//if (tile_no==86) { encImage.setTextureRect(sf::IntRect(tileX, tileY , 72, 136)); }
			//if (tile_no==82) { encImage.setTextureRect(sf::IntRect(tileX, tileY , 72, 128)); }

		}

	// drawAtariAnimation - draws single frame and updates counter

		public static void DrawAtariAnimation()
		{
			if (animationNotStarted)
			{
				currentFrame = firstFrame;
				yOffset = encounterAnim[currentFrame].yOffset;
				xOffset = encounterAnim[currentFrame].xOffset;
				animImage = encounterAnim[currentFrame].image;
				animDuration = encounterAnim[currentFrame].duration;
				animationNotStarted = false;
			}

			if (animDuration == 0)
			{
				currentFrame++;
				if (currentFrame == (lastFrame+1))
					currentFrame = firstFrame;
				yOffset = encounterAnim[currentFrame].yOffset;
				xOffset = encounterAnim[currentFrame].xOffset;
				animImage = encounterAnim[currentFrame].image;
				animDuration = encounterAnim[currentFrame].duration;
			}

			animDuration--;



			//sf::Sprite largeEncImage; // only used for mode 2 full screen 3d view
			int encWidth;
			int encHeight;
			int encX;
			int encY;

			// Alternate and set animation frame as required
			encImage.setTexture(encImageSheet);
			//encImageSheet.setSmooth(true);
			// Original Atari 8bit image at original size

			SetTileImage(animImage);
			//if ((plyr.encounterAnimationRef==92) && (plyr.gender==2)) { SetTileImage(72); } // female doppelganger image

			//MLT: Downcast to int
			// Calculate new image width and height based on viewport size
			encWidth = (int)(viewWidth / 4.5);
			encHeight = (int)(viewHeight / 1.125);

			/*
			if (graphicMode==ATARI_SMALL)
			{
				// Scale large image based on viewport height and width
				float scaleX = float(encWidth) / float(64);
				float scaleY = float(encHeight) / float(128);
				encImage.setScale(scaleX, scaleY);
			}
		
			*/


			/* SET POSITION OF RESIZED IMAGE ON SCREEN */

			//encX = (windowWidth - encWidth)/2;
			//encY = ((viewPortY+viewHeight)-2)-encHeight;
			//if ((plyr.encounterRef==59)||(plyr.encounterRef==86)) encX = (windowWidth/2)-(encWidth);
			encX = (windowWidth / 2) - 32;
			encY = (viewPortY + viewHeight) - 130;

			//encImage.setPosition(encX+(xOffset/2),encY+(yOffset/2));
			if ((xOffset == 0) && (yOffset == 0))
				encImage.setPosition(encX,encY);
			else
				encImage.setPosition((viewPortX - 32 + (xOffset)),(viewPortY + (yOffset * 2)));

			// DRAW DISPLAY AND FINAL ENCOUNTER IMAGE
			DispMain();

			if (graphicMode == (int)DisplayOptions.AtariSmall)
				App.draw(encImage);
		}
		//void drawEncounterAnimation();

		public static void LoadResources()
		{
			LoadBackgroundNames();
			LoadTextureNames();
			InitTextures();
			InitLyricFont();

			LoadCounterImages();

			compassN.loadFromFile("data/images/compass_n.png");
			compassS.loadFromFile("data/images/compass_s.png");
			compassW.loadFromFile("data/images/compass_w.png");
			compassE.loadFromFile("data/images/compass_e.png");

			// Create a sprite for the stat banner
			BannerImageCity.loadFromFile("data/images/cityBanner.png");
			Banner.setTexture(BannerImageCity);
			BannerImageStrip.loadFromFile("data/images/cityBannerStatusLine.png");
			BannerStrip.setTexture(BannerImageStrip);
			//sf::Texture consoleImage, BannerImageCity, BannerImageStrip;
			//sf::Texture compassN,compassS,compassW,compassE;
			//sf::Sprite Banner, BannerStrip;

			// Load Atari 8 bit encounter images sheet
			encImageSheet.loadFromFile("data/images/encounters/encounters.png"); // Atari 8bit

		}
		public static void DisplayQuitMenu()
		{
			DrawText(6, 11, " Are you sure you want to quit?");
			DrawText(15, 13, " ( es or  o)");
			SetFontColour(40, 96, 244, 255);
			DrawText(15, 13, "  Y      N");
			SetFontColour(215, 215, 215, 255);
		}
		public static void DisplayOptionsMenu()
		{
			//string str;


			//SetFontColour(40, 96, 244, 255);
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


			SetFontColour(40, 96, 244, 255);
			DrawText(8, 2, " S");
			DrawText(8, 4, " Q");

			SetFontColour(215, 215, 215, 255);

			DrawText(13, 7, "Or ESC to cancel");
			SetFontColour(40, 96, 244, 255);
			DrawText(13, 7, "   ESC");
			SetFontColour(215, 215, 215, 255);

			//SetFontColour(215, 215, 215, 255);
		}
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//void DrawShopImage(int imageno);
		public static void DisplayDungeonGateImage()
		{
			dungeonGate.setTexture(imgDungeonGate);
			dungeonGate.setPosition(gateX, gateY + 64);
			App.draw(dungeonGate);
		}
		public static void DisplayCityGateImage()
		{
			cityGate.setTexture(imgCityGate);
			cityGate.setPosition(gateX, gateY + 78);
			App.draw(cityGate);
		}
		public static void LoadCounterImages()
		{
			// Dungeon gate counters
			img0.loadFromFile("data/images/0.png");
			img1.loadFromFile("data/images/1.png");
			img2.loadFromFile("data/images/2.png");
			img3.loadFromFile("data/images/3.png");
			img4.loadFromFile("data/images/4.png");
			img5.loadFromFile("data/images/5.png");
			img6.loadFromFile("data/images/6.png");
			img7.loadFromFile("data/images/7.png");
			img8.loadFromFile("data/images/8.png");
			img9.loadFromFile("data/images/9.png");

			//City gate counters
			imgc0.loadFromFile("data/images/c0.png");
			imgc1.loadFromFile("data/images/c1.png");
			imgc2.loadFromFile("data/images/c2.png");
			imgc3.loadFromFile("data/images/c3.png");
			imgc4.loadFromFile("data/images/c4.png");
			imgc5.loadFromFile("data/images/c5.png");
			imgc6.loadFromFile("data/images/c6.png");
			imgc7.loadFromFile("data/images/c7.png");
			imgc8.loadFromFile("data/images/c8.png");
			imgc9.loadFromFile("data/images/c9.png");

			//imgDungeonGate.loadFromFile("data/images/gat.png");
			imgDungeonGate.loadFromFile("data/images/locations2/gate3.png");
			imgDungeonGate.setSmooth(false);
			imgCityGate.loadFromFile("data/images/cityGate.png");
			imgCityGate.setSmooth(false);

		}
		public static void DispInit()
		{
			SetScreenValues();
			/* Set up window based on choice of display option NOT screen resolution */
			//setScreenValues(); // Determine positions of screen elements from array
			glEnable(GL_TEXTURE_2D); // enable texture mapping
			glShadeModel(GL_SMOOTH); // Enable Smooth Shading
			glHint(GL_PERSPECTIVE_CORRECTION_HINT, GL_NICEST);

			// Set color and depth clear value
			glClearDepth(1.0f);

			// Enable Z-buffer read and write
			glEnable(GL_DEPTH_TEST);
			glDepthMask(GL_TRUE);

			// Setup a perspective projection
			glMatrixMode(GL_PROJECTION);
			glLoadIdentity();

			/* Original small 3D view */
			if (graphicMode < 2)
			{
				gluPerspective(45.0f,(GLfloat)viewWidth / (GLfloat)viewHeight,0.1f,100.0f);
				int z = windowHeight - (viewPortY + viewHeight);
				glViewport(viewPortX, z, viewWidth, viewHeight);
				glTranslatef(0.0f,0.0f,-1.0f); // -2.4f  - move x units into the screen. was 1.0
			} else
			{
				gluPerspective(45.0f,(GLfloat)viewWidth / (GLfloat)viewHeight,0.1f,100.0f);
				//glViewport (0, windowHeight, windowWidth, windowHeight);
				glTranslatef(0.0f,0.0f,-1.2f); // -2.4f  - move x units into the screen. was 1.0
		}

			glMatrixMode(GL_MODELVIEW);
		}

		public static void DrawConsoleBackground()
		{
			/* Draws a transparent box with yellow border around the console window whilst exploring and in large 3D view mode */
			if ((plyr.status != 2) && (graphicMode == 2)) // Whilst not shopping
			{
				sf.RectangleShape rectangle = new sf.RectangleShape();
				rectangle.setSize(sf.Vector2f(670, 182)); // 672, 184
				rectangle.setOutlineColor(sf.Color.Yellow);
				rectangle.setFillColor(sf.Color(0, 0, 0, 128));
				rectangle.setOutlineThickness(1);
				rectangle.setPosition(consoleX - 16, consoleY); // Offset to give a 16 pixel border to text
				App.draw(rectangle);
			}
		}
		public static void DisplayLoading()
		{
			ClearDisplay();
			DrawText(loadingX, loadingY, "Loading...");
			UpdateDisplay();
		}
		public static void DisplayMainMenu()
		{
			RandomNumbers.Seed(time(null));
			DrawLogo();
			int tempy = (windowHeight - (180 + 240)) / 2;
			int z = (240) / 18;
			//int z = 12;
			int x = 2;
			//drawText(1,0,"x");
			DrawText(x + 3, z, "(1) Create a new City character");
			DrawText(x + 3, z + 1, "(2) Create a new Dungeon character");
			DrawText(x + 3, z + 2, "(3) Resume a character");
			DrawText(x + 3, z + 3, "(4) Acknowledgements");
			//drawText(x+3,z+5,"(5) Modify view:");
			DrawText(x + 3, z + 6, "(6) Modify audio:");
			DrawText(x + 3, z + 7, "(7) Modify font:");
			DrawText(x + 3, z + 9, "(0) Leave the game");


			//if (graphicMode == ATARI_SMALL) { drawText(x+21,z+5,"Atari 8bit"); }
			//if (graphicMode == ALTERNATE_SMALL) { drawText(x+21,z+5,"Small 3D"); }
			//if (graphicMode == ALTERNATE_LARGE) { drawText(x+21,z+5,"Large 3D"); }

			if (plyr.musicStyle == 0)
				DrawText(x + 21, z + 6, "Atari 8bit");
			else
				DrawText(x + 21, z + 6, "Alternate");
			if (plyr.fontStyle == 0)
				DrawText(x + 21, z + 7, "Smooth");
			else
				DrawText(x + 21, z + 7, "Atari 8bit");
		}
		public static void DisplayAcknowledgements()
		{
			bool acknowledgements = true;

			while (acknowledgements)
			{
				ClearDisplay();
				SetFontColour(40, 96, 244, 255);
				DrawText(1, 3, "Alternate Reality X       New Music");
				DrawText(1, 6, "Alternate Reality Copyright and Concept");
				DrawText(1, 9, "Alternate Reality: The Dungeon");
				DrawText(1, 12, "Original AR Music");
				DrawText(1, 15, "Original AR Artwork          New Art");
				DrawText(1, 19, "Disassembly Genius");
				DrawText(1, 22, "Additional Programming");

				SetFontColour(215, 215, 215, 255);
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
				SetFontColour(40, 96, 244, 255);
				DrawText(1, 3, "   Special thanks for supporting the");
				DrawText(1, 5, "    development of release 0.82 to:");

				SetFontColour(215, 215, 215, 255);
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
				SetFontColour(40, 96, 244, 255);
				DrawText(1, 3, "    Special thanks for their support");

				SetFontColour(215, 215, 215, 255);
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


		public static void ClearDisplay()
		{
			glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
			App.pushGLStates();
		}

		public static void UpdateDisplay()
		{
			App.popGLStates();
			App.display();
		}
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//void ClearDisplay(); // clear display prior to displaying non openGL scene
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//void UpdateDisplay();

		public static void DispMain()
		{
			// All non OpenGL drawing should be performed between the pushGLStates and popGLStates commands
			// Need to determine here if 3D view or shop image

			Draw3DView();
			DrawStatsPanel();
			DrawCompass();
			DrawAutomap();
			if ((graphicMode == (int)DisplayOptions.AlternateLarge) && (plyr.status != 3))
				DrawConsoleBackground();
			//updateWeatherDisplay();
			//updateEncounterAnimation();
			//drawInfoPanels(); moved out of dispMain so as not to overlap with USE or ENCOUNTER text in bottom half of display
			//App.popGLStates() and App.Display() will be carried out later once encounter or use object text added to display

			// App.popGLStates will follow other SFML 2D drawing
		}
		//void draw3DBackground();
		public static void DrawImage(string imagename, int x, int y)
		{
			// Counter images for Dungeon gate character creation
			if (plyr.scenario == 1)
			{
			if (imagename == "0")
				counterImage.setTexture(img0);
			if (imagename == "1")
				counterImage.setTexture(img1);
			if (imagename == "2")
				counterImage.setTexture(img2);
			if (imagename == "3")
				counterImage.setTexture(img3);
			if (imagename == "4")
				counterImage.setTexture(img4);
			if (imagename == "5")
				counterImage.setTexture(img5);
			if (imagename == "6")
				counterImage.setTexture(img6);
			if (imagename == "7")
				counterImage.setTexture(img7);
			if (imagename == "8")
				counterImage.setTexture(img8);
			if (imagename == "9")
				counterImage.setTexture(img9);
			} else
			{
			if (imagename == "0")
				counterImage.setTexture(imgc0);
			if (imagename == "1")
				counterImage.setTexture(imgc1);
			if (imagename == "2")
				counterImage.setTexture(imgc2);
			if (imagename == "3")
				counterImage.setTexture(imgc3);
			if (imagename == "4")
				counterImage.setTexture(imgc4);
			if (imagename == "5")
				counterImage.setTexture(imgc5);
			if (imagename == "6")
				counterImage.setTexture(imgc6);
			if (imagename == "7")
				counterImage.setTexture(imgc7);
			if (imagename == "8")
				counterImage.setTexture(imgc8);
			if (imagename == "9")
				counterImage.setTexture(imgc9);
		}

			counterImage.setPosition(gateX + x, gateY + y);
			App.draw(counterImage);

		}
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//void DrawText(int x, int y, string txt);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//void InitGL();
		public static void SetScreenValues()
		{
			// Determines screen element locations based on window dimensions
			gateX = (windowWidth - 640) / 2;
			gateY = ((windowHeight - 384) / 2) - 78;
			loadingX = 16;
			loadingY = 11;
			int temp = 0;
			int spacer = 0; // spacer value between screen elements - adjust here
			int consoleHeight = (16 + 2) * 10; // How tall is 10 lines of console text (16 pixels with 2 pixel space between lines)
			statPanelX = (windowWidth - 640) / 2; // Centre in middle of window width
			consoleX = (windowWidth - 640) / 2; // Centre in middle of window width

			/* Original small 3D view */
			if (graphicMode < 2)
			{
				//viewWidth = windowWidth/2.22;
				//viewHeight = viewWidth/2;
				viewWidth = 288;
				viewHeight = 144;
				viewPortX = (windowWidth - viewWidth) / 2;
				temp = 110 + spacer + viewHeight + spacer + consoleHeight;
				statPanelY = (windowHeight - temp) / 2;
				viewPortY = statPanelY + 110 + spacer;
				consoleY = viewPortY + viewHeight + 4;
				miniMapX = windowWidth - (((((windowWidth - viewWidth) / 2) - 144) / 2) + 144);
				miniMapY = spacer + viewPortY + ((viewHeight - 144) / 2);
			}

			/* New large 3D view */
			if (graphicMode == 2)
			{
				viewWidth = windowWidth;
				viewHeight = windowHeight;
				viewPortX = 0;
				viewPortY = windowHeight - viewHeight;
				statPanelY = 16;
				consoleY = (windowHeight - 32) - 144; // 16 x 10 not quite right
				miniMapX = (windowWidth - 16) - 176;
				miniMapY = (windowHeight - 176) / 2;
			}

			/* Shop positions are the same regardless of small / large 3D view choice */
			temp = 110 + spacer + 144 + 16 + consoleHeight;
			shopStatsY = ((windowHeight - temp) / 2);
			shopStatsY = statPanelY;
			shopPictureY = shopStatsY + 110 + spacer;
			shopConsoleY = shopPictureY + 144 + spacer; // 16 to put space between image and console text

			/* Misc assignments for positioning */

			lyricX = (windowWidth - 640) / 2;
			lyricY = shopPictureY - 18;

		}


		public static void DrawInfoPanels()
		{

		//if (graphicMode==2) drawConsoleBackground();

		if (plyr.status != 3)
		{
			if (plyr.infoPanel == 1)
			{

				if ((plyr.special >= 0xc0) && (plyr.special <= 0xff) && (plyr.scenario == 1)) // 224 - 255 was d2
				{
					string str;
					int ind = (plyr.special - 0xc0);
					  str = roomMessages[ind];
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
					if (plyr.scenario == 1)
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
					if (plyr.scenario == 2)
					{
						  BText(11, 5, "You are in the Arena");
						  BText(12, 6, "of Xebec's Demise");
					}

					string thirstDesc = CheckThirst();
					BText(1, 7, thirstDesc);
					string hungerDesc = CheckHunger();
					BText(1, 8, hungerDesc);
					string alcoholDesc = CheckAlcohol();
					BText(1, 9, alcoholDesc);
					string weightDesc = CheckEncumbrance();
					BText(30, 7, weightDesc);
					string poisonDesc = CheckPoison();
					BText(31, 8, poisonDesc);
					string diseaseDesc = CheckDisease();
					BText(31, 9, diseaseDesc);

			}

			}


			   if (plyr.infoPanel == 2)
			   {

		//        int weapon = objectBuffer[plyr.priWeapon].index;
				//drawTop();
				BText(1, 1, "Gold Coins   Silver Coins   Copper Coins");
				BText(3, 2, plyr.gold);
				BText(17, 2, plyr.silver);
				BText(32, 2, plyr.copper);


				//drawText(1, 1, "  %d             %d             %d",plyr.gold, plyr.silver, plyr.copper);

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

		//        int weapon = objectBuffer[plyr.priWeapon].index;
				//drawTop();
				BText(18, 1, "Weapons");
				string str = "Primary: Bare hand";
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
					str = str = itemBuffer[plyr.legsArmour].name;
					;
				}
				BText(7, 9, str);
			}

			if (plyr.infoPanel == 4)
			{
				CyText(1, "Apparel");
				if ((plyr.clothing[0] == 255) && (plyr.clothing[1] == 255) && (plyr.clothing[2] == 255) && (plyr.clothing[3] == 255))
					CyText(3, "Birthday suit");
				int y = 3;
				//int c3 = plyr.clothing[3];
				//itemBuffer[plyr.clothing[3]].index;
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
				int y = 3; // starting value for displaying items
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
				int y = 3; // starting value for displaying items
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

			//if (plyr.status==3) App.clear(sf::Color(0,0,0,192)); // if in combat wipe the panel
		}
		}
		public static void DrawStatsPanel()
		{
			if ((graphicMode == 2) && (plyr.status != 5) && (plyr.status != 2)) // not shopping
			{
				sf.RectangleShape rectangle = new sf.RectangleShape();
				rectangle.setSize(sf.Vector2f(640, 110)); // 640, 110
				rectangle.setOutlineColor(sf.Color.Yellow);
				//rectangle.setFillColor(sf::Color(255, 255, 255, 0));
				rectangle.setOutlineThickness(1);
				rectangle.setPosition(statPanelX, statPanelY - 1);
				App.draw(rectangle);
			}

			Banner.setPosition(statPanelX,statPanelY - 1);
			if (plyr.status == 2)
			{
				int statsX = (windowWidth - 640) / 2;
				int statsY = ((windowHeight - 144) / 2) - 126; // 144 pixels for picture + 16 space + stats height
				Banner.setPosition(statsX,shopStatsY - 1);
			}
			App.draw(Banner);
			if ((plyr.status == 1) || (plyr.status == 3))
			{
				BannerStrip.setPosition(statPanelX,(statPanelY + 89));
				App.draw(BannerStrip);
			}
			if (plyr.status != GameStates.Module)
				App.draw(BannerStrip);
			if (!plyr.diagOn)
			{
				SetFontColour(162, 114, 64, 255);
				DrawText(2, 0, plyr.name);
				DrawText(32, 0, "Level:");
				DrawText(38, 0, plyr.level);
				SetFontColour(147, 69, 130, 255);
				DrawText(2, 1, "Stats:  STA  CHR  STR  INT  WIS  SKL");
				SetFontColour(138, 68, 158, 255);
				DrawText(11, 2, plyr.sta);
				DrawText(16, 2, plyr.chr);
				DrawText(21, 2, plyr.str);
				DrawText(26, 2, plyr.inte);
				DrawText(31, 2, plyr.wis);
				DrawText(36, 2, plyr.skl);

				SetFontColour(62, 106, 162, 255);
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

				string str;
				str = "You are " + descriptions[plyr.location];

				// Draw status line text
				CheckForItemsHere();

				SetFontColour(102, 149, 40, 255);

				if ((plyr.status_text != " ") && (plyr.status != 3) && (plyr.alive))
					//App.draw(BannerStrip);
					DrawText(2, 5, plyr.status_text);

				SetFontColour(102, 149, 40, 255);
				if (plyr.alive)
					DrawText(2, 4, str);
				else
					DrawText(12, 4, "$ Where are you? $");

				SetFontColour(215, 215, 215, 255); // set text colour to white for all other text
			}

			// Diag Text

			if (plyr.diagOn)
			{
				string zoneDesc = "X:" + Itos(plyr.x) + "  Y:" + Itos(plyr.y) + "  Special:" + Itos(plyr.special) + "  Zone:" + Itos(plyr.zone) + "  Set:" + Itos(plyr.zoneSet);
				DrawText(2, 0, zoneDesc);
				zoneDesc = "Front:" + Itos(plyr.front) + "  Left:" + Itos(plyr.left) + "  Right:" + Itos(plyr.right) + "  Back:" + Itos(plyr.back);
				DrawText(2, 1, zoneDesc);
				string text;
				std::stringstream @out = new std::stringstream();
				@out << "Offset:" << plyr.z_offset;
				text = @out.str();
				DrawText(2, 5, text);
				zoneDesc = "Floor:" + Itos(zones[plyr.zoneSet].floor) + "  Ceiling:" + Itos(zones[plyr.zoneSet].ceiling);
				DrawText(2, 2, zoneDesc);
				zoneDesc = "Location:" + Itos(plyr.location);
				DrawText(2, 3, zoneDesc);
				int e = ReturnCarriedWeight();
				zoneDesc = "Encumbrance:" + Itos(e);
				DrawText(2, 4, zoneDesc);
				zoneDesc = "T: " + Itos(plyr.hours) + ":" + Itos(plyr.minutes);
				DrawText(30, 4, zoneDesc);
				//zoneDesc = "Buffer Ref:"+itos(plyr.buffer_index);
				//drawText(2,2,zoneDesc);
			}
			//string hourDesc = "th";
			//str = "It is " + itos(plyr.minutes) + " minutes past the " + itos(plyr.hours) + hourDesc + " hour";
			//cyText(10,str);

			/*
			std::string text;
			std::stringstream out;
			out << plyr.z_offset;
			text = out.str();
			drawText(22,6,text);
			//out << plyr.front;
			//text = out.str();
			//drawText(35,6,text);
			*/

		}

		public static void DrawCompass()
		{
			if (plyr.compasses > 0)
			{
				if ((plyr.status != 2) && (graphicMode == 2)) // if exploring and full screen draw a background
				{
					int x = 16;
					int y = (windowHeight - 128) / 2;

					sf.RectangleShape rectangle = new sf.RectangleShape();
					rectangle.setSize(sf.Vector2f(130, 130));
					rectangle.setOutlineColor(sf.Color.Yellow);
					rectangle.setFillColor(sf.Color(0, 0, 0, 128));
					rectangle.setOutlineThickness(1);
					rectangle.setPosition(x, y); // Offset to give a 16 pixel border to text
					App.draw(rectangle);
				}

				if (plyr.facing == 1)
					compass.setTexture(compassW);
				if (plyr.facing == 2)
					compass.setTexture(compassN);
				if (plyr.facing == 3)
					compass.setTexture(compassE);
				if (plyr.facing == 4)
					compass.setTexture(compassS);

				if (graphicMode == 2)
				{
					int x = 16;
					int y = (windowHeight - 128) / 2;
					compass.setPosition(x, y);
				} else
				{
					/* Normal Small 3D view mode */
					int x = (viewPortX - 128) / 2;
					int y = viewPortY + ((viewHeight - 128) / 2);
					compass.setPosition(x, y);
			}
				App.draw(compass);
			}
		}
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
	//void FlashView();

		public static void ClearShopDisplay()
		{
			//App.clear(sf::Color::Black);
			App.clear();
			App.pushGLStates();
			App.draw(ShopSprite);
			DrawStatsPanel();
		}
		public static void LoadShopImage(int imageno)
		{
			//consoleY = ((windowHeight-144)/2)+144+16;
			//cout << consoleY;
			if (graphicMode == 0)
			{
				if (imageno == 1)
					ShopImage.loadFromFile("data/images/locations/retreat.png");
				if (imageno == 2)
					ShopImage.loadFromFile("data/images/locations/rathskeller.png");
				if (imageno == 3)
					ShopImage.loadFromFile("data/images/locations/oDamon.png");
				if (imageno == 4)
					ShopImage.loadFromFile("data/images/locations/evilGuild.png");
				if (imageno == 5)
					ShopImage.loadFromFile("data/images/locations/goodGuild.png");
				if (imageno == 6)
					ShopImage.loadFromFile("data/images/locations/stairwayUp.png");
				if (imageno == 7)
					ShopImage.loadFromFile("data/images/locations/stairwayDown.png");
				if (imageno == 8)
					ShopImage.loadFromFile("data/images/locations/citySmithyNight.png");
				if (imageno == 9)
					ShopImage.loadFromFile("data/images/locations/imgCitySmithy.png");
				if (imageno == 10)
					ShopImage.loadFromFile("data/images/locations/imgCityTavern.png");
				if (imageno == 11)
					ShopImage.loadFromFile("data/images/locations/imgCityInn.png");
				if (imageno == 12)
					ShopImage.loadFromFile("data/images/locations/imgCityShop.png");
				if (imageno == 13)
					ShopImage.loadFromFile("data/images/locations/imgCityBank.png");
				if (imageno == 14)
					ShopImage.loadFromFile("data/images/locations/imgCityGuild.png");
				if (imageno == 15)
					ShopImage.loadFromFile("data/images/locations/imgCityHealer.png");
				if (imageno == 16)
					ShopImage.loadFromFile("data/images/locations/trolls.png");
				if (imageno == 17)
					ShopImage.loadFromFile("data/images/locations/goblins.png");
				if (imageno == 18)
					ShopImage.loadFromFile("data/images/locations/chapel.png");
				if (imageno == 19)
					ShopImage.loadFromFile("data/images/locations/fountain.png");
				if (imageno == 20)
					ShopImage.loadFromFile("data/images/locations/oracle.png");
				if (imageno == 21)
					ShopImage.loadFromFile("data/images/locations/imgCityHealer.png");
				if (imageno == 22)
					ShopImage.loadFromFile("data/images/locations/lift.png");
				if (imageno == 23)
					ShopImage.loadFromFile("data/images/locations/ferry.png");
				if (imageno == 24)
					ShopImage.loadFromFile("data/images/locations/undead.png");
				if (imageno == 25)
					ShopImage.loadFromFile("data/images/locations/arena.png");
				if (imageno == 26)
					ShopImage.loadFromFile("data/images/locations/dwarvenSmithy.png");
				if (imageno == 27)
					ShopImage.loadFromFile("data/images/locations/6.png");
			}
			if (graphicMode > 0)
			{
				if (imageno == 9)
					ShopImage.loadFromFile("data/images/locations2/smithy.png");
				if (imageno == 10)
					ShopImage.loadFromFile("data/images/locations2/tavern.png");
				if (imageno == 11)
					ShopImage.loadFromFile("data/images/locations2/inn.png");
				if (imageno == 12)
					ShopImage.loadFromFile("data/images/locations2/shop.png");
				if (imageno == 13)
					ShopImage.loadFromFile("data/images/locations2/bank.png");
				if (imageno == 14)
					ShopImage.loadFromFile("data/images/locations2/guild.png");
				if (imageno == 15)
					ShopImage.loadFromFile("data/images/locations2/healer1.png");
				if (imageno == 16)
					ShopImage.loadFromFile("data/images/locations2/trolls.png");
				if (imageno == 17)
					ShopImage.loadFromFile("data/images/locations2/goblins.png");
				if (imageno == 1)
					ShopImage.loadFromFile("data/images/locations2/inn.png");
				if (imageno == 2)
					ShopImage.loadFromFile("data/images/locations2/rathskeller.png");
				if (imageno == 3)
					ShopImage.loadFromFile("data/images/locations2/shop.png");
				if (imageno == 4)
					ShopImage.loadFromFile("data/images/locations2/guild.png");
				if (imageno == 5)
					ShopImage.loadFromFile("data/images/locations2/guild.png");
				if (imageno == 6)
					ShopImage.loadFromFile("data/images/locations/stairwayUp.png");
				if (imageno == 7)
					ShopImage.loadFromFile("data/images/locations/stairwayDown.png");
				if (imageno == 8)
					ShopImage.loadFromFile("data/images/locations/citySmithyNight.png");
				if (imageno == 18)
					ShopImage.loadFromFile("data/images/locations/chapel.png");
				if (imageno == 19)
					ShopImage.loadFromFile("data/images/locations/fountain.png");
				if (imageno == 20)
					ShopImage.loadFromFile("data/images/locations/oracle.png");
				if (imageno == 21)
					ShopImage.loadFromFile("data/images/locations2/healer2.png");
				if (imageno == 22)
					ShopImage.loadFromFile("data/images/locations/lift.png");
				if (imageno == 23)
					ShopImage.loadFromFile("data/images/locations2/river.png");
				if (imageno == 24)
					ShopImage.loadFromFile("data/images/locations2/undead.png");
				if (imageno == 25)
					ShopImage.loadFromFile("data/images/locations/arena.png");
			}


			ShopSprite.setTexture(ShopImage);
			ShopSprite.setScale(2.0, 2.0);
			ShopSprite.setPosition(((windowWidth - 640) / 2),shopPictureY);

		}


		public static int CheckCityDoors()
		{
			return 0;
		}

		public static void LoadLogoImage()
		{
			int x;
			int y;
			LogoImage.loadFromFile("data/images/logo640x240.png");
			LogoSprite.setTexture(LogoImage);
			x = (windowWidth - 640) / 2;
			y = (windowHeight - (180 + 240)) / 2;
			LogoSprite.setPosition(x,y);

		}
		public static void DrawLogo()
		{
			App.draw(LogoSprite);
		}

		//extern sf::RenderWindow App;
		//extern int graphicMode;
		//extern int viewWidth, viewHeight, viewPortX, viewPortY;
		//extern bool animationNotStarted;
		//extern int firstFrame;
		//extern int lastFrame;


		public static sf.Sprite encImage = new sf.Sprite();

		public static string version = "0.82";

		public static int windowMode;
		public static int graphicMode;
		public static int windowWidth;
		public static int windowHeight;
		public static int viewWidth;
		public static int viewHeight;
		public static int viewPortX;
		public static int viewPortY;
		public static int statPanelX; // x starting position for displaying the stats banner for centering
		public static int statPanelY;
		public static int consoleY; // y position for displaying the bottom screen info panel
		public static int consoleX; // x starting position for displaying the panel for centering
		public static int miniMapY; // y position for displaying the bottom screen info panel
		public static int miniMapX; // x starting position for displaying the panel for centering
		public static int lyricX;
		public static int lyricY;
		public static int loadingX;
		public static int loadingY;
		public static int gateX;
		public static int gateY;
		public static int shopStatsY; // y position for stats in shops
		public static int shopConsoleY; // y position for console in shops
		public static int shopPictureY;

		//extern string roomMessages[255];
		//extern string descriptions[255];

		//extern buffer_item itemBuffer[100];
		//extern effectItem effectBuffer[50]; // active time limited effects from spells, scrolls, eyes

		// Main window
		public static sf.RenderWindow App = new sf.RenderWindow();

		public static bool mainMenuQuit = false;

		public static sf.Texture img0 = new sf.Texture();
		public static sf.Texture img1 = new sf.Texture();
		public static sf.Texture img2 = new sf.Texture();
		public static sf.Texture img3 = new sf.Texture();
		public static sf.Texture img4 = new sf.Texture();
		public static sf.Texture img5 = new sf.Texture();
		public static sf.Texture img6 = new sf.Texture();
		public static sf.Texture img7 = new sf.Texture();
		public static sf.Texture img8 = new sf.Texture();
		public static sf.Texture img9 = new sf.Texture();
		public static sf.Texture imgDungeonGate = new sf.Texture();
		public static sf.Texture imgCityGate = new sf.Texture();
		public static sf.Texture imgc0 = new sf.Texture();
		public static sf.Texture imgc1 = new sf.Texture();
		public static sf.Texture imgc2 = new sf.Texture();
		public static sf.Texture imgc3 = new sf.Texture();
		public static sf.Texture imgc4 = new sf.Texture();
		public static sf.Texture imgc5 = new sf.Texture();
		public static sf.Texture imgc6 = new sf.Texture();
		public static sf.Texture imgc7 = new sf.Texture();
		public static sf.Texture imgc8 = new sf.Texture();
		public static sf.Texture imgc9 = new sf.Texture();
		public static sf.Texture consoleImage = new sf.Texture();
		public static sf.Texture BannerImageCity = new sf.Texture();
		public static sf.Texture BannerImageStrip = new sf.Texture();
		public static sf.Texture compassN = new sf.Texture();
		public static sf.Texture compassS = new sf.Texture();
		public static sf.Texture compassW = new sf.Texture();
		public static sf.Texture compassE = new sf.Texture();

		public static sf.Sprite Banner = new sf.Sprite();
		public static sf.Sprite BannerStrip = new sf.Sprite();
		public static sf.Sprite counterImage = new sf.Sprite();
		public static sf.Sprite dungeonGate = new sf.Sprite();
		public static sf.Sprite cityGate = new sf.Sprite();
		public static sf.Sprite compass = new sf.Sprite();
		public static sf.Sprite ShopSprite = new sf.Sprite();
		public static sf.Sprite LogoSprite = new sf.Sprite();
		public static sf.Texture ShopImage = new sf.Texture();
		public static sf.Texture LogoImage = new sf.Texture();
		public static string olddrawText; // text string used for setting and passing strings to the print routine

		// If image2 == 255 then just display image1 rather than use animations below
		//Dungeon Monster Animation Scripts

		public static AnimFrame[] encounterAnim =
		{
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 0, duration = 12 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 1, duration = 12 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 2, duration = 12 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 3, duration = 111 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 3, duration = 111 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 6, duration = 100 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 7, duration = 36 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 8, duration = 36 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 9, duration = 48 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 10, duration = 48 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 11, duration = 32 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 12, duration = 16 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 13, duration = 16 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 14, duration = 16 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 15, duration = 16 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 16, duration = 36 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 17, duration = 100 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 18, duration = 50 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 19, duration = 50 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 20, duration = 144 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 21, duration = 144 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 22, duration = 144 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 23, duration = 144 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 24, duration = 20 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 25, duration = 18 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 26, duration = 18 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 27, duration = 18 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 28, duration = 88 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 29, duration = 88 },
			new AnimFrame() { xOffset = 154, yOffset = 9, image = 30, duration = 20 },
			new AnimFrame() { xOffset = 150, yOffset = 11, image = 31, duration = 24 },
			new AnimFrame() { xOffset = 153, yOffset = 13, image = 30, duration = 20 },
			new AnimFrame() { xOffset = 156, yOffset = 11, image = 31, duration = 24 },
			new AnimFrame() { xOffset = 157, yOffset = 10, image = 30, duration = 20 },
			new AnimFrame() { xOffset = 160, yOffset = 9, image = 31, duration = 24 },
			new AnimFrame() { xOffset = 159, yOffset = 11, image = 30, duration = 20 },
			new AnimFrame() { xOffset = 157, yOffset = 12, image = 31, duration = 24 },
			new AnimFrame() { xOffset = 154, yOffset = 9, image = 32, duration = 20 },
			new AnimFrame() { xOffset = 150, yOffset = 11, image = 33, duration = 24 },
			new AnimFrame() { xOffset = 153, yOffset = 13, image = 32, duration = 20 },
			new AnimFrame() { xOffset = 156, yOffset = 11, image = 33, duration = 24 },
			new AnimFrame() { xOffset = 157, yOffset = 10, image = 32, duration = 20 },
			new AnimFrame() { xOffset = 160, yOffset = 9, image = 33, duration = 24 },
			new AnimFrame() { xOffset = 159, yOffset = 11, image = 32, duration = 20 },
			new AnimFrame() { xOffset = 157, yOffset = 12, image = 33, duration = 24 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 34, duration = 88 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 35, duration = 88 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 36, duration = 16 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 37, duration = 16 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 38, duration = 16 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 39, duration = 16 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 38, duration = 16 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 40, duration = 42 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 41, duration = 42 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 43, duration = 20 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 44, duration = 18 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 45, duration = 18 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 42, duration = 18 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 46, duration = 35 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 47, duration = 35 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 48, duration = 35 },
			new AnimFrame() { xOffset = 156, yOffset = 11, image = 49, duration = 42 },
			new AnimFrame() { xOffset = 155, yOffset = 10, image = 49, duration = 42 },
			new AnimFrame() { xOffset = 155, yOffset = 9, image = 49, duration = 42 },
			new AnimFrame() { xOffset = 156, yOffset = 10, image = 49, duration = 42 },
			new AnimFrame() { xOffset = 157, yOffset = 11, image = 49, duration = 42 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 50, duration = 22 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 51, duration = 22 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 52, duration = 22 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 53, duration = 22 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 54, duration = 40 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 55, duration = 10 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 56, duration = 56 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 57, duration = 28 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 58, duration = 33 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 59, duration = 33 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 60, duration = 64 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 61, duration = 64 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 62, duration = 10 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 63, duration = 10 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 64, duration = 10 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 65, duration = 10 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 66, duration = 10 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 67, duration = 10 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 68, duration = 56 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 69, duration = 28 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 70, duration = 40 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 71, duration = 40 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 72, duration = 40 },
			new AnimFrame() { xOffset = 157, yOffset = 8, image = 73, duration = 18 },
			new AnimFrame() { xOffset = 157, yOffset = 8, image = 74, duration = 19 },
			new AnimFrame() { xOffset = 157, yOffset = 8, image = 75, duration = 19 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 76, duration = 100 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 77, duration = 100 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 78, duration = 32 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 79, duration = 32 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 80, duration = 32 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 81, duration = 32 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 82, duration = 32 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 83, duration = 32 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 84, duration = 18 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 85, duration = 18 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 86, duration = 10 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 87, duration = 14 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 88, duration = 32 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 89, duration = 32 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 90, duration = 32 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 91, duration = 32 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 92, duration = 32 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 93, duration = 32 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 94, duration = 32 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 95, duration = 32 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 96, duration = 32 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 97, duration = 32 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 98, duration = 32 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 99, duration = 32 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 100, duration = 32 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 101, duration = 48 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 102, duration = 48 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 101, duration = 48 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 102, duration = 48 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 103, duration = 48 },
			new AnimFrame() { xOffset = 0, yOffset = 0, image = 102, duration = 48 }
		};
		// end of animation sequences excluding city images






		public static bool animationNotStarted;
		public static sf.Texture encImageSheet = new sf.Texture();
		public static int firstFrame;
		public static int lastFrame;
		public static int currentFrame; // within encounterAnim 0-7
		public static int yOffset;
		public static int xOffset;
		public static int animImage;
		public static int animDuration;


		// Disabled large and small_alternate options for release 0.75
		/*
		void drawEncounterAnimation()
		{
			sf::Texture encImageSheet;
			sf::Sprite largeEncImage; // only used for mode 2 full screen 3d view
		    int encWidth, encHeight, encX, encY;
		
		   // SET IMAGE WHICH IS TO BE USED (AT ITS ORIGINAL SIZE - UNSCALED)
		
			if (graphicMode==ATARI_SMALL) encImageSheet.loadFromFile("data/images/encounters/encounters.png"); // Atari 8bit
		
			if ((graphicMode==ALTERNATE_LARGE) || (graphicMode==ALTERNATE_SMALL))
			{
				encImageSheet.loadFromFile("data/images/encounters/nobleman.png");
				if (plyr.encounterRef<6) encImageSheet.loadFromFile("data/images/encounters/thief.png");
				if (plyr.encounterRef==0) encImageSheet.loadFromFile("data/images/encounters/devourer.png"); // Devourer
				if (plyr.encounterRef==9) encImageSheet.loadFromFile("data/images/encounters/knight.png");
				if (plyr.encounterRef==10) encImageSheet.loadFromFile("data/images/encounters/guard.png");
				if (plyr.encounterRef==12) encImageSheet.loadFromFile("data/images/encounters/knight.png");
				if (plyr.encounterRef==14) encImageSheet.loadFromFile("data/images/encounters/wizard.png");
				if (plyr.encounterRef==15) encImageSheet.loadFromFile("data/images/encounters/knight.png");
				if (plyr.encounterRef==17) encImageSheet.loadFromFile("data/images/encounters/nobleman.png");
				if (plyr.encounterRef==16) encImageSheet.loadFromFile("data/images/encounters/pauper2.png");
				if (plyr.encounterRef==20) encImageSheet.loadFromFile("data/images/encounters/novice.png");
				if (plyr.encounterRef==21) encImageSheet.loadFromFile("data/images/encounters/novice.png");
				if ((plyr.encounterRef>21) && (plyr.encounterRef<26)) encImageSheet.loadFromFile("data/images/encounters/wizard.png");
				if (plyr.encounterRef==35) encImageSheet.loadFromFile("data/images/encounters/rat.png");
				if (plyr.encounterRef==81) encImageSheet.loadFromFile("data/images/encounters/wizard.png");
				if (plyr.encounterRef==11) encImageSheet.loadFromFile("data/images/encounters/guard.png");
				if (plyr.encounterRef==12) encImageSheet.loadFromFile("data/images/encounters/guard.png");
				if (plyr.encounterRef==13) encImageSheet.loadFromFile("data/images/encounters/guard.png");
				if (plyr.encounterRef==31) encImageSheet.loadFromFile("data/images/encounters/bat.png");
				if (plyr.encounterRef==44) encImageSheet.loadFromFile("data/images/encounters/bat.png");
				if (plyr.encounterRef==53) encImageSheet.loadFromFile("data/images/encounters/guard.png");
				if (plyr.encounterRef==54) encImageSheet.loadFromFile("data/images/encounters/guard.png");
				if (plyr.encounterRef==56) encImageSheet.loadFromFile("data/images/encounters/guard.png");
				if (plyr.encounterRef==57) encImageSheet.loadFromFile("data/images/encounters/guard.png");
				if ((plyr.encounterRef==58) && (plyr.gender==1)) encImageSheet.loadFromFile("data/images/encounters/doppleganger-m.png");
				if ((plyr.encounterRef==58) && (plyr.gender==2)) encImageSheet.loadFromFile("data/images/encounters/doppleganger-f.png");
				if (plyr.encounterRef==57) encImageSheet.loadFromFile("data/images/encounters/adventurer512.png");
				if (plyr.encounterRef==76) encImageSheet.loadFromFile("data/images/encounters/guard.png");
				if (plyr.encounterRef==77) encImageSheet.loadFromFile("data/images/encounters/guard.png");
				if (plyr.encounterRef==29) encImageSheet.loadFromFile("data/images/encounters/slime.png");
				if (plyr.encounterRef==86) encImageSheet.loadFromFile("data/images/encounters/noblewoman.png");
				if (plyr.encounterRef==59) encImageSheet.loadFromFile("data/images/encounters/adventurer.png");
			}
		
			// Alternate and set animation frame as required
			encImage.setTexture(encImageSheet);
			encImageSheet.setSmooth(true);
			if (graphicMode == ALTERNATE_LARGE) largeEncImage.setTexture(encImageSheet);
			if (graphicMode == ALTERNATE_SMALL) largeEncImage.setTexture(encImageSheet);
		
		    // Original Atari 8bit image at original size
			if (graphicMode==ATARI_SMALL)
			{
				SetTileImage(plyr.encounterAnimationRef);
				if ((plyr.encounterAnimationRef==92) && (plyr.gender==2)) { SetTileImage(72); } // female doppelganger image
				// Nightstalker & dragon image position changes
				//if (plyr.encounterAnimationRef==86) { encImage.setPosition(306, 168); }
				//if (plyr.encounterAnimationRef==82) { encImage.setPosition(306, 176); }
			}
		
		
		
		    // NOW IMAGE CHOSEN NEED TO SET SCALE - SHRINK OR ENLARGE
		
		    // Calculate new image width and height based on viewport size
		    //encWidth = viewWidth / 4.5;
		    //encHeight = viewHeight / 1.125;
		
		    if (graphicMode==ATARI_SMALL)
			{
				// Scale large image based on viewport height and width
				//float scaleX = float(encWidth) / float(64);
				//float scaleY = float(encHeight) / float(128);
				//encImage.setScale(scaleX, scaleY);
			}
		
		
		    if (graphicMode==ALTERNATE_SMALL)
			{
			    // Adjustment for new art in small window
				// Scale large image based on window height and width
		        encWidth = viewWidth / 4.0; // adjustment to provide larger encounter image
				encHeight = encWidth*2;
				//if ((plyr.encounterRef==59)||(plyr.encounterRef==86)||(plyr.encounterRef==29)) encWidth=encWidth*2;
		
				float scaleX = float(encWidth) / float(256);
				//if ((plyr.encounterRef==59) || (plyr.encounterRef==86)) scaleX = float(encWidth) / float(512);
				float scaleY = float(encHeight) / float(512);
				largeEncImage.setScale(scaleX, scaleY);
			}
		
		    if (graphicMode==ALTERNATE_LARGE)
			{
				// For fullscreen mode ensure that aspect ratio of 1:2 is maintained for encounter image
				encWidth = viewWidth / 4.0; // adjustment to provide larger encounter image
				encHeight = encWidth*2;
				//if ((plyr.encounterRef==59)||(plyr.encounterRef==86)||(plyr.encounterRef==29)) encWidth=encWidth*2;
		
				float scaleX = float(encWidth) / float(256);
				//if ((plyr.encounterRef==59) || (plyr.encounterRef==86)) scaleX = float(encWidth) / float(512);
				float scaleY = float(encHeight) / float(512);
				largeEncImage.setScale(scaleX, scaleY);
			}
		
		
		    // SET POSITION OF RESIZED IMAGE ON SCREEN
		
		    encX = (windowWidth - encWidth)/2;
		    encY = ((viewPortY+viewHeight)-2)-encHeight;
		    if ((plyr.encounterRef==59)||(plyr.encounterRef==86)) encX = (windowWidth/2)-(encWidth);
		
		    if (graphicMode==ATARI_SMALL) { encImage.setPosition(encX,encY); }
		    if ((graphicMode==ALTERNATE_SMALL) || (graphicMode==2)) { largeEncImage.setPosition(encX,encY); }
		
		    // DRAW DISPLAY AND FINAL ENCOUNTER IMAGE
		    dispMain();
			if (graphicMode != ATARI_SMALL )  App.draw(largeEncImage);
			if (graphicMode == ATARI_SMALL ) App.draw(encImage);
		}
		
		*/

	}
}