using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using XRL;
using XRL.Language;
using static XRL.Language.Grammar;
using XRL.World;
using XRL.World.Conversations;
using XRL.World.Parts;
using XRL.Messages;

using QudCrossroads;
using QRand = QudCrossroads.Utilities.QudCrossroads_Random;
using static QudCrossroads.Dialogue.Elements;
using static QudCrossroads.Dialogue.Functions;
using static QudCrossroads.Dialogue.Builders;
//TODO: reduce my using directives
namespace QudCrossroads.Dialogue
{
    public static partial class QC_Lists
    {
        public static Dictionary<string, object> CrossroadsLVR = new Dictionary<string, object>
        {
            { "Resource.Farmer", new List<string> { "|picktwo|","|pickspecific|","food","shelter" } },
            { "Resource.Watervinefarmer", new List<string> { "|picktwo|", "watervine", "water", "wafers" } },                        
            { "LaborVerb", new List<string> { "labor", "task", "trade", "calling" } },                        
            { "HarshAdjective", new List<string> { "|picktwo|", "harsh", "hard", "tough", "trying", "tiresome" } },
            { "greet", new Func<Phrase, string, string>((phrase, key) => ParseFamiliarity(phrase, key)) },
            { "title", new Func<Phrase, string, string>((phrase, key) => ParseFamiliarity(phrase, key)) },
            { "pleasantry", new Func<Phrase, string, string>((phrase, key) => ParseFamiliarity(phrase, key)) }//,
            //{ "questHint", new Func<Phrase, string, string>((phrase, key) => GetQuest(phrase, key)) },
            //{ "questHerring", new Func<Phrase, string, string>((phrase, key) => GetQuest(phrase, key)) },
            //{ "questConclusion", new Func<Phrase, string, string>((phrase, key) => GetQuest(phrase, key)) },
        };
    }
}




