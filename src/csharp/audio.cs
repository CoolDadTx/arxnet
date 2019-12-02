using System;
using System.Collections.Generic;
using System.Linq;
namespace P3Net.Arx
{
	public partial class GlobalMembers
	{
		public static sf.SoundBuffer spellSoundBuffer = new sf.SoundBuffer();
		public static sf.Sound spellSound = new sf.Sound();

		public static sf.SoundBuffer dungeonGate1Buffer = new sf.SoundBuffer();
		public static sf.SoundBuffer dungeonGate2Buffer = new sf.SoundBuffer();
		public static sf.Sound dungeonGate1Sound = new sf.Sound();
		public static sf.Sound dungeonGate2Sound = new sf.Sound();

		public static sf.SoundBuffer cityGate1Buffer = new sf.SoundBuffer();
		public static sf.SoundBuffer cityGate2Buffer = new sf.SoundBuffer();
		public static sf.SoundBuffer cityGate3Buffer = new sf.SoundBuffer();
		public static sf.Sound cityGate1Sound = new sf.Sound();
		public static sf.Sound cityGate2Sound = new sf.Sound();
		public static sf.Sound cityGate3Sound = new sf.Sound();

		public static sf.SoundBuffer[] encounterBuffers = Arrays.InitializeWithDefaultInstances<SoundBuffer>(5);
		//sf::Sound encounterSounds[5];
		public static sf.Sound encounterThemeSound = new sf.Sound();

		public static sf.Music shopMusic = new sf.Music();

		public static sf.SoundSource.Status encounterThemeStatus = new sf.SoundSource.Status();

		public static bool musicPlaying = false;





		public static void InitEncounterThemes()
		{
			encounterBuffers[0].loadFromFile("data/audio/cityEncounter2.ogg"); //encounterSounds[0].setBuffer(encounterBuffers[0]);
			encounterBuffers[1].loadFromFile("data/audio/cityEncounter1.ogg"); //encounterSounds[1].setBuffer(encounterBuffers[1]);
			encounterBuffers[2].loadFromFile("data/audio/e1.ogg"); //encounterSounds[2].setBuffer(encounterBuffers[2]);
			encounterBuffers[3].loadFromFile("data/audio/e2.ogg"); //encounterSounds[3].setBuffer(encounterBuffers[3]);
			encounterBuffers[4].loadFromFile("data/audio/e3.ogg"); //encounterSounds[4].setBuffer(encounterBuffers[4]);
		}




		public static void PlayEncounterTheme(int number)
		{
			encounterThemeSound.setBuffer(encounterBuffers[number]);
			//encounterSounds[number].play();
			encounterThemeSound.play();
		}


		public static bool EncounterThemeNotPlaying()
		{
			encounterThemeStatus = encounterThemeSound.getStatus();
			if (encounterThemeStatus == sf.Sound.Stopped)
				return true;
			else
				return false;
		}




		public static void PlayShopMusic(int musicNo)
		{
			if (musicPlaying == false)
			{
				if (musicNo == 1)
					shopMusic.openFromFile("data/audio/trolls.ogg");
				if (musicNo == 2)
					shopMusic.openFromFile("data/audio/goblins.ogg");
				if (musicNo == 3)
					shopMusic.openFromFile("data/audio/chapel.ogg");
				if (musicNo == 4)
					shopMusic.openFromFile("data/audio/B/trolls.ogg");
				if (musicNo == 5)
					shopMusic.openFromFile("data/audio/B/goblins.ogg");
				if (musicNo == 6)
					shopMusic.openFromFile("data/audio/B/chapel.ogg");
				shopMusic.play();
				musicPlaying = true;
			}
		}

		public static void StopShopMusic()
		{
			musicPlaying = false;
			shopMusic.stop();
		}



		public static void InitCityGateSounds()
		{
			cityGate1Buffer.loadFromFile("data/audio/cityGate1.ogg");
			cityGate1Sound.setBuffer(cityGate1Buffer);
			//dungeonGate1Sound.setLoop(true);

			cityGate2Buffer.loadFromFile("data/audio/cityGate3v2.ogg");
			cityGate2Sound.setBuffer(cityGate2Buffer);
			//cityGate2Sound.setLoop(true);

			cityGate3Buffer.loadFromFile("data/audio/cityGate4.ogg");
			cityGate3Sound.setBuffer(cityGate3Buffer);
		}

		public static void InitDungeonGateSounds()
		{
			dungeonGate1Buffer.loadFromFile("data/audio/gate1.wav");
			dungeonGate1Sound.setBuffer(dungeonGate1Buffer);
			dungeonGate1Sound.setLoop(true);

			dungeonGate2Buffer.loadFromFile("data/audio/gate2.wav");
			dungeonGate2Sound.setBuffer(dungeonGate2Buffer);
		}

		public static void PlayDungeonGateSound1()
		{
		   dungeonGate1Sound.play();
		}

		public static void PlayDungeonGateSound2()
		{
		   dungeonGate2Sound.play();
		}

		public static void StopDungeonGateSound1()
		{
		   dungeonGate1Sound.stop();
		}

		public static void StopDungeonGateSound2()
		{
		   dungeonGate2Sound.stop();
		}


		public static void PlayCityGateSound1()
		{
		   cityGate1Sound.play();
		}

		public static void PlayCityGateSound2()
		{
		   cityGate2Sound.play();
		}

		public static void PlayCityGateSound3()
		{
		   cityGate3Sound.play();
		}

		public static void StopCityGateSound1()
		{
		   cityGate1Sound.stop();
		}

		public static void StopCityGateSound2()
		{
		   cityGate2Sound.stop();
		}

		public static void StopCityGateSound3()
		{
		   cityGate3Sound.stop();
		}

		public static void PlaySpellSound()
		{
			spellSoundBuffer.loadFromFile("data/audio/spell.wav");
			spellSound.setBuffer(spellSoundBuffer);
			spellSound.play();
		}

        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //void InitEncounterThemes();
        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //void PlayEncounterTheme(int number);
        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //bool EncounterThemeNotPlaying();

        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //void PlayShopMusic(int musicNo);
        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //void StopShopMusic();

        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //void PlaySpellSound();

        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //void InitCityGateSounds();
        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //void PlayCityGateSound1();
        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //void StopCityGateSound1();
        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //void PlayCityGateSound2();
        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //void StopCityGateSound2();
        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //void PlayCityGateSound3();
        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //void StopCityGateSound3();

        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //void InitDungeonGateSounds();
        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //void PlayDungeonGateSound1();
        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //void StopDungeonGateSound1();
        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //void PlayDungeonGateSound2();
        //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
        //void StopDungeonGateSound2();
    }
}