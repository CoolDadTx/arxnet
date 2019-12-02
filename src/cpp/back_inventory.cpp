// Inventory.cpp
//
// Handles the binary object block that holds all information for in game items
//
// Items, Corpses, Curses, Diseases, Spells "carried" by player
//
// newItemBuffer is used to hold the locations of the items in the itemBlock
//
// Items are located by a reference number - starting with 0 for the first item in "inventory"


#include <string>
#include <sstream>
//MLT: Changed order so that itemBufferSize is defined first
#include "items.h"
//MLT: inventory.h does not exist but back_inventory.h does
#include "back_inventory.h"

int itemOffset;         // Position of current item
int newItemOffset;      // Position where next new item should be added

int itemBufferRef;
int newItemBufferRef;   // Position where next new item refs should be added

unsigned char itemBlock[inventorySize];
bufferItem newItemBuffer[itemBufferSize];

std::string readItemName(int stringOffset)
{
    std::stringstream ss;
    int z = stringOffset; // current location in the binary
    int c = 0;          // current byte
    std::string result = "";

   while (!(itemBlock[z]==0))
    {
        c = itemBlock[z];

        if (c == 32) { c = 95; }                    // Convert space to underscore
        if ((c > 96) && (c < 123)) { c = c - 32; }    // Capitalise lower case letters

        ss << (char) c;
        z++;
    }
    result = ss.str();
    return result;
}

void clearInventory()
{
    for ( int i = 0 ; i < inventorySize ; i++ )
    {
        itemBlock[i] = 0;
    }
    newItemOffset = 0;
}



void addNewItem(int itemRef)
{
    // Add new entry to newItemBuffer
    newItemBuffer[newItemBufferRef].offset = newItemOffset;

    //MLT: Bad code
    //newItemBuffer[newItemBufferRef].

    // Adds a new Dungeon item to the inventory stack - not to the player's inventory
    // E.g addNewItem(EBON_BLADE)

//    itemOffset = itemOffsets[itemRef];
    int itemSize = dungeonItems[itemOffset+1];
    int idx = newItemOffset;

    for (int i=0 ; i < itemSize ; i++)
    {
        itemBlock[idx] = dungeonItems[itemOffset+i];
        idx++;
    }
    newItemOffset = newItemOffset + itemSize;

    // Add new entry to newItemBuffer
    //newItemBuffer[newItemBufferRef].offset = ;

}


void deleteItem(int itemNo);



