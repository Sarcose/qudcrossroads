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


//EXAMINE QUESTS??
//Consider a new errand system, instead of putting them in a quest log
//Consider a tracking system in a journal item that opens up its own dialogue
//Attempt to create rudimentary quests

namespace QudCrossroads.QuestSystem
{
    public static partial class QuestEngine
    {
        public class QCFactionChange
        {
            public int value        {get; set; }
            public string faction   {get; set; }
        }
        public class QCQuestReward
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
            public GameObject questGiver;       //same as in vQuest
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

            public void createQuestTest(Phrase phrase, GameObject questGiver){
                int qcti = QRand(0,7);
                qcti = 0;       //for testing
                this.questGiver = questGiver;
                switch (qcti){
                    case 0:     //Gift          //Take giftItem from questGiver and give to targetNPC
                        this.giftItem = null;
                        this.targetNPC = null;
                        break;
                    case 1:     //Retrieve      //Go to targetNPC and get retrieveItem from them, then bring back to questGiver
                        this.retrieveItem = null;
                        this.targetNPC = null;
                        break;
                    case 2:     //Trade         //Take giftItem from questGiver, bring it to targetNPC and get retrieveItem, bring retrieveItem to questGiver
                        this.giftItem = null;
                        this.retrieveItem = null;
                        this.targetNPC = null;
                        break;
                    case 3:     //Request       //Gather or find any generic instance of requestItem in amount amount
                        this.requestItem = null;
                        this.amount = 0;
                        break;
                    case 4:     //ClassicFindLocation   //Generate a classic "Find the location" quest
                        break;
                    case 5:     //ClassicFindItem       //Generate a classic "Find the item" quest
                        break;
                    case 6:     //Cull          //Destroy amount of cullObject (if legendary, amount is 1)
                        this.cullObject = null;
                        this.amount = 0;
                        break;
                    case 7:     //Gossip        //Tell amount targetNPCs the conversationOnTarget       //todo: enable multiple target npc types
                        this.targetNPC = null;
                        this.conversationOnTarget = "";
                        break;
                    case 8:     //Interact      //Find interactObject and perform its contextual interaction in amount amount. If legendary, amount is 1.
                        this.interactObject = null;
                        this.amount = 0;
                        break;
                }

            }
            public void createQuestConversation(Phrase phrase){

            }
            public void addQuest(Phrase phrase){

            }
        }
    }
}