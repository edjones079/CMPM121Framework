using UnityEngine;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

public class ArcaneBolt : Spell
{

    public ArcaneBolt()
    {

    }

    override public void SetProperties(JObject properties)
    {
        name = properties["name"].ToString();
        icon = properties["icon"].ToObject<int>();
        description = properties["description"].ToObject<string>();
        damage = properties["damage"]["amount"].ToString();
        damage_type = Damage.TypeFromString(properties["damage"]["type"].ToString());
        mana_cost = properties["mana_cost"].ToString();
        cooldown = properties["cooldown"].ToString();
        projectile["trajectory"] = properties["projectile"]["trajectory"].ToString();
        projectile["speed"] = properties["projectile"]["speed"].ToString();
        projectile["sprite"] = properties["projectile"]["sprite"].ToString();
        Debug.Log(name);
        Debug.Log(icon);
        Debug.Log(description);
        Debug.Log(damage);
        Debug.Log(damage_type);
        Debug.Log(mana_cost);
        Debug.Log(cooldown);
        Debug.Log(projectile);
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
