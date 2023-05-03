#include <lib.h>
#include <rooms.h>
#include "../include.h"

inherit LIB_ROOM;

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
