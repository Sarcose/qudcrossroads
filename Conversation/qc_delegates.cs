//using directives derived from ImpatientShopper. Seems to be a good universal start.
using System;
using System.Linq;
using System.Text;
using XRL;
using XRL.Language;
using XRL.World;
using XRL.World.Conversations;
using XRL.World.Encounters.EncounterObjectBuilders;
using XRL.World.Parts;
//using static ImpatientShopper.TradeHelper;
//this was in impatientshopper but may not be necessary
//we may also end up using a member in this way of QudCrossroads.member?

/*
Delegates to do:

checkInfamousItems: check player inventory for legendary items with -rep for village, only done by Mayors
            (if the village doesn't have a mayor then maybe whoever the quest giver is?)

*/
/*
namespace QudCrossroads
{
    public static class DelegateContainer
    {
        // A predicate that receives a DelegateContext object with our values assigned, this to protect mods from signature breaks.
        [ConversationDelegate(Speaker = true)]
        public static bool IfHaveItem(DelegateContext Context)
        {
            // Context.Value holds the quoted value from the XML attribute.
            // Context.Target holds the game object.
            // Context.Element holds the parent element.
            return Context.Target.HasObjectInInventory(Context.Value);
        }
    }
}*/