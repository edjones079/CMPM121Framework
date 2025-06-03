using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using System.Diagnostics;

public class RelicEffects
{
    protected RPNEvaluator rpn = new RPNEvaluator();
    protected Dictionary<string, int> variables = new Dictionary<string, int>();

    protected int amount;
    public string until;
    protected PlayerController owner;

    public virtual void apply()
    {

    }

    public virtual void remove()
    {

    }
}

public class GainMana : RelicEffects
{
    public GainMana(string amount, PlayerController owner)
    {
        this.amount = rpn.Eval(amount, variables);
        this.owner = owner;
    }

    public override void apply()
    {
        owner.spellcaster.mana += amount;
        UnityEngine.Debug.Log("Applied!");
    }
}

public class GainSpellPower : RelicEffects
{
    public GainSpellPower(string amount, string until, PlayerController owner)
    {
        variables["wave"] = GameManager.Instance.GetWave();
        this.amount = rpn.Eval(amount, variables);
        this.until = until;
        this.owner = owner;
    }

    public override void apply()
    {
        owner.spellcaster.spellpower += amount;
        UnityEngine.Debug.Log("In GainSpellPower.apply()\nOwner's spellpower set to: " + owner.spellcaster.spellpower);
    }

    public override void remove()
    {
        owner.spellcaster.spellpower -= amount;
        UnityEngine.Debug.Log("In GainSpellPower.remove()\nOwner's spellpower set to: " + owner.spellcaster.spellpower);
    }
}

public class GainDefense : RelicEffects
{
    float defense_multiplier;

    public GainDefense(string amount, string until, PlayerController owner)
    {
        variables["wave"] = GameManager.Instance.GetWave();
        defense_multiplier = rpn.EvalFloat(amount, variables);
        this.owner = owner;
        this.until = until;
    }

    public override void apply()
    {
        owner.hp.defense *= defense_multiplier;
        UnityEngine.Debug.Log("In GainDefense.apply()\nOwner's defense set to: " + owner.hp.defense);
    }

    public override void remove()
    {
        owner.hp.defense /= defense_multiplier;
        UnityEngine.Debug.Log("In GainDefense.remove()\nOwner's defense set to: " + owner.hp.defense);
    }
}

