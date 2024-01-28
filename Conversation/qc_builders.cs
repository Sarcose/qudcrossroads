using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using XRL;
using XRL.Language;
using XRL.World;
using XRL.Messages;
using QudCrossroads;
using QRand = QudCrossroads.Utilities.QudCrossroads_Random;
using static QudCrossroads.Dialogue.Elements;

namespace QudCrossroads.Dialogue
{
    public static partial class Builders
    {
        private static void qprintc(string message)
        {
            XRL.Messages.MessageQueue.AddPlayerMessage(message);
        }
        public class Phrase
        {
            public string Culture;
            public string Familiarity;
            public string ReStr;
        }
        /*public Phrase phrase = new Phrase{ 
            WhichCulture = "SaltMarshCulture",
            WhichFamiliarity = "Unfamiliar"
        }*/
        //string greeting = $"{GreetFn(phrase)}, traveler, how are you this morn?";

        public static string GetRandString(List<string> strList)
        {
            return strList[QRand.Next(0,strList.Count-1)];
        }

        public static void GreetFn(Phrase phrase)
        {
            List<string> stringList = AllCultures.Cultures[phrase.Culture].Greet["keys"].Familiarities[phrase.Familiarity];
            phrase.ReStr += GetRandString(stringList);
        }

        public static string TestString_Quatro()
        {
            Phrase phrase = new Phrase{
                Culture = "SaltMarshCulture",
                Familiarity = "unfamiliar",         //TODO: decide on case for all of these. Probably upper case.
                ReStr = ""
            };
            for (int i=0; i < 5; i++)
            {
                GreetFn(phrase);
                phrase.ReStr += ", ";
            }
            return phrase.ReStr;
        }
        //we will need separate functions for each of Greet, Title, and all others
        //however I find this probably makes sense in the long run as they will grow in complexity per-member.


        /*public Phrase phrase = new Phrase{ 
            WhichCulture = "SaltMarshCulture",
            WhichFaamiliarity = "Unfamiliar"
        }*/
        //string greeting = $"{GreetFn(phrase)}, traveler, how are you this morn?";




        public static string OutfitNotice(GameObject player, string curString)
        {
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