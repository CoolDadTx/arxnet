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
using System.Linq;

/* DWARVEN SMITHY.CPP
		 *
		 * TODO:
		 *
		 *  Offer to reforge Goblin / Troll ring
		 *
		 *
		 */
namespace P3Net.Arx
{
    public enum DwarvenSmithyMenus
    {
        MenuLeft,
        MenuMain,
        MenuChooseSmithyItem,
        MenuPreOffer,
        MenuSelectOffer,
        MenuSmithyMakesOffer,
        MenuOfferRefused,
        MenuNoFunds,
        MenuAnythingElse,
        MenuAnythingElse2,
        MenuCustom,
        MenuCustomOrdered,
        MenuBusyForging,
        MenuCustomReady,
        MenuNoHaggle,
        MenuNoNameProvided
    }
}