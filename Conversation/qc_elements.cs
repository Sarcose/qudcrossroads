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
/***************************************************************/
//                     Initializations                         //
/***************************************************************/
        public class Personality
        {
            public Dictionary<string, List<string>> Elements { get; set; } = new Dictionary<string, List<string>>
            {
                { "greet", new List<string>() },        //generic will not have one, but personalities will
                { "emoteintro", new List<string>() },
                { "transition", new List<string>() },   // generic transition statements like "At any rate" or personality transition statements like "Whatever..."
                { "emotetransition", new List<string>() },
                { "emoteconclusion", new List<string>() },
                { "toquest", new List<string>() },      // a second transition type specifically for opening up the quest hook. "I have a tale to tell."
                { "saying", new List<string>() },       // upon generation, a villager will pick a random number between 1-20 and choose a saying from amongst their options. This can be changed
                { "proverb", new List<string>() },
                { "flavor", new List<string>() },
                { "comedy", new List<string>() },       // a series of generative phrase templates for saying something comical 
                { "serious", new List<string>() },      // a series of generative phrase templates for saying something serious
                { "hobby", new List<string>() },        // upon generation, a villager will pick a 2-4 random numbers between 1-20 and choose hobbies from amongst their options. This cannot be changed
                { "insults", new List<string>() },      // generic insults for when a villager is displeased. Can also be cultural, special, and job based
                { "compliment", new List<string>() }    // generic compliments for when a villager is pleased. Can also be cultural, special, and job based
            };
        }
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

/***************************************************************/
//          Personality Based Conversation Elements            //
/***************************************************************/
        public static class PersonalityConversation
        {
            public static Dictionary<string, Personality> Personalities { get; set; } = new Dictionary<string, Personality>
            {
                { "Generic", new Personality()                                      //Everyone has a chance to pick from this list
                    {
                        Elements =
                        {
                            { "transition", new List<string> { "Anyway", "Regardless","Whatever the case","At any rate","Nonetheless","You know" } },
                            { "toquest", new List<string> { "I have a tale to tell.","Wait til you hear this.","Now that I have your attention...","I've been thinking about something, lately.","Let me tell you a story.","I'm glad you asked.","" } },
                            { "saying", new List<string> { "hmmmm","the sun always sets","by the salt sun","by the Eaters","at any cost","traveler","for the birds","*the =localfaction= know the way","*=randomcreature= guide us","*=MARKOVWORD=" } },
                            { "comedy", new List<string> { "comedy1", "comedy2" } },
                            { "serious", new List<string> { "serious1", "serious2" } },
                            { "hobby", new List<string> { "Hobby1", "Hobby2" } },
                            { "insults", new List<string> { "insult1", "insult2" } },
                            { "compliment", new List<string> { "Compliment1", "Compliment2" } }
                        }
                    }
                },
                { "Elder", new Personality()
                    {
                        Elements =
                        {
                            { "greet", new List<string> { "Greeeets!" } }, 
                            { "transition", new List<string> { "Elder transition1", "Elder transition2" } },   
                            { "toquest", new List<string> { "Elder QuestHook1", "Elder QuestHook2" } },      
                            { "saying", new List<string> { "Elder Saying1", "Elder Saying2" } },
                            { "comedy", new List<string> { "Elder comedy1", "Elder comedy2" } },
                            { "serious", new List<string> { "Elder serious1", "Elder serious2" } },
                            { "hobby", new List<string> { "Elder Hobby1", "Elder Hobby2" } },
                            { "insults", new List<string> { "Elder insult1", "Elder insult2" } },
                            { "compliment", new List<string> { "Elder Compliment1", "Elder Compliment2" } }
                        }
                    }
                },
                { "Lazy", new Personality
                    {
                        Elements =
                        {   
                            { "greet", new List<string> { "*yawn* how's it goin?" } }, 
                            { "transition", new List<string> { "transition1", "transition2" } },
                            { "toquest", new List<string> { "QuestHook1", "QuestHook2" } },
                            { "saying", new List<string> { "Saying1", "Saying2" } },
                            { "comedy", new List<string> { "comedy1", "comedy2" } },
                            { "serious", new List<string> { "serious1", "serious2" } },
                            { "hobby", new List<string> { "Hobby1", "Hobby2" } },
                            { "insults", new List<string> { "insult1", "insult2" } },
                            { "compliment", new List<string> { "Compliment1", "Compliment2" } }
                        }
                    }
                },
                { "Snooty", new Personality
                    {
                        Elements =
                        {
                            { "greet", new List<string> { "Greeeets!" } }, 
                            { "transition", new List<string> { "transition1", "transition2" } },
                            { "toquest", new List<string> { "QuestHook1", "QuestHook2" } },
                            { "saying", new List<string> { "Saying1", "Saying2" } },
                            { "comedy", new List<string> { "comedy1", "comedy2" } },
                            { "serious", new List<string> { "serious1", "serious2" } },
                            { "hobby", new List<string> { "Hobby1", "Hobby2" } },
                            { "insults", new List<string> { "insult1", "insult2" } },
                            { "compliment", new List<string> { "Compliment1", "Compliment2" } }
                        }
                    }
                },
                { "Peppy", new Personality
                    {
                        Elements =
                        {
                            { "greet", new List<string> { "Beautiful Day!" } }, 
                            { "transition", new List<string> { "transition1", "transition2" } },
                            { "toquest", new List<string> { "QuestHook1", "QuestHook2" } },
                            { "saying", new List<string> { "Saying1", "Saying2" } },
                            { "comedy", new List<string> { "comedy1", "comedy2" } },
                            { "serious", new List<string> { "serious1", "serious2" } },
                            { "hobby", new List<string> { "Hobby1", "Hobby2" } },
                            { "insults", new List<string> { "insult1", "insult2" } },
                            { "compliment", new List<string> { "Compliment1", "Compliment2" } }
                        }
                    }
                },
                { "Warrior", new Personality
                    {
                        Elements =
                        {
                            { "greet", new List<string> { "Greeeets!" } }, 
                            { "transition", new List<string> { "transition1", "transition2" } },
                            { "toquest", new List<string> { "QuestHook1", "QuestHook2" } },
                            { "saying", new List<string> { "Saying1", "Saying2" } },
                            { "comedy", new List<string> { "comedy1", "comedy2" } },
                            { "serious", new List<string> { "serious1", "serious2" } },
                            { "hobby", new List<string> { "Hobby1", "Hobby2" } },
                            { "insults", new List<string> { "insult1", "insult2" } },
                            { "compliment", new List<string> { "Compliment1", "Compliment2" } }
                        }
                    }
                },
                { "Chimera", new Personality
                    {
                        Elements =
                        {
                            { "greet", new List<string> { "Greeeets!" } }, 
                            { "transition", new List<string> { "transition1", "transition2" } },
                            { "toquest", new List<string> { "QuestHook1", "QuestHook2" } },
                            { "saying", new List<string> { "Saying1", "Saying2" } },
                            { "comedy", new List<string> { "comedy1", "comedy2" } },
                            { "serious", new List<string> { "serious1", "serious2" } },
                            { "hobby", new List<string> { "Hobby1", "Hobby2" } },
                            { "insults", new List<string> { "insult1", "insult2" } },
                            { "compliment", new List<string> { "Compliment1", "Compliment2" } }
                        }
                    }
                },
                { "Esper", new Personality
                    {
                        Elements =
                        {
                            { "greet", new List<string> { "Greeeets!" } }, 
                            { "transition", new List<string> { "transition1", "transition2" } },
                            { "toquest", new List<string> { "QuestHook1", "QuestHook2" } },
                            { "saying", new List<string> { "Saying1", "Saying2" } },
                            { "comedy", new List<string> { "comedy1", "comedy2" } },
                            { "serious", new List<string> { "serious1", "serious2" } },
                            { "hobby", new List<string> { "Hobby1", "Hobby2" } },
                            { "insults", new List<string> { "insult1", "insult2" } },
                            { "compliment", new List<string> { "Compliment1", "Compliment2" } }
                        }
                    }
                },
                { "Studious", new Personality
                    {
                        Elements =
                        {
                            { "greet", new List<string> { "Greeeets!" } }, 
                            { "transition", new List<string> { "transition1", "transition2" } },
                            { "toquest", new List<string> { "QuestHook1", "QuestHook2" } },
                            { "saying", new List<string> { "Saying1", "Saying2" } },
                            { "comedy", new List<string> { "comedy1", "comedy2" } },
                            { "serious", new List<string> { "serious1", "serious2" } },
                            { "hobby", new List<string> { "Hobby1", "Hobby2" } },
                            { "insults", new List<string> { "insult1", "insult2" } },
                            { "compliment", new List<string> { "Compliment1", "Compliment2" } }
                        }
                    }
                },
                { "Downer", new Personality
                    {
                        Elements =
                        {
                            { "greet", new List<string> { "Greeeets!" } }, 
                            { "transition", new List<string> { "transition1", "transition2" } },
                            { "toquest", new List<string> { "QuestHook1", "QuestHook2" } },
                            { "saying", new List<string> { "Saying1", "Saying2" } },
                            { "comedy", new List<string> { "comedy1", "comedy2" } },
                            { "serious", new List<string> { "serious1", "serious2" } },
                            { "hobby", new List<string> { "Hobby1", "Hobby2" } },
                            { "insults", new List<string> { "insult1", "insult2" } },
                            { "compliment", new List<string> { "Compliment1", "Compliment2" } }
                        }
                    }
                },
                { "Cranky", new Personality
                    {
                        Elements =
                        {
                            { "greet", new List<string> { "Greeeets!" } }, 
                            { "transition", new List<string> { "transition1", "transition2" } },
                            { "toquest", new List<string> { "QuestHook1", "QuestHook2" } },
                            { "saying", new List<string> { "Saying1", "Saying2" } },
                            { "comedy", new List<string> { "comedy1", "comedy2" } },
                            { "serious", new List<string> { "serious1", "serious2" } },
                            { "hobby", new List<string> { "Hobby1", "Hobby2" } },
                            { "insults", new List<string> { "insult1", "insult2" } },
                            { "compliment", new List<string> { "Compliment1", "Compliment2" } }
                        }
                    }
                },
            };
        }







/***************************************************************/
//              Culture Based Conversation Elements            //
/***************************************************************/


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
                                    { "unfamiliar", new List<string> { "Salt and sun", "Good meet to you", "Bountiful harvest","Water and fortune to you" } },
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
                                    { "friendly", new List<string> { "my friend", "my sibling", "my fellow", "dear sib" } }, //my =sib=, dear =sib=
                                    { "unfriendly", new List<string> { "you jerk", "you troublemaker" } }
                                }
                            }
                        }
                    }
                };
            }
        }






/***************************************************************/
//            Profession Based Conversation Elements            //
/***************************************************************/
//Separated into general -> specific, job categories are sorted based on whether the character qualifies out of a list. Starting here:
        public class Job
        {
            public Dictionary<string, List<string>> Elements { get; set; } = new Dictionary<string, List<string>>
            {
                { "emoteintro",      new List<string>() },
                { "transition",      new List<string>() },   // generic transition statements like "At any rate" or personality transition statements like "Whatever..."
                { "emotetransition", new List<string>() },
                { "emoteconclusion", new List<string>() },
                { "toquest",         new List<string>() },      // a second transition type specifically for opening up the quest hook. "I have a tale to tell."
                { "saying",          new List<string>() },       // upon generation, a villager will pick a random number between 1-20 and choose a saying from amongst their options. This can be changed
                { "proverb",         new List<string>() },
                { "flavor",          new List<string>() },
                { "comedy",          new List<string>() },       // a series of generative phrase templates for saying something comical 
                { "serious",         new List<string>() },      // a series of generative phrase templates for saying something serious
                { "hobby",           new List<string>() },        // upon generation, a villager will pick a 2-4 random numbers between 1-20 and choose hobbies from amongst their options. This cannot be changed
                { "insults",         new List<string>() },      // generic insults for when a villager is displeased. Can also be cultural, special, and job based
                { "compliment",      new List<string>() }    // generic compliments for when a villager is pleased. Can also be cultural, special, and job based
            };
        }
        //this should probably use inheritance but LAZYYYYYY
        public static class FarmerConversation
        {
            public static Dictionary<string, Job> Jobs { get; set; } = new Dictionary<string, Job>
            {
                { "Generic", new Job()                                      //Everyone has a chance to pick from this list
                    {
                        Elements =
                        {        //TODO: see  notes: csharp snippets, regex replacement
                            { "emoteintro",  new List<string> {"=pronouns.possessive= back bent to you, =pronouns.subjective= is hard at work. A moment passes, and =pronouns.subjective= stands",""}},
                            { "transition", new List<string> { "Anyway", "Regardless","Whatever the case","At any rate","Nonetheless","You know","But ah, what am I to complain?" } },
                            { "emotetransition", new List<string> {"=pronouns.posessive= kneels back down, biding you kneel with =pronouns.objective=. Together the two of you speak between the whispers of watervine in the gentle breeze."}},
                            { "emoteconclusion", new List<string>{""}},
                            { "toquest", new List<string> { "" } },
                            { "saying", new List<string> { "" } },
                            { "proverb", new List<string> { "|picktwo|","so it is, that the Salt Sun rises and the Salt Sun sets"} },
                            { "flavor", new List<string> { "|picktwo|","To ply |resource.farmer| from the |biome| is a |harshadjective| |laborverb|" } },
                            { "comedy", new List<string> { "comedy1", "comedy2" } },
                            { "serious", new List<string> { "serious1", "serious2" } },
                            { "hobby", new List<string> { "Hobby1", "Hobby2" } },
                            { "insults", new List<string> { "insult1", "insult2" } },
                            { "compliment", new List<string> { "Compliment1", "Compliment2" } }
                        }
                    }
                },
                { "AppleFarmer", new Job()                                      //And this is specific
                    {
                        Elements =
                        {
                            { "emoteintro", new List<string> {"=pronouns.possessive= back bent to you, =pronouns= is hard at work. A moment passes, and =pronoun= stands",""}},
                            { "transition", new List<string> { "Anyway", "Regardless","Whatever the case","At any rate","Nonetheless","You know","*EMOTE*" } },
                            { "emotetransition", new List<string> {"=pronouns.posessive= kneels back down, biding you kneel with =pronouns.objective=. Together the two of you speak between the whispers of watervine in the gentle breeze."}},
                            { "emoteconclusion", new List<string>{""}},
                            { "toquest", new List<string> { "" } },
                            { "saying", new List<string> { "" } },
                            { "flavor", new List<string> { "flavor1", "flavor2" } },
                            { "proverb", new List<string> { "", "proverb2" } },
                            { "comedy", new List<string> { "comedy1", "comedy2" } },
                            { "serious", new List<string> { "serious1", "serious2" } },
                            { "hobby", new List<string> { "Hobby1", "Hobby2" } },
                            { "insults", new List<string> { "insult1", "insult2" } },
                            { "compliment", new List<string> { "Compliment1", "Compliment2" } }
                        }
                    }
                },
                { "WatervineFarmer", new Job()                                      //And this is specific
                    {
                        Elements =
                        {       //TODO: create a table for longstrings like this one, bespoke emote sentences
                            { "emoteintro", new List<string> {"The day's grime darkening =pronouns.posessive= brow, a weather-beaten farmer adjusts =pronouns.posessive= green-and-mud-stained vinereaper to leverage a hand of work and salt to adjust =pronouns.posessive= kufiya, and looks up at you from =pronouns.posessive= resting squat briefly before standing to give address",""}},
                            { "transition", new List<string> { "Anyway", "Regardless","Whatever the case","At any rate","Nonetheless","You know" } },
                            { "emotetransition", new List<string> {"=pronouns.posessive= kneels back down, biding you kneel with =pronouns.objective=. Together the two of you speak between the whispers of watervine in the gentle breeze."}},
                            { "emoteconclusion", new List<string>{""}},
                            { "toquest", new List<string> { "" } },
                            { "saying", new List<string> { "" } },
                            { "flavor", new List<string> { "Do you see how thin these wafers are, unfertilized?", "flavor2" } },
                            { "proverb", new List<string> { "we work the moisture from the vines", "proverb2" } },
                            { "comedy", new List<string> { "comedy1", "comedy2" } },
                            { "serious", new List<string> { "serious1", "serious2" } },
                            { "hobby", new List<string> { "Hobby1", "Hobby2" } },
                            { "insults", new List<string> { "insult1", "insult2" } },
                            { "compliment", new List<string> { "Compliment1", "Compliment2" } }
                        }
                    }
                },
            };        
            public static List<string> getElement(string key, string specific = "Generic")
            {
                if (Jobs[specific].Elements.ContainsKey(key))
                {
                    return Jobs[specific].Elements[key];
                }
                else
                {
                    return new List<string>(); // Return an empty list for undefined categories or keys
                }
            }
        }
        public static class MerchantConversation
        {
            public static Dictionary<string, Job> Jobs { get; set; } = new Dictionary<string, Job>
            {
                { "Generic", new Job()                                      //Everyone has a chance to pick from this list
                    {
                        Elements =
                        {        //TODO: see  notes: csharp snippets, regex replacement
                            { "emoteintro",  new List<string> {"=pronouns.possessive= back bent to you, =pronouns.subjective= is hard at work. A moment passes, and =pronouns.subjective= stands",""}},
                            { "transition", new List<string> { "Anyway", "Regardless","Whatever the case","At any rate","Nonetheless","You know","But ah, what am I to complain?" } },
                            { "emotetransition", new List<string> {"=pronouns.posessive= kneels back down, biding you kneel with =pronouns.objective=. Together the two of you speak between the whispers of watervine in the gentle breeze."}},
                            { "emoteconclusion", new List<string>{""}},
                            { "toquest", new List<string> { "" } },
                            { "saying", new List<string> { "" } },
                            { "proverb", new List<string> { "|picktwo|","so it is, that the Salt Sun rises and the Salt Sun sets"} },
                            { "flavor", new List<string> { "|picktwo|","To ply |resource.farmer| from the |biome| is a |harshadjective| |laborverb|" } },
                            { "comedy", new List<string> { "comedy1", "comedy2" } },
                            { "serious", new List<string> { "serious1", "serious2" } },
                            { "hobby", new List<string> { "Hobby1", "Hobby2" } },
                            { "insults", new List<string> { "insult1", "insult2" } },
                            { "compliment", new List<string> { "Compliment1", "Compliment2" } }
                        }
                    }
                },
            };           
            public static List<string> getElement(string key, string specific = "Generic")
            {
                if (Jobs[specific].Elements.ContainsKey(key))
                {
                    return Jobs[specific].Elements[key];
                }
                else
                {
                    return new List<string>(); // Return an empty list for undefined categories or keys
                }
            }
        }
        public static class WarriorConversation
        {
            public static Dictionary<string, Job> Jobs { get; set; } = new Dictionary<string, Job>
            {
                { "Generic", new Job()                                      //Everyone has a chance to pick from this list
                    {
                        Elements =
                        {        //TODO: see  notes: csharp snippets, regex replacement
                            { "emoteintro",  new List<string> {"=pronouns.possessive= back bent to you, =pronouns.subjective= is hard at work. A moment passes, and =pronouns.subjective= stands",""}},
                            { "transition", new List<string> { "Anyway", "Regardless","Whatever the case","At any rate","Nonetheless","You know","But ah, what am I to complain?" } },
                            { "emotetransition", new List<string> {"=pronouns.posessive= kneels back down, biding you kneel with =pronouns.objective=. Together the two of you speak between the whispers of watervine in the gentle breeze."}},
                            { "emoteconclusion", new List<string>{""}},
                            { "toquest", new List<string> { "" } },
                            { "saying", new List<string> { "" } },
                            { "proverb", new List<string> { "|picktwo|","so it is, that the Salt Sun rises and the Salt Sun sets"} },
                            { "flavor", new List<string> { "|picktwo|","To ply |resource.farmer| from the |biome| is a |harshadjective| |laborverb|" } },
                            { "comedy", new List<string> { "comedy1", "comedy2" } },
                            { "serious", new List<string> { "serious1", "serious2" } },
                            { "hobby", new List<string> { "Hobby1", "Hobby2" } },
                            { "insults", new List<string> { "insult1", "insult2" } },
                            { "compliment", new List<string> { "Compliment1", "Compliment2" } }
                        }
                    }
                },
            };            
            public static List<string> getElement(string key, string specific = "Generic")
            {
                if (Jobs[specific].Elements.ContainsKey(key))
                {
                    return Jobs[specific].Elements[key];
                }
                else
                {
                    return new List<string>(); // Return an empty list for undefined categories or keys
                }
            }

        }
    }
}

