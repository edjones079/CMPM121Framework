using Newtonsoft.Json.Linq;
using Unity.Properties;
using UnityEngine;

public class MagicMissile : Spell
{

    public MagicMissile()
    {

    }

    public override void SetProperties(JObject properties)
    {
        icon = properties["icon"].ToObject<int>();
        description = properties["Description"].ToObject<string>();
        damage = properties["damage"]["amount"].ToString();
        damage_type = Damage.TypeFromString(properties["damage"]["type"].ToString());
        mana_cost = properties[mana_cost].ToString();
        cooldown = properties[cooldown].ToString();
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
