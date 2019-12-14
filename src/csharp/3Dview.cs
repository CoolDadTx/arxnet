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
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using SFML.Graphics;
using SFML.System;

namespace P3Net.Arx
{
    public partial class GlobalMembers
    {
        public static void Draw3DView ()
        {
            glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
            App.PushGLStates();
            Draw3DBackground(); // Draw SFML 2D item
            App.PopGLStates();
            BuildLevelView(); // Draw the OpenGL 3D corridor and room view
            App.PushGLStates();
        }

        public static void Draw3DBackground ()
        {
            var Background = new Sprite();
            Background.Scale = new Vector2f(1, 1);

            if (graphicMode == 0) // Atari 8bit original textures and size
            {
                var scaleX = viewWidth / 360F;
                var scaleY = viewHeight / 190F;
                Background.Scale = new Vector2f(scaleX, scaleY);
                Background.Position = new Vector2f(viewPortX, viewPortY);
            }
            if (graphicMode == 1) // New textures and original size
            {
                var scaleX = viewWidth / 1024F;
                var scaleY = (viewHeight / 2) / 384F;
                Background.Scale = new Vector2f(scaleX, scaleY);
                Background.Position = new Vector2f(viewPortX, viewPortY);
            }
            if (graphicMode == 2) // New textures and large size
            {
                Background.Position = new Vector2f(0, 0); // Assumes large 3D view
                var scaleX = viewWidth / 1024F;
                var scaleY = (viewHeight / 2) / 384F;
                Background.Scale = new Vector2f(scaleX, scaleY);
                Background.Position = new Vector2f(viewPortX, (viewPortY));
            }

            //TODO: Encapsulate this
            var texture = (plyr.scenario == 2) ? background[44] : null;

            switch (plyr.zoneSet)
            {
                case 0: texture = background[15]; break;
                case 1: texture = background[10]; break;
                case 2: texture = background[8]; break;
                case 4: texture = background[48]; break;
                case 5: texture = background[46]; break;
                case 11: texture = background[12]; break;
                case 14: texture = background[15]; break;
                case 16: texture = background[16]; break;
                case 18: texture = background[17]; break;
                case 21: texture = background[44]; break;
                case 22: texture = background[44]; break;
                case 23: texture = background[45]; break;
                case 24: texture = background[8]; break;
                case 25: texture = background[15]; break;
                case 26: texture = background[12]; break;
                case 27: texture = background[16]; break;
            };            

            if ((plyr.scenario == 0) && (plyr.zone == 99))
            {
                switch (plyr.timeOfDay)
                {
                    case 0:
                    {
                        switch (plyr.facing)
                        {
                            case Directions.West:
                            texture = background[7];
                            break;
                            case Directions.North:
                            texture = background[6];
                            break;
                            case Directions.East:
                            texture = background[4];
                            break;
                            case Directions.South:
                            texture = background[5];
                            break;
                        };
                        break;
                    };

                    //Night
                    case 1:
                    {
                        switch (plyr.facing)
                        {
                            case Directions.West: texture = background[3]; break;
                            case Directions.North: texture = background[2]; break;
                            case Directions.East: texture = background[0]; break;
                            case Directions.South: texture = background[1]; break;
                        };
                        break;
                    };

                    //Sunrise1
                    case 2:
                    {
                        switch (plyr.facing)
                        {
                            case Directions.West: texture = background[21]; break;
                            case Directions.North: texture = background[20]; break;
                            case Directions.East: texture = background[18]; break;
                            case Directions.South: texture = background[19]; break;
                        };
                        break;
                    };

                    //Sunrise2
                    case 3:
                    {
                        switch (plyr.facing)
                        {
                            case Directions.West: texture = background[25]; break;
                            case Directions.North: texture = background[24]; break;
                            case Directions.East: texture = background[22]; break;
                            case Directions.South: texture = background[23]; break;
                        };
                        break;
                    };

                    //Sunrise3
                    case 4:
                    {
                        switch (plyr.facing)
                        {
                            case Directions.West: texture = background[29]; break;
                            case Directions.North: texture = background[28]; break;
                            case Directions.East: texture = background[26]; break;
                            case Directions.South: texture = background[27]; break;
                        };
                        break;
                    };

                    //Sunrise3
                    case 5:
                    {
                        switch (plyr.facing)
                        {
                            case Directions.West: texture = background[33]; break;
                            case Directions.North: texture = background[32]; break;
                            case Directions.East: texture = background[30]; break;
                            case Directions.South: texture = background[31]; break;
                        };
                        break;
                    };

                    //Sunrise3
                    case 6:
                    {
                        switch (plyr.facing)
                        {
                            case Directions.West: texture = background[37]; break;
                            case Directions.North: texture = background[36]; break;
                            case Directions.East: texture = background[34]; break;
                            case Directions.South: texture = background[35]; break;
                        };
                        break;
                    };

                    //Sunrise3
                    case 7:
                    {
                        switch (plyr.facing)
                        {
                            case Directions.West: texture = background[41]; break;
                            case Directions.North: texture = background[41]; break;
                            case Directions.East: texture = background[38]; break;
                            case Directions.South: texture = background[39]; break;
                        };
                        break;
                    };                    
                };                          
            };

            Background.Texture = texture;
            App.Draw(Background);
        }

        public static void CalculateWallPositions ( int c, int d )
        {
            // Calculates the actual positions within OpenGL space to draw the 3 quads that make up a map cell
            var x = 0;
            var y = 0;
            switch (plyr.facing)
            {
                case Directions.North:
                x = (plyr.x - ((columns - 1) / 2)) + c; // total colums -1 / 2
                y = ((plyr.y - (depth - 1)) + d); // actual depth
                break;

                case Directions.West:
                x = (plyr.x - (depth - 1)) + d;
                y = (plyr.y + ((columns - 1) / 2)) - c;
                break;

                case Directions.East:
                x = (plyr.x + (depth - 1)) - d;
                y = (plyr.y - ((columns - 1) / 2)) + c;
                break;

                case Directions.South:
                x = (plyr.x + ((columns - 1) / 2)) - c;
                y = ((plyr.y + (depth - 1)) - d);
                break;
            }

            if ((x >= 0) && (x < (plyr.mapWidth)) && (y >= 0) && (y < (plyr.mapHeight))) // valid location on map? (64 x 64 in example)
            {
                var ind = GetMapIndex(x, y);
                TransMapIndex(ind);
                frontwall = plyr.front; // front wall texture number
                leftwall = plyr.left; // left wall texture number
                rightwall = plyr.right; // right wall texture number

                frontheight = plyr.frontheight; // front wall texture number
                leftheight = plyr.leftheight; // left wall texture number
                rightheight = plyr.rightheight; // right wall texture number

                specialwall = plyr.specialwall; // special used for guild sign etc in City
                var xm = c * 2F; // x float value to be added to texture positioning co-ords
                var zm = d * 2F; // z float value to be added to texture positioning co-ords
                zm = (zm + plyr.z_offset) - 1.0f; //-1.0f;

                // Draw front, left and right walls for current map cell
                DrawCellWalls(c, d, xm, zm, frontwall, leftwall, rightwall, frontheight, leftheight, rightheight); // pass wall numbers and x and z mods
            }
        }

        public static void BuildLevelView ()
        {
            glFogi(GL_FOG_MODE, fogMode[fogfilter]); // Fog Mode
            glFogfv(GL_FOG_COLOR, fogColor); // Set Fog Color
            glFogf(GL_FOG_DENSITY, 0.2f); // How Dense Will The Fog Be
            glHint(GL_FOG_HINT, GL_DONT_CARE); // Fog Hint Value
            glFogf(GL_FOG_START, 1.0f); // Fog Start Depth
            glFogf(GL_FOG_END, 5.0f); // Fog End Depth

            // Enable and disable fog based on area and could adjust fog properties here for zones
            if ((plyr.scenario == 1) && (graphicMode == 2))
                glEnable(GL_FOG); // Enables GL_FOG for the Dungeon
            if ((plyr.scenario == 1) && (graphicMode == 1))
                glEnable(GL_FOG); // Enables GL_FOG for the Dungeon
            if ((plyr.scenario == 1) && (graphicMode == 0))
                glDisable(GL_FOG); // Disables GL_FOG for the Dungeon
            if (plyr.scenario == 0)
                glDisable(GL_FOG); // Disable GL_FOG for City

            // Start with 5 variables - columns, depth, plyr.x, plyr.y, plyr.facing
            // c and d hold current column and current depth value

            // Draw left hand block of quads
            var leftmostColumn = 0;
            var rightmostColumn = ((columns - 1) / 2);

            for (var d = 0; d < depth; d++)
            {
                for (var c = leftmostColumn; c < rightmostColumn; c++)
                    CalculateWallPositions(c, d);
            }

            // Draw right hand block of quads
            leftmostColumn = ((columns - 1) / 2) + 1;
            rightmostColumn = columns;

            for (var d = 0; d < depth; d++)
            {
                for (var c = rightmostColumn; c > (leftmostColumn - 1); c--) 
                    CalculateWallPositions(c, d);
            }

            // Draw front block of quads
            var x = ((columns - 1) / 2); // This should be the central column 13 if columns = 25
            for (var d = 0; d < depth; d++)
                CalculateWallPositions(x, d);
        }

        public static void DrawCellWalls ( int c, int d, float xm, float zm, int frontwall, int leftwall, int rightwall, int frontheight, int leftheight, int rightheight )
        {
            var texture_no = 0;
            int wall_type;
            var depthdistantfar = (-depth * 2) + 1;
            var depthdistantnear = (-depth * 2) + 3;

            // Original graphic style for standard height walls?
            if (graphicMode == 0)
            {
                frontheight = 1;
                leftheight = 1;
                rightheight = 1;
            }

            // Draw ceiling			
            if ((plyr.zone == 99) && (plyr.map == 1))
                texture_no = 61;
            if ((plyr.zone == 99) && (plyr.map == 2))
                texture_no = 36;
            if ((plyr.zone == 99) && (plyr.map == 4))
                texture_no = 52;

            if (plyr.zone != 99)
            {
                if (plyr.ceiling == 0)
                    texture_no = zones[plyr.zoneSet].ceiling;
                else
                    texture_no = plyr.ceiling;
            }

            if (texture_no != 0) // 0 = no ceiling texture
            {
                glBindTexture(GL_TEXTURE_2D, texture[texture_no]);
                glBegin(GL_QUADS);
                glTexCoord2f(0.0f, 0.0f);
                glVertex3f(-25.0f + xm, 0.5, depthdistantfar + zm); // Bottom Left
                glTexCoord2f(1.0f, 0.0f);
                glVertex3f(-23.0f + xm, 0.5, depthdistantfar + zm); // Bottom Right
                glTexCoord2f(1.0f, 1.0f);
                glVertex3f(-23.0f + xm, 0.5, depthdistantnear + zm); // Top Right
                glTexCoord2f(0.0f, 1.0f);
                glVertex3f(-25.0f + xm, 0.5, depthdistantnear + zm); // Top Left
                glEnd();
            }

            // Draw floor
            texture_no = 0;
            if (zones[plyr.zoneSet].floor > 0)
                texture_no = zones[plyr.zoneSet].floor;
            if (plyr.floorTexture > 0)
                texture_no = plyr.floorTexture;
            if ((plyr.scenario == 0) && (plyr.floorTexture == 0) && (graphicMode == 0))
                texture_no = 0;
            if (plyr.zone != 99)
                texture_no = (plyr.floorTexture == 0) ? zones[plyr.zoneSet].floor : plyr.floorTexture;

            if (texture_no != 0) // 0 = no floor texture
            {
                glBindTexture(GL_TEXTURE_2D, texture[texture_no]);
                glBegin(GL_QUADS);
                glTexCoord2f(0.0f, 0.0f);
                glVertex3f(-25.0f + xm, -0.5, depthdistantfar + zm); // Bottom Left
                glTexCoord2f(1.0f, 0.0f);
                glVertex3f(-23.0f + xm, -0.5, depthdistantfar + zm); // Bottom Right
                glTexCoord2f(1.0f, 1.0f);
                glVertex3f(-23.0f + xm, -0.5, depthdistantnear + zm); // Top Right
                glTexCoord2f(0.0f, 1.0f);
                glVertex3f(-25.0f + xm, -0.5, depthdistantnear + zm); // Top Left
                glEnd();
            }

            var midcol = ((columns - 1) / 2);

            // left wall
            if ((!(leftwall < 1)) && (c <= midcol))
            {
                wall_type = leftwall;
                if ((wall_type == 1) || (wall_type == 2))
                {
                    glEnable(GL_BLEND); // Enable Blending
                    glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA);
                }
                texture_no = GetTextureIndex(wall_type);
                glBindTexture(GL_TEXTURE_2D, texture[texture_no]);
                glBegin(GL_QUADS); // begin drawing walls
                glTexCoord2f(0.0f, 1.0f);
                glVertex3f(-25.0f + xm, -0.5, depthdistantnear + zm); // Bottom Left
                glTexCoord2f(1.0f, 1.0f);
                glVertex3f(-25.0f + xm, -0.5, depthdistantfar + zm); // Bottom Right

                //MLT: Fix double to float conversion
                glTexCoord2f(1.0f, 0.0f);
                glVertex3f(-25.0f + xm, -0.5F + leftheight, depthdistantfar + zm); // Top Right
                glTexCoord2f(0.0f, 0.0f);
                glVertex3f(-25.0f + xm, -0.5F + leftheight, depthdistantnear + zm); // Top Left
                glEnd();
                if (((wall_type == 1) || (wall_type == 2))) // was 1
                {
                    glEnable(GL_DEPTH_TEST); // Enable Depth Testing
                    glDisable(GL_BLEND);
                }
            }

            if ((!(rightwall < 1)) && (c >= midcol))
            {
                wall_type = rightwall;
                if ((wall_type == 1) || (wall_type == 2))
                {
                    glEnable(GL_BLEND); // Enable Blending
                    glDisable(GL_DEPTH_TEST); // Disable Depth Testing
                    glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA);
                }
                texture_no = GetTextureIndex(wall_type);
                glBindTexture(GL_TEXTURE_2D, texture[texture_no]);
                glBegin(GL_QUADS); // begin drawing walls
                glTexCoord2f(0.0f, 1.0f);
                glVertex3f(-23.0f + xm, -0.5, depthdistantfar + zm); // Bottom Left
                glTexCoord2f(1.0f, 1.0f);
                glVertex3f(-23.0f + xm, -0.5, depthdistantnear + zm); // Bottom Right

                //MLT: Fix double to float conversion
                glTexCoord2f(1.0f, 0.0f);
                glVertex3f(-23.0f + xm, -0.5F + rightheight, depthdistantnear + zm); // Top Right
                glTexCoord2f(0.0f, 0.0f);
                glVertex3f(-23.0f + xm, -0.5F + rightheight, depthdistantfar + zm); // Top Left
                glEnd();
                if ((wall_type == 1) || (wall_type == 2))
                {
                    glEnable(GL_DEPTH_TEST); // Enable Depth Testing
                    glDisable(GL_BLEND);
                }
            }

            if (!(frontwall < 1)) // Ignore wall type o (clear) and 1 (arch)
            {
                wall_type = frontwall;
                if ((wall_type == 1) || (wall_type == 2))
                {
                    glEnable(GL_BLEND); // Enable Blending
                    glDisable(GL_DEPTH_TEST); // Disable Depth Testing
                    glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA);
                }
                texture_no = 0;
                if (wall_type == 3)
                    texture_no = CheckCityDoors();
                if (texture_no == 0)
                    texture_no = GetTextureIndex(wall_type);
                glBindTexture(GL_TEXTURE_2D, texture[texture_no]);
                glBegin(GL_QUADS); // begin drawing walls
                glTexCoord2f(0.0f, 1.0f);
                glVertex3f(-25.0f + xm, -0.5, depthdistantfar + zm); // Bottom Left
                glTexCoord2f(1.0f, 1.0f);
                glVertex3f(-23.0f + xm, -0.5, depthdistantfar + zm); // Bottom Right

                //MLT: Fix double to float conversion
                glTexCoord2f(1.0f, 0.0f);
                glVertex3f(-23.0f + xm, -0.5F + frontheight, depthdistantfar + zm); // Top Right
                glTexCoord2f(0.0f, 0.0f);
                glVertex3f(-25.0f + xm, -0.5F + frontheight, depthdistantfar + zm); // Top Left
                glEnd();
                if ((wall_type == 1) || (wall_type == 2))
                {
                    glEnable(GL_DEPTH_TEST); // Enable Depth Testing
                    glDisable(GL_BLEND);
                }
            }
        }

        public static int GetTextureIndex ( int x )
        {
            int texture_index;

            switch (x)
            {
                case 1:
                case 2:
                texture_index = zones[plyr.zoneSet].arch; // arch image with transparency2 6
                break;
                case 3:
                case 4:
                texture_index = zones[plyr.zoneSet].door; // door1 5
                break;
                case 7:
                case 8:
                case 9: // bolted door
                case 10:
                case 11:
                case 12:
                texture_index = zones[plyr.zoneSet].door; // barred door1 5
                break;

                case 5:
                case 6:
                texture_index = zones[plyr.zoneSet].wall; // secret door1 5
                break;
                case 13:
                case 14:
                texture_index = zones[plyr.zoneSet].wall; // wall0 4
                break;
                case 27:
                texture_index = 27; // City Shop door
                break;
                case 28:
                texture_index = 28; // City Inn door
                break;
                case 29:
                texture_index = 29; // City Tavern door
                break;
                case 30:
                texture_index = 30; // City Smithy door
                break;
                case 31:
                texture_index = 31; // City Bank door
                break;
                case 32:
                texture_index = 32; // City Guild door
                break;
                case 33:
                texture_index = 33; // City Healer door
                break;
                default:
                texture_index = x; // use the non-zone value assigned to the wall
                break;
            }
            return texture_index;
        }

        public static void LoadTextureNames ()
        {
            for (var i = 0; i < numberOfTextures; i++)
                textureNames[i] = "";
            
            var filename = graphicMode == 0 ? "data/map/textures.txt" : "data/map/texturesUpdated.txt";
            
            //TODO: Ignore fixed texture count - numberOfTextures
            var lines = File.ReadAllLines(filename);
            for (var i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                var idx = line.IndexOf('=');
                var text = line.Substring(idx + 2);
                textureNames[i] = text;
            };
        }

        public static void InitTextures ()
        {
            // Load an OpenGL texture.
            // We could directly use a sf::Image as an OpenGL texture (with its Bind() member function),
            // but here we want more control on it (generate mipmaps, ...) so we create a new one

            var imagePath = (graphicMode == 0) ? "data/images/textures_original/" : "data/images/textures_alternate/";

            glGenTextures(numberOfTextures, texture[0]); // problem line - don't include in loop. Always 0???
            for (var i = 0; i < numberOfTextures; i++)
            {
                var filename = textureNames[i];

                var img = new Image($"{imagePath}{filename}.png");
                glBindTexture(GL_TEXTURE_2D, texture[i]);
                gluBuild2DMipmaps(GL_TEXTURE_2D, GL_RGBA, img.Size.X, img.Size.Y, GL_RGBA, GL_UNSIGNED_BYTE, img.Pixels);
                glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
                glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR_MIPMAP_LINEAR);
                glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAX_ANISOTROPY_EXT, (GLint)8.0f);
            }

            // Need to delete SFML image...
        }

        public static void LoadBackgroundNames ()
        {
            for (var i = 0; i < numberOfBackgrounds; i++)
                backgroundNames[i] = "";

            var filename = (graphicMode == 0) ? "data/map/backgrounds.txt" : "data/map/backgroundsUpdated.txt";

            //TODO: Ignoring # of backgrounds - numberOfBackgrounds
            var lines = File.ReadAllLines(filename);            
            for (var i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                var idx = line.IndexOf('=');
                var text = line.Substring(idx + 2);

                backgroundNames[i] = text;
                background[i] = new Texture("data/images/backgrounds/" + text + ".png");
            };
        }

        // Storage for textures
        public static readonly int numberOfTextures = 68;
        public static readonly int numberOfBackgrounds = 49; //was 46
        public static GLuint[] texture = Arrays.InitializeWithDefaultInstances<GLuint>(numberOfTextures);
        public static Texture[] background = new Texture[numberOfBackgrounds];
        public static string[] textureNames = new string[numberOfTextures];
        public static string[] backgroundNames = new string[numberOfBackgrounds];

        public static int filter; // Which Filter To Use
        public static int[] fogMode = { GL_EXP, GL_EXP2, GL_LINEAR }; // Storage For Three Types Of Fog
        public static int fogfilter = 1; // Which Fog To Use
        public static float[] fogColor = { 0.0f, 0.0f, 0.0f, 1.0f }; // Fog Color

        public static int depth = 33; // should be 13 was 33
        public static int columns = 25; // should be an odd number 25
        public static int frontwall = 0;
        public static int leftwall = 0;
        public static int rightwall = 0;
        public static int frontheight = 0;
        public static int leftheight = 0;
        public static int rightheight = 0;
        public static int ceiling = 0;
        public static int floorTexture = 0;
        public static int specialwall = 0;
        public static int zone = 0;
    }
}