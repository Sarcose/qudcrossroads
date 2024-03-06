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

/*

[ ] Evaluate that all strings show in QuestTest and a Quest is created
[ ] Create a random quest and follow it all the way through, validating it is working
[ ] Custom quest using the vanilla system adding minimum or zero steps
[ ] Custom quest, using the vanilla system, and multiple followup quests
[ ] Custom quest with "hook" properties that QCQuest can use instead -- with custom dialogue for quest elements not supported by vanilla
[ ] As above, now go through every quest type and test them one by one. At this point or perhaps before we will need QCQuestHandler

*/

namespace QudCrossroads.Dialogue
{
    public static partial class QuestEngine
    {
        public class QCFactionChange
        {
            public int value        {get; set; }
            public string faction   {get; set; }
        }
        public class QCQuestReward  //*DO* we need this? Idunno bruhv
        {
            public int XP;
            public List<string> items {get; set; }
            public QCFactionChange[] factions {get; set;}
        }
        public class QCQuest    //some example members
        {   
            public Quest vQuest;                //original quest
            public QCQuestReward reward;
            public string QCType;               //"Gift "Retrieve" "Trade" "Request" "Classic" "Cull" "Gossip" "Interact"
            public int difficulty;              // which determines how far away a quest is
            public string ZoneID;               //retrieved by some magical algorithm which is then put into vQuest and here
            public GameObject giftItem;         //given by QuestGiver to give to TargetNPC
            public GameObject retrieveItem;     //given by TargetNPC, in a Trade there is both this and the above
            public GameObject requestItem;      //retrieve from somewhere
            public GameObject cullObject;       //destroy this object (can be a creature or non)
            public GameObject targetNPC;        //speak to this NPC, take a gift to this NPC, or retrieve from this NPC
            public GameObject interactObject;   //interact with this object, which will actually require a contextual interaction verb like "SIT on the chair"
            public GameObject QuestGiver;       //same as in vQuest
            public int amount;                  //amount if applicable
            public String conversationOnTarget; //conversation to add to the target -- for GossipObject or giftItem like "I'm Basch Von Ronsenberg of Dalmasca!"
            //we now need methods to interface with own vQuest.
            public T get<T>(string memberName){     //for use with conversation building so we can easily translate member variables
                var property = GetType().GetProperty(memberName);
                if (property != null && property.PropertyType == typeof(T))
                {
                    return (T)property.GetValue(this);
                }
                var vqProperty = vQuest?.GetType().GetProperty(memberName);
                if (vqProperty != null && vqProperty.PropertyType == typeof(T))
                {
                    return (T)vqProperty.GetValue(vQuest);
                }
                return default;
            }
        }
    }
}
/*

code from ilspy:

public Quest fabricateFindASpecificSiteQuest(GameObject giver)
public Quest fabricateFindASpecificItemQuest(GameObject giver, string objectToFetchCacheID)
Quest quest = QuestsAPI.fabricateEmptyQuest();
                //do things to quest


addQuestConversationToGiver(giver, quest);      //mine this for quest information to figure out how to generate quest from convo                


//probably highly relevant members here:
//public Dictionary<string, object> Properties;             //I don't see "properties" anywhere.
//public Dictionary<string, int> IntProperties;
//public Dictionary<string, QuestStep> StepsByID;           //QuestStep questStep = new QuestStep(), then do things to it
//public DynamicQuestReward dynamicReward
//Somewhere hidden in the fabricate function is code that solidifies a location, item, landmark

//relevant to the building of quests is finding locations and items that are unclaimed, which already exist
//relevant even moreso is the building of quest steps, which is only a little more complicated

//*we* will be spawning items wholesale into zones for quests, as well as NPCs, and claiming pre-existing NPCs that have names
        //pick a zone. Is zone ungenerated? If not, generate zone. Then, pick NPCs and items from it
        // public Zone ZoneManager.GetZone(string ZoneID)

        //the Zone class is huge. We need to find NPCs and Items and that's about it. Parsing it will take a while.
        //public void FindObjects(List<XRL.World.GameObject> Store, Predicate<XRL.World.GameObject> Filter)
        //public List<XRL.World.GameObject> FindObjectsWithTagOrProperty(string Name)
        //will need to find out what the object list is and figure out how to parse it for:
            //NPCs
            //Items/Furniture

    //Needed: API calls that can advance the vQuest steps when appropriate


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