/*    /lib/race.c
 *    from the Dead Souls LPC Library
 *    handles all race specific management
 *    created by Descartes of Borg 950122
 *    Version: @(#) race.c 1.8@(#)
 *    Last modified: 96/11/11
 */
/*
	STELLARMASS PROJECT
	7/2007
	JAMES CLARK
	
	I added some debug messages to GetMaxHealthPoints() and GetMaxStaminaPoints().
	These are being overriden in combat.c (#included in raceappend.c). This should
	help iddentify if these base functions ever get called instead of the overriding ones.
	
	UPDATE: J.C. 12/14/2008
		-I said 'fuck this shit' to moving all the get/set max stuff in
		'raceappendmod.c'. It's just easier to change the orginal code rather
		than try to override it so I've moved all the variables to this file,
		commented out the original getset max functions and replaced them
		with my own
	
	BUGFIX 12/21/2008 J.C.
		- While tracking down a bug that completely froze combat I realized that the error was occuring with this file (maybe).
		During my searching I realized that the calls to AddSave() and GetSave() in the create method were pointless so I removed them.
		Combat works fine now! I have no idea why that had an effect but I'm happy now :D  I also gave default values to the max
		health, magic, and stamnina variables so that they don't get read as undefined if something doesn't call 'SetMax<whatever>()' in time.
	
	UPDATE 1/10/2009 J.C.
		- Due to the start-hp/hp-per-level change to classes HP, ST, and MP now get a bonus of 1/5 the related stat
		score within the related 'get' function instead of using the stat for the starting level.
	
	UPDATE 7/23/2009 J.C.
		- Edited GetLuck() to remove randomness.
		- nevermind, put it back the way it was :O
	
	UPDATE 8/3/2009 J.C.
		- Updated stamina so that it would return 200 more constant points. Needed this so that new
		 players could get around.
*/
#include <lib.h>
#include <daemons.h>
#include <armor_types.h>
#include <damage_types.h>
#include <meal_types.h>
#include <medium.h>
#include <respiration_types.h>
#include "include/race.h"

inherit LIB_BODY;
inherit LIB_GENETICS;
inherit LIB_LANGUAGE;
inherit LIB_TALK;

private string Town, Race, Gender;
private static int Bulk, Respiration;
private static int MaximumHealth = 0;

//STELLARMASS 12/14/2008 J.C.
//NOTE: These will all need to be 'AddSave()'ed in create().
private int sm_MaxHealthPoints = 10;
private int sm_MaxMagicPoints = 10;
private float sm_MaxStaminaPoints = 10;
//END STELLARMASS

int GetRespiration(){
    int resp = RACES_D->GetRaceRespirationType(this_object()->GetRace());
    if(Respiration) return Respiration;
    return resp;
}

int SetRespiration(int i){
    if(i & R_VACUUM){
        SetResistance(ANOXIA, "immune");
    }
    return Respiration = i;
}

varargs int CanBreathe(object what, object where, int dbg){
    object env = room_environment(this_object());
    int medium, restype, roomres; 

    if(this_object()->GetGodMode()){
        if(dbg) debug("CanBreathe("+identify(what)+", "+identify(where)+"): godmode");
        return 1;
    }
    if(!env){
        if(dbg) debug("CanBreathe("+identify(what)+", "+identify(where)+"): noenv");
        return 0;
    }
    medium = env->GetMedium();
    restype = this_object()->GetRespiration();
    roomres = env->GetRespirationType();

    if(restype & roomres){
        if(dbg) debug("CanBreathe("+identify(what)+", "+identify(where)+"): a"+ 1);
        return 1;
    }
    if(roomres && (restype & roomres)){
        if(dbg) debug("CanBreathe("+identify(what)+", "+identify(where)+"): b"+ 1);
        return 1;
    }
    if(restype & R_VACUUM){
        if(dbg) debug("CanBreathe("+identify(what)+", "+identify(where)+"): c"+ 1);
        return 1;
    }
    if(roomres && !(restype & roomres)){
        if(dbg) debug("CanBreathe("+identify(what)+", "+identify(where)+"): d"+ 0);
        return 0;
    }

    if((medium == MEDIUM_AIR || medium == MEDIUM_LAND || 
                medium == MEDIUM_SURFACE) && (restype & R_AIR) ){
        if(dbg) debug("CanBreathe("+identify(what)+", "+identify(where)+"): e"+ 1);
        return 1;
    }

    if((medium == MEDIUM_WATER || medium == MEDIUM_SURFACE)
            && (restype & R_WATER) ){
        if(dbg) debug("CanBreathe("+identify(what)+", "+identify(where)+"): f"+ 1);
        return 1;
    }

    if( medium == MEDIUM_METHANE && (restype & R_METHANE) ){
        if(dbg) debug("CanBreathe("+identify(what)+", "+identify(where)+"): g"+ 1);
        return 1;
    }

    if(dbg) debug("CanBreathe("+identify(what)+", "+identify(where)+"): h"+ 0);
    return 0;
}

// abstract methods
int GetParalyzed();
// end abstract methods

static void create(){
    body::create();
    genetics::create();
    Race = "blob";
    Gender = "neuter";
    Town = "Town";
}

mixed CanDrink(object ob){
    int strength, type;

    if( !ob ) return 0;
    strength = (int)ob->GetStrength();
    type = (int)ob->GetMealType();
    
    //STELLARMASS
    //allowing alcohol and caffine to go above stat level
    if( (type & MEAL_ALCOHOL) && ((strength + GetAlcohol()) > 150) )
        return "That drink is too strong for you right now.";
    if( (type & MEAL_CAFFEINE) && ((strength + GetCaffeine()) > 150) )
        return "That is too much caffeine for you right now.";
    /*if( (type & MEAL_ALCOHOL) && ((strength + GetAlcohol()) >
                GetStatLevel("durability")) )
        return "That drink is too strong for you right now.";
    if( (type & MEAL_CAFFEINE) && ((strength + GetCaffeine()) >
                GetStatLevel("durability")) )
        return "That is too much caffeine for you right now.";
    */
    //END STELLARMASS
    
    //STELLARMASS -raised drink limit to 150
    if( (type & MEAL_DRINK) && ((strength + GetDrink()) > 150) )
        return "You can't drink any more fluids right now.";
    return 1;
}

mixed CanEat(object ob){
	//STELLARMASS - raises eat limit to 150
        if( ((int)ob->GetStrength() + GetFood()) > 150 )
            return "This is more food than you can handle right now.";
        else return 1;
    }

varargs int eventDie(mixed agent){
    int x;

    if( (x = body::eventDie(agent)) != 1 ) return x;
    return 1;
}

mixed eventDrink(object ob){
    int type, strength;

    type = (int)ob->GetMealType();
    strength = (int)ob->GetStrength();
    if( type & MEAL_POISON ) AddPoison(strength);
    if( type & MEAL_DRINK ) AddDrink(strength);
    if( type & MEAL_ALCOHOL ) AddAlcohol(strength);
    if( type & MEAL_CAFFEINE ) AddCaffeine(strength);
    return 1;
}

mixed eventEat(object ob){
    AddFood((int)ob->GetStrength());
    if( (int)ob->GetMealType() & MEAL_POISON )
        AddPoison((int)ob->GetStrength());
    return 1;
}

varargs string SetRace(string race, mixed extra){
    mixed array args = allocate(5);
    mixed array tmp;
    mixed mixt;

    RACES_D->SetCharacterRace(race, args);

    switch(race){
        case "tree" : this_object()->SetBodyComposition("wood");break;
        case "balrog" : this_object()->SetBodyComposition("stone");break;
        case "elemental" : this_object()->SetBodyComposition("stone");break;
        case "golem" : this_object()->SetBodyComposition("clay");break;
        case "plant" : this_object()->SetBodyComposition("vegetation");break;
    }

    if(sizeof(args[4])){
        foreach(mixed key, mixed val in args[4]){
            this_object()->AddSkill(key,atoi(val[1]),atoi(val[0]));
        }
    }

    foreach(tmp in args[0]){
        mixt = copy(args[0]);
        SetResistance(tmp...);
    }
    foreach(tmp in args[1]){
        mixt = copy(args[1]);
        AddStat(tmp...);
    }
    if( stringp(args[2]) ){
        mixt = copy(args[2]);

        if(!ENGLISH_ONLY){
            SetLanguage(args[2], 100, 1);
        }
        else {
            SetLanguage("English", 100, 1);
        }
    }
    if( sizeof(args[3]) == 2 ){
        mixt = copy(args[3]);

        SetLightSensitivity(args[3]...);
    }
    if( extra != 1 ) NewBody(race);
    tmp = this_object()->GetId();
    if(tmp && member_array(race,tmp) == -1){
        tmp += ({ race });
        this_object()->SetId(tmp);
    }
    if( stringp(extra) ) return (Race = extra), race; 
    else return (Race = race);
}



string GetRace(){ return Race; }

string SetGender(string gender){ return (Gender = gender); }

string GetGender(){ return Gender; }

varargs void SetStat(string stat, int level, int classes){
    int healthPoints;

    genetics::SetStat(stat, level, classes);
    switch(stat){
        case "durability":
            eventCompleteHeal(healthPoints = GetMaxHealthPoints());
        eventHealDamage(healthPoints);
        break;
        case "intelligence":
            AddMagicPoints(GetMaxMagicPoints());
        break;
        case "agility":
            AddStaminaPoints(GetMaxStaminaPoints());
        break;
    }
}

//**************************************************************************
//STELLARMASS PROJECT

/*
varargs int GetMaxHealthPoints(string limb){
    int ret = 1;
    if(!limb && MaximumHealth) ret = MaximumHealth;
    else if(!limb && !MaximumHealth){
        ret = ( 50 + (GetStatLevel("durability") * 10) );
    }
    else {
        int x;
        x = GetLimbClass(limb);
        if(!x) x = 5;
        if(MaximumHealth) ret = ( (1 + MaximumHealth) / x );
        else ret = ( (1 + GetStatLevel("durability")/x) * 10 );
    }
    if(ret < 1) ret = 1;
    return ret;
}

int SetMaxHealthPoints(int x){
    if(x) MaximumHealth = x;
    else SetStat("durability", to_int((x-50)/10), GetStatClass("durability"));
    return GetMaxHealthPoints();
}

int GetMaxMagicPoints(){
    return ( 50 + (GetStatLevel("intelligence") * 10) );
}

float GetMaxStaminaPoints(){
    return (50.0 + (GetStatLevel("agility") * 3.0) +
            (GetStatLevel("durability") * 7.0) );
}
*/

varargs int GetMaxHealthPoints(string limb)
	{
	int ret;
	mixed bonus = this_object()->GetLevel() * (this_object()->GetStatLevel("durability")/5);
	//mixed bonus = ceil(to_float(this_object()->GetStatLevel("durability")) / (float)5.0);
	//bonus = this_object()->GetLevel() * to_int(bonus);//(this_object()->GetLevel() * (this_object()->GetStatLevel("durability")/5));
	//write("Yeahhaw!");
	if(!limb)
		{		
		return sm_MaxHealthPoints + bonus;
		}
	else{
		int limbClass = GetLimbClass(limb);
		if(!limbClass)	limbClass = 5;
		ret = ((sm_MaxHealthPoints + bonus)/ limbClass);

		return ret;
		}
	
	return 1;
	}
//STELLARMASS
void SetMaxHealthPoints(int x)
	{	
	sm_MaxHealthPoints = x;
	}
//STELLARMASS
float GetMaxStaminaPoints()
	{
	//200 more ST no for characters
	return 200 + sm_MaxStaminaPoints + (this_object()->GetLevel() * (this_object()->GetStatLevel("speed")/5));
	}
//STELLARMASS
void SetMaxStaminaPoints(int x)
	{	
	sm_MaxStaminaPoints = x;
	}
//STELLARRMASS
int GetMaxMagicPoints()
	{	
	return sm_MaxMagicPoints + (this_object()->GetLevel() * (this_object()->GetStatLevel("intelligence")/5));
	}
//STELLARMASS
void SetMaxMagicPoints(int x)
	{	
	sm_MaxMagicPoints = x;
	}

//END STELLARMASS
//********************************************************************************************************


void NewBody(string race){
    mixed array args = allocate(2);
    mixed array tmp;

    body::NewBody(race);
    if(!race) return;
    RACES_D->SetCharacterLimbs(race, args);
    foreach(tmp in args[0]) AddLimb(tmp...);
    foreach(tmp in args[1]) AddFingers(tmp...);
}

string SetTown(string str){ return (Town = str); }

string GetTown(){ return Town; }

string GetResistance(int type){ return genetics::GetResistance(type); }

int GetLuck(){
    int x;

    x = random(GetStatLevel("luck")) / 20;
    x = ((x > 4) ? 4 : x);
    if( newbiep() ) x += random(7);
    return (x + random(4));
}

int GetMobility(){
    int max = GetMaxCarry();
    int encum, mob;

    if( GetParalyzed() ){
        return 0;
    }
    if( max < 1 ){
        max = 1;
    }
    encum = (GetCarriedMass() * 100)/max;
    encum -= (encum * this_object()->GetStatLevel("agility"))/200;
    mob = 100 - encum;
    if( mob > 100 ){
        mob = 100;
    }
    else if( mob < 1 ){
        mob = 0;
    }
    return mob;
}

int GetCarriedMass(){ return 0; }

int GetMaxCarry(){ 
    int carry_max;
    carry_max = this_object()->GetLivingMaxCarry();
    if(carry_max) return carry_max;
    else return ((2 + this_object()->GetStatLevel("strength")) * 50); 
}

int GetHeartRate(){
    int x, y;
    x = body::GetHeartRate();
    y = GetStatLevel("speed");
    if( y > 80 ) x -= 2;
    else if( y > 60 ) x -= 1;
    else if( y > 40 ) x = x;
    else if( y > 20 ) x += 1;
    else x += 2;
    if( x > 6 ) x = 6;
    else if( x < 1 ) x = 1;
#ifdef FAST_COMBAT
    if(FAST_COMBAT && this_object()->GetInCombat()) return 1;
#endif
    return x;
}

#if 0
int GetHealRate(){
    int x;

    x = body::GetHealRate() + random((GetStatLevel("durability")/40) + 1);
    if( x > 6 ) x = 6;
    return x;
}
#endif

int GetStatLevel(string stat){ return genetics::GetStatLevel(stat); }

int GetAlcohol(){ return body::GetAlcohol(); }

static void heart_beat(){
    body::heart_beat();
    language::heart_beat();
    genetics::heart_beat();
    set_heart_beat(GetHeartRate());
}
