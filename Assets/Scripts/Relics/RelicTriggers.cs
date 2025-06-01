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

    override public void Register(RelicEffects effect)
    {
        this.effect = effect;   
    }

    override public void ApplyEffect()
    {
        effect.apply();
    }

    public EnemyDeath() 
    {
        EventBus.Instance.OnEnemyDeath += ApplyEffect;
    }
}

public class StandStill : RelicTriggers
{
    public StandStill(string amount)
    {
        this.amount = rpn.Eval(amount, variables);
    }

    public void ActivateEffect(PlayerController player)
    {

    }
}

public class TakeDamage : RelicTriggers
{
    public TakeDamage()
    {
        
    }

    public void ActivateEffect(Vector3 where, Damage dmg, Hittable target)
    {

    }
}