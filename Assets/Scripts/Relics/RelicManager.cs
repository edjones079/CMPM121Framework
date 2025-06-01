using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using System.Reflection;
using System.Linq;

public class RelicManager : MonoBehaviour
{

    private static RelicManager theInstance;
    public static RelicManager Instance
    {
        get
        {
            if (theInstance == null)
                theInstance = GameObject.FindFirstObjectByType<RelicManager>();
            return theInstance;
        }
    }

    private List<Relic> all_relics = new List<Relic>();

    JArray relic_data;
    List<JObject> relic_objects;
    RelicBuilder relic_builder;
    PlayerController player;

    public void Start()
    {
        relic_data = ReadRelicData();
        BuildRelicObjects(relic_data);
        relic_builder = new RelicBuilder();
        player = GameObject.FindFirstObjectByType<PlayerController>();
        //Debug.Log(all_relics.Count);
    }

    public JArray ReadRelicData()
    {
        var relictext = Resources.Load<TextAsset>("relics");

        return JArray.Parse(relictext.text);
    }

    public void BuildRelicObjects(JArray relic_data)
    {
        relic_objects = new List<JObject>();

        foreach (JObject relic_object in relic_data)
        {
            relic_objects.Add(relic_object);
        }
    }

    public Relic BuildRelic()
    {
        int i = UnityEngine.Random.Range(0, all_relics.Count - 1);
        return relic_builder.BuildRelic(relic_objects[2]);
    }

    public RelicTriggers BuildTrigger(JObject trigger_object)
    {
        string trigger_type = trigger_object["type"].ToObject<string>();
        string amount;
        string until;

        UnityEngine.Debug.Log("Trigger Type: " + trigger_object["type"].ToObject<string>());

        if (trigger_type == "take-damage")
        {
            return new TakeDamage();
        }
        else if (trigger_type == "stand-still")
        {
            amount = trigger_object["amount"].ToObject<string>();
            return new StandStill(amount);
        }
        else if (trigger_type == "on-kill")
        {
            UnityEngine.Debug.Log("Attempting to make On-Kill Trigger.");
            return new EnemyDeath();
        }

        return new RelicTriggers();
    }

    public RelicEffects BuildEffect(JObject effect_object)
    {
        string effect_type = effect_object["type"].ToObject<string>();
        string amount;
        string until;

        if (effect_type == "gain-mana")
        {
            amount = effect_object["amount"].ToObject<string>();
            return new GainMana(amount, player);
        }
        else if (effect_type == "gain-spellpower")
        {
            amount = effect_object["amount"].ToObject<string>();
            until = effect_object["amount"].ToObject<string>();
            return new GainSpellPower(amount, until, player);
        }

        return new RelicEffects();
    }

}
