//this document is for assigning specific creatures to conversation types. It may also denote functions for procedurally assigning
//creatures to different conversation types as well, not sure yet.
using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

using XRL;
using XRL.Core;
using XRL.Rules;
using XRL.World;


namespace QudCrossroads.Dialogue
{
    public static partial class Assignments
    {   // Accessing the "Armorer" job assignments
        //List<string> armorerAssignments = jobAssignments["Armorer"];
        //int numberOfAssignments = armorerAssignments.Count;
        Dictionary<string, List<string>> jobAssignments = new Dictionary<string, List<string>>
        {
            {"Armorer", new List<string> {"Merchant"}}//, "armorer"}},
            {"Vintner", new List<string> {"Merchant"}}//, "vintner"}},
            {"Bookbinder", new List<string> {"Merchant"}}//, "scribe"}},
            {"Scribe", new List<string> {"Merchant"}}//, "scribe"}},
            {"Gemcutter", new List<string> {"Merchant"}}//, "jeweler"}},
            {"Jeweler", new List<string> {"Merchant"}}//, "jeweler"}},
            {"Glover", new List<string> {"Merchant"}}//, "clothier"}}, //generalist
            {"Grenadier", new List<string> {"Merchant"}}//, "grenadier"}}, //weapon maker generalist?
            {"Gunsmith", new List<string> {"Merchant"}}//, "gunsmith"}},
            {"Gutsmonger", new List<string> {"Merchant"}}//, "gutsmonger"}},
            {"Haberdasher", new List<string> {"Merchant"}}//, "clothier"}},
            {"Hatter", new List<string> {"Merchant"}}//, "clothier"}},
            {"IchorMerchant", new List<string> {"Merchant"}}//, "ichor"}},
            {"DiskMerchant", new List<string> {"Merchant"}}//, "Tinker"}},
            {"Shoemaker", new List<string> {"Merchant"}}//, "clothier"}},


        };
    }
        
}