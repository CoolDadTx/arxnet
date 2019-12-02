using System;
using System.Collections.Generic;
using System.Linq;
namespace P3Net.Arx
{
	public class SpellItem
	{
		public int no { get; set; }
		//string name;
		public int percentage { get; set; }
	}

	public class SpellRecord
	{
		public string name { get; set; }
		public int percentage { get; set; }
		public int cost { get; set; }
		public int effect { get; set; }
		public int negativeValue { get; set; }
		public int positiveValue { get; set; }
		public int duration { get; set; }
		public bool[] guilds { get; set; } = new bool[14];
	}

	public partial class GlobalMembers
	{
		public static void CastSpellMessage(string spellDesc)
		{
			string str; // for message text
			string key;
			bool keynotpressed = true;
			PlaySpellSound();
			while (keynotpressed)
			{
				if (plyr.status == 3)
					DrawEncounterView();
				if (plyr.status == 1)
					DispMain();
				str = "You cast the spell of@@$ " + spellDesc + " $";
				CyText(3, str);
				UpdateDisplay();
				key = GetSingleKey();
				if (key != "")
					keynotpressed = false;
			}
		}
		public static void SpellBackfiredMessage(int spellPoints)
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
				if (plyr.status == 0)
					DispMain();
				str = "The spell failed@@and backfired for " + Itos(spellPoints) + "!";
				CyText(3, str);
				UpdateDisplay();
				key = GetSingleKey();
				if (key != "")
					keynotpressed = false;
			}
			// update ring charges and hp
		}
		public static void CastSpells()
		{
			// Based on SelectItem code using "pages" of spells hence reference to "pages > 2" etc

			int itemRef = 9999; // Nothing selected
			string str;
			string selectDesc;
			selectDesc = "CAST";

			int menuitem1 = 255; // 255 is used here as nil
			int menuitem2 = 255;
			int menuitem3 = 255;
			int menuitem4 = 255;
			bool selectDone = false;

			int no_items = plyr.spellIndex; // Number of spells in players inventory
			int cur_idx = 0;
			int pages = 0;
			int page = 3;
			int page_item = 0;
			pages = 0;

			int noPages = no_items / 4; // based on 4 oncreen items per page
			pages += noPages;
			int tempRemainder = no_items % 4;
			if (tempRemainder != 0)
				pages++;

			while (!selectDone)
			{
				if (page > 2) // Variable items
				{
					bool keypressed = false;
					while (!keypressed)
					{
						if (plyr.status == 3)
							DrawEncounterView();
						if (plyr.status == 1)
							DispMain();
						if (plyr.status == 0)
							DispMain();
						CyText(1, selectDesc);
						BText(5, 3, "(1)");
						BText(5, 4, "(2)");
						BText(5, 5, "(3)");
						BText(5, 6, "(4)");
						BText(2, 8, "Item #, Forward, Back, or ESC to exit");
						SetFontColour(40, 96, 244, 255);
						BText(2, 8, "     #  F        B        ESC");
						SetFontColour(215, 215, 215, 255);

						page_item = 1;
						cur_idx = ((page-3) * 4);
						menuitem1 = 9999; // 9999 is used as nil
						menuitem2 = 9999;
						menuitem3 = 9999;
						menuitem4 = 9999;

						while ((cur_idx < plyr.spellIndex) && (page_item < 5))
						{
							str = spells[(spellBuffer[cur_idx].no)].name + " " + Itos(spellBuffer[cur_idx].percentage) + "%";
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
						}
						if ((key_value == "down") && (pages > (page-2)))
						{
							keypressed = true;
							page++;
						}
					}
				} // page > 0 loop


			} // while cast not done

			if (itemRef != 9999)
				AttemptSpell(itemRef);
		}
		public static void AttemptSpell(int spellRef)
		{
			// Attempt to cast the selected spell from "castSpells()"

			// spellRef = index within spellBuffer foe selected spell
			int spellNo = spellBuffer[spellRef].no;
			int spellPercentage = spellBuffer[spellRef].percentage;
			string spellDesc = spells[spellBuffer[spellRef].no].name;
			int spellPoints = Randn(1, 5);

			int castProb = Randn(0, 100);
			if (castProb < spellPercentage)
			{
				CastSpellMessage(spellDesc);
				plyr.ringCharges -= spellPoints;
				if (plyr.ringCharges < 0)
					plyr.ringCharges = 0;

				// Check for specific spells and their effects
				if (spellNo == 4)
					plyr.food++;
				if (spellNo == 5)
					plyr.keys++;
				if (spellNo == 13)
				{
					plyr.hp += Randn(1, 10);
					if (plyr.hp > plyr.maxhp)
						plyr.hp = plyr.maxhp;
				}
				if (spellNo == 16)
					DisplayLocation();
				if (spellNo == 8) // Dexterity
				{
					effectBuffer[plyr.effectIndex].effect = 1; // Dexterity
					effectBuffer[plyr.effectIndex].negativeValue = 0;
					effectBuffer[plyr.effectIndex].positiveValue = 30; // +30 to plyr.dex
					effectBuffer[plyr.effectIndex].duration = 8; // hours
				}

			} else
			{
				SpellBackfiredMessage(spellPoints);
				plyr.hp -= spellPoints;
				// NEED  NEW METHOD OF CHECKING HP REDUCTION TO CATCH DEATH!
		}

		}

		//extern spellRecord spells[35];
		//extern spellItem spellBuffer[35]; // learnt spells that can be cast


		public static SpellItem[] spellBuffer = Arrays.InitializeWithDefaultInstances<SpellItem>(35); // learnt spells that can be cast

		public static SpellRecord[] spells =
		{
			{ "Bewilder", 50, 42, 10, 10, 0, 1, 1, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 1, 0 },
			{ "Blinding", 50, 42, 10, 10, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ "Charisma", 50, 42, 10, 10, 0, 1, 1, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 1, 0 },
			{ "Cold Blast", 50, 42, 10, 10, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 1, 0, 0, 0, 0 },
			{ "Conjure Food", 50, 42, 10, 10, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0 },
			{ "Conjure Key", 50, 42, 10, 10, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0 },
			{ "Defeat Evil", 50, 42, 10, 10, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0 },
			{ "Defeat Good", 50, 42, 10, 10, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0 },
			{ "Dexterity", 50, 42, 10, 10, 0, 1, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ "Fear", 50, 42, 10, 10, 0, 1, 0, 0, 1, 0, 0, 1, 0, 1, 0, 1, 0, 0, 0, 0 },
			{ "Fireballs", 50, 42, 10, 10, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0 },
			{ "Fireblade", 50, 42, 10, 10, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1 },
			{ "Fury", 50, 42, 10, 10, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
			{ "Healing", 50, 42, 10, 10, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1 },
			{ "Light", 50, 42, 10, 10, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 1 },
			{ "Lightning Bolts", 50, 42, 10, 10, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ "Location", 50, 42, 10, 10, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0 },
			{ "Luck", 50, 42, 10, 10, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0 },
			{ "Magic Darts", 50, 42, 10, 10, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ "Night Vision", 50, 42, 10, 10, 0, 1, 1, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 1, 0 },
			{ "Paralysis", 50, 42, 10, 10, 0, 1, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0 },
			{ "Prism", 50, 42, 10, 10, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ "Protect from Evil", 50, 42, 10, 10, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0 },
			{ "Protect from Good", 50, 42, 10, 10, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0 },
			{ "Protection", 50, 42, 10, 10, 0, 1, 1, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1 },
			{ "Razoredge", 50, 42, 10, 10, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 },
			{ "Repair", 50, 42, 10, 10, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 },
			{ "Shadowmeld", 50, 42, 10, 10, 0, 1, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ "Shield", 50, 42, 10, 10, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
			{ "Slay Evil", 50, 42, 10, 10, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0 },
			{ "Slay Good", 50, 42, 10, 10, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0 },
			{ "Speed", 50, 42, 10, 10, 0, 1, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ "Strength", 50, 42, 10, 10, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 1 },
			{ "Super Vision", 50, 42, 10, 10, 0, 1, 1, 0, 1, 0, 0, 0, 0, 1, 0, 1, 0, 0, 1, 0 },
			{ "Vigor", 50, 42, 10, 10, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 1 }
		};

	}
}