﻿using System;
using System.Collections.Generic;
using System.Linq;
/********************************************
 * globals.h                                *
 * Global enums to make code easier to read *
 ********************************************/


namespace P3Net.Arx
{
	 public enum DisplayOptions
	 {
		AtariSmall,
		AlternateSmall,
		AlternateLarge
	 }


	 public enum GameStates
	 {
		Explore = 1,
		Module = 2,
		Encounter = 3,
		Dead = 4
	 }


	 public enum SelectStates
	 {
		 Use = 1,
		 Drop = 2,
		 Offer = 3,
		 Deposit = 4,
		 Withdrawal = 5
	 }


	 public enum Scenarios
	 {
		 City,
		 Dungeon,
		 Arena,
		 Palace,
		 Wilderness,
		 Revelation,
		 Destiny
	 }

	 public enum Directions
	 {
		 None,
		 West,
		 North,
		 East,
		 South
	 }

	 public enum Modules
	 {
		 VAULT = 0x06,
		 RATHSKELLER = 0x1D,
		 DwarvenSmithy = 21
	 }

	public enum Encounters
	{
		Devourer = 0,
		Thief = 1,
		Assassin = 2,
		Goblin = 3,
		Troll = 4,
		Lich = 5,
		UndeadKnight = 6,
		Guard = 7,
		FbiAgent = 8,
		DarkKnight = 9,
		Champion = 10,
		Healer = 11,
		Knight = 12,
		Pauper = 13,
		Nobleman = 14,
		AlienSentry = 15,
		Robot = 16,
		Novice = 17,
		Apprentice = 18,
		Mage = 19,
		Wizard = 20,
		Acolyte = 21,
		Sage = 22,
		Orc = 23,
		Gnome = 24,
		Dwarf = 25,
		Slime = 26,
		Mold = 27,
		Homunculus = 28,
		Phoenix = 29,
		Sorceress = 30,
		Whirlwind = 31,
		GiantRat = 32,
		SmallDragon = 33,
		Skeleton = 34,
		Zombie = 35,
		Ghoul = 36,
		Ghost = 37,
		Spectre = 38,
		Wraith = 39,
		Vampire = 40,
		GreatBat = 41,
		Hellhound = 42,
		Harpy = 43,
		Gremlin = 44,
		Imp = 45,
		FlameDemon = 46,
		StormDevil = 47,
		GiantWolf = 48,
		Werewolf = 49,
		Warrior = 50,
		Weaponmaster = 51,
		Valkyrie = 52,
		Gladiator = 53,
		Mercenary = 54,
		Doppleganger = 55,
		Adventurer = 56,
		Watersprite = 57,
		Nightstalker = 58,
		Salamander = 59,
		Ronin = 60,
		Serpentman = 61,
		BigSnake = 62,
		GreatNaga = 63,
		Berserker = 64,
		Basilisk = 65,
		GreatWyrm = 66,
		GoblinLord = 67,
		TrollTyrant = 68,
		IceDemon = 69,
		HornedDevil = 70,
		Mugger = 71, // City only from here
		Robber = 72,
		Fighter = 73,
		Swordsman = 74,
		Courier = 75,
		Commoner = 76,
		Merchant = 77,
		Archmage = 78,
		Gnoll = 79,
		Hobbit = 80,
		Giant = 81,
		SmallGreenDragon = 82,
		Noblewoman = 83
	}

	// Items from encounters, found on Dungeon floor or from Dungeon quests
	public enum Items
	{
		TheStarCard = 0,
		TheFoolCard = 1,
		TheHeirophantCard = 2,
		DeathCard = 3,
		AceOfCups = 4,
		TheChariotCard = 5,
		StrengthCard = 6,
		AceOfWands = 7,
		TemperanceCard = 8,
		KingOfWands = 9,
		PageOfCupsCard = 10,
		AceOfPentacles = 11,
		HighPriestessCard = 12,
		ColdWand = 13,
		FireWand = 14,
		ParalysisWand = 15,
		EyeOfVulnerabilty = 16,
		LightWand = 17,
		HealingWand = 18,
		FrostbladeScroll = 19,
		FirebladeScroll = 20,
		CloutScroll = 21,
		RenewScroll = 22,
		RemoveCurseScroll = 23,
		WizardEyeScroll = 24,
		RubyEye = 25,
		EmeraldEye = 26,
		SapphireEye = 27,
		AmberEye = 28,
		WizardsEye = 29,
		HypnoticEye = 30,
		TomeOfKnowledge = 31,
		TomeOfUnderstanding = 32,
		TomeOfLeadership = 33,
		BronzeHorn = 34,
		SilverHorn = 35,
		GoldHorn = 36,
		GoldHornB = 37,
		PotionOfFleetness = 38,
		PotionOfStrength = 39,
		PotionOfIntelligence = 40,
		PotionOfCharisma = 41,
		PotionOfEndurance = 42,
		PotionOfInvBlunt = 43,
		PotionOfInvSharp = 44,
		PotionOfInvEarth = 45,
		PotionOfInvAir = 46,
		PotionOfInvFire = 47,
		PotionOfInvWater = 48,
		PotionOfRegeneration = 49,
		PotionOfInvMental = 50,
		PotionOfInvCold = 51,
		PotionOfFruitJuice = 52,
		PotionOfWizardEye = 53,
		PotionOfDexterity = 54,
		PotionOfInfravision = 55,
		PotionOfCleansing = 56,
		PotionOfAntidote = 57,
		PotionOfRestoration = 58,
		PotionOfHealing = 59,
		PotionOfHemlock = 60,
		PotionOfInebriation = 61,
		CrystalShield = 62,
		ShieldOfGalahad = 63,
		SpikedShield = 64,
		ShieldOfMordred = 65,
		SpiritShield = 66,
		IronwoodBoken = 67,
		IronFan = 68,
		TowerShield = 69,
		Crossbow = 70,
		Quarrels = 71,
		ChaosClub = 72,
		ShortSword = 73,
		HolyHandGrenade = 74,
		Pike = 75,
		Dirk = 76,
		PantherGloves = 77,
		HelmOfLight = 78,
		DragonskinHauberk = 79,
		GoldenGreaves = 80,
		PlateHelm = 81,
		PlateGauntlets = 82,
		PlateLeggings = 83,
		PlateArmor = 84,
		ScaleArmor = 85,
		TruesilverHelm = 86,
		TruesilverCoat = 87,
		TruesilverGuantlets = 88,
		TruesilverLeggings = 89,
		CuirbouilliHelm = 90,
		BronzeBreastplate = 91,
		BronzeBracers = 92,
		WhiteLinenShirt = 93,
		BlackSilkKimono = 94,
		CheapRobe = 95,
		ElvenCloak = 96,
		ElvenBoots = 97,
		CrystalBelt = 98,
		BlueSuedeShoes = 99,
		BlackWoolenBreeches = 100,
		SilverBrocadedBodice = 101,
		RedPlaidKilt = 102,
		GoldSilkPantaloons = 103,
		LeatherJerkin = 104,
		FloppyLeatherHat = 105,
		BlackCottonPartlet = 106,
		SilverSash = 107,
		StealthSuit = 108,
		SilverKey = 109,
		GoblinRingHalf = 110,
		TrollRingHalf = 111,
		StaffPiece = 112,
		PacCard = 113,
		MirroredShield = 114,
		ReforgedRing = 115,
		Bloodstone = 116,
		WingedSandals = 117,
		MorganasTiara = 118,
		CloakOfLevitation = 119,
		CrystalBreastplate = 120,
		JunaisSword = 121,
		Loadstone = 122,
		IronPalmSalve = 123,
		SwordOfTheAdept = 124,
		RazorIce = 125,
		Whetstone = 126,
		SaurianBrandy = 127,
		BluePearlDagger = 128,
		SixPack = 129,
		MelvinsHelm = 130,
		AmethystRod = 131,
		MapStone = 132,
		FlameQuarrels = 133,
		ThunderQuarrels = 134,
		StaffOfAmber = 135,
		RobinsHood = 136,
		GoldenApple = 137,
		GaussRifle = 138,
		SolarSuit = 139,
		BeamWeapon = 140
	}

}