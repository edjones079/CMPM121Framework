using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

public class Splitter : ModifierSpell
{

    RPNEvaluator rpnEval = new RPNEvaluator();
    Dictionary<string, int> variables = new Dictionary<string, int>();

    public Splitter()
    {

    }

    override public void SetProperties(JObject properties)
    {
        isModifier = true;

        name = properties["name"].ToString();
        description = properties["description"].ToObject<string>();

        string angle_string = properties["angle"].ToObject<string>();
        angle = float.Parse(angle_string);

        mana_multiplier = properties["mana_multiplier"].ToObject<string>();

    }

    override public IEnumerator Cast(Vector3 where, Vector3 target, Hittable.Team team, Modifiers mods)
    {
        inner.Cast(where, target, team);
        inner.Cast(where, target, team);
        yield return new WaitForEndOfFrame();
    }

}
