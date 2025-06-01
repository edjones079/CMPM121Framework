using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
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

    public void Start()
    {
        JArray relic_data = ReadRelicData();
        BuildRelics(relic_data);
        UnityEngine.Debug.Log(all_relics.Count);
    }

    public JArray ReadRelicData()
    {
        var relictext = Resources.Load<TextAsset>("relics");

        return JArray.Parse(relictext.text);
    }

    public void BuildRelics(JArray relic_objects)
    {
        RelicBuilder relic_builder = new RelicBuilder();
        foreach (JObject relic_object in relic_objects)
        {
            Relic relic = relic_builder.BuildRelic(relic_object);
            all_relics.Add(relic);
        }
    }

    public RelicTriggers CreateTrigger(JObject trigger_object)
    {
        string trigger_type = trigger_object["type"].ToObject<string>();
        string amount;
        string until;

        if (trigger_type == "take_damage")
        {
            return new TakeDamage();
        }
        else if (trigger_type == "stand_still")
        {
            amount = trigger_object["amount"].ToObject<string>();
            return new StandStill(amount);
        }
        else if (trigger_type == "on_kill")
        {
            return new EnemyDeath();
        }

        return new RelicTriggers();
    }

    public RelicEffects CreateEffect(JObject effect_object)
    {
        string effect_type = effect_object["type"].ToObject<string>();
        string amount;
        string until;

        if (effect_type == "gain_mana")
        {
            amount = effect_object["amount"].ToObject<string>();
            return new GainMana(amount);
        }
        else if (effect_type == "gain_spellpower")
        {
            amount = effect_object["amount"].ToObject<string>();
            until = effect_object["amount"].ToObject<string>();
            return new GainSpellPower(amount, until);
        }

        return new RelicEffects();
    }

    public Relic SelectRelic()
    {
        int i = UnityEngine.Random.Range(0, all_relics.Count - 1);
        return all_relics.ElementAt(i);
    }
}
