using System;
using System.Linq;
using XRL;
using XRL.World;
using XRL.World.Conversations;
using XRL.World.Parts;
using XRL.World.Parts.Mutation;
using XRL.World.Parts.Skill;
//using static Hearthpyre.Static; using static QCS.Static? Copy Hearthpyre's namespace structure when we get home

/*
Here we can use IConversationPart to put together more complex logic it seems. This is done with 
<part Name="QCS_Start" Target="QCS_Followup" />
*/

namespace QCS.Dialogue.General
{
    public class QCSChat : IConversationPart //it appears to use 
    {
        public string Target = "End"; //why?

        public override bool WantEvent(int ID, int Propagation) //this is in SnapjawLaugh as well as Hearthpyre
        {
            return base.WantEvent(ID, Propagation) //this appears to use keys to overload HandleEvent
                   || ID == IsElementVisibleEvent.ID //Can we chat?
                   || ID == GetTargetElementEvent.ID //what is this?
                   || ID == GetChoiceTagEvent.ID //probably for determining which choice
                   || ID == PrepareTextEvent.ID //displaying text?
                ;
        }

        public override bool HandleEvent(IsElementVisibleEvent E)   //HandleEvent seems to be overloaded for most all functions in IConversationParts
        {

            var speaker = The.Speaker;                              //is this valid outside of hearthpyre? test
            if (speaker.pBrain == null) return false;               //is this valid outside of hearthpyre? test, presumably on an ooze
            //if (speaker.OwnPart<HearthpyreSettler>()) return false; //kept for reference
            //if (speaker.HasTagOrProperty("IncludeInSettlementInvitation")) return true; //kept for reference
            //if (!OptionAllInvite && speaker.HasTagOrProperty("ExcludeFromDynamicEncounters")) return false; //kept for reference
            
            //some possible checks in pseudocode
            if (speaker.hasName) return true;
            return base.HandleEvent(E);
        }
        //let's try some pseudocode to generate a conversation
        public override bool HandleEvent(PrepareTextEvent E) //PrepareTextEvent is from the SnapjawLaugh example code
        {
            E.Text.Append("\n\nehehehehe!");    
            //E.Text.Append can be used for directly loading a string out but can we do more?
            string sup = "what's up buttercup"
            E.Text.Append(sup); //is this the correct syntax?

            //string randomizedString = createRandomString(dictToLookUp); //where do functions go? where do variables go?
            //E.Text.Append(randomizedString);

            return base.HandleEvent(E);
        }
        /*
        //these were not commented out originally, they appear to be overloaded functions. Under what circumstances are ICParts overloaded?
        //oh, obviously, HandleEvent is used for multiple purposes based on input. Duh.
        public override bool HandleEvent(GetTargetElementEvent E)
        {
            var req = 50;
            var feeling = The.Speaker.pBrain.GetPersonalFeeling(The.Player) ?? 0;
            foreach (var pair in The.Speaker.pBrain.FactionMembership)
            {
                var rep = Factions.GetFeelingFactionToObject(pair.Key, The.Player);
                feeling += (int) Math.Round(pair.Value / 100f * rep);
            }

            if (The.Player.OwnPart<Customs_Tactful>()) req -= 25;
            if (The.Player.OwnPart<SociallyRepugnant>()) req += 25;

            if (feeling < req) E.Target = Target;
            return base.HandleEvent(E);
        }

        public override bool HandleEvent(GetChoiceTagEvent E)
        {
            E.Tag = "{{g|[invite]}}";
            return base.HandleEvent(E);
        }
        */
    }
}