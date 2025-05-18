using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

public class ModifierSpell : Spell
{
    public Spell inner;

    public string damage_multiplier;
    public string mana_multiplier;
    public string speed_multiplier;
    public string cooldown_multiplier;
    public float delay;
    public float angle;
    public string mana_adder;
    public string projectile_trajectory;

    RPNEvaluator rpnEval = new RPNEvaluator();
    Dictionary<string, int> variables = new Dictionary<string, int>();

    public ModifierSpell()
    {

    }

    public void SetInnerSpell(Spell inner)
    {
        this.inner = inner;
    }

    override public void SetProperties(JObject properties)
    {
        isModifier = true;

        name = properties["name"].ToString();
        description = properties["description"].ToObject<string>();

    }

    override public IEnumerator Cast(Vector3 where, Vector3 target, Hittable.Team team)
    {
        inner.Cast(where, target, team);
        yield return new WaitForEndOfFrame();
    }
}

public class DamageAmp : ModifierSpell
{
    public DamageAmp()
    {

    }
}

public class SpeedAmp : ModifierSpell
{
    public SpeedAmp()
    {

    }
}

public class Chaos : ModifierSpell
{
    public Chaos()
    {

    }
}
public class Homing : ModifierSpell
{
    public Homing()
    {

    }
}

