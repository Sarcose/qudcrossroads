using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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
using static QudCrossroads.Dialogue.QC_Lists;

namespace QudCrossroads.Dialogue
{
    public static partial class QuestEngine
    {
        public class QCFactionChange
        {
            public int value {get; set; }
            public string faction {get; set; }
        }
        public class QCQuestReward
        {
            public int XP {get; set; }
            public List<string> items {get; set; }
            public QCFactionChange[] factions {get; set;}
        }
        public class QCQuest    //some example members
        {   
            public QCQuestReward reward {get; set;}
        }
    }
}
/*

code from ilspy:

public Quest fabricateFindASpecificSiteQuest(GameObject giver)
public Quest fabricateFindASpecificItemQuest(GameObject giver, string objectToFetchCacheID)
Quest quest = QuestsAPI.fabricateEmptyQuest();
                //do things to quest



basically, use the above to produce my own version of fabriceQudCrossroadsQuest
            quest types: Errands
                         Classic
                         Requests
                         

addQuestConversationToGiver(giver, quest);      //mine this for quest information to figure out how to generate quest from convo                

*/






/*

Starting with the most basic types of errands for quests, we have:

Errands (tier 0; no familiarity)
- Bring Q item to X person ("I borrowed Q" "I wish to gift Q" "Q was requested of me" "X needs Q")
- Get Q item from X person ("X borrowed Q" "I wish to obtain Q from X")
- Tell X person Q news/thing
- Trade R item for Q item from X person ("I wish to buy/barter Q for R")

Requests
- Find Q for me ("I need to get Q" )
- Find Q for me and give to X ("I wish to gift Q to X")

Qud-style (higher tier)
- Interact with Q object at Y location (context interact; sit on chair for instance )
- Retrieve Q object from Y location



Steps:
    - Pick quest template
    Priority Elements:
        - Difficulty (calculated from intimacy to player - unfamiliar, familiar, disliked, faction rep, player level, number of quests already done for that NPCj, NPC level)
        - TargetLocation (location to travel to, distance of which intimates the primary difficulty factor)
            - GetLocation() (if TargetPerson then verify there is a TargetPerson there)
                            (or generate said TargetPerson? But prefer a list of persons probably. Look at Village NPCs first)
        - TargetPerson (person to deliver to, person to retrieve from -- located in location)
            - PickNPC("Target")
        - TargetItem (item of importance)
            - PickItem("Deliver"), PickItem("Retrieve"), -- leaves room for using context to narrow the scope of item types
                -- PickItem("Interact") for qud-style "go  sit on the legendary folding chair and I won't tell you why"
        - BarterItem (if necessary)
            - PickItem("Barter")
        - Reward (based on difficulty)

*/