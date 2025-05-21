using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

public class Piercer : ModifierSpell
{

    RPNEvaluator rpnEval = new RPNEvaluator();
    Dictionary<string, int> variables = new Dictionary<string, int>();

    public Piercer()
    {

    }

    override public void SetProperties(JObject properties)
    {
        isModifier = true;

        name = properties["name"].ToString();
        description = properties["description"].ToObject<string>();

        mana_multiplier = properties["mana_multiplier"].ToObject<string>();
        cooldown_multiplier = properties["cooldown_multiplier"].ToObject<string>();

    }

    /*override public Action<Hittable, Vector3> GetOnHit(SpellModifiers mods)
    {
        void OnHit(Hittable other, Vector3 impact)
        {
            if (other.team != team)
            {
                other.Damage(new Damage(GetDamage(mods), Damage.Type.ARCANE));
            }
        }

        return OnHit;
    }*/
}
