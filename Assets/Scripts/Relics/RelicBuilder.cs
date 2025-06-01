using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System;
using System.Reflection;

public class RelicBuilder
{

    string name;
    int sprite;

    List<string> relicnames = new List<string>();
    JArray properties;

    public RelicBuilder()
    {

    }

    public Relic BuildRelic(JObject relic_object)
    {

        string name = relic_object["name"].ToObject<string>();
        int sprite = relic_object["sprite"].ToObject<int>();
        string description = relic_object["trigger"]["description"].ToObject<string>() + ", " + relic_object["effect"]["description"].ToObject<string>();
        RelicTriggers trigger = RelicManager.Instance.BuildTrigger(relic_object["trigger"].ToObject<JObject>());
        RelicEffects effect = RelicManager.Instance.BuildEffect(relic_object["effect"].ToObject<JObject>());

        Relic relic = new Relic(name, sprite, description, trigger, effect);

        return relic;
    }
}


