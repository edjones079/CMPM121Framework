using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

public class Modifiers
{

}

public class SpellModifiers : Modifiers
{

    public List<ValueModifier<int>> damage = new List<ValueModifier<int>>();
    public List<ValueModifier<float>> speed = new List<ValueModifier<float>>();
    public List<ValueModifier<float>> lifetime = new List<ValueModifier<float>>();
    public List<ValueModifier<int>> mana_cost = new List<ValueModifier<int>>();
    public List<ValueModifier<int>> spellpower = new List<ValueModifier<int>>();
    public List<ValueModifier<float>> cooldown = new List<ValueModifier<float>>();
    public List<ValueModifier<string>> projectile_trajectory = new List<ValueModifier<string>>();

    public SpellModifiers() 
    { 

    }

    public void AddManaCostMod(ValueModifier<int> mod)
    {
        mana_cost.Add(mod);
    }

    public void AddDamageMod(ValueModifier<int> mod)
    {
        damage.Add(mod);
    }

    public void AddSpeedMod(ValueModifier<float> mod)
    {
        speed.Add(mod);
    }

    public void AddCooldownMod(ValueModifier<float> mod)
    {
        cooldown.Add(mod);
    }

    public void AddLifetimeMod(ValueModifier<float> mod)
    {
        lifetime.Add(mod);
    }

    public void AddSpellpowerMod(ValueModifier<int> mod)
    {
        spellpower.Add(mod);
    }

    public void AddProjectileTrajectoryMod(ValueModifier<string> mod)
    {
        projectile_trajectory.Add(mod);
    }
}

public class PhysicalModifiers : Modifiers
{
    public List<ValueModifier<int>> defense = new List<ValueModifier<int>>();

    public void AddDefenseMod(ValueModifier<int> mod)
    {
        defense.Add(mod);
    }
}


