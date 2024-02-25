using System;
using System.IO;
using System.Linq;
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
        public static string GetRandString_Child(params List<string>[] strArrays)
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
            return result;
        }
        public static bool CheckForPipe(string str){
            if (str != null && str.Length > 0 && str[0] == '|') return true; else {return false;}
        }
        public static string GetRandString(params List<string>[] strArrays)     // result = GetRandString(stringList, stringList2, stringList3, stringList4, etc...)
        {   //ERROR: not all code paths return a value
            qprintc("---[GetRandString start");
            string result = GetRandString_Child(strArrays);;
            qprintc($"--[GetRandString_Child resolved with result {result}");
            if (result == "|picktwo|"){//need to expand this for a whole host of cases
                qprintc("---[Result = " + result);
                result = "";
                string child = "|picktwo|";
                int elCount = 2;
                while (CheckForPipe(child) || elCount > 0)
                {
                    child = GetRandString_Child(strArrays);
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
                    child = GetRandString_Child(strArrays);
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
                            return GetRandString(stringList);
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
            // Use regular expression to find all placeholders
            //string resultString = ReplaceQCVR(inputString); //replace |bar|            
            string pattern = @"\|([^|]+)\|";
            string result = Regex.Replace(input, pattern, match =>
            {
                string key = match.Groups[1].Value;
                return QCVR(key, phrase); // the only thing left here is that we will need to define some kind of context variables that LVR uses to define pronouns!
            });

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
                subPersonality = "Lazy",
                profession = "Farmer",
                job = "WatervineFarmer",
                morphotype = "Chimera",
                subMorpho = "HideousSpecimen",
                mutation = "SociallyRepugnant"
            };
            //string testInput = "|intro||greeting||title||toQuest||questHint||questHerring||transition||flavor||proverb||transition||emoteTransition||questConclusion|";
            string testInput = "|intro|;|greeting|;|title|;|pleasantry|;|toQuest|;|transition|;|flavor|;|proverb|";
            //intro~|greeting|~~~Now that I have your attention...~At any rate~flavor~

            string finalString = RegexToQCVR(testInput, testPhrase);
            return finalString;
        }
        public static string OutfitNotice(GameObject player, string curString)  //probably need to change GameObject player tbh...
        {
            return curString;
        }


    }


}

/*

" O = coded, X = done
|intro|
X |greeting|
O |title|
|toQuest|
|questHint|
|questHerring|
|transition|
|flavor|
|proverb|
|transition|
|emoteTransition|
|questConclusion|




        // {_(Greet)}
        // {Pluralize(_(Title))}
        //TODO: another wrap function that checks if your character has multiple heads, is plural, or has followers, and uses Pluralize() in response



                =MARKOVPARAGRAPH=
                =verb:grab= -- grabbed from the mental mutation text, but does not become "grabs" in this usage
                =alchemist= -- seems to generate a randomized 'alchemist' babble
                =prounouns.siblingTerm= -- probably gives a generic if i had to guess
                =factionaddress:Barathrumites= --this is probably a global variable reference to the player
                =factionaddress:Mopango= --same as above
                =MARKOVCORVIDSENTENCE=
                =MARKOVSENTENCE=
                =MARKOVWATERBIRDSENTENCE=
                =name=  - player name
                =player.apparentSpecies=
                =player.formalAddressTerm=
                =player.offspringTerm=
                =player.personTerm=
                =player.reflexive=
                =player.siblingTerm=
                =pronouns.formalAddressTerm= -- this produced "friend" however, for whatever reason
                =pronouns.personTerm= --this produced 'human'
                =pronouns.possessive=
                =pronouns.siblingTerm=
                =pronouns.subjective=
                =subject.refname= --again, by producing "thing", this leads me to believe it is contextual and processed here without context.

*/