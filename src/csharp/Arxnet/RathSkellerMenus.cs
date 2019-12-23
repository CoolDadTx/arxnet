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

namespace P3Net.Arx
{
    /* RATHSKELLER.CPP
		 *
		 * TODO:
		 * carrying corpse message once corpses implemented, dragon/wyrm corpse gold offer,
		 * enclosed booth???
		 * all music and on screen lyrics
		 *
		 */
    public enum RathSkellerMenus
    {
        MenuLeft,
        MenuMain,
        MenuSeated,
        MenuOrder,
        MenuRound,
        MenuLeaveATip,
        MenuTransact,
        MenuThanks,
        MenuNoFunds,
        MenuNoTippingFunds,
        MenuGetTipValue,
        MenuGetItemChoice,
        MenuRightAway,
        MenuOrderAnythingElse,
        MenuLeavingAlready,
        MenuNpcEnters,
        MenuNpcOpener,
        MenuNpcMeal,
        MenuNpcDrink,
        MenuNpcTransact,
        MenuNpcRumour
    }
}