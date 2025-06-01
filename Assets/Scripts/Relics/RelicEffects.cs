using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System;

public class RelicEffects
{
    protected RPNEvaluator rpn = new RPNEvaluator();
    protected Dictionary<string, int> variables = new Dictionary<string, int>();

    protected int amount;
    protected string until;
}

public class GainMana : RelicEffects
{
    public GainMana(string amount)
    {
        this.amount = rpn.Eval(amount, variables);
    }

    public void Invoke(EnemyController enemy)
    {
        //spellcaster.mana += amount;
    }
}

public class GainSpellPower : RelicEffects
{
    public GainSpellPower(string amount, string until)
    {
        variables["wave"] = GameManager.Instance.GetWave();
        this.amount = rpn.Eval(amount, variables);
    }
}
