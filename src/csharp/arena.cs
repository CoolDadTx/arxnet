using System;
using System.Collections.Generic;
using System.Linq;
namespace P3Net.Arx
{
	public partial class GlobalMembers
	{
		public static void ArenaSouthernEntrance()
		{
			int menu = 1; // high level menu
			string str;
			string key;
			plyr.status = 2; // special module

			LoadShopImage(25);

			while (menu > 0)
			{
				while (menu == 1) // main menu
				{
					ClearShopDisplay();
					CyText(1, "You are at the southern entrance");
					CyText(3, " to the Arena of Xebec's Demise.");
					CyText(5, "Dost thou wish to?");
					BText(9, 7, "  (1) Enter the Arena");
					BText(9, 8, "  (0) Leave");
					UpdateDisplay();

					key = GetSingleKey();
					if (key == "0")
						menu = 0;
					if (key == "1")
						menu = 2;
				}

				while (menu == 2) // Enter the Arena attempt
				{
					key = "";
					while (key == "")
					{
						ClearShopDisplay();
						if (plyr.gender == 1)
							str = "Away with you knave!";
						else
							str = "Away with you scullion!";
						CyText(2, str);
						CyText(4, "Only those of the great houses");
						CyText(6, "may enter here.");
						UpdateDisplay();
						key = GetSingleKey();
						if (key != "")
							menu = 0;
					}
				}

			} // end main while loop

			LeaveShop();
		}
		public static void ArenaNorthernEntrance()
		{
			int menu = 1; // high level menu
			string key;
			plyr.status = 2; // special module

			//loadShopImage(25);

			while (menu > 0)
			{
				while (menu == 1) // main menu
				{
					ClearShopDisplay();
					CyText(1, "You are at the northern entrance");
					CyText(3, " to the Arena of Xebec's Demise.");
					CyText(5, "Dost thou wish to?");
					BText(9, 7, "  (1) Enter the Arena");
					BText(9, 8, "  (0) Leave the Arena");
					UpdateDisplay();

					key = GetSingleKey();
					if (key == "0")
					{
						plyr.x = 16;
						plyr.y = 1;
						plyr.z_offset = 1.0;
						plyr.scenario = 0;
						plyr.status = 1; // explore
						menu = 0;
					}

					if (key == "1")
					{
						plyr.x = 16;
						plyr.y = 3;
						plyr.z_offset = 1.0;
						plyr.scenario = 2;
						plyr.status = 1; // explore
						menu = 0;
					}
				}

			} // end main while loop

		}
		public static void ArenaWesternEntrance()
		{
			string key = "";
			while (key != "0")
			{
				ClearShopDisplay();
				CyText(1, "You are at the western entrance");
				CyText(3, "to the Arena. A heavy portcullis");
				CyText(5, "blocks the entrance.");
				CyText(8, "(0) Leave");
				UpdateDisplay();
				key = GetSingleKey();
			}
			LeaveShop();
		}

	}
}