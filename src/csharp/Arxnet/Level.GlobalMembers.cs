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

namespace P3Net.Arx
{
    public partial class GlobalMembers
    {
        public static int GetMapIndex ( int x, int y ) => (y * plyr.MapSize.Width) + x;

        public static int GetMapIndex ( Point pos ) => (pos.Y * plyr.MapSize.Width) + pos.X;

        public static void InitMaps ()
        {
            //TODO: Read as structure instead
            using (var reader = new StreamReader("data/map/maps.txt"))
            {
                var i = 0;
                while (!reader.EndOfStream)
                {
                    // read first line as blank
                    reader.ReadLine();

                    var fields = 5; // should be 45                    
                    for (var a = 0; a < fields; ++a) // number of attributes - test for 33
                    {
                        var line = reader.ReadLine();
                        var idx = line.IndexOf('=');
                        var text = line.Substring(idx + 2);

                        if (a == 0)
                            maps[i].filename = text;
                        if (a == 1)
                            maps[i].width = Convert.ToInt32(text);
                        if (a == 2)
                            maps[i].height = Convert.ToInt32(text);
                        if (a == 3)
                            maps[i].description = text;
                        if (a == 4)
                            maps[i].background = Convert.ToInt32(text);
                    }
                    reader.ReadLine();
                    i++;
                }
            };
        }

        //TODO: Combine with map loading
        public static void LoadDescriptions ( int map )
        {
            //TODO: Ignoring description size here
            var filename = $"data/map/{maps[map].filename}Descriptions.txt";

            descriptions = File.ReadAllLines(filename);
        }

        //TODO: Combine with map loading        
        public static void LoadMapData ( int map )
        {
            //TODO: Read as structured data
            var filename = $"data/map/{maps[map].filename}Cells.txt";

            using (var reader = new StreamReader(filename))
            {
                var totalMapCells = plyr.MapSize.Width * plyr.MapSize.Height;
                for (var i = 0; i < totalMapCells; ++i)
                {
                    var mapCell_attributes = 13; // should be 12

                    // read first line as blank
                    reader.ReadLine();

                    for (var a = 0; a < mapCell_attributes; ++a) // number of attributes - test for 33
                    {
                        var line = reader.ReadLine();

                        var idx = line.IndexOf('=');
                        var text = line.Substring(idx + 2);

                        if (a == 0)
                            levelmap[i].east = Convert.ToInt32(text);
                        if (a == 1)
                            levelmap[i].north = Convert.ToInt32(text);
                        if (a == 2)
                            levelmap[i].west = Convert.ToInt32(text);
                        if (a == 3)
                            levelmap[i].south = Convert.ToInt32(text);
                        if (a == 4)
                            levelmap[i].eastHeight = Convert.ToInt32(text);
                        if (a == 5)
                            levelmap[i].northHeight = Convert.ToInt32(text);
                        if (a == 6)
                            levelmap[i].westHeight = Convert.ToInt32(text);
                        if (a == 7)
                            levelmap[i].southHeight = Convert.ToInt32(text);
                        if (a == 8)
                            levelmap[i].ceiling = Convert.ToInt32(text);
                        if (a == 9)
                            levelmap[i].floor = Convert.ToInt32(text);
                        if (a == 10)
                            levelmap[i].zone = Convert.ToInt32(text);
                        if (a == 11)
                            levelmap[i].location = Convert.ToInt32(text);
                        if (a == 12)
                            levelmap[i].special = Convert.ToInt32(text);
                    }
                    reader.ReadLine();
                }
            };
        }

        //TODO: Combine with map loading        
        public static void LoadMessages ( int map )
        {
            //TODO: Ignoring fixed size of roomMessages            
            var filename = $"data/map/{maps[map].filename}Messages.txt";
            roomMessages = File.ReadAllLines(filename);
        }

        //TODO: Combine with map loading        
        public static void LoadZoneData ( int map )
        {
            //TODO: Use structured data

            var filename = $"data/map/{maps[map].filename}Zones.txt";

            using (var reader = new StreamReader(filename))
            {
                var i = 0;
                while (!reader.EndOfStream)
                {
                    var fields = 5;
                    //Read first line as blank
                    reader.ReadLine();

                    for (var a = 0; a < fields; ++a) // number of attributes
                    {
                        var line = reader.ReadLine();
                        var idx = line.IndexOf('=');
                        var text = line.Substring(idx + 2);

                        if (a == 0)
                            zones2[i].y1 = Convert.ToInt32(text);
                        if (a == 1)
                            zones2[i].x1 = Convert.ToInt32(text);
                        if (a == 2)
                            zones2[i].y2 = Convert.ToInt32(text);
                        if (a == 3)
                            zones2[i].x2 = Convert.ToInt32(text);
                        if (a == 4)
                            zones2[i].zoneRef = Convert.ToInt32(text);
                    }
                    reader.ReadLine();
                    i++;
                }
            };
        }

        //TODO: Hook to event
        public static void MoveMapLevel ()
        {
            //TODO: Make this data-bound
            if ((plyr.Position.X == 50) && (plyr.Position.Y == 3) && (plyr.map == 1)) // to the city from dungeon 1
            {
                plyr.Position = new Point(49, 3);
                plyr.facing = Directions.West;
                plyr.map = 0; // The City
                plyr.scenario = Scenarios.City;
                plyr.z_offset = 1.0f;
                LoadMapData(0);
                LoadDescriptions(0);
                LoadZoneData(0);
                LoadMessages(0);
            }

            if ((plyr.Position.X == 59) && (plyr.Position.Y == 62) && (plyr.map == 1)) // to the city from dungeon 1
            {
                plyr.Position = new Point(59, 63);
                plyr.facing = Directions.South;
                plyr.map = 0; // The City
                plyr.scenario = Scenarios.City;
                plyr.z_offset = 1.0f;
                LoadMapData(0);
                LoadDescriptions(0);
                LoadZoneData(0);
                LoadMessages(0);
            }

            if ((plyr.Position.X == 50) && (plyr.Position.Y == 3) && (plyr.map == 0)) // to the dungeon 1 from city
            {
                plyr.Position = new Point(49, 3);
                plyr.facing = Directions.West;
                plyr.map = 1; // The Dungeon
                plyr.scenario = Scenarios.Dungeon;
                plyr.z_offset = 1.0f;
                LoadMapData(1);
                LoadDescriptions(1);
                LoadZoneData(1);
                LoadMessages(1);
            }

            if ((plyr.Position.X == 59) && (plyr.Position.Y == 62) && (plyr.map == 0)) // to the dungeon 1 from city
            {
                plyr.Position = new Point(59, 61);
                plyr.facing = Directions.North;
                plyr.map = 1; // The Dungeon
                plyr.scenario = Scenarios.Dungeon;
                plyr.z_offset = 1.0f;
                LoadMapData(1);
                LoadDescriptions(1);
                LoadZoneData(1);
                LoadMessages(1);
            }

            if ((plyr.Position.X == 48) && (plyr.Position.Y == 48) && (plyr.map == 1)) // from dungeon 1 to dungeon 2 se fix
            {
                plyr.Position = new Point(30, 31);
                plyr.facing = Directions.West;
                plyr.map = 2; // The Dungeon
                plyr.scenario = Scenarios.Dungeon;
                plyr.z_offset = 1.0f;
                
                //TODO: Shouldn't this come from the map itself, not stored as part of the player
                plyr.MapSize = new Size(32, 32);

                LoadMapData(2);
                LoadDescriptions(2);
                LoadZoneData(2);
                LoadMessages(2);
            }

            if ((plyr.Position.X == 31) && (plyr.Position.Y == 31) && (plyr.map == 2)) // from dungeon 2 to dungeon 1 se fix
            {
                plyr.Position = new Point(47, 48);
                plyr.facing = Directions.West;
                plyr.map = 1; // The Dungeon
                plyr.scenario = Scenarios.Dungeon;
                plyr.z_offset = 1.0f;
                plyr.MapSize = new Size(64, 64);
                LoadMapData(1);
                LoadDescriptions(1);
                LoadZoneData(1);
                LoadMessages(1);
            }

            if ((plyr.Position.X == 16) && (plyr.Position.Y == 48) && (plyr.map == 1)) // from dungeon 1 to dungeon 2 sw fix
            {
                plyr.Position = new Point(1, 31);
                plyr.facing = Directions.East;
                plyr.map = 2; // The Dungeon
                plyr.scenario = Scenarios.Dungeon;
                plyr.z_offset = 1.0f;
                plyr.MapSize = new Size(32, 32);
                LoadMapData(2);
                LoadDescriptions(2);
                LoadZoneData(2);
                LoadMessages(2);
            }

            if ((plyr.Position.X == 0) && (plyr.Position.Y == 31) && (plyr.map == 2)) // from dungeon 2 to dungeon 1 ne fix
            {
                plyr.Position = new Point(17, 48);                
                plyr.facing = Directions.East;
                plyr.map = 1; // The Dungeon
                plyr.scenario = Scenarios.Dungeon;
                plyr.z_offset = 1.0f;
                plyr.MapSize = new Size(64, 64);
                LoadMapData(1);
                LoadDescriptions(1);
                LoadZoneData(1);
                LoadMessages(1);
            }

            if ((plyr.Position.X == 49) && (plyr.Position.Y == 17) && (plyr.map == 1)) // from dungeon 1 to dungeon 2 fix
            {
                plyr.Position = new Point(30, 0);
                plyr.facing = Directions.West;
                plyr.map = 2; // The Dungeon
                plyr.scenario = Scenarios.Dungeon;
                plyr.z_offset = 1.0f;
                plyr.MapSize = new Size(32, 32);
                LoadMapData(2);
                LoadDescriptions(2);
                LoadZoneData(2);
                LoadMessages(2);
            }

            if ((plyr.Position.X == 16) && (plyr.Position.Y == 17) && (plyr.map == 1)) // from dungeon 1 to dungeon 2 nw - fix
            {
                plyr.Position = new Point(0, 0);
                plyr.facing = Directions.North;
                plyr.map = 2; // The Dungeon
                plyr.scenario = Scenarios.Dungeon;
                plyr.z_offset = 1.0f;
                plyr.MapSize = new Size(32, 32);
                LoadMapData(2);
                LoadDescriptions(2);
                LoadZoneData(2);
                LoadMessages(2);
            }

            if ((plyr.Position.X == 0) && (plyr.Position.Y == 1) && (plyr.map == 2)) // from dungeon 2 to dungeon 1 nw fix
            {
                plyr.Position = new Point(16, 16);
                plyr.facing = Directions.North;
                plyr.map = 1; // The Dungeon
                plyr.scenario = Scenarios.Dungeon;
                plyr.z_offset = 1.0f;
                plyr.MapSize = new Size(64, 64);
                LoadMapData(1);
                LoadDescriptions(1);
                LoadZoneData(1);
                LoadMessages(1);
            }

            if ((plyr.Position.X == 31) && (plyr.Position.Y == 0) && (plyr.map == 2)) // from dungeon 2 to dungeon 1 ne fix
            {
                plyr.Position = new Point(48, 17);
                plyr.facing = Directions.West;
                plyr.map = 1; // The Dungeon
                plyr.scenario = Scenarios.Dungeon;
                plyr.z_offset = 1.0f;
                plyr.MapSize = new Size(64, 64);
                LoadMapData(1);
                LoadDescriptions(1);
                LoadZoneData(1);
                LoadMessages(1);
            }

            if ((plyr.Position.X == 17) && (plyr.Position.Y == 12) && (plyr.map == 2)) // from dungeon 2 to dungeon 3 fix
            {
                plyr.Position = new Point(9, 3);
                plyr.facing = Directions.West;
                plyr.map = 3; // The Dungeon level 3
                plyr.scenario = Scenarios.Dungeon;
                plyr.z_offset = 1.0f;
                plyr.MapSize = new Size(32, 32);
                LoadMapData(3);
                LoadDescriptions(3);
                LoadZoneData(3); // temp
                LoadMessages(2); // temp
            }

            if ((plyr.Position.X == 10) && (plyr.Position.Y == 3) && (plyr.map == 3)) // from dungeon 3 to dungeon 2
            {
                plyr.Position = new Point(16, 12);
                plyr.facing = Directions.West;
                plyr.map = 2; // The Dungeon level 2
                plyr.scenario = Scenarios.Dungeon;
                plyr.z_offset = 1.0f;
                plyr.MapSize = new Size(32, 32);
                LoadMapData(2);
                LoadDescriptions(2);
                LoadZoneData(2); // temp
                LoadMessages(2); // temp
            }

            if ((plyr.Position.X == 6) && (plyr.Position.Y == 15) && (plyr.map == 3)) // from dungeon 3 to dungeon 2
            {
                plyr.Position = new Point(14, 22);
                plyr.facing = Directions.North;
                plyr.map = 2; // The Dungeon level 2
                plyr.scenario = Scenarios.Dungeon;
                plyr.z_offset = 1.0f;
                plyr.MapSize = new Size(32, 32);
                LoadMapData(2);
                LoadDescriptions(2);
                LoadZoneData(2); // temp
                LoadMessages(2); // temp
            }
        }

        //TODO: Hook to move event
        public static void MoveMapLevelTeleport ()
        {
            plyr.z_offset = 1.0f;
            plyr.MapSize = maps[plyr.map].Size;
            LoadMapData(plyr.map);
            LoadDescriptions(plyr.map);
            LoadZoneData(plyr.map);
            LoadMessages(plyr.map);
        }

        public static void SetCurrentZone ()
        {
            // x >= entry 3 x2
            // x < entry 1 x1
            // y >= entry 0 y1
            // y < entry 2 y2

            var identifiedZone = 99; // 99 used to represent not part of a zone - default
            var x = plyr.Position.X;
            var y = plyr.Position.Y;

            for (var z = 0; z < 255; ++z) // 44
            {
                if ((zones2[z].TopLeft.Y <= y) && (zones2[z].BottomRight.Y > y) && (zones2[z].TopLeft.X > x) && (zones2[z].BottomRight.X <= x))
                    identifiedZone = z;
            }

            plyr.zone = identifiedZone;
            plyr.zoneSet = zones2[identifiedZone].zoneRef; // set index for image set
            if (identifiedZone == 99)
            {
                plyr.zoneSet = 0; // set an image set of 0 for default / non zone members
                if ((x >= 0) && (x <= 31) && (y >= 0) && (y <= 31))
                    plyr.zoneSet = 14;
                if ((x >= 0) && (x <= 31) && (y >= 32) && (y <= 63))
                    plyr.zoneSet = 14;
                if ((x >= 32) && (x <= 63) && (y >= 32) && (y <= 63))
                    plyr.zoneSet = 14;

                // Dungeon level 2 default
                if (plyr.map == 2)
                    plyr.zoneSet = 20;

                // Dungeon level 3 default
                if (plyr.map == 3)
                    plyr.zoneSet = 25;

                // Dungeon level 4 default
                if (plyr.map == 4)
                    plyr.zoneSet = 27;

                // City override
                if (plyr.map == 0)
                    plyr.zoneSet = 17;

                // Wilderness override
                if (plyr.map == 5)
                    plyr.zoneSet = 19;
            }
        }

        public static void TransMapIndex ( int idx )
        {
            var facing = plyr.facing;

            if (facing == Directions.West)
            {
                plyr.back = levelmap[idx].east;
                plyr.left = levelmap[idx].south;
                plyr.right = levelmap[idx].north;
                plyr.front = levelmap[idx].west;
                plyr.leftheight = levelmap[idx].southHeight;
                plyr.rightheight = levelmap[idx].northHeight;
                plyr.frontheight = levelmap[idx].westHeight;
            } else if (facing == Directions.North)
            {
                plyr.back = levelmap[idx].south;
                plyr.left = levelmap[idx].west;
                plyr.right = levelmap[idx].east;
                plyr.front = levelmap[idx].north;
                plyr.leftheight = levelmap[idx].westHeight;
                plyr.rightheight = levelmap[idx].eastHeight;
                plyr.frontheight = levelmap[idx].northHeight;
            } else if (facing == Directions.East)
            {
                plyr.back = levelmap[idx].west;
                plyr.right = levelmap[idx].south;
                plyr.left = levelmap[idx].north;
                plyr.front = levelmap[idx].east;
                plyr.rightheight = levelmap[idx].southHeight;
                plyr.leftheight = levelmap[idx].northHeight;
                plyr.frontheight = levelmap[idx].eastHeight;
            } else
            {
                plyr.back = levelmap[idx].north;
                plyr.right = levelmap[idx].west;
                plyr.left = levelmap[idx].east;
                plyr.front = levelmap[idx].south;
                plyr.rightheight = levelmap[idx].westHeight;
                plyr.leftheight = levelmap[idx].eastHeight;
                plyr.frontheight = levelmap[idx].southHeight;
            }

            plyr.specialwall = levelmap[idx].special;
            plyr.floorTexture = levelmap[idx].floor;
            plyr.ceiling = levelmap[idx].ceiling;
        }

        #region Review Data

        public static string[] descriptions = new string[255];

        public static Mapcell[] levelmap = Arrays.InitializeWithDefaultInstances<Mapcell>(4096); // 4096 = 64 by 64 cells, 96x128 - 12288        

        public static Map[] maps = Arrays.InitializeWithDefaultInstances<Map>(6);
        public static string[] roomMessages = new string[255];

        public static Teleport[] teleports =
        {
            new Teleport()
            { @ref = 0xe0, new_x = 0x12, new_y = 0x1E, new_map = 0, new_facing = 0xFF },
            new Teleport()
            { @ref = 0xe1, new_x = 0x12, new_y = 0x1B, new_map = 0, new_facing = 0x03 },
            new Teleport()
            { @ref = 0xe2, new_x = 0x13, new_y = 0x1B, new_map = 0, new_facing = 0x01 },
            new Teleport()
            { @ref = 0xe3, new_x = 0x0F, new_y = 0x17, new_map = 0, new_facing = 0x00 },
            new Teleport()
            { @ref = 0xe4, new_x = 0x1D, new_y = 0x17, new_map = 0, new_facing = 0x00 },
            new Teleport()
            { @ref = 0xe5, new_x = 0x05, new_y = 0x1A, new_map = 0, new_facing = 0xFF },
            new Teleport()
            { @ref = 0xe6, new_x = 0x14, new_y = 0x1F, new_map = 0, new_facing = 0x00 },
            new Teleport()
            { @ref = 0xe7, new_x = 0x10, new_y = 0x01, new_map = 0, new_facing = 0xFF },
            new Teleport()
            { @ref = 0xe8, new_x = 0x0B, new_y = 0x01, new_map = 1, new_facing = 0xFF },
            new Teleport()
            { @ref = 0xe9, new_x = 0x11, new_y = 0x07, new_map = 1, new_facing = 0xFF },
            new Teleport()
            { @ref = 0xea, new_x = 0x11, new_y = 0x0A, new_map = 1, new_facing = 0xFF },
            new Teleport()
            { @ref = 0xeb, new_x = 0x0B, new_y = 0x1C, new_map = 1, new_facing = 0xFF },
            new Teleport()
            { @ref = 0xec, new_x = 0x0B, new_y = 0x1F, new_map = 1, new_facing = 0xFF },
            new Teleport()
            { @ref = 0xed, new_x = 0x08, new_y = 0x0F, new_map = 1, new_facing = 0x80 },
            new Teleport()
            { @ref = 0xee, new_x = 0x12, new_y = 0x09, new_map = 2, new_facing = 0x80 },
            new Teleport()
            { @ref = 0xef, new_x = 0x00, new_y = 0x01, new_map = 2, new_facing = 0xFF },
            new Teleport()
            { @ref = 0xf0, new_x = 0x00, new_y = 0x1E, new_map = 0, new_facing = 0xFF },
            new Teleport()
            { @ref = 0xf1, new_x = 0x01, new_y = 0x00, new_map = 1, new_facing = 0xFF },
            new Teleport()
            { @ref = 0xf2, new_x = 0x1E, new_y = 0x00, new_map = 0, new_facing = 0xFF },
            new Teleport()
            { @ref = 0xf3, new_x = 0x1F, new_y = 0x1E, new_map = 1, new_facing = 0xFF },
            new Teleport()
            { @ref = 0xf4, new_x = 0x1F, new_y = 0x01, new_map = 3, new_facing = 0xFF },
            new Teleport()
            { @ref = 0xf5, new_x = 0x01, new_y = 0x1F, new_map = 3, new_facing = 0xFF },
            new Teleport()
            { @ref = 0xf6, new_x = 0x1E, new_y = 0x1F, new_map = 2, new_facing = 0xFF },
            new Teleport()
            { @ref = 0xf7, new_x = 0x00, new_y = 0x12, new_map = 5, new_facing = 0xFF },
            new Teleport()
            { @ref = 0xf8, new_x = 0x0D, new_y = 0x0F, new_map = 5, new_facing = 0xFF },
            new Teleport()
            { @ref = 0xf9, new_x = 0x03, new_y = 0x01, new_map = 3, new_facing = 0xFF },
            new Teleport()
            { @ref = 0xfa, new_x = 0x03, new_y = 0x1E, new_map = 1, new_facing = 0xFF },
            new Teleport()
            { @ref = 0xfb, new_x = 0x0B, new_y = 0x00, new_map = 4, new_facing = 0xFF },
            new Teleport()
            { @ref = 0xfc, new_x = 0x16, new_y = 0x07, new_map = 1, new_facing = 0x02 },
            new Teleport()
            { @ref = 0xfd, new_x = 0x1A, new_y = 0x05, new_map = 1, new_facing = 0x00 },
            new Teleport()
            { @ref = 0xfe, new_x = 0x1C, new_y = 0x07, new_map = 1, new_facing = 0x02 },
            new Teleport()
            { @ref = 0xff, new_x = 0x18, new_y = 0x1B, new_map = 2, new_facing = 0x03 }
        };
        public static string tempString = new string(new char[100]); // temporary string

        public static ZoneRecord[] zones =
        {
            new ZoneRecord()
            { wall = 9, door = 10, arch = 11, floor = 3, ceiling = 61 },
            new ZoneRecord()
            { wall = 4, door = 5, arch = 6, floor = 8, ceiling = 60 },
            new ZoneRecord()
            { wall = 4, door = 5, arch = 6, floor = 7, ceiling = 0 },
            new ZoneRecord()
            { wall = 4, door = 5, arch = 6, floor = 7, ceiling = 0 },
            new ZoneRecord()
            { wall = 12, door = 13, arch = 14, floor = 18, ceiling = 0 },
            new ZoneRecord()
            { wall = 4, door = 5, arch = 6, floor = 7, ceiling = 0 },
            new ZoneRecord()
            { wall = 4, door = 5, arch = 6, floor = 7, ceiling = 0 },
            new ZoneRecord()
            { wall = 4, door = 5, arch = 6, floor = 7, ceiling = 0 },
            new ZoneRecord()
            { wall = 4, door = 5, arch = 6, floor = 7, ceiling = 0 },
            new ZoneRecord()
            { wall = 4, door = 5, arch = 6, floor = 7, ceiling = 0 },
            new ZoneRecord()
            { wall = 4, door = 5, arch = 6, floor = 7, ceiling = 0 },
            new ZoneRecord()
            { wall = 16, door = 17, arch = 2, floor = 18, ceiling = 0 },
            new ZoneRecord()
            { wall = 12, door = 13, arch = 2, floor = 15, ceiling = 64 },
            new ZoneRecord()
            { wall = 12, door = 13, arch = 2, floor = 19, ceiling = 63 },
            new ZoneRecord()
            { wall = 4, door = 5, arch = 6, floor = 7, ceiling = 0 },
            new ZoneRecord()
            { wall = 20, door = 20, arch = 20, floor = 7, ceiling = 0 },
            new ZoneRecord()
            { wall = 21, door = 22, arch = 23, floor = 24, ceiling = 0 },
            new ZoneRecord()
            { wall = 25, door = 26, arch = 26, floor = 7, ceiling = 0 },
            new ZoneRecord()
            { wall = 25, door = 26, arch = 26, floor = 34, ceiling = 40 },
            new ZoneRecord()
            { wall = 35, door = 35, arch = 35, floor = 3, ceiling = 0 },
            new ZoneRecord()
            { wall = 21, door = 22, arch = 23, floor = 42, ceiling = 36 },
            new ZoneRecord()
            { wall = 43, door = 44, arch = 0, floor = 45, ceiling = 62 },
            new ZoneRecord()
            { wall = 46, door = 47, arch = 0, floor = 45, ceiling = 62 },
            new ZoneRecord()
            { wall = 48, door = 48, arch = 49, floor = 50, ceiling = 0 },
            new ZoneRecord()
            { wall = 51, door = 51, arch = 51, floor = 52, ceiling = 0 },
            new ZoneRecord()
            { wall = 55, door = 56, arch = 57, floor = 42, ceiling = 0 },
            new ZoneRecord()
            { wall = 58, door = 59, arch = 58, floor = 8, ceiling = 0 },
            new ZoneRecord()
            { wall = 65, door = 66, arch = 67, floor = 61, ceiling = 52 }
        };
        public static ZoneRect[] zones2 = Arrays.InitializeWithDefaultInstances<ZoneRect>(255); // across full first dungeon level

        #endregion
    }
}