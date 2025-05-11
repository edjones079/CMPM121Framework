using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;


public class SpellBuilder 
{

    Dictionary<string, Spell> spell_types = new Dictionary<string, Spell>();

    JObject attributes;

    private SpellBuilder MakeSpell()

    public Spell BuildSpell(SpellCaster owner)
    {
        return new Spell(owner);
    }

    virtual public void SetAttributes(JToken attributes)
    {
        return;
    }

    public SpellBuilder()
    {        

    }

    void Start()
    {
        var spelltext = Resources.Load<TextAsset>("spells");

        JToken jo = JToken.Parse(spelltext.text);
        foreach (var spell in jo)
        {
            Spell s = spell.ToObject<Spell>();
            spell_types[s.name] = s;
            UnityEngine.Debug.Log(s.name);
        }
    }

}
