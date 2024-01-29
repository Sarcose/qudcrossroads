using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using XRL;
using XRL.Language;
using static XRL.Language.Grammar;
using XRL.World;
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
        public delegate void ProcessFnDelegate(string name);
        private static Func<string, string> GetProcessFn(Phrase phrase)
        {
            return (name) => functionDictionary[name]?.Invoke(phrase);
        }

        public static string TestString_Ces()
        {
            Phrase newPhrase = new Phrase
            {
                Culture = "SaltMarshCulture",
                Familiarity = "unfamiliar"
            };

            Func<string, string> _ = GetProcessFn(newPhrase);
            return $"{_(Greet)}, {_(Title)}, how are you on this day? {_(Greet)}, {_(Title)} {_(Greet)}, {_(Title)} {_(Greet)}, {_(Title)} {_(Greet)}, {_(Title)} {_(Greet)}, {_(Title)}";
        }

        public static string TestString_Pluralize()
        {
            return Pluralize("human") + Pluralize("person") + Pluralize("dog") + Pluralize("plant") + Pluralize("child");
        }
        public static string OutfitNotice(GameObject player, string curString)
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