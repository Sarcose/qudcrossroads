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
