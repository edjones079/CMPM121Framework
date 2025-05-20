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

    override public IEnumerator Cast(Vector3 where, Vector3 target, Vector3 direction, Hittable.Team team, SpellModifiers mods)
    {
        float a = angle / 100;

        UnityEngine.Debug.Log(a);

        float directionAngle = Mathf.Atan2(direction.y, direction.x);
        directionAngle += a;
        Vector3 newDirection = new Vector3(Mathf.Cos(directionAngle), Mathf.Sin(directionAngle), 0);

        yield return inner.Cast(where, target, newDirection, team, mods);

        directionAngle = Mathf.Atan2(direction.y, direction.x);
        directionAngle -= a;
        newDirection = new Vector3(Mathf.Cos(directionAngle), Mathf.Sin(directionAngle), 0);

        yield return inner.Cast(where, target, newDirection, team, mods);
        yield return new WaitForEndOfFrame();
    }

}
