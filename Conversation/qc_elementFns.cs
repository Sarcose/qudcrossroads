using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using XRL;
using XRL.Language;
using XRL.World;
using XRL.Messages;
using QudCrossroads;
using QRand = QudCrossroads.Utilities.QudCrossroads_Random;
using static QudCrossroads.Dialogue.Elements;
using static QudCrossroads.Dialogue.Builders;

namespace QudCrossroads.Dialogue
{
    public static partial class Functions
    {
        public static string Title = "Title"; public static string Greet = "Greet";
        public static Dictionary<string, Func<Phrase, string>> functionDictionary = new Dictionary<string, Func<Phrase, string>>
        {
            { Greet, GreetFn },
            { Title, TitleFn }
        };
        public static Dictionary<string, object> jobCategories = new Dictionary<string, object>
        {
            { "Farmer", new FarmerConversation() },
            { "Warrior", new WarriorConversation() },
            { "Merchant", new MerchantConversation() }
        };
        public static List<string> getJob(string job, string key, string specific = null)
        {
            if (!string.IsNullOrEmpty(specific))    // We're looking for a specific job
            {
                return ((dynamic)JobCategories[job])[specific].Greet;   //explicitly cast the objects in jobCategories as dynamic but return Greet as specific
            }
            else        // We're looking for the general job under the job string
            {
                return ((dynamic)JobCategories[job]).General.Greet;
            }
        }
        
        public static string GreetFn(Phrase phrase)
        {
            List<string> greetCulture = AllCultures.Cultures[phrase.Culture].Greet["keys"].Familiarities[phrase.Familiarity];
            List<string> greetJob = getJob(phrase.Job, "greet");
            List<string> greetPersonality = PersonalityConversation.Personalities[phrase.Personality].Elements["greet"];
            List<string> greetsubPersonality = new List<string>;
            List<string> greetJobSpecific = new List<string>;
            if (!string.IsNullOrEmpty(phrase.specificJob)){List<string> greetJobSpecific = getJob(phrase.Job, "greet", phrase.specificJob);}
            if (!string.IsNullOrEmpty(phrase.subPersonality)){List<string> greetsubPersonality = PersonalityConversation.Personalities[phrase.subPersonality].Elements["greet"];}
            
            List<string>[] greetArray = new List<string>[] { greetCulture, greetPersonality, greetJob, greetsubPersonality };
            return GetRandString(greetArray);
        }
        public static string TitleFn(Phrase phrase)     //phrase
        {
            List<string> titleCulture =  AllCultures.Cultures[phrase.Culture].Title["keys"].Familiarities[phrase.Familiarity];
            List<string> titlePersonality = new List<string> { "Item3", "Item4" };
            List<string> titleJob = new List<string> { "Item5", "Item6" };

            List<string>[] titleArray = new List<string>[] { titleCulture, titlePersonality, titleJob };
            return GetRandString(titleArray);
        }

        public static string GetBiome()
        {
            //TODO: global container for CurrentConversationContext such as "whaat biome am I in?" etc.
            //ex return string CurrentConversationContext.currentBiome
            return null;
        }
    }
}





/*
Building TODOs:

|greeting|: 
    [X] get it to parse and pass GreetFn using Phrase
    [ ] greetPersonality based on phrase personality in qc_elements.cs -- grab an arbitrary number of personalities actually!!
    [ ] greetJob based on phrase Job                in qc_elements.cs
    [ ] greetSpecificJob based on phrase specific job if it exists in qc_elements.cs