using System;
using System.Linq;
using XRL;
using XRL.World;
using XRL.World.Conversations;
using XRL.World.Parts;
using XRL.World.Parts.Mutation;
using XRL.World.Parts.Skill;
using static Hearthpyre.Static;

namespace Hearthpyre.Dialogue.Settler
{
    public class Invite : IConversationPart
    {
        public string Target = "End";

        public override bool WantEvent(int ID, int Propagation)
        {
            return base.WantEvent(ID, Propagation)
                   || ID == IsElementVisibleEvent.ID
                   || ID == GetTargetElementEvent.ID
                   || ID == GetChoiceTagEvent.ID
                ;
        }

        public override bool HandleEvent(IsElementVisibleEvent E)
        {
            if (RealmSystem.Settlements.Count == 0) return false;

            var speaker = The.Speaker;
            if (speaker.pBrain == null) return false;
            if (speaker.OwnPart<HearthpyreSettler>()) return false;
            if (speaker.HasTagOrProperty("IncludeInSettlementInvitation")) return true;
            if (!OptionAllInvite && speaker.HasTagOrProperty("ExcludeFromDynamicEncounters")) return false;

            return base.HandleEvent(E);
        }

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
    }
}