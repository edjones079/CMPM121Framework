using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System;


public class SpellBuilder
{

    List<string> spells = new List<string>(); // List of all spells from the JSON file
    JObject properties;
    string name;

    HashSet<int> used = new HashSet<int>();

    private Spell MakeSpell()
    {
        int val = UnityEngine.Random.Range(0, spells.Count - 1);

        while (used.Contains(val))
        {
            val = UnityEngine.Random.Range(0, spells.Count - 1);
        }

        used.Add(val);
        
        name = spells[val];
        UnityEngine.Debug.Log(name);

        if (name == "arcane_bolt")
            return new ArcaneBolt();
        else if (name == "magic_missile")
            return new MagicMissile();
        else if (name == "arcane_blast")
            return new ArcaneBlast();
        else if (name == "arcane_spray")
            return new ArcaneSpray();

        else if (name == "damage_amp" || name == "speed_amp" || name == "chaos" || name == "homing")
        {
            return new ModifierSpell();
        }

        else if (name == "doubler")
        {
            return new Doubler();
        }
        else if (name == "splitter")
        {
            return new Splitter();
        }

        return new ArcaneBolt();
    }

    // Creates a Spell object and assigns it the corresponding attributes to the JSON file

    public void BuildSpells(List<string> spell, SpellCaster owner)
    {
        return;
    }

    public Spell BuildSpell(SpellCaster owner)
    {
        Spell spell = MakeSpell();

        //UnityEngine.Debug.Log(name);

        JObject jobject = properties[name].Value<JObject>();
        spell.SetProperties(jobject);
        spell.SetOwner(owner);

        UnityEngine.Debug.Log(jobject);

        if (spell.IsModifier())
        {
            Spell mod_spell = spell;
            spell = BuildSpell(owner);
            spell.AddChild(mod_spell.GetName());
        }

        return spell;
    }

    public SpellBuilder()
    {
        var spelltext = Resources.Load<TextAsset>("spells");

        properties = JObject.Parse(spelltext.text);
        foreach (var a in properties)
        {
            spells.Add(a.Key);
        }
    }

    void Start()
    {
        
    }

}
