#include <lib.h>
#include <rooms.h>
#include "../include.h"

inherit LIB_ROOM;

void create() {
    room::create();
    SetAmbientLight(30);
    SetShort("the void");
    SetLong("The void.  Go down to get out.");
    SetExits( ([ "down" : ROOM_START ]) );
    SetNoModify(1);
}
void init(){
    ::init();
}
