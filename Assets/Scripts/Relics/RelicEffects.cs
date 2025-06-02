using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;

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
        Debug.Log("Applied!");
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
        Debug.Log("In GainSpellPower.apply()\nOwner's spellpower set to: " + owner.spellcaster.spellpower);
    }

    public override void remove()
    {
        owner.spellcaster.spellpower -= amount;
        Debug.Log("In GainSpellPower.remove()\nOwner's spellpower set to: " + owner.spellcaster.spellpower);
    }
}

public class GainDefense : RelicEffects
{
    public GainDefense(string amount, PlayerController owner)
    {
        variables["wave"] = GameManager.Instance.GetWave();
        this.amount = rpn.Eval(amount, variables);
        this.owner = owner;
    }

    public override void apply()
    {
        //owner.spellcaster.relic_mods = amount;
    }
}
