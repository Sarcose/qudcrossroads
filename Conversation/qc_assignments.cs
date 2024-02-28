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
        //int numberOfAssignments = armorerAssignments.Count; //loop through it

        public static Dictionary<string, Phrase> phraseDefaults = new Dictionary<string, Phrase>{
            //*********Merchant
            {"Armorer",        new Phrase { profession = "Merchant" }},       
            {"Vintner",        new Phrase { profession = "Merchant" }},
            {"Bookbinder",     new Phrase { profession = "Merchant" }},
            {"Scribe",         new Phrase { profession = "Merchant" }},
            {"Gemcutter",      new Phrase { profession = "Merchant" }},
            {"Jeweler",        new Phrase { profession = "Merchant" }},
            {"Glover",         new Phrase { profession = "Merchant" }},
            {"Grenadier",      new Phrase { profession = "Merchant" }},
            {"Gunsmith",       new Phrase { profession = "Merchant" }},
            {"Gutsmonger",     new Phrase { profession = "Merchant" }},
            {"Haberdasher",    new Phrase { profession = "Merchant" }},
            {"Hatter",         new Phrase { profession = "Merchant" }},
            {"IchorMerchant",  new Phrase { profession = "Merchant" }},
            {"DiskMerchant",   new Phrase { profession = "Merchant" }},
            {"HumanTinker1",   new Phrase { profession = "Merchant" }},
            {"Shoemaker",      new Phrase { profession = "Merchant" }},
            //********Farmers**
            {"AmoebaFarmer",   new Phrase { profession = "Farmer" }},
            {"AppleMerchant",  new Phrase { profession = "Farmer" }},
            {"Beekeeper",      new Phrase { profession = "Farmer" }},
            {"BeetleFarmer",   new Phrase { profession = "Farmer" }},
            {"CatHerder",      new Phrase { profession = "Farmer" }},
            {"CrabFarmer",     new Phrase { profession = "Farmer" }},
            {"GoatHerder",     new Phrase { profession = "Farmer" }},
            {"LeechFarmer",    new Phrase { profession = "Farmer" }},
            {"SnailFarmer",    new Phrase { profession = "Farmer" }},
            {"PigFarmer",      new Phrase { profession = "Farmer" }},
            {"WatervineFarmer",new Phrase { profession = "Farmer" , job = "watervineFarmer"}},
            {"AppleFarmer",    new Phrase { profession = "Farmer" , job = "starappleFarmer"}},
            {"BananaRancher",  new Phrase { profession = "Farmer" }}
            //********Warriors*
        };
    }
        
}