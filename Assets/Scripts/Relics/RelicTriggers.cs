using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System;
using System.Threading;

public class RelicTriggers
{

    protected RPNEvaluator rpn = new RPNEvaluator();
    protected Dictionary<string, int> variables = new Dictionary<string, int>();

    protected PlayerController owner;
    protected int amount;

    virtual public void Register(RelicEffects effect)
    {
        
    }

    virtual public void ApplyEffect()
    {

    }

    virtual public void RemoveEffect(RelicEffects effect)
    {

    }

}

public class EnemyDeath : RelicTriggers
{
    RelicEffects effect = new RelicEffects();

    public EnemyDeath(PlayerController owner)
    {
        this.owner = owner;
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

    override public void RemoveEffect(RelicEffects effect)
    {

    }

}

public class StandStill : RelicTriggers
{
    RelicEffects effect = new RelicEffects();

    public StandStill(string amount, PlayerController owner)
    {
        this.amount = rpn.Eval(amount, variables);
        this.owner = owner;

        StartTimer();
        UnityEngine.Debug.Log("Coroutine Started!");
        owner.unit.OnMove += ResetTimer;
    }

    override public void Register(RelicEffects effect)
    {
        this.effect = effect;
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(amount);
        ApplyEffect();
    }

    public void StartTimer()
    {
        CoroutineManager.Instance.Run(Timer());
        
    }

    public void ResetTimer(float val)
    {
        CoroutineManager.Instance.Cancel(Timer());
        StartTimer();
        UnityEngine.Debug.Log("Coroutine Restarted!");
    }

    override public void ApplyEffect()
    {
        effect.apply();
        UnityEngine.Debug.Log("Effect Applied!");
    }

    override public void RemoveEffect(RelicEffects effect)
    {

    }
}

public class TakeDamage : RelicTriggers
{
    RelicEffects effect = new RelicEffects();

    public TakeDamage(PlayerController owner)
    {
        this.owner = owner;
        EventBus.Instance.OnTakeDamage += ApplyEffect;
    }

    override public void Register(RelicEffects effect)
    {
        this.effect = effect;
    }

    override public void ApplyEffect()
    {
        effect.apply();
    }

    override public void RemoveEffect(RelicEffects effect)
    {

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
    }

    override public void RemoveEffect(RelicEffects effect)
    {

    }
}
