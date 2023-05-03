#include <lib.h>

inherit LIB_STORAGE;


static void create()
	{
	::create();
	SetKeyName("thing");
	SetId(({"thing"}));
	SetShort("a thing");
	SetLong("A thing.");
	}

void init()
	{
	::init();
	}
