using System;
using System.Linq;
using System.Text;
using System.Reflection;
using XRL;
using XRL.Language;
using XRL.World;
using XRL.World.Conversations;
using XRL.World.Parts;
using QudCrossroads;
using static QudCrossroads.Dialogue.Builders;

namespace QudCrossroads.Dialogue
{
    public class QCS_Chat_Try : IConversationPart    
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
    public class QCS_Chat_Respond : IConversationPart
        {
            public override bool WantEvent(int id, int propagation){
                return 
                base.WantEvent(id, propagation) 
                || id == DisplayTextEvent.ID
                ;
            }
            public override bool HandleEvent(DisplayTextEvent E)
            {
                //E.VariableReplace = true;


                //E.Text.Append("=village.activity= | =pronouns.reflexive= | =pronouns.substantivePossessive= | =pronouns.indicativeDistal= | =pronouns.subjective= kneels back down, bidding you kneel with =pronouns.objective=.");
                E.Text.Append(TestString_Ocho());
                return base.HandleEvent(E);





            }
        }
}



/*
The class members to be aware of:

XRL.The.Player
XRL.The.Speaker
XRL.The.Listener


use recursion to see namespace members:

                string testString = "||";
                Type type = typeof(XRL.The.Speaker);

                // Get all members of the class
                MemberInfo[] members = type.GetMembers();

                // Print the names of all members
                foreach (MemberInfo member in members)
                {
                    testString += member.Name;
                    testString += "||";
                }


*/
