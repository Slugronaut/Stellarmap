#include <lib.h>
#include <vendor_types.h>
#include "../include.h"

///Stellarmap Generator Metacode, do not modify
inherit LIB_BARKEEP;
///StellarmapCode:Inherit


static void create() {
    ::create();
    SetKeyName("barkeep");
    SetId( ({ "barman" }) );
    SetShort("a generic barkeep");
    SetLevel(1);
    SetLong("A nondescript being whose job is to sell food and drink.");
    SetMenuItems(([
                ]));
    SetGender("male");
    SetRace("human");
    SetClass("vitalnpc");
    SetLevel(10);
    SetSkill("bargaining", 40);
    SetSkill("bargaining", 1);
    SetProperty("no bump", 1);
    SetLocalCurrency("credits");
    
    SetLanguage("common");
    AddLanguagePoints("common",100);
    SetDefaultLanguage("common");
}
void init(){
    ::init();
}

varargs int eventReceiveDamage(object agent, int type, int x, int internal, mixed limbs)
	{
	return 0;
	}
