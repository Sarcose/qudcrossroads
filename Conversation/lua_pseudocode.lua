--if you're reading this get outta here, this is my scratch space to write lua tables
--and then translate them into C# because my brain thinks in Lua terms


KendrinCulture = {
        greetings = {
            unfamiliar = {},
            familiar = {},
            friendly = {},
            unfriendly = {},
        },
        titles = {
            unfamiliar = {},
            familiar = {},
            friendly = {},
            unfriendly = {},
        },
        compatibleErrands = {
            
        },

    }


    CultureConversation = {
        greetings = {
            unfamiliar = {}, --array of strings
            familiar = {},  --array of strings
            friendly = {},  --array of strings
            unfriendly = {},  --array of strings
        },
        titles = {
            unfamiliar = {},    --array of strings
            familiar = {},      --array of strings
            friendly = {},      --array of strings
            unfriendly = {},    --array of strings
        },
    }

FetchForMe = {}


SaltMarshCulture = {
    Greetings = {
        Unfamiliar = {"Salt and sun","Good meet to you.","Bountiful harvest"},
        Familiar = {"Salt and sun","How are you on this day?","How fare you?"},
        Friendly = {"Hail","Merry meet","Bright tidings"},
        Unfriendly = {"What","What could you want","Will this take long"}
    },
    Titles = {
        Unfamiliar = {"wayfarer","traveler","stranger"},
        Familiar = {"wayfarer","fellow"},
        Friendly = {"my friend","my sibling","my fellow","dear sib"},
        Unfriendly = {"you jerk","you troublemaker",}
    }
}



AllCultures = {
    SaltMarshCulture = {},--etc
    DesertCulture = {},--etc
    ForestCulture = {},--etc
}

local whichCulture = "SaltMarshCulture"
local specificGreetString = AllCultures[whichCulture].Greetings.Unfamiliar[1]








local Phrase = {
    Culture = "",
    Familiarity = "",
    ReStr = "",
}

local function GreetFn(phrase)  --phrase is needed, here
    --logic

end

local function TitleFn(phrase)
    --logic
end

local functionDictionary = {
    Greet = GreetFn,
    Title = TitleFn
}

local function GetProcessFn(phrase)
    return function(name) functionDictionary[name](phrase) end
end

local function logic()
    local newPhrase = New(Phrase)   --New is pseudocode for a table instantiation routine, ignore
    newPhrase.Culture = "SaltMarshCulture"
    newPhrase.Familiarity = "unfamiliar"
    local element = "Greet"

    local localProcessFn = GetProcessFn(newPhrase)
    
    for i=1,10 do
        localProcessFn(element)
    end
end