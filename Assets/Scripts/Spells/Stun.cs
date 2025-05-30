using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System;

public class Stun : ModifierSpell
{
    RPNEvaluator rpnEval = new RPNEvaluator();
    Dictionary<string, int> variables = new Dictionary<string, int>();

    public Stun()
    {
        
    }

    public override void SetProperties(JObject properties)
    {
        isModifier = true;

        name = properties["name"].ToString();
        description = properties["description"].ToObject<string>();
        damage_multiplier = properties["damage_multiplier"].ToObject<string>();
        duration = properties["duration"].ToObject<string>();
    }

    public override Action<Hittable, Vector3> GetOnHit(SpellModifiers mods)
    {
        void OnHit(Hittable other, Vector3 impact)
        {
            if (other.team != team)
            {
                other.Damage(new Damage(GetDamage(mods), Damage.Type.ARCANE));
                Debug.Log("Attempting to set enemy's speed to 0");
                other.owner.GetComponent<EnemyController>().SetSpeed(0);
                Debug.Log("enemy's speed is: " + other.owner.GetComponent<EnemyController>().speed);
                int stunDuration = rpnEval.Eval(duration, variables);
                CoroutineManager.Instance.StartCoroutine(Freeze(stunDuration, other));
            }
        }

        return OnHit;
    }

    IEnumerator Freeze(int stunDuration, Hittable other)
    {
        var ec = other.owner.GetComponent<EnemyController>();
        
        int speed = ec.speed;
        ec.speed = 0;
        Debug.Log("ec.speed: " + ec.speed);
        yield return new WaitForSeconds(stunDuration);
        ec.speed = speed;
    }
}
