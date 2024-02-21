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
            {   //#PERSONALITIES
                "Personalities", new Dictionary<string, object>
                {
                    {
                        "_TEMPLATE", new Dictionary<string, object>     //copy/paste and leave blank if the category doesn't have one
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
                        "_TEMPLATE", new Dictionary<string, object>     //copy/paste and leave blank if the category doesn't have one
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
                        "_TEMPLATE", new Dictionary<string, object>     //copy/paste and leave blank if the category doesn't have one
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
                        "_TEMPLATE", new Dictionary<string, object>     //copy/paste and leave blank if the category doesn't have one
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
                        "_TEMPLATE", new Dictionary<string, object>     //copy/paste and leave blank if the category doesn't have one
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
                        "_TEMPLATE", new Dictionary<string, object>     //copy/paste and leave blank if the category doesn't have one
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
                        "_TEMPLATE", new Dictionary<string, object>     //copy/paste and leave blank if the category doesn't have one
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
                        "_TEMPLATE", new Dictionary<string, object>     //copy/paste and leave blank if the category doesn't have one
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
                        "_TEMPLATE", new Dictionary<string, object>     //copy/paste and leave blank if the category doesn't have one
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
                        "_TEMPLATE", new Dictionary<string, object>     //copy/paste and leave blank if the category doesn't have one
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
                        "_TEMPLATE", new Dictionary<string, object>     //copy/paste and leave blank if the category doesn't have one
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
                        "_TEMPLATE", new Dictionary<string, object>     //copy/paste and leave blank if the category doesn't have one
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
                "Cultures", new Dictionary<string, object>
                {
                    {
                        "SaltMarshCulture", new Dictionary<string, object>
                        {
                            {
                                "Unfamiliar", new Dictionary<string, object>
                                {
                                    { "greetings", new List<string> { "smugreeting1", "smugreeting2" } },
                                    { "titles", new List<string> { "smutitle1", "smutitle2" } }
                                }
                            },
                            {
                                "Familiar", new Dictionary<string, object>
                                {
                                    { "greetings", new List<string> { "smfgreeting1", "smfgreeting2" } },
                                    { "titles", new List<string> { "smftitle1", "smftitle2" } }
                                }
                            }
                        }
                    },
                    {
                        "DesertCulture", new Dictionary<string, object>
                        {
                            {
                                "Unfamiliar", new Dictionary<string, object>
                                {
                                    { "greetings", new List<string> { "dugreeting1", "dugreeting2" } },
                                    { "titles", new List<string> { "dutitle1", "dutitle2" } }
                                }
                            },
                            {
                                "Familiar", new Dictionary<string, object>
                                {
                                    { "greetings", new List<string> { "dfgreeting1", "dfgreeting2" } },
                                    { "titles", new List<string> { "dftitle1", "dftitle2" } }
                                }
                            }
                        }
                    }
                }
            },
            {
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