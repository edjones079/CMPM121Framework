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
    }
}

public class GainSpellPower : RelicEffects
{
    public GainSpellPower(string amount, string until)
    {
        variables["wave"] = GameManager.Instance.GetWave();
        this.amount = rpn.Eval(amount, variables);
    }

    public override void apply()
    {
        
    }
}
