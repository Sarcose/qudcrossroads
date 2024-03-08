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
using HistoryKit;
using XRL.Rules;

using QudCrossroads;
using QudCrossroads.QuestSystem;
using Qud.API;
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
        {   //[ ] Test |picktwo| and |pickthree|
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

        //=== !WIP Qud Crossroads 0.0 Errors ===
// An object reference is required for the non-static field, method, or property 'FindASpecificSiteDynamicQuestTemplate_FabricateQuestGiver.fabricateFindASpecificSiteQuest(GameObject)'
        public static string QuestTest(){
            string result = "";
            //see qc_quest notes
            Quest tQuest = new Quest();//QudCrossroads.Quests.VanillaQuest.fabricateFindASpecificSiteQuest(The.Speaker);
            if (tQuest is Quest){
                qprintc("Quest type generated");
            }else{
                qprintc("Quest generation failed");
            }
            /*
            See: targetLocation, LandmarkLocation, GeneratedLocationInfo,
            */
            List<string> addTable = new List<string> {
                "===Quest Test===",
                $"ID: {tQuest.ID ?? "NULL"}",                               //ID = Guid.NewGuid().ToString()
                $"Name: {tQuest.Name ?? "NULL"}",                           //quest.Name = Grammar.MakeTitleCase(ColorUtility.StripFormatting(targetLocation.Text));
                $"DisplayName: {tQuest.DisplayName ?? "NULL"}",
                $"SystemTypeID: {tQuest.SystemTypeID ?? "NULL"}",
                $"Accomplishment: {tQuest.Accomplishment ?? "NULL"}",
                $"Achievement: {tQuest.Achievement ?? "NULL"}",
                $"BonusAtLevel: {tQuest.BonusAtLevel ?? "NULL"}",
                $"Level: {(tQuest.Level != null ? tQuest.Level.ToString() : "NULL")}",
                $"Factions: {tQuest.Factions ?? "NULL"}",
                $"Reputation: {tQuest.Reputation ?? "NULL"}",
                $"Hagiograph: {tQuest.Hagiograph ?? "NULL"}",
                $"HagiographCategory: {tQuest.HagiographCategory ?? "NULL"}",
                $"Hagiograph: {tQuest.Hagiograph ?? "NULL"}",
                "==Properties==",
                "==IntProperties==",
                "==StepsByID==",
            };
            foreach (string item in addTable)
            {
                result += item;
            }
            return result;
        }
        public static string DisplayObjectMembers(){
            string result = "";
            List<string> addTable = new List<string> {
                //"|intro||greeting||title||toQuest||questHint||questHerring||transition||flavor||proverb||transition||emoteTransition||questConclusion|";
                "Oneline: introgreettitletoQuesttransitionproverb \n",
                "|intro||greeting||title||toQuest||transition||flavor||proverb|\n",
                "greeting: |greet|\n",
                "title: |title|\n",
                "toQuest: |toQuest|\n",
                "Player pronoun: =player.subjective=\n ",
                "A player equipment piece: |randEquipment|\n",    //TODO: |keyword|
                "A player item: |randInventory|\n",
                "A player equip OR item: |randHeld|\n",
                //A player equipment piece with specific rep
                "Speaker pronouns: =pronouns.subjective=\n",
                "Current TimeOfDay: unimplemented\n",      //oh wow it actually looks like this is the time graphic + time
                "Current Day: |today|\n",  
                "Current year: |year|\n",  
                "informalPersonTerm: |informalPersonTerm|\n",  
                "Current year: |year|\n",  
            };
            foreach (string item in addTable)
            {
                result += item;
            }

            //A player equipment piece with specific rep (use a loop to continually check probably)
                    //The.Listener.GetEquippedObjects().Any((Obj) => Obj.HasPart<GivesRep>()) 
            return result;
        }
        public static string QCVR(string key, Phrase phrase)
        {
            qprintc($"===============================");
            qprintc($"=========={key}==========");
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
        static string RegexToQCVR(string input, Phrase phrase)
        {
            string pattern = @"\|([^|]+)\|";
            string result = "";
            string newlinePlaceholder = "__NEWLINE__";
            
            //----Replace |variables| based on QCVR element-phrase logic----\\
            result = Regex.Replace(input, pattern, match =>
                {   string key = match.Groups[1].Value;
                return QCVR(key, phrase) + " ";  });
            result = result.Replace("\n", newlinePlaceholder);
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
            result = result.Replace(newlinePlaceholder, "\n");

            return result;
        }

/***************************************************************/
//                      Phrase Class                           //
/***************************************************************/
        public class Phrase                 
        {
            public string culture;
            public int familiarity;
            public string personality;
            public string subPersonality;
            public string profession;
            public string job;
            public string morphotype;
            public string subMorpho;
            public string mutation;
            public string mood;
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

        public static string TestString_Nueve() //brief aside to get the syntax of addressing various members of various objects
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
            }; //*Gets up and looks at you* Hail, traveler. The salt sun rises and the salt sun sets. At any rate, 
           // string testString = "|intro||greeting||title||toQuest||questHint||questHerring||transition||proverb||transition||emoteTransition||questConclusion|";
            return RegexToQCVR(QuestTest(), testPhrase);
        }


    }


}

/*

Namespace Study:

    XRL:    
        XRL.World
        XRL.The      

*/