using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using XRL;
using XRL.Language;
using XRL.World;
using QudCrossroads;
using QRand = QudCrossroads.Utilities.QudCrossroads_Random;
using static QudCrossroads.Dialogue.Elements;

namespace QudCrossroads.Dialogue
{
    public static partial class Builders
    {
        public static string GetRandString(List<string> strList)
        {
            return strList[QRand.Next(0,strList.Count)];
        }
        public static string TestString() //no input for now
        {
            string retStr = "";
            for (int i = 0; i < 3; i++) 
                {
                retStr = retStr + " " + testArray[QRand.Next(0, 2)];//C# uses "next" for random??
                }
            
            return retStr;
        }

        public static string TestString_Dos()
        {
            string[] names = { "Freya", "Bearjerkyton", "Starappleton", "Hindsun", "Vinewaferton", "Shushwat", "Tufi", "Biodynamic Cell", "Teal-Colored Waterskin" };
            string[] actions = { "wearing", "browsing", "ran into", "held onto", "prized dearly", "let it go", "grumbles profusely" };
            string[] locations = { "Salt and Sun", "regalia of Starappleton", "Hindsun", "Vinewaferton", "Shushwat's wares", "days long past", "a bygone era", "knollworm" };

            Random random = new Random();
            int paragraphLength = random.Next(5, 10);

            string generatedParagraph = "";

            for (int i = 0; i < paragraphLength; i++)
            {
                string name = names[random.Next(names.Length)];
                string action = actions[random.Next(actions.Length)];
                string location = locations[random.Next(locations.Length)];

                generatedParagraph += $"{name} my sister. I see you are {action} {location} - shame on you, my sister! ";

                if (random.Next(2) == 0)
                {
                    generatedParagraph += $"It would be much lovelier to see you in the regalia of {names[random.Next(names.Length)]}, your hometown. ";
                }

                if (random.Next(2) == 0)
                {
                    generatedParagraph += $"At any rate, I am of a sour disposition of late. Do you know why? ";
                }

                if (random.Next(2) == 0)
                {
                    generatedParagraph += $"I have a tale to tell. Some days past at {location} I was {action} {names[random.Next(names.Length)]}'s wares for a new {names[random.Next(names.Length)]} when I ran into {names[random.Next(names.Length)]}, my old classmate from {locations[random.Next(locations.Length)]}. ";
                    generatedParagraph += $"And what did that lack-salt do? He chose then to bring up a slight of a {locations[random.Next(locations.Length)]}; it seemed to needle him as a {locations[random.Next(locations.Length)]}. ";
                }
            }

            generatedParagraph += "*grumbles profusely*";

            return generatedParagraph;
        }

        public static string TestString_Tres()
        {
            string retStr = "";
            string whichCulture = "SaltMarshCulture";
            //List<string> convElement  = new List<string> {"Greet","Title"};
            //List<string> whichFamiliarity  = new List<string> {"unfamiliar","familiar","friendly","unfriendly"};
            List<string> stringList;
            for (int i=0; i < 5; i++)
            
            {
                //stringList = AllCultures.Cultures[whichCulture][GetRandString(convElement)].Familiarities[GetRandString(whichFamiliarity)];
                //stringList = AllCultures.Cultures["SaltMarshCulture"]Greet.Familiarities.Unfamiliar;
                string specificGreetString = AllCultures.Cultures[whichCulture].Greet["keys"].Familiarities["unfriendly"][1];
                //retStr += GetRandString(stringList);
                retStr += ", ";
            }
            return retStr;
        }


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