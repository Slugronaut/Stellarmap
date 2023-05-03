#include <lib.h>
#include <rooms.h>
#include "../include.h"

#include <instancerooms.h>
inherit LIB_INSTANCEROOM;

void create() {
    ::create();
    SetClimate("indoors");
    SetAmbientLight(30);
    SetShort("a blank room");
    SetLong("A featureless area.");
}

void init(){
    ::init();
}
