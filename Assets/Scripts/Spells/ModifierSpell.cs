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

    override public void SetInnerSpell(Spell inner)
    {
        this.inner = inner;
    }

    override public Spell GetInnerSpell()
    {
        return this.inner;
    }

    override public void SetProperties(JObject properties)
    {
        isModifier = true;

        name = properties["name"].ToString();
        description = properties["description"].ToObject<string>();

    }

    override public IEnumerator Cast(Vector3 where, Vector3 target, Hittable.Team team, Modifiers mods)
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

    override public void SetProperties(JObject properties)
    {
        isModifier = true;

        name = properties["name"].ToString();
        description = properties["description"].ToObject<string>();
        damage_multiplier = properties["damage_multiplier"].ToObject<string>();
        mana_multiplier = properties["mana_multiplier"].ToObject<string>();

    }

    override public IEnumerator Cast(Vector3 where, Vector3 target, Hittable.Team team)
    {
        yield return inner.Cast(where, target, team);
    }

    override public int GetDamage()
    {
        return inner.GetDamage();
    }
}

public class SpeedAmp : ModifierSpell
{
    public SpeedAmp()
    {

    }

    override public void SetProperties(JObject properties)
    {
        isModifier = true;

        name = properties["name"].ToString();
        description = properties["description"].ToObject<string>();
        speed_multiplier = properties["speed_multiplier"].ToObject<string>();

    }

    override public IEnumerator Cast(Vector3 where, Vector3 target, Hittable.Team team)
    {
        yield return inner.Cast(where, target, team);
    }
}

public class Chaos : ModifierSpell
{
    public Chaos()
    {

    }

    override public void SetProperties(JObject properties)
    {
        isModifier = true;

        name = properties["name"].ToString();
        description = properties["description"].ToObject<string>();
        damage_multiplier = properties["damage_multiplier"].ToObject<string>();
        projectile_trajectory = properties["projectile_trajectory"].ToObject<string>();

    }

    override public IEnumerator Cast(Vector3 where, Vector3 target, Hittable.Team team)
    {
        yield return inner.Cast(where, target, team);
    }
}
public class Homing : ModifierSpell
{
    public Homing()
    {

    }

    override public void SetProperties(JObject properties)
    {
        isModifier = true;

        name = properties["name"].ToString();
        description = properties["description"].ToObject<string>();
        mana_adder = properties["mana_adder"].ToObject<string>();
        damage_multiplier = properties["damage_multiplier"].ToObject<string>();
        projectile_trajectory = properties["projectile_trajectory"].ToObject<string>();

    }

    override public IEnumerator Cast(Vector3 where, Vector3 target, Hittable.Team team)
    {
        yield return inner.Cast(where, target, team);
    }
}

