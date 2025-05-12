using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEngine;

public class MagicMissile : Spell
{
    RPNEvaluator rpnEval = new RPNEvaluator();
    Dictionary<string, int> variables = new Dictionary<string, int>();

    public MagicMissile()
    {

    }

    public override void SetProperties(JObject properties)
    {
        name = properties["name"].ToString();
        icon = properties["icon"].ToObject<int>();
        description = properties["description"].ToObject<string>();
        damage = properties["damage"]["amount"].ToString();
        damage_type = Damage.TypeFromString(properties["damage"]["type"].ToString());
        mana_cost = properties["mana_cost"].ToString();
        cooldown = properties["cooldown"].ToString();
        projectile["trajectory"] = properties["projectile"]["trajectory"].ToString();
        projectile["speed"] = properties["projectile"]["speed"].ToString();
        projectile["sprite"] = properties["projectile"]["sprite"].ToString();
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
