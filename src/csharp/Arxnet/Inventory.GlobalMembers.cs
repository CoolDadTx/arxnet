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
    // Inventory.cpp
    //
    // Handles the binary object block that holds all information for in game items
    //
    // Items, Corpses, Curses, Diseases, Spells "carried" by player
    //
    // newItemBuffer is used to hold the locations of the items in the itemBlock
    //
    // Items are located by a reference number - starting with 0 for the first item in "inventory"
    public partial class GlobalMembers
    {        
        public static readonly int inventorySize = 4096;

        public static void ClearInventory ()
        {
            for (var i = 0; i < inventorySize; i++)
                itemBlock[i] = 0;
            newItemOffset = 0;
        }

        public static void AddNewItem ( int itemRef )
        {
            // Add new entry to newItemBuffer
            newItemBuffer[newItemBufferRef].offset = newItemOffset;

            // Adds a new Dungeon item to the inventory stack - not to the player's inventory
            // E.g addNewItem(EBON_BLADE)

            var itemSize = dungeonItems[itemOffset + 1];
            var idx = newItemOffset;

            for (var i = 0; i < itemSize; i++)
            {
                itemBlock[idx] = dungeonItems[itemOffset + i];
                idx++;
            }
            newItemOffset = newItemOffset + itemSize;
        }

        public static int itemOffset; // Position of current item
        public static int newItemOffset; // Position where next new item should be added

        public static int itemBufferRef;
        public static int newItemBufferRef; // Position where next new item refs should be added

        public static byte[] itemBlock = new byte[inventorySize];
        public static BufferItem[] newItemBuffer = Arrays.InitializeWithDefaultInstances<BufferItem>(itemBufferSize);

        public static string ReadItemName ( int stringOffset )
        {            
            var value = ReadBinaryString(itemBlock, stringOffset);
            
            //TODO: Why are we doing this
            value = value.Replace(Char.ConvertFromUtf32(32), Char.ConvertFromUtf32(95));

            //TODO: Leaving this out for now, just make sure name is valid
            //if ((c > 96) && (c < 123))
            //   c = c - 32;

            return value;
        }

        //extern byte itemBlock[inventorySize];

        //MLT: Extern does not match definition, going with definition
        //extern bufferItem newItemBuffer[1024];
        //extern bufferItem newItemBuffer[itemBufferSize];

        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //void DeleteItem(int itemNo);
        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //void DeleteItem(int itemNo);    
    }
}