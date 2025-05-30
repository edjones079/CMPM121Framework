using UnityEngine;
using System;

public class EventBus 
{
    private static EventBus theInstance;
    public static EventBus Instance
    {
        get
        {
            if (theInstance == null)
                theInstance = new EventBus();
            return theInstance;
        }
    }

    public event Action OnWaveEnd;
    public event Action OnThirdWave;

    public void DoWaveEnd()
    {
        OnWaveEnd?.Invoke();
    }

    public void DoBuildRelics()
    {
        OnThirdWave?.Invoke();
    }

    //Trigger Events

    public event Action<Vector3, Damage, Hittable> OnDamage;
    public event Action<EnemyController> OnEnemyDeath;
    public event Action<PlayerController> OnStandStill;

    public void DoDamage(Vector3 where, Damage dmg, Hittable target)
    {
        OnDamage?.Invoke(where, dmg, target);
    }

    public void DoEnemyDeath(EnemyController enemy)
    {
        OnEnemyDeath?.Invoke(enemy);
    }

    public void DoStandStill(PlayerController player)
    {
        OnStandStill?.Invoke(player);
    }

}
