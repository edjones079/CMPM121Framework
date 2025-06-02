using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using Unity.Jobs;
using UnityEngine.Rendering.Universal;

public class RelicTriggers
{

    protected RPNEvaluator rpn = new RPNEvaluator();
    protected Dictionary<string, int> variables = new Dictionary<string, int>();

    protected int amount;
    protected string until;
    protected bool applied;

    virtual public void Register(RelicEffects effect)
    {
        
    }

    virtual public void ApplyEffect()
    {

    }

    virtual public void RemoveEffect()
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
        if (effect.until != null)
        {
            if (effect.until == "cast-spell")
            {
                EventBus.Instance.OnCastSpell += effect.remove;
            }
            else if (effect.until == "move")
            {
                EventBus.Instance.OnMove += effect.remove;
            }
        }
    }

}

public class StandStill : RelicTriggers
{
    RelicEffects effect = new RelicEffects();

    public StandStill(string amount)
    {
        this.amount = rpn.Eval(amount, variables);
        EventBus.Instance.OnStandStill += ApplyEffect;
    }

    override public void Register(RelicEffects effect)
    {
        this.effect = effect;
    }

    override public void ApplyEffect()
    {
        effect.apply();
        if (effect.until != null)
        {
            if (effect.until == "cast-spell")
            {
                EventBus.Instance.OnCastSpell += effect.remove;
            }
            else if (effect.until == "move")
            {
                EventBus.Instance.OnMove += effect.remove;
            }
        }
    }
}

public class TakeDamage : RelicTriggers
{
    RelicEffects effect = new RelicEffects();

    public TakeDamage()
    {
        EventBus.Instance.OnTakeDamage += ApplyEffect;
        EventBus.Instance.OnCastSpell -= RemoveEffect;
    }

    override public void Register(RelicEffects effect)
    {
        this.effect = effect;
    }

    override public void ApplyEffect()
    {
        if (!applied)
        {
            effect.apply();
            applied = true;
            Debug.Log("Effect applied!");
            Debug.Log("applied is: " + applied);
        }
    }

    override public void RemoveEffect()
    {
        Debug.Log("Event heard!");
        if (applied)
        {
            effect.remove();
            applied = false;
            Debug.Log("Effect removed!");
        }
    }
}

public class MaxMana : RelicTriggers
{
    RelicEffects effect = new RelicEffects();

    public MaxMana()
    {
        EventBus.Instance.OnMaxMana += ApplyEffect;
    }

    override public void Register(RelicEffects effect)
    {
        this.effect = effect;
    }

    override public void ApplyEffect()
    {
        effect.apply();
        if (effect.until != null)
        {
            if (effect.until == "cast-spell")
            {
                EventBus.Instance.OnCastSpell += effect.remove;
            }
            else if (effect.until == "move")
            {
                EventBus.Instance.OnMove += effect.remove;
            }
        }
    }
}
