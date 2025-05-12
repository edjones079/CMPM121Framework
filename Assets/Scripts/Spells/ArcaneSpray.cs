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
    public override int GetDamage()
    {
        variables["power"] = owner.GetSpellPower();
        return rpn.Eval(damage, variables);
    }
    public override int GetManaCost()
    {
        variables["power"] = owner.GetSpellPower();
        return rpn.Eval(mana_cost, variables);
    }
    public override float GetCooldown()
    {
        float cd = float.Parse(cooldown);
        return cd;
    }

    public override IEnumerator Cast(Vector3 where, Vector3 target, Hittable.Team team)
    {
        variables["power"] = owner.GetSpellPower();
        int speed = rpn.Eval(projectile["speed"], variables);
        int sprite = int.Parse(projectile["sprite"]);
        int projectileCount = rpn.Eval(N, variables);
        float sprayAngle = float.Parse(spray);
        float lifetime = rpn.EvalFloat(projectile["lifetime"], variables);
        this.team = team;
        for (int i = 0; i < projectileCount - 1; i++)
        {
            float r = Random.Range(-(sprayAngle / 2), (sprayAngle / 2));
            Vector3 direction = target - where;
            float directionAngle = Mathf.Atan2(direction.y, direction.x);
            directionAngle += r;
            Vector3 newDirection = new Vector3(Mathf.Cos(directionAngle), Mathf.Sin(directionAngle), 0);
            GameManager.Instance.projectileManager.CreateProjectile(sprite, projectile["trajectory"], where, newDirection, speed, OnHit, lifetime);

        }
        yield return new WaitForEndOfFrame();
    }

    public override void OnHit(Hittable other, Vector3 impact)
    {
        if (other.team != team)
        {
            other.Damage(new Damage(GetDamage(), damage_type));
        }
    }
}
