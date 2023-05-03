#include <lib.h>
#include <vendor_types.h>
#include "../include.h"

///Stellarmap Generator Metacode, do not modify
inherit LIB_VENDOR;
///StellarmapCode:Inherit


static void create() {
    ::create();
    SetKeyName("vendor");
    SetId( ({ "shopkeep" }) );
    SetAdjectives( ({ "generic" }) );
    SetShort("a generic vendor");
    SetLong("A nondescript person whose job it is to sell things.");
    
    SetGender("male");
    SetRace("human");
    SetClass("vitalnpc");
    SetLevel(1);
    SetSkill("bargaining", 20);
    SetProperty("no bump", 1);
    SetLocalCurrency("credits");
    SetStorageRoom("/obj/room");
    SetMaxItems(100);
    SetVendorType(VT_ALL);
    SetAggressive(0);
    
    SetNativeLanguage("common");
    AddLanguagePoints("common",100);
    SetDefaultLanguage("common");
}

void init()
	{
	::init();
	}

varargs int eventReceiveDamage(object agent, int type, int x, int internal, mixed limbs)
	{
	return 0;
	}
