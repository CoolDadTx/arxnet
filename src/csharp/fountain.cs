using System;
using System.Collections.Generic;
using System.Linq;
namespace P3Net.Arx
{
	public partial class GlobalMembers
	{


	// extern Player plyr;


		public static void ShopFountain()
		{

			int fountainMenu = 1; // high level menu
			string str;
			string key;
			plyr.status = 2; // shopping
			LoadShopImage(19);

			if (plyr.map == 1)
			{
				SetAutoMapFlag(plyr.map, 13, 52);
				SetAutoMapFlag(plyr.map, 14, 52);
				SetAutoMapFlag(plyr.map, 15, 52);
				SetAutoMapFlag(plyr.map, 13, 53);
				SetAutoMapFlag(plyr.map, 14, 53);
				SetAutoMapFlag(plyr.map, 15, 53);
				SetAutoMapFlag(plyr.map, 13, 54);
				SetAutoMapFlag(plyr.map, 14, 54);
				SetAutoMapFlag(plyr.map, 15, 54);
			}

			if (plyr.map == 2)
			{
				SetAutoMapFlag(plyr.map, 24, 16);
				SetAutoMapFlag(plyr.map, 25, 16);
				SetAutoMapFlag(plyr.map, 24, 15);
				SetAutoMapFlag(plyr.map, 25, 15);
			}

			if (plyr.map == 3)
			{
				//TODO Add level 3 fountain!
			}

			while (fountainMenu > 0)
			{
				while (fountainMenu == 1) // main menu
				{
					ClearShopDisplay();
					CyText(1, "You're facing a crystal clear fountain.");
					BText(6, 3, "Do you (1) Take a drink or");
					BText(13, 4, "(0) Leave?");
					UpdateDisplay();

					key = GetSingleKey();
					if (key == "0")
						fountainMenu = 0;
					if (key == "down")
						fountainMenu = 0;
					if (key == "1")
						fountainMenu = 2;
				}


				while (fountainMenu == 2) // main menu
				{
					ClearShopDisplay();

					if (plyr.map == 1)
					{
						int strNo = 1;
						if (strNo == 1)
							str = "The water tastes delicious!";
						if (strNo == 2)
							str = "Ahh! The water tastes@@absolutely marvelous!";
						if (strNo == 3)
							str = "The water is cool, clear and@@really hits the spot!";
						if (strNo == 4)
							str = "You feel rejuvenated!";
						if (strNo == 5)
							str = "Ahh! Now you feel much better!";
						plyr.hp = plyr.maxhp;
						plyr.thirst = 0;
						//Increase consumption FULL and too bloated to drink message
					}

					if (plyr.map == 2)
					{
						int strNo = 2;
						if (strNo == 1)
							str = "Your health improves rapidly!";
						if (strNo == 2)
							str = "Every cell in your body seems purified!";
						if (strNo == 3)
							str = "You feel a wave of relief@@sweep over you!";
						//TODO Diseases cleansed!
						//Increase consumption FULL
						plyr.thirst = 0;
					}

					if (plyr.map == 3)
					{
						str = "The water tastes delicious!";
						//TODO Fatigue removed!
						//Increase consumption FULL
						plyr.thirst = 0;
					}
					CyText(3, str);
					UpdateDisplay();

					key = GetSingleKey();
					if (key == "SPACE")
						fountainMenu = 1;
				}

			}

			LeaveShop();
		}


	}
}