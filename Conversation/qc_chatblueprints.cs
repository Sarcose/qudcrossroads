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

namespace QudCrossroads.Dialogue
{
    public static partial class ChatBlueprints
    {
        public static List<string> chatList = new List<string>
        {   //|intro| instead of emoteintro, with a chance to emote instead. Same for transitions
            "|intro||greeting||title||toQuest||questHint||questHerring||transition||flavor||proverb||transition||emoteTransition||questConclusion|",
        };
    }
}



/*
somewhere, questConclusion. Perhaps another qc_questGenerator that creates a:
    questHint
    questHerring(s)
    questConclusion

    based on mood etc
"No, indeed, friend. I wouldn't trade this life for that of yours, that's for sure. Mind you not the bitter tone of an old salt, after all."

*/