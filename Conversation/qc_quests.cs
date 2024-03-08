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

Notes regarding quest generation in Jademouth:

Chaos Theory is a quest that asks for a specific item and then makes nonstandard modifications to a character and environment using CSharp. It's perfect.

The text of the quest is statically declared in Quests.xml using 
                <quest Name="Chaos Theory" Level="25" Manager="Ava_Jademouth_ChaosTheoryManager" ID="Ava_Jademouth_ChaosTheory" 
It additionally has <step ID="IDNAME"> </step> and <step ID="CollectReward"> </step> which ends up creating a dialogue to select from tinkering recipes

Wishes.cs contains a declaration for a wish for testing the quest 'ChaosTheoryTest'


It uses custom objects in Objects.xml such as 
	</object>
	<object Name="Ava_Jademouth_ChaosTheorySconceSpawner" Inherits="Widget">
	</object>


in Bright_Conversations.xml:
			<choice Target="GiveStatic" IfHaveLiquid="warmstatic" IfHaveActiveQuest="Ava_Jademouth_ChaosTheory" CompleteQuestStep="Ava_Jademouth_ChaosTheory~FindStatic" UseLiquid="warmstatic">
				<text>I have your static. Take it.</text>
			</choice>
			<choice Target="Project" IfNotHaveQuest="Ava_Jademouth_ChaosTheory">What are you working on?</choice>
			<choice Target="Reminder" IfHaveActiveQuest="Ava_Jademouth_ChaosTheory">Remind me what you want me to find for you again?</choice>

Here we see that the conversation logic uses custom parts such as "Ava_Jademouth_BrightRewardingPending" to establish additional choices,
    leading to further iparts that run CS code. This is where the custom quest behavior comes in.

<start ID="Greeting_PostQuest" IfFinishedQuest="Ava_Jademouth_ChaosTheory">
			<text>=name=! How lovely to see you. What can I do for you?
			</text>
			<choice Target="GiveReward" IfHaveProperty="Ava_Jademouth_BrightRewardingPending">You mentioned that you could help me learn item mods?</choice>
			<choice Target="Mutations">How are things?</choice>
			<choice Target="Cat_PostQuest" IfTestState="Ava_Jademouth_CatMentionedBright">Do you get along with Cat these days?</choice>
			<choice Target="End">Live and drink, Bright.</choice>
		</start>

Here, we see that the entry CallScript="Scriptname" is used to run the custom CS code that creates the tinker recipe choice.
    --This is a direct accessing of the relevant member in the namespace contained in ChaosTheoryManager.cs
        <node ID="GiveReward">
			<text>That is correct. Would you like to learn now? You need not know the requisite skills -- anything I teach you may be put into practice even if you only know the basics of tinkering.</text>
			<choice Target="End" CallScript="XRL.World.QuestManagers.Ava_Jademouth_ChaosTheoryManager.HandleReward">Yes, teach me now.</choice>
			<choice Target="End">Perhaps not yet. Live and drink.</choice>
		</node>



--==XML elements:==--
quest Name="QUESTNAME" Manager="MANAGER"  -- this is necessary for creating the quest entry in the journal. We need to see if we can use Delegates to create this.

<choice Target="GiveStatic" IfHaveLiquid="warmstatic" IfHaveActiveQuest="Ava_Jademouth_ChaosTheory" CompleteQuestStep="Ava_Jademouth_ChaosTheory~FindStatic" UseLiquid="warmstatic">
    Target = "SOMEQUESTCONTEXT"      IfHaveItem     IfHaveActiveQuest    CompleteQuestStep     UseLiquid
    All of these need to be generated and modular. IfHave could *potentially* be determined by a script that returns a bool.
            "" crumpled sheet of paper: there is not; generally that would be writing your own delegate"" 
        look at using a "Quest dialogue" generic XML with a custom delegate to create the info instead of the below

<choice in general> -- can we add this programmatically? Let's check ILSpy


[ ] HERE IS OUR PRIMARY KEY FOR CONVERSATIONS and probably how we add the above XML members
from AddQuestConversationToGiver:
    most of the function is logic to generate the string (I can do that, fine) and then we have this gem here:

        ConversationXMLBlueprint questIntroNode = ConversationsAPI.AddNode(conversationXMLBlueprint, null, text2);
        ConversationXMLBlueprint questIntroNode = ConversationsAPI.AddNode(conversationXMLBlueprint, null, text2);
        ConversationXMLBlueprint conversationXMLBlueprint2 = DynamicQuestConversationHelper.fabricateIntroAcceptChoice("Yes. I will locate " + targetLocation.Text + "&G as you ask.", questIntroNode, quest);
        conversationXMLBlueprint2["IfNotHaveMapNote"] = targetLocation.ID;
        conversationXMLBlueprint2["RevealMapNote"] = landmarkLocation.ID;
        DynamicQuestConversationHelper.fabricateIntroRejectChoice("No, I will not.", questIntroNode);
        ConversationXMLBlueprint conversationXMLBlueprint3 = DynamicQuestConversationHelper.fabricateIntroAdditionalChoice("I already know where " + targetLocation.Text + " is.", questIntroNode);
        conversationXMLBlueprint3["StartQuest"] = quest.ID;
        conversationXMLBlueprint3["IfNotHaveQuest"] = quest.ID;
        conversationXMLBlueprint3["IfHaveMapNote"] = targetLocation.ID;
        conversationXMLBlueprint3["RevealMapNote"] = landmarkLocation.ID;
        ConversationXMLBlueprint conversationXMLBlueprint4 = ConversationsAPI.AddPart(conversationXMLBlueprint3, "QuestHandler");
        conversationXMLBlueprint4["QuestID"] = quest.ID;
        conversationXMLBlueprint4["StepID"] = "a_locate~b_return";
        conversationXMLBlueprint4["Action"] = "Step";
        DynamicQuestConversationHelper.appendQuestCompletionSequence(conversationXMLBlueprint, quest, questIntroNode, "I've located " + targetLocation.Text + ".", "I haven't located " + targetLocation.Text + " yet.", delegate
        {
        }, delegate(ConversationXMLBlueprint gotoAcceptNodeFinalizer)
        {
            gotoAcceptNodeFinalizer["IfNotFinishedQuest"] = quest.ID;
            gotoAcceptNodeFinalizer["IfFinishedQuestStep"] = quest.ID + "~a_locate";
        }, delegate(ConversationXMLBlueprint incompleteNodeFinalizer)
        {
            incompleteNodeFinalizer["IfNotFinishedQuest"] = quest.ID;
            incompleteNodeFinalizer["IfNotFinishedQuestStep"] = quest.ID + "~a_locate";
        }, delegate(ConversationXMLBlueprint completeNodeFinalizer)
        {
            completeNodeFinalizer["CompleteQuestStep"] = quest.ID + "~b_return";
        }, delegate
        {
        });

    

*/










/*
[ ] Pull multiple mods that add quests and analyze them for XML vs C# content.
[ ] Document the workflow of creating a new quest
[ ] Parse out the essential elements of every desired quest type of mine in order to determine how to integrate them into the existing system

*/


namespace QudCrossroads.QuestSystem
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