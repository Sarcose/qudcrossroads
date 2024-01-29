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
        public static void GreetFn(Phrase phrase)
        {
            List<string> stringList = AllCultures.Cultures[phrase.Culture].Greet["keys"].Familiarities[phrase.Familiarity];
            phrase.ReStr += GetRandString(stringList);
        }
        public static void TitleFn(Phrase phrase)
        {
            List<string> stringList = AllCultures.Cultures[phrase.Culture].Title["keys"].Familiarities[phrase.Familiarity];
            phrase.ReStr += GetRandString(stringList);
        }
    }
}