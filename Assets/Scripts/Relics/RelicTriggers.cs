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

}

public class EnemyDeath : RelicTriggers
{

    private static List<RelicEffects> effects;

    public static void Invoke(EnemyController enemy)
    {
        foreach (RelicEffects effect in effects)
        {
            //effect.OnEnemyDeath(enemy);
        }
    }

    public static void Register(RelicEffects effect)
    {
        effects.Add(effect);
    }

    public EnemyDeath() { }
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