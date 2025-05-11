using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;


public class SpellBuilder 
{

    List<string> spells = new List<string>();
    JObject properties;
    string name;

    private Spell MakeSpell(string name)
    {
        if (name == "arcane_bolt")
            return new ArcaneBolt();

        return new ArcaneBolt();
    }

    virtual public void SetProperties(JObject properties)
    {
        return;
    }

    // Creates a Spell object and assigns it the corresponding attributes to the JSON file

    public Spell BuildSpell(string name, SpellCaster owner)
    {
        Spell spell = MakeSpell(name);

        spell.SetProperties(properties);
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
            UnityEngine.Debug.Log(properties["arcane_bolt"]);
        }
    }

    void Start()
    {
        
    }

}
