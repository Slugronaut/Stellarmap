/*    /lib/living.c
 *    from the Dead Souls Object Library
 *    handles common living code
 *    created by Descartes of Borg 951218
 *    Version: @(#) living.c 1.29@(#)
 *    Last Modified: 96/12/15
 */
/*
	STELLARMASS PROJECT
	6/2008
	JAMES CLARK
	
	Due to the changes in /daemons/classes.c I now can check when
	a player can recieve a skill automatically based on level.
	SetLevel() has been appended to this file and makes a call to
	the same function inherited from "/std/abilities.c"
	
	Their HP, ST, and MP are also based on class and level now.
	
	NOTES:
		-I've confirmed that calling ::SetLevel() in this file's SetLevel()
		will proceed to /lib/lvs/abilities.c->SetLevel()
		
	8/10/2008 J.C.	-Added functions supporting tracks left in rooms. Checkout 
					tracks.c, exit.c room.c	for more info.	
	
	7/24/2009 J.C. -Added storage for stellarmass status effects along with a few interfaces for
				accessing them.
	
	7/26/2009 J.C. -Added SkillTraining system for skill verbs. Take note: for ease of character levelup,
					we are going to assume that anycharacter that has GetSkillTraining() called for a skill
					not already in the map should simply receive that skill at level 1. There shouldn't be
					a case where it isn't called unless the character should already have it. The skillverb
					system would cancel out once it sees the players doesn't have the appropriate skill.

	8/1/2009 J.C. - Create calls ResetStatuses() in order to ensure things don't get stupid during reloads.

	8/19/2009 J.C -Added leech support for wraiths.
	
	9/2/2009 J.C. -Added 'eventDie' and made a call to remove statuses during death.
*/

#include <lib.h>
#include <rooms.h>
#include <daemons.h>
#include <position.h>
#include <message_class.h>
#include "include/living.h"

inherit LIB_CARRY;
inherit LIB_DROP;
inherit LIB_GET;
inherit LIB_COMBAT;
inherit LIB_CURRENCY;
inherit LIB_FOLLOW;
inherit LIB_MAGIC;
inherit LIB_LEAD;
inherit LIB_TEACH;
inherit LIB_LEARN;
inherit LIB_QUEST;
inherit LIB_STOP;



//STELLARMASS PROJECT
//J.C.
//allow stellarmass class skills to work on all living creatures
#include <stellarmass.h>
#include "/lib/stellarmass/include/status.h"
inherit LIB_SKILLVERBLIVING;
inherit LIB_LIVINGINSTANCE;



//  PROBLEMS DUE TO 2.10 UPDATE:
//There was no room left for the 6 variables I added to the living.c and combat.c files. I have opted to remove
//the ones I don't need and store the rest in the properties mapping. I've managed to squeeze the 'Statuses'
//mapping back into the object however. If I hadn't, I would have had to revert to storing statuses as
//invisible items in the player's inventory again (yuk).

//Tracks.c related variables

//2.10 Just got rid of these. Tracks are always left now
//private int TracksLeft = 1;
//private static int FootprintsLeft = 0;


private static mapping Statuses = ([]);

//2.10 now stored in properties
//private int smSkillTrainingPoints = 0;
//mapping TrainedSkills = ([]);

//END STELLARMASS



private int isPK;
private mixed Attackable = 1;
private int NoCondition = 0;

varargs mixed CanReceiveHealing(object who, string limb);

static void create(){
    combat::create();
    currency::create();
    follow::create();
    lead::create();
    isPK = 0;
    
    //::AddSave(::GetSave());
    //::AddSave( ({"TrainedSkills"}) );
    
    //STELLARMASS
    //2.10 edit (this actually had to be move further up the line to ensure
    //it would accur sooner in the chain during object load)
	if(!this_object()->GetProperty("TrainedSkills"))
		{
		this_object()->SetProperty("TrainedSkills",([]) );
		}
	
	//this_object()->ResetStatuses();
	this_object()->SetCanLeaveTracks(1); //left for archive purposes, doesn't do anything in 2.10 as tracks are always left now
	
	
	
	//if(!this_object()->GetProperty("smSkillTrainingPoints"))
	//	this_object()->SetProperty("smSkillTrainingPoints",0);
	//END STELLARMASS
}

int is_living(){ return 1; }

int inventory_accessible(){ return 1; }

mixed direct_verb_rule(string verb){
    return SOUL_D->CanTarget(this_player(), verb, this_object());
}

mixed direct_ride_str(){
    return this_object()->GetMount();
}

mixed direct_ride_word_str(){
    return this_object()->GetMount();
}

mixed direct_mount_liv(){
    return this_object()->GetMount();
}

mixed direct_dismount_liv(){
    return this_object()->GetMount();
}

mixed direct_dismount_from_liv(){
    return this_object()->GetMount();
}

mixed direct_attack_liv(){
    if(intp(Attackable) && !Attackable){
        return "You are unable to attack "+this_object()->GetShort()+".";
    }
    if(stringp(Attackable)) return Attackable;
    return 1;
}

mixed CanAttack(){
    if(this_player() == this_object()){
        return "You can't attack yourself.";
    }
    if( userp(this_player()) && userp(this_object()) ){
        if(!(environment()->CanAttack(this_player(), this_object()))){
            return "Player killing is not permitted in this area!";
        }
        if(intp(Attackable) && !Attackable){
            return "You are unable to attack "+this_object()->GetShort()+".";
        }
        if(this_player()->GetPK() && this_object()->GetPK()){
            if(!PLAYER_KILL) return "This is not a PK mud.";
        }
        else return "One of you is not a player killer. You cannot fight them.";
    }
    if(functionp(Attackable)) return evaluate(Attackable, this_player());
    else {
        return (Attackable || 0);
    }
}

mixed direct_attack_only_liv(){
    return direct_attack_liv();
}

mixed direct_attack_liv_only(){
    return direct_attack_liv();
}

mixed direct_target_liv(){
    return direct_attack_liv();
}

mixed direct_target_only_liv(){
    return direct_attack_liv();
}

mixed direct_target_liv_only(){
    return direct_attack_liv();
}

mixed direct_bite_liv(){
    return 1;
}

mixed direct_capture_liv_word_obj(){
    return 1;
}

mixed direct_pray_for_str_against_str_for_liv(){
    return 1;
}

mixed direct_cast_str_on_obj(){
    return 1;
}

mixed direct_cast_str_against_str(){
    return 1;
}


mixed direct_cast_str_on_str_of_obj(){
    return 1;
}

mixed direct_free_liv_from_obj(){
    return 1;
}

mixed direct_resurrect_obj(){ return 1; }
mixed indirect_resurrect_obj(){ return 1; }

mixed direct_get_obj(mixed args...){
    int mysize = this_object()->GetSize(1);
    int theirsize = this_player()->GetSize(1);
    if(archp(this_player())) return 1;
    if(creatorp(this_player()) && !creatorp(this_object())){
        return 1;
    }
    if(interactive(this_player()) && creatorp(this_object())){
        return "NO.";
    }
    if(this_object()->GetBefriended(this_player())) return 1;
    if((theirsize - mysize) > 1) return 1;
    return "It's too big!";
}

mixed direct_get_obj_from_obj(mixed args...){
    return direct_get_obj(args...);
}

mixed direct_show_liv_obj(){
    if( this_player() == this_object() ) return "Are you confused?";
    return 1;
}

mixed indirect_show_obj_to_liv(object item){
    if( !item ) return 0;
    if( this_player() == this_object() ) return "Are you confused?";
    if( environment(item) != this_player() ) return "You don't have that!";
    else return 1;
}

mixed indirect_show_obj_liv(object item){
    return indirect_show_obj_to_liv(item);
}

mixed direct_give_liv_obs(){
    return direct_give_liv_obj();
}

mixed direct_give_liv_obj(){
    if( this_player() == this_object() ) return "Are you confused?";
    return 1;
}

mixed indirect_give_obj_to_liv(object item){
    if( !item ) return 0;
    if( this_player() == this_object() ) return "Are you confused?";
    if( environment(item) != this_player() ) return "You don't have that!";
    if(!CanCarry(item->GetMass())){
        return this_object()->GetName()+" is carrying too much.";
    }
    else return CanCarry(item->GetMass());
}

mixed indirect_give_obj_liv(object item){
    return indirect_give_obj_to_liv(item);
}

mixed indirect_give_obs_to_liv(object *items){
    return 1;
}

mixed indirect_give_obs_liv(object *items){
    return indirect_give_obs_to_liv(items);
}

mixed direct_give_liv_wrd_wrd(object targ, string num, string curr){
    return direct_give_wrd_wrd_to_liv(num, curr);
}

mixed direct_give_wrd_wrd_to_liv(string num, string curr){
    mixed tmp;
    int amt;

    if( this_object() == this_player() )
        return "Are you feeling a bit confused?";
    if( (amt = to_int(num)) < 1 ) return "What sort of amount is that?";
    tmp = CanCarry(currency_mass(amt, curr));
    if( tmp != 1 ) return GetName() + " cannot carry that much "+ curr+ ".";
    return 1;
}

mixed direct_steal_wrd_from_liv(string wrd){
    if( wrd != "money" ) return 0;
    if( this_player() == this_object() ) return "Are you a fool?";
    if( this_player()->GetInCombat() )
        return "You are too busy fighting at the moment.";
    return 1;
}

mixed indirect_steal_obj_from_liv(object item, mixed args...){
    mixed tmp;

    if( environment()->GetProperty("no attack") )
        return "Mystical forces prevent your malice.";
    if( !item ) return 1;
    if( environment(item) != this_object() ) return 0;
    if( this_player() == this_object() ) return "Are you a fool?";
    if( this_player()->GetInCombat() )
        return "You are too busy fighting at the moment.";
    tmp = (mixed)item->CanDrop(this_object());
    if( tmp != 1 )
        return GetName() + " will not let go of " + item->GetShort()+".";
    return 1;
}

    mixed direct_backstab_liv(){
        if( this_object() == this_player() )
            return "That would be messy.";
        if( member_array(this_object(), this_player()->GetEnemies()) != -1 )
            return "%^RED%^You have lost the element of surprise.";
        if( environment()->GetProperty("no attack") ||
                GetProperty("no backstab") )
            return "A mysterious forces stays your hand.";
        return 1;
    }

mixed direct_heal_str_of_liv(string limb){
    string array limbs = GetLimbs();
    mixed tmp;

    limb = lower_case(remove_article(limb));
    if( !limbs ){
        if( this_object() == this_player() ){
            return "You have no limbs!";
        }
        else {
            return GetName() + " has no limbs!";
        }
    }
    else if( member_array(limb, limbs) == -1 ){
        if( this_object() == this_player() ){
            return "You have no " + limb + ".";
        }
        else {
            return GetName() + " has no " + limb + ".";
        }
    }
    tmp = CanReceiveHealing(this_player(), limb);
    if( tmp != 1 ){
        return tmp;
    }
    return CanReceiveMagic(0, "heal");
}

mixed direct_remedy_str_of_liv(string limb){
    string *limbs;
    limbs = GetLimbs();
    if( !limbs ){
        if( this_object() == this_player() ) return "You have no limbs!";
        else return GetName() + " has no limbs!";
    }
    else if( member_array(limb, limbs) == -1 ){
        if( this_object() == this_player() ) return "You have no " + limb + ".";
        else return GetName() + " has no " + limb + ".";
    }
    return CanReceiveMagic(0, "remedy");
}

mixed direct_regen_str_on_liv(string limb){
    if( !limb ) return 0;
    if( member_array(limb, GetMissingLimbs()) == -1 ){
        return "That is not a missing limb!";
    }
    return CanReceiveMagic(0, "regen");
}

mixed direct_teleport_to_liv(){
    if( environment()->GetProperty("no teleport") ||
            environment()->GetProperty("no magic") ){
        return "Mystical forces prevent your magic.";
    }
    else return CanReceiveMagic(0, "teleport");
}

mixed direct_portal_to_liv(){
    return direct_teleport_to_liv();
}

    mixed direct_resurrect_liv(){
        if( this_player() == this_object() )
            return "You cannot resurrect yourself.";
        if( !GetUndead() ) 
            return GetName() + " is not dead!";
        return CanReceiveMagic(0, "resurrect");
    }

mixed direct_scry_liv(){
    object env = environment();

    if( this_player() == this_object() )
        return "Scry yourself??";
    if( !env ) return GetName() + " is nowhere.";
    if( env->GetProperty("no magic") || env->GetProperty("no scry") )
        return GetName() + " is beyond your reach.";
    return CanReceiveMagic(0, "scry");
}

mixed indirect_zap_liv(){ return 1; }
mixed direct_zap_liv(){ return 1; }
mixed indirect_pulsecheck_liv(){ return 1; }
mixed direct_pulsecheck_liv(){ return 1; }

/* hostile spells */

int direct_rockwhip_liv(){ return CanReceiveMagic(1, "rockwhip"); }
int direct_acidspray_liv(){ return CanReceiveMagic(1, "acidspray"); }
int direct_annihilate_at_liv(){ return CanReceiveMagic(1, "annihilate"); }
int direct_annihilate_liv(){ return CanReceiveMagic(1, "annihilate"); }
int direct_arrow_liv(){ return CanReceiveMagic(1, "arrow"); }
int direct_arrow_at_liv(){ return CanReceiveMagic(1, "arrow"); }
int direct_blades_at_liv(){ return CanReceiveMagic(1, "blades"); }
int direct_blades_liv(){ return CanReceiveMagic(1, "blades"); }
int direct_corrupt_liv(){ return CanReceiveMagic(1, "currupt"); }
int direct_demonclaw_liv(){ return CanReceiveMagic(1, "demonclaw"); }
int direct_dispel_liv(){ return CanReceiveMagic(1, "dispel"); }
int direct_drain_at_liv(){ return CanReceiveMagic(1, "drain"); }
int direct_drain_liv(){ return CanReceiveMagic(1, "drain"); }
int direct_fireball_at_liv(){ return CanReceiveMagic(1, "fireball"); }
int direct_fireball_liv(){ return CanReceiveMagic(1, "fireball"); }
int direct_frigidus_at_liv(){ return CanReceiveMagic(1, "frigidus"); }
int direct_frigidus_liv(){ return CanReceiveMagic(1, "frigidus"); }
int direct_holylight_liv(){ return CanReceiveMagic(1, "holylight"); }
int direct_missile_liv(){ return CanReceiveMagic(1, "missile"); }
int direct_missile_at_liv(){ return CanReceiveMagic(1, "missile"); }
int direct_shock_liv(){ return CanReceiveMagic(1, "shock"); }
int direct_palm_liv(){ return CanReceiveMagic(1, "palm"); }
int direct_immolate_liv(){ return CanReceiveMagic(1, "immolate"); }
int direct_gale_liv(){ return CanReceiveMagic(1, "gale"); }

/* other spells */

int direct_aura_liv(){ return CanReceiveMagic(0, "aura"); }
int direct_soulseek_liv(){ return CanReceiveMagic(0, "soulseek"); } 
int direct_cloak_wrd(){ return CanReceiveMagic(0, "cloak"); }
int direct_stealth_wrd(){ return CanReceiveMagic(0, "stealth"); }
int direct_backlash_for_liv(){ return CanReceiveMagic(0, "backlash"); }
int direct_backlash_for_liv_against_wrd(){ return CanReceiveMagic(0, "backlash"); }
int direct_balance_obj_to_obj(){ return CanReceiveMagic(0, "balance"); }
int direct_buffer_liv(){ return CanReceiveMagic(0, "buffer"); }
int direct_calm_liv(){ return CanReceiveMagic(0, "calm"); }
int direct_cleanse_liv(){ return CanReceiveMagic(0, "cleanse"); }
int direct_convert_liv(){ return CanReceiveMagic(0, "convert"); }
int direct_shield_liv(){ return CanReceiveMagic(0, "shield"); }
int direct_veil_liv_against_wrd_wrd(){ return CanReceiveMagic(0, "veil"); }
int direct_ward_liv_against_wrd(){ return CanReceiveMagic(0, "ward"); }
int direct_remedy_liv(){ return CanReceiveMagic(0, "remedy"); }
int direct_command_str_to_str(){ return CanReceiveMagic(0, "command"); }
int direct_gaze(){ return CanReceiveMagic(0, "gaze"); }
int direct_send_str_to_str(){ return CanReceiveMagic(0, "send"); }
int direct_connect_str(){ return CanReceiveMagic(0, "connect"); }
int direct_heal_liv(){ return CanReceiveMagic(0, "heal"); }
int direct_mend_liv(){ return CanReceiveMagic(0, "mend"); }
int direct_refresh_liv(){ return CanReceiveMagic(0, "refresh"); }
int direct_rejuvinate_liv(){ return CanReceiveMagic(0, "rejuvinate"); }
int direct_farsight_liv(){ return 1; }
int direct_bump_liv(){ return 1; }
int direct_evade_liv(){ return 1; }
int direct_follow_liv(){ return 1; }
int direct_lead_liv(){ return 1; }
int direct_marry_liv_to_liv(){ return 1; }
int direct_party_wrd_liv(){ return 1; }
int direct_challenge_liv(){ return 1; }
int direct_ignore_liv(){ return 1; }

int indirect_throw_obj_at_obj(){ return 1; }
int indirect_toss_obj_at_obj(){ return 1; }
int indirect_buy_str_from_liv(){ return 1; }
int indirect_sell_obj_to_liv(){ return 1; }
int indirect_marry_liv_to_liv(){ return 1; }


/*     **********     /lib/living.c modal methods     **********     */
int CanCarry(int amount){ return carry::CanCarry(amount); }

varargs mixed CanReceiveHealing(object who, string limb){
    int max, hp;

    max = GetMaxHealthPoints(limb);
    hp = GetHealthPoints(limb);
    if( (max-hp) < max/20 ){
        if( limb ){
            return possessive_noun(GetName()) + " " + limb + " needs no help.";
        }
        else {
            return GetName() + " needs no help.";
        }
    }
    return 1;
}

mixed CanReceiveMagic(int hostile, string spell){
    if( GetProperty(spell) == "immune" ){
        this_player()->eventPrint(GetName() + " is immune to such magic.");
        return 0;
    }
    if( !hostile ) return 1;
    if( this_player() == this_object() ){
        eventPrint("That would be construed as quite foolish.");
        return 0;
    }
    return 1;
}

varargs mixed CanCastMagic(int hostile, string spell){
    object env = environment();

    if( !env ) eventPrint("You are nowhere!");
    if( spell && GetProperty("no " + spell) ){
        eventPrint("A mysterious forces prevents you from doing that.");
        return 0;
    }
    if( env->GetProperty("no magic") ){
        eventPrint("Mystical forces prevent your magic.");
        return 0;
    }
    if( !hostile ) return 1;
    if( env->GetProperty("no attack" ) ){
        eventPrint("Mystical forces prevent your hostile intentions.");
        return 0;
    }
    return 1;
}

/*     **********     /lib/living.c event methods     **********     */

mixed eventCure(object who, int amount, string type){
    object array germs = filter(all_inventory(),
            (: $1->IsGerm() && $1->GetType()== $(type) :));

    if( !sizeof(germs) ){
        return GetName() + " suffers from no such affliction.";
    }
    return germs[0]->eventCure(who, amount, type);
}

int eventFollow(object dest, int followChance){
    string dir;
    int ret;

    if( objectp(dest) ){
        if( !environment() ){
            Destruct();
            return 0;
        }
        dir = environment()->GetDirection(base_name(dest));
    }
    if( !stringp(dir) ) dir = "";
    if( dir != "" && followChance > random(100) ){
        eventForce(dir);
    }
    if( environment() == dest ) return 1;
    else {
        string newdir = replace_string(dir,"go ","");
        if(newdir) ret = environment(this_object())->eventGo(this_object(), newdir);
        if(!ret){
            newdir = replace_string(dir,"enter ","");
            ret = environment(this_object())->eventEnter(this_object(), newdir);
        }
    }
    return ret;
}

mixed eventInfect(object germ){
    return germ->eventInfect(this_object());
}

varargs mixed eventShow(object who, string str){
    who->eventPrint(this_object()->GetLong(str));
    environment(who)->eventPrint(this_player()->GetName() +
            " looks at " + this_object()->GetShort() + ".",
            ({ who, this_object() }));
    return 1;
}

/* when who == this_object(), I am doing the stealing
 * otherwise I am being stolen from
 */
varargs mixed eventSteal(object who, mixed what, object target, int skill){

    int i, sr;

    if( who == this_object() ){
        mixed tmp;
        int amt, skill2;

        skill2 = to_int(to_float(GetSkillLevel("stealing"))*2.5);
        skill2 += GetMobility();
        skill2 += GetStatLevel("coordination");
        skill2 += GetStatLevel("charisma");
        skill2 += to_int(GetStatLevel("luck")/2);
        if( ClassMember("rogue") ) skill2 += GetLevel();
        if( ClassMember("thief") ) skill2 += GetLevel();

        if( !stringp(what) ){
            int x;

            x = sizeof(what);
            if( GetStaminaPoints() < 20.0*x ){
                eventPrint("You are clumsy in your fatigue.");
                if(target->GetRace() != "kender"){
                    target->SetAttack(this_object());
                    target->eventExecuteAttack(this_object());
                }
                return 1;
            }
            AddStaminaPoints(-20);

            tmp = (mixed)target->eventSteal(who, what, target,skill2);

            /* You can't steal from this target */
            if( !tmp )
                return "You cannot steal from " + target->GetName() +".";

            /* Steal from target was succesful */
            else if( tmp == 1 ){

                what = filter(what, (: $1 :));
                AddSkillPoints("stealing", implode(map(what,
                                (: $1->GetMass() :)),
                            (: $1 + $2 :)) +
                        GetSkillLevel("stealing") * GetSkillLevel("stealing")*3);
                AddSkillPoints("stealth", random(sizeof(what)) * 20);
                AddStaminaPoints(-2);
                this_player()->eventPrint(sprintf("You steal %s from %s.",
                            "something", target->GetName()) );
                what->eventMove(this_object());
                return 1;
            }

            /* Steal was unsuccesful */
            else if( tmp == 2 ){
                AddSkillPoints("stealing", GetSkillLevel("stealing")*2);
                AddStaminaPoints(-5);
                return 1;
            }

            else return tmp;
        }
        /********************************************/
        /* This part deals with stealing money */

        amt = GetNetWorth();
        eventPrint("You reach for " + possessive_noun(target) + " money.");
        tmp = (mixed)target->eventSteal(who, what, target, skill2);

        /* You can't steal from this target */
        if( !tmp )
            return "You cannot steal from " + target->GetName() + ".";

        /* Steal from target was succesful */
        else if( tmp == 1 ){
            amt = GetNetWorth() - amt;
            if( amt < 1 ) return tmp;
            AddSkillPoints("stealing", random(7*amt) +
                    GetSkillLevel("stealing") * GetSkillLevel("stealing") * 2);
            AddSkillPoints("stealth", random(amt));
            AddStatPoints("coordination", random(amt));
            AddStaminaPoints(-3);
            eventPrint("You come away with some money!");
            return tmp;
        }

        /* Steal was unsuccesful */
        else if( tmp == 2){
            AddSkillPoints("stealing", GetSkillLevel("stealing")*2);
            AddStaminaPoints(-5);
            return 1;
        }

        return tmp;
    }
    /*******************************************************************/
    if( GetInCombat() ){
        who->eventPrint(GetName() + " is busy in combat.");
        return 0;
    }
    skill -= to_int(GetMobility()/2);
    skill -= GetStatLevel("agility");
    skill -= GetStatLevel("wisdom");
    skill -= to_int(GetStatLevel("luck")/3);

    if( objectp(what) ) sr = 100 * sizeof(what);
    else sr = 100;
    if( random(sr) > skill ){
        target->eventPrint("You notice " + who->GetName() + " trying "
                "to steal from you!");
        if( !userp(this_object()) ){
            who->eventPrint("%^RED%^" + GetName() + "%^RED%^ "
                    "notices your attempt at treachery!",
                    environment(who) );
            eventForce("attack " + who->GetKeyName());
            this_object()->SetProperty("steal victim", 1);
        }
        return 2;
    }

    if( random(2*sr) > skill ){
        who->eventPrint("You are unsure if anyone noticed your foolish "
                "attempt at thievery.",environment(who) );
        return 2;
    }

    if( stringp(what) ){

        foreach(string curr in GetCurrencies()){
            int x;

            if( !(x = random(GetCurrency(curr)/5)) ) continue;
            if( x > 100 ) x = 100;
            x = (x * skill)/100;
            AddCurrency(curr, -x);
            who->AddCurrency(curr, x);
        }
        return 1;
    }
    for(i=0; i<sizeof(what); i++){
        if( (mixed)what[i]->eventSteal(who) != 1 ) what[i] = 0;
    }
    return 1;
}

int AddCarriedMass(int x){ return carry::AddCarriedMass(x); }

int GetCarriedMass(){
    return (currency::GetCurrencyMass() + carry::GetCarriedMass());
}

int GetNonCurrencyMass(){
    return (carry::GetCarriedMass());
}

int GetMaxCarry(){ return combat::GetMaxCarry(); }

int SetPK(int x){ return (isPK = x); }

int GetPK(){ return isPK; }

int SetDead(int i){
    return combat::SetDead(i);
}

mixed SetAttackable(mixed foo){
    Attackable = foo;
    return Attackable;
}

mixed GetAttackable(){
    return Attackable;
}

int SetNoCondition(int foo){
    NoCondition = foo;
    return NoCondition;
}

int GetNoCondition(){
    return NoCondition;
}

varargs int eventMoveLiving(mixed dest, string omsg, string imsg, mixed dir){
    object *inv;
    object prev;
    string prevclim, newclim;
    //STELLARMASS
    int instResult;
    //END STELLARMASS
    int check = GUARD_D->CheckMove(this_object(), dest, dir);

    if(!check){
        eventPrint("You remain where you are.", MSG_SYSTEM);
        return 0;
    }
    
    
    //STELLARMASS
	if(this_object()->HasStatus("entrenched"))
		{
		eventPrint("You can't move while under cover. Try 'endcover'.");
		return 0;
		}
    
    
    this_object()->DebugMsg("Instanced? " + dest->GetShort());
	//This function calls eventMoveLiving with an instance if passed a master copy.
	//However, it will simply return 0 if passed a cloned room.
	if(instResult = this_object()->eventRegisterInstance(dest,omsg,imsg,dir))
		{
		this_object()->DebugMsg("Successfully Instanced Room!");
		return instResult;
		}
	this_object()->DebugMsg("Enter Room: " + (string)dest);
    //END STELLARMASS
    
    
    if(omsg && stringp(omsg)){
        omsg = replace_string(omsg, "$N", this_object()->GetName());
    }
    if(imsg && stringp(imsg)){
        imsg = replace_string(imsg, "$N", this_object()->GetName());
    }

    if( prev = environment() ){
        prevclim = prev->GetClimate();
        if( stringp(dest) ){
            if(dest[0] != '/'){
                string *arr;

                arr = explode(file_name(prev), "/");
                dest = "/"+implode(arr[0..sizeof(arr)-2], "/")+"/"+dest;
            }
        }
        if( !eventMove(dest) ){
            eventPrint("You remain where you are.", MSG_SYSTEM);
            return 0;
        }
        if(prev){
            inv = filter(all_inventory(prev), (: (!this_object()->GetInvis($1) 
                            && living($1) && !GetProperty("stealthy") && ($1 != this_object())) :));
        }
        if(!dir) dir = "away";
        if(query_verb() == "home" ){
            if(!omsg || omsg == "") omsg = GetMessage("telout");
            if(!imsg || imsg == "") imsg = GetMessage("telin");
        }
        else if(GetPosition() == POSITION_SITTING ||
                GetPosition() == POSITION_LYING ){
            if(!omsg || omsg == "") omsg = GetName()+" crawls "+dir+".";
            if(!imsg || imsg == "") imsg = GetName()+" crawls in.";
        }
        else if(GetPosition() == POSITION_FLYING ){
            if(!omsg || omsg == "") omsg = GetName()+" flies "+dir+".";
            if(!imsg || imsg == "") imsg = GetName()+" flies in.";
        }
        else {
            if(!omsg || omsg == "") omsg = GetMessage("leave",dir);
            if(!imsg || imsg == "") imsg = GetMessage("come");
        }
        if(sizeof(inv)) inv->eventPrint(omsg, MSG_ENV);
    }
    else if( !eventMove(dest) ){
        eventPrint("You remain where you are.", MSG_SYSTEM);
        return 0;
    }
    inv = filter(all_inventory(environment()),
            (: (!this_object()->GetInvis($1) && !GetProperty("stealthy") &&
                living($1) && ($1 != this_object())) :));

    inv->eventPrint(imsg, MSG_ENV);
    if(this_object()->GetInvis()){
        if(!creatorp(this_object())) AddStaminaPoints(-(15-(GetSkillLevel("stealth")/10)));
        AddSkillPoints("stealth", 30 + GetSkillLevel("stealth")*2);
        eventPrint("%^RED%^You move along quietly....%^RESET%^\n");
    }
    if(GetProperty("stealthy") && interactive(this_object())){
        if(!creatorp(this_object())) AddStaminaPoints(-3 - random(3));
        AddSkillPoints("stealth", 10 + GetSkillLevel("stealth")*2);
    }
    this_object()->eventDescribeEnvironment(this_object()->GetBriefMode());
    newclim = environment()->GetClimate();
    if( !GetUndead() ) switch( newclim ){
        case "arid":
            if(!creatorp(this_object())) AddStaminaPoints(-0.3);
        break;
        case "tropical":
            if(!creatorp(this_object())) AddStaminaPoints(-0.3);
        break;
        case "sub-tropical":
            if(!creatorp(this_object())) AddStaminaPoints(-0.2);
        break;
        case "sub-arctic":
            if(!creatorp(this_object())) AddStaminaPoints(-0.2);
        break;
        case "arctic":
            if(!creatorp(this_object())) AddStaminaPoints(-0.3);	  
        break;
        default:
        if(!creatorp(this_object())) AddStaminaPoints(-0.1);	  
        break;	    
    }
    if( prevclim != newclim && prevclim != "indoors" && newclim != "indoors" ){
        switch(prevclim){
            case "arid":
                if( newclim == "tropical" || newclim == "sub-tropical" )
                    message("environment", "The air is much more humid.",
                            this_object());
                else message("environment", "The air is getting a bit cooler.",
                        this_object());
            break;
            case "tropical":
                if( newclim != "arid" )
                    message("environment", "The air is not quite as humid.",
                            this_object());
                else message("environment", "The air has become suddenly dry.",
                        this_object());
            break;
            case "sub-tropical":
                if( newclim == "arid" )
                    message("environment", "The air has become suddenly dry.",
                            this_object());
                else if( newclim == "tropical" )
                    message("environment","The air has gotten a bit more humid.",
                            this_object());
                else message("environment", "The air is not quite as humid.",
                        this_object());
            break;
            case "temperate":
                if( newclim == "arid" )
                    message("environment", "The air is a bit drier and warmer.",
                            this_object());
                else if( newclim == "tropical" )
                    message("environment", "The air is much more humid.",
                            this_object());
                else if( newclim == "sub-tropical" )
                    message("environment", "The air is a bit more humid.",
                            this_object());
                else message("environment", "The air is a bit colder now.",
                        this_object());
            break;
            case "sub-arctic":
                if( newclim == "arid" || newclim == "tropical" ||
                        newclim == "sub-tropical" )
                    message("environment", "It has suddenly grown very hot.",
                            this_object());
                else if( newclim == "arctic" )
                    message("environment", "It is a bit cooler than before.",
                            this_object());
                else message("environment", "It is not quite as cold as "
                        "before.", this_object());
            break;
            case "arctic":
                if( newclim == "sub-arctic" )
                    message("environment", "It is not quite as cold now.",
                            this_object());
                else message("environment", "It is suddenly much warmer than "
                        "before.", this_object());
        }
    }
    eventMoveFollowers(environment(this_object()));
    TRACKER_D->TrackLiving(this_object());
    return 1;
}

string GetEquippedShort(){
    return this_object()->GetHealthShort();
}

int GeteventPrints(){
    return 1;
}



//*********************************************************************************************************************
// STELLARMASS PROJECT
// 7/2008 
// J.C.
//
void SetLevelClassSkills(int level)
	{
	string playerClass = GetClass();
	if(!playerClass || playerClass == "") playerClass = "npc";
	
	//make sure no skills exist that shouldn't
	foreach(string skill in CLASSES_D->GetSkills(playerClass))
		{
		int levelReceived = CLASSES_D->GetSkillReceiveLevel(playerClass,skill);
				
		//check if skill already exists
		if(GetSkillLevel(skill))
			{
			//is the player supposed to have it?
			if(levelReceived > level)
				{
				//they shouldn't have this skill at this level				
				RemoveSkill(skill);
				tell_object(this_object(),"%^YELLOW%^You have lost the class skill " + skill + "!");
				}			
			}
		else{			
			//should they recive it?
			if(levelReceived <= level)
				{
				//...yes? Then give it to them
						
				//J.C. For some reason that is unknown to me or the gods the tier is
				//the second param in this this call rather than the third like in SetSkill().
				//-Actually, I guess it's so that it will always be set in the 'Add' version and can
				//be vararged out in the 'Set' version. *sigh*
				AddSkill(skill,CLASSES_D->GetSkillTier(playerClass,skill),CLASSES_D->GetSkillStart(playerClass,skill));
				tell_object(this_object(),"%^GREEN%^You have received " + skill + " as a new class skill!");
				}			
			}
		}
	}

void SetLevelRaceSkills(int level)
	{
	string playerRace = GetRace();
	if(!playerRace || playerRace == "") playerRace = "unidentified";
	
	//make sure no skills exist that shouldn't
	foreach(string skill in RACES_D->GetSkills(playerRace))
		{
		int levelReceived = RACES_D->GetSkillReceiveLevel(playerRace,skill);
		
		if(GetSkillLevel(skill))
			{			
			if(levelReceived > level)
				{			
				RemoveSkill(skill);
				tell_object(this_object(),"%^YELLOW%^You have lost the racial skill " + skill + "!");
				}			
			}
		else{
			if(levelReceived <= level)
				{				
				AddSkill(skill,RACES_D->GetSkillTier(playerRace,skill),RACES_D->GetSkillStart(playerRace,skill));
				tell_object(this_object(),"%^GREEN%^You have received " + skill + " as a new racial skill!");
				}			
			}
		}
	}

int SetLevel(int level)
	{
	//mixed dur,spd,itl;
	int oldLevel = GetLevel();
	int ret = ::SetLevel(level);	
	string playerRace = GetRace();
	string playerClass = GetClass();
		
	if(!playerRace || playerRace == "") playerRace = "unidentified";
	if(!playerClass || playerClass == "") playerClass = "npc";
	
	if(level < 1)
		{
		level = 1;
		error("Attempt to SetLevel( < 1)");
		tc("Char: " + this_object()->GetName() + " - Set Level = " + level);
		tc("Attempt to SetLevel( < 1 )!!");
		}
	
	//add/remove class skills based on level
	SetLevelClassSkills(level);
	SetLevelRaceSkills(level);
	
	
	//set HP, ST, and MP
	//1/3/2009 J.C.
	//Updated these stats and class files so that they have a different default starting value.
	SetMaxHealthPoints( CLASSES_D->GetHP(playerClass) * (level-1) + CLASSES_D->GetStartHP(playerClass) );
	SetMaxStaminaPoints(CLASSES_D->GetST(playerClass) * (level-1) + CLASSES_D->GetStartST(playerClass) );
	SetMaxMagicPoints(  CLASSES_D->GetMP(playerClass) * (level-1) + CLASSES_D->GetStartMP(playerClass) );
	
	
	this_object()->SetHealthPoints(this_object()->GetMaxHealthPoints());
	this_object()->SetStaminaPoints(this_object()->GetMaxStaminaPoints());
	this_object()->SetMagicPoints(this_object()->GetMaxMagicPoints());
	
	return ret;
	}


//----------------------------------------------------------------------------------------------------------
//STELLARMASS 2.10 EDIT

int CanLeaveTracks()
	{
	return 1;//TracksLeft;
	}

int CanLeaveFootprints()
	{
	return 0;//FootprintsLeft;
	}
	
void SetCanLeaveTracks(int allowed)
	{
	//TracksLeft = allowed;
	}

void SetCanLeaveFootprints(int allowed)
	{
	//FootprintsLeft = allowed;
	}

int AddStatus(object status)
	{
	string* keys;
	if(!status || !objectp(status) || !status->IsStellarmassStatus())	return 0;
	
	keys = keys(Statuses);
	if(!sizeof(keys))
		{
		//there are no statuses at all
		Statuses = ([status->GetName() : status]);
		if(!(status->eventMove(this_object() )) )
			{
			error("There was an error moving the status into the target's inventory.");
			return 0;
			}
		return 1;
		}
	
	
	//check for any previous status of the same name
	foreach(string item in keys)
		{
		if(item == status->GetName())
			{
			//replacing a previously existing status
			
			//NOTE: For now all different statusstack types do the same thing
			//the new status always overrides the old status as far as
			//StatusType is concerned.
			//map_delete(Statuses,previous->GetName());
			Statuses[item]->eventDestruct(0);
			Statuses[status->GetName()] = status;
			return 1;
			}
		}
	
	//adding a new status
	Statuses += ([status->GetName() : status]);
	if(!(status->eventMove(this_object() )) )
		{
		error("There was an error moving the status into the target's inventory.");
		return 0;
		}
	return 1;
	}

/*
	Removes the reference to the status object and moves the actual object from the person's
	inventory to the furnace. It dones't destroy it since that is what should have triggered
	this in the first place.
*/
varargs void RemoveStatus(object status)
	{
	if(!status || !objectp(status))
		{
		error("Invalid status passed to living->RemovStatus()");
		return;
		}
	if(!Statuses || !mapp(Statuses))
		{
		error("living->Statuses found to be invalid in living->RemoveStatus()");
		return;
		}
	
	status->eventMove(ROOM_FURNACE);
	map_delete(Statuses,status->GetName());
	}

array GetAllStatuses()
	{
	return values(Statuses);
	}

int HasStatus(string name)
	{
	if(member_array(name,keys(Statuses)) == -1)
		{
		return 0;
		}
	return 1;
	}

void CleanInventoryOfStatuses()
	{
	foreach(object status in all_inventory(this_player()))
		{
		if(status->IsStellarmassStatus())
			{
			//we aren't triggering the whole destruct chain and removal, we just want this object gone!
			destruct(status);
			}
		}
	}

void ResetStatuses()
	{
	foreach(object status in values(Statuses))
		{
		//causes all statuses to trigger their restoring functions as well as
		//callback to this object's 'RemoveStatus()'
		if(status)	{status->eventDestruct(0);}
		}
	
	Statuses = ([]);
	CleanInventoryOfStatuses();
	}

int eventRemoveStatus(string name)
	{
	object status;
	
	if(catch(status = Statuses[name]))
		{
		return 0;
		}
	
	if(status && objectp(status) && status->IsStellarmassStatus())
		{
		//automagically calls back to this object's 'RemoveStatus()'
		status->eventDestruct(0);
		}
	return 1;
	}
/*
int GetSkillTraining(string skill)
	{
	int x = 0;
	if(catch(x = TrainedSkills[skill]))
		{
		return x;
		}
	return x;
	}

int SetSkillTraining(string skill,int x)
	{
	if(x)	{return (TrainedSkills[skill] = x);}
	else{map_delete(TrainedSkills,skill);}
	return 0;
	}

mixed AddSkillTraining(string skill,int x)
	{
	if(!this_object()->GetSkillLevel(skill))	
		{
		//SPECIAL CASES!!!
		if(skill != "wheresmyhat" && skill != "nevertellmetheodds")
			{
			return "You don't have that skill.";
			}
		}
	
	if(TRAINABLESKILLS_D->IsTrainableSkill(skill))
		{
		if(TrainedSkills[skill] + x > 100)
			{
			return "You can't put that many points into this skill. The remaining amount you can spend is " + (100 - TrainedSkills[skill]) + " points.";
			}
		
		//we need to add the default point first
		return (TrainedSkills[skill] += x);
		}
	
	return 0;
	}

mapping GetTrainedSkills()
	{
	return TrainedSkills;
	}

int AddSkillTrainingPoints(int x)
	{
	return (smSkillTrainingPoints += x);
	}

int GetSkillTrainingPoints()
	{
	return smSkillTrainingPoints;
	}
*/
//END STELLARMASS 2.9a13

//STELLARMASS 2.10 EDIT
int GetSkillTraining(string skill)
	{
	int x = 0;
	mapping map = this_object()->GetProperty("TrainedSkills");
	if(!map)	{this_object()->SetProperty("TrainedSkills",([]) );}
	
	if(catch(x = this_object()->GetProperty("TrainedSkills")[skill]))
		{
		return x;
		}
	return x;
	}

int SetSkillTraining(string skill,int x)
	{
	mapping map = this_object()->GetProperty("TrainedSkills");
	if(!map)	{this_object()->SetProperty("TrainedSkills",([]) );}
	
	if(x)	{return (map[skill],x);}
	else{map_delete(map,skill);}
	return 0;
	}

mixed AddSkillTraining(string skill,int x)
	{
	mapping map = this_object()->GetProperty("TrainedSkills");
	if(!map)	{this_object()->SetProperty("TrainedSkills",([]) );}
	
	if(!this_object()->GetSkillLevel(skill))
		{
		//SPECIAL CASES!!!
		if(skill != "wheresmyhat" && skill != "nevertellmetheodds")
			{
			return "You don't have that skill.";
			}
		}
	
	if(TRAINABLESKILLS_D->IsTrainableSkill(skill))
		{
		//we need to add the default point first, it is assumes that if the players has this
		//skill and we've made it this far, they should have one point in it
		if(!map[skill]) {map[skill] = 1;}
		
		if(map[skill] + x > 100)
			{
			return "You can't put that many points into this skill. The remaining amount you can spend is " + (100 - map[skill]) + " points.";
			}
		
		return (map[skill] += x );
		}
	
	return 0;
	}

mapping GetTrainedSkills()
	{
	mapping map = this_object()->GetProperty("TrainedSkills");
	if(!map)	{this_object()->SetProperty("TrainedSkills",([]) );}
	
	return map;
	}

int AddSkillTrainingPoints(int x)
	{
	return (this_object()->SetProperty("smSkillTrainingPoints", this_object()->GetProperty("smSkillTrainingPoints") + x) );
	}

int GetSkillTrainingPoints()
	{
	return this_object()->GetProperty("smSkillTrainingPoints");
	}
//----------------------------------------------------------------------------------------------------------
//END STELLARMASS (2.10 EDIT)


/*
	Checks for 'fedora' for the 'wheres my hat' skill
*/
int eventWear(object ob, mixed limbs)
	{
	if(this_object()->GetSkillLevel("Where's my hat?"))
		{
		if(ob && objectp(ob))
			{
			if(sizeof( filter_array(ob->GetId(),(: $1 == "fedora" :)) ))
				{
				object status = new("/lib/stellarmass/premade_status/wheresmyhat");
				if(status && objectp(status))	status->init(this_player());
				}
			}
		}
	
	return ::eventWear(ob,limbs);
	}
//END STELLARMASS

int eventRemoveItem(object ob)
	{
	if(ob && objectp(ob))
		{
		eventRemoveStatus("Where's my hat?");
		}
	
	return ::eventRemoveItem(ob);
	}

int CanLeech()
	{
	return 1;
	}

mixed direct_leech_liv()
	{
	return 1;
	}
#define LEECH_COST 5

int eventLeech(object who)
	{
	if(!who || !objectp(who))	return 0;
	if(who->GetCurrency("credits") < LEECH_COST)	{return 0;}
	
	this_object()->AddCurrency("credits",LEECH_COST);
	this_object()->AddStaminaPoints(-3);
	
	who->AddCurrency("credits",-LEECH_COST);
	who->AddStaminaPoints(20);
	who->AddHP(1);
	return 1;
	}

int eventLeechEnemy(object who)
	{
	if(!who || !objectp(who))	return 0;
	
	if(this_object()->eventReceiveAttack(random(who->CanMelee(this_object())),"melee",who))
		{
		this_object()->AddStaminaPoints(-10);
		
		who->AddStaminaPoints(25);
		who->AddHP(1);
		return 1;
		}
	return 0;
	}

varargs int eventDie(mixed agent)
	{
	this_object()->ResetStatuses();
	return ::eventDie(agent);
	}

void RestorePassiveStatuses()
	{
	object player = this_object();
	
	if(player->GetSkillLevel("wrench"))
		{
		object wrench = new(DIR_STELLARMASSSTATUSDIR "/wrench");
		if(!wrench)
			{
			receive("Failed creating wrench status during character update.");
			}
		else{wrench->init(player);}
		}
	if(player->GetSkillLevel("5fingerdiscount"))
		{
		object discount = new(DIR_STELLARMASSSTATUSDIR "/5fingerdiscount");
		if(!discount)
			{
			receive("Failed creating 5fingerdiscount status during character update.");
			}
		else{discount->init(player);}
		}
	}

// END STELLARMASS PROJECT
//**********************************************************************************************************************


