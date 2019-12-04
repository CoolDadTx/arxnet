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
	public class ClothingItem
	{
		// Description is created from values below if name blank
		// Clothing items have no body location
		// 4 items can be worn at same time
		public string name { get; set; }
		public int quality { get; set; }
		public int colour { get; set; }
		public int fabric { get; set; }
		public int type { get; set; }
		public int weight { get; set; }
	}


	public class PotionItem
	{
		   public string name { get; set; }
		   public string color { get; set; }
		   public string taste { get; set; }
		   public string sip { get; set; }
	}

	public class QuestItem
	{
		   public string name { get; set; }
		   public int weight { get; set; }
	}

	public class BufferItem
	{
		public int type { get; set; } // 83, 03, etc
		public int index { get; set; } // in appropriate array (e.g. armour array) dependent on type above
		// 0 - limbo, 1 - floor, 10 - carried, 11 - primary, 12 - secondary,
		// 13 - worn, 14 - head, 15 - arms, 16 - legs, 17 - body
		public int location { get; set; }
		public int x { get; set; }
		public int y { get; set; }
		public int level { get; set; }
		public int hp { get; set; } // hp or no. of charges or no. of items for generic items like food packets

		public string name { get; set; }
		public int maxHP { get; set; }
		public int flags { get; set; }
		public int minStrength { get; set; }
		public int minDexterity { get; set; }
		public int useStrength { get; set; }
		public int blunt { get; set; }
		public int sharp { get; set; }
		public int earth { get; set; }
		public int air { get; set; }
		public int fire { get; set; }
		public int water { get; set; }
		public int power { get; set; }
		public int magic { get; set; } // mental
		public int good { get; set; } // cleric
		public int evil { get; set; }
		public int cold { get; set; }
		public int weight { get; set; }
		public int alignment { get; set; }
		public int melee { get; set; }
		public int ammo { get; set; }
		public int parry { get; set; }
	}

	public partial class GlobalMembers
	{
		public static readonly int itemBufferSize = 250;

		public static readonly int dungeonItemsSize = 10496;
		//extern byte dungeonItems[dungeonItemsSize];
		//extern int itemOffsets[141];

		public static void LoadDungeonItems()
		{
			// Load items into binary char array

			FileStream fp; // file pointer - used when reading files
			string tempString = new string(new char[100]); // temporary string
			tempString = string.Format("{0}{1}", "data/map/", "items.bin");
			fp = fopen(tempString, "rb");
			if (fp != null)
			{
				for (int i = 0;i < dungeonItemsSize;i++)
					dungeonItems[i] = fgetc(fp);
			}
			fclose(fp);
		}
		public static void CreateBareHands()
		{
			int itemRef = CreateItem(178, 0, "bare hand", 255, 255, 6, 0, 0, 1, 0x15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 255, 0, 0);
			itemBuffer[itemRef].location = 99; // body part of player - 99 so it doesn't show up in the inventory
			//plyr.priWeapon = itemRef;
			//plyr.secWeapon = itemRef;
		}


		public static int CreateItem(int type, int index, string name, int hp, int maxHP, int flags, int minStrength, int minDexterity, int useStrength, int blunt, int sharp, int earth, int air, int fire, int water, int power, int magic, int good, int evil, int cold, int weight, int alignment, int melee, int ammo, int parry)
		{
			// Create a new item in itemBuffer[]

			// 0 - limbo, 1 - floor, 10 - carried, 11 - primary, 12 - secondary,
			// 13 - worn, 14 - head, 15 - arms, 16 - legs, 17 - body

			// Clean up itemBuffer[] before adding a new item
			TidyObjectBuffer();

			// Create a new item
			BufferItem new_item = new BufferItem();

			// Set item attributes
			new_item.type = type;
			new_item.index = index;
			new_item.name = name;
			new_item.hp = hp;
			new_item.maxHP = maxHP;
			new_item.flags = flags;
			new_item.minStrength = minStrength;
			new_item.minDexterity = minDexterity;
			new_item.useStrength = useStrength;
			new_item.blunt = blunt;
			new_item.sharp = sharp;
			new_item.earth = earth;
			new_item.air = air;
			new_item.fire = fire;
			new_item.water = water;
			new_item.power = power;
			new_item.magic = magic;
			new_item.good = good;
			new_item.evil = evil;
			new_item.cold = cold;
			new_item.weight = weight;
			new_item.alignment = alignment;
			new_item.melee = melee;
			new_item.ammo = ammo;
			new_item.parry = parry;

			// Set location attributes
			new_item.location = 1; // the floor
			new_item.x = plyr.x;
			new_item.y = plyr.y;
			new_item.level = plyr.map;

			// Update buffer and buffer references
			itemBuffer[plyr.buffer_index] = new_item;
			int new_item_ref = plyr.buffer_index;
			plyr.buffer_index++;
			return new_item_ref; // what was the new items index in the object buffer
		}

		public static string GetItemDesc(int itemRef)
		{
			string itemDesc = "ERROR";
			if (itemBuffer[itemRef].type == 176)
				itemDesc = "Potion";
			if (itemBuffer[itemRef].type == 177)
				itemDesc = itemBuffer[itemRef].name;
			if (itemBuffer[itemRef].type == 178)
				itemDesc = itemBuffer[itemRef].name;
			if (itemBuffer[itemRef].type == 180)
				itemDesc = itemBuffer[itemRef].name;
			if (itemBuffer[itemRef].type == 199)
				itemDesc = itemBuffer[itemRef].name;
			if (itemBuffer[itemRef].type == 200)
				itemDesc = questItems[(itemBuffer[itemRef].index)].name;
			if (itemBuffer[itemRef].type == 201)
				itemDesc = questItems[(itemBuffer[itemRef].index)].name;
			return itemDesc;
		}
		public static void MoveItem(int itemRef, int newLocation)
		{
			itemBuffer[itemRef].location = newLocation;
			if (plyr.priWeapon == itemRef)
				plyr.priWeapon = 255;
			if (plyr.secWeapon == itemRef)
				plyr.secWeapon = 255;
			if (plyr.armsArmour == itemRef)
				plyr.armsArmour = 255;
			if (plyr.legsArmour == itemRef)
				plyr.legsArmour = 255;
			if (plyr.headArmour == itemRef)
				plyr.headArmour = 255;
			if (plyr.bodyArmour == itemRef)
				plyr.bodyArmour = 255;
			if (plyr.clothing[0] == itemRef)
				plyr.clothing[0] = 255;
			if (plyr.clothing[1] == itemRef)
				plyr.clothing[1] = 255;
			if (plyr.clothing[2] == itemRef)
				plyr.clothing[2] = 255;
			if (plyr.clothing[3] == itemRef)
				plyr.clothing[3] = 255;
		}

		public static void CannotCarryMessage()
		{
			string key = "";
			while (key != "SPACE")
			{
				DispMain();
				CText("@You cannot carry any more!@@@@(Press SPACE to continue)");
				UpdateDisplay();
				key = GetSingleKey();
			}
		}
		public static void DisplayLocation()
		{
			string str; // for message text
			string key;
			string levelDesc;
			bool keynotpressed = true;
			int squaresNorth = 63 - plyr.y;
			while (keynotpressed)
			{
				if (plyr.map == 0)
					levelDesc = "the City";
				if (plyr.map == 1)
					levelDesc = "level 1";
				if (plyr.map == 2)
				{
					levelDesc = "level 2";
					squaresNorth = 31 - plyr.y;
				}
				if (plyr.map == 3)
				{
					levelDesc = "level 3";
					squaresNorth = 15 - plyr.y;
				}
				if (plyr.map == 4)
				{
					levelDesc = "level 4";
					squaresNorth = 7 - plyr.y;
				}

				if (plyr.status == 3)
					DrawEncounterView();
				if (plyr.status == 1)
					DispMain();
				if (plyr.status == 0)
					DispMain();
				str = "You are " + Itos(squaresNorth) + " squares North@and " + Itos(plyr.x) + " squares East from the SouthWest@corner of " + levelDesc + ".";
				CyText(3, str);
				CyText(8, "<<< Press any key to continue >>>");
				UpdateDisplay();
				key = GetSingleKey();
				if (key != "")
					keynotpressed = false;
			}
		}

		public static int CreateQuestItem(int questItemNo)
		{
			 // location options:
			 // 0 - limbo, 1 - floor, 10 - carried, 11 - primary, 12 - secondary,
			// 13 - worn, 14 - head, 15 - arms, 16 - legs, 17 - body
			 //int weapon_no = encounters[monster_no].weapon_no;
			TidyObjectBuffer();
			BufferItem new_item = new BufferItem();
			 new_item.type = 200; // type for quest items e.g ring halves, silver key etc
			 new_item.index = questItemNo; // for quest items index is used to identify the object
			 new_item.location = 1; // the floor
			 new_item.x = plyr.x;
			 new_item.y = plyr.y;
			 new_item.level = plyr.map;
			 itemBuffer[plyr.buffer_index] = new_item;
			 int new_item_ref = plyr.buffer_index;
			 plyr.buffer_index++;
			 return new_item_ref; // what was the new items index in the object buffer
		}
		public static int CreateWeapon(int weapon_no)
		{
			// Create a new monster weapon on the floor

			// 0 - limbo, 1 - floor, 10 - carried, 11 - primary, 12 - secondary,
			// 13 - worn, 14 - head, 15 - arms, 16 - legs, 17 - body

			TidyObjectBuffer();
			BufferItem new_item = new BufferItem();

			// Set weapon type
			new_item.type = monsterWeapons[weapon_no].type;
			new_item.type = 178; // type for a weapon that can be wielded (e.g not claws)
			new_item.index = weapon_no; // Reference to monsterWeapons - currently index not a binary offset

			// Copy weapon attributes
			new_item.name = monsterWeapons[weapon_no].name;
			new_item.hp = monsterWeapons[weapon_no].hp;
			new_item.maxHP = monsterWeapons[weapon_no].maxHP;
			new_item.flags = monsterWeapons[weapon_no].flags;
			new_item.minStrength = monsterWeapons[weapon_no].minStrength;
			new_item.minDexterity = monsterWeapons[weapon_no].minDexterity;
			new_item.useStrength = monsterWeapons[weapon_no].useStrength;
			new_item.blunt = monsterWeapons[weapon_no].blunt;
			new_item.sharp = monsterWeapons[weapon_no].sharp;
			new_item.earth = monsterWeapons[weapon_no].earth;
			new_item.air = monsterWeapons[weapon_no].air;
			new_item.fire = monsterWeapons[weapon_no].fire;
			new_item.water = monsterWeapons[weapon_no].water;
			new_item.power = monsterWeapons[weapon_no].power;
			new_item.magic = monsterWeapons[weapon_no].magic;
			new_item.good = monsterWeapons[weapon_no].good;
			new_item.evil = monsterWeapons[weapon_no].evil;
			new_item.cold = monsterWeapons[weapon_no].cold;
			new_item.weight = monsterWeapons[weapon_no].weight;
			new_item.alignment = monsterWeapons[weapon_no].alignment;
			new_item.melee = monsterWeapons[weapon_no].melee; // Don't think needed
			new_item.ammo = monsterWeapons[weapon_no].ammo; // Don't think needed
			new_item.parry = monsterWeapons[weapon_no].parry;

			// Set weapon location
			new_item.location = 1; // the floor
			new_item.x = plyr.x;
			new_item.y = plyr.y;
			new_item.level = plyr.map;

			// Update buffer and buffer references
			itemBuffer[plyr.buffer_index] = new_item;
			int new_item_ref = plyr.buffer_index;
			plyr.buffer_index++;
			return new_item_ref; // what was the new items index in the object buffer
		}
		public static int CreateClothing(int clothing_no)
		{
			 TidyObjectBuffer();
			 BufferItem new_item = new BufferItem();
			 new_item.name = clothingItems[clothing_no].name;
			 new_item.type = 180; // clothing type
			 new_item.index = clothing_no;
			 new_item.location = 10; // carried in inventory but not in use
			 new_item.x = plyr.x;
			 new_item.y = plyr.y;
			 new_item.level = plyr.map;
			 new_item.hp = 12; // temp value to act as breakable value
			 itemBuffer[plyr.buffer_index] = new_item;
			 int new_item_ref = plyr.buffer_index;
			 plyr.buffer_index++;
			 return new_item_ref; // what was the new items index in the object buffer
		}
		public static int CreateArmor(int armor_no)
		{
			 // location options:
			 // 0 - limbo, 1 - floor, 10 - carried, 11 - primary, 12 - secondary,
			 // 13 - worn, 14 - head, 15 - arms, 16 - legs, 17 - body
			 TidyObjectBuffer();
			 BufferItem new_item = new BufferItem();
			 new_item.type = 177; // temporary object type to indicate armor
			 new_item.index = armor_no;
			 new_item.location = 10; // carried in inventory but not in use
			 new_item.x = plyr.x;
			 new_item.y = plyr.y;
			 new_item.level = plyr.map;
			 new_item.hp = 12; // temp value to act as breakable value
			 itemBuffer[plyr.buffer_index] = new_item;
			 int new_item_ref = plyr.buffer_index;
			 plyr.buffer_index++;
			 return new_item_ref; // what was the new items index in the object buffer
		}
		public static int CreatePotion(int potion_no)
		{
			 // location options:
			 // 0 - limbo, 1 - floor, 10 - carried, 11 - primary, 12 - secondary,
			// 13 - worn, 14 - head, 15 - arms, 16 - legs, 17 - body
			 TidyObjectBuffer();
			 BufferItem new_item = new BufferItem();
			 new_item.type = 176; // object type to indicate potion
			 new_item.index = potion_no; // Index will define which of 42 potion types this is
			 new_item.location = 1; // On floor after encounter
			 new_item.x = plyr.x;
			 new_item.y = plyr.y;
			 new_item.level = plyr.map;
			 new_item.hp = 0; // For potions 0 indicates unidentified
			 itemBuffer[plyr.buffer_index] = new_item;
			 int new_item_ref = plyr.buffer_index;
			 plyr.buffer_index++;
			 return new_item_ref; // what was the new items index in the object buffer
		}

		public static void SwapClothing(int object_ref)
		{
			bool keypressed = false;
			string key;
			string str;

			while (!keypressed)
			{
				if (plyr.status == 2)
					ClearGuildDisplay();
				if (plyr.status == 3)
					DrawEncounterView();
				if (plyr.status == 1)
					DispMain();
				if (plyr.status == 0)
					DispMain();

				CyText(1, "Wear instead of:");
				str = "(1) " + clothingItems[itemBuffer[plyr.clothing[0]].index].name;
				BText(5, 3, str);
				str = "(2) " + clothingItems[itemBuffer[plyr.clothing[1]].index].name;
				BText(5, 4, str);
				str = "(3) " + clothingItems[itemBuffer[plyr.clothing[2]].index].name;
				BText(5, 5, str);
				str = "(4) " + clothingItems[itemBuffer[plyr.clothing[3]].index].name;
				BText(5, 6, str);
				BText(2, 8, "Item # or ESC to exit");
				SetFontColour(40, 96, 244, 255);
				BText(2, 8, "     #    ESC");
				SetFontColour(215, 215, 215, 255);

				//  clothing = itemBuffer[cur_idx].index;
				//  str = clothingItems[clothing].name;
				UpdateDisplay();

				key = GetSingleKey();
				if (key == "1")
				{
					plyr.clothing[0] = object_ref;
					keypressed = true;
				}
				if (key == "2")
				{
					plyr.clothing[1] = object_ref;
					keypressed = true;
				}
				if (key == "3")
				{
					plyr.clothing[2] = object_ref;
					keypressed = true;
				}
				if (key == "4")
				{
					plyr.clothing[3] = object_ref;
					keypressed = true;
				}
				if (key == "ESC")
					keypressed = true;
			}
		}
		public static int CreateGenericItem(int type, int value)
		{
			// generic item type = 1 - food, 2 - water, 3 - torches, 4 - timepieces, 5 - compasses
			 // location options:
			 // 0 - limbo, 1 - floor, 10 - carried, 11 - primary, 12 - secondary,
			// 13 - worn, 14 - head, 15 - arms, 16 - legs, 17 - body
			 //int weapon_no = encounters[monster_no].weapon_no;
			TidyObjectBuffer();
			BufferItem new_item = new BufferItem();
			 //new_item.index = 0; // should be weapon_no?
			 new_item.type = type; // gold, crystals, keys, gems
			 //new_item.index = weapon_no;
			 new_item.location = 1; // the floor
			 new_item.x = plyr.x;
			 new_item.y = plyr.y;
			 new_item.level = plyr.map;
			 //new_item.hp = Weapons[weapon_no].hp;
			 new_item.hp = value; // for generic items sets number e.g. 4 food packets, 3 gold
			 itemBuffer[plyr.buffer_index] = new_item;
			 int new_item_ref = plyr.buffer_index;
			 plyr.buffer_index++;
			 return new_item_ref; // what was the new items index in the object buffer
		}
		public static bool CheckForQuestItem(int itemNo)
		{
		  // checks through item buffer for carried quest items of type 200
		  bool response = false;
		  int cur_idx = 0;
		  while (cur_idx < plyr.buffer_index)
		  {
				if ((itemBuffer[cur_idx].location == 10) && (itemBuffer[cur_idx].type == 200) && (itemBuffer[cur_idx].index == itemNo))
					response = true;
				if ((itemBuffer[cur_idx].location == 10) && (itemBuffer[cur_idx].type == 201) && (itemBuffer[cur_idx].index == itemNo))
					response = true;
				cur_idx++;
		  }
		  return response;
		}
		public static int GetQuestItemRef(int itemNo)
		{
		  // checks through item buffer for carried quest items of type 200
		  int response = 255; // 255 indicates no match
		  int cur_idx = 0;
		  while (cur_idx < plyr.buffer_index)
		  {
				if ((itemBuffer[cur_idx].location == 10) && (itemBuffer[cur_idx].type == 200) && (itemBuffer[cur_idx].index == itemNo))
					response = cur_idx;
				if ((itemBuffer[cur_idx].location == 10) && (itemBuffer[cur_idx].type == 201) && (itemBuffer[cur_idx].index == itemNo))
					response = cur_idx;
				cur_idx++;
		  }
		  return response;
		}
		public static void GetItems()
		{
		  // types = 1 - food, 2 - water, 3 - torches, 4 - timepieces, 5 - compasses
		  int cur_idx = 0;
		  bool noGetQuit = true;
		  while ((cur_idx <= plyr.buffer_index) && (noGetQuit))
		  {
				if ((itemBuffer[cur_idx].x == plyr.x) && (itemBuffer[cur_idx].y == plyr.y) && (itemBuffer[cur_idx].level == plyr.map) && (itemBuffer[cur_idx].location == 1))
				{

				string str;
				string key_value;
				bool keypressed = false;

				while (!keypressed)
				{
					DispMain();
					DrawConsoleBackground();
					CyText(1, "GET?");
					if (itemBuffer[cur_idx].type == 1)
						str = Itos(itemBuffer[cur_idx].hp) + " Food Packet(s)";
					if (itemBuffer[cur_idx].type == 2)
						str = Itos(itemBuffer[cur_idx].hp) + " Water Flask(s)";
					if (itemBuffer[cur_idx].type == 3)
						str = Itos(itemBuffer[cur_idx].hp) + " Torch(es)";
					if (itemBuffer[cur_idx].type == 4)
						str = Itos(itemBuffer[cur_idx].hp) + " Timepiece(s)";
					if (itemBuffer[cur_idx].type == 5)
						str = Itos(itemBuffer[cur_idx].hp) + " Compass(es)";
					if (itemBuffer[cur_idx].type == 6)
						str = Itos(itemBuffer[cur_idx].hp) + " Key(s)";
					if (itemBuffer[cur_idx].type == 7)
						str = Itos(itemBuffer[cur_idx].hp) + " Crystal(s)";
					if (itemBuffer[cur_idx].type == 8)
						str = Itos(itemBuffer[cur_idx].hp) + " Gem(s)";
					if (itemBuffer[cur_idx].type == 9)
						str = Itos(itemBuffer[cur_idx].hp) + " Jewel(s)";
					if (itemBuffer[cur_idx].type == 10)
						str = Itos(itemBuffer[cur_idx].hp) + " Gold";
					if (itemBuffer[cur_idx].type == 11)
						str = Itos(itemBuffer[cur_idx].hp) + " Silver";
					if (itemBuffer[cur_idx].type == 12)
						str = Itos(itemBuffer[cur_idx].hp) + " Copper";
					if ((itemBuffer[cur_idx].type == 176) && (itemBuffer[cur_idx].hp == 0))
						str = "Potion";
					if ((itemBuffer[cur_idx].type == 176) && (itemBuffer[cur_idx].hp == 1))
						str = Potions[(itemBuffer[cur_idx].index)].name;
					if (itemBuffer[cur_idx].type == 177)
						str = itemBuffer[cur_idx].name;
					if (itemBuffer[cur_idx].type == 178)
						str = itemBuffer[cur_idx].name;
					if (itemBuffer[cur_idx].type == 199)
						str = itemBuffer[cur_idx].name;
					if (itemBuffer[cur_idx].type == 180)
						str = itemBuffer[cur_idx].name;
					if (itemBuffer[cur_idx].type == 200)
						str = questItems[(itemBuffer[cur_idx].index)].name;
					CyText(4, str);
					CyText(7, "Yes, No or ESC.");
					UpdateDisplay();

					key_value = GetSingleKey();

					if (key_value == "Y")
					{
						string encText = CheckEncumbrance();
						if (encText == "Immobilized!")
							CannotCarryMessage();
						else
						{
							if (itemBuffer[cur_idx].type < 13) // get food packets
							{
								int type = itemBuffer[cur_idx].type;
								if (type == 1)
									plyr.food += (itemBuffer[cur_idx].hp);
								if (type == 2)
									plyr.water += (itemBuffer[cur_idx].hp);
								if (type == 3)
									plyr.torches += (itemBuffer[cur_idx].hp);
								if (type == 4)
									plyr.timepieces += (itemBuffer[cur_idx].hp);
								if (type == 5)
									plyr.compasses += (itemBuffer[cur_idx].hp);
								if (type == 6)
									plyr.keys += (itemBuffer[cur_idx].hp);
								if (type == 7)
									plyr.crystals += (itemBuffer[cur_idx].hp);
								if (type == 8)
									plyr.gems += (itemBuffer[cur_idx].hp);
								if (type == 9)
									plyr.jewels += (itemBuffer[cur_idx].hp);
								if (type == 10)
									plyr.gold += (itemBuffer[cur_idx].hp);
								if (type == 11)
									plyr.silver += (itemBuffer[cur_idx].hp);
								if (type == 12)
									plyr.copper += (itemBuffer[cur_idx].hp);
								itemBuffer[cur_idx].location = 0; // remove this item to the void
							}

							if (itemBuffer[cur_idx].type > 150)
								itemBuffer[cur_idx].location = 10; // moved to player inventory
						}
						keypressed = true;
					}


					if (key_value == "N")
						keypressed = true;

					if (key_value == "ESC")
					{
						keypressed = true;
						noGetQuit = false;
					}
				}

					//int weapon_idx, weapon_type;
					//weapon_idx = itemBuffer[cur_idx].index;
					//weapon_type = itemBuffer[cur_idx].type;
					//str = "GET?@@ " + weapons[weapon_idx].name + "@@Yes, No or ESC";
					//newtext(str);

				}
				cur_idx++;
		  }
		}

		public static int SelectItem(int selectItemMode)
		{
			// 1 - USE, 2 - DROP, 3 - OFFER, 4 - Deposit
			// item types : 1-weapon 177-armour
			int itemRef = 9999; // Nothing selected
			string str;
			string selectDesc;
			if (selectItemMode == 1)
				selectDesc = "USE";
			if (selectItemMode == 2)
				selectDesc = "DROP";
			if (selectItemMode == 3)
				selectDesc = "OFFER";
			if (selectItemMode == 4)
				selectDesc = "Deposit";
			if (selectItemMode == 5)
				selectDesc = "Withdrawal";

			int menuitem1 = 255; // 255 is used here as nil
			int menuitem2 = 255;
			int menuitem3 = 255;
			int menuitem4 = 255;
			bool selectDone = false;

			int no_items = 0;
			int cur_idx = 0;
			int pages = 0;
			int page = 0;
			int weapon = 0;
			int armor = 0;
			int clothing = 0;
			//int itemCount;
			//int pageStartItem;
			int page_item = 0;

			while (cur_idx < plyr.buffer_index)
			{
				if (itemBuffer[cur_idx].location == 10)
					no_items++;
				cur_idx++;
			}
			pages = 0;
			int noPages = no_items / 4; // based on 4 oncreen items per page
			pages += noPages;
			int tempRemainder = no_items % 4;
			if (tempRemainder != 0)
				pages++;

			while (!selectDone)
			{
				if (page == 0)
				{
					// this is effectively page 0 in terms of using items
					if (plyr.status == 2)
						ClearGuildDisplay();
					if (plyr.status == 3)
						DrawEncounterView();
					if (plyr.status == 1)
					{
						DispMain();
						DrawConsoleBackground();
					}
					if (plyr.status == 0)
					{
						DispMain();
						DrawConsoleBackground();
					}

					//if (plyr.status == 3 ) { drawEncounterView(); }
					//else { dispMain(); }

					CyText(1, selectDesc);
					str = "(1) Food Packets: " + Itos(plyr.food);
					if (selectItemMode == 5)
						str = "(1) Food Packets: " + Itos(plyr.lfood);
					BText(5, 3, str);

					str = "(2) Water Flasks: " + Itos(plyr.water);
					if (selectItemMode == 5)
						str = "(2) Water Flasks: " + Itos(plyr.lwater);
					BText(5, 4, str);

					str = "(3) Unlit Torches: " + Itos(plyr.torches);
					if (selectItemMode == 5)
						str = "(3) Unlit Torches: " + Itos(plyr.ltorches);
					BText(5, 5, str);

					str = "(4) Timepieces: " + Itos(plyr.timepieces);
					if (selectItemMode == 5)
						str = "(4) Timepieces: " + Itos(plyr.ltimepieces);
					BText(5, 6, str);

					BText(2, 8, "Item #, Forward, Back, or ESC to exit");
					SetFontColour(40, 96, 244, 255);
					BText(2, 8, "     #  F        B        ESC");
					SetFontColour(215, 215, 215, 255);
					//str = "Page: " + itos(page) + " of " + itos(pages);
					//bText(28, 1, str);
					UpdateDisplay();

					string key_value;
					key_value = GetSingleKey();
					if (key_value == "1")
					{
						itemRef = 1000;
						selectDone = true;
					}
					if (key_value == "2")
					{
						itemRef = 1001;
						selectDone = true;
					}
					if (key_value == "3")
					{
						itemRef = 1002;
						selectDone = true;
					}
					if (key_value == "4")
					{
						itemRef = 1003;
						selectDone = true;
					}
					if (key_value == "E")
						selectDone = true;
					if (key_value == "ESC")
						selectDone = true;
					if ((key_value == "F") || (key_value == "down"))
					{
						if ((selectItemMode == 1) && (pages > 0))
							page = 3;
						if ((selectItemMode == 2) || (selectItemMode == 3) || (selectItemMode == 4) || (selectItemMode == 5))
							page = 1;
					}

				}

				if (page == 1)
				{
					// this is effectively page 1 in terms of using items
					if (plyr.status == 2)
						ClearGuildDisplay();
					if (plyr.status == 3)
						DrawEncounterView();
					if (plyr.status == 1)
					{
						DispMain();
						DrawConsoleBackground();
					}
					if (plyr.status == 0)
					{
						DispMain();
						DrawConsoleBackground();
					}

					CyText(1, selectDesc);
					str = "(1) Compasses: " + Itos(plyr.compasses);
					if (selectItemMode == 5)
						str = "(1) Compasses: " + Itos(plyr.lcompasses);
					BText(5, 3, str);
					str = "(2) Keys: " + Itos(plyr.keys);
					if (selectItemMode == 5)
						str = "(2) Keys: " + Itos(plyr.lkeys);
					BText(5, 4, str);
					str = "(3) Crystals: " + Itos(plyr.crystals);
					if (selectItemMode == 5)
						str = "(3) Crystals: " + Itos(plyr.lcrystals);
					BText(5, 5, str);
					str = "(4) Gems: " + Itos(plyr.gems);
					if (selectItemMode == 5)
						str = "(4) Gems: " + Itos(plyr.lgems);
					BText(5, 6, str);
					BText(2, 8, "Item #, Forward, Back, or ESC to exit");
					SetFontColour(40, 96, 244, 255);
					BText(2, 8, "     #  F        B        ESC");
					SetFontColour(215, 215, 215, 255);
					//str = "Page: " + itos(page) + " of " + itos(pages);
					//bText(28, 1, str);
					UpdateDisplay();

					string key_value;
					key_value = GetSingleKey();
					if (key_value == "1")
					{
						itemRef = 1004;
						selectDone = true;
					}
					if (key_value == "2")
					{
						itemRef = 1005;
						selectDone = true;
					}
					if (key_value == "3")
					{
						itemRef = 1006;
						selectDone = true;
					}
					if (key_value == "4")
					{
						itemRef = 1007;
						selectDone = true;
					}
					if (key_value == "E")
						selectDone = true;
					if (key_value == "ESC")
						selectDone = true;
					if (key_value == "F")
						page = 2;
					if (key_value == "down")
						page = 2;
					if (key_value == "B")
						page = 0;
					if (key_value == "up")
						page = 0;
				}

				if (page == 2)
				{
					// this is effectively page 2 in terms of using items
					if (plyr.status == 2)
						ClearGuildDisplay();
					if (plyr.status == 3)
						DrawEncounterView();
					if (plyr.status == 1)
					{
						DispMain();
						DrawConsoleBackground();
					}
					if (plyr.status == 0)
					{
						DispMain();
						DrawConsoleBackground();
					}

					CyText(1, selectDesc);
					str = "(1) Jewels: " + Itos(plyr.jewels);
					if (selectItemMode == 5)
						str = "(1) Jewels: " + Itos(plyr.ljewels);
					BText(5, 3, str);
					str = "(2) Gold: " + Itos(plyr.gold);
					if (selectItemMode == 5)
						str = "(2) Gold: " + Itos(plyr.lgold);
					BText(5, 4, str);
					str = "(3) Silver: " + Itos(plyr.silver);
					if (selectItemMode == 5)
						str = "(3) Silver: " + Itos(plyr.lsilver);
					BText(5, 5, str);
					str = "(4) Copper: " + Itos(plyr.copper);
					if (selectItemMode == 5)
						str = "(4) Copper: " + Itos(plyr.lcopper);
					BText(5, 6, str);
					BText(2, 8, "Item #, Forward, Back, or ESC to exit");
					SetFontColour(40, 96, 244, 255);
					BText(2, 8, "     #  F        B        ESC");
					SetFontColour(215, 215, 215, 255);
					//str = "Page: " + itos(page) + " of " + itos(pages);
					//bText(28, 1, str);
					UpdateDisplay();

					string key_value;
					key_value = GetSingleKey();
					if (key_value == "1")
					{
						itemRef = 1008;
						selectDone = true;
					}
					if (key_value == "2")
					{
						itemRef = 1009;
						selectDone = true;
					}
					if (key_value == "3")
					{
						itemRef = 1010;
						selectDone = true;
					}
					if (key_value == "4")
					{
						itemRef = 1011;
						selectDone = true;
					}
					if (key_value == "E")
						selectDone = true;
					if (key_value == "ESC")
						selectDone = true;
					if ((key_value == "F") && (pages > 0) && (selectItemMode < 4))
						page = 3;
					if ((key_value == "down") && (pages > 0) && (selectItemMode < 4))
						page = 3;
					if (key_value == "B")
						page = 1;
					if (key_value == "up")
						page = 1;
				}

				if (page > 2) // Variable items
				{

					bool keypressed = false;
					while (!keypressed)
					{

						//itemCount = 0;
						if (plyr.status == 2)
							ClearGuildDisplay();
						if (plyr.status == 3)
							DrawEncounterView();
						if (plyr.status == 1)
						{
							DispMain();
							DrawConsoleBackground();
						}
						if (plyr.status == 0)
						{
							DispMain();
							DrawConsoleBackground();
						}
						CyText(1, selectDesc);
						BText(5, 3, "(1)");
						BText(5, 4, "(2)");
						BText(5, 5, "(3)");
						BText(5, 6, "(4)");
						BText(2, 8, "Item #, Forward, Back, or ESC to exit");
						SetFontColour(40, 96, 244, 255);
						BText(2, 8, "     #  F        B        ESC");
						SetFontColour(215, 215, 215, 255);
						//str = "Page: " + itos(page-2) + " of " + itos(pages);
						//bText(28, 1, str);



						// Identify starting value for cur_idx for page x of carried objects excluding items not carried
						int mypage = page-3; // should be 0 for page 0 (first page with items displayed on it)
						cur_idx = 0;
						if (mypage > 0)
						{
							int idx = 0;

							int tempPage = 0;
							int pageItems = 0;
							while ((mypage > tempPage) && (idx <= plyr.buffer_index))
							{
								if (itemBuffer[idx].location == 10)
								{
									cur_idx++;
									pageItems++;
									if (pageItems == 4)
									{
										tempPage++;
										pageItems = 0;
									}
								}
								if (itemBuffer[idx].location != 10)
									cur_idx++;
								idx++;
							}
						}


						//cur_idx = ((page-3)*4); // Problem?

						page_item = 1;
						//pageStartItem = ((pages-1)*4)+1;
						menuitem1 = 9999; // 9999 is used as nil
						menuitem2 = 9999;
						menuitem3 = 9999;
						menuitem4 = 9999;

						while ((cur_idx < plyr.buffer_index) && (page_item < 5))
						{
							if ((itemBuffer[cur_idx].location == 10))
							{

								// Display a Potion item
								if (itemBuffer[cur_idx].type == 176) // armour
								{
									int potion = itemBuffer[cur_idx].index;
									if (itemBuffer[cur_idx].hp == 0)
										str = "Potion";
									if (itemBuffer[cur_idx].hp == 1)
										str = Potions[potion].name;

								  //cout << cur_idx << " " << str << " " <<  Armor[armor].bodyLocation << endl;
								}

								// Display an armour, weapon or clothing item name
								if (itemBuffer[cur_idx].type == 177)
									str = itemBuffer[cur_idx].name;
								if (itemBuffer[cur_idx].type == 178)
									str = itemBuffer[cur_idx].name;
								if (itemBuffer[cur_idx].type == 180)
									str = itemBuffer[cur_idx].name;
								if (itemBuffer[cur_idx].type == 199)
									str = itemBuffer[cur_idx].name;

								// Display quest items in inventory
								if (itemBuffer[cur_idx].type == 200)
									str = questItems[(itemBuffer[cur_idx].index)].name;

								// Display guild ring name and charges
								if (itemBuffer[cur_idx].type == 201)
									//str = questItems[(itemBuffer[cur_idx].index)].name;
									str = (questItems[(itemBuffer[cur_idx].index)].name) + " [" + Itos(plyr.ringCharges) + "]";

								// Indicate items worn or in use as primary or secondary weapons
								if (cur_idx == plyr.priWeapon)
									BText(4, (page_item + 2), "*");
								if (cur_idx == plyr.secWeapon)
									BText(4, (page_item + 2), "*");
								if (cur_idx == plyr.headArmour)
									BText(4, (page_item + 2), "*");
								if (cur_idx == plyr.bodyArmour)
									BText(4, (page_item + 2), "*");
								if (cur_idx == plyr.armsArmour)
									BText(4, (page_item + 2), "*");
								if (cur_idx == plyr.legsArmour)
									BText(4, (page_item + 2), "*");
								if (cur_idx == plyr.clothing[0])
									BText(4, (page_item + 2), "*");
								if (cur_idx == plyr.clothing[1])
									BText(4, (page_item + 2), "*");
								if (cur_idx == plyr.clothing[2])
									BText(4, (page_item + 2), "*");
								if (cur_idx == plyr.clothing[3])
									BText(4, (page_item + 2), "*");



								BText(9, (page_item + 2), str);
								  switch (page_item)
								  {
										 case 1:
											  menuitem1 = cur_idx;
											  break;
										 case 2:
											  menuitem2 = cur_idx;
											  break;
										 case 3:
											  menuitem3 = cur_idx;
											  break;
										 case 4:
											  menuitem4 = cur_idx;
											  break;
								  }
							page_item++;
							}
							cur_idx++;
						}
						UpdateDisplay();

						string key_value;
						key_value = GetSingleKey();
						if ((key_value == "1") && (menuitem1 != 9999))
						{
							itemRef = menuitem1;
							keypressed = true;
							selectDone = true;
						}
						if ((key_value == "2") && (menuitem2 != 9999))
						{
							itemRef = menuitem2;
							keypressed = true;
							selectDone = true;
						}
						if ((key_value == "3") && (menuitem3 != 9999))
						{
							itemRef = menuitem3;
							keypressed = true;
							selectDone = true;
						}
						if ((key_value == "4") && (menuitem4 != 9999))
						{
							itemRef = menuitem4;
							keypressed = true;
							selectDone = true;
						}

						if (key_value == "ESC")
						{
							keypressed = true;
							selectDone = true;
						}
						if ((key_value == "B") && (selectItemMode == 1) && (page == 3))
						{
							keypressed = true;
							page = 0;
						}
						if ((key_value == "B") && (selectItemMode == 2) && (page == 3))
						{
							keypressed = true;
							page = 2;
						}
						if ((key_value == "B") && (selectItemMode == 3) && (page == 3))
						{
							keypressed = true;
							page = 2;
						}
						if ((key_value == "B") && (page > 3))
						{
							keypressed = true;
							page--;
						}
						if ((key_value == "up") && (page > 3))
						{
							keypressed = true;
							page--;
						}
						if ((key_value == "F") && (pages > (page-2)))
						{
							keypressed = true;
							page++;
						} // check required
						if ((key_value == "down") && (pages > (page-2)))
						{
							keypressed = true;
							page++;
						} // check required
					}
				} // page > 0 loop


			} // while use not done

		// END - SELECT DONE LOOP

			//BUG
			//if ((itemBuffer[itemRef].type==200) && (itemBuffer[itemRef].index==5)) { return 666; } // Amethyst Rod only
			if ((itemRef != 9999) && (selectItemMode != 3))
				DetermineItemAction(selectItemMode, itemRef); // Pass on mode and index for buffer items only if something selected
			if ((itemRef != 9999) && (selectItemMode == 3))
				return itemRef;
			if ((itemRef == 9999) && (selectItemMode == 3))
				return 9999;

			//MLT: What to return?
			return 9999;
		}

		public static void DetermineItemAction(int selectItemMode, int itemRef)
		{
			if (selectItemMode == 1) // Use
			{
				if (itemRef < 1000)
					UseObject(itemRef);
				if (itemRef == 1000)
					UseFood();
				if (itemRef == 1001)
					UseWater();
				if (itemRef == 1002)
					UseTorch();
				if (itemRef == 1003)
					UseTimepiece();
			}

			if (selectItemMode == 2) // Drop
			{
				if (itemRef < 1000)
					DropObject(itemRef);
				if (itemRef > 999)
					DropVolumeObject(selectItemMode, itemRef);
			}

			if (selectItemMode == 4)
				DepositObject(itemRef);
			if (selectItemMode == 5)
				WithdrawalObject(itemRef);
		}
		public static int InputItemQuantity(int selectItemMode)
		{
			int itemQuantity = 0;

			string str;
			string key;
			string inputText = "";
			int maxNumberSize = 6;
			bool enterKeyNotPressed = true;
			while (enterKeyNotPressed)
			{

				if (plyr.status == 3)
					DrawEncounterView();
				if (plyr.status == 2)
					ClearShopDisplay();
				if ((plyr.status != 3) && (plyr.status != 2))
				{
					DispMain();
					DrawConsoleBackground();
				}

				if (selectItemMode == 2)
					CyText(2, "Drop how many?");
				if (selectItemMode == 3)
					CyText(2, "Offer how many?");
				if (selectItemMode == 4)
					CyText(2, "Deposit how many?");

				str = ">" + inputText + "_";
				BText(10, 5, str);
				BText(10, 9, "Enter amount or press ESC.");
				UpdateDisplay();
				key = GetSingleKey();
				if ((key == "0") || (key == "1") || (key == "2") || (key == "3") || (key == "4") || (key == "5") || (key == "6") || (key == "7") || (key == "8") || (key == "9"))
				{
					int numberLength = inputText.Length;
					if (numberLength < maxNumberSize)
						inputText = inputText + key;
				}
				if (key == "BACKSPACE")
				{
					int numberLength = inputText.Length;
					if (numberLength != 0)
					{
					  int numberLength = inputText.Length;
					  inputText = inputText.Substring(0,(numberLength - 1));
					}
				}
				if (key == "RETURN")
					enterKeyNotPressed = false;
				if (key == "ESC")
				{
					itemQuantity = 0;
					enterKeyNotPressed = false;
				}
			}
			itemQuantity = Convert.ToInt32(inputText);

			return itemQuantity;
		}
		public static void DropVolumeObject(int selectItemMode, int object_ref)
		{
			int itemQuantity = 0;
			int existingItem = 9999;
			itemQuantity = InputItemQuantity(selectItemMode);
			if ((object_ref == 1000) && (plyr.food > 0) && (itemQuantity > 0))
			{
				if (itemQuantity > plyr.food)
					itemQuantity = plyr.food;
				existingItem = CheckForGenericItemsHere(1);
				if (existingItem == 9999)
					CreateGenericItem(1, itemQuantity);
				else
					itemBuffer[existingItem].hp += itemQuantity;
				plyr.food -= itemQuantity;
			}
			if ((object_ref == 1001) && (plyr.water > 0) && (itemQuantity > 0))
			{
				if (itemQuantity > plyr.water)
					itemQuantity = plyr.water;
				existingItem = CheckForGenericItemsHere(2);
				if (existingItem == 9999)
					CreateGenericItem(2, itemQuantity);
				else
					itemBuffer[existingItem].hp += itemQuantity;
				plyr.water -= itemQuantity;
			}
			if ((object_ref == 1002) && (plyr.torches > 0) && (itemQuantity > 0))
			{
				if (itemQuantity > plyr.torches)
					itemQuantity = plyr.torches;
				existingItem = CheckForGenericItemsHere(3);
				if (existingItem == 9999)
					CreateGenericItem(3, itemQuantity);
				else
					itemBuffer[existingItem].hp += itemQuantity;
				plyr.torches -= itemQuantity;
			}
			if ((object_ref == 1003) && (plyr.timepieces > 0) && (itemQuantity > 0))
			{
				if (itemQuantity > plyr.timepieces)
					itemQuantity = plyr.timepieces;
				existingItem = CheckForGenericItemsHere(4);
				if (existingItem == 9999)
					CreateGenericItem(4, itemQuantity);
				else
					itemBuffer[existingItem].hp += itemQuantity;
				plyr.timepieces -= itemQuantity;
			}
			if ((object_ref == 1004) && (plyr.compasses > 0) && (itemQuantity > 0))
			{
				if (itemQuantity > plyr.compasses)
					itemQuantity = plyr.compasses;
				existingItem = CheckForGenericItemsHere(5);
				if (existingItem == 9999)
					CreateGenericItem(5, itemQuantity);
				else
					itemBuffer[existingItem].hp += itemQuantity;
				plyr.compasses -= itemQuantity;
			}
			if ((object_ref == 1005) && (plyr.keys > 0) && (itemQuantity > 0))
			{
				if (itemQuantity > plyr.keys)
					itemQuantity = plyr.keys;
				int existingItem = CheckForGenericItemsHere(6);
				if (existingItem == 9999)
					CreateGenericItem(6, itemQuantity);
				else
					itemBuffer[existingItem].hp += itemQuantity;
				plyr.keys -= itemQuantity;
			}
			if ((object_ref == 1006) && (plyr.crystals > 0) && (itemQuantity > 0))
			{
				if (itemQuantity > plyr.crystals)
					itemQuantity = plyr.crystals;
				existingItem = CheckForGenericItemsHere(7);
				if (existingItem == 9999)
					CreateGenericItem(7, itemQuantity);
				else
					itemBuffer[existingItem].hp += itemQuantity;
				plyr.crystals -= itemQuantity;
			}
			if ((object_ref == 1007) && (plyr.gems > 0) && (itemQuantity > 0))
			{
				if (itemQuantity > plyr.gems)
					itemQuantity = plyr.gems;
				existingItem = CheckForGenericItemsHere(8);
				if (existingItem == 9999)
					CreateGenericItem(8, itemQuantity);
				else
					itemBuffer[existingItem].hp += itemQuantity;
				plyr.gems -= itemQuantity;
			}
			if ((object_ref == 1008) && (plyr.jewels > 0) && (itemQuantity > 0))
			{
				if (itemQuantity > plyr.jewels)
					itemQuantity = plyr.jewels;
				existingItem = CheckForGenericItemsHere(9);
				if (existingItem == 9999)
					CreateGenericItem(9, itemQuantity);
				else
					itemBuffer[existingItem].hp += itemQuantity;
				plyr.jewels -= itemQuantity;
			}
			if ((object_ref == 1009) && (plyr.gold > 0) && (itemQuantity > 0))
			{
				if (itemQuantity > plyr.gold)
					itemQuantity = plyr.gold;
				existingItem = CheckForGenericItemsHere(10);
				if (existingItem == 9999)
					CreateGenericItem(10, itemQuantity);
				else
					itemBuffer[existingItem].hp += itemQuantity;
				plyr.gold -= itemQuantity;
			}
			if ((object_ref == 1010) && (plyr.silver > 0) && (itemQuantity > 0))
			{
				if (itemQuantity > plyr.silver)
					itemQuantity = plyr.silver;
				existingItem = CheckForGenericItemsHere(11);
				if (existingItem == 9999)
					CreateGenericItem(11, itemQuantity);
				else
					itemBuffer[existingItem].hp += itemQuantity;
				plyr.silver -= itemQuantity;
			}
			if ((object_ref == 1011) && (plyr.copper > 0) && (itemQuantity > 0))
			{
				if (itemQuantity > plyr.copper)
					itemQuantity = plyr.copper;
				existingItem = CheckForGenericItemsHere(12);
				if (existingItem == 9999)
					CreateGenericItem(12, itemQuantity);
				else
					itemBuffer[existingItem].hp += itemQuantity;
				plyr.copper -= itemQuantity;
			}

		}
		public static void CheckForItemsHere()
		{
		  // counts number of objects on a map square - equal to 1
		  int no_items = 0;
		  int cur_idx = 0;
		  while (cur_idx < plyr.buffer_index)
		  {
			//    if ((itemBuffer[cur_idx].x == plyr.x) && (itemBuffer[cur_idx].y == plyr.y)
			//    && (itemBuffer[cur_idx].location == 1)) { no_items++; }
			if ((itemBuffer[cur_idx].x == plyr.x) && (itemBuffer[cur_idx].y == plyr.y) && (itemBuffer[cur_idx].level == plyr.map) && (itemBuffer[cur_idx].location == 1))
				no_items++;
			cur_idx++;
		  }

		  if (no_items == 1)
			  plyr.status_text = "There is something here.";
		  if (no_items > 1)
			  plyr.status_text = "There are several things here.";

		}
		public static int CheckForGenericItemsHere(int type)
		{
		  // counts number of objects on a map square - equal to 1
		  int value = 9999; // null return
		  int cur_idx = 0;
		  while (cur_idx < plyr.buffer_index)
		  {
			//    if ((itemBuffer[cur_idx].x == plyr.x) && (itemBuffer[cur_idx].y == plyr.y)
			//    && (itemBuffer[cur_idx].location == 1)) { no_items++; }
			if ((itemBuffer[cur_idx].type == type) && (itemBuffer[cur_idx].x == plyr.x) && (itemBuffer[cur_idx].y == plyr.y) && (itemBuffer[cur_idx].level == plyr.map) && (itemBuffer[cur_idx].location == 1))
				value = cur_idx;
			cur_idx++;

		  }
		  return value;
		}
		public static void UseFood()
		{
			 if (plyr.food > 0)
			 {
				 plyr.hunger -= 16;
				 if (plyr.hunger < 0)
					 plyr.hunger = 0;
				 plyr.food--;
			 }
		}
		public static void UseWater()
		{

			 if (plyr.water > 0)
			 {
				 plyr.thirst -= 15;
				 if (plyr.thirst < 0)
					 plyr.thirst = 0;
				plyr.water--;
			 }

		}
		public static void UseTorch()
		{
			// Header - 8B 24 00 00 02 10
			// Text -   4C 69 74 20 54 6F 72 63 68 00
			// Ammo -   00 FF 00
			// Damage - 13 00 00 00 13 00 00 00 00 00 00 (blunt & fire)
			// 04 01 16 16 82 03

			string str;
			string key_value;
			bool keypressed = false;
			 if (plyr.torches > 0)
			 {

				while (!keypressed)
				{
				DispMain();
				BText(17, 1, "Use as:");
				BText(12, 4, "1 Primary weapon");
				BText(12, 5, "2 Secondary weapon");
				BText(9, 8, "Press number or ESC to exit");
				UpdateDisplay();

				key_value = GetSingleKey();

			// Header - 8B 24 00 00 02 10
			// Text -   4C 69 74 20 54 6F 72 63 68 00
			// Ammo -   00 FF 00
			// Damage - 13 00 00 00 13 00 00 00 00 00 00 (blunt & fire)
			// 04 01 16 16 82 03

				if (key_value == "1")
				{
					plyr.priWeapon = CreateItem(178, 0x0, "Lit Torch", 0x16, 0x16, 0x82, 0x04, 0x01, 0x0, 0x13, 0, 0, 0, 0x13, 0, 0, 0, 0, 0, 0, 0x02, 0, 0xFF, 0, 0x03);
					//createWeapon(71); // create a new lit torch was 11
					itemBuffer[plyr.priWeapon].location = 10; // primary was 11
					plyr.torches--;
					// remove old primary ref if exists
					keypressed = true;
				}
				if (key_value == "2")
				{
					plyr.secWeapon = CreateItem(178, 0x0, "Lit Torch", 0x16, 0x16, 0x82, 0x04, 0x01, 0x0, 0x13, 0, 0, 0, 0x13, 0, 0, 0, 0, 0, 0, 0x02, 0, 0xFF, 0, 0x03);
					itemBuffer[plyr.secWeapon].location = 10; // secondary was 12
					plyr.torches--;
					keypressed = true;
					// remove old secondary ref if exists
				}
				if (key_value == "ESC")
					keypressed = true;
				}
				// use_weapon();
			 } else
			 {
				 while (!keypressed)
				 {
					DispMain();
					CyText(2, "You have none.");
					CyText(9, "( Press a key )");
					UpdateDisplay();
					key_value = GetSingleKey();
					if (key_value != "")
						keypressed = true;
				 }
		 }

		}
		public static void UseTimepiece()
		{
			string key;
			string hourtext = "th";
			if ((plyr.hours == 1) || (plyr.hours == 21))
				hourtext = "st";
			if ((plyr.hours == 2) || (plyr.hours == 22))
				hourtext = "nd";
			if ((plyr.hours == 3) || (plyr.hours == 23))
				hourtext = "rd";
			ostringstream myStream = new ostringstream();
			myStream << "It is " << plyr.minutes << " minutes@past the " << plyr.hours << hourtext << " hour.@@@@(Press SPACE to continue)";
			string str = myStream.str();

			 if (plyr.timepieces > 0)
			 {
				key = "";
				while (key != "SPACE")
				{
					DispMain();
					CText(str);
					UpdateDisplay();
					key = GetSingleKey();
				}

			 } else
			 {
				key = "";
				while (key != "SPACE")
				{
					DispMain();
					CText("You have none.");
					UpdateDisplay();
					key = GetSingleKey();
				}

		 }


		}
		public static void UsePotion(int object_ref)
		{
			// Need to add POOF!
			// Need to add option of identifying potion in response to SIP

			string str;
			string key_value;
			bool keypressed = false;
			int potionType = itemBuffer[object_ref].index;

			while (!keypressed)
			{
				if (plyr.status == 3)
					DrawEncounterView();
				else
					DispMain();

				CyText(1, "POTION");
				//bText (12, 3, "Do you want to:");
				BText(16, 3, "(1) Taste");
				BText(16, 4, "(2) Sip");
				BText(16, 5, "(3) Examine");
				BText(16, 6, "(4) Quaff");
				CyText(8, "Press number or ESC to exit");
				UpdateDisplay();

					key_value = GetSingleKey();
					if (key_value == "1")
					{
						string str = "The potion tastes " + Potions[potionType].taste + ".";
						ItemMessage(str);
					}
					if (key_value == "2")
					{
						string str = "You take a sip of the potion,@and feel it is " + Potions[potionType].sip + ".";
						if (Potions[potionType].sip == "caution")
							str = "You take a sip of the potion,@and feel that you@should show caution.";
						ItemMessage(str);
					}
					if (key_value == "3")
					{
						string str = "You open the potion and@see a potion that is " + Potions[potionType].color + "@in colour.";
						ItemMessage(str);
					}
					if (key_value == "4")
					{
						QuaffPotion(object_ref);
						keypressed = true;
					}
					if (key_value == "ESC")
						keypressed = true;
			}
		}
		public static void QuaffPotion(int object_ref)
		{
			string str;
			string key_value;
			bool keypressed = false;
			int potionType = itemBuffer[object_ref].index;
			string potionName = Potions[potionType].name;

			str = "You drink a@@" + potionName;
			ItemMessage(str);

			// Implement potion effect
			if (potionType == 0)
			{
				plyr.thirst -= 2;
				if (plyr.thirst < 0)
					plyr.thirst = 0;
			}
			if (potionType == 1)
				plyr.alcohol += 3;
			if (potionType == 2)
				plyr.alcohol += 4;
			if (potionType == 3)
			{
				plyr.thirst -= 3;
				if (plyr.thirst < 0)
					plyr.thirst = 0;
			}
			if (potionType == 4)
			{
				plyr.thirst -= 3;
				if (plyr.thirst < 0)
					plyr.thirst = 0;
			}
			if (potionType == 5)
			{
				plyr.thirst -= 2;
				if (plyr.thirst < 0)
					plyr.thirst = 0;
			}
			if (potionType == 6)
				plyr.thirst += 3;
			if (potionType == 7)
				plyr.invisibility = 1;
			if (potionType == 8)
				plyr.thirst += 2;
			if (potionType == 9)
				plyr.hp -= 5;
			if (potionType == 10)
				plyr.poison[0] = 1;
			if (potionType == 11)
				plyr.poison[1] = 1;
			if (potionType == 12)
				plyr.poison[2] = 1;
			if (potionType == 13)
				plyr.poison[3] = 1;
			if (potionType == 14)
			{
				plyr.hp += 5;
				if (plyr.hp > plyr.maxhp)
					plyr.hp = plyr.maxhp;
			}
			if (potionType == 15)
			{
				plyr.hp += 8;
				if (plyr.hp > plyr.maxhp)
					plyr.hp = plyr.maxhp;
			}
			if (potionType == 16)
			{
				plyr.hp += 11;
				if (plyr.hp > plyr.maxhp)
					plyr.hp = plyr.maxhp;
			}
			if (potionType == 17)
				plyr.hp = plyr.maxhp;
			if (potionType == 18)
			{
				plyr.poison[0] = 0;
				plyr.poison[1] = 0;
				plyr.poison[2] = 0;
				plyr.poison[3] = 0;
			}
			if (potionType == 19)
			{
				plyr.diseases[0] = 0;
				plyr.diseases[1] = 0;
				plyr.diseases[2] = 0;
				plyr.diseases[3] = 0;
			}
			if (potionType == 20)
				plyr.delusion = 1;
			if (potionType == 21)
				plyr.invulnerability[0] += 8;
			if (potionType == 22)
				plyr.invulnerability[1] += 8;
			if (potionType == 23)
				plyr.invulnerability[2] += 8;
			if (potionType == 24)
				plyr.invulnerability[3] += 8;
			if (potionType == 25)
				plyr.invulnerability[4] += 8;
			if (potionType == 26)
				plyr.invulnerability[5] += 8;
			if (potionType == 27)
				plyr.invulnerability[6] += 8;
			if (potionType == 28)
				plyr.invulnerability[7] += 8;
			if (potionType == 29)
				plyr.invulnerability[8] += 8;
			if (potionType == 30)
				plyr.noticeability += 2;
			if (potionType == 31)
				plyr.alcohol += 65;
			if (potionType == 32)
				plyr.str += 1;
			if (potionType == 33)
				plyr.inte += 1;
			if (potionType == 34)
				plyr.chr += 1;
			if (potionType == 35)
				plyr.chr -= 2;
			if (potionType == 36)
				plyr.str -= 2;
			if (potionType == 37)
				plyr.inte -= 2;
			if (potionType == 38)
				plyr.speed -= 2;
			if (potionType == 39)
				plyr.speed -= 2;
			if (potionType == 40)
				plyr.protection1 += 2;
			if (potionType == 41)
				plyr.protection2 += 2;
			if (potionType == 42)
				plyr.treasureFinding += 5;
			if (potionType == 43)
			{
				plyr.noticeability -= 2;
				if (plyr.noticeability < 0)
					plyr.noticeability = 0;
			}

			itemBuffer[object_ref].location = 0; // Move used potion to void
			TidyObjectBuffer();
		}
		public static void UseWeapon(int object_ref)
		{
				string str;
				string key_value;
			bool keypressed = false;

				while (!keypressed)
				{
					if (plyr.status == 3)
						DrawEncounterView();
					else
						DispMain();

					BText(17, 1, "Use as:");
					BText(12, 4, "1 Primary weapon");
					BText(12, 5, "2 Secondary weapon");
					BText(9, 8, "Press number or ESC to exit");
					UpdateDisplay();

					key_value = GetSingleKey();

					if (key_value == "1")
					{
						plyr.priWeapon = object_ref;
						if (object_ref == plyr.secWeapon)
							plyr.secWeapon = 255;
						itemBuffer[plyr.priWeapon].location = 10; // primary was 11
						// remove old primary ref if exists
						keypressed = true;
					}
					if (key_value == "2")
					{
						plyr.secWeapon = object_ref;
						if (object_ref == plyr.priWeapon)
							plyr.priWeapon = 255;
						itemBuffer[plyr.secWeapon].location = 10; // secondary was 12
						plyr.torches--; // ??????
						keypressed = true;
						// remove old secondary ref if exists
					}
					if (key_value == "ESC")
						keypressed = true;
				}
				// use_weapon();


		}
		public static void UseClothing(int object_ref)
		{
			//int clothingRef = itemBuffer[object_ref].index;
			bool allocated = false;
			if (plyr.clothing[0] == 255)
			{
				plyr.clothing[0] = object_ref;
				allocated = true;
			}
			if ((plyr.clothing[1] == 255) && (!allocated))
			{
				plyr.clothing[1] = object_ref;
				allocated = true;
			}
			if ((plyr.clothing[2] == 255) && (!allocated))
			{
				plyr.clothing[2] = object_ref;
				allocated = true;
			}
			if ((plyr.clothing[3] == 255) && (!allocated))
			{
				plyr.clothing[3] = object_ref;
				allocated = true;
			}

							//	  clothing = itemBuffer[object_ref].index;
							//	  str = clothingItems[clothing].name;

			if ((plyr.clothing[3] != 255) && (!allocated))
				SwapClothing(object_ref);

			//if (object_ref == plyr.secWeapon) { plyr.secWeapon = 255; }    // can't have same pri and sec weapon
		}

		public static void UseObject(int object_ref)
		{
			// Determine object type and pass object_ref to appropriate function
			if (itemBuffer[object_ref].type == 178)
				UseWeapon(object_ref);
			if (itemBuffer[object_ref].type == 177)
				UseArmor(object_ref);
			if ((itemBuffer[object_ref].type == 176) && (itemBuffer[object_ref].hp == 0))
				UsePotion(object_ref);
			if ((itemBuffer[object_ref].type == 176) && (itemBuffer[object_ref].hp == 1))
				QuaffPotion(object_ref);
			if (itemBuffer[object_ref].type == 180)
			{
				if (!((plyr.clothing[0] == object_ref) || (plyr.clothing[1] == object_ref) || (plyr.clothing[2] == object_ref) || (plyr.clothing[3] == object_ref)))
					UseClothing(object_ref);
			}
			if (itemBuffer[object_ref].type == 199)
				UseAmmoItem(object_ref);
			if (itemBuffer[object_ref].type == 200)
				UseQuestItem(object_ref);
			if (itemBuffer[object_ref].type == 201)
				UseQuestItem(object_ref);
		}
		public static void DropObject(int object_ref)
		{
			// Turn lit torch to stick when dropped
			itemBuffer[object_ref].location = 1;
			itemBuffer[object_ref].x = plyr.x;
			itemBuffer[object_ref].y = plyr.y;
			itemBuffer[object_ref].level = plyr.map;
			if (plyr.headArmour == object_ref)
				plyr.headArmour = 255;
			if (plyr.bodyArmour == object_ref)
				plyr.bodyArmour = 255;
			if (plyr.armsArmour == object_ref)
				plyr.armsArmour = 255;
			if (plyr.legsArmour == object_ref)
				plyr.legsArmour = 255;
			if (plyr.priWeapon == object_ref)
				plyr.priWeapon = 0; // Set bufferItem[0] - bare hand
			if (plyr.secWeapon == object_ref)
				plyr.secWeapon = 0; // Set bufferItem[0] - bare hand
			if (plyr.clothing[0] == object_ref)
				plyr.clothing[0] = 255;
			if (plyr.clothing[1] == object_ref)
				plyr.clothing[1] = 255;
			if (plyr.clothing[2] == object_ref)
				plyr.clothing[2] = 255;
			if (plyr.clothing[3] == object_ref)
				plyr.clothing[3] = 255;
		}

		public static void ItemMessage(string message)
		{
			string str; // for message text
			string key;
			bool keynotpressed = true;
			while (keynotpressed)
			{
				if (plyr.status == 3)
					DrawEncounterView();
				if (plyr.status == 1)
					DispMain();

				CText(message);
				CyText(8, "( Press SPACE to continue )");
				UpdateDisplay();
				key = GetSingleKey();
				if (key != "")
					keynotpressed = false;
			}
		}
		//void depositObject(int object_ref);

		public static void DepositObject(int itemRef)
		{
			int itemQuantity = InputDepositQuantity(itemRef);
			if (itemQuantity > 0)
			{
				if ((itemRef == 1000) && (plyr.food > 0))
				{
					if (itemQuantity > plyr.food)
						itemQuantity = plyr.food;
					plyr.food -= itemQuantity;
					plyr.lfood += itemQuantity;
				}
				if ((itemRef == 1001) && (plyr.water > 0))
				{
					if (itemQuantity > plyr.water)
						itemQuantity = plyr.water;
					plyr.water -= itemQuantity;
					plyr.lwater += itemQuantity;
				}
				if ((itemRef == 1002) && (plyr.torches > 0))
				{
					if (itemQuantity > plyr.torches)
						itemQuantity = plyr.torches;
					plyr.torches -= itemQuantity;
					plyr.ltorches += itemQuantity;
				}
				if ((itemRef == 1003) && (plyr.timepieces > 0))
				{
					if (itemQuantity > plyr.timepieces)
						itemQuantity = plyr.timepieces;
					plyr.timepieces -= itemQuantity;
					plyr.ltimepieces += itemQuantity;
				}
				if ((itemRef == 1004) && (plyr.compasses > 0))
				{
					if (itemQuantity > plyr.compasses)
						itemQuantity = plyr.compasses;
					plyr.compasses -= itemQuantity;
					plyr.lcompasses += itemQuantity;
				}
				if ((itemRef == 1005) && (plyr.keys > 0))
				{
					if (itemQuantity > plyr.keys)
						itemQuantity = plyr.keys;
					plyr.keys -= itemQuantity;
					plyr.lkeys += itemQuantity;
				}
				if ((itemRef == 1006) && (plyr.crystals > 0))
				{
					if (itemQuantity > plyr.crystals)
						itemQuantity = plyr.crystals;
					plyr.crystals -= itemQuantity;
					plyr.lcrystals += itemQuantity;
				}
				if ((itemRef == 1007) && (plyr.gems > 0))
				{
					if (itemQuantity > plyr.gems)
						itemQuantity = plyr.gems;
					plyr.gems -= itemQuantity;
					plyr.lgems += itemQuantity;
				}
				if ((itemRef == 1008) && (plyr.jewels > 0))
				{
					if (itemQuantity > plyr.jewels)
						itemQuantity = plyr.jewels;
					plyr.jewels -= itemQuantity;
					plyr.ljewels += itemQuantity;
				}
				if ((itemRef == 1009) && (plyr.gold > 0))
				{
					if (itemQuantity > plyr.gold)
						itemQuantity = plyr.gold;
					plyr.gold -= itemQuantity;
					plyr.lgold += itemQuantity;
				}
				if ((itemRef == 1010) && (plyr.silver > 0))
				{
					if (itemQuantity > plyr.silver)
						itemQuantity = plyr.silver;
					plyr.silver -= itemQuantity;
					plyr.lsilver += itemQuantity;
				}
				if ((itemRef == 1011) && (plyr.copper > 0))
				{
					if (itemQuantity > plyr.copper)
						itemQuantity = plyr.copper;
					plyr.copper -= itemQuantity;
					plyr.lcopper += itemQuantity;
				}

			}
		}
		public static void WithdrawalObject(int itemRef)
		{
			int itemQuantity = InputWithdrawalQuantity(itemRef);
			if (itemQuantity > 0)
			{
				if ((itemRef == 1000) && (plyr.lfood > 0))
				{
					if (itemQuantity > plyr.lfood)
						itemQuantity = plyr.lfood;
					plyr.food += itemQuantity;
					plyr.lfood -= itemQuantity;
				}
				if ((itemRef == 1001) && (plyr.lwater > 0))
				{
					if (itemQuantity > plyr.lwater)
						itemQuantity = plyr.lwater;
					plyr.water += itemQuantity;
					plyr.lwater -= itemQuantity;
				}
				if ((itemRef == 1002) && (plyr.ltorches > 0))
				{
					if (itemQuantity > plyr.ltorches)
						itemQuantity = plyr.ltorches;
					plyr.torches += itemQuantity;
					plyr.ltorches -= itemQuantity;
				}
				if ((itemRef == 1003) && (plyr.ltimepieces > 0))
				{
					if (itemQuantity > plyr.ltimepieces)
						itemQuantity = plyr.ltimepieces;
					plyr.timepieces += itemQuantity;
					plyr.ltimepieces -= itemQuantity;
				}
				if ((itemRef == 1004) && (plyr.lcompasses > 0))
				{
					if (itemQuantity > plyr.lcompasses)
						itemQuantity = plyr.lcompasses;
					plyr.compasses += itemQuantity;
					plyr.lcompasses -= itemQuantity;
				}
				if ((itemRef == 1005) && (plyr.lkeys > 0))
				{
					if (itemQuantity > plyr.lkeys)
						itemQuantity = plyr.lkeys;
					plyr.keys += itemQuantity;
					plyr.lkeys -= itemQuantity;
				}
				if ((itemRef == 1006) && (plyr.lcrystals > 0))
				{
					if (itemQuantity > plyr.lcrystals)
						itemQuantity = plyr.lcrystals;
					plyr.crystals += itemQuantity;
					plyr.lcrystals -= itemQuantity;
				}
				if ((itemRef == 1007) && (plyr.lgems > 0))
				{
					if (itemQuantity > plyr.lgems)
						itemQuantity = plyr.lgems;
					plyr.gems += itemQuantity;
					plyr.lgems -= itemQuantity;
				}
				if ((itemRef == 1008) && (plyr.ljewels > 0))
				{
					if (itemQuantity > plyr.ljewels)
						itemQuantity = plyr.ljewels;
					plyr.jewels += itemQuantity;
					plyr.ljewels -= itemQuantity;
				}
				if ((itemRef == 1009) && (plyr.lgold > 0))
				{
					if (itemQuantity > plyr.lgold)
						itemQuantity = plyr.lgold;
					plyr.gold += itemQuantity;
					plyr.lgold -= itemQuantity;
				}
				if ((itemRef == 1010) && (plyr.lsilver > 0))
				{
					if (itemQuantity > plyr.lsilver)
						itemQuantity = plyr.lsilver;
					plyr.silver += itemQuantity;
					plyr.lsilver -= itemQuantity;
				}
				if ((itemRef == 1011) && (plyr.lcopper > 0))
				{
					if (itemQuantity > plyr.lcopper)
						itemQuantity = plyr.lcopper;
					plyr.copper += itemQuantity;
					plyr.lcopper -= itemQuantity;
				}
			}
		}
		public static int InputDepositQuantity(int itemRef)
		{
			int itemQuantity = 0;

			string str;
			string key;
			string inputText = "";
			int maxNumberSize = 6;
			bool enterKeyNotPressed = true;
			while (enterKeyNotPressed)
			{
			CyText(2, "It costs a silver per unit. How many?");


				ClearGuildDisplay();
				if (itemRef == 1000)
					CyText(2, "Deposit how many Food Packets?");
				if (itemRef == 1001)
					CyText(2, "Deposit how many Water Flasks?");
				if (itemRef == 1002)
					CyText(2, "Deposit how many Unlit Torches?");
				if (itemRef == 1003)
					CyText(2, "Deposit how many Timepieces?");
				if (itemRef == 1004)
					CyText(2, "Deposit how many Compasses?");
				if (itemRef == 1005)
					CyText(2, "Deposit how many Keys?");
				if (itemRef == 1006)
					CyText(2, "Deposit how many Crystals?");
				if (itemRef == 1007)
					CyText(2, "Deposit how many Gems?");
				if (itemRef == 1008)
					CyText(2, "Deposit how many Jewels?");
				if (itemRef == 1009)
					CyText(2, "Deposit how much Gold?");
				if (itemRef == 1010)
					CyText(2, "Deposit how much Silver?");
				if (itemRef == 1011)
					CyText(2, "Deposit how much Copper?");
				if (itemRef == 1012)
					CyText(2, "It costs a silver per unit. How many?");
				if (itemRef == 1000)
					str = "(up to " + Itos(plyr.food) + ")";
				if (itemRef == 1001)
					str = "(up to " + Itos(plyr.water) + ")";
				if (itemRef == 1002)
					str = "(up to " + Itos(plyr.torches) + ")";
				if (itemRef == 1003)
					str = "(up to " + Itos(plyr.timepieces) + ")";
				if (itemRef == 1004)
					str = "(up to " + Itos(plyr.compasses) + ")";
				if (itemRef == 1005)
					str = "(up to " + Itos(plyr.keys) + ")";
				if (itemRef == 1006)
					str = "(up to " + Itos(plyr.crystals) + ")";
				if (itemRef == 1007)
					str = "(up to " + Itos(plyr.gems) + ")";
				if (itemRef == 1008)
					str = "(up to " + Itos(plyr.jewels) + ")";
				if (itemRef == 1009)
					str = "(up to " + Itos(plyr.gold) + ")";
				if (itemRef == 1010)
					str = "(up to " + Itos(plyr.silver) + ")";
				if (itemRef == 1011)
					str = "(up to " + Itos(plyr.copper) + ")";
				if (itemRef == 1012)
					str = "(up to " + Itos(99 - plyr.ringCharges) + ")";
				CyText(3, str);

				str = ">" + inputText + "_";
				BText(10, 5, str);
				BText(10, 9, "Enter amount or press ESC.");
				UpdateDisplay();
				key = GetSingleKey();
				if ((key == "0") || (key == "1") || (key == "2") || (key == "3") || (key == "4") || (key == "5") || (key == "6") || (key == "7") || (key == "8") || (key == "9"))
				{
					int numberLength = inputText.Length;
					if (numberLength < maxNumberSize)
						inputText = inputText + key;
				}
				if (key == "BACKSPACE")
				{
					int numberLength = inputText.Length;
					if (numberLength != 0)
					{
					  int numberLength = inputText.Length;
					  inputText = inputText.Substring(0,(numberLength - 1));
					}
				}
				if (key == "RETURN")
					enterKeyNotPressed = false;
				if (key == "ESC")
				{
					itemQuantity = 0;
					enterKeyNotPressed = false;
				}
			}
			itemQuantity = Convert.ToInt32(inputText);

			return itemQuantity;
		}
		public static int InputWithdrawalQuantity(int itemRef)
		{
			int itemQuantity = 0;

			string str;
			string key;
			string inputText = "";
			int maxNumberSize = 6;
			bool enterKeyNotPressed = true;
			while (enterKeyNotPressed)
			{
				ClearGuildDisplay();
				if (itemRef == 1000)
					CyText(2, "Withdraw how many Food Packets?");
				if (itemRef == 1001)
					CyText(2, "Withdraw how many Water Flasks?");
				if (itemRef == 1002)
					CyText(2, "Withdraw how many Unlit Torches?");
				if (itemRef == 1003)
					CyText(2, "Withdraw how many Timepieces?");
				if (itemRef == 1004)
					CyText(2, "Withdraw how many Compasses?");
				if (itemRef == 1005)
					CyText(2, "Withdraw how many Keys?");
				if (itemRef == 1006)
					CyText(2, "Withdraw how many Crystals?");
				if (itemRef == 1007)
					CyText(2, "Withdraw how many Gems?");
				if (itemRef == 1008)
					CyText(2, "Withdraw how many Jewels?");
				if (itemRef == 1009)
					CyText(2, "Withdraw how much Gold?");
				if (itemRef == 1010)
					CyText(2, "Withdraw how much Silver?");
				if (itemRef == 1011)
					CyText(2, "Withdraw how much Copper?");
				if (itemRef == 1000)
					str = "(up to " + Itos(plyr.lfood) + ")";
				if (itemRef == 1001)
					str = "(up to " + Itos(plyr.lwater) + ")";
				if (itemRef == 1002)
					str = "(up to " + Itos(plyr.ltorches) + ")";
				if (itemRef == 1003)
					str = "(up to " + Itos(plyr.ltimepieces) + ")";
				if (itemRef == 1004)
					str = "(up to " + Itos(plyr.lcompasses) + ")";
				if (itemRef == 1005)
					str = "(up to " + Itos(plyr.lkeys) + ")";
				if (itemRef == 1006)
					str = "(up to " + Itos(plyr.lcrystals) + ")";
				if (itemRef == 1007)
					str = "(up to " + Itos(plyr.lgems) + ")";
				if (itemRef == 1008)
					str = "(up to " + Itos(plyr.ljewels) + ")";
				if (itemRef == 1009)
					str = "(up to " + Itos(plyr.lgold) + ")";
				if (itemRef == 1010)
					str = "(up to " + Itos(plyr.lsilver) + ")";
				if (itemRef == 1011)
					str = "(up to " + Itos(plyr.lcopper) + ")";
				CyText(3, str);

				str = ">" + inputText + "_";
				BText(10, 5, str);
				BText(10, 9, "Enter amount or press ESC.");
				UpdateDisplay();
				key = GetSingleKey();
				if ((key == "0") || (key == "1") || (key == "2") || (key == "3") || (key == "4") || (key == "5") || (key == "6") || (key == "7") || (key == "8") || (key == "9"))
				{
					int numberLength = inputText.Length;
					if (numberLength < maxNumberSize)
						inputText = inputText + key;
				}
				if (key == "BACKSPACE")
				{
					int numberLength = inputText.Length;
					if (numberLength != 0)
					{
					  int numberLength = inputText.Length;
					  inputText = inputText.Substring(0,(numberLength - 1));
					}
				}
				if (key == "RETURN")
					enterKeyNotPressed = false;
				if (key == "ESC")
				{
					itemQuantity = 0;
					enterKeyNotPressed = false;
				}
			}
			itemQuantity = Convert.ToInt32(inputText);

			return itemQuantity;
		}

		public static void UseArmor(int object_ref)
		{
			// The "melee" attribute is used for bodypart in armour items
			if (itemBuffer[object_ref].melee == 0)
				plyr.headArmour = object_ref;
			if (itemBuffer[object_ref].melee == 1)
				plyr.bodyArmour = object_ref;
			if (itemBuffer[object_ref].melee == 2)
				plyr.armsArmour = object_ref;
			if (itemBuffer[object_ref].melee == 3)
				plyr.legsArmour = object_ref;
			Console.Write(itemBuffer[object_ref].melee);
			Console.Write("\n");
			//int armorRef = itemBuffer[object_ref].index;
		//	if (Armor[armorRef].bodyLocation==1) {plyr.headArmour = object_ref;}
		//	if (Armor[armorRef].bodyLocation==2) {plyr.bodyArmour = object_ref;}
		//	if (Armor[armorRef].bodyLocation==3) {plyr.armsArmour = object_ref;}
		//	if (Armor[armorRef].bodyLocation==4) {plyr.legsArmour = object_ref;}

		}
		public static void UseQuestItem(int object_ref)
		{
			if (itemBuffer[object_ref].index == 4)
				DisplayLocation();
		}
		public static void UseAmmoItem(int object_ref)
		{
			// Assume Thunder quarrels for now + leave out ammo type check
			if (itemBuffer[plyr.priWeapon].melee != 0xff)
			{
				itemBuffer[plyr.priWeapon].name = "Crossbow [10]";
				itemBuffer[plyr.priWeapon].ammo = 10;
				itemBuffer[plyr.priWeapon].power = 0x18;
				itemBuffer[object_ref].location = 0; // Destroy the ammo following a reload
			}

		}

		public static void DisplayObjectBuffer()
		{

				string str;
				string key_value;
				bool keypressed = false;
				int oldStatus = plyr.status;
				plyr.status = 255; // Diag screen being displayed

				while (!keypressed)
				{
					ClearDisplay();

					Text(0, 2, "No");
					Text(4, 2, "Typ ");
					Text(8, 2, "Loc");
					Text(12, 2, "X");
					Text(15, 2, "Y");
					Text(18, 2, "L");
					Text(20, 2, "Item");

					str = "Buffer Index " + Itos(plyr.buffer_index) + " of " + Itos(itemBufferSize);
					Text(0, 0, str);

					int cur_idx = 0;
					while (cur_idx < plyr.buffer_index)
					{
						Text(0, (cur_idx + 3), cur_idx);
						Text(4, (cur_idx + 3), itemBuffer[cur_idx].type);
						Text(8, (cur_idx + 3), itemBuffer[cur_idx].location);
						Text(12, (cur_idx + 3), itemBuffer[cur_idx].x);
						Text(15, (cur_idx + 3), itemBuffer[cur_idx].y);
						Text(18, (cur_idx + 3), itemBuffer[cur_idx].level);
						if (itemBuffer[cur_idx].type < 20)
							str = "Volume item";
						//if (itemBuffer[cur_idx].type==178) { str = monsterWeapons[(itemBuffer[cur_idx].index)].name; }
						if (itemBuffer[cur_idx].type == 178)
							str = itemBuffer[cur_idx].name;
						if (itemBuffer[cur_idx].type == 177)
							str = itemBuffer[cur_idx].name;
						if ((itemBuffer[cur_idx].type == 176) && (itemBuffer[cur_idx].hp == 0))
							str = "Potion";
						if ((itemBuffer[cur_idx].type == 176) && (itemBuffer[cur_idx].hp == 1))
							str = Potions[(itemBuffer[cur_idx].index)].name;
						if (itemBuffer[cur_idx].type == 180)
							str = itemBuffer[cur_idx].name;
						if (itemBuffer[cur_idx].type == 199)
							str = itemBuffer[cur_idx].name;
						if (itemBuffer[cur_idx].type == 200)
							str = questItems[(itemBuffer[cur_idx].index)].name;
						if (itemBuffer[cur_idx].type == 201)
							str = questItems[(itemBuffer[cur_idx].index)].name;
							//str = (questItems[(itemBuffer[cur_idx].index)].name) + " [" + itos(plyr.ringCharges) + "]";
						Text(20, (cur_idx + 3), str);

						cur_idx++;
					}
					Text(0, (cur_idx + 4), "(Press SPACE to continue)");
					UpdateDisplay();

					key_value = GetSingleKey();

					if ((key_value == "SPACE") || (key_value == "B"))
						keypressed = true;
				}
				plyr.status = oldStatus;

		}

		public static void TidyObjectBuffer()
		{
			// copy itemBuffer[250] to tempBuffer[250] before starting to reorganise entries
			// Maximum of 250 objects in play at the same time
			// 0 - limbo, 1 - floor, 10 - carried, 11 - primary, 12 - secondary,
			// 13 - worn, 14 - head, 15 - arms, 16 - legs, 17 - body

			int bufferSafetyCheckIndex = (itemBufferSize-3);

			if (plyr.buffer_index > bufferSafetyCheckIndex)
			{
				int idx = plyr.buffer_index; // Number of items in play +1
				int newIndex = 0;
				for (int i = 0 ; i < idx ; i++)
					tempBuffer[i] = itemBuffer[i];

				for (int i = 0 ; i < idx ; i++)
				{
					if (tempBuffer[i].location == 10)
					{
						itemBuffer[newIndex] = tempBuffer[i];
						if (plyr.priWeapon == i)
							plyr.priWeapon = newIndex;
						if (plyr.secWeapon == i)
							plyr.secWeapon = newIndex;
						if (plyr.headArmour == i)
							plyr.headArmour = newIndex;
						if (plyr.bodyArmour == i)
							plyr.bodyArmour = newIndex;
						if (plyr.armsArmour == i)
							plyr.armsArmour = newIndex;
						if (plyr.legsArmour == i)
							plyr.legsArmour = newIndex;
						// worn clothing?
						newIndex++;
					}

				}

				// Reset the buffer index value
				plyr.buffer_index = newIndex;
			}
		}

		public static int ReturnCarriedWeight()
		{
			int carriedWeight = 0;
			int itemWeight = 0;

			int gold = plyr.gold / 16;
			int silver = plyr.silver / 16;
			int copper = plyr.copper / 16;
			int torches = plyr.torches / 16;
			int flasks = plyr.water / 16;
			int food = plyr.food / 16;
			int crystals = plyr.crystals / 16;
			int keys = plyr.keys / 16;
			int gems = plyr.gems / 16;
			int jewels = plyr.jewels / 16;
			int timepieces = plyr.timepieces / 16;
			int compasses = plyr.compasses / 16;
			carriedWeight = gold + silver + copper + torches + flasks + food + crystals + keys + gems + jewels + timepieces + compasses;
			int idx = plyr.buffer_index;
			for (int i = 0 ; i < idx ; i++)
			{
				if (itemBuffer[i].location == 10)
				{
					if (itemBuffer[i].type == 177)
						itemWeight = itemBuffer[i].weight;
					if (itemBuffer[i].type == 178)
						itemWeight = itemBuffer[i].weight;
					if (itemBuffer[i].type == 180)
						itemWeight = itemBuffer[i].weight;
		//carriedWeight += itemWeight;
					itemWeight = 0;
				}
			}
			return carriedWeight;
		}

		//extern buffer_item itemBuffer[itemBufferSize];


		public static BufferItem[] itemBuffer = Arrays.InitializeWithDefaultInstances<BufferItem>(itemBufferSize); // Items on floor,carried and in void - now 250

		public static byte[] dungeonItems = new byte[dungeonItemsSize];

		public static int[] itemOffsets = { 742, 782, 854, 900, 937, 973, 1016, 1056, 1109, 1151, 1221, 1281, 1322, 1400, 1433, 1466, 1504, 1548, 1614, 1650, 1724, 1797, 1866, 1890, 1921, 2027, 2060, 2096, 2133, 2167, 2236, 2273, 2315, 2361, 2404, 2440, 2476, 2510, 2544, 2619, 2693, 2771, 2845, 2888, 2964, 3040, 3116, 3190, 3265, 3341, 3403, 3480, 3555, 3600, 3708, 3783, 3861, 3889, 3916, 3961, 4002, 4059, 4104, 4177, 4269, 4309, 4401, 4473, 4546, 4581, 4620, 4660, 4694, 4747, 4785, 4859, 4890, 4921, 4989, 5056, 5096, 5164, 5196, 5233, 5269, 5302, 5335, 5372, 5409, 5451, 5492, 5530, 5570, 5606, 5633, 5660, 5680, 5765, 5818, 5872, 5898, 5929, 5961, 5985, 6015, 6039, 6067, 6097, 6150, 6204, 6224, 6250, 6275, 6296, 6330, 6404, 6475, 6511, 6567, 6608, 6669, 6710, 6766, 6799, 6839, 6884, 6920, 6941, 6996, 7040, 7093, 7128, 7182, 7203, 7243, 7285, 7326, 7380, 7434, 7477, 7541 };


		public static BufferItem[] tempBuffer = Arrays.InitializeWithDefaultInstances<BufferItem>(itemBufferSize); // Temp buffer for rebuilding new object buffer when bufferIndex reaches 99

		//extern effectItem effectBuffer[50]; // active time limited effects from spells, scrolls, eyes



		public static ClothingItem[] clothingItems =
		{
			new ClothingItem() { name = "Cheap Robe", quality = 0, colour = 0, fabric = 0, type = 0, weight = 4 },
			new ClothingItem() { name = "Green Cap with Feather", quality = 0, colour = 0, fabric = 0, type = 0, weight = 4 },
			new ClothingItem() { name = "Floppy Leather Hat", quality = 0, colour = 0, fabric = 0, type = 0, weight = 4 },
			new ClothingItem() { name = "Leather Sandals", quality = 0, colour = 0, fabric = 0, type = 0, weight = 4 },
			new ClothingItem() { name = "High Leather Boots", quality = 0, colour = 0, fabric = 0, type = 0, weight = 4 },
			new ClothingItem() { name = "Showshoes", quality = 0, colour = 0, fabric = 0, type = 0, weight = 4 },
			new ClothingItem() { name = "White Cotton Robe", quality = 0, colour = 0, fabric = 0, type = 0, weight = 4 },
			new ClothingItem() { name = "White Cotton Tunic", quality = 0, colour = 0, fabric = 0, type = 0, weight = 4 },
			new ClothingItem() { name = "Brown Cotton Breeches", quality = 0, colour = 0, fabric = 0, type = 0, weight = 4 },
			new ClothingItem() { name = "Green Cotton Skirt", quality = 0, colour = 0, fabric = 0, type = 0, weight = 4 },
			new ClothingItem() { name = "Purple Flowing Cape", quality = 0, colour = 0, fabric = 0, type = 0, weight = 4 },
			new ClothingItem() { name = "Blue Woolen Sweater", quality = 0, colour = 0, fabric = 0, type = 0, weight = 4 },
			new ClothingItem() { name = "Heavy Leather Jacket", quality = 0, colour = 0, fabric = 0, type = 0, weight = 6 },
			new ClothingItem() { name = "Fine Yellow Wool Pants", quality = 283, colour = 0, fabric = 0, type = 0, weight = 4 },
			new ClothingItem() { name = "Simple Violet Jerkin", quality = 196, colour = 0, fabric = 0, type = 0, weight = 4 },
			new ClothingItem() { name = "Simple White Wool Robe", quality = 872, colour = 0, fabric = 0, type = 0, weight = 4 },
			new ClothingItem() { name = "Simple Red Jerkin", quality = 161, colour = 0, fabric = 0, type = 0, weight = 4 },
			new ClothingItem() { name = "Simple Purple Fur-Lined Toga", quality = 457, colour = 0, fabric = 0, type = 0, weight = 4 },
			new ClothingItem() { name = "Simple Gold Jerkin", quality = 327, colour = 0, fabric = 0, type = 0, weight = 4 },
			new ClothingItem() { name = "Cheap Gray Silk Hat", quality = 248, colour = 0, fabric = 0, type = 0, weight = 4 },
			new ClothingItem() { name = "Cheap Gold Wool Toga", quality = 139, colour = 0, fabric = 0, type = 0, weight = 4 },
			new ClothingItem() { name = "Fine Silver Jerkin", quality = 357, colour = 0, fabric = 0, type = 0, weight = 4 },
			new ClothingItem() { name = "Simple Gray Cloak", quality = 170, colour = 0, fabric = 0, type = 0, weight = 4 },
			new ClothingItem() { name = "Simple White Fur-Lined Robe", quality = 4260, colour = 0, fabric = 0, type = 0, weight = 4 },
			new ClothingItem() { name = "Cheap Black Jerkin", quality = 221, colour = 0, fabric = 0, type = 0, weight = 4 },
			new ClothingItem() { name = "Fine Purple Fur-Lined Shirt", quality = 979, colour = 0, fabric = 0, type = 0, weight = 6 },
			new ClothingItem() { name = "Simple Purple Dragonskin Blouse", quality = 2364, colour = 0, fabric = 0, type = 0, weight = 4 },
			new ClothingItem() { name = "Simple Orange Silk Vest", quality = 1107, colour = 0, fabric = 0, type = 0, weight = 4 },
			new ClothingItem() { name = "Cheap Gold Silk Skirt", quality = 924, colour = 0, fabric = 0, type = 0, weight = 6 }
		};



		public static QuestItem[] questItems =
		{
			new QuestItem() { name = "Troll Ring Half", weight = 2 },
			new QuestItem() { name = "Goblin Ring Half", weight = 2 },
			new QuestItem() { name = "Reforged Ring", weight = 2 },
			new QuestItem() { name = "Guild Ring", weight = 2 },
			new QuestItem() { name = "Map Stone", weight = 2 },
			new QuestItem() { name = "Amethyst Rod", weight = 2 },
			new QuestItem() { name = "Staff Piece", weight = 2 }
		};



		public static PotionItem[] Potions =
		{
			new PotionItem() { name = "Potion of Water", color = "clear", taste = "plain", sip = "safe" },
			new PotionItem() { name = "Potion of Wine", color = "red", taste = "dry", sip = "caution" },
			new PotionItem() { name = "Potion of Spirits", color = "amber", taste = "sour", sip = "caution" },
			new PotionItem() { name = "Potion of Milk", color = "white", taste = "alkaline", sip = "safe" },
			new PotionItem() { name = "Potion of Fruit Juice", color = "red", taste = "sweet", sip = "safe" },
			new PotionItem() { name = "Potion of Mineral Water", color = "clear", taste = "dry", sip = "safe" },
			new PotionItem() { name = "Potion of Saltwater", color = "clear", taste = "salty", sip = "caution" },
			new PotionItem() { name = "Potion of Invisibility", color = "clear", taste = "dry", sip = "safe" },
			new PotionItem() { name = "Potion of Vinegar", color = "red", taste = "acidic", sip = "caution" },
			new PotionItem() { name = "Potion of ACID!", color = "clear", taste = "acidic", sip = "dangerous" },
			new PotionItem() { name = "Potion of Weak Poison", color = "silver", taste = "bitter", sip = "dangerous" },
			new PotionItem() { name = "Potion of Poison!", color = "white", taste = "alkaline", sip = "dangerous" },
			new PotionItem() { name = "Potion of Strong Poison!", color = "black", taste = "sour", sip = "dangerous" },
			new PotionItem() { name = "Potion of DEADLY POISON!", color = "red", taste = "sweet", sip = "dangerous" },
			new PotionItem() { name = "Potion of Heal Minor Wounds", color = "green", taste = "sour", sip = "safe" },
			new PotionItem() { name = "Potion of Heal Wounds", color = "yellow", taste = "plain", sip = "safe" },
			new PotionItem() { name = "Potion of Heal Major Wounds", color = "silver", taste = "plain", sip = "safe" },
			new PotionItem() { name = "Potion of Heal All Wounds", color = "white", taste = "salty", sip = "safe" },
			new PotionItem() { name = "Potion of Curing Poison", color = "black", taste = "bitter", sip = "caution" },
			new PotionItem() { name = "Potion of Cleansing", color = "black", taste = "bitter", sip = "caution" },
			new PotionItem() { name = "Potion of Delusion", color = "black", taste = "bitter", sip = "caution" },
			new PotionItem() { name = "Potion of Invulnerability Blunt", color = "black", taste = "sweet", sip = "safe" },
			new PotionItem() { name = "Potion of Invulnerability Sharp", color = "black", taste = "plain", sip = "safe" },
			new PotionItem() { name = "Potion of Invulnerability Earth", color = "black", taste = "sour", sip = "safe" },
			new PotionItem() { name = "Potion of Invulnerability Air", color = "black", taste = "salty", sip = "safe" },
			new PotionItem() { name = "Potion of Invulnerability Fire", color = "black", taste = "acidic", sip = "safe" },
			new PotionItem() { name = "Potion of Invulnerability Water", color = "black", taste = "alkaline", sip = "safe" },
			new PotionItem() { name = "Potion of Invulnerability Power", color = "black", taste = "dry", sip = "safe" },
			new PotionItem() { name = "Potion of Invulnerability Mental", color = "black", taste = "plain", sip = "safe" },
			new PotionItem() { name = "Potion of Invulnerability Cleric", color = "black", taste = "sweet", sip = "safe" },
			new PotionItem() { name = "Potion of Noticeability", color = "yellow", taste = "bitter", sip = "dangerous" },
			new PotionItem() { name = "Potion of Inebriation", color = "orange", taste = "plain", sip = "caution" },
			new PotionItem() { name = "Potion of Strength", color = "red", taste = "bitter", sip = "safe" },
			new PotionItem() { name = "Potion of Intelligence", color = "silver", taste = "bitter", sip = "safe" },
			new PotionItem() { name = "Potion of Charisma", color = "silver", taste = "sweet", sip = "safe" },
			new PotionItem() { name = "Potion of Ugliness", color = "green", taste = "sweet", sip = "dangerous" },
			new PotionItem() { name = "Potion of Weakness", color = "yellow", taste = "dry", sip = "dangerous" },
			new PotionItem() { name = "Potion of Dumbness", color = "orange", taste = "sweet", sip = "dangerous" },
			new PotionItem() { name = "Potion of Fleetness", color = "black", taste = "plain", sip = "safe" },
			new PotionItem() { name = "Potion of Slowness", color = "white", taste = "bitter", sip = "dangerous" },
			new PotionItem() { name = "Potion of Protection +1", color = "orange", taste = "sweet", sip = "safe" },
			new PotionItem() { name = "Potion of Protection +2", color = "orange", taste = "sour", sip = "safe" },
			new PotionItem() { name = "Potion of TREASURE FINDING", color = "red", taste = "sweet", sip = "safe" },
			new PotionItem() { name = "Potion of Unnoticeability", color = "clear", taste = "bitter", sip = "safe" }
		};

	}
}