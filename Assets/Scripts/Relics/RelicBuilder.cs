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
        EventBus.Instance.OnEnemyDeath += OnEnemyDeath;
    }

    public void OnEnemyDeath(EnemyController enemy)
    {

    }
}

public class StandStill : Triggers
{

}

public class TakeDamage : Triggers
{

}
