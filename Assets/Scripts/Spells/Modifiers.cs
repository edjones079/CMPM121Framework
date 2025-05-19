using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

public class Modifiers
{

    public Modifiers() 
    { 

    }

    virtual public int Apply(int value)
    {
        return value; 
    }

    virtual public string Change(string name)
    {
        return name;
    }
}

public class Adder : Modifiers
{
    int amount; 

    override public int Apply(int value)
    {
        return value + amount;
    }

    Adder(int amount) { this.amount = amount;  }
}

public class Multiplier : Modifiers
{
    int amount;

    override public int Apply(int value)
    {
        return value * amount;
    }

    Multiplier(int amount) { this.amount = amount; }
}

