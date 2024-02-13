using System;
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
        private static void qprintc(string message)     //move to utilities
        {
            XRL.Messages.MessageQueue.AddPlayerMessage(message);
        }
        public static string GetRandString(params List<string>[] strArrays)     // result = GetRandString(stringList, stringList2, stringList3, stringList4, etc...)
        {   //ERROR: not all code paths return a value
            int totalCount = 0;
            foreach (var strArray in strArrays)
            {
                totalCount += strArray.Count;
            }
            int randomIndex = QRand.Next(0, totalCount);
            foreach (var strArray in strArrays)
            {
                if (randomIndex < strArray.Count) {return strArray[randomIndex]; }
                else { randomIndex -= strArray.Count; }
            }
            qprintc("GetRandString error - no elements found.");
            return null;
        }
        public static int GetRandStringIndex(params List<string>[] strArrays)
        {
            int totalCount = 0;
            foreach (var strArray in strArrays)
            {
                totalCount += strArray.Count;
            }
            return QRand.Next(0, totalCount); //ERROR: There is no argument given that corresponds to the required formal parameter 'maxInclusive'
        }
        public static string GetSpecificString(int index, params List<string>[] strArrays)
        { //ERROR: not all code paths return a value
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
            //|intro||greeting||title||toQuest||questHint||questHerring||transition||flavor||proverb||transition||emoteTransition||questConclusion|";
            //use a GlobalContainer to establish global pronouns and other contexts for speaker and such
            qprintc(key);
            Dictionary<string, bool> doTest = new Dictionary<string, bool>
            {
                { "intro", false },
                { "greeting", true },
                { "title", false },
                { "toQuest", false },
                { "questHint", false },
                { "questHerring", false },
                { "transition", false },
                { "flavor", false },
                { "proverb", false },
                { "emoteTransition", false },
                { "questConclusion", false },
                // See TODO and building notes on qc_elementFns
            };
            if (doTest.ContainsKey(key) && doTest[key]) //only check, for now, if the key is in doTest, so we avoid checking lots of unimplemented keys
                {
                    if (CrossroadsLVR.TryGetValue(key, out object value))
                    {
                        if (value is List<string> stringList)
                        {
                            // Handle the case where the value is a list of strings
                            qprintc("--List");
                            return GetRandString(stringList);
                        }
                        else if (value is Func<Phrase, string> function)
                        {
                            // Handle the case where the value is a function
                            qprintc("--Func");
                            return function(phrase);
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
                            return $"|Unsupported type: {key}|";
                        }
                    }
                    else
                    {
                        // Key not found in the dictionary
                        qprintc("--NotFound");
                        return $"|Key not found: {key}|";
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
        public delegate void ProcessFnDelegate(string name);
        private static Func<string, string> GetProcessFn(Phrase phrase)
        {
            return (name) => functionDictionary[name]?.Invoke(phrase);
        }

/***************************************************************/
//                      Phrase Class                           //
/***************************************************************/
        public class Phrase                 
        {
            public string Culture { get; set; }
            public string Familiarity { get; set; }
            public string Personality {get; set; }
            public string subPersonality {get; set; }
            public string Job {get; set; }
            public string specificJob {get; set; }
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
        public static string TestString_Siete()
        {
            Phrase newPhrase = new Phrase
            {
                Culture = "SaltMarshCulture",
                Familiarity = "unfamiliar"
            };
            Func<string, string> _ = GetProcessFn(newPhrase);
            return $"{_(Greet)}, {Pluralize(_(Title))}, how are you on this day? {LVR("=verb:grab=")}";
        }

        public static string TestString_Ocho()
        {
            Phrase testPhrase = new Phrase
            {
                Culture = "SaltMarshCulture",
                Familiarity = "unfamiliar",
                Personality = "Peppy",
                subPersonality = "Lazy",
                Job = "Farmer",
                specificJob = "WatervineFarmer"
            };
            string testInput = "|intro||greeting||title||toQuest||questHint||questHerring||transition||flavor||proverb||transition||emoteTransition||questConclusion|";
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