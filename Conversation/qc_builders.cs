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
using static QudCrossroads.Dialogue.Functions;

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
            public string Culture { get; set; }
            public string Familiarity { get; set; }
            public string ReStr { get; set; }
        }
        public static string GetRandString(List<string> strList)
        {
            return strList[QRand.Next(0,strList.Count-1)];
        }
        private static Dictionary<string, Action<Phrase>> functionDictionary = new Dictionary<string, Action<Phrase>>
        {
            { "Greet", GreetFn },
            { "Title", TitleFn }
        };
        public delegate void ProcessFnDelegate(string name);
        private static Action<string> GetProcessFn(Phrase phrase)
        {
            return (name) => functionDictionary[name]?.Invoke(phrase);
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
        public static string TestString_Sinco()
        {
                Phrase newPhrase = new Phrase
                {
                    Culture = "SaltMarshCulture",
                    Familiarity = "unfamiliar"
                };
                string element = "Greet";

                // Get the process function based on the newPhrase
                Action<string> localProcessFn = GetProcessFn(newPhrase);

                // Invoke the process function with the provided element
                for (int i = 0; i < 10; i++)
                {
                    localProcessFn(element);
                }
                return newPhrase.ReStr;
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