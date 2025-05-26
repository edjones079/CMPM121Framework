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
    JObject properties;

    public RelicBuilder()
    {
        var relictext = Resources.Load<TextAsset>("relics");

        properties = JObject.Parse(relictext.text);
        foreach (var a in properties)
        {
            relicnames.Add(a.Key);
        }
    }

}

public class Triggers
{


}

public class EnemyDeath : Triggers
{
    public EnemyDeath()
    {
        EventBus.Instance.OnEnemyDeath += ActivateEffect;
    }

    public void ActivateEffect(EnemyController enemy)
    {

    }
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
