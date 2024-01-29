# using static XRL.Language.Grammar;

## public static string Pluralize(string word){}
## public static string PossessivePronoun(string subjectPronoun)
## public static string Weirdify(string word, int Chance = 100)
## public static string ReflexivePronoun(string subjectPronoun)
## public static string PastTenseOf(string verb)



# using XRL.GameText;
## public static string VariableReplace(   // This appears to translate ==Text== into replacers!
#### StringBuilder Message, 
#### GameObject Subject = null, 
#### string ExplicitSubject = null, 
#### bool   ExplicitSubjectPlural = false, 
#### GameObject Object = null, 
#### string ExplicitObject = null, 
#### bool ExplicitObjectPlural = false, 
#### bool StripColors = false)

// string text = GameText.VariableReplace(key, ParentObject);
