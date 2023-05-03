using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


using Parser = Stellarmass.LPC.Parser;
using Lexer = Stellarmass.LPC.Lexer;
using Scanner = Stellarmass.LPC.Scanner;

namespace ParserTester.LPC
	{
	/*
	 * Unit Test List:
	 * 
	 * -all unknown
	 * -all known
	 * -begin with unknown and end with known
	 * -begin with known and end with unknown
	 * 
	 * -begin and end with unknown
	 * -begin and end with known
	 * -interlaced known and unknown
	 * -interlaced unknow and known
	 * 
	 * -recursive knowns
	 * -recursive unknowns
	 * 
	 * -double recursive knowns
	 * -double recursive unknowns
	 * 
	 * -compilation of all elements
	 * -actual code test
	 * 
	 */
	public static class RoomUnitTests
		{
		readonly public static List<string> UnitTests = new List<string>();
		
		static RoomUnitTests()
			{
			string code;
			
			//Unit Test Section 1
			//0
			code = @"int Value = 0; inherit LIB_THING; float fValue = 0.0;";
			UnitTests.Add(code);
			
			//1
			code = @"aFunc(1); bFunc(2); cFunc(3);";
			UnitTests.Add(code);
			
			//2
			code = @"int Value = 0; Func();";
			UnitTests.Add(code);
			
			//3
			code = @"Func(); int Value = 0;";
			UnitTests.Add(code);
			
			
			//Unit Test Section 2
			//4
			code = @"inherit LIB_THING; Func(); int Value = 0;";
			UnitTests.Add(code);
			
			//5
			code = @"fFunc(); inherit LIB_THING; sFunc();";
			UnitTests.Add(code);
			
			//6
			code = @"fFunc(); interlace sFunc(); int Value = 0;";
			UnitTests.Add(code);
			
			//7
			code = @"interlace fFunc(); int Value = 0; sFunc();";
			UnitTests.Add(code);
			
			
			//Unit Test Section 3
			//8
			code = @"fFunc(1); sFunc();";
			UnitTests.Add(code);
			
			//9
			code = @"fFunc(); sFunc(2);";
			UnitTests.Add(code);
			
			//10
			code = @"fFunc(int Value = 0;); sFunc(2);";
			UnitTests.Add(code);
			
			//11
			code = @"fFunc(2); inherit LIB_THING; sFunc(int Value = 0;);";
			UnitTests.Add(code);
			
			//12
			code = @"unknown(1) inherit LIB_THING; sFunc(int Value = 0;);";
			UnitTests.Add(code);
			
			//13
			code = @"unknown(1) inherit {LIB_THING}; sFunc(int Value = 0;);";
			UnitTests.Add(code);
			
			//14
			code = @"sFunc(int Value = 0;); unknown(1) inherit {LIB_THING};";
			UnitTests.Add(code);
			
			//15
			code = @"unknown(1) {other} func(""string""); unknown2(1){2}";
			UnitTests.Add(code);
			
			
			//Unit Test Section 4
			//16
			code = @"fFunc(iFunc();); sFunc();";
			UnitTests.Add(code);
			
			//17
			code = @"fFunc(); sFunc(iFunc(););";
			UnitTests.Add(code);
			
			//18
			code = @"fFunc(iFunc();); sFunc(eFunc(););";
			UnitTests.Add(code);
			
			//19
			code = @"fFunc(iFunc();); inherit(eFunc();) {int value = 0;}";
			UnitTests.Add(code);
			
			
			//Unit Test Section 5
			
			//20
			code = @"int
			 Value = 0; inherit;
func();
::func();
inherit;
";
			UnitTests.Add(code);
			
			//21
			code = @"main()
			{
			functionCall();
			}";
			UnitTests.Add(code);


			//22
			code = @"main()
			{
			functionCall();
			}
			
			init()
			{
			Call();
			}";
			UnitTests.Add(code);
			
			
			//23
			code = @"Function(){
	inherit LIB_ROOM();
	body();
	::body();
	}

Function() {
	""string""body();
	}
";
			UnitTests.Add(code);
			
			
			
			//24
			code = @"#include <lib.h>
	
inherit LIB_ROOM;
inherit LIB_CONTAINER;
re::ImAFuncCall();
static void create(int variable){
	::create();
	inherit ();
	SetShort(""Empty Room"");
	SetLong(""An empty room."");
	
}


void init(){
init();
}";
			UnitTests.Add(code);
			

			//25   - newline tests
			code = @"#inherit THING;
			inherit OTHER_THING;";
			UnitTests.Add(code);

			//26  - commented out block starers
			code = @"thing//comment
//comment2";
			UnitTests.Add(code);

			//27 - PARSER FLAW: Comment Lines are only detected when a newline comes after them
			code = @"///StellarmapCode:Inherit
			";
			UnitTests.Add(code);

			//28
			code = @"/* }
			(
 */";
			UnitTests.Add(code);


			//29 - checking for, not recognized by the language, found inside of string and comments
			code = @"string = ""C:/ds/lib""";
			UnitTests.Add(code);
			
			//30 - checking for, not recognized by the language, found inside of string and comments
			code = @"string = ""C:\ds\lib""";
			UnitTests.Add(code);

			//31
			code = @"string = ""It's a beautiful day in the neighborhood!""";
			UnitTests.Add(code);

			//32
			code = @"eventForce(""speak in common There is one right over there. Why don't you pick it up with \'get "" + ammo->GetKeyName() + ""\'."");";
			UnitTests.Add(code);

			//33
			code = @"#include <lib.h>
#include <rooms.h>
#include <position.h>

inherit LIB_ROOM;

int PreRelease(string str)
{
    if(this_player()->GetPosition() == POSITION_LYING)
    {
        write(""You kick your way through a vent and jump down from quite a height. You never even realized that you had been crawling up that whole time. It feels so good to be out in a normal room again!"");
        return 1;
    }

    write(""You need to %^GREEN%^lie down %^WHITE%^and %^GREEN%^crawl out %^WHITE%^of the vent."");
    return 0;
}

void create() {
    room::create();
    SetClimate(""indoors"");
    SetAmbientLight(30);
    SetShort(""Solid Snaking It"");
    
    AddExit(""out"",""/domains/basictraining/room/training5.c"",(:PreRelease:));
    SetLong(""You crawl through a long series of metal vents. The sound of your movement reverberates all around you. Just when you think it's going to drive you insane you see a light at last!"");
}

void init(){	
    ::init();
}";			UnitTests.Add(code);

			//34
			UnitTests.Add(BigCode());

			//35
			code = @"sluggy->CallFunc();";
			UnitTests.Add(code);
			
			//36 - failed (its ok, it just means there is a syntax error)
			//code = @"sluggy-'>CallFunc();";
			//UnitTests.Add(code);
			
			//36
			code = @"/*STELLARMASS""string""*/
""string""";
			UnitTests.Add(code);

			//37
			code = ReadFile("meal.c");
			UnitTests.Add(code);
			
			//38
			code = ReadFile("exits.c");
			UnitTests.Add(code);
			
			//39
			code = ReadFile("race.c");
			UnitTests.Add(code);

			//40
			code = ReadFile("living.c");
			UnitTests.Add(code);
			
			//41 - FAILURE - missread variable declaration as inherit command
			code = @"inherit STUFF;
int x;";
			UnitTests.Add(code);
			
			//42
			code = @"inherit STUFF;
int x;
Function1();
::Function2();
de::Function3( stuff, other stuff );
static void create() 
	{}";
			UnitTests.Add(code);
			
			code = @"trhing();
FUnction2();";
			UnitTests.Add(code);

			}//end constructor
		
		
		public static string GetUnitTest(string sectionName,int index)
			{
			return UnitTests[index];
			}
		
		public static List<string> GetUnitTestSection(string sectionName)
			{
			return null;
			}

		public static void CompareUnitTests(List<Parser.Token> tokenList, int unitIndex)
			{
			StringBuilder str = new StringBuilder();
			foreach(Parser.Token token in tokenList)
				{
				str.Append(token.Code);
				}
			
			string s = LPC.RoomUnitTests.GetUnitTest("",unitIndex);
			s = s.Replace(Environment.NewLine,"\n");
			s = s.Replace("\r\n","\n");
			s = s.Replace('\r','\n');
			
			
			string sb = str.ToString(); //this is here just for visual debugging - remove when done
			if(str.ToString() != s)
				{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("WARNING!!! UNIT TEST FAILED!!!");
				Console.ForegroundColor = ConsoleColor.Gray;
				}
			else{
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine("Unit Test Success");
				Console.ForegroundColor = ConsoleColor.Gray;
				}
			}

		public static string BigCode()
			{
			string c = @"#include <lib.h>

inherit LIB_SENTIENT;

#define GUN_FILE	""/domains/stellarmass/weap/laserpistol""
#define AMMO_FILE	""/domains/stellarmass/weap/ammo/smblastercell""

mapping Comments = ([
		""firearm"" : ""You need to learn how to operate standard galactic firearms. Try \'help load\' and \'help unload\' for details."",
		""ammotype"" : ""There are two arch-types of weapons and ammo, magazine-based and round-based."",
		""magazine"" : ""Clips contain all of their rounds in one convenient package. They load faster than round-based weapons but their ammo cannot be transfered from one clip to another. Weapons that use them often tend to be weaker too."",
		""round"" : ""Round-based weapons can be a double edged sword. They need to be loaded one at a time which can be excruciating during combat. However, if you plan ahead before a fight you can load multiple types of compatible ammo to add a good mix of damage. Many round-based weapons tend to quite powerful."",
		""combat"" : ""If you run out of ammo during a fight you will automatically %^GREEN%^scrounge%^BOLD%^%^CYAN%^ for more. However, since you are in a rush you won't have time to check what type it is, only that it fits and fires!"",
		""scrounge"" : ""If you can't find any ammo during a firefight you'll simply stop scrounging and pistol whip with your weapon. Scrounging will cease to work until you have loaded fresh ammo into that weapon."",
		""firemode"" : ""Many firearms have mutiple modes of fire that you can cycle through. Try \'help firemode\' for details."",
		""licking"" : ""Must you lick everything? Yeech."",
		""school"" : ""And remember kids! Stay in milk, drink yo' school, don't do sleep, and get at least eight hours of drugs, everyday! 'Cause dat's what Mr. T say!"",
		]);
	
array Required = ({
	""firearm"",
	""ammotype"",
	""magazine"",
	""round"",
	""combat"",
	""scrounge"",
	""firemode"",
	});

void TalkAbout(string param)
	{
	tell_object(this_player(),""Schrody tells you, %^BOLD%^%^CYAN%^\"" + Comments[param] + \""%^RESET%^"");
	this_player()->SetProperty(param,1);
	
	foreach(string str in Required)
		{
		if(!this_player()->GetProperty(str))
			{			
			return;
			}
		}
	
	if(this_player()->GetProperty(""training level"") < 1)
		{
		tell_object(this_player(),""%^YELLOW%^Alright! You are getting the hang of it! Let's move on to the shooting range."");
		this_player()->SetProperty(""training level"",1);
		}
	}

void GiveAmmo()
	{
	object ammo;
	int numclips = 0;
	
	//make sure we don't have one lying around somewhere
	if(ammo = present(""ammo"",environment(this_object())) )
		{
		eventForce(""speak in common There is one right over there. Why don't you pick it up with \'get "" + ammo->GetKeyName() + ""\'."");
		return;
		}
	
	//make sure the player isn't being tricky and trying to hide one
	//on poor Schrody...
	ammo = present(""ammo"",this_object());
	
	//looks like we can safely try to clone a new one
	if(!ammo && !objectp(ammo))	{ammo = new(AMMO_FILE);}
	
	
	//only let the player have some if they have fewer than four clips
	foreach(object thing in all_inventory(this_player()))
		{
		if(thing->GetKeyName() == ammo->GetKeyName())
			{
			numclips ++;
			if(numclips >= 4)
				{
				eventForce(""speak in common Hmm, looks like you already have a few there."");			
				return;
				}
			}
		}	
	
	//uh-oh, can't create the object, better let the player know.
	if(!ammo || !objectp(ammo))
		{
		eventForce(""speak in common Oh! I can't seem to find anymore ammo! You'd better let an admin know."");
		return;
		}
	eventForce(""speak in common Ah, there you are chap!"");
	tell_object(this_player(),""Schrody hands you "" + ammo->GetShort() + ""."");
	ammo->eventMove(this_player());	
	}

void GiveGun()
	{
	object gun;
	
	//make sure we don't have one lying around somewhere
	if(gun = present(""pistol"",environment(this_object())) )
		{
		eventForce(""speak in common There is one right over there. Why don't you pick it up with \'get "" + gun->GetKeyName() + ""\'."");
		return;
		}
			
	//make sure the player isn't being tricky and trying to hide one
	//on poor Schrody...
	gun = present(""pistol"",this_object());
	
	//looks like we can safely try to clone a new one
	if(!gun && !objectp(gun))	{gun = new(GUN_FILE);}
	
	//make sure players doesn't have one already
	foreach(object thing in all_inventory(this_player()))
		{
		if(thing->GetKeyName() == gun->GetKeyName())
			{
			eventForce(""speak in common Hmm, looks like you already have one there."");			
			return;
			}
		}
		
	//uh-oh, can't create the object, better let the player know.
	if(!gun || !objectp(gun))
		{
		eventForce(""speak in common Errrrm whoops! I seem to have misplaced all my guns! You'd better let an admin know."");
		return;
		}
	eventForce(""speak in common Right right, here we go!"");
	tell_object(this_player(),""Schrody slips you "" + gun->GetShort() + ""."");
	gun->eventMove(this_player());	
	}

varargs int eventReceiveAttack(int speed, string def, object agent)
	{
	this_object()->eventForce(""speak in common *Tsk tsk*"");
	return 0;
	}
varargs int eventReceiveDamage(object agent, int type, int x, int internal, mixed limbs)
	{
	return 0;
	}
	
static void create() {
    sentient::create();
    SetKeyName(""Schrodinger"");
    
    //let's try a funny comment here for the parser ;)
    SetId(({""schrodinger"",""shrodinger"",""shcrodinger"",""schroddy"",""schrody"",""shrody"",""shroddy"",""shcrody"",""shcroddy"",""guide"",""calico zuraki""}));
    SetAdjectives(({""non-player"", ""non player""}));
    SetShort(""a calico zuraki."");
    SetLong(""A charming and unusually calico-colored Zuraki with glasses sitting at the tip of his muzzle."");
    SetRace(""zuraki"");
    SetClass(""vitalnpc"");
    SetGender(""male"");
    SetLevel(60);    
    SetMelee(0);
    SetPacifist(1);
    
    SetNativeLanguage(""Common"");
        
    SetTalkResponses( ([		
		""firearm"" 	: (:TalkAbout,""firearm"":),
		""ammotype""  : (:TalkAbout,""ammotype"":),
		""ammo type"" : (:TalkAbout,""ammotype"":),
		""clip""		: (:TalkAbout,""magazine"":),
		""magazine"" 	: (:TalkAbout,""magazine"":),
		""round"" 	: (:TalkAbout,""round"":),
		""combat"" 	: (:TalkAbout,""combat"":),
		""scroung"" 	: (:TalkAbout,""scrounge"":),
		""firemode"" 	: (:TalkAbout,""firemode"":),
		""poop"" : ""Oh! What is wrong with you!"",
		""sex"" : ""I don't even want to hear about it!"",
		""butt"" : ""I like big butts and I cannot lie."",
		""cheese"" : ""Cheese! Where? May I have some?"",
		""drug"" : ""Drugs are bad, m'kay?"",
		""more ammo"" : (: GiveAmmo :),
		""need ammo"" : (: GiveAmmo :),
		""need a gun"" : (: GiveGun :),
		""lick"" : (:TalkAbout,""licking"":),
		]) );
	
	SetEmoteResponses( ([""lick"":""Whoa there tony-sir-licks-alot. Must you lick everything? Yeech!""]) );
	SetAction(3,({""Schrody jumps up and down with glee."",""Schrody wags both of his tails."",""Schrody adjusts his glasses."",""!speak in common Do you need any ammo or weapons? Just %^GREEN%^say%^CYAN%^ so if you do.""}));	
}
void init(){
    ::init();
    
}
";			return c;
			}//end of BigCode Function
		
		public static string ReadFile(string fileName)
			{
			string str;
			using(StreamReader file = File.OpenText(fileName))
				{
				str = file.ReadToEnd();
				}
			
			
			return str; 
			}
		}
	}
