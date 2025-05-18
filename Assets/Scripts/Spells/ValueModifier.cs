using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

public class ValueModifier
{
    public ValueModifier() { }

    virtual public void Apply(List<ValueModifier> mods)
    {
        foreach (ValueModifier mod in mods)
        {

        }

        return;
    }
}
