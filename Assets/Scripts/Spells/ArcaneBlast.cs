using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneBlast : Spell
{
    string N;
    string secondary_damage;
    Dictionary<string, string> secondary_projectile = new Dictionary<string, string>();

    public ArcaneBlast()
    {
        
    }

    public override void SetProperties(JObject properties)
    {
        name = properties["name"].ToString();
        icon = properties["icon"].ToObject<int>();
        description = properties["description"].ToObject<string>();
        N = properties["N"].ToString();
        damage = properties["damage"]["amount"].ToString();
        secondary_damage = properties["secondary_damage"].ToString();
        damage_type = Damage.TypeFromString(properties["damage"]["type"].ToString());
        mana_cost = properties["mana_cost"].ToString();
        cooldown = properties["cooldown"].ToString();
        projectile["trajectory"] = properties["projectile"]["trajectory"].ToString();
        projectile["speed"] = properties["projectile"]["speed"].ToString();
        projectile["sprite"] = properties["projectile"]["sprite"].ToString();
        secondary_projectile["trajectory"] = properties["secondary_projectile"]["trajectory"].ToString();
        secondary_projectile["speed"] = properties["secondary_projectile"]["speed"].ToString();
        secondary_projectile["lifetime"] = properties["secondary_projectile"]["lifetime"].ToString();
        secondary_projectile["sprite"] = properties["secondary_projectile"]["sprite"].ToString();
        Debug.Log(name);
        Debug.Log(icon);
        Debug.Log(description);
        Debug.Log(N);
        Debug.Log(damage);
        Debug.Log(secondary_damage);
        Debug.Log(damage_type);
        Debug.Log(mana_cost);
        Debug.Log(cooldown);
        Debug.Log(projectile);
        Debug.Log(secondary_projectile);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
