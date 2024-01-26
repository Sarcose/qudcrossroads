using System;
using System.Linq;
using System.Text;
using XRL;
using XRL.Language;
using XRL.World;
using XRL.World.Conversations;
using XRL.World.Encounters.EncounterObjectBuilders;
using XRL.World.Parts;
//using static Hearthpyre.Static; using static QCS.Static? Copy Hearthpyre's namespace structure when we get home

/*
Here we can use IConversationPart to put together more complex logic it seems. This is done with 
<part Name="QCS_Start" Target="QCS_Followup" />
*/
namespace XRL.World.Conversations.Parts
{
    public class QCS_Chat_Try : IConversationPart //it appears to use 
        {
            public override bool WantEvent(int id, int propagation){
                XRL.Messages.MessageQueue.AddPlayerMessage("WantEventFired");
                return 
                base.WantEvent(id, propagation) 
                || id == IsElementVisibleEvent.ID
                ;
            }
            public override bool HandleEvent(IsElementVisibleEvent E)
            {
                XRL.Messages.MessageQueue.AddPlayerMessage("IsElementVisibleFired");
                GameObject speaker = The.Speaker;
                if (!speaker.IsCreature)
                {
                    return false;
                }
                if (!speaker.HasProperName)
                {
                    return false;
                }
                return base.HandleEvent(E);
            }
        }
    public class QCS_Chat_Respond : IConversationPart //it appears to use 
        {
            public override bool WantEvent(int id, int propagation){
                XRL.Messages.MessageQueue.AddPlayerMessage("WantEventFired");
                return 
                base.WantEvent(id, propagation) 
                || id == PrepareTextEvent.ID
                ;
            }
            public override bool HandleEvent(PrepareTextEvent E)
            {
                XRL.Messages.MessageQueue.AddPlayerMessage("PrepareTextEvent fired");
                string laugh = "\n\nehehehehe!";
                E.Text.Append(laugh);
                return base.HandleEvent(E);
            }
        }
}