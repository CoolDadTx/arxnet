#include <GL/glew.h>
#include <string>
#include <fstream>
#include <iostream>
#include <sstream>

#include "3Dview.h"
#include "display.h"
#include "player.h"
#include "level.h"

// Storage for textures
const int numberOfTextures = 68;
const int numberOfBackgrounds = 49; //was 46
GLuint texture[numberOfTextures];
sf::Texture background[numberOfBackgrounds];
string textureNames[numberOfTextures];
string backgroundNames[numberOfBackgrounds];

int filter;                                     // Which Filter To Use
int fogMode[]= { GL_EXP, GL_EXP2, GL_LINEAR };  // Storage For Three Types Of Fog
int fogfilter= 1;                               // Which Fog To Use
float fogColor[4]= {0.0f, 0.0f, 0.0f, 1.0f};    // Fog Color

int depth = 33; // should be 13 was 33
int columns = 25; // should be an odd number 25
int frontwall = 0;
int leftwall = 0;
int rightwall = 0;
int frontheight = 0;
int leftheight = 0;
int rightheight = 0;
int ceiling = 0;
int floorTexture = 0;
int specialwall = 0;
int zone = 0;

void draw3DView()
{
   glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
	App.pushGLStates();
		draw3DBackground();	// Draw SFML 2D item
	App.popGLStates();
	buildLevelView(); // Draw the OpenGL 3D corridor and room view
	App.pushGLStates();
}

void draw3DBackground()
{
	sf::Sprite Background;
	Background.setScale(1.0, 1.0);

		if (graphicMode==0) // Atari 8bit original textures and size
		{
			float scaleX = float(viewWidth) / float(360);
			float scaleY = float(viewHeight) / float(190);
			Background.setScale(scaleX, scaleY);
			Background.setPosition(viewPortX, viewPortY);
		}
		if (graphicMode==1) // New textures and original size
		{
			float scaleX = float(viewWidth) / float(1024);
			float scaleY = float(viewHeight/2) / float(384);
			Background.setScale(scaleX, scaleY);
			Background.setPosition(viewPortX, viewPortY);
		}
		if (graphicMode==2) // New textures and large size
		{
			Background.setPosition(0, 0); // Assumes large 3D view
			float scaleX = float(viewWidth) / float(1024);
			float scaleY = float(viewHeight/2) / float(384);
			Background.setScale(scaleX, scaleY);
			Background.setPosition(viewPortX, (viewPortY));
		}


	//Background.setTexture(background[15]);

	if (plyr.scenario==2) { Background.setTexture(background[44]); }

	if ( plyr.zoneSet==0 )  { Background.setTexture(background[15]); }
	if ( plyr.zoneSet==1 )  { Background.setTexture(background[10]); }
	if ( plyr.zoneSet==2 )  { Background.setTexture(background[8]); }
	//if ( plyr.zoneSet==3 )  { Background.setTexture(background[1]); } default greys in dungeon
	if ( plyr.zoneSet==5 ) { Background.setTexture(background[46]); } // Taurian maze sky override
	if ( plyr.zoneSet==4 )  { Background.setTexture(background[48]); } // KJ Initials Dangerous Area
	//if ( plyr.zoneSet==5 )  { Background.setTexture(background[12]); }
	//if ( plyr.zoneSet==6 )  { Background.setTexture(background[13]); }
	//if ( plyr.zoneSet==7 )  { Background.setTexture(background[14]); }
	//if ( plyr.zoneSet==8 )  { Background.setTexture(background[15]); }
	//if ( plyr.zoneSet==9 )  { Background.setTexture(background[16]); }
	//if ( plyr.zoneSet==10 ) { Background.setTexture(background[14]); }
	if ( plyr.zoneSet==11 ) { Background.setTexture(background[12]); }
	//if ( plyr.zoneSet==12 ) { Background.setTexture(background[13]); } goblins
	//if ( plyr.zoneSet==13 ) { Background.setTexture(background[14]); } trolls
	if ( plyr.zoneSet==14 ) { Background.setTexture(background[15]); }
	//if ( plyr.zoneSet==15 ) { Background.setTexture(background[14]); }
	if ( plyr.zoneSet==16 ) { Background.setTexture(background[16]); }
	//if ( plyr.zoneSet==17 ) { Background.setTexture(background[14]); }
	if ( plyr.zoneSet==18 ) { Background.setTexture(background[17]); }
	//if ( plyr.zoneSet==20 ) { Background.setTexture(background[43]); } // dungeon level 2 brown sky
	if ( plyr.zoneSet==21 ) { Background.setTexture(background[44]); }
	if ( plyr.zoneSet==22 ) { Background.setTexture(background[44]); }
	if ( plyr.zoneSet==23 ) { Background.setTexture(background[45]); }
	if ( plyr.zoneSet==24 ) { Background.setTexture(background[8]); }
	if ( plyr.zoneSet==25 ) { Background.setTexture(background[15]); } // dungeon level 3
	if ( plyr.zoneSet==26 ) { Background.setTexture(background[12]); } // dungeon level 3 - dragon lair
	if ( plyr.zoneSet==27 ) { Background.setTexture(background[16]); } // dungeon level 3 - dragon lair

	if ((plyr.scenario==0) && (plyr.zone==99))
	{

		if (plyr.timeOfDay==1) // night
		{
			if (plyr.facing==1) { Background.setTexture(background[3]); } // w view
			if (plyr.facing==2) { Background.setTexture(background[2]); } // n view
			if (plyr.facing==3) { Background.setTexture(background[0]); } // e view
			if (plyr.facing==4) { Background.setTexture(background[1]); } // s view
		}

		if (plyr.timeOfDay==2) // sunrise1
		{
			if (plyr.facing==1) { Background.setTexture(background[21]); } // w view
			if (plyr.facing==2) { Background.setTexture(background[20]); } // n view
			if (plyr.facing==3) { Background.setTexture(background[18]); } // e view
			if (plyr.facing==4) { Background.setTexture(background[19]); } // s view
		}


		if (plyr.timeOfDay==3) // sunrise2
		{
			if (plyr.facing==1) { Background.setTexture(background[25]); } // w view
			if (plyr.facing==2) { Background.setTexture(background[24]); } // n view
			if (plyr.facing==3) { Background.setTexture(background[22]); } // e view
			if (plyr.facing==4) { Background.setTexture(background[23]); } // s view
		}

		if (plyr.timeOfDay==4) // sunrise3
		{
			if (plyr.facing==1) { Background.setTexture(background[29]); } // w view
			if (plyr.facing==2) { Background.setTexture(background[28]); } // n view
			if (plyr.facing==3) { Background.setTexture(background[26]); } // e view
			if (plyr.facing==4) { Background.setTexture(background[27]); } // s view
		}

		if (plyr.timeOfDay==5) // sunrise3
		{
			if (plyr.facing==1) { Background.setTexture(background[33]); } // w view
			if (plyr.facing==2) { Background.setTexture(background[32]); } // n view
			if (plyr.facing==3) { Background.setTexture(background[30]); } // e view
			if (plyr.facing==4) { Background.setTexture(background[31]); } // s view
		}

		if (plyr.timeOfDay==6) // sunrise3
		{
			if (plyr.facing==1) { Background.setTexture(background[37]); } // w view
			if (plyr.facing==2) { Background.setTexture(background[36]); } // n view
			if (plyr.facing==3) { Background.setTexture(background[34]); } // e view
			if (plyr.facing==4) { Background.setTexture(background[35]); } // s view
		}

		if (plyr.timeOfDay==7) // sunrise3
		{
			if (plyr.facing==1) { Background.setTexture(background[41]); } // w view
			if (plyr.facing==2) { Background.setTexture(background[41]); } // n view
			if (plyr.facing==3) { Background.setTexture(background[38]); } // e view
			if (plyr.facing==4) { Background.setTexture(background[39]); } // s view
		}

		if (plyr.timeOfDay==0)
		{
			if (plyr.facing==1) { Background.setTexture(background[7]); } // w view
			if (plyr.facing==2) { Background.setTexture(background[6]); } // n view
			if (plyr.facing==3) { Background.setTexture(background[4]); } // e view
			if (plyr.facing==4) { Background.setTexture(background[5]); } // s view
		}
	}
		App.draw(Background);
}



void loadBackgroundNames()
{
	string::size_type idx;
	string filename;
	for (int i=0 ; i<numberOfBackgrounds ; i++) { backgroundNames[i]=""; }

	if (graphicMode==0)
	{
		filename = "data/map/backgrounds.txt";
	}
	else
	{
		filename = "data/map/backgroundsUpdated.txt";
	}
	std::ifstream instream;
	std::string line,text;
	instream.open(filename.c_str());
	if ( !instream )
	{
      //cerr << "Error: background names file could not be loaded" << endl;
	}
	for (int i=0 ; i<numberOfBackgrounds ; i++)
	{
		getline(instream, line);
		idx = line.find('=');
		text = line.substr(idx+2);
		backgroundNames[i]= text;
		background[i].loadFromFile("data/images/backgrounds/"+text+".png");
	}
	instream.close();
}



void loadTextureNames()
{
	string::size_type idx;
	string filename;
	for (int i=0 ; i<numberOfTextures ; i++) { textureNames[i]=""; }
	if (graphicMode==0)
	{
		filename = "data/map/textures.txt";
	}
	else
	{
		filename = "data/map/texturesUpdated.txt";
	}
	std::ifstream instream;
	std::string line,text;
	instream.open(filename.c_str());
	if ( !instream )
	{
      //cerr << "Error: textureNames file could not be loaded" << endl;
	}
	for (int i=0 ; i<numberOfTextures ; i++)
	{
		getline(instream, line);
		idx = line.find('=');
		text = line.substr(idx+2);
		textureNames[i]= text;
	}
	instream.close();
}




void initTextures()
{
	// Load an OpenGL texture.
    // We could directly use a sf::Image as an OpenGL texture (with its Bind() member function),
    // but here we want more control on it (generate mipmaps, ...) so we create a new one

    sf::Image Image;
	string filename;
    char tempfilename[100];
	glGenTextures(numberOfTextures, &texture[0]); // problem line - don't include in loop. Always 0???
	for ( int i=0; i<numberOfTextures; i++ )
    {
		filename = textureNames[i];
		if (graphicMode==0)
	{
		sprintf(tempfilename,"%s%s.png","data/images/textures_original/",filename.c_str());
	}
	else
	{
		sprintf(tempfilename,"%s%s.png","data/images/textures_alternate/",filename.c_str());
	}
		Image.loadFromFile(tempfilename);
		glBindTexture(GL_TEXTURE_2D, texture[i]);
        gluBuild2DMipmaps(GL_TEXTURE_2D, GL_RGBA, Image.getSize().x, Image.getSize().y, GL_RGBA, GL_UNSIGNED_BYTE, Image.getPixelsPtr());
		glTexParameteri(GL_TEXTURE_2D,GL_TEXTURE_MAG_FILTER,GL_LINEAR);
		glTexParameteri(GL_TEXTURE_2D,GL_TEXTURE_MIN_FILTER,GL_LINEAR_MIPMAP_LINEAR);
        glTexParameteri(GL_TEXTURE_2D,GL_TEXTURE_MAX_ANISOTROPY_EXT, (GLint)8.0f);
	}

	// Need to delete SFML image...

}





void buildLevelView()
{
	glFogi(GL_FOG_MODE, fogMode[fogfilter]);    // Fog Mode
	glFogfv(GL_FOG_COLOR, fogColor);			// Set Fog Color
	glFogf(GL_FOG_DENSITY, 0.2f);				// How Dense Will The Fog Be
	glHint(GL_FOG_HINT, GL_DONT_CARE);          // Fog Hint Value
	glFogf(GL_FOG_START, 1.0f);					// Fog Start Depth
	glFogf(GL_FOG_END, 5.0f);					// Fog End Depth

	// Enable and disable fog based on area and could adjust fog properties here for zones
	if ((plyr.scenario==1) && (graphicMode==2)) glEnable(GL_FOG);  // Enables GL_FOG for the Dungeon
	if ((plyr.scenario==1) && (graphicMode==1)) glEnable(GL_FOG);  // Enables GL_FOG for the Dungeon
	if ((plyr.scenario==1) && (graphicMode==0)) glDisable(GL_FOG);  // Disables GL_FOG for the Dungeon
	if (plyr.scenario==0)  glDisable(GL_FOG); // Disable GL_FOG for City

	// Start with 5 variables - columns, depth, plyr.x, plyr.y, plyr.facing
	// c and d hold current column and current depth value

	// Draw left hand block of quads
	int leftmostColumn = 0;
	int rightmostColumn = ((columns-1)/2);

	for ( int d=0; d<depth; d++ )
	{
		for ( int c=leftmostColumn; c<rightmostColumn; c++ )
		{
			calculateWallPositions(c,d);
		}
	}

	// Draw right hand block of quads
	leftmostColumn = ((columns-1)/2)+1;
	rightmostColumn = columns;

	for ( int d=0; d<depth; d++ )
	{
		for ( int c=rightmostColumn; c>(leftmostColumn-1); c-- ) //  int c=leftmostColumn; c<rightmostColumn; c++
		{
			   calculateWallPositions(c,d);
		}
	}

	// Draw front block of quads
	int c = ((columns-1)/2); // This should be the central column 13 if columns = 25
	for ( int d=0; d<depth; d++ )
	{
		   calculateWallPositions(c,d);
	}
}



void calculateWallPositions(int c,int d)
{
	// Calculates the actual positions within OpenGL space to draw the 3 quads that make up a map cell
	int x = 0;
	int y = 0;
	switch (plyr.facing)
	{
		case 2: // north
			x = (plyr.x - ((columns-1)/2)) + c; // total colums -1 / 2
			y = ((plyr.y - (depth-1)) + d); // actual depth
			break;

		case 1: // west
			x = (plyr.x - (depth-1)) + d;
			y = (plyr.y + ((columns-1)/2)) - c;
			break;

		case 3: // east
			x = (plyr.x + (depth-1)) - d;
			y = (plyr.y -  ((columns-1)/2)) + c;
			break;

		case 4: // south
			x = (plyr.x + ((columns-1)/2)) - c;
			y = ((plyr.y + (depth-1)) - d);
			break;
	}

	if ( (x >= 0) && (x < (plyr.mapWidth)) && (y >= 0) && (y < (plyr.mapHeight)) )  // valid location on map? (64 x 64 in example)
	{
		int ind = getMapIndex( x,y );
//		plyr.current_zone = levelmap[ind].zone;
		transMapIndex (ind);
		frontwall = plyr.front; // front wall texture number
		leftwall = plyr.left;   // left wall texture number
		rightwall = plyr.right; // right wall texture number

		frontheight = plyr.frontheight; // front wall texture number
		leftheight = plyr.leftheight;   // left wall texture number
		rightheight = plyr.rightheight; // right wall texture number

		specialwall = plyr.specialwall; // special used for guild sign etc in City
		float xm = c*2;           // x float value to be added to texture positioning co-ords
		float zm = d*2;           // z float value to be added to texture positioning co-ords
		zm = (zm+plyr.z_offset)-1.0f; //-1.0f;
		// Draw front, left and right walls for current map cell
		drawCellWalls(c, d, xm, zm, frontwall, leftwall, rightwall,frontheight,leftheight,rightheight); // pass wall numbers and x and z mods
	}
}




int getTextureIndex(int x)
{
	int texture_index;

	switch (x)
	{
		case 1:
		case 2:
			texture_index = zones[plyr.zoneSet].arch; // arch image with transparency2 6
			break;
		case 3:
		case 4:
			texture_index = zones[plyr.zoneSet].door; // door1 5
			break;
		case 7:
		case 8:
		case 9:     // bolted door
		case 10:
		case 11:
		case 12:
			texture_index = zones[plyr.zoneSet].door; // barred door1 5
			break;

		case 5:
		case 6:
			texture_index = zones[plyr.zoneSet].wall; // secret door1 5
			break;
		case 13:
		case 14:
			texture_index = zones[plyr.zoneSet].wall; // wall0 4
			break;
		case 27:
			texture_index = 27; // City Shop door
			break;
		case 28:
			texture_index = 28; // City Inn door
			break;
		case 29:
			texture_index = 29; // City Tavern door
			break;
		case 30:
			texture_index = 30; // City Smithy door
			break;
		case 31:
			texture_index = 31; // City Bank door
			break;
		case 32:
			texture_index = 32; // City Guild door
			break;
		case 33:
			texture_index = 33; // City Healer door
			break;
		default:
			texture_index = x; // use the non-zone value assigned to the wall
			break;
	}
    return texture_index;
}


void drawCellWalls(int c, int d, float xm, float zm, int frontwall, int leftwall, int rightwall, int frontheight, int leftheight,int rightheight)
{
    int texture_no = 0;
    int wall_type;
	float depthdistantfar = (-depth*2)+1;
	float depthdistantnear = (-depth*2)+3;

	// Original graphic style for standard height walls?
	if (graphicMode==0)
	{
		frontheight=1;
		leftheight=1;
		rightheight=1;
	}



	// Draw ceiling

	/*
	if ( (plyr.zone == 99) && (graphicMode==1) && (plyr.scenario==0) && (plyr.ceiling==0) )
	{
		texture_no = 53; // city floor texture - change
	}
	else
	{
		texture_no = plyr.ceiling;
	}
	*/

	if ((plyr.zone == 99) && (plyr.map==1)) { texture_no = 61; } // dungeon level 1 ceiling texture
	if ((plyr.zone == 99) && (plyr.map==2)) { texture_no = 36; } // dungeon level 2 ceiling texture
	//if ((plyr.zone == 99) && (plyr.map==3)) { texture_no = 42; } // dungeon level 2 ceiling texture
	if ((plyr.zone == 99) && (plyr.map==4)) { texture_no = 52; } // dungeon level 4 ceiling texture

	if (plyr.zone != 99)
	{
		if (plyr.ceiling == 0) { texture_no = zones[plyr.zoneSet].ceiling; } else { texture_no = plyr.ceiling; }
	}
	//texture_no = 0;
	if (texture_no != 0) // 0 = no ceiling texture
	{
			glBindTexture(GL_TEXTURE_2D, texture[texture_no]);
			glBegin(GL_QUADS);
				glTexCoord2f(0.0f, 0.0f); glVertex3f(-25.0f+xm, 0.5,  depthdistantfar+zm);	// Bottom Left
				glTexCoord2f(1.0f, 0.0f); glVertex3f(-23.0f+xm, 0.5,  depthdistantfar+zm);	// Bottom Right
				glTexCoord2f(1.0f, 1.0f); glVertex3f(-23.0f+xm, 0.5,  depthdistantnear+zm);	// Top Right
				glTexCoord2f(0.0f, 1.0f); glVertex3f(-25.0f+xm, 0.5,  depthdistantnear+zm);	// Top Left
			glEnd();
	}



    // Draw floor
	texture_no = 0;
	if (zones[plyr.zoneSet].floor > 0) texture_no = zones[plyr.zoneSet].floor;
	if (plyr.floorTexture > 0) texture_no = plyr.floorTexture;
	if ((plyr.scenario==0) && (plyr.floorTexture==0) && (graphicMode==0)) texture_no = 0;
	if (plyr.zone != 99)
	{
		if (plyr.floorTexture == 0) { texture_no = zones[plyr.zoneSet].floor; } else { texture_no = plyr.floorTexture; }
	}

	if (texture_no != 0) // 0 = no floor texture
	{
		glBindTexture(GL_TEXTURE_2D, texture[texture_no]);
		glBegin(GL_QUADS);
		glTexCoord2f(0.0f, 0.0f); glVertex3f(-25.0f+xm, -0.5,  depthdistantfar+zm);	// Bottom Left
		glTexCoord2f(1.0f, 0.0f); glVertex3f(-23.0f+xm, -0.5,  depthdistantfar+zm);	// Bottom Right
		glTexCoord2f(1.0f, 1.0f); glVertex3f(-23.0f+xm,  -0.5,  depthdistantnear+zm);	// Top Right
		glTexCoord2f(0.0f, 1.0f); glVertex3f(-25.0f+xm,  -0.5,  depthdistantnear+zm);	// Top Left
		glEnd();
	}


	int midcol = ((columns-1)/2);


	// left wall

    if ((!(leftwall<1)) && (c <= midcol))
    {
        wall_type = leftwall;
        if ( ( wall_type == 1 ) || ( wall_type == 2) )
        {
			glEnable(GL_BLEND);									// Enable Blending
			//glDisable(GL_DEPTH_TEST);							// Disable Depth Testing
			glBlendFunc( GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA );
		}
			texture_no = getTextureIndex(wall_type);
			glBindTexture(GL_TEXTURE_2D, texture[texture_no]);
			glBegin(GL_QUADS);		                // begin drawing walls
			glTexCoord2f(0.0f, 1.0f); glVertex3f(-25.0f+xm, -0.5, depthdistantnear+zm);	// Bottom Left
			glTexCoord2f(1.0f, 1.0f); glVertex3f(-25.0f+xm, -0.5, depthdistantfar+zm);	// Bottom Right

            //MLT: Fix double to float conversion
			glTexCoord2f(1.0f, 0.0f); glVertex3f(-25.0f+xm,  -0.5F+leftheight, depthdistantfar+zm);	// Top Right
			glTexCoord2f(0.0f, 0.0f); glVertex3f(-25.0f+xm,  -0.5F+leftheight, depthdistantnear+zm);	// Top Left
			glEnd();
		 if (( ( wall_type == 1 ) || ( wall_type == 2))) // was 1
		 {
			glEnable(GL_DEPTH_TEST);							// Enable Depth Testing
			glDisable(GL_BLEND);
		 }
}

    if ((!(rightwall<1)) && (c >= midcol))
    {
        wall_type = rightwall;
        if (( wall_type == 1 ) || ( wall_type == 2))
		{
			glEnable(GL_BLEND);									// Enable Blending
			glDisable(GL_DEPTH_TEST);							// Disable Depth Testing
			glBlendFunc( GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA );
		}
	   texture_no = getTextureIndex(wall_type);
		glBindTexture(GL_TEXTURE_2D, texture[texture_no]);
		glBegin(GL_QUADS);		                // begin drawing walls
		glTexCoord2f(0.0f, 1.0f); glVertex3f(-23.0f+xm, -0.5, depthdistantfar+zm);	// Bottom Left
		glTexCoord2f(1.0f, 1.0f); glVertex3f(-23.0f+xm, -0.5, depthdistantnear+zm);	// Bottom Right

        //MLT: Fix double to float conversion
		glTexCoord2f(1.0f, 0.0f); glVertex3f(-23.0f+xm,  -0.5F+rightheight, depthdistantnear+zm); // Top Right
		glTexCoord2f(0.0f, 0.0f); glVertex3f(-23.0f+xm,  -0.5F+rightheight, depthdistantfar+zm);	// Top Left
		glEnd();
		if (( wall_type == 1 ) || ( wall_type == 2))
		{
			glEnable(GL_DEPTH_TEST);							// Enable Depth Testing
			glDisable(GL_BLEND);
		}


    }


	if (!(frontwall<1)) // Ignore wall type o (clear) and 1 (arch)
	{
		wall_type = frontwall;
		if (( wall_type == 1 ) || ( wall_type == 2))
		{
			glEnable(GL_BLEND);	// Enable Blending
			glDisable(GL_DEPTH_TEST);							// Disable Depth Testing
			glBlendFunc( GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA );
		}
		texture_no = 0;
		if (wall_type==3) { texture_no = checkCityDoors(); }
		if (texture_no==0) { texture_no = getTextureIndex(wall_type); }
		glBindTexture(GL_TEXTURE_2D, texture[texture_no]);
		glBegin(GL_QUADS); // begin drawing walls
		glTexCoord2f(0.0f, 1.0f); glVertex3f(-25.0f+xm, -0.5,  depthdistantfar+zm);	// Bottom Left
		glTexCoord2f(1.0f, 1.0f); glVertex3f(-23.0f+xm, -0.5,  depthdistantfar+zm);	// Bottom Right

        //MLT: Fix double to float conversion
		glTexCoord2f(1.0f, 0.0f); glVertex3f(-23.0f+xm,  -0.5F+frontheight,  depthdistantfar+zm); // Top Right
		glTexCoord2f(0.0f, 0.0f); glVertex3f(-25.0f+xm,  -0.5F+frontheight,  depthdistantfar+zm); // Top Left
		glEnd();
		if (( wall_type == 1 ) || ( wall_type == 2))
		{
			glEnable(GL_DEPTH_TEST);	// Enable Depth Testing
			glDisable(GL_BLEND);
		}
    }


}
