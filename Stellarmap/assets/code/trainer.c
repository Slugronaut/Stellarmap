#include <lib.h>
#include "../include.h"

inherit  LIB_TRAINER;
void create(){
    ::create();
    SetKeyName("trainer");
    SetId("generic trainer");
    SetShort("a generic trainer");
    SetLong("This is a person whose job it is to teach you things. "+
            "For example, 'ask trainer "+
            "to teach wibbling' would prompt him to begin a wibble learning "+
            "lesson with you, if wibbling is in his skill set and you have "+
            "earned sufficient training points. If you lack training points, then "+
            "do some adventuring and earn a level promotion. You will "+
            "then be awarded training points."); 
    SetGender("male");
    SetRace("human");
    SetClass("vitalnpc");
    SetLevel(10);
    AddTrainingSkills( ({"wibbling"}));
    SetProperty("no bump",1);
    SetNativeLanguage("common");
    AddLanguagePoints("common",100);
    AddLanguagePoints("common",100);
    SetDefaultLanguage("common");
}
void init() {
    ::init();
}
varargs int eventReceiveDamage(object agent, int type, int x, int internal, mixed limbs)
	{
	return 0;
	}

