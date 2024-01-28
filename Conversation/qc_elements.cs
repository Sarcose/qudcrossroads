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

namespace QudCrossroads.Dialogue
{
    public static partial class Elements
    {
        public static string[] testArray = { 
            "mahaha", 
            "hehehe", 
            "hoohoohoo" 
        };
        public static string[] transitionFrom = {"At any rate,"};
        public static string[] transitionTo = {"I have a tale to tell."};
        
        //Dictionary<string, string>[] matrix = new Dictionary<string, string>[] 
        //        {
        //            new[],
        //            new[],
        //        };                                    
        

       // public class CulturePhrases {
       //     public string[] greeting {get; set;}
       //     public string[] 
       // }
            public class CultureConversation
            {
                public class Greetings
                {
                    public Dictionary<string, List<string>> Familiarities { get; set; } = new Dictionary<string, List<string>>();
                }

                public class Titles
                {
                    public Dictionary<string, List<string>> Familiarities { get; set; } = new Dictionary<string, List<string>>();
                }

                public Dictionary<string, Greetings> Greet { get; set; } = new Dictionary<string, Greetings>();
                public Dictionary<string, Titles> Title { get; set; } = new Dictionary<string, Titles>();
            }

            public static class AllCultures
            {
                public static Dictionary<string, CultureConversation> Cultures { get; set; } = new Dictionary<string, CultureConversation>();

                static AllCultures()
                {
                    Cultures["SaltMarshCulture"] = new CultureConversation
                    {
                        Greet = new Dictionary<string, CultureConversation.Greetings>
                        {
                            {
                                "keys", new CultureConversation.Greetings
                                {
                                    Familiarities = new Dictionary<string, List<string>>
                                    {
                                        { "unfamiliar", new List<string> { "Salt and sun", "Good meet to you.", "Bountiful harvest" } },
                                        { "familiar", new List<string> { "Salt and sun", "How are you on this day?", "How fare you?" } },
                                        { "friendly", new List<string> { "Hail", "Merry meet", "Bright tidings" } },
                                        { "unfriendly", new List<string> { "What", "What could you want", "Will this take long" } }
                                    }
                                }
                            }
                        },

                        Title = new Dictionary<string, CultureConversation.Titles>
                        {
                            {
                                "keys", new CultureConversation.Titles
                                {
                                    Familiarities = new Dictionary<string, List<string>>
                                    {
                                        { "unfamiliar", new List<string> { "wayfarer", "traveler", "stranger" } },
                                        { "familiar", new List<string> { "wayfarer", "fellow" } },
                                        { "friendly", new List<string> { "my friend", "my sibling", "my fellow", "dear sib" } },
                                        { "unfriendly", new List<string> { "you jerk", "you troublemaker" } }
                                    }
                                }
                            }
                        }
                    };
                }
            }
            //AllCultures allCultures = new AllCultures();
/*
                string whichCulture = "keys";
                string convElement = "Title";
                string whichFamiliarity = "unfriendly";

                List<string> stringList = allCultures.Cultures[whichCulture][convElement].Familiarities[whichFamiliarity]
                string specificString = stringList[1];
*/





        //      string whichCulture = "SaltMarshCulture";
        //      string whichFamiliarity = "unfriendly";
        //      string whichTitle = "unfamiliar";

        //      string specificString = allCultures.Cultures[whichCulture][convElement][whichFamiliarity].Familiarities[whichTitle][1];



        /*Example: Pull a specific string from a specific culture using arbitrary references
        string whichCulture = "SaltMarshCulture";
        string whichFamiliarity = "unfamiliar";
        string convElement = "Greet";

        // Accessing Greet or Title arbitrarily based on convElement
        var whichStringTable = allCultures.Cultures[whichCulture][convElement][whichFamiliarity].Familiarities;

        // Derive a specificString from whichStringTable
        string specificString = whichStringTable[whichFamiliarity][QRand.next(0,whichStringTable.Count)];
        */

    }
}

