using System;
using System.Collections.Generic;
using System.Linq;
namespace P3Net.Arx
{
	public partial class GlobalMembers
	{


	// extern Player plyr;
	// extern sf::RenderWindow App;




		public static void ShopLift()
		{
			int liftMenu = 1; // high level menu
			string str;
			string key;
			plyr.status = 2; // shopping

			//setAutoMapFlag(plyr.map, 58, 3);


			LoadShopImage(22);

			while (liftMenu > 0)
			{
				while (liftMenu == 1) // main menu
				{
					ClearShopDisplay();
					CyText(1, "In the elevator you find three buttons.");
					CyText(3, "Which do you press?");
					BText(11, 5, "(1) The red button");
					BText(11, 6, "(2) The green button");
					BText(11, 7, "(3) The blue button");
					UpdateDisplay();


					key = GetSingleKey();


					if (key == "0")
						liftMenu = 0;
					if (key == "down")
						liftMenu = 0;
				}



			}
			LeaveShop();
		}


	}
}