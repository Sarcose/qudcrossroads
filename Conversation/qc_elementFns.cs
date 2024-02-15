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
        public static List<string> getJob(string job, string key, string specific = "Generic")
        {
            qprintc("----getJob started");
            if (job == "Farmer"){
            qprintc("----farmer");
                return FarmerConversation.getElement(key, specific);
            }else if (job == "Merchant"){
            qprintc("----merchant");
                return MerchantConversation.getElement(key, specific);
            }else if (job == "Warrior"){
            qprintc("----warrior");
                return WarriorConversation.getElement(key, specific);
            }else{
            qprintc("----null");
                return null;
            }
        }
        
        public static string GreetFn(Phrase phrase)
        {
            qprintc("---GreetFn");
            List<string> greetCulture = AllCultures.Cultures[phrase.Culture].Greet["keys"].Familiarities[phrase.Familiarity];
            qprintc("----getJob");
            List<string> greetPersonality = PersonalityConversation.Personalities[phrase.Personality].Elements["greet"];
            List<string> greetJob = getJob(phrase.Job, "greet");
            List<string> greetsubPersonality = new List<string>{};
            List<string> greetJobSpecific = new List<string>{};
            qprintc("----checkSpecifics");
            if (!string.IsNullOrEmpty(phrase.specificJob)){greetJobSpecific = getJob(phrase.Job, "greet", phrase.specificJob);}
            if (!string.IsNullOrEmpty(phrase.subPersonality)){greetsubPersonality = PersonalityConversation.Personalities[phrase.subPersonality].Elements["greet"];}
            qprintc("----buildArray");
            List<string>[] greetArray = new List<string>[] { greetCulture, greetPersonality, greetJob, greetsubPersonality };
            qprintc("----getRandString");
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
    */