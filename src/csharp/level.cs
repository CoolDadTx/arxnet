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
    public class Teleport
    {
        public int new_facing { get; set; }

        public int new_map { get; set; }

        public int new_x { get; set; }

        public int new_y { get; set; }

        public int @ref { get; set; }
    }

    public class Mapcell
    {
        public int ceiling { get; set; }

        public int east { get; set; }

        public int eastHeight { get; set; }

        public int floor { get; set; }

        public int location { get; set; }

        public int north { get; set; }

        public int northHeight { get; set; }

        public int south { get; set; }

        public int southHeight { get; set; }

        public int special { get; set; }

        public int west { get; set; }

        public int westHeight { get; set; }

        public int zone { get; set; }
    }

    public class ZoneRect
    {
        public int x1 { get; set; }

        public int x2 { get; set; }

        public int y1 { get; set; }

        public int y2 { get; set; }

        public int zoneRef { get; set; }
    }

    public class Map
    {
        public int background { get; set; }

        public string description { get; set; }

        public string filename { get; set; }

        public int height { get; set; }

        public int width { get; set; }
    }

    public class ZoneRecord
    {
        public int arch { get; set; }

        public int ceiling { get; set; }

        public int door { get; set; }

        public int floor { get; set; }

        public int wall { get; set; }
    }

    public partial class GlobalMembers
    {
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

        public static int GetMapIndex ( int x, int y ) => (y * plyr.mapWidth) + x;

        public static void InitMaps ()
        {
            var instream = new ifstream();
            string junk;
            string line;

            instream.open("data/map/maps.txt");
            if (instream == null)
                cerr << "Error: MAPS.TXT file could not be loaded" << "\n";
            var i = 0;
            while (junk != "EOF")
            {
                var fields = 5; // should be 45
                getline(instream, junk); // read first line as blank
                for (var a = 0; a < fields; ++a) // number of attributes - test for 33
                {
                    getline(instream, line);
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
                getline(instream, junk);
                i++;
            }
            instream.close();
        }

        // Level handling routines

        public static void LoadBinaryLevel ()
        {
            // Define an array of mapcells used to hold map data converted from binary data
            var tempString = $"{"data/map/"}{"dun4.bin"}"; //dungeon1
            var fp = fopen(tempString, "rb");
            if (fp != null)
            {
                for (var i = 0; i < 4096; i++)
                {
                    var tmp = fgetc(fp);
                    if (tmp == 2)
                    {
                        Console.Write(tmp);
                        Console.Write(" , ");
                        Console.Write(i);
                        Console.Write("\n");
                    }
                    levelmap[i].east = (tmp & 240) >> 4;
                    levelmap[i].north = tmp & 15;
                    tmp = fgetc(fp);
                    levelmap[i].west = (tmp & 240) >> 4;
                    levelmap[i].south = tmp & 15;
                    tmp = fgetc(fp);
                    levelmap[i].location = tmp;
                    tmp = fgetc(fp);
                    levelmap[i].special = tmp;
                }
            }
            fclose(fp);
        }

        public static void LoadDescriptions ( int map )
        {
            for (var i = 0; i < 255; i++)
                descriptions[i] = "";
            var filename = $"data/map/{(maps[map].filename)}Descriptions.txt";
            var instream = new ifstream();
            string line;
            instream.open(filename);
            if (instream == null)
                cerr << "Error: " << filename << " file could not be loaded" << "\n";
            var i = 0;
            while (line != "EOF")
            {
                getline(instream, line);
                descriptions[i] = line;
                i++;
            }
            instream.close();
        }

        public static void LoadMapData ( int map )
        {
            var instream = new ifstream();
            var filename = $"data/map/{(maps[map].filename)}Cells.txt";
            instream.open(filename);
            if (instream == null)
                cerr << "Error: terrain file could not be loaded" << "\n";

            var totalMapCells = plyr.mapWidth * plyr.mapHeight;
            for (var i = 0; i < totalMapCells; ++i)
            {
                string junk;
                string line;
                var mapCell_attributes = 13; // should be 12
                getline(instream, junk); // read first line as blank

                for (var a = 0; a < mapCell_attributes; ++a) // number of attributes - test for 33
                {
                    getline(instream, line);

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
                getline(instream, junk);
            }
            instream.close();
        }

        public static void LoadMessages ( int map )
        {
            for (var i = 0; i < 100; i++)
                roomMessages[i] = "";
            var filename = $"data/map/{(maps[map].filename)}Messages.txt";
            var instream = new ifstream();
            string line;
            instream.open(filename);
            if (instream == null)
                cerr << "Error:" << filename << "could not be loaded" << "\n";
            var i = 0;
            while (line != "EOF")
            {
                getline(instream, line);
                roomMessages[i] = line;
                i++;
            }
            instream.close();
        }

        public static void LoadZoneData ( int map )
        {
            for (var a = 0; a < 255; ++a)
            {
                zones2[a].x1 = 0;
                zones2[a].x2 = 0;
                zones2[a].y1 = 0;
                zones2[a].y2 = 0;
                zones2[a].zoneRef = 0;
            }

            var filename = $"data/map/{(maps[map].filename)}Zones.txt";
            var instream = new ifstream();
            string junk;
            string line;

            instream.open(filename);

            if (instream == null)
                cerr << "Error:" << filename << " could not be loaded" << "\n";
            var i = 0;
            while (junk != "EOF")
            {
                var fields = 5;
                getline(instream, junk); // read first line as blank
                for (var a = 0; a < fields; ++a) // number of attributes
                {
                    getline(instream, line);
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
                getline(instream, junk);
                i++;
            }
            instream.close();
        }

        public static void MoveMapLevel ()
        {
            if ((plyr.x == 50) && (plyr.y == 3) && (plyr.map == 1)) // to the city from dungeon 1
            {
                plyr.x = 49;
                plyr.y = 3;
                plyr.facing = 1;
                plyr.map = 0; // The City
                plyr.scenario = 0; // The City
                plyr.z_offset = 1.0f;
                LoadMapData(0);
                LoadDescriptions(0);
                LoadZoneData(0);
                LoadMessages(0);
            }

            if ((plyr.x == 59) && (plyr.y == 62) && (plyr.map == 1)) // to the city from dungeon 1
            {
                plyr.x = 59;
                plyr.y = 63;
                plyr.facing = 4;
                plyr.map = 0; // The City
                plyr.scenario = 0; // The City
                plyr.z_offset = 1.0f;
                LoadMapData(0);
                LoadDescriptions(0);
                LoadZoneData(0);
                LoadMessages(0);
            }

            if ((plyr.x == 50) && (plyr.y == 3) && (plyr.map == 0)) // to the dungeon 1 from city
            {
                plyr.x = 49;
                plyr.y = 3;
                plyr.facing = 1;
                plyr.map = 1; // The Dungeon
                plyr.scenario = 1; // The Dungeon
                plyr.z_offset = 1.0f;
                LoadMapData(1);
                LoadDescriptions(1);
                LoadZoneData(1);
                LoadMessages(1);
            }

            if ((plyr.x == 59) && (plyr.y == 62) && (plyr.map == 0)) // to the dungeon 1 from city
            {
                plyr.x = 59;
                plyr.y = 61;
                plyr.facing = 2;
                plyr.map = 1; // The Dungeon
                plyr.scenario = 1; // The Dungeon
                plyr.z_offset = 1.0f;
                LoadMapData(1);
                LoadDescriptions(1);
                LoadZoneData(1);
                LoadMessages(1);
            }

            if ((plyr.x == 48) && (plyr.y == 48) && (plyr.map == 1)) // from dungeon 1 to dungeon 2 se fix
            {
                plyr.x = 30;
                plyr.y = 31;
                plyr.facing = 1;
                plyr.map = 2; // The Dungeon
                plyr.scenario = 1; // The Dungeon
                plyr.z_offset = 1.0f;
                plyr.mapWidth = 32;
                plyr.mapHeight = 32;
                LoadMapData(2);
                LoadDescriptions(2);
                LoadZoneData(2);
                LoadMessages(2);
            }

            if ((plyr.x == 31) && (plyr.y == 31) && (plyr.map == 2)) // from dungeon 2 to dungeon 1 se fix
            {
                plyr.x = 47;
                plyr.y = 48;
                plyr.facing = 1;
                plyr.map = 1; // The Dungeon
                plyr.scenario = 1; // The Dungeon
                plyr.z_offset = 1.0f;
                plyr.mapWidth = 64;
                plyr.mapHeight = 64;
                LoadMapData(1);
                LoadDescriptions(1);
                LoadZoneData(1);
                LoadMessages(1);
            }

            if ((plyr.x == 16) && (plyr.y == 48) && (plyr.map == 1)) // from dungeon 1 to dungeon 2 sw fix
            {
                plyr.x = 1;
                plyr.y = 31;
                plyr.facing = 3;
                plyr.map = 2; // The Dungeon
                plyr.scenario = 1; // The Dungeon
                plyr.z_offset = 1.0f;
                plyr.mapWidth = 32;
                plyr.mapHeight = 32;
                LoadMapData(2);
                LoadDescriptions(2);
                LoadZoneData(2);
                LoadMessages(2);
            }

            if ((plyr.x == 0) && (plyr.y == 31) && (plyr.map == 2)) // from dungeon 2 to dungeon 1 ne fix
            {
                plyr.x = 17;
                plyr.y = 48;
                plyr.facing = 3;
                plyr.map = 1; // The Dungeon
                plyr.scenario = 1; // The Dungeon
                plyr.z_offset = 1.0f;
                plyr.mapWidth = 64;
                plyr.mapHeight = 64;
                LoadMapData(1);
                LoadDescriptions(1);
                LoadZoneData(1);
                LoadMessages(1);
            }

            if ((plyr.x == 49) && (plyr.y == 17) && (plyr.map == 1)) // from dungeon 1 to dungeon 2 fix
            {
                plyr.x = 30;
                plyr.y = 0;
                plyr.facing = 1;
                plyr.map = 2; // The Dungeon
                plyr.scenario = 1; // The Dungeon
                plyr.z_offset = 1.0f;
                plyr.mapWidth = 32;
                plyr.mapHeight = 32;
                LoadMapData(2);
                LoadDescriptions(2);
                LoadZoneData(2);
                LoadMessages(2);
            }

            if ((plyr.x == 16) && (plyr.y == 17) && (plyr.map == 1)) // from dungeon 1 to dungeon 2 nw - fix
            {
                plyr.x = 0;
                plyr.y = 0;
                plyr.facing = 2;
                plyr.map = 2; // The Dungeon
                plyr.scenario = 1; // The Dungeon
                plyr.z_offset = 1.0f;
                plyr.mapWidth = 32;
                plyr.mapHeight = 32;
                LoadMapData(2);
                LoadDescriptions(2);
                LoadZoneData(2);
                LoadMessages(2);
            }

            if ((plyr.x == 0) && (plyr.y == 1) && (plyr.map == 2)) // from dungeon 2 to dungeon 1 nw fix
            {
                plyr.x = 16;
                plyr.y = 16;
                plyr.facing = 2;
                plyr.map = 1; // The Dungeon
                plyr.scenario = 1; // The Dungeon
                plyr.z_offset = 1.0f;
                plyr.mapWidth = 64;
                plyr.mapHeight = 64;
                LoadMapData(1);
                LoadDescriptions(1);
                LoadZoneData(1);
                LoadMessages(1);
            }

            if ((plyr.x == 31) && (plyr.y == 0) && (plyr.map == 2)) // from dungeon 2 to dungeon 1 ne fix
            {
                plyr.x = 48;
                plyr.y = 17;
                plyr.facing = 1;
                plyr.map = 1; // The Dungeon
                plyr.scenario = 1; // The Dungeon
                plyr.z_offset = 1.0f;
                plyr.mapWidth = 64;
                plyr.mapHeight = 64;
                LoadMapData(1);
                LoadDescriptions(1);
                LoadZoneData(1);
                LoadMessages(1);
            }

            if ((plyr.x == 17) && (plyr.y == 12) && (plyr.map == 2)) // from dungeon 2 to dungeon 3 fix
            {
                plyr.x = 9;
                plyr.y = 3;
                plyr.facing = 1;
                plyr.map = 3; // The Dungeon level 3
                plyr.scenario = 1; // The Dungeon
                plyr.z_offset = 1.0f;
                plyr.mapWidth = 32; // due to rooms of confusion
                plyr.mapHeight = 32; // due to rooms of confusion
                LoadMapData(3);
                LoadDescriptions(3);
                LoadZoneData(3); // temp
                LoadMessages(2); // temp
            }

            if ((plyr.x == 10) && (plyr.y == 3) && (plyr.map == 3)) // from dungeon 3 to dungeon 2
            {
                plyr.x = 16;
                plyr.y = 12;
                plyr.facing = 1;
                plyr.map = 2; // The Dungeon level 2
                plyr.scenario = 1; // The Dungeon
                plyr.z_offset = 1.0f;
                plyr.mapWidth = 32;
                plyr.mapHeight = 32;
                LoadMapData(2);
                LoadDescriptions(2);
                LoadZoneData(2); // temp
                LoadMessages(2); // temp
            }

            if ((plyr.x == 6) && (plyr.y == 15) && (plyr.map == 3)) // from dungeon 3 to dungeon 2
            {
                plyr.x = 14;
                plyr.y = 22;
                plyr.facing = 2;
                plyr.map = 2; // The Dungeon level 2
                plyr.scenario = 1; // The Dungeon
                plyr.z_offset = 1.0f;
                plyr.mapWidth = 32;
                plyr.mapHeight = 32;
                LoadMapData(2);
                LoadDescriptions(2);
                LoadZoneData(2); // temp
                LoadMessages(2); // temp
            }
        }

        public static void MoveMapLevelTeleport ()
        {
            plyr.z_offset = 1.0f;
            plyr.mapWidth = maps[plyr.map].width;
            plyr.mapHeight = maps[plyr.map].height;
            LoadMapData(plyr.map);
            LoadDescriptions(plyr.map);
            LoadZoneData(plyr.map);
            LoadMessages(plyr.map);
        }

        public static void PrintSpecial ()
        {
            // 80 - 9F for encounters
            for (var y = 0; y < plyr.mapHeight; y++)
            {
                for (var x = 0; x < plyr.mapWidth; x++)
                {
                    var mapIdx = GetMapIndex(x, y);
                    if ((levelmap[mapIdx].special > 0x79) && (levelmap[mapIdx].special < 0x9F))
                    {
                        Console.Write($"{x}");
                        Console.Write($"{","}");
                        Console.Write($"{y}");
                        Console.Write($"{","}");
                        Console.Write($"{levelmap[mapIdx].special:x}");
                        Console.Write($"{",\n":x}");
                    }
                }
            }
        }

        public static void SaveHumanReadableMap ()
        {
            var outdata = new ofstream(); // outdata is like cin
            outdata.open("data/map/dungeon4.txt"); // opens the file
            if (outdata == null)
            { // file couldn't be opened
                cerr << "Error: map file could not be saved" << "\n";
                Environment.Exit(1);
            }

            var z = 0; // Up to 4096 - current mapcell
            for (var y = 0; y < 32; ++y)
            {
                for (var x = 0; x < 32; ++x)
                {
                    outdata << "# Map Cell Item: " << x << "," << y << "\n";
                    outdata << "East wall = " << levelmap[z].east << "\n";
                    outdata << "North wall = " << levelmap[z].north << "\n";
                    outdata << "West wall = " << levelmap[z].west << "\n";
                    outdata << "South wall = " << levelmap[z].south << "\n";
                    outdata << "East wall height = " << 1 << "\n";
                    outdata << "North wall height = " << 1 << "\n";
                    outdata << "West wall height = " << 1 << "\n";
                    outdata << "South wall height = " << 1 << "\n";
                    outdata << "Ceiling = " << 0 << "\n";
                    outdata << "Floor = " << 0 << "\n";
                    outdata << "Zone = " << 0 << "\n";
                    outdata << "Location = " << levelmap[z].location << "\n";
                    outdata << "Special = " << levelmap[z].special << "\n";
                    outdata << "      " << "\n";
                    z++;
                }
            }
            outdata.close();
        }

        public static void SetCurrentZone ()
        {
            // x >= entry 3 x2
            // x < entry 1 x1
            // y >= entry 0 y1
            // y < entry 2 y2

            var identifiedZone = 99; // 99 used to represent not part of a zone - default
            var x = plyr.x;
            var y = plyr.y;

            for (var z = 0; z < 255; ++z) // 44
            {
                if ((zones2[z].y1 <= y) && (zones2[z].y2 > y) && (zones2[z].x1 > x) && (zones2[z].x2 <= x))
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

            if (facing == 1) // facing west?
            {
                plyr.back = levelmap[idx].east;
                plyr.left = levelmap[idx].south;
                plyr.right = levelmap[idx].north;
                plyr.front = levelmap[idx].west;
                plyr.leftheight = levelmap[idx].southHeight;
                plyr.rightheight = levelmap[idx].northHeight;
                plyr.frontheight = levelmap[idx].westHeight;
            }

            if (facing == 2) // facing north?
            {
                plyr.back = levelmap[idx].south;
                plyr.left = levelmap[idx].west;
                plyr.right = levelmap[idx].east;
                plyr.front = levelmap[idx].north;
                plyr.leftheight = levelmap[idx].westHeight;
                plyr.rightheight = levelmap[idx].eastHeight;
                plyr.frontheight = levelmap[idx].northHeight;
            }

            if (facing == 3) // facing east?
            {
                plyr.back = levelmap[idx].west;
                plyr.right = levelmap[idx].south;
                plyr.left = levelmap[idx].north;
                plyr.front = levelmap[idx].east;
                plyr.rightheight = levelmap[idx].southHeight;
                plyr.leftheight = levelmap[idx].northHeight;
                plyr.frontheight = levelmap[idx].eastHeight;
            }

            if (facing == 4) // facing south?
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

        //extern zoneRecord zones[28];

        // extern Player plyr;
    }
}