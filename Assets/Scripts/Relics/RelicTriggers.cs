using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System;

public class RelicTriggers
{

    protected RPNEvaluator rpn = new RPNEvaluator();
    protected Dictionary<string, int> variables = new Dictionary<string, int>();

    protected int amount;
    protected string until;

    virtual public void Register(RelicEffects effect)
    {
        
    }

    virtual public void ApplyEffect()
    {

    }

}

public class EnemyDeath : RelicTriggers
{
    RelicEffects effect = new RelicEffects();

    public EnemyDeath()
    {
        EventBus.Instance.OnEnemyDeath += ApplyEffect;
    }

    override public void Register(RelicEffects effect)
    {
        this.effect = effect;   
    }

    override public void ApplyEffect()
    {
        effect.apply();
    }

}

public class StandStill : RelicTriggers
{
    RelicEffects effect = new RelicEffects();

    public StandStill(string amount)
    {
        this.amount = rpn.Eval(amount, variables);
        EventBus.Instance.OnEnemyDeath += ApplyEffect;
    }

    override public void ApplyEffect()
    {
        effect.apply();
    }
}

public class TakeDamage : RelicTriggers
{
    RelicEffects effect = new RelicEffects();

    public TakeDamage()
    {
        EventBus.Instance.OnTakeDamage += ApplyEffect;
    }

    override public void ApplyEffect()
    {
        effect.apply();
    }
}

public class MaxMana : RelicTriggers
{
    RelicEffects effect = new RelicEffects();

    public MaxMana()
    {
        EventBus.Instance.OnMaxMana += ApplyEffect;
    }

    override public void ApplyEffect()
    {
        effect.apply();
    }
}
