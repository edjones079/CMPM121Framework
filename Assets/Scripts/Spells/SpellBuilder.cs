using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;


public class SpellBuilder 
{

    List<string> spells = new List<string>(); // List of all spells from the JSON file
    JObject properties;
    string name;

    private Spell MakeSpell(string name)
    {
        if (name == "arcane_bolt")
            return new ArcaneBolt();
        else if (name == "magic_missile")
            return new MagicMissile();
        else if (name == "arcane_blast")
            return new ArcaneBlast();
        else if (name == "arcane_spray")
            return new ArcaneSpray();

        return new ArcaneBolt();
    }

    // Creates a Spell object and assigns it the corresponding attributes to the JSON file

    public Spell BuildSpell(string name, SpellCaster owner)
    {
        Spell spell = MakeSpell(name);

        JObject inner = properties[name].Value<JObject>();

        spell.SetProperties(inner);
        spell.SetOwner(owner);

        return spell;
    }

    public SpellBuilder()
    {
        var spelltext = Resources.Load<TextAsset>("spells");

        properties = JObject.Parse(spelltext.text);
        foreach (var a in properties)
        {
            spells.Add(a.Key);
            //UnityEngine.Debug.Log(properties["arcane_bolt"]);
        }
    }

    void Start()
    {
        
    }

}
