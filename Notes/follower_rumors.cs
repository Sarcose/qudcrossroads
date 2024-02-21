//This is for a different mod. Plug this in when you get home and delete this file.


//AfterChangePartyLeaderEvent is the event we're listening for.
/*

Steps:
    1. Find out how to run above event check on all NPCs when they've become the player's follower
    2. Check if my party leader == player
    3. Give an item or script or part that updates my current zone coordinates
    4. If I go through an STV or player leaves me behind 


// I would have an IPlayerPart listen to it and then “register” the E.Actor 
// -if E.NewLeader is The.Player() and “unregister” the creature if it’s not or if OldLeader is the player
*/


//To listen for an event you typically override the Register method of the base IPart or Effect class.
//This is executed once when the IPart/Effect is added to the object and subsequently serialized.



/* XML:

<?xml version="1.0" encoding="utf-8"?>
<objects>
    <object Name="Player"  Load="Merge">
            <part Name="sarc_followerTracker">
    </object>
</objects>

*/


//CS
//implemented somewhere in the namespace??

                                  //namespace QudCrossroads.Dialogue
//{
//    public class QCS_Chat_Try : IConversationPart    



public override void Register(GameObject obj) {
	// Listen for when the GameObject in obj changes leader.
	obj.RegisterPartEvent(this, "AfterChangePartyLeaderEvent");

	// The otherwise identical method call used for when [this] is an Effect.
	obj.RegisterEffectEvent(this, "AwardXP");

	// Call the base Register method that we overrode.
	base.Register(obj);
}