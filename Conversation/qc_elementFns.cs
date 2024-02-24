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
using static QudCrossroads.Dialogue.Builders;

namespace QudCrossroads.Dialogue
{
    public static partial class Functions
    {
        public static Dictionary<string,List<string>> familiarityDictionary = new Dictionary<string,List<string>>{
            { "greet", new List<string> {"snubGreet","strangeGreet","friendGreet" } },
            { "title", new List<string> {"snubTitle","strangeTitle","friendTitle" } },
            { "pleasantry", new List<string> {"insult","pleasantry","compliment" } },

        }
        public static string ParseFamiliarity(Phrase phrase, string key){
            return ElementByCategories(phrase, FamiliarityDictionary[key][phrase.familiarity+1]);
        }
        // cultureList, genericList, personalityList, 
        // subPersonalityList, professionList, jobList, morphoList, mutationList
        public static string ElementByCategories(Phrase phrase, string key){
            List<List<string>> nonEmptyLists = new List<List<string>>();

            string address = "Cultures." + phrase.culture + "." + key;
            List<string> cultureList = GetEntry<List<string>>(Conversations, address);
            if (cultureList.Count > 0) nonEmptyLists.Add(cultureList);

            address = "Personalities.Generic." + key; 
            List<string> genericList = GetEntry<List<string>>(Conversations, address);
            if (genericList.Count > 0) nonEmptyLists.Add(genericList);

            address = "Personalities." + phrase.personality + "." + key;
            List<string> personalityList = GetEntry<List<string>>(Conversations, address);
            if (personalityList.Count > 0) nonEmptyLists.Add(personalityList);

            address = "Personalities." + phrase.subPersonality + "." + key
            List<string> subPersonalityList = GetEntry<List<string>>(Conversations, address);
            if (subPersonalityList.Count > 0) nonEmptyLists.Add(subPersonalityList);

            address = "Professions." + phrase.profession + ".Generic." + key;
            List<string> professionList = GetEntry<List<string>>(Conversations, address);
            if (professionList.Count > 0) nonEmptyLists.Add(professionList);

            address = "Professions." + phrase.profession + "." + phrase.job + "." + key;
            List<string> jobList = GetEntry<List<string>>(Conversations, address);
            if (jobList.Count > 0) nonEmptyLists.Add(jobList);

            address = "Morphotypes."+ phrase.morphotype + "." + phrase.subMorpho + "." + key;
            List<string> morphoList = GetEntry<List<string>>(Conversations, address);
            if (morphoList.Count > 0) nonEmptyLists.Add(morphoList);

            address = "Morphotypes." + "Mutations" + "." + GetMutation(phrase) + "." + key;
            List<string> mutationList = GetEntry<List<string>>(Conversations, address);  
            if (mutationList.Count > 0) nonEmptyLists.Add(mutationList);
            
            return nonEmptyLists.Count > 0 ? GetRandString(nonEmptyLists[QRand.Next(0, nonEmptyLists.Count)]) : "No options available";
        }

        public static string GetMutation(Phrase phrase){
            // for now we will just grab the test mutation
            // in the future we will parse a list of implemented mutations, selecting one that the entity has
            return phrase.mutation;
        }
        public static string GetBiome()
        {
            //TODO: global container for CurrentConversationContext such as "whaat biome am I in?" etc.
            //ex return string CurrentConversationContext.currentBiome
            return null;
        }
    }
}





/*
Building TODOs:

|greeting|: 
    [X] get it to parse and pass GreetFn using Phrase
    [ ] greetPersonality based on phrase personality in qc_elements.cs -- grab an arbitrary number of personalities actually!!
    [ ] greetJob based on phrase Job                in qc_elements.cs
    [ ] greetSpecificJob based on phrase specific job if it exists in qc_elements.cs
    */