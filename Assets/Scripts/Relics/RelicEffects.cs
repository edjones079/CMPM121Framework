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
    protected string until;
    protected PlayerController owner;

    public virtual void apply()
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
        this.owner = owner;
    }

    public override void apply()
    {
        
    }
}
