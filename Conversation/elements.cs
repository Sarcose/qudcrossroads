using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

using XRL;
using XRL.Core;
using XRL.Rules;
using XRL.World;

using static QudCrossroads.Dialogue.Builders;
//  -   this compiles! Thank fucking god. We can move 
//  -   convert elements to this structure, wholesale.
//  -   convert parser to use dots for parsing
//  -   figure out other stuff? I guess?
//  -   need two monitors for this nonsense.
//  -   TODO: Emotes have been removed from first implementation. Emotes will later be brought back generated
//  -   TODO: Comedy is being left in but may not be used in this form.
//  -   CHANGE: "choose from these" things like hobbies, favorites etc will be preceded by a &
//  -   TODO: Might not use familiarity after all! Might use , Insult/Pleasantry/Compliment SnubGreet/StrangeGreet/WarmGreet, SnubTitle/StrangeTitle/WarmTitle
//  -                   ^ insert the above into _TEMPLATE and then apply it to all, including personalities and jobs. All will have the same!
//  -                   ^ [pleasantry], [greet], and [titlel] will use functions to replace with the above based on familiarity!

/* grammar reference

=pronouns.

reflexive=               |   himself
substantivePossessive=   |   his
subjective=              |   he
objective=               |   him
indicativeDistal=        |   that

*/

namespace QudCrossroads.Dialogue
{
    public static partial class Elements
    {
        public static Dictionary<string, object> Conversations = new Dictionary<string, object>
        {   
            {   //#TEMPLATE
                "_TEMPLATE", new Dictionary<string, object>
                {
                    {
                        "__TEMPLATE", new Dictionary<string, object>     //copy/paste and leave blank if the category doesn't have one
                        {   
                            //[pleasantry]
                            { "insult",         new List<string>() {  } },
                            { "pleasantry",     new List<string>() {  } },
                            { "compliment",     new List<string>() {  } },
                            //[greet]
                            { "snubGreet",      new List<string>() {  } },
                            { "strangeGreet",   new List<string>() {  } },
                            { "friendGreet",    new List<string>() {  } },
                            //[title]
                            { "snubTitle",      new List<string>() {  } },
                            { "strangeTitle",   new List<string>() {  } },
                            { "friendTitle",    new List<string>() {  } },
                            //non-opinion based
                            { "intro",          new List<string>() {  } },
                            { "transition",     new List<string>() {  } },
                            { "toQuest",        new List<string>() {  } },
                            { "saying",         new List<string>() {  } },
                            { "proverb",        new List<string>() {  } },
                            { "flavor",         new List<string>() {  } },
                            { "comedy",         new List<string>() {  } },
                            { "serious",        new List<string>() {  } },
                            { "&hobbies",       new List<string>() {  } }
                        }
                    },
                }
            },
            {   
                //#PERSONALITIES
                "Personalities", new Dictionary<string, object>
                {
                    {
                        "__TEMPLATE", new Dictionary<string, object>     //copy/paste and leave blank if the category doesn't have one
                        {   
                            //[pleasantry]
                            { "insult",         new List<string>() {  } },
                            { "pleasantry",     new List<string>() {  } },
                            { "compliment",     new List<string>() {  } },
                            //[greet]
                            { "snubGreet",      new List<string>() {  } },
                            { "strangeGreet",   new List<string>() {  } },
                            { "friendGreet",    new List<string>() {  } },
                            //[title]
                            { "snubTitle",      new List<string>() {  } },
                            { "strangeTitle",   new List<string>() {  } },
                            { "friendTitle",    new List<string>() {  } },
                            //non-opinion based
                            { "intro",          new List<string>() {  } },
                            { "transition",     new List<string>() {  } },
                            { "toQuest",        new List<string>() {  } },
                            { "saying",         new List<string>() {  } },
                            { "proverb",        new List<string>() {  } },
                            { "flavor",         new List<string>() {  } },
                            { "comedy",         new List<string>() {  } },
                            { "serious",        new List<string>() {  } },
                            { "&hobbies",       new List<string>() {  } }
                        }
                    },
                    {
                        "Generic", new Dictionary<string, object>     //copy/paste and leave blank if the category doesn't have one
                        {   
                            //[pleasantry]
                            { "insult",         new List<string>() {  } },
                            { "pleasantry",     new List<string>() {  } },
                            { "compliment",     new List<string>() {  } },
                            //[greet]
                            { "snubGreet",      new List<string>() {  } },
                            { "strangeGreet",   new List<string>() {  } },
                            { "friendGreet",    new List<string>() {  } },
                            //[title]
                            { "snubTitle",      new List<string>() {  } },
                            { "strangeTitle",   new List<string>() {  } },
                            { "friendTitle",    new List<string>() {  } },
                            //non-opinion based
                            { "intro",          new List<string>() {  } },
                            { "transition",     new List<string>() { "Anyway", "Regardless","Whatever the case","At any rate","Nonetheless","You know" } },
                            { "toQuest",        new List<string>() { "I have a tale to tell.","Wait til you hear this.","Now that I have your attention...","I've been thinking about something, lately.","Let me tell you a story.","I'm glad you asked.","" } },
                            { "saying",         new List<string>() { "hmmmm","the sun always sets","by the salt sun","by the Eaters","at any cost","traveler","for the birds","*the =localfaction= know the way","*=randomcreature= guide us","*=MARKOVWORD=" } },
                            { "proverb",        new List<string>() {  } },
                            { "flavor",         new List<string>() {  } },
                            { "comedy",         new List<string>() {  } },
                            { "serious",        new List<string>() {  } },
                            { "&hobbies",       new List<string>() {  } }
                        }
                    },
                    {
                        "Elder", new Dictionary<string, object>     //copy/paste and leave blank if the category doesn't have one
                        {   
                            //[pleasantry]
                            { "insult",         new List<string>() {  } },
                            { "pleasantry",     new List<string>() {  } },
                            { "compliment",     new List<string>() {  } },
                            //[greet]
                            { "snubGreet",      new List<string>() {  } },
                            { "strangeGreet",   new List<string>() {  } },
                            { "friendGreet",    new List<string>() {  } },
                            //[title]
                            { "snubTitle",      new List<string>() {  } },
                            { "strangeTitle",   new List<string>() {  } },
                            { "friendTitle",    new List<string>() {  } },
                            //non-opinion based
                            { "intro",          new List<string>() {  } },
                            { "transition",     new List<string>() {  } },
                            { "toQuest",        new List<string>() {  } },
                            { "saying",         new List<string>() {  } },
                            { "proverb",        new List<string>() {  } },
                            { "flavor",         new List<string>() {  } },
                            { "comedy",         new List<string>() {  } },
                            { "serious",        new List<string>() {  } },
                            { "&hobbies",       new List<string>() {  } }
                        }
                    },
                    {
                        "Lazy", new Dictionary<string, object>     //copy/paste and leave blank if the category doesn't have one
                        {   
                            //[pleasantry]
                            { "insult",         new List<string>() {  } },
                            { "pleasantry",     new List<string>() {  } },
                            { "compliment",     new List<string>() {  } },
                            //[greet]
                            { "snubGreet",      new List<string>() { "*blinks slowly*" } },
                            { "strangeGreet",   new List<string>() { "Oh! I didn't see you there!" } },
                            { "friendGreet",    new List<string>() { "*yawn* how's it goin, friend?" } },
                            //[title]
                            { "snubTitle",      new List<string>() {  } },
                            { "strangeTitle",   new List<string>() {  } },
                            { "friendTitle",    new List<string>() {  } },
                            //non-opinion based
                            { "intro",          new List<string>() {  } },
                            { "transition",     new List<string>() {  } },
                            { "toQuest",        new List<string>() {  } },
                            { "saying",         new List<string>() {  } },
                            { "proverb",        new List<string>() {  } },
                            { "flavor",         new List<string>() {  } },
                            { "comedy",         new List<string>() {  } },
                            { "serious",        new List<string>() {  } },
                            { "&hobbies",       new List<string>() {  } }
                        }
                    },
                    {
                        "Snooty", new Dictionary<string, object>     //copy/paste and leave blank if the category doesn't have one
                        {   
                            //[pleasantry]
                            { "insult",         new List<string>() {  } },
                            { "pleasantry",     new List<string>() {  } },
                            { "compliment",     new List<string>() {  } },
                            //[greet]
                            { "snubGreet",      new List<string>() {  } },
                            { "strangeGreet",   new List<string>() {  } },
                            { "friendGreet",    new List<string>() {  } },
                            //[title]
                            { "snubTitle",      new List<string>() {  } },
                            { "strangeTitle",   new List<string>() {  } },
                            { "friendTitle",    new List<string>() {  } },
                            //non-opinion based
                            { "intro",          new List<string>() {  } },
                            { "transition",     new List<string>() {  } },
                            { "toQuest",        new List<string>() {  } },
                            { "saying",         new List<string>() {  } },
                            { "proverb",        new List<string>() {  } },
                            { "flavor",         new List<string>() {  } },
                            { "comedy",         new List<string>() {  } },
                            { "serious",        new List<string>() {  } },
                            { "&hobbies",       new List<string>() {  } }
                        }
                    },
                    {
                        "Peppy", new Dictionary<string, object>     //copy/paste and leave blank if the category doesn't have one
                        {   
                            //[pleasantry]
                            { "insult",         new List<string>() {  } },
                            { "pleasantry",     new List<string>() {  } },
                            { "compliment",     new List<string>() {  } },
                            //[greet]
                            { "snubGreet",      new List<string>() { "...well hi, SO nice to see you!" } },
                            { "strangeGreet",   new List<string>() { "Oh! Hi!" } },
                            { "friendGreet",    new List<string>() { "Beautiful day, isn't it?" } },
                            //[title]
                            { "snubTitle",      new List<string>() {  } },
                            { "strangeTitle",   new List<string>() {  } },
                            { "friendTitle",    new List<string>() {  } },
                            //non-opinion based
                            { "intro",          new List<string>() {  } },
                            { "transition",     new List<string>() {  } },
                            { "toQuest",        new List<string>() {  } },
                            { "saying",         new List<string>() {  } },
                            { "proverb",        new List<string>() {  } },
                            { "flavor",         new List<string>() {  } },
                            { "comedy",         new List<string>() {  } },
                            { "serious",        new List<string>() {  } },
                            { "&hobbies",       new List<string>() {  } }
                        }
                    },
                    {
                        "Macho", new Dictionary<string, object>     //formerly WARRIOR, meant to emulate AC's "Jock"
                        {   
                            //[pleasantry]
                            { "insult",         new List<string>() {  } },
                            { "pleasantry",     new List<string>() {  } },
                            { "compliment",     new List<string>() {  } },
                            //[greet]
                            { "snubGreet",      new List<string>() {  } },
                            { "strangeGreet",   new List<string>() {  } },
                            { "friendGreet",    new List<string>() {  } },
                            //[title]
                            { "snubTitle",      new List<string>() {  } },
                            { "strangeTitle",   new List<string>() {  } },
                            { "friendTitle",    new List<string>() {  } },
                            //non-opinion based
                            { "intro",          new List<string>() {  } },
                            { "transition",     new List<string>() {  } },
                            { "toQuest",        new List<string>() {  } },
                            { "saying",         new List<string>() {  } },
                            { "proverb",        new List<string>() {  } },
                            { "flavor",         new List<string>() {  } },
                            { "comedy",         new List<string>() {  } },
                            { "serious",        new List<string>() {  } },
                            { "&hobbies",       new List<string>() {  } }
                        }
                    },
                    {
                        "Studious", new Dictionary<string, object>     //  Barathrumites
                        {   
                            //[pleasantry]
                            { "insult",         new List<string>() {  } },
                            { "pleasantry",     new List<string>() {  } },
                            { "compliment",     new List<string>() {  } },
                            //[greet]
                            { "snubGreet",      new List<string>() {  } },
                            { "strangeGreet",   new List<string>() {  } },
                            { "friendGreet",    new List<string>() {  } },
                            //[title]
                            { "snubTitle",      new List<string>() {  } },
                            { "strangeTitle",   new List<string>() {  } },
                            { "friendTitle",    new List<string>() {  } },
                            //non-opinion based
                            { "intro",          new List<string>() {  } },
                            { "transition",     new List<string>() {  } },
                            { "toQuest",        new List<string>() {  } },
                            { "saying",         new List<string>() {  } },
                            { "proverb",        new List<string>() {  } },
                            { "flavor",         new List<string>() {  } },
                            { "comedy",         new List<string>() {  } },
                            { "serious",        new List<string>() {  } },
                            { "&hobbies",       new List<string>() {  } }
                        }
                    },
                    {
                        "Downer", new Dictionary<string, object>     //     Eeyore
                        {   
                            //[pleasantry]
                            { "insult",         new List<string>() {  } },
                            { "pleasantry",     new List<string>() {  } },
                            { "compliment",     new List<string>() {  } },
                            //[greet]
                            { "snubGreet",      new List<string>() {  } },
                            { "strangeGreet",   new List<string>() {  } },
                            { "friendGreet",    new List<string>() {  } },
                            //[title]
                            { "snubTitle",      new List<string>() {  } },
                            { "strangeTitle",   new List<string>() {  } },
                            { "friendTitle",    new List<string>() {  } },
                            //non-opinion based
                            { "intro",          new List<string>() {  } },
                            { "transition",     new List<string>() {  } },
                            { "toQuest",        new List<string>() {  } },
                            { "saying",         new List<string>() {  } },
                            { "proverb",        new List<string>() {  } },
                            { "flavor",         new List<string>() {  } },
                            { "comedy",         new List<string>() {  } },
                            { "serious",        new List<string>() {  } },
                            { "&hobbies",       new List<string>() {  } }
                        }
                    },
                    {
                        "Cranky", new Dictionary<string, object>     //     Literally Cranky Kong
                        {   
                            //[pleasantry]
                            { "insult",         new List<string>() {  } },
                            { "pleasantry",     new List<string>() {  } },
                            { "compliment",     new List<string>() {  } },
                            //[greet]
                            { "snubGreet",      new List<string>() {  } },
                            { "strangeGreet",   new List<string>() {  } },
                            { "friendGreet",    new List<string>() {  } },
                            //[title]
                            { "snubTitle",      new List<string>() {  } },
                            { "strangeTitle",   new List<string>() {  } },
                            { "friendTitle",    new List<string>() {  } },
                            //non-opinion based
                            { "intro",          new List<string>() {  } },
                            { "transition",     new List<string>() {  } },
                            { "toQuest",        new List<string>() {  } },
                            { "saying",         new List<string>() {  } },
                            { "proverb",        new List<string>() {  } },
                            { "flavor",         new List<string>() {  } },
                            { "comedy",         new List<string>() {  } },
                            { "serious",        new List<string>() {  } },
                            { "&hobbies",       new List<string>() {  } }
                        }
                    },
                }
            },   
            {   //#MORPHOTYPE
                "Morphotype", new Dictionary<string, object>
                {
                    {
                        "TrueKin", new Dictionary<string, object>   //not sure what to do with these yet tbh
                        {
                            {
                                "Syzgrizor", new Dictionary<string, object> //i don't like this precedent as it leads to "why don't i just implement every caste and calling?"
                                {
                                    { "&hobbies",       new List<string>() {  } }
                                }
                            }
                        }
                    },
                    {
                        "Esper", new Dictionary<string, object> //not confined to esper morphotypes, but for mental-focused mutants in general
                        {
                            {
                                "StarEyed", new Dictionary<string, object>
                                {
                                    { "strangeGreet",       new List<string>() { "...oh? Have we met...?" } },
                                    { "friendGreet",        new List<string>() { "Oh, look! A friend!" } },
                                }
                            },
                            {
                                "RealityFolder", new Dictionary<string, object>
                                {
                                    { "strangeGreet",       new List<string>() { "...which timeline are you from?" } },
                                    { "friendGreet",        new List<string>() { "Ah, friend! I hope you don't vanish." } },
                                }
                            },
                        }
                    },
                    {
                        "Chimera", new Dictionary<string, object>   //not confined to chimera morphotypes, but for physical-focused mutants in general
                        {
                            {
                                "HideousSpecimen", new Dictionary<string, object>
                                {
                                    { "strangeGreet",       new List<string>() { "*vague wet noises*" } },
                                    { "friendGreet",        new List<string>() { "*a voice comes from =pronouns.substantivePossessive= foot*" } },
                                }
                            },
                            {
                                "SadMutant", new Dictionary<string, object>
                                {
                                    { "strangeGreet",       new List<string>() { "...have you come to mock me?" } },
                                    { "friendGreet",        new List<string>() { "I'm... glad you came." } },
                                }
                            },
                            {
                                "Plantlike", new Dictionary<string, object>
                                {
                                    { "strangeGreet",       new List<string>() { "*=pronouns.substantivePossessive= leafy visage turns slowly toward you*" } },
                                    { "friendGreet",        new List<string>() { "*rustles gently*" } },
                                }
                            },
                        }
                    },
                    {
                        "Mutations", new Dictionary<string, object> //for creatures that have specific mutaitons which could add flavor to interactions
                        {
                            {   //todo: get actual mutation names. Only use certain ones for this concept
                                "SociallyRepugnant", new Dictionary<string, object>
                                {
                                    { "strangeGreet",       new List<string>() { "What do you want?" } },
                                    { "friendGreet",        new List<string>() { "Hey." } },
                                }
                            },
                            {
                                "Two-headed", new Dictionary<string, object>
                                {
                                    { "strangeGreet",       new List<string>() { "Hi. Hi!" } },
                                    { "friendGreet",        new List<string>() { "Hey friend! Friend, hi!" } },
                                }
                            }
                        }
                    },
                }
            },
            { //#CULTURES
            // { "emoteintro",  new List<string> {"=pronouns.possessive= back bent to you, =pronouns.subjective= is hard at work. A moment passes, and =pronouns.subjective= stands",""}},
            // { "emotetransition", new List<string> {"=pronouns.posessive= kneels back down, biding you kneel with =pronouns.objective=. Together the two of you speak between the whispers of watervine in the gentle breeze."}},
                "Cultures", new Dictionary<string, object>
                {
                    {
                        "SaltMarshCulture", new Dictionary<string, object>     //copy/paste and leave blank if the category doesn't have one
                        {   
                            //[pleasantry]
                            { "insult",         new List<string>() {  } },
                            { "pleasantry",     new List<string>() {  } },
                            { "compliment",     new List<string>() {  } },
                            //[greet]
                            { "snubGreet",      new List<string>() { "What", "What could you want", "Will this take long" } },
                            { "strangeGreet",   new List<string>() { "Salt and sun", "Good meet to you", "Bountiful harvest","Water and fortune to you",  "How are you on this day?", "How fare you?" } },
                            { "friendGreet",    new List<string>() { "Hail", "Merry meet", "Bright tidings" } },
                            //[title]
                            { "snubTitle",      new List<string>() { "blighted", "troublemaker", "polluter" } },
                            { "strangeTitle",   new List<string>() { "wayfarer", "traveler", "stranger"  } },
                            { "friendTitle",    new List<string>() { "my friend", "my sibling", "my fellow", "dear sib" } },
                            //non-opinion based
                            { "intro",          new List<string>() {  } },
                            { "transition",     new List<string>() { "But ah, what am I to complain?" } },
                            { "toQuest",        new List<string>() {  } },
                            { "saying",         new List<string>() {  } },
                            { "proverb",        new List<string>() { "so it is, that the Salt Sun rises and the Salt Sun sets"  } },
                            { "flavor",         new List<string>() {  } },
                            { "comedy",         new List<string>() {  } },
                            { "serious",        new List<string>() {  } },
                            { "&hobbies",       new List<string>() {  } }
                        }
                    },
                }
            },
            {   //#PROFESSION
            // { "emoteintro",  new List<string> {"=pronouns.possessive= back bent to you, =pronouns.subjective= is hard at work. A moment passes, and =pronouns.subjective= stands",""}},
            //{ "emoteintro", new List<string> {"=pronouns.possessive= back bent to you, =pronouns= is hard at work. A moment passes, and =pronoun= stands",""}},
            // { "emotetransition", new List<string> {"=pronouns.posessive= kneels back down, biding you kneel with =pronouns.objective=. Together the two of you speak between the whispers of watervine in the gentle breeze."}},
              
                "Profession", new Dictionary<string, object>
                {
                    {
                        "Farmer", new Dictionary<string, object>
                        {
                            {
                                "Generic", new Dictionary<string, object>
                                {
                                    { "transition",     new List<string>() { "But ah, what am I to complain?" } },
                                    { "flavor", new List<string> { "To ply |resource.farmer| from the |biome| is a |harshadjective| |laborverb|" } },
                                    { "proverb", new List<string> { "so it is, that the Salt Sun rises and the Salt Sun sets", "Salt and Sun"} }
                                }
                            },
                            {
                                "watervineFarmer", new Dictionary<string, object>
                                {
                                    { "flavor", new List<string> { "Do you see how thin these wafers are, unfertilized?", "flavor2" } },
                                    { "transition",     new List<string>() { "But ah, what am I to complain?" } },
                                    { "proverb", new List<string> { "do you see these sheafs, |title|? They are our Way.",  "we work the moisture from the vines"} }
                                }
                            },
                            {
                                "starappleFarmer", new Dictionary<string, object>
                                {
                                    { "proverb", new List<string> { "the starapple falls not far from the tree", "one bad starapple..." } }
                                }
                            }
                        }
                    },
                    {
                        "Warrior", new Dictionary<string, object>
                        {
                            {
                                "warden", new Dictionary<string, object>
                                {
                                    { "proverb", new List<string> { "tread lightly...", "look alive." } }
                                }
                            },
                            {
                                "guard", new Dictionary<string, object>
                                {
                                    { "proverb", new List<string> { "Keep your wits about you.", "Keep the sword sharp." } }
                                }
                            }
                        }
                    },
                    {
                        "Merchant", new Dictionary<string, object>
                        {
                            {
                                "tinker", new Dictionary<string, object>
                                {
                                    { "proverb", new List<string>() }
                                }
                            },
                            {
                                "jeweller", new Dictionary<string, object>
                                {
                                    { "proverb", new List<string>() }
                                }
                            }
                        }
                    }
                }
            }
        };

        // Function to get a nested entry from the dictionary
        //                                                             in reality these addresses will be put into an 'address' variable

        //List<string> cultureList = GetEntry<List<string>>(Conversations, "Cultures."+culture+"."+element);
        //List<string> genericList = GetEntry<List<string>>(Conversations, "Personalities.Generic."+"."+element);
        //List<string> personalityList = GetEntry<List<string>>(Conversations, "Personalities."+personality+"."+element);
        //List<string> subPersonalityList = GetEntry<List<string>>(Conversations, "Personalities."+subPersonality+"."+element);
        //List<string> professionList = GetEntry<List<string>>(Conversations, "Professions."+profession+"."+"Generic"+"."+element);
        //List<string> jobList = GetEntry<List<string>>(Conversations, "Professions."+profession+"."+job+"."+element);
        //List<string> morphoList = GetEntry<List<string>>(Conversations, "Morphotypes."+morphotype+"."+subMorpho+"."+element);
        
        //List<string> mutationList = GetEntry<List<string>>(Conversations, "Morphotypes."+"Mutations"+"."+getMutation()+"."+element);

        //List<string> elementList = GetEntry<List<string>>(Conversations, elementAddress)
        static List<string> GetEntry(Dictionary<string, object> dict, string address)
        {
            string[] parts = address.Split('.');
            object current = dict;

            foreach (string part in parts)
            {
                if (current is Dictionary<string, object> currentDict && currentDict.ContainsKey(part))
                {
                    current = currentDict[part];
                }
                else
                {
                    return new List<string>(); // Return an empty list if any part is not found
                }
            }

            return current as List<string> ?? new List<string>(); // Return the list or an empty list if the final part is not a list
        }
    }
}