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
            if (job == "Farmer"){
                return FarmerConversation.getElement(key, specific);
            }else if (job == "Merchant"){
                return MerchantConversation.getElement(key, specific);
            }else if (job == "Warrior"){
                return WarriorConversation.getElement(key, specific);
            }else{
                return null;
            }
        }
        
        public static string GreetFn(Phrase phrase)
        {
            List<string> greetCulture = AllCultures.Cultures[phrase.Culture].Greet["keys"].Familiarities[phrase.Familiarity];
            List<string> greetPersonality = PersonalityConversation.Personalities[phrase.Personality].Elements["greet"];
            List<string> greetJob = getJob(phrase.Job, "greet");
            List<string> subPersonality = new List<string>{};
            List<string> jobSpecific = new List<string>{};
            if (!string.IsNullOrEmpty(phrase.specificJob)){jobSpecific = getJob(phrase.Job, "greet", phrase.specificJob);}
            if (!string.IsNullOrEmpty(phrase.subPersonality)){subPersonality = PersonalityConversation.Personalities[phrase.subPersonality].Elements["greet"];}
            List<string>[] greetArray = new List<string>[] { greetCulture, greetPersonality, greetJob, jobSpecific, subPersonality };
            return GetRandString(greetArray);
        }
        public static string TitleFn(Phrase phrase)     //phrase
        {
            List<string> titleCulture =  AllCultures.Cultures[phrase.Culture].Title["keys"].Familiarities[phrase.Familiarity];
            List<string> titlePersonality = PersonalityConversation.Personalities[phrase.Personality].Elements["title"];
            List<string> titleJob = getJob(phrase.Job, "title");
            List<string> titlesubPersonality = new List<string>{};
            List<string> titleJobSpecific = new List<string>{};
            if (!string.IsNullOrEmpty(phrase.specificJob)){titleJobSpecific = getJob(phrase.Job, "title", phrase.specificJob);}
            if (!string.IsNullOrEmpty(phrase.subPersonality)){titlesubPersonality = PersonalityConversation.Personalities[phrase.subPersonality].Elements["title"];}
            List<string>[] titleArray = new List<string>[] { titleCulture, titlePersonality, titleJob, titleJob, titlesubPersonality };
            return GetRandString(titleArray);
        }
        public static string ElementByCategories(Phrase phrase, string key)
        {
            List<string> culture = AllCultures.Cultures[phrase.Culture].Greet["keys"].Familiarities[phrase.Familiarity];
            List<string> personality = PersonalityConversation.Personalities[phrase.Personality].Elements[key];
            List<string> job = getJob(phrase.Job, "greet");
            List<string> subPersonality = new List<string>{};
            List<string> jobSpecific = new List<string>{};
            if (!string.IsNullOrEmpty(phrase.specificJob)){jobSpecific = getJob(phrase.Job, key, phrase.specificJob);}
            if (!string.IsNullOrEmpty(phrase.subPersonality)){subPersonality = PersonalityConversation.Personalities[phrase.subPersonality].Elements[key];}
            List<string>[] elementArray = new List<string>[] { culture, personality, job, jobSpecific, jobSpecific subPersonality };
            return GetRandString(elementArray);


            //Personality
            //Subpersonality
            //Generic Personality
            //Culture -> Affinity
            //Job
            //Job, specific
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