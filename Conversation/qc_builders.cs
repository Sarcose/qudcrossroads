using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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
using static QudCrossroads.Dialogue.QC_Lists;


using System.Windows.Forms;

namespace QudCrossroads.Dialogue
{
    public static partial class Builders
    {
/***************************************************************/
//          Utility Functions for Phrase Building              //
/***************************************************************/
        public static void qprintc(string message)     //move to utilities
        {
            XRL.Messages.MessageQueue.AddPlayerMessage(message, null, false);
        }
        public static string GetRandString_Child(Phrase phrase, params List<string>[] strArrays)
        {
            qprintc("---[[GetRandString_Child start");
            long totalCount = 0; // Use long to handle potential overflow
            string result = null;
            foreach (var strArray in strArrays)
            {
                qprintc("---[[building Count");
                totalCount += strArray.Count;
                if (totalCount > int.MaxValue)
                {
                    qprintc("Combined count exceeds Int32.MaxValue");
                }
            }
            int randomIndex = QRand.Next(0, (int)totalCount - 1);
            int loopCount = 0;
            foreach (var strArray in strArrays)
            {
                qprintc($"---[[foreach {loopCount} at randomInxed {randomIndex} with strArray.Count {strArray.Count}");
                loopCount++;
                if (randomIndex < strArray.Count)
                {
                    qprintc($"---[[getting result from array of count {strArray.Count} with randomIndex {randomIndex}...");
                    result = strArray[randomIndex];
                    qprintc($"---[[result = {result}");
                }
                else
                {
                    qprintc("---[[ind decrement");
                    randomIndex -= (strArray.Count - 1);
                }
            }
            if (CheckForPipe(result)){
                qprintc("---[[ recursive check on included variable");
                result = QCVR( result.Trim('|'), phrase);
            }
            return result;
        }
        public static bool CheckForPipe(string str){
            if (str != null && str.Length > 0 && str[0] == '|') return true; else {return false;}
        }
        public static string GetRandString(Phrase phrase, params List<string>[] strArrays)     // result = GetRandString(stringList, stringList2, stringList3, stringList4, etc...)
        {   //ERROR: not all code paths return a value
            qprintc("---[GetRandString start");
            string result = GetRandString_Child(phrase, strArrays);;
            qprintc($"--[GetRandString_Child resolved with result {result}");
            if (result == "|picktwo|"){//need to expand this for a whole host of cases
                qprintc("---[Result = " + result);
                result = "";
                string child = "|picktwo|";
                int elCount = 2;
                while (CheckForPipe(child) || elCount > 0)
                {
                    child = GetRandString_Child(phrase, strArrays);
                    if (!CheckForPipe(child)){ 
                        elCount --;
                        result += child;
                        if (elCount == 1){
                            result += " and ";
                        }else {result += ".";}
                    }
                }

                
            }else if (result == "|pickthree|"){
                qprintc("---[Result = " + result);
                result = "";
                string child = "|pickthree|";
                int elCount = 3;
                while (CheckForPipe(child) || elCount > 0)
                {
                    child = GetRandString_Child(phrase,strArrays);
                    if (!CheckForPipe(child)){ 
                        elCount --;
                        result += child;
                        if (elCount == 2){
                            result += ", ";
                        }else if (elCount == 1){
                            result += ", and ";
                        }else {result += ".";}
                        }
                }
            }else if (!CheckForPipe(result)){
                qprintc("---[Else");
                qprintc("---[Result = " + result);
                //handle other specific cases here (such as |pickspecific| which will require another round of spaghetti)
            }
            qprintc("---[Result = " + result);
            qprintc("---[GetRandString return");
            return result;
        }
        public static int GetRandStringIndex(params List<string>[] strArrays)
        {
            int totalCount = 0;
            foreach (var strArray in strArrays)
            {
                totalCount += strArray.Count;
            }
            return QRand.Next(0, totalCount);
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
            return null;
        }
        public static string DumpObject(object obj)
        {
            Type type = obj.GetType();
            StringBuilder sb = new StringBuilder();

            // Get all fields
            FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (var field in fields)
            {
                sb.AppendLine($"{field.Name} = {field.GetValue(obj)}");
            }

            // Get all properties
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (var prop in properties)
            {
                sb.AppendLine($"{prop.Name} = {prop.GetValue(obj)}");
            }

            return sb.ToString();
        }
        public static string PrintNamespaceMembers(string namespaceName)
        {
            string ret = "";
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (Assembly assembly in assemblies)
            {
                Type[] typesInNamespace = assembly.GetTypes()
                    .Where(type => type.Namespace == namespaceName)
                    .ToArray();

                foreach (Type type in typesInNamespace)
                {
                    ret += $"\n| Type: {type.FullName}";

                    // Get all fields
                    FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                    foreach (var field in fields)
                    {
                        ret += $"\n||  Field: {field.Name}";
                    }

                    // Get all properties
                    PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                    foreach (var prop in properties)
                    {
                        ret += $"\n|||  Property: {prop.Name}";
                    }

                }
            }
            return ret;
        }
        public static string DisplayObjectMembers(){
            string result = "";
            List<string> addTable = new List<string> {
                "Player pronoun: =The.Listener.pronouns.subjective= \n ", //player pronoun?
                $"A player equipment piece: {GetEquippedRandom(The.Listener)}\n",    //TODO: |keyword|
                $"A player item: {GetInventoryRandom(The.Listener)}\n",
                $"A player equip OR item: {GetHeldRandom(The.Listener)}\n",
                //A player equipment piece with specific rep
                $"Speaker pronouns: =The.Speaker.pronouns.subjective= \n",
                $"Current Time: {XRL.World.Calendar.getTime()}",
                $"Current Day: {XRL.World.Calendar.getDay()}",
                $"Current Month: {XRL.World.Calendar.getMonth()}",
                $"Current year: {XRL.World.Calendar.getYear()}",
            };
            foreach (string item in addTable)
            {
                result += item;
            }

            //A player equipment piece with specific rep (use a loop to continually check probably)
                    //The.Listener.GetEquippedObjects().Any((Obj) => Obj.HasPart<GivesRep>()) 
            //XRL.World.Calendar
                //Day
                //Time
                //Time name
                //Date
                //Month


            //Quest tempQuest = fabricateFindASpecificSiteQuest(XRL.The.Speaker);
            //return += " | tempQuest: ";
            //return += DumpObject(tempQuest);
            return result;
            

            
        }
        public static string LVR(string varstring)     //add more later?
        {   //TODO: use a GlobalContainer to establish global pronouns for speaker and such
            XRL.Messages.MessageQueue.AddPlayerMessage(varstring);
            return GameText.VariableReplace(varstring, null);
        }
        public static string QCVR(string key, Phrase phrase)     //look for |Variables| instead
        {   //CrossroadsLVR in qc_lists.cs
            //|intro||greeting||title||pleasantry||toQuest||questHint||questHerring||transition||flavor||proverb||transition||emoteTransition||questConclusion|";
            // "|intro|~|greeting|~|title|~|pleasantry|~|toQuest|~|transition|~|flavor|~|proverb|"
            //use a GlobalContainer to establish global pronouns and other contexts for speaker and such
            qprintc("======================");
            qprintc("=========" + key + "=========");
            Dictionary<string, bool> doTest = new Dictionary<string, bool>{                   
                { "intro", true },    
                { "pleasantry", true},
                { "greet", true },
                { "title", true },    
                { "toQuest", true },            
                { "questHint", true },          
                { "questHerring", true },       
                { "transition", true },         
                { "flavor", true },             
                { "proverb", true },            
                { "questConclusion", true },    
                // See TODO and building notes on qc_elementFns
            };
            if (doTest.ContainsKey(key) && doTest[key]) //only check, for now, if the key is in doTest, so we avoid checking lots of unimplemented keys
                {
                    if (CrossroadsLVR.TryGetValue(key, out object value))   //see if the item is in CrossroadsLVR
                    {
                        if (value is List<string> stringList)
                        {
                            // Handle the case where the value is a list of strings
                            qprintc("--List");
                            return GetRandString(phrase, stringList);   //pass phrase to maintain it in case of recursive checks
                        }
                        else if (value is Func<Phrase, string, string> function)
                        {
                            // Handle the case where the value is a function
                            qprintc("--Func");
                            return function(phrase, key);
                        }
                        else if (value is string stringValue)
                        {
                            // Handle the case where the value is a single string
                            qprintc("--String");
                            return stringValue;
                        }
                        else
                        {
                            // Handle other cases if needed
                            qprintc("--Unsup");
                            return $"|Unsupported type for key: {key}|";
                        }
                    }
                    else
                    {
                        qprintc("--ElementByCategories (key not in dict)");
                        return ElementByCategories(phrase, key);

                    }
                }
            else    //ignore and don't translate
                {
                    qprintc("-Skipped");
                    return "|" + key + "|";
                }
        }
        static string RegexToLVR(string input) // pass LVR to this function
        {
            //string resultString = RegexToLVR(inputString); //replace =foo=
            // Use regular expression to find all placeholders
            string pattern = "=([^=]+)=";
            string result = Regex.Replace(input, pattern, match =>     //The name 'Regex' does not exist in the current context
            {
                string key = match.Groups[1].Value;
                return LVR(key); // the only thing left here is that we will need to define some kind of context variables that LVR uses to define pronouns!
            });

            return result;
        }
        static string RegexToQCVR(string input, Phrase phrase) // pass LVR to this function
        {
            string pattern = @"\|([^|]+)\|";
            string result = "";
            //----Replace |variables| based on QCVR element-phrase logic----\\
            result = Regex.Replace(input, pattern, match =>
                {   string key = match.Groups[1].Value;
                return QCVR(key, phrase) + " ";  });
            
            //----Fill the new result with punctuation based on # placeholders and the phrase's "mood"----\\
            result = Regex.Replace(result, @"\s+", " ");    //clean up double spaces
            result = Regex.Replace(result, @"\s+\.", "#");  //clean up spaces before #
            result = Regex.Replace(result, @"[^\w\s]\#", match => //if an element has punctuation in it, override the placeholder #
                {  return match.Value[0].ToString() + " "; });
            result = result.Replace("#", getPunct(phrase)); //if a # is left, pseudo-randomize punctuation and add a space at the end

            //----Make sure there aren't too many semicolons. Stylistic choice.----\\
            // Calculate the threshold as a percentage of total punctuation symbols
            int totalPunctuationCount = result.Count(c => !char.IsLetterOrDigit(c) && c != ' '); // Punctuation symbols excluding letters and digits
            double percentageThreshold = 0.4;
            int dynamicThreshold = (int)(totalPunctuationCount * percentageThreshold);
            // Replace ; with . randomly if the count is above the dynamic threshold
            int semicolonCount = result.Count(c => c == ';');
            if (semicolonCount > dynamicThreshold)
            {
                result = result.Replace(';', QRand.Next(0, 2) == 0 ? '.' : ' ');
            }

            return result;
        }

/***************************************************************/
//                      Phrase Class                           //
/***************************************************************/
        public class Phrase                 
        {
            public string culture { get; set; }
            public int familiarity { get; set; }
            public string personality {get; set; }
            public string subPersonality {get; set; }
            public string profession {get; set; }
            public string job {get; set; }
            public string morphotype {get; set;}
            public string subMorpho {get; set;}
            public string mutation {get; set;}      //will probably not use this in the future
            public string mood {get; set;}
        }

        /*
        public Phrase phrase = new Phrase
        {
            Culture = "SaltMarshCulture",
            Familiarity = "Unfamiliar" // Corrected the typo here
        };
        */

        
/***************************************************************/
//                      Testing Area                           //
/***************************************************************/

        public static string TestString_Ocho()
        {
            Phrase testPhrase = new Phrase
            {
                culture = "SaltMarshCulture",
                familiarity = 1,        //0 = unfriendly, 1 = unfamiliar, 2 = friendly
                personality = "Peppy",
                subPersonality = "Tired",
                profession = "Farmer",
                job = "WatervineFarmer",
                morphotype = "Chimera",
                subMorpho = "HideousSpecimen",
                mutation = "SociallyRepugnant",
                mood = "random"
            }; 
            //string testInput = "|intro||greeting||title||toQuest||questHint||questHerring||transition||flavor||proverb||transition||emoteTransition||questConclusion|";
            string testInput = "|intro|;|greeting|;|title|;|pleasantry|;|toQuest|;|transition|;|flavor|;|proverb|";
            //intro~|greeting|~~~Now that I have your attention...~At any rate~flavor~

            string finalString = RegexToQCVR(testInput, testPhrase);
            return finalString;
        }

        public static string TestString_Nueve() //brief aside to get the syntax of addressing various members of various objects
        {
            return DisplayObjectMembers();
            //string XRLString = DumpObject(XRL);
            //string XRLString = PrintNamespaceMembers("The"); //we can't use this for now it's too complex to delve into lolol
            //string result = "XRL: " + XRLString;
            //Clipboard.SetText(result);
            //return result; 
        }


    }


}

/*

Namespace Study:

    XRL:    
        XRL.World
        XRL.The      

*/