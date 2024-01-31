using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using XRL;
using XRL.Language;
using static XRL.Language.Grammar;
using XRL.World;
using XRL.World.Conversations;
using XRL.World.Parts;
using XRL.Messages;

using QudCrossroads;
using QRand = QudCrossroads.Utilities.QudCrossroads_Random;
using static QudCrossroads.Dialogue.Elements;
using static QudCrossroads.Dialogue.Functions;

namespace QudCrossroads.Dialogue
{
    public static partial class Builders
    {
/***************************************************************/
//          Utility Functions for Phrase Building              //
/***************************************************************/
        private static void qprintc(string message)     //move to utilities
        {
            XRL.Messages.MessageQueue.AddPlayerMessage(message);
        }
        public static string GetRandString(params List<string>[] strArrays)     // result = GetRandString(stringList, stringList2, stringList3, stringList4, etc...)
        {
            int totalCount = 0;
            foreach (var strArray in strArrays)
            {
                totalCount += strArray.Count;
            }
            int randomIndex = QRand.Next(totalCount);
            foreach (var strArray in strArrays)
            {
                if (randomIndex < strArray.Count) {return strArray[randomIndex]; }
                else { randomIndex -= strArray.Count; }
            }
            qprintc("GetRandString error - no elements found.");
        }
        public static int GetRandStringIndex(params List<string>[] strArrays)
        {
            int totalCount = 0;
            foreach (var strArray in strArrays)
            {
                totalCount += strArray.Count;
            }
            return QRand.Next(totalCount);
        }
        public static string GetSpecificString(int index, params List<string>[] strArrays)
        {
            int totalCount = 0;
            foreach (var strArray in strArrays)
            {
                totalCount += strArray.Count;
            }
            foreach (var strArray in strArrays)
            {
                if (index < strArray.Count) {return strArray[index]; }
                else { index -= strArray.Count; }
            }
            qprintc("GetSpecificString error - no elements found.");
        }
        public static string LVR(string varstring)     //add more later?
        {
            XRL.Messages.MessageQueue.AddPlayerMessage(varstring);
            return GameText.VariableReplace(varstring, null);
        }
        public delegate void ProcessFnDelegate(string name);
        private static Func<string, string> GetProcessFn(Phrase phrase)
        {
            return (name) => functionDictionary[name]?.Invoke(phrase);
        }

/***************************************************************/
//                      Phrase Class                           //
/***************************************************************/
        public class Phrase                 
        {
            public string Culture { get; set; }
            public string Familiarity { get; set; }
        }




        
/***************************************************************/
//                      Testing Area                           //
/***************************************************************/
        //TODO:
        /*  

        UPDATE: VariableReplace in /Qud/ on Drive has the majority of the actual =variable= replacement logic.
        

        here is the code in source:
        
            public override bool HandleEvent(GetDisplayNameEvent E)
            {
                foreach (string key in E.DB.Keys)
                {
                    if (key.Contains("="))
                    {
                        string text = GameText.VariableReplace(key, ParentObject);          --here is the key to deciphering this
                        if (text != key)
                        {
                            E.DB.Add(text, E.DB[key]);
                            E.DB.Remove(key);
                        }
                    }
                }
                return base.HandleEvent(E);
            }




        We need to:
                parse the returned string
                check for an *ENCLOSED* =variable= because that's how it will be written
                replace enclosed variable with the replacer
                concatenate back into the string // rebuild the string with concatenated

        Standardization:
            If an element has a =variable= it must be at the end -- will require parsing a bit (everything after = is the variable) and replacement
                Example: "my dear =gendersib=" //(not an actual variable tag)
            
            ## Working with LVR currently:
                =MARKOVPARAGRAPH=
                =verb:grab= -- grabbed from the mental mutation text, but does not become "grabs" in this usage
                =alchemist= -- seems to generate a randomized 'alchemist' babble
                =prounouns.siblingTerm= -- probably gives a generic if i had to guess
                =factionaddress:Barathrumites= --this is probably a global variable reference to the player
                =factionaddress:Mopango= --same as above
                =GGREESULT= -- Grit Gate attack assessment (won't need this for Qud Crossing)
                =MarkOfDeath= -- Doubt I'll need this
                =MARKOVCORVIDSENTENCE=
                =MARKOVSENTENCE=
                =MARKOVWATERBIRDSENTENCE=
                =name=  - player name
                =object.nameSingle= -   i suspect this may be contextual and grabbed a generic
                =player.apparentSpecies=
                =player.formalAddressTerm=
                =player.offspringTerm=
                =player.personTerm=
                =player.reflexive=
                =player.siblingTerm=
                =pronouns.formalAddressTerm= -- this produced "friend" however, for whatever reason
                =pronouns.personTerm= --this produced 'human'
                =pronouns.possessive=
                =pronouns.siblingTerm=
                =pronouns.subjective=
                =subject.refname= --again, by producing "thing", this leads me to believe it is contextual and processed here without context.
                =SULTAN4=       -- comes up with a Sultan
                =V0tinkeraddendum= - dunno what this does
                =verb:grab=
                =verb:tug=

            ## Not working   -- ime these are looking for contextual variables that would be set in the XML interp
                =subject.waterRitualLiquid= -- the 'subject' here is probably the source. I don't want to custom roll a string parsing alg tho.
                =subject.T=
                =all.influence=
                =circumstance.influence=
                =hermit=
                =motive.influence=-
                =mount.complete.days=
                =mutation.name=
                =pluralize=     -- this strikes me as setting a temporary flag for the XML interp. Use PLuralize()
                =recipe=
                =thief.name=    ----
                =village.activity=  --\
                =village.name=      --|
                =village.profane=   --| //TODO: need to find out how to trigger these specifically. I'm sure there's simply a context I need to invoke. 
                =village.sacred=    --| -- I am imagining my LVR function needs another layer of assertions to build these various contextual phrases
                =villageZeroName=   --/

            ## Crashes the conversation outright
                =generic=       --looks like this is actually a NOOP to establish the format.
        */
        //Usage examples:
        // {_(Greet)}
        // {Pluralize(_(Title))}
        // {LVR("=verb:grab=")}
        //
        //
        //TODO: another wrap function that checks if your character has multiple heads, is plural, or has followers, and uses Pluralize() in response
        public static string TestString_Siete()
        {
            Phrase newPhrase = new Phrase
            {
                Culture = "SaltMarshCulture",
                Familiarity = "unfamiliar"
            };
            Func<string, string> _ = GetProcessFn(newPhrase);
            return $"{_(Greet)}, {Pluralize(_(Title))}, how are you on this day? {LVR("=verb:grab=")}";
        }
        public static string OutfitNotice(GameObject player, string curString)  //probably need to change GameObject player tbh...
        {
            //The.Listener.GetEquippedObjects().Any((Obj) => Obj.HasPart<GivesRep>()) // will need to import System.Linq and XRL
            //The.Listener.GetEquippedObjects().Any((obj) => obj.GetPart("AddsRep") is AddsRep p && p.CachedCommaExpansion().Contains(Faction) && p.AppliedBonus);



            /*
            if player.isWearing(thingIDoLike){
                curString += $"Ahh, it is {positiveAdjective} to see you in {myTown}'s colors! \n{transitionFrom}, "
            }    
            elseif player.isWearing(thingIDontLike){
                curString += $"I see you are wearing the {insult} {rivalTown}'s colors. Shame on you, {title}! It would be much {positiveComparativeAdjective} to see you
                in the regalia of {myTown}! \n{transitionFrom}, "
            }
            return curString;
            //todo: make these more dynamic
            */
            return curString;
        }


    }


}


/*
public static string MyFirstName (string userID)
{
        string sReturnString = null;
    using (OdbcConnection Cn = new OdbcConnection(GlobalVariables.DatabaseConnectionString)) //Access database
    {
        Cn.Open();
        OdbcCommand cmd = Cn.CreateCommand();
        cmd.CommandText = "SELECT FirstName FROM TableUser WHERE Username = '" + userID + "'";
        OdbcDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            sReturnString = (string)reader.GetValue(0);
            break;
        } 
     }
     return sReturnString;
}
*/