//The entirety of the village expansion function is here:

// Assembly-CSharp, Version=2.0.206.62, Culture=neutral, PublicKeyToken=null
// Qud.API.HistoryAPI
using System;
using System.Text;
using HistoryKit;
using XRL.Rules;
using XRL.World;

public static void __ExpandVillageText(StringBuilder Text, string Faction = null, HistoricEntitySnapshot Snapshot = null)
{
	if (Snapshot == null && !Faction.IsNullOrEmpty())
	{
		Snapshot = GetVillageSnapshot(Faction);
	}
	string newValue = "a village";
	string newValue2 = "the act of procreating";
	string newValue3 = "those who oppose arable land and the act of procreating";
	string newValue4 = "roaming around idly";
	if (Snapshot != null)
	{
		Random stableRandomGenerator = Stat.GetStableRandomGenerator(Snapshot.Name);
		GameObjectBlueprint randomElement = GameObjectFactory.Factory.GetFactionMembers(Snapshot.GetProperty("baseFaction")).GetRandomElement(stableRandomGenerator);
		newValue = Snapshot.Name;
		newValue2 = Snapshot.GetRandomElementFromListProperty("sacredThings", null, stableRandomGenerator) ?? Snapshot.GetProperty("defaultSacredThing");
		newValue3 = Snapshot.GetRandomElementFromListProperty("profaneThings", null, stableRandomGenerator) ?? Snapshot.GetProperty("defaultProfaneThing");
		newValue4 = randomElement.GetxTag_CommaDelimited("TextFragments", "Activity", null, stableRandomGenerator);
	}
	Text.Replace("=village.name=", newValue).Replace("=village.sacred=", newValue2).Replace("=village.profane=", newValue3)
		.Replace("=village.activity=", newValue4);
}
