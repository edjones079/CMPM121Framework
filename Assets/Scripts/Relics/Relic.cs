using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System;

public class Relic
{

    string name;
    int sprite;

    Dictionary<string, string> trigger_data = new Dictionary<string, string>();
    Dictionary<string, string> effect_data = new Dictionary<string, string>();

    Triggers trigger;
    Effects effect;

    public Relic() { }

    public void SetProperties(JObject properties)
    {
        name = properties["name"].ToObject<string>();
        sprite = properties["sprite"].ToObject<int>();
        trigger_data["description"] = properties["trigger"]["description"].ToObject<string>();
        trigger_data["type"] = properties["trigger"]["type"].ToObject<string>();

        effect_data["description"] = properties["effect"]["description"].ToObject<string>();
        effect_data["type"] = properties["effect"]["type"].ToObject<string>();
        effect_data["amount"] = properties["effect"]["amount"].ToObject<string>();
     
    }

    public int GetIcon()
    {
        return sprite;
    }

    public string GetDescription()
    {
        string description = trigger_data["description"] + ", " + effect_data["description"];
        return description;
    }

    public void SetTrigger()
    {

    }

    public void SetEffect()
    {

    }


    
}