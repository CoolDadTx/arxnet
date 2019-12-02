using System;
using System.Collections.Generic;
using System.Linq;
namespace P3Net.Arx
{
	public partial class GlobalMembers
	{


	// extern Player plyr;
	// extern sf::RenderWindow App;


		public static void Staircase()
		{
			//int staircaseNo = getstaircaseNo();
			int staircaseMenu = 1; // high level menu
			string str;
			string key;
			plyr.status = 2; //shopping

			bool stairwayUp = true;
			if ((plyr.x == 50) && (plyr.y == 3) && (plyr.map == 0))
				stairwayUp = false;
			if ((plyr.x == 59) && (plyr.y == 62) && (plyr.map == 0))
				stairwayUp = false;
			if ((plyr.x == 49) && (plyr.y == 17) && (plyr.map == 1))
				stairwayUp = false;
			if ((plyr.x == 16) && (plyr.y == 17) && (plyr.map == 1))
				stairwayUp = false;
			if ((plyr.x == 16) && (plyr.y == 48) && (plyr.map == 1))
				stairwayUp = false;
			if ((plyr.x == 48) && (plyr.y == 48) && (plyr.map == 1))
				stairwayUp = false;
			if ((plyr.x == 17) && (plyr.y == 12) && (plyr.map == 2))
				stairwayUp = false;
			// Need to determine if up or down staircase
			//glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
			if (stairwayUp)
				LoadShopImage(6);
			else
				LoadShopImage(7);



			while (staircaseMenu > 0)
			{
				while (staircaseMenu == 1) // main menu
				{
					ClearShopDisplay();
					//drawStatsPanel();

					if (stairwayUp)
						CyText(1, "A stairway leads up, do you take it?");
					else
						CyText(1, "A stairway leads down, do you take it?");
					CyText(3, "( es or  o)");
					SetFontColour(40, 96, 244, 255);
					CyText(3, " Y      N  ");
					SetFontColour(215, 215, 215, 255);
					UpdateDisplay();

					key = GetSingleKey();

					//if ( key=="Y" ) { staircaseMenu = 2; }
					if (key == "N")
					{
						staircaseMenu = 0;
						LeaveShop();
					}
					if (key == "Y")
					{
						;
						MoveMapLevel();
						plyr.status = 1;
						staircaseMenu = 0;
					};
				}

			}

		}

	}
}