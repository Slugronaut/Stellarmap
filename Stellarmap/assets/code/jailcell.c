#include <lib.h>
#include <rooms.h>
#include "../include.h"

inherit LIB_JAILCELL;

void create() {
    ::create();
    SetClimate("indoors");
    SetAmbientLight(33);
    SetShort("Jail Cell");
    SetLong("You are in standard holding cell for the GSP. Surprisingly it\'s not that dingy. Lots of shiny metal and clean, reflective surfaces. You can find out how much time you have left to serve by typing \'sentence\'.");
	SetProperties((["no attack":1,"no steal":1,"no magic":0,"no bump":1,"no paralyze":1,"no teleport":0,"no clear":0,"login":1,"hospital":0,"mechs allowed":0]));
	SetPlayerKill(0);
	SetNoModify(1);
}

void init(){
    ::init();
}
