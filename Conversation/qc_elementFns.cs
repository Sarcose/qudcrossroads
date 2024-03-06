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
using static QudCrossroads.Dialogue.QC_Lists;

namespace QudCrossroads.Dialogue
{
    public static partial class Functions
    {
        public static Dictionary<string,List<string>> FamiliarityDictionary = new Dictionary<string,List<string>>{
            { "greet", new List<string> {"snubGreet","strangeGreet","friendGreet" } },
            { "title", new List<string> {"snubTitle","strangeTitle","friendTitle" } },
            { "pleasantry", new List<string> {"insult","pleasantry","compliment" } },
        };
        public static string ParseFamiliarity(Phrase phrase, string key){
            qprintc("----ParseFamiliarity Started");

            return ElementByCategories(phrase, FamiliarityDictionary[key][phrase.familiarity]);
        }
        public static string ParseMiscGenderTerm(Phrase phrase, string key){
            string gender = "";
            switch (key)
            {
                case "informalPersonTerm":          //gender of the player - most used.
                    gender = "Neuter";
                    return GetRandString(phrase, CrossroadsCasualPronounAddress[gender]);

                case "informalPersonTerm.Subject":  //gender of the person being spoken about -- this is contextual based on quest dialogue probably
                    gender = "Neuter";
                    return GetRandString(phrase, CrossroadsCasualPronounAddress[gender]);

                // more cases can be added as needed

                case "informalPersonTerm.Speaker":  //gender of the speaker
                    gender = "Neuter";
                    return GetRandString(phrase, CrossroadsCasualPronounAddress[gender]);
                
                default:
                    return "person";
            }
        }
        public static string GetPart(Phrase phrase, string key){
            switch(key){
                case "randEquipment":
                    return GetEquippedRandom(The.Listener);
                case "randInventory":
                    return GetInventoryRandom(The.Listener);
                case "randHeld":
                    return GetHeldRandom(The.Listener);
                default:
                    return "~GetPart Unimplemented~"
            }

            //if key is "equipment" then return a phrase of just a piece of equipment, contextually:
            //posEquip -- a piece of equipment with +rep, or else a normal piece of equipment
            //negEquip -- a piece of equipment with +rep of another faction, or else a normal piece of equipment
            //posClothes -- a generative positive phrase about clothes
            //negClothes -- a generative negative phrase about clothes that says "You are wearing the X of our rival!"

            //part -- get if player is a TK or a mutation and prioritize in this order: implant, mutation, bodyslot
            //posPartAdj -- simple replacement. If TK, something like "shiny" "lumescent" etc. If mutant, get mutation adjectives
            //negPartAdj -- same
            //posPart | negPart -- some kind of village context, historyspice, or other that comes up with a liked or disliked implant or mutation by the NPC
                        //to comment on

        }
        public static string GetDate(Phrase phrase, string key){
            switch (key)
            {   //TODO: find the location of all below day variables
                case "today":               
                    return $"{XRL.World.Calendar.getDay()} of {XRL.World.Calendar.getMonth()}";
                case "month":
                    return XRL.World.Calendar.getMonth();
                case "timeOfDay":
                    return "morning";       //ah, what a fine morning!
                case "year":
                    return $"{XRL.World.Calendar.getYear()}";    //the year 1001 has been a harsh one...
                case "daysAgo":             //return a date phrase that is then saved in the quest description
                    return "today";         //just use a random span of time for this
                case "monthsAgo":    
                    return "today";
                case "yearsAgo":    
                    return "today";
                default:    
                    return "today";
            }
        }

        public static string GetEquippedRandom(GameObject obj){
            List<GameObject> l = obj.GetEquippedObjects();
            GameObject item = l[QRand.Next(0, l.Count)];
            return item.DisplayNameOnly;
        }
        public static string GetInventoryRandom(GameObject obj){
            List<GameObject> l = obj.GetInventory();
            GameObject item = l[QRand.Next(0, l.Count)];
            return item.DisplayNameOnly;
        }
        public static string GetHeldRandom(GameObject obj){
            List<GameObject> l = obj.GetWholeInventory();
            GameObject item = l[QRand.Next(0, l.Count)];
            return item.DisplayNameOnly;
        }

        public static string GetHobby(Phrase phrase, string key){
            //hobby
            //hobbyPhrase - from a future flavor list of some kind where various phrases are attached to hobbies.
                    //compliment: you make me feel like the time I kicked the winning goal!
            //hobbyFumble - from a future flavor list of some kind where various negative experience anecdotes are attached to hobbies
                    //insult: you're as bad as dropping a stitch!
            return "GetHobby NOOP";
        }
        // cultureList, genericList, personalityList, 
        // subPersonalityList, professionList, jobList, morphoList, mutationList
        public static string ElementByCategories(Phrase phrase, string key){
            qprintc($"----ElementByCategories started key {key}");
            List<List<string>> nonEmptyLists = new List<List<string>>();

            string address = "Cultures." + phrase.culture + "." + key;
            List<string> cultureList = GetEntry(Elements.Conversations, address);
            if (cultureList.Count > 0) nonEmptyLists.Add(cultureList);

            address = "Personalities.Generic." + key; 
            List<string> genericList = GetEntry(Elements.Conversations, address);
            if (genericList.Count > 0) nonEmptyLists.Add(genericList);

            address = "Personalities." + phrase.personality + "." + key;
            List<string> personalityList = GetEntry(Elements.Conversations, address);
            if (personalityList.Count > 0) nonEmptyLists.Add(personalityList);

            address = "Personalities." + phrase.subPersonality + "." + key;
            List<string> subPersonalityList = GetEntry(Elements.Conversations, address);
            if (subPersonalityList.Count > 0) nonEmptyLists.Add(subPersonalityList);

            address = "Professions." + phrase.profession + ".Generic." + key;
            List<string> professionList = GetEntry(Elements.Conversations, address);
            if (professionList.Count > 0) nonEmptyLists.Add(professionList);

            address = "Professions." + phrase.profession + "." + phrase.job + "." + key;
            List<string> jobList = GetEntry(Elements.Conversations, address);
            if (jobList.Count > 0) nonEmptyLists.Add(jobList);

            address = "Morphotypes."+ phrase.morphotype + "." + phrase.subMorpho + "." + key;
            List<string> morphoList = GetEntry(Elements.Conversations, address);
            if (morphoList.Count > 0) nonEmptyLists.Add(morphoList);

            address = "Morphotypes." + "Mutations" + "." + GetMutation(phrase) + "." + key;
            List<string> mutationList = GetEntry(Elements.Conversations, address);  
            if (mutationList.Count > 0) nonEmptyLists.Add(mutationList);
            

            qprintc($"----ElementByCategories nonEmptyLists count = {nonEmptyLists.Count}");
            
            if (nonEmptyLists.Count > 0){ 
                return GetRandString(phrase, nonEmptyLists[QRand.Next(0, nonEmptyLists.Count - 1)]);}
            else { qprintc($"---key {key} returned"); return key;}
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