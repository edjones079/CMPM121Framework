using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ArcaneBlast : Spell
{
    RPNEvaluator rpnEval = new RPNEvaluator();
    string N;
    string secondary_damage;
    Dictionary<string, string> secondary_projectile = new Dictionary<string, string>();
    Dictionary<string, int> variables = new Dictionary<string, int>();

    public ArcaneBlast()
    {
        
    }

    public override void SetProperties(JObject properties)
    {
        name = properties["name"].ToString();
        icon = properties["icon"].ToObject<int>();
        description = properties["description"].ToObject<string>();
        N = properties["N"].ToString();
        damage = properties["damage"]["amount"].ToString();
        secondary_damage = properties["secondary_damage"].ToString();
        damage_type = Damage.TypeFromString(properties["damage"]["type"].ToString());
        mana_cost = properties["mana_cost"].ToString();
        cooldown = properties["cooldown"].ToString();
        projectile["trajectory"] = properties["projectile"]["trajectory"].ToString();
        projectile["speed"] = properties["projectile"]["speed"].ToString();
        projectile["sprite"] = properties["projectile"]["sprite"].ToString();
        secondary_projectile["trajectory"] = properties["secondary_projectile"]["trajectory"].ToString();
        secondary_projectile["speed"] = properties["secondary_projectile"]["speed"].ToString();
        secondary_projectile["lifetime"] = properties["secondary_projectile"]["lifetime"].ToString();
        secondary_projectile["sprite"] = properties["secondary_projectile"]["sprite"].ToString();
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
        this.team = team;
        GameManager.Instance.projectileManager.CreateProjectile(sprite, projectile["trajectory"], where, target - where, speed, OnHit);
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
