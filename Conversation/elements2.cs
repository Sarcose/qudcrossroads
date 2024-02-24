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
namespace QudCrossroads.Dialogue
{
    public static partial class ElementsTwo
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
                                    { "strangeGreet",       new List<string>() { "What do you want?" } },
                                    { "friendGreet",        new List<string>() { "Hey." } },
                                }
                            }
                        }
                    },
                }
            },
            { //#CULTURES
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
            {   //#PROFESSION
                "Profession", new Dictionary<string, object>
                {
                    {
                        "Farmer", new Dictionary<string, object>
                        {
                            {
                                "watervineFarmer", new Dictionary<string, object>
                                {
                                    { "proverb", new List<string> { "do you see these sheafs, wayfarer? They are our Way.", "Salt and Sun" } }
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
        static T GetEntry<T>(Dictionary<string, object> dict, string address)
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
                    Console.WriteLine($"Address part '{part}' not found.");
                    return default;
                }
            }

            return (T)current;
        }
    }
}