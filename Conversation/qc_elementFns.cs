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
        
        public static string GreetFn(Phrase phrase)
        {
            List<string> greetCulture = AllCultures.Cultures[phrase.Culture].Greet["keys"].Familiarities[phrase.Familiarity];
            List<string> greetPersonality = new List<string> { "Item3", "Item4" };
            List<string> greetJob = new List<string> { "Item5", "Item6" };

            List<string>[] greetArray = new List<string>[] { greetCulture, greetPersonality, greetJob };
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