using System;
using System.Linq;
using System.Text;
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