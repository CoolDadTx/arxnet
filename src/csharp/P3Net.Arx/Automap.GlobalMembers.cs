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

namespace P3Net.Arx
{
    public static partial class GlobalMembers
    {
        public static void Automap ()
        {
            plyr.status = 0;

            var done = false;

            do
            {
                ClearDisplay();
                DrawFullAutomap();
                UpdateDisplay();

                switch (GetSingleKey())
                {
                    case "SPACE":
                    case "RETURN":
                    case "M":
                    case "ESC": done = true; break;
                };
            } while (!done);

            plyr.status = GameStates.Explore;
        }

        public static void SetAutoMapFlag ( int mapno, int x, int y )
        {
            var cellNo = GetMapIndex(x, y);
            autoMapExplored[mapno, cellNo] = true;
        }

        //TODO: Not used but we'll leave it for now
        //public static void ClearAutoMaps ()
        //{       
        //    for (var y = 0; y < 5; y++)
        //    {
        //        for (var x = 0; x < 4096; x++)
        //            autoMapExplored[y, x] = false;
        //    }
        //}

        public static void InitMap ()
        {
            cellImage.Texture = new Texture("data/images/maptiles.png");

            switch (plyr.scenario)
            {
                case Scenarios.City: mapLegend.Texture = new Texture("data/images/cityLegend.png"); break;
                case Scenarios.Dungeon: mapLegend.Texture = new Texture("data/images/dungeonLegend.png"); break;

                default: throw new NotSupportedException();
            };
        }

        public static void DrawAutomap ()
        {
            if (plyr.miniMapOn)
            {
                if ((graphicMode == GraphicsMode.AlternateLarge) && (plyr.status != GameStates.Module)) // shopping?
                {
                    var rectangle2 = new RectangleShape() {
                                            Size = new Vector2f(176, 176),
                                            OutlineColor = Color.Yellow,
                                            FillColor = new Color(0, 0, 0, 128),
                                            OutlineThickness = 1,
                                            Position = new Vector2f(miniMapX + 1, miniMapY + 1)
                                    };
                    App.Draw(rectangle2);
                }
                                
                var automapSize = new Drawing.Size(9, 9);// how many map cells displayed including central player cell + 1 for for loop 

                var startx = plyr.Position.X - ((automapSize.Width - 1) / 2);     // map cell coords for first x
                var starty = plyr.Position.Y - ((automapSize.Height - 1) / 2);    // map cell coords for first y

                for (var y = 0; y < automapSize.Height; y++)
                {
                    for (var x = 0; x < automapSize.Width; x++)
                    {
                        // check for valid on map square
                        var currentx = startx + x;
                        var currenty = starty + y;
                        if ((currentx >= 0) && (currentx < plyr.MapSize.Width) && (currenty >= 0) && (currenty < plyr.MapSize.Height))
                        {
                            var pixelx = miniMapX + (x * s_pixelSize); // 16 = pixels in cell image
                            var pixely = miniMapY + (y * s_pixelSize); // 16 = pixels in cell image
                            mapLocation = GetMapIndex(currentx, currenty);
                            DrawCell(currentx, currenty, pixelx, pixely);
                            if (!autoMapExplored[plyr.map, mapLocation])
                                DrawImage(pixelx, pixely, 24);
                        }
                    }
                }

                // Draw arrow to represent position and direction of player                
                int tile;
                switch (plyr.facing)
                {
                    case Directions.West: tile = 17; break;
                    case Directions.North: tile = 14; break;
                    case Directions.East: tile = 16; break;
                    case Directions.South: tile = 15; break;
                    default: throw new NotSupportedException();
                };

                var pixelPos = new Drawing.Point(miniMapX + (((automapSize.Width - 1) / 2) * s_pixelSize),
                                            miniMapY + (((automapSize.Height - 1) / 2) * s_pixelSize));
                DrawImage(pixelPos, tile);
            }
        }        
        
         #region Private Members

        //TODO: Probably should be tied to map
        private static void DrawFullAutomap ()
        {            
            plyr.drawingBigAutomap = true;            

            var automapSize = new Drawing.Size(32, 32);  // how many map cells displayed including central player cell + 1 for for loop

            // top left pixel coordinate for automap
            var corner = Drawing.Point.Empty;

            var startx = (plyr.Position.X < 32) ? 0 : 32;
            var starty = (plyr.Position.Y < 32) ? 0 : 32;

            for (var y = 0; y < automapSize.Height; y++)
            {
                for (var x = 0; x < automapSize.Width; x++)
                {
                    // check for valid on map square
                    var current = new Drawing.Point(startx + x, starty + y);
                    mapLocation = GetMapIndex(current);                    

                    // 16 = pixels in cell image
                    var pixel = new Drawing.Point(corner.X + (x * s_pixelSize), corner.Y + (y * s_pixelSize));                                        
                    DrawCell(current, pixel);

                    if (!autoMapExplored[plyr.map, mapLocation])
                        DrawImage(pixel, 24);
                }
            }

            //TODO: Seems like Min/Max would work here but what if X/Y is > 64?
            // Draw arrow to represent position and direction of player
            var arrowPos = new Drawing.Point((plyr.Position.X > 31 ? (plyr.Position.X - 32) : plyr.Position.X) * s_pixelSize,
                                            (plyr.Position.Y > 31 ? (plyr.Position.Y - 32) : plyr.Position.Y) * s_pixelSize
                                            );            
            int tile;
            switch (plyr.facing)
            {
                case Directions.West: tile = 17; break;
                case Directions.North: tile = 14; break;
                case Directions.East: tile = 16; break;
                case Directions.South: tile = 15; break;

                default: throw new NotSupportedException();
            }
            
            DrawImage(arrowPos, tile);            

            // Draw legend sprite
            mapLegend.Position = new Vector2f(512 + 16, 16);
            App.Draw(mapLegend);

            plyr.drawingBigAutomap = false;
        }

        // Draw an individual image to the display at pixel x,y
        private static void DrawImage ( int x, int y, int tileNo )
        {
            x++;
            y++;
            int row;
            int column;
            var tilesPerRow = 8; // number of tiles per row in font image containing all tiles (16 default)
            var tileSize = 16; // 16 pixels height and width

            //Select 16x16 pixel section of tile sheet for cell tile
            if (tileNo >= tilesPerRow)
            {
                column = (tileNo % tilesPerRow); // remainder
                row = ((tileNo - column) / tilesPerRow);
            } else
            {
                column = tileNo;
                row = 0; // = row 1 on the actual tile sheet at y=0
            }

            var tileX = (column) * tileSize; // x loc on tiles image in pixels
            var tileY = ((row) * tileSize); // y loc on tiles image in pixels
            cellImage.TextureRect = new IntRect(tileX, tileY, tileSize, tileSize);
            cellImage.Position = new Vector2f(x, y); // simply display at x,y pixel locations
            App.Draw(cellImage);
        }

        private static void DrawImage ( Drawing.Point position, int tileNo ) => DrawImage(position.X, position.Y, tileNo);

        // Draw all the images required for a single cell on the automap
        private static void DrawCell ( Drawing.Point position, Drawing.Point pixel ) => DrawCell(position.X, position.Y, pixel.X, pixel.Y);

        private static void DrawCell ( int x, int y, int pixelx, int pixely )
        {
            var idx = GetMapIndex(x, y);
            var special = levelmap[idx].special;

            // Draw cell background color
            if (autoMapExplored[plyr.map, mapLocation])
            {
                DrawImage(pixelx, pixely, 0);

                var specialTile = 0;
                switch (special)
                {
                    case 144:
                    case 21: specialTile = 13; break;

                    case 112:
                    case 0x0D: specialTile = 12; break;

                    case 80:
                    case 0x0C: specialTile = 9; break;

                    case 16:
                    case 0x0F: specialTile = 10; break;

                    case 48:
                    case 3:
                    case 35:
                    case 0x1D:
                    case 7: specialTile = 11; break;

                    case 19:
                    case 87: specialTile = 25; break;

                    case 208: specialTile = plyr.scenario == Scenarios.City ? 25 : 0; break;

                    case 0xF0: specialTile = plyr.scenario == Scenarios.City ? 9 : 0; break;
                };

                if (specialTile != 0)
                    DrawImage(pixelx, pixely, specialTile);
            };

            if (!autoMapExplored[plyr.map, mapLocation])
                DrawImage(pixelx, pixely, 24);

            // Standard Dungeon "special" ranges			
            if ((special >= 0xE0) && (special <= 0xFF) && (plyr.scenario == Scenarios.Dungeon))
                DrawImage(pixelx, pixely, 22);

            //TODO: All these switch statements are identical (ignoring North which is probably a bug) reacting to fixed values and picking 1 of 3 tile options - simplify this
            // switch statement to set value to image tile
            var tile = 0;
            switch (levelmap[idx].north)
            {
                case 3:
                case 4:
                case 8:
                case 9:
                case 10:
                case 19: tile = 8; break;

                case 5:
                case 6:
                case 13:
                case 14:
                case 37: tile = 4; break;
            };
            if (tile != 0)
                DrawImage(pixelx, pixely, tile);

            // switch statement to set value to image tile
            var south = levelmap[idx].south;
            switch (south)
            {
                case 3:
                case 4:
                case 8:
                case 9:
                case 10: tile = 6; break;

                case 5:
                case 6:
                case 13:
                case 14:
                case 37: tile = 2; break;

                default: tile = (south > 19) ? 6 : 0; break;
            };
            if (tile != 0)
                DrawImage(pixelx, pixely, tile);

            // switch statement to set value to image tile
            tile = 0;
            var west = levelmap[idx].west;
            switch (west)
            {
                case 3:
                case 4:
                case 8:
                case 9:
                case 10: tile = 5; break;

                case 5:
                case 6:
                case 13:
                case 14:
                case 37: tile = 1; break;

                default: tile = (west > 19) ? 5 : 0; break;
            };
            if (tile != 0)
                DrawImage(pixelx, pixely, tile);

            // switch statement to set value to image tile
            var east = levelmap[idx].east;
            switch (east)
            {
                case 3:
                case 4:
                case 8:
                case 9:
                case 10: tile = 7; break;

                case 5:
                case 6:
                case 13:
                case 14:
                case 37: tile = 3; break;

                default: tile = (east > 19) ? 7 : 0; break;                                };
            if (tile != 0)
                DrawImage(pixelx, pixely, tile);
        }

        private static int mapLocation;
        private static Sprite cellImage = new Sprite();
        private static Sprite mapLegend = new Sprite();

        private const int s_pixelSize = 16;
        
        #endregion
    }
}