using UnityEngine;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

public class ArcaneBolt : Spell
{

    public ArcaneBolt()
    {

    }

    override public void SetProperties(JObject properties)
    {
        this.name = properties["name"].ToString();
        UnityEngine.Debug.Log(this.name);
        return;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
