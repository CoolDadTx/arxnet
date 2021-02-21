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

using SFML.Audio;

namespace P3Net.Arx
{
    public static partial class GlobalMembers
    {
        //TODO: Move to Encounter
        public static void InitEncounterThemes ()
        {
            //Lazy load sound
            encounterBuffers[0] = new SoundBuffer("data/audio/cityEncounter2.ogg");
            encounterBuffers[1] = new SoundBuffer("data/audio/cityEncounter1.ogg");
            encounterBuffers[2] = new SoundBuffer("data/audio/e1.ogg");
            encounterBuffers[3] = new SoundBuffer("data/audio/e2.ogg");
            encounterBuffers[4] = new SoundBuffer("data/audio/e3.ogg");
        }

        //TODO: Move to Encounter
        public static void PlayEncounterTheme ( int number )
        {
            encounterThemeSound.SoundBuffer = encounterBuffers[number];
            encounterThemeSound.Play();
        }

        //TODO: Move to Encounter
        public static bool EncounterThemeNotPlaying ()
        {
            encounterThemeStatus = encounterThemeSound.Status;

            return encounterThemeStatus == SoundStatus.Stopped;
        }

        //TODO: Move to Shop
        public static void PlayShopMusic ( int musicNo )
        {
            if (!musicPlaying)
            {
                string filename;
                switch (musicNo)
                {
                    case 1:
                    filename = "data/audio/trolls.ogg";
                    break;
                    case 2:
                    filename = "data/audio/goblins.ogg";
                    break;
                    case 3:
                    filename = "data/audio/chapel.ogg";
                    break;
                    case 4:
                    filename = "data/audio/B/trolls.ogg";
                    break;
                    case 5:
                    filename = "data/audio/B/goblins.ogg";
                    break;
                    case 6:
                    filename = "data/audio/B/chapel.ogg";
                    break;

                    default:
                    throw new InvalidOperationException("Unknown music");
                };

                shopMusic = new Music(filename);
                shopMusic.Play();
                musicPlaying = true;
            }
        }

        //TODO: Move to Shop
        public static void StopShopMusic ()
        {
            musicPlaying = false;
            shopMusic.Stop();
        }

        //TODO: Move to Create Character
        public static void InitCityGateSounds ()
        {
            cityGate1Buffer = new SoundBuffer("data/audio/cityGate1.ogg");
            cityGate1Sound.SoundBuffer = cityGate1Buffer;

            cityGate2Buffer =  new SoundBuffer("data/audio/cityGate3v2.ogg");
            cityGate2Sound.SoundBuffer = cityGate2Buffer;

            cityGate3Buffer = new SoundBuffer("data/audio/cityGate4.ogg");
            cityGate3Sound.SoundBuffer = cityGate3Buffer;
        }

        //TODO: Move to Create Character
        public static void InitDungeonGateSounds ()
        {
            dungeonGate1Buffer = new SoundBuffer("data/audio/gate1.wav");
            dungeonGate1Sound.SoundBuffer = dungeonGate1Buffer;
            dungeonGate1Sound.Loop = true;

            dungeonGate2Buffer = new SoundBuffer("data/audio/gate2.wav");
            dungeonGate2Sound.SoundBuffer = dungeonGate2Buffer;
        }

        //TODO: Move to Create Character
        public static void PlayDungeonGateSound1 ()
        {
            dungeonGate1Sound.Play();
        }

        //TODO: Move to Create Character
        public static void PlayDungeonGateSound2 ()
        {
            dungeonGate2Sound.Play();
        }

        //TODO: Move to Create Character
        public static void StopDungeonGateSound1 ()
        {
            dungeonGate1Sound.Stop();
        }

        //TODO: Move to Create Character
        public static void StopDungeonGateSound2 ()
        {
            dungeonGate2Sound.Stop();
        }

        //TODO: Move to Create Character
        public static void PlayCityGateSound1 ()
        {
            cityGate1Sound.Play();
        }

        //TODO: Move to Create Character
        public static void PlayCityGateSound2 ()
        {
            cityGate2Sound.Play();
        }

        //TODO: Move to Create Character
        public static void PlayCityGateSound3 ()
        {
            cityGate3Sound.Play();
        }

        //TODO: Move to Create Character
        public static void StopCityGateSound1 ()
        {
            cityGate1Sound.Stop();
        }

        //TODO: Move to Create Character
        public static void StopCityGateSound2 ()
        {
            cityGate2Sound.Stop();
        }

        //TODO: Move to Create Character
        public static void StopCityGateSound3 ()
        {
            cityGate3Sound.Stop();
        }

        //TODO: Move to Spell
        public static void PlaySpellSound ()
        {
            spellSoundBuffer = new SoundBuffer("data/audio/spell.wav");
            spellSound.SoundBuffer = spellSoundBuffer;
            spellSound.Play();
        }

        #region Review Data

        public static SoundBuffer spellSoundBuffer;
        public static Sound spellSound = new Sound();

        public static SoundBuffer dungeonGate1Buffer;
        public static SoundBuffer dungeonGate2Buffer;
        public static Sound dungeonGate1Sound = new Sound();
        public static Sound dungeonGate2Sound = new Sound();

        public static SoundBuffer cityGate1Buffer;
        public static SoundBuffer cityGate2Buffer;
        public static SoundBuffer cityGate3Buffer;
        public static Sound cityGate1Sound = new Sound();
        public static Sound cityGate2Sound = new Sound();
        public static Sound cityGate3Sound = new Sound();

        public static SoundBuffer[] encounterBuffers = new SoundBuffer[5];

        public static Sound encounterThemeSound = new Sound();

        public static Music shopMusic;

        public static SoundStatus encounterThemeStatus;

        public static bool musicPlaying = false;

        #endregion                        
    }
}