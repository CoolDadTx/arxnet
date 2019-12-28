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
using System.Drawing;
using System.IO;

using SFML.Graphics;
using SFML.System;

using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

using PrimitiveType = OpenTK.Graphics.OpenGL.PrimitiveType;

namespace P3Net.Arx
{
    //TODO: Separate low-level graphics stuff from specifics (e.g. draw texture from draw panel)
    public partial class GlobalMembers
    {
        public static void Draw3DView ()
        {            
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            App.PushGLStates();
            Draw3DBackground(); // Draw SFML 2D item
            App.PopGLStates();
            BuildLevelView(); // Draw the OpenGL 3D corridor and room view
            App.PushGLStates();
        }

        public static void InitTextures ()
        {
            // Load an OpenGL texture.
            // We could directly use a sf::Image as an OpenGL texture (with its Bind() member function),
            // but here we want more control on it (generate mipmaps, ...) so we create a new one

            var imagePath = graphicMode.UseAlternateTextures() ? "data/images/textures_alternate/" : "data/images/textures_original/";
            GL.GenTextures(numberOfTextures, out texture[0]);  // problem line - don't include in loop. Always 0???

            for (var i = 0; i < numberOfTextures; i++)
            {
                var filename = textureNames[i];

                var img = new SFML.Graphics.Image($"{imagePath}{filename}.png");
                GL.BindTexture(TextureTarget.Texture2D, texture[i]);

                //TODO: Does this work, X/Y are uints?
                Glu.Build2DMipmap(TextureTarget.Texture2D, (int)All.Rgba, (int)img.Size.X, (int)img.Size.Y, PixelFormat.Rgba, PixelType.UnsignedByte, img.Pixels);

                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)All.Linear);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)All.LinearMipmapLinear);

                GL.TexParameter(TextureTarget.Texture2D, (TextureParameterName)All.TextureMaxAnisotropyExt, 8);
            }

            // Need to delete SFML image...
        }

        public static void LoadBackgroundNames ()
        {
            for (var i = 0; i < numberOfBackgrounds; i++)
                backgroundNames[i] = "";

            var filename = graphicMode.UseAlternateTextures() ? "data/map/backgroundsUpdated.txt" : "data/map/backgrounds.txt";

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

        public static void LoadTextureNames ()
        {
            for (var i = 0; i < numberOfTextures; i++)
                textureNames[i] = "";

            var filename = graphicMode.UseAlternateTextures() ? "data/map/texturesUpdated.txt" : "data/map/textures.txt";

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

#region Private Members

        private static void Draw3DBackground ()
        {
            float scaleX, scaleY;

            switch (graphicMode)
            {
                case DisplayOptions.AtariSmall:
                {
                    scaleX = ViewSize.Width / 360F;
                    scaleY = ViewSize.Height / 190F;
                    break;
                };

                case DisplayOptions.AlternateSmall:
                case DisplayOptions.AlternateLarge:
                {
                    scaleX = ViewSize.Width / 1024F;
                    scaleY = (ViewSize.Height / 2F) / 384F;
                    break;
                };

                default: throw new NotSupportedException("Bad graphicMode");
            };

            var image = new Sprite() {
                Scale = new Vector2f(scaleX, scaleY),
                Position = new Vector2f(viewPortX, (viewPortY))
            };

            //TODO: Encapsulate this
            var texture = (plyr.scenario == Scenarios.Arena) ? background[44] : null;

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

            image.Texture = texture;
            App.Draw(image);
        }

        private static void CalculateWallPositions ( int c, int d )
        {
            // Calculates the actual positions within OpenGL space to draw the 3 quads that make up a map cell
            var pos = new Point();
            switch (plyr.facing)
            {
                case Directions.North:
                    pos.X = plyr.Position.X - ((columns - 1) / 2) + c;
                    pos.Y = plyr.Position.Y - (depth - 1) + d; // actual depth
                    break;

                case Directions.West:
                    pos.X = plyr.Position.X - (depth - 1) + d;
                    pos.Y = plyr.Position.Y + ((columns - 1) / 2) - c;
                    break;

                case Directions.East:
                    pos.X = plyr.Position.X + (depth - 1) - d;
                    pos.Y = plyr.Position.Y - ((columns - 1) / 2) + c;
                    break;

                case Directions.South:
                    pos.X = plyr.Position.X + ((columns - 1) / 2) - c;
                    pos.Y = plyr.Position.Y + (depth - 1) - d;
                    break;
            };

            // valid location on map? (64 x 64 in example)
            if ((pos.X >= 0) && (pos.X < plyr.MapSize.Width) && (pos.Y >= 0) && (pos.Y < plyr.MapSize.Height)) 
            {
                var ind = GetMapIndex(pos);
                TransMapIndex(ind);
                frontwall = plyr.front; // front wall texture number
                leftwall = plyr.left; // left wall texture number
                rightwall = plyr.right; // right wall texture number

                frontheight = plyr.frontheight; // front wall texture number
                leftheight = plyr.leftheight; // left wall texture number
                rightheight = plyr.rightheight; // right wall texture number

                specialwall = plyr.specialwall; // special used for guild sign etc in City
                var xm = c * 2F; // x float value to be added to texture positioning coords
                var zm = d * 2F; // z float value to be added to texture positioning coords
                zm = (zm + plyr.z_offset) - 1.0f; //-1.0f;

                // Draw front, left and right walls for current map cell
                DrawCellWalls(c, xm, zm, frontwall, leftwall, rightwall, frontheight, leftheight, rightheight); // pass wall numbers and x and z mods
            }
        }

        private static void BuildLevelView ()
        {                           
            GL.Fog(FogParameter.FogMode, (int)fogMode[fogfilter]); // Fog Mode
            GL.Fog(FogParameter.FogColor, fogColor); // Set Fog Color
            GL.Fog(FogParameter.FogDensity, 0.2f); // How Dense Will The Fog Be            
            
            GL.Hint(HintTarget.FogHint, HintMode.DontCare);

            GL.Fog(FogParameter.FogStart, 1F); // Fog Start Depth
            GL.Fog(FogParameter.FogEnd, 5F); // Fog End Depth

            // Enable and disable fog based on area and could adjust fog properties here for zones
            if (plyr.scenario == Scenarios.Dungeon)
            {
                switch (graphicMode)
                {
                    case DisplayOptions.AtariSmall: GL.Disable(EnableCap.Fog); break;

                    case DisplayOptions.AlternateSmall:
                    case DisplayOptions.AlternateLarge: GL.Enable(EnableCap.Fog); break;

                    default: throw new NotSupportedException("Unknown graphicMode");
                };
            } else if (plyr.scenario == Scenarios.City)
                GL.Disable(EnableCap.Fog);

            // Start with 5 variables - columns, depth, plyr.Position.X, plyr.Position.Y, plyr.facing
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

        private static void DrawCellWalls ( int c, float xm, float zm, int frontwall, int leftwall, int rightwall, int frontheight, int leftheight, int rightheight )
        {
            var texture_no = 0;
            int wall_type;
            var depthdistantfar = (-depth * 2) + 1;
            var depthdistantnear = (-depth * 2) + 3;

            // Original graphic style for standard height walls?
            if (graphicMode == DisplayOptions.AtariSmall)
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
                texture_no = (plyr.ceiling == 0) ? zones[plyr.zoneSet].ceiling : plyr.ceiling;

            if (texture_no != 0) // 0 = no ceiling texture
            {                
                GL.BindTexture(TextureTarget.Texture2D, texture[texture_no]);

                GL.Begin(PrimitiveType.Quads);
                GL.TexCoord2(0.0f, 0.0f);
                GL.Vertex3(-25.0f + xm, 0.5, depthdistantfar + zm); // Bottom Left                
                GL.TexCoord2(1.0f, 0.0f);
                GL.Vertex3(-23.0f + xm, 0.5, depthdistantfar + zm); // Bottom Right
                GL.TexCoord2(1.0f, 1.0f);
                GL.Vertex3(-23.0f + xm, 0.5, depthdistantnear + zm); // Top Right
                GL.TexCoord2(0.0f, 1.0f);
                GL.Vertex3(-25.0f + xm, 0.5, depthdistantnear + zm); // Top Left
                GL.End();
            }

            // Draw floor
            texture_no = 0;
            if (zones[plyr.zoneSet].floor > 0)
                texture_no = zones[plyr.zoneSet].floor;
            if (plyr.floorTexture > 0)
                texture_no = plyr.floorTexture;
            if ((plyr.scenario == 0) && (plyr.floorTexture == 0) && (graphicMode == DisplayOptions.AtariSmall))
                texture_no = 0;
            if (plyr.zone != 99)
                texture_no = (plyr.floorTexture == 0) ? zones[plyr.zoneSet].floor : plyr.floorTexture;

            if (texture_no != 0) // 0 = no floor texture
            {
                GL.BindTexture(TextureTarget.Texture2D, texture[texture_no]);
                GL.Begin(PrimitiveType.Quads);
                GL.TexCoord2(0.0f, 0.0f);
                GL.Vertex3(-25.0f + xm, -0.5, depthdistantfar + zm); // Bottom Left
                GL.TexCoord2(1.0f, 0.0f);
                GL.Vertex3(-23.0f + xm, -0.5, depthdistantfar + zm); // Bottom Right
                GL.TexCoord2(1.0f, 1.0f);
                GL.Vertex3(-23.0f + xm, -0.5, depthdistantnear + zm); // Top Right
                GL.TexCoord2(0.0f, 1.0f);
                GL.Vertex3(-25.0f + xm, -0.5, depthdistantnear + zm); // Top Left
                GL.End();
            }

            var midcol = ((columns - 1) / 2);

            // left wall
            if ((!(leftwall < 1)) && (c <= midcol))
            {
                wall_type = leftwall;
                if ((wall_type == 1) || (wall_type == 2))
                {
                    GL.Enable(EnableCap.Blend);
                    GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
                };

                texture_no = GetTextureIndex(wall_type);
                GL.BindTexture(TextureTarget.Texture2D, texture[texture_no]);

                GL.Begin(PrimitiveType.Quads); // begin drawing walls
                GL.TexCoord2(0.0f, 1.0f);
                GL.Vertex3(-25.0f + xm, -0.5, depthdistantnear + zm); // Bottom Left
                GL.TexCoord2(1.0f, 1.0f);
                GL.Vertex3(-25.0f + xm, -0.5, depthdistantfar + zm); // Bottom Right

                //MLT: Fix double to float conversion
                GL.TexCoord2(1.0f, 0.0f);
                GL.Vertex3(-25.0f + xm, -0.5F + leftheight, depthdistantfar + zm); // Top Right
                GL.TexCoord2(0.0f, 0.0f);
                GL.Vertex3(-25.0f + xm, -0.5F + leftheight, depthdistantnear + zm); // Top Left
                GL.End();

                if (((wall_type == 1) || (wall_type == 2))) // was 1
                {
                    GL.Enable(EnableCap.DepthTest);
                    GL.Disable(EnableCap.Blend);
                }
            }

            if ((!(rightwall < 1)) && (c >= midcol))
            {
                wall_type = rightwall;
                if ((wall_type == 1) || (wall_type == 2))
                {
                    GL.Enable(EnableCap.Blend);
                    GL.Disable(EnableCap.DepthTest);
                    GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
                };

                texture_no = GetTextureIndex(wall_type);
                GL.BindTexture(TextureTarget.Texture2D, texture[texture_no]);

                GL.Begin(PrimitiveType.Quads); // begin drawing walls
                GL.TexCoord2(0.0f, 1.0f);
                GL.Vertex3(-23.0f + xm, -0.5, depthdistantfar + zm); // Bottom Left
                GL.TexCoord2(1.0f, 1.0f);
                GL.Vertex3(-23.0f + xm, -0.5, depthdistantnear + zm); // Bottom Right

                //MLT: Fix double to float conversion
                GL.TexCoord2(1.0f, 0.0f);
                GL.Vertex3(-23.0f + xm, -0.5F + rightheight, depthdistantnear + zm); // Top Right
                GL.TexCoord2(0.0f, 0.0f);
                GL.Vertex3(-23.0f + xm, -0.5F + rightheight, depthdistantfar + zm); // Top Left
                GL.End();

                if ((wall_type == 1) || (wall_type == 2))
                {
                    GL.Enable(EnableCap.DepthTest);
                    GL.Disable(EnableCap.Blend);
                }
            }

            if (!(frontwall < 1)) // Ignore wall type o (clear) and 1 (arch)
            {
                wall_type = frontwall;
                if ((wall_type == 1) || (wall_type == 2))
                {
                    GL.Enable(EnableCap.Blend);
                    GL.Disable(EnableCap.DepthTest);                    
                    GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
                }
                texture_no = 0;
                if (wall_type == 3)
                    texture_no = CheckCityDoors();
                if (texture_no == 0)
                    texture_no = GetTextureIndex(wall_type);

                GL.BindTexture(TextureTarget.Texture2D, texture[texture_no]);
                GL.Begin(PrimitiveType.Quads); // begin drawing walls
                GL.TexCoord2(0.0f, 1.0f);
                GL.Vertex3(-25.0f + xm, -0.5, depthdistantfar + zm); // Bottom Left
                GL.TexCoord2(1.0f, 1.0f);
                GL.Vertex3(-23.0f + xm, -0.5, depthdistantfar + zm); // Bottom Right

                //MLT: Fix double to float conversion
                GL.TexCoord2(1.0f, 0.0f);
                GL.Vertex3(-23.0f + xm, -0.5F + frontheight, depthdistantfar + zm); // Top Right
                GL.TexCoord2(0.0f, 0.0f);
                GL.Vertex3(-25.0f + xm, -0.5F + frontheight, depthdistantfar + zm); // Top Left
                GL.End();

                if ((wall_type == 1) || (wall_type == 2))
                {
                    GL.Enable(EnableCap.DepthTest);
                    GL.Disable(EnableCap.Blend);                    
                }
            }
        }

        //TODO: Return Zone?
        private static int GetTextureIndex ( int x )
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
        
        #endregion

        #region Review Data

        // Storage for textures
        public static readonly int numberOfTextures = 68;
        public static readonly int numberOfBackgrounds = 49; //was 46
        public static uint[] texture = new uint[numberOfTextures];
        public static Texture[] background = new Texture[numberOfBackgrounds];
        public static string[] textureNames = new string[numberOfTextures];
        public static string[] backgroundNames = new string[numberOfBackgrounds];

        public static int filter; // Which Filter To Use
        public static FogMode[] fogMode = new FogMode[] { FogMode.Exp, FogMode.Exp2, FogMode.Linear }; // Storage For Three Types Of Fog
        public static int fogfilter = 1; // Which Fog To Use
        public static float[] fogColor = { 0.0f, 0.0f, 0.0f, 1.0f }; // Fog Color

        public static int depth = 33; // should be 13 was 33
        public static int columns = 25; // should be an odd number 25
        public static int frontwall;
        public static int leftwall;
        public static int rightwall;
        public static int frontheight;
        public static int leftheight;
        public static int rightheight;
        public static int ceiling;
        public static int floorTexture;
        public static int specialwall;
        public static int zone;
        #endregion
    }
}