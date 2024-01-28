using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Reflection;
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
                public Dictionary<string, List<string>> Familiarities { get; set; } = new Dictionary<string, List<string>>
                {
                    { "unfamiliar", new List<string>() },
                    { "familiar", new List<string>() },
                    { "friendly", new List<string>() },
                    { "unfriendly", new List<string>() }
                };
            }

            public class Titles
            {
                public Dictionary<string, List<string>> Familiarities { get; set; } = new Dictionary<string, List<string>>
                {
                    { "unfamiliar", new List<string>() },
                    { "familiar", new List<string>() },
                    { "friendly", new List<string>() },
                    { "unfriendly", new List<string>() }
                };
            }

            public Greetings Greet { get; set; } = new Greetings();
            public Titles Title { get; set; } = new Titles();
        }

        public class AllCultures
        {
            public Dictionary<string, CultureConversation> Cultures { get; set; } = new Dictionary<string, CultureConversation>();
        }
        
        AllCultures allCultures = new AllCultures();
        allCultures.Cultures["SaltMarshCulture"] = new CultureConversation
        {
            Greet = new CultureConversation.Greetings
            {
                Familiarities =
                {
                    { "unfamiliar", new List<string> { "Salt and sun", "Good meet to you.", "Bountiful harvest" } },
                    { "familiar", new List<string> { "Salt and sun", "How are you on this day?", "How fare you?" } },
                    { "friendly", new List<string> { "Hail", "Merry meet", "Bright tidings" } },
                    { "unfriendly", new List<string> { "What", "What could you want", "Will this take long" } }
                }
            },
            Title = new CultureConversation.Titles
            {
                Familiarities =
                {
                    { "unfamiliar", new List<string> { "wayfarer", "traveler", "stranger" } },
                    { "familiar", new List<string> { "wayfarer", "fellow" } },
                    { "friendly", new List<string> { "my friend", "my sibling", "my fellow", "dear sib" } },
                    { "unfriendly", new List<string> { "you jerk", "you troublemaker" } }
                }
            }
        };

        allCultures.Cultures["DesertCulture"] = new CultureConversation
        {
            Greet = new CultureConversation.Greetings
            {
                Familiarities =
                {
                    { "unfamiliar", new List<string> { "Salt and sun", "Good meet to you.", "Bountiful harvest" } },
                    { "familiar", new List<string> { "Salt and sun", "How are you on this day?", "How fare you?" } },
                    { "friendly", new List<string> { "Hail", "Merry meet", "Bright tidings" } },
                    { "unfriendly", new List<string> { "What", "What could you want", "Will this take long" } }
                }
            },
            Title = new CultureConversation.Titles
            {
                Familiarities =
                {
                    { "unfamiliar", new List<string> { "wayfarer", "traveler", "stranger" } },
                    { "familiar", new List<string> { "wayfarer", "fellow" } },
                    { "friendly", new List<string> { "my friend", "my sibling", "my fellow", "dear sib" } },
                    { "unfriendly", new List<string> { "you jerk", "you troublemaker" } }
                }
            }
        };
        allCultures.Cultures["KendrinCulture"] = new CultureConversation
        {
            Greet = new CultureConversation.Greetings
            {
                Familiarities =
                {
                    { "unfamiliar", new List<string> { "Salt and sun", "Good meet to you.", "Bountiful harvest" } },
                    { "familiar", new List<string> { "Salt and sun", "How are you on this day?", "How fare you?" } },
                    { "friendly", new List<string> { "Hail", "Merry meet", "Bright tidings" } },
                    { "unfriendly", new List<string> { "What", "What could you want", "Will this take long" } }
                }
            },
            Title = new CultureConversation.Titles
            {
                Familiarities =
                {
                    { "unfamiliar", new List<string> { "wayfarer", "traveler", "stranger" } },
                    { "familiar", new List<string> { "wayfarer", "fellow" } },
                    { "friendly", new List<string> { "my friend", "my sibling", "my fellow", "dear sib" } },
                    { "unfriendly", new List<string> { "you jerk", "you troublemaker" } }
                }
            }
        };

        /*
        public string[] familiarityRanking = {"unfamiliar","familiar","friendly","unfriendly"};
        int myFamiliarity = 3;
        string whichCulture = "SaltMarshCulture";
        var whichGreetFamiliarity = allCultures.Cultures[whichCulture].Greet.Familiarities[familiarityRanking[myFamiliarity]];
        string randomGreetString = whichGreetFamiliarity[QRand.Next(0,whichGreetFamiliarity.Count)];
        */
    }
}

