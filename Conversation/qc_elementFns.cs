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
        public static string GreetFn(Phrase phrase)
        {
            List<string> stringList = AllCultures.Cultures[phrase.Culture].Greet["keys"].Familiarities[phrase.Familiarity];
            return GetRandString(stringList);
        }
        public static string TitleFn(Phrase phrase)
        {
            List<string> stringList = AllCultures.Cultures[phrase.Culture].Title["keys"].Familiarities[phrase.Familiarity];
            return GetRandString(stringList);
        }
    }
}