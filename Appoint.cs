using XRL;
using XRL.Language;
using XRL.World;
using XRL.World.Conversations;
using XRL.World.Parts;
using XRL.World.ZoneBuilders;

namespace Hearthpyre.Dialogue.Settler
{
	public class Appoint : IConversationPart
	{
		public string Retainer;
		public string Stock;
		public string Token;
		public int Max;

		public override void Awake()
		{
			Max = Static.GetMaxAppointedRetainers();
		}

		public override bool WantEvent(int ID, int Propagation)
		{
			return base.WantEvent(ID, Propagation)
			       || ID == IsElementVisibleEvent.ID
			       || ID == EnteredElementEvent.ID
			       || ID == GetChoiceTagEvent.ID
				;
		}

		public override bool HandleEvent(IsElementVisibleEvent E)
		{
			if (Max <= 0 || Retainer.IsNullOrEmpty()) return false;
			if (Token.IsNullOrEmpty() || !The.Player.Inventory.HasObject(Token)) return false;

			var settlement = The.Speaker.TakePart<HearthpyreSettler>()?.Settlement;
			if (settlement == null) return false;
			if (settlement.GetRetainer(The.Speaker) > 0) return false;
			if (settlement.CountRetainer(Retainer) >= Max) return false;

			return base.HandleEvent(E);
		}

		public override bool HandleEvent(EnteredElementEvent E)
		{
			var speaker = The.Speaker;
			if (Stock != null)
			{
				speaker.SetStringProperty("GenericInventoryRestockerPopulationTable", Stock);
				speaker.IncludePart<GenericInventoryRestocker>();
				speaker.IncludePart<HearthpyreStockUpgrader>();
			}

			var template = "SpecialVillagerHeroTemplate_" + Retainer;
			if (GameObjectFactory.Factory.Blueprints.ContainsKey(template))
			{
				Specialize(speaker, template);
				AddProperties(speaker);
			}


			var settlement = speaker.TakePart<HearthpyreSettler>()?.Settlement;
			if (settlement == null) return false;

			settlement.Retainers[speaker.id] = Static.RetainerMap[Retainer];
			var token = The.Player.Inventory.FindObjectByBlueprint(Token);
			token.SplitFromStack();
			token.Destroy(Silent: true);

			return base.HandleEvent(E);
		}

		public override bool HandleEvent(GetChoiceTagEvent E)
		{
			E.Tag = "{{g|[Present token]}}";
			return base.HandleEvent(E);
		}

		public void Specialize(GameObject Object, string Special)
		{
			var name = Object.pRender.DisplayName;
			var C = Object.CurrentCell;
			var Z = C.ParentZone;
			var visibility = Z.GetVisibility(C.X, C.Y);

			// Dodge level up messages with the wrong name applied by hiding object.
			Object.pPhysics.DidX("become", Grammar.A(Retainer.ToLower()), "!", ColorAsGoodFor: Object);
			Z.SetVisibility(C.X, C.Y, false);
			if (!Object.IsHero()) HeroMaker.MakeHero(Object, null, Special);
			Object.SetTitle(name, Special);
			Object.IncludePart<Interesting>();
			Object.DiscardPart<HasGuards>();
			Object.DiscardPart<HasThralls>();
			Z.SetVisibility(C.X, C.Y, visibility);
			Static.PlayUISound(Static.SND_LVLO);
		}

		public void AddProperties(GameObject Object)
		{
			if (Retainer == "Warden" && !Object.pBrain.IsFactionMember("Wardens"))
			{
				Object.pBrain.setFactionMembership("Wardens", 50);
			}
			else if (Retainer == "Tinker")
			{
				Object.AddSkill("Tinkering");
				Object.AddSkill("Tinkering_Repair");
				Object.AddSkill("Tinkering_Tinker1");
			}
			else if (Retainer == "Apothecary")
			{
				Object.AddSkill("CookingAndGathering");
				Object.AddSkill("CookingAndGathering_Harvestry");
				Object.AddSkill("CookingAndGathering_Butchery");
			}
		}
	}
}
