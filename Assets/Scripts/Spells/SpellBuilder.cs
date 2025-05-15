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

    private (Spell, string) MakeSpell(string name)
    {
        if (name == "arcane_bolt")
            return (new ArcaneBolt(), name);
        else if (name == "magic_missile")
            return (new MagicMissile(), name);
        else if (name == "arcane_blast")
            return (new ArcaneBlast(), name);
        else if (name == "arcane_spray")
            return (new ArcaneSpray(), name);

        return (new ArcaneBolt(), name);
    }

    private (Spell, string) MakeRandomSpell()
    {
        int val = UnityEngine.Random.Range(0, spells.Count - 1);
        UnityEngine.Debug.Log(val);
        return MakeSpell(spells[val]);
    }

    // Creates a Spell object and assigns it the corresponding attributes to the JSON file

    public Spell BuildSpell(SpellCaster owner)
    {
        var spell_data = MakeRandomSpell();

        Spell spell = spell_data.Item1;
        string name = spell_data.Item2;

        UnityEngine.Debug.Log(spell);

        JObject inner = properties[name].Value<JObject>();

        spell.SetProperties(inner);
        spell.SetOwner(owner);

        //random_spell.SetProperties(inner);
        //random_spell.SetOwner(owner);

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
