using System;
using System.Linq;
using System.Text;
using XRL;
using XRL.Language;
using XRL.World;
using XRL.World.Conversations;
using XRL.World.Parts;
using QudCrossroads;
using static QudCrossroads.Dialogue.Builders;

namespace QudCrossroads.Dialogue
{
    public class QCS_Chat_Try : IConversationPart //it appears to use 
        {
            public override bool WantEvent(int id, int propagation){
                return 
                base.WantEvent(id, propagation) 
                || id == IsElementVisibleEvent.ID
                ;
            }
            public override bool HandleEvent(IsElementVisibleEvent E)
            {
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
                return 
                base.WantEvent(id, propagation) 
                || id == DisplayTextEvent.ID
                ;
            }
            public override bool HandleEvent(DisplayTextEvent E)
            {
                E.Text.Append(TestString_Siete());
                return base.HandleEvent(E);
            }
        }
}