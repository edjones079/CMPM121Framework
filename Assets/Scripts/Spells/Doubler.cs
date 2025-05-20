using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

public class Doubler : ModifierSpell
{

    RPNEvaluator rpnEval = new RPNEvaluator();
    Dictionary<string, int> variables = new Dictionary<string, int>();

    public Doubler()
    {

    }

    override public void SetProperties(JObject properties)
    {
        isModifier = true;

        name = properties["name"].ToString();
        description = properties["description"].ToObject<string>();

        string delay_string = properties["delay"].ToObject<string>();
        delay = float.Parse(delay_string);

        mana_multiplier = properties["mana_multiplier"].ToObject<string>();
        cooldown_multiplier = properties["cooldown_multiplier"].ToObject<string>();

    }

    override public IEnumerator Cast(Vector3 where, Vector3 target, Vector3 direction, Hittable.Team team, SpellModifiers mods)
    {
        yield return inner.Cast(where, target, direction, team, mods);
        yield return new WaitForSeconds(delay);
         yield return inner.Cast(where, target, direction, team, mods);
        yield return new WaitForEndOfFrame();
    }

}
