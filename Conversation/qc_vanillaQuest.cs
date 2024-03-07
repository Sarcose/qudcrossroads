// Assembly-CSharp, Version=2.0.206.67, Culture=neutral, PublicKeyToken=null
// XRL.World.ZoneBuilders.FindASpecificSiteDynamicQuestTemplate_FabricateQuestGiver
using System;
using System.Collections.Generic;
using ConsoleLib.Console;
using Qud.API;
using XRL.Language;
using XRL.Rules;
using XRL.World.WorldBuilders;
using XRL.World;

using QudCrossroads;

namespace QudCrossroads.QuestSystem
{
	public static partial class VanillaQuest
	{
		public static Quest fabricateFindASpecificSiteQuest(GameObject giver)
		{
			typeOfDirections = "path";
			Quest quest = QuestsAPI.fabricateEmptyQuest();
			int num = Stat.Random(0, 12);
			min = 12;
			max = 18;
			targetLocation = null;
			int broaden = 0;
			while (targetLocation == null && broaden <= 240)
			{
				targetLocation = JournalAPI.GetUnrevealedMapNotesWithinZoneRadiusN(zone, min - broaden, max + broaden, questContext.IsValidQuestDestination).GetRandomElement();
				broaden++;
			}
			if (targetLocation == null)
			{
				targetLocation = JournalAPI.GetMapNotesWithinRadiusN(zone, min + broaden).GetRandomElement();
			}
			while (true)
			{
				broaden = 0;
				if (typeOfDirections == "radius")
				{
					min = 0;
					max = 1;
					landmarkLocation = null;
					landmarkLocation = JournalAPI.GetUnrevealedMapNotesWithinZoneRadiusN(targetLocation.ZoneID, min - broaden, max + broaden, questContext.IsValidQuestDestination).GetRandomElement();
					if (landmarkLocation == null)
					{
						typeOfDirections = "direction";
						continue;
					}
				}
				if (typeOfDirections == "radius_Failsafe")
				{
					min = 0;
					max = 2;
					landmarkLocation = null;
					broaden = 0;
					while (landmarkLocation == null && broaden <= 240)
					{
						landmarkLocation = JournalAPI.GetUnrevealedMapNotesWithinZoneRadiusN(targetLocation.ZoneID, min - broaden, max + broaden, questContext.IsValidQuestDestination).GetRandomElement();
						broaden++;
					}
					break;
				}
				if (typeOfDirections == "direction")
				{
					List<JournalMapNote> mapNotesInCardinalDirections = JournalAPI.GetMapNotesInCardinalDirections(targetLocation.ZoneID);
					min = 12;
					max = 18;
					for (broaden = 0; broaden <= 240; broaden += 3)
					{
						List<JournalMapNote> list = mapNotesInCardinalDirections.FindAll((JournalMapNote l) => l.ResolvedLocation.Distance(targetLocation.ResolvedLocation) >= min - broaden && l.ResolvedLocation.Distance(targetLocation.ResolvedLocation) <= max + broaden && questContext.IsValidQuestDestination(targetLocation.ResolvedLocation) && Math.Abs(l.ZoneZ - targetLocation.ZoneZ) == 0);
						if (list.Count > 0)
						{
							landmarkLocation = list.GetRandomElement();
							break;
						}
					}
					if (landmarkLocation != null)
					{
						min = Math.Max(1, landmarkLocation.ResolvedLocation.Distance(targetLocation.ResolvedLocation) - num);
						max = min + 12;
						if (landmarkLocation.ResolvedX > targetLocation.ResolvedX)
						{
							direction = "west";
						}
						if (landmarkLocation.ResolvedX < targetLocation.ResolvedX)
						{
							direction = "east";
						}
						if (landmarkLocation.ResolvedY < targetLocation.ResolvedY)
						{
							direction = "south";
						}
						if (landmarkLocation.ResolvedY > targetLocation.ResolvedY)
						{
							direction = "north";
						}
						break;
					}
					typeOfDirections = "radius_Failsafe";
				}
				else
				{
					if (!(typeOfDirections == "path"))
					{
						break;
					}
					if (questContext == null)
					{
						throw new Exception("questContext missimg");
					}
					if (questContext.worldInfo == null)
					{
						throw new Exception("worldInfo missing");
					}
					if (targetLocation == null)
					{
						throw new Exception("targetLocation missimg");
					}
					string directionToDestination;
					string directionFromLandmark;
					GeneratedLocationInfo generatedLocationInfo = questContext.worldInfo.FindLocationAlongPathFromLandmark(targetLocation.ResolvedLocation, out path, out directionToDestination, out directionFromLandmark, questContext.IsValidQuestDestination);
					if (generatedLocationInfo != null)
					{
						direction = directionFromLandmark;
						landmarkLocation = JournalAPI.GetMapNote(generatedLocationInfo.secretID);
						break;
					}
					typeOfDirections = new List<string> { "radius", "direction" }.GetRandomElement();
				}
			}
			if (landmarkLocation == null)
			{
				MetricsManager.LogError("fabricateFindASpecificSiteQuest", "Couldn't find a site!");
				return quest;
			}
			quest.Name = Grammar.MakeTitleCase(ColorUtility.StripFormatting(targetLocation.Text));
			quest.Manager = new FindASiteDynamicQuestManager(targetLocation.ZoneID, targetLocation.ID, quest.ID, "a_locate");
			quest.StepsByID = new Dictionary<string, QuestStep>();
			QuestStep questStep = new QuestStep();
			questStep.ID = "a_locate";
			questStep.Name = Grammar.MakeTitleCase("Find " + ColorUtility.StripFormatting(targetLocation.Text));
			string text = Grammar.InitLowerIfArticle(landmarkLocation.Text);
			if (typeOfDirections == "radius_Failsafe")
			{
				questStep.Text = "Locate " + targetLocation.Text + ", located within " + max + " parasangs of " + text + ".";
			}
			if (typeOfDirections == "radius")
			{
				questStep.Text = "Locate " + targetLocation.Text + ", located next to " + text + ".";
			}
			if (typeOfDirections == "direction")
			{
				questStep.Text = "Locate " + targetLocation.Text + ", located " + Math.Max(min / 3, 1) + "-" + max / 3 + " parasangs " + direction + " of " + text + ".";
			}
			if (typeOfDirections == "path")
			{
				questStep.Text = "Locate " + targetLocation.Text + ", located " + direction + " along the " + path + " that runs through " + text + ".";
			}
			questStep.XP = 100;
			questStep.Finished = false;
			quest.StepsByID.Add(questStep.ID, questStep);
			QuestStep questStep2 = new QuestStep();
			questStep2.ID = "b_return";
			questStep2.Name = Grammar.MakeTitleCase("Return to " + questContext.getQuestOriginZone());
			questStep2.Text = "Return to " + questContext.getQuestOriginZone() + " and speak to " + giver.DisplayNameOnlyDirectAndStripped + ".";
			questStep2.XP = 100;
			questStep2.Finished = false;
			quest.StepsByID.Add(questStep2.ID, questStep2);
			quest.dynamicReward = questContext.getQuestReward();
			DynamicQuestsGamestate.addQuest(quest);
			addQuestConversationToGiver(giver, quest);
			return quest;
		}
	}
}