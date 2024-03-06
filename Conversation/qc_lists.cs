using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using XRL;
using XRL.Language;
using static XRL.Language.Grammar;
using XRL.World;
using XRL.World.Conversations;
using XRL.World.Parts;
using XRL.Messages;

using QudCrossroads;
using QRand = QudCrossroads.Utilities.QudCrossroads_Random;
using static QudCrossroads.Dialogue.Elements;
using static QudCrossroads.Dialogue.Functions;
using static QudCrossroads.Dialogue.Builders;
//TODO: reduce my using directives
namespace QudCrossroads.Dialogue
{
    public static partial class QC_Lists
    {
        public static Dictionary<string, object> CrossroadsLVR = new Dictionary<string, object>
        {   //NOTE many of the adjectives are contextually appropriate for humans but there are monster situations these wouldn't work for -- a whole new set is needed for monsters
            { "Resource.Farmer",            new List<string> { "|picktwo|","food","shelter" } },
            { "Resource.Watervinefarmer",   new List<string> { "|picktwo|", "watervine", "water", "wafers" } },                        
            { "LaborVerb",                  new List<string> { "labor", "task", "trade", "calling" } },                        
            { "harshAdjective",             new List<string> { "|picktwo|", "harsh", "hard", "tough", "trying", "tiresome" } },
            { "meanAdjective",              new List<string> { "|harshAdjective|", "ugly", "wet", "gross", "annoying", "unseemly","unsightly" } },
            { "genNegObject",               new List<string> { "a knollworm's beak", "a slime's tentacle", "a slugsnout's scood", "a salt kraken's leavings", "Svardym slime", "salt in my eyes" } },
            { "exclaim",                    new List<string> { "By the sultans","Fie","Egads","Aye","Avaunt","Woe","Nay","Alack","Vexation","Curses","Ya Lhoos","Ya L'asif","Aib","Law Shwayya","Bous","Wayli" } },
            { "greet",                      new Func<Phrase, string, string>((phrase, key) => ParseFamiliarity(phrase, key)) },
            { "title",                      new Func<Phrase, string, string>((phrase, key) => ParseFamiliarity(phrase, key)) },
            { "pleasantry",                 new Func<Phrase, string, string>((phrase, key) => ParseFamiliarity(phrase, key)) },
            
            { "informalPersonTerm",         new Func<Phrase, string, string>((phrase, key) => ParseMiscGenderTerm(phrase, key)) }, //[ ] guy, gal, pal, person, something else for other pronouns 
            { "informalPersonTerm.subject", new Func<Phrase, string, string>((phrase, key) => ParseMiscGenderTerm(phrase, key)) }, //[ ] guy, gal, pal, person, something else for other pronouns 
            { "informalPersonTerm.speaker", new Func<Phrase, string, string>((phrase, key) => ParseMiscGenderTerm(phrase, key)) }, //[ ] guy, gal, pal, person, something else for other pronouns 

            { "today",                      new Func<Phrase, string, string>((phrase, key) => GetDate(phrase, key)) }, //[X] day 30th of Ubu Ut
            { "timeOfDay",                  new Func<Phrase, string, string>((phrase, key) => GetDate(phrase, key)) }, //[ ] morning, noon, night etc
            { "year",                       new Func<Phrase, string, string>((phrase, key) => GetDate(phrase, key)) }, //[X] year
            { "month",                      new Func<Phrase, string, string>((phrase, key) => GetDate(phrase, key)) }, //[X] month
            { "daysAgo",                    new Func<Phrase, string, string>((phrase, key) => GetDate(phrase, key)) }, //[ ] time ago
            { "monthsAgo",                  new Func<Phrase, string, string>((phrase, key) => GetDate(phrase, key)) }, //[ ] time ago
            { "yearsAgo",                   new Func<Phrase, string, string>((phrase, key) => GetDate(phrase, key)) }, //[ ] time ago

            //{ "contextDay",                 new Func<Phrase, string, string>((phrase, key) => GetContext(phrase, key)) }, //[ ] day


            { "equipment",                  new Func<Phrase, string, string>((phrase, key) => GetPart(phrase, key)) }, //[ ] equipment
            { "randEquipment",              new Func<Phrase, string, string>((phrase, key) => GetPart(phrase, key)) }, //[X] equipment
            { "randInventory",              new Func<Phrase, string, string>((phrase, key) => GetPart(phrase, key)) }, //[X] equipment
            { "randHeld",                   new Func<Phrase, string, string>((phrase, key) => GetPart(phrase, key)) }, //[X] equipment
            { "posEquip",                   new Func<Phrase, string, string>((phrase, key) => GetPart(phrase, key)) }, //[ ] posEquip
            { "negEquip",                   new Func<Phrase, string, string>((phrase, key) => GetPart(phrase, key)) }, //[ ] negEquip

            { "part",                       new Func<Phrase, string, string>((phrase, key) => GetPart(phrase, key)) }, //[ ] part
            { "posPart",                    new Func<Phrase, string, string>((phrase, key) => GetPart(phrase, key)) }, //[ ] posPart
            { "posPartAdj",                 new Func<Phrase, string, string>((phrase, key) => GetPart(phrase, key)) }, //[ ] posPartAdj -- use this to get a specific one from the below lists
            { "posMutAdj",                  new List<string> { "slimy","plump","muscular","shiny new","becoming","intriguing","fun" } },
            { "posTKAdj",                   new List<string> { "shiny","chrome","polished","fancy","expensive","refactored","organized" } },
            { "posSlotAdj",                 new List<string> { "becoming" } },
            { "negPart",                    new Func<Phrase, string, string>((phrase, key) => GetPart(phrase, key)) }, //[ ] negPart
            { "negPartAdj",                 new Func<Phrase, string, string>((phrase, key) => GetPart(phrase, key)) }, //[ ] negPartAdj -- use this to get a specific one from the below lists
            { "negMutAdj",                  new List<string> { "disjointed","stunted","unsightly","unseemly","nerve-wracking","disturbing","jittery","corrupted","stubby" } },
            { "negTKAdj",                   new List<string> { "rusty","filthy","bent-up","frayed","antique","dirty and old","dirty","old" } },
            { "negSlotAdj",                 new List<string> { "unsightly" } },
            
            { "hobbyPhrase",                new Func<Phrase, string, string>((phrase, key) => GetHobby(phrase, key)) }, //[ ] hobbyPhrase
            { "hobbyFumble",                new Func<Phrase, string, string>((phrase, key) => GetHobby(phrase, key)) }, //[ ] hobbyFumble
            { "hobby",                      new Func<Phrase, string, string>((phrase, key) => GetHobby(phrase, key)) }, //[ ] hobby
 
            //{ "questHint",                  new Func<Phrase, string, string>((phrase, key) => GetQuest(phrase, key)) }, //[ ] questHint
            //{ "questHerring",               new Func<Phrase, string, string>((phrase, key) => GetQuest(phrase, key)) }, //[ ] questHerring
            //{ "questConclusion",            new Func<Phrase, string, string>((phrase, key) => GetQuest(phrase, key)) }, //[ ] questConclusion
        };

        public static Dictionary<string, List<string>> CrossroadsCasualPronounAddress = new Dictionary<string, List<string>>
        {   //TODO: find the actual member of a given game object that gives us the gender and pronouns and use that term
            { "Neuter",                 new List<string> { "person" } },
            { "Nonspecific",            new List<string> { "person" } },
            { "Male",                   new List<string> { "guy","boy" } },
            { "Female",                 new List<string> { "gal","girl" } },
            { "Plural",                 new List<string> { "people", "folks", "peeps" } },
            { "Collective",             new List<string> { "mind", "thoughts" } },
            { "Males",                  new List<string> { "guys", "boys" } },
            { "Females",                new List<string> { "gals", "girls" } },
            { "HindrenMale",            new List<string> { "buck" } },
            { "HindrenFemale",          new List<string> { "doe" } },
            { "Hartind",                new List<string> { "hartind" } },
            { "it",                     new List<string> { "thing" } },
            { "they",                   new List<string> { "person" } },
            { "plural",                 new List<string> { "people" } },
            { "he",                     new List<string> { "guy", "boy" } },
            { "she",                    new List<string> { "gal","girl" } },
            { "ey",                     new List<string> { "kid", "child" } },
            { "sie",                    new List<string> { "gil" } },
            { "xe",                     new List<string> { "xerson" } },
            { "ze",                     new List<string> { "zerson" } },
        };

        //general moods:
            // simple
            // excited - angry probably the same as excited
            // sad / downer
            // confused
        public static Dictionary<string, List<string>> CrossroadsPunctuationByMood = new Dictionary<string, List<string>>
        {   //pseudo-weighted by adding multiple identical list members rather than using a weight logic because that's annoying
            {"simple",      new List<string> {".",".",";","!","!"} },
            {"excited",     new List<string> {"!"} },
            {"sad",         new List<string> {".",".","...",";"} },
            {"confused",    new List<string> {".","?","?","!?","@#!?"} },
        };
        public static string getPunct(Phrase phrase){
            string ret = "";
            if (phrase.mood == "random"){   //probably only used for testing but good to have
                int randMood = QRand.Next(0, 3);
                switch (randMood){
                    case 0:
                        ret = GetRandString(phrase, CrossroadsPunctuationByMood["simple"]);
                        break;
                    case 1:
                        ret = GetRandString(phrase, CrossroadsPunctuationByMood["excited"]);
                        break;
                    case 2:
                        ret = GetRandString(phrase, CrossroadsPunctuationByMood["sad"]);
                        break;
                    case 3:
                        ret = GetRandString(phrase, CrossroadsPunctuationByMood["confused"]);
                        break;
                    default:
                        ret = GetRandString(phrase, CrossroadsPunctuationByMood["simple"]);
                        break;
                }
            }
            else if (string.IsNullOrEmpty(phrase.mood)){
                    ret = GetRandString(phrase, CrossroadsPunctuationByMood["simple"]);
            }
            else {
                    ret = GetRandString(phrase, CrossroadsPunctuationByMood[phrase.mood]);

            }
            return ret + " ";
        }
    }
}




