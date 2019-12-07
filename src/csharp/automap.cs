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
        public static void Automap ()
        {
            plyr.status = 0;
            var mapComplete = false;

            while (!mapComplete)
            {
                ClearDisplay();
                DrawFullAutomap();
                UpdateDisplay();

                var single_key = GetSingleKey();
                if (single_key == "SPACE")
                    mapComplete = true;
                if (single_key == "RETURN")
                    mapComplete = true;
                if (single_key == "M")
                    mapComplete = true;
                if (single_key == "ESC")
                    mapComplete = true;
            }
            plyr.status = 1;
        }

        public static void SetAutoMapFlag ( int mapno, int x, int y )
        {
            var cellNo = GetMapIndex(x, y);
            autoMapExplored[mapno][cellNo] = true;
        }

        public static void ClearAutoMaps ()
        {
            for (var y = 0; y < 5; y++)
            {
                for (var x = 0; x < 4096; x++)
                    autoMapExplored[y][x] = false;
            }
        }

        public static void InitMap ()
        {
            mapImage.loadFromFile("data/images/maptiles.png");
            cellImage.setTexture(mapImage);

            if (plyr.scenario == 0)
                legendImage.loadFromFile("data/images/cityLegend.png");
            if (plyr.scenario == 1)
                legendImage.loadFromFile("data/images/dungeonLegend.png");
            mapLegend.setTexture(legendImage);
        }

        public static void DrawAutomap ()
        {
            if (plyr.miniMapOn)
            {
                if ((graphicMode == 2) && (plyr.status != 2)) // shopping?
                {
                    var rectangle2 = new sf.RectangleShape();
                    rectangle2.setSize(sf.Vector2f(176, 176)); // 672, 184
                    rectangle2.setOutlineColor(sf.Color.Yellow);
                    rectangle2.setFillColor(sf.Color(0, 0, 0, 128));
                    rectangle2.setOutlineThickness(1);
                    rectangle2.setPosition(miniMapX + 1, miniMapY + 1);
                    App.draw(rectangle2);
                }

                pixelSize = 16;
                var automapHeight = 9; // how many map cells displayed including central player cell + 1 for for loop
                var automapWidth = 9; //was 9

                var startx = 0; // map cell coords for first x
                var starty = 0; // map cell coords for first y
                var currentx = 0;
                var currenty = 0;
                var pixelx = 0;
                var pixely = 0;
                startx = plyr.x - ((automapWidth - 1) / 2);
                starty = plyr.y - ((automapHeight - 1) / 2);

                for (var y = 0; y < (automapHeight); y++)
                {
                    for (var x = 0; x < (automapWidth); x++)
                    {
                        // check for valid on map square
                        currentx = startx + x;
                        currenty = starty + y;
                        if ((currentx >= 0) && (currentx < plyr.mapWidth) && (currenty >= 0) && (currenty < plyr.mapHeight))
                        {
                            pixelx = miniMapX + (x * pixelSize); // 16 = pixels in cell image
                            pixely = miniMapY + (y * pixelSize); // 16 = pixels in cell image
                            mapLocation = GetMapIndex(currentx, currenty);
                            DrawCell(currentx, currenty, pixelx, pixely);
                            if (!autoMapExplored[plyr.map][mapLocation])
                                DrawImage(pixelx, pixely, 24);
                        }
                    }
                }

                // Draw arrow to represent position and direction of player
                pixelx = miniMapX + (((automapWidth - 1) / 2) * pixelSize);
                pixely = miniMapY + (((automapHeight - 1) / 2) * pixelSize);
                if (plyr.facing == 1)
                    DrawImage(pixelx, pixely, 17);
                if (plyr.facing == 2)
                    DrawImage(pixelx, pixely, 14);
                if (plyr.facing == 3)
                    DrawImage(pixelx, pixely, 16);
                if (plyr.facing == 4)
                    DrawImage(pixelx, pixely, 15);
            }
        }

        public static void DrawFullAutomap ()
        {
            plyr.drawingBigAutomap = true;
            pixelSize = 16;
            var automapHeight = 32; // how many map cells displayed including central player cell + 1 for for loop
            var automapWidth = 32;
            var cornerX = 0; // top left pixel coordinate for automap 522
            var cornerY = 0; // top left pixel coordinate for automap
            var startx = 0; // map cell coords for first x
            var starty = 0; // map cell coords for first y
            var currentx = 0;
            var currenty = 0;
            var pixelx = 0;
            var pixely = 0;
            if ((plyr.x < 32) && (plyr.y < 32))
            {
                startx = 0;
                starty = 0;
            }
            if ((plyr.x > 31) && (plyr.y < 32))
            {
                startx = 32;
                starty = 0;
            }
            if ((plyr.x > 31) && (plyr.y > 31))
            {
                startx = 32;
                starty = 32;
            }
            if ((plyr.x < 32) && (plyr.y>31))
            {
                startx = 0;
                starty = 32;
            }

            for (var y = 0; y < (automapHeight); y++)
            {
                for (var x = 0; x < (automapWidth); x++)
                {
                    // check for valid on map square
                    currentx = startx + x;
                    currenty = starty + y;
                    pixelx = cornerX + (x * pixelSize); // 16 = pixels in cell image
                    pixely = cornerY + (y * pixelSize); // 16 = pixels in cell image
                    mapLocation = GetMapIndex(currentx, currenty);
                    DrawCell(currentx, currenty, pixelx, pixely);
                    if (!autoMapExplored[plyr.map][mapLocation])
                        DrawImage(pixelx, pixely, 24);
                }
            }

            // Draw arrow to represent position and direction of player
            pixelx = (plyr.x) * pixelSize+16;
            pixely = (plyr.y) * pixelSize;
            if (plyr.y > 31)
                pixely = (plyr.y - 32) * pixelSize;
            if (plyr.x > 31)
                pixelx = (plyr.x - 32) * pixelSize;
            if (plyr.facing == 1)
                DrawImage(pixelx, pixely, 17);
            if (plyr.facing == 2)
                DrawImage(pixelx, pixely, 14);
            if (plyr.facing == 3)
                DrawImage(pixelx, pixely, 16);
            if (plyr.facing == 4)
                DrawImage(pixelx, pixely, 15);

            // Draw legend sprite
            mapLegend.setPosition(512 + 16, 16);
            App.draw(mapLegend);

            plyr.drawingBigAutomap = false;
        }

        public static sf.Texture mapImage = new sf.Texture();
        public static sf.Texture legendImage = new sf.Texture();
        public static sf.Sprite cellImage = new sf.Sprite();

        public static sf.Sprite mapLegend = new sf.Sprite();

        //float scale;
        public static int pixelSize;
        public static int mapLocation;

        // Draw an individual image to the display at pixel x,y
        public static void DrawImage ( int x, int y, int tileNo )
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
            cellImage.setTextureRect(sf.IntRect(tileX, tileY, tileSize, tileSize));
            cellImage.setPosition(x, y); // simply display at x,y pixel locations
            App.draw(cellImage);
        }

        // Draw all the images required for a single cell on the automap
        public static void DrawCell ( int x, int y, int pixelx, int pixely )
        {
            var idx = GetMapIndex(x, y);
            var north = levelmap[idx].north;
            var west = levelmap[idx].west;
            var east = levelmap[idx].east;
            var south = levelmap[idx].south;
            var special = levelmap[idx].special;
            int tile;

            // Draw cell background colour
            if (autoMapExplored[plyr.map][mapLocation])
            {
                DrawImage(pixelx, pixely, 0);
                if (special == 144)
                    DrawImage(pixelx, pixely, 13);
                if (special == 21)
                    DrawImage(pixelx, pixely, 13);
                if (special == 112)
                    DrawImage(pixelx, pixely, 12);
                if (special == 80)
                    DrawImage(pixelx, pixely, 9);
                if (special == 16)
                    DrawImage(pixelx, pixely, 10);
                if (special == 48)
                    DrawImage(pixelx, pixely, 11);
                if ((special == 208) && (plyr.scenario == 0))
                    DrawImage(pixelx, pixely, 25);
                if (special == 3)
                    DrawImage(pixelx, pixely, 11);
                if (special == 35)
                    DrawImage(pixelx, pixely, 11);
                if ((special == 0xF0) && (plyr.scenario == 0))
                    DrawImage(pixelx, pixely, 9);
                if (special == 0x0F)
                    DrawImage(pixelx, pixely, 10);
                if (special == 0x1D)
                    DrawImage(pixelx, pixely, 11);
                if (special == 0x0C)
                    DrawImage(pixelx, pixely, 9);
                if (special == 0x0D)
                    DrawImage(pixelx, pixely, 12);
                if (special == 7)
                    DrawImage(pixelx, pixely, 11);
                if (special == 19)
                    DrawImage(pixelx, pixely, 25);
                if (special == 87)
                    DrawImage(pixelx, pixely, 25);
            }

            if (!autoMapExplored[plyr.map][mapLocation])
                DrawImage(pixelx, pixely, 24);

            // Standard Dungeon "special" ranges			
            if ((special >= 0xE0) && (special <= 0xFF) && (plyr.scenario == 1))
                DrawImage(pixelx, pixely, 22);

            // switch statement to set value to image tile
            tile = 0;
            if (north == 3)
                tile = 8;
            if (north == 4)
                tile = 8;
            if (north == 5)
                tile = 4;
            if (north == 6)
                tile = 4;
            if (north == 8)
                tile = 8;
            if (north == 9)
                tile = 8;
            if (north == 10)
                tile = 8;
            if (north > 19)
                tile = 8;
            if (north == 13)
                tile = 4;
            if (north == 37)
                tile = 4;
            if (north == 14)
                tile = 4;


            if (tile != 0)
                DrawImage(pixelx, pixely, tile);

            // switch statement to set value to image tile
            tile = 0;
            if (south == 3)
                tile = 6;
            if (south == 4)
                tile = 6;
            if (south == 5)
                tile = 2;
            if (south == 6)
                tile = 2;
            if (south == 8)
                tile = 6;
            if (south == 9)
                tile = 6;
            if (south == 10)
                tile = 6;
            if (south > 19)
                tile = 6;
            if (south == 13)
                tile = 2;
            if (south == 37)
                tile = 2;
            if (south == 14)
                tile = 2;

            if (tile != 0)
                DrawImage(pixelx, pixely, tile);

            // switch statement to set value to image tile
            tile = 0;
            if (west == 3)
                tile = 5;
            if (west == 4)
                tile = 5;
            if (west == 5)
                tile = 1;
            if (west == 6)
                tile = 1;
            if (west == 8)
                tile = 5;
            if (west == 9)
                tile = 5;
            if (west == 10)
                tile = 5;
            if (west > 19)
                tile = 5;
            if (west == 13)
                tile = 1;
            if (west == 37)
                tile = 1;
            if (west == 14)
                tile = 1;
            if (tile != 0)
                DrawImage(pixelx, pixely, tile);

            // switch statement to set value to image tile
            tile = 0;
            if (east == 3)
                tile = 7;
            if (east == 4)
                tile = 7;
            if (east == 5)
                tile = 3;
            if (east == 6)
                tile = 3;
            if (east == 8)
                tile = 7;
            if (east == 9)
                tile = 7;
            if (east == 10)
                tile = 7;
            if (east > 19)
                tile = 7;
            if (east == 13)
                tile = 3;
            if (east == 37)
                tile = 3;
            if (east == 14)
                tile = 3;
            if (tile != 0)
                DrawImage(pixelx, pixely, tile);
        }

        //extern bool autoMapExplored[5][4096];
        //extern Mapcell levelmap[4096]; // 12288

        //extern int miniMapY; // y position for displaying the bottom screen info panel
        //extern int miniMapX; // x starting position for displaying the panel for centering
        //extern int graphicMode;

        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //void DrawImage(int topY, int x, int y, int char_no);
        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //void DrawCell(int x, int y);
    }
}