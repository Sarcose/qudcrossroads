using System;
using System.Linq;
using System.Text;
using XRL;
using XRL.Language;
using XRL.World;
using QudCrossroads;
using static QudCrossroads.Dialogue.Elements;

namespace QudCrossroads.Dialogue
{
    public static partial class Builders
    {
        public static string TestString() //no input for now
        {
            string retStr = tester[0] + tester[1] + tester[2];
            return retStr;
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