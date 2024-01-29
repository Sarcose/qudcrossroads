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
        private static void qprintc(string message)     //move to utilities
        {
            XRL.Messages.MessageQueue.AddPlayerMessage(message);
        }
        public static string GetRandString(List<string> strList)        //move to utilities
        {
            return strList[QRand.Next(0,strList.Count-1)];
        }
        public class Phrase                 
        {
            public string Culture { get; set; }
            public string Familiarity { get; set; }
        }
        public static string LVR(string varstring)     //add more later?
        {
            return GameText.VariableReplace(varstring, null);
        }
        public delegate void ProcessFnDelegate(string name);
        private static Func<string, string> GetProcessFn(Phrase phrase)
        {
            return (name) => functionDictionary[name]?.Invoke(phrase);
        }
        //TODO:
        /*   ************ Test resultstrings for =XMLVARS= so I can put them directly into my own string declarations

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
        */

        //TODO: another wrap function that checks if your character has multiple heads, is plural, or has followers, and uses Pluralize() in response
        public static string TestString_Siete()
        {
            Phrase newPhrase = new Phrase
            {
                Culture = "SaltMarshCulture",
                Familiarity = "unfamiliar"
            };
            Func<string, string> _ = GetProcessFn(newPhrase);
            return $"{_(Greet)}, {Pluralize(_(Title))}, how are you on this day? {LVR("=MARKOVPARAGRAPH=")}";
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