using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ArcaneSpray : Spell
{
    
    RPNEvaluator rpnEval = new RPNEvaluator();
    string N;
    string spray;
    Dictionary<string, int> variables = new Dictionary<string, int>();

    public ArcaneSpray()
    {

    }

    public override void SetProperties(JObject properties)
    {
        name = properties["name"].ToString();
        icon = properties["icon"].ToObject<int>();
        description = properties["description"].ToObject<string>();
        N = properties["N"].ToString();
        spray = properties["spray"].ToString();
        damage = properties["damage"]["amount"].ToString();
        damage_type = Damage.TypeFromString(properties["damage"]["type"].ToString());
        mana_cost = properties["mana_cost"].ToString();
        cooldown = properties["cooldown"].ToString();
        projectile["trajectory"] = properties["projectile"]["trajectory"].ToString();
        projectile["speed"] = properties["projectile"]["speed"].ToString();
        projectile["sprite"] = properties["projectile"]["sprite"].ToString();
        projectile["lifetime"] = properties["projectile"]["lifetime"].ToString();
    }

    public override string GetName()
    {
        return name;
    }

    public override int GetIcon()
    {
        return icon;
    }

    public override int GetDamage(SpellModifiers mods)
    {
        variables["power"] = owner.GetSpellPower();
        variables["wave"] = GameManager.Instance.GetWave();

        int init_damage = rpnEval.Eval(damage, variables);
        int new_damage = ValueModifier<int>.ApplyModifiers(mods.damage, init_damage);

        return new_damage;
    }

    public override int GetManaCost(SpellModifiers mods)
    {
        variables["power"] = owner.GetSpellPower();

        int init_mana_cost = rpnEval.Eval(mana_cost, variables);
        int new_mana_cost = ValueModifier<int>.ApplyModifiers(mods.mana_cost, init_mana_cost);

        return new_mana_cost;
    }

    public override float GetSpeed(SpellModifiers mods)
    {
        variables["power"] = owner.GetSpellPower();

        int init_speed = rpnEval.Eval(projectile["speed"], variables);
        float new_speed = ValueModifier<float>.ApplyModifiers(mods.speed, init_speed);

        return new_speed;
    }

    public override float GetCooldown(SpellModifiers mods)
    {
        float cd = float.Parse(cooldown);

        float new_cd = ValueModifier<float>.ApplyModifiers(mods.cooldown, cd);

        return new_cd;
    }

    public override float GetLifetime(SpellModifiers mods)
    {
        variables["power"] = owner.GetSpellPower();

        float init_lifetime = rpnEval.EvalFloat(projectile["lifetime"], variables);
        float new_lifetime = ValueModifier<float>.ApplyModifiers(mods.lifetime, init_lifetime);

        return new_lifetime;
    }

    public override string GetProjectileTrajectory(SpellModifiers mods)
    {

        string init_projectile_trajectory = projectile["trajectory"];
        string new_projectile_trajectory = ValueModifier<string>.ApplyModifiers(mods.projectile_trajectory, init_projectile_trajectory);
        return new_projectile_trajectory;
    }


    public override IEnumerator Cast(Vector3 where, Vector3 target, Vector3 direction, Hittable.Team team, SpellModifiers mods)
    {
        variables["power"] = owner.GetSpellPower();
        int speed = rpn.Eval(projectile["speed"], variables);
        int sprite = int.Parse(projectile["sprite"]);
        int projectileCount = rpn.Eval(N, variables);
        float sprayAngle = float.Parse(spray);
        float lifetime = rpn.EvalFloat(projectile["lifetime"], variables);
        this.team = team;
        for (int i = 0; i < projectileCount; i++)
        {
            float r = Random.Range(-(sprayAngle / 2), (sprayAngle / 2));
            float directionAngle = Mathf.Atan2(direction.y, direction.x);
            directionAngle += r;
            Vector3 newDirection = new Vector3(Mathf.Cos(directionAngle), Mathf.Sin(directionAngle), 0);
            GameManager.Instance.projectileManager.CreateProjectile(sprite, GetProjectileTrajectory(mods), where, newDirection, GetSpeed(mods), GetOnHit(mods), GetLifetime(mods));

        }
        yield return new WaitForEndOfFrame();
    }

}
