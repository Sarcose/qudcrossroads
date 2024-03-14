/*

Notes regarding quest generation in Jademouth:

Chaos Theory is a quest that asks for a specific item and then makes nonstandard modifications to a character and environment using CSharp. It's perfect.

The text of the quest is statically declared in Quests.xml using 
                <quest Name="Chaos Theory" Level="25" Manager="Ava_Jademouth_ChaosTheoryManager" ID="Ava_Jademouth_ChaosTheory" 
It additionally has <step ID="IDNAME"> </step> and <step ID="CollectReward"> </step> which ends up creating a dialogue to select from tinkering recipes

Wishes.cs contains a declaration for a wish for testing the quest 'ChaosTheoryTest'


It uses custom objects in Objects.xml such as 
	</object>
	<object Name="Ava_Jademouth_ChaosTheorySconceSpawner" Inherits="Widget">
	</object>


in Bright_Conversations.xml:
			<choice Target="GiveStatic" IfHaveLiquid="warmstatic" IfHaveActiveQuest="Ava_Jademouth_ChaosTheory" CompleteQuestStep="Ava_Jademouth_ChaosTheory~FindStatic" UseLiquid="warmstatic">
				<text>I have your static. Take it.</text>
			</choice>
			<choice Target="Project" IfNotHaveQuest="Ava_Jademouth_ChaosTheory">What are you working on?</choice>
			<choice Target="Reminder" IfHaveActiveQuest="Ava_Jademouth_ChaosTheory">Remind me what you want me to find for you again?</choice>

Here we see that the conversation logic uses custom parts such as "Ava_Jademouth_BrightRewardingPending" to establish additional choices,
    leading to further iparts that run CS code. This is where the custom quest behavior comes in.

<start ID="Greeting_PostQuest" IfFinishedQuest="Ava_Jademouth_ChaosTheory">
			<text>=name=! How lovely to see you. What can I do for you?
			</text>
			<choice Target="GiveReward" IfHaveProperty="Ava_Jademouth_BrightRewardingPending">You mentioned that you could help me learn item mods?</choice>
			<choice Target="Mutations">How are things?</choice>
			<choice Target="Cat_PostQuest" IfTestState="Ava_Jademouth_CatMentionedBright">Do you get along with Cat these days?</choice>
			<choice Target="End">Live and drink, Bright.</choice>
		</start>

Here, we see that the entry CallScript="Scriptname" is used to run the custom CS code that creates the tinker recipe choice.
    --This is a direct accessing of the relevant member in the namespace contained in ChaosTheoryManager.cs
        <node ID="GiveReward">
			<text>That is correct. Would you like to learn now? You need not know the requisite skills -- anything I teach you may be put into practice even if you only know the basics of tinkering.</text>
			<choice Target="End" CallScript="XRL.World.QuestManagers.Ava_Jademouth_ChaosTheoryManager.HandleReward">Yes, teach me now.</choice>
			<choice Target="End">Perhaps not yet. Live and drink.</choice>
		</node>

--==XML elements:==--
quest Name="QUESTNAME" Manager="MANAGER"  -- this is necessary for creating the quest entry in the journal. We need to see if we can use Delegates to create this.

<choice Target="GiveStatic" IfHaveLiquid="warmstatic" IfHaveActiveQuest="Ava_Jademouth_ChaosTheory" CompleteQuestStep="Ava_Jademouth_ChaosTheory~FindStatic" UseLiquid="warmstatic">
    Target = "SOMEQUESTCONTEXT"      IfHaveItem     IfHaveActiveQuest    CompleteQuestStep     UseLiquid
    All of these need to be generated and modular. IfHave could *potentially* be determined by a script that returns a bool.
            "" crumpled sheet of paper: there is not; generally that would be writing your own delegate"" 
        look at using a "Quest dialogue" generic XML with a custom delegate to create the info instead of the below

<choice in general> -- can we add this programmatically? Let's check ILSpy


[ ] HERE IS OUR PRIMARY KEY FOR CONVERSATIONS and probably how we add the above XML members
from AddQuestConversationToGiver:
    most of the function is logic to generate the string (I can do that, fine) and then we have this gem here:

        ConversationXMLBlueprint questIntroNode = ConversationsAPI.AddNode(conversationXMLBlueprint, null, text2);
        ConversationXMLBlueprint questIntroNode = ConversationsAPI.AddNode(conversationXMLBlueprint, null, text2);
        ConversationXMLBlueprint conversationXMLBlueprint2 = DynamicQuestConversationHelper.fabricateIntroAcceptChoice("Yes. I will locate " + targetLocation.Text + "&G as you ask.", questIntroNode, quest);
        conversationXMLBlueprint2["IfNotHaveMapNote"] = targetLocation.ID;
        conversationXMLBlueprint2["RevealMapNote"] = landmarkLocation.ID;
        DynamicQuestConversationHelper.fabricateIntroRejectChoice("No, I will not.", questIntroNode);
        ConversationXMLBlueprint conversationXMLBlueprint3 = DynamicQuestConversationHelper.fabricateIntroAdditionalChoice("I already know where " + targetLocation.Text + " is.", questIntroNode);
        conversationXMLBlueprint3["StartQuest"] = quest.ID;
        conversationXMLBlueprint3["IfNotHaveQuest"] = quest.ID;
        conversationXMLBlueprint3["IfHaveMapNote"] = targetLocation.ID;
        conversationXMLBlueprint3["RevealMapNote"] = landmarkLocation.ID;
        ConversationXMLBlueprint conversationXMLBlueprint4 = ConversationsAPI.AddPart(conversationXMLBlueprint3, "QuestHandler");
        conversationXMLBlueprint4["QuestID"] = quest.ID;
        conversationXMLBlueprint4["StepID"] = "a_locate~b_return";
        conversationXMLBlueprint4["Action"] = "Step";
        DynamicQuestConversationHelper.appendQuestCompletionSequence(conversationXMLBlueprint, quest, questIntroNode, "I've located " + targetLocation.Text + ".", "I haven't located " + targetLocation.Text + " yet.", delegate
        {
        }, delegate(ConversationXMLBlueprint gotoAcceptNodeFinalizer)
        {
            gotoAcceptNodeFinalizer["IfNotFinishedQuest"] = quest.ID;
            gotoAcceptNodeFinalizer["IfFinishedQuestStep"] = quest.ID + "~a_locate";
        }, delegate(ConversationXMLBlueprint incompleteNodeFinalizer)
        {
            incompleteNodeFinalizer["IfNotFinishedQuest"] = quest.ID;
            incompleteNodeFinalizer["IfNotFinishedQuestStep"] = quest.ID + "~a_locate";
        }, delegate(ConversationXMLBlueprint completeNodeFinalizer)
        {
            completeNodeFinalizer["CompleteQuestStep"] = quest.ID + "~b_return";
        }, delegate
        {
        });

    

*/



