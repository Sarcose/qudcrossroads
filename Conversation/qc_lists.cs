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
//TODO: reduce my using directives
namespace QudCrossroads.Dialogue
{
    public static partial class QC_Lists
    {
        public static Dictionary<string, object> CrossroadsLVR = new Dictionary<string, object>
        {
            //function example
            //{ "Greet", (Action<string>)GreetFunction },
                                                        //in parsing, check for =phrases= for doubling up or other logic
            { "|resource.farmer|", new List<string> { "|picktwo|","|pickspecific|","food","shelter" } },
            { "|resource.watervinefarmer|", new List<string> { "|picktwo|", "watervine", "water", "wafers" } },                        
            { "|laborverb|", new List<string> { "labor", "task", "trade", "calling" } },                        
            { "|harshadjective|", new List<string> { "|picktwo|", "harsh", "hard", "tough", "trying", "tiresome" } },

            //{ "|biome|", (Action<string>)GetBiome },    //see GetBiome under qc_elementFns

            //{ "|questHint|", (Action<string>)GetQuestHint },    //see GetBiome under qc_elementFns
            //{ "|questHerring|", (Action<string>)GetQuestHerring },    //see GetBiome under qc_elementFns
        };
    }
}