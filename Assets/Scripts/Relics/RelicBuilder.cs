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
        var relictext = Resources.Load<TextAsset>("relics");

        properties = JArray.Parse(relictext.text);
        foreach (var a in properties)
        {
            relicnames.Add(a["name"].ToObject<string>());
        }

    }

    public Relic BuildRelic()
    {
        Relic relic = MakeRelic();

        int i = UnityEngine.Random.Range(0, properties.Count - 1);

        JObject jobject = properties[i].Value<JObject>();
        relic.SetProperties(jobject);

        return relic;
    }

    public Relic MakeRelic()
    {

        return new Relic();
    }
}

public class Triggers
{

    public void ActivateEffect()
    {

    }
}

public class EnemyDeath : Triggers
{

    private static List<Effects> effects;

    public static void Invoke(EnemyController enemy)
    {
        foreach (Effects effect in effects)
        {
            //effect.OnEnemyDeath(enemy);
        }
    }

    public static void Register(Effects effect)
    {
        effects.Add(effect);
    }

    public EnemyDeath() { }
}

public class StandStill : Triggers
{
    public StandStill()
    {
        EventBus.Instance.OnStandStill += ActivateEffect;
    }

    public void ActivateEffect(PlayerController player)
    {

    }
}

public class TakeDamage : Triggers
{
    public TakeDamage()
    {
        EventBus.Instance.OnDamage += ActivateEffect;
    }

    public void ActivateEffect(Vector3 where, Damage dmg, Hittable target)
    {

    }
}
