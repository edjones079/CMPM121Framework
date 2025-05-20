using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

public class ValueModifier<T>
{
    public T value;

    public ValueModifier()
    {
        
    }

    public virtual T Apply(T value) { return value; }

    public static T ApplyModifiers(List<ValueModifier<T>> mods, T init)
    {
        foreach(var mod in mods)
        {
            init = mod.Apply(init);
        }

        return init;
    }
}

public class Adder : ValueModifier<int>
{
    int amount;

    override public int Apply(int value)
    {
        return value + amount;
    }

    public Adder(int amount) { this.amount = amount; }
}

public class Multiplier : ValueModifier<int>
{
    float amount;

    override public int Apply(int value)
    {
        return (int) (value * amount);
    }

    public Multiplier(float amount) { this.amount = amount; }
}

public class FloatAdder : ValueModifier<float>
{
    float amount;

    override public float Apply(float value)
    {
        return value + amount;
    }

    public FloatAdder(float amount) { this.amount = amount; }
}

public class FloatMultiplier : ValueModifier<float>
{
    float amount;

    override public float Apply(float value)
    {
        return value * amount;
    }

    public FloatMultiplier(float amount) { this.amount = amount; }
}

public class Setter<T> : ValueModifier<T>
{
    public T new_value;

    override public T Apply(T value)
    {
        return new_value;
    }

    public Setter(T new_value) { this.new_value = new_value; }
}
