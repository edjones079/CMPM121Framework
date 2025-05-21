using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpellCaster 
{
    public int mana;
    public int max_mana;
    public int mana_reg;
    public int spellpower;
    public Hittable.Team team;
    public Spell spell;
    public Spell newSpell;
    public SpellBuilder spellbuilder = new SpellBuilder();

    public List<Spell> spellbook = new List<Spell>();

    public IEnumerator ManaRegeneration()
    {
        while (true)
        {
            mana += mana_reg;
            mana = Mathf.Min(mana, max_mana);
            yield return new WaitForSeconds(1);
        }
    }

    public SpellCaster(int mana, int mana_reg, int spellpower, Hittable.Team team)
    {
        this.mana = mana;
        this.max_mana = mana;
        this.mana_reg = mana_reg;
        this.spellpower = spellpower;
        this.team = team;
        spell = spellbuilder.BuildSpell("arcane_bolt", this);

        // spell = spellbuilder.BuildSpells("chaos", "doubler", "arcane_spray", this);
        spellbook.Add(spell);

        UnityEngine.Debug.Log("Parent Spell: " + spell);
    }

    public IEnumerator Cast(Vector3 where, Vector3 target)
    {
        Vector3 direction = target - where;
        if (mana >= spell.GetManaCost() && spell.IsReady())
        {
            mana -= spell.GetManaCost();
            yield return spell.Cast(where, target, direction, team);
        }
        yield break;
    }

    public int GetSpellPower()
    {
        return spellpower;
    }

}
