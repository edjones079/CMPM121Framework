using UnityEngine;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;
using System;

public class ClassSelector : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    List<JObject> class_types = new List<JObject>();
    public GameObject button;
    public int buttonPos;
    public Image class_selector;
    public PlayerController player;

    public ClassSelector()
    {

    }

    void Start()
    {
        var classtext = Resources.Load<TextAsset>("classes");

        JObject jo = JObject.Parse(classtext.text);
        foreach (var c in jo)
        {

            GameObject selector = Instantiate(button, class_selector.transform);
            selector.transform.localPosition = new Vector3(0, buttonPos);
            selector.GetComponent<MenuSelectorController>().player = player;
            selector.GetComponent<MenuSelectorController>().class_selector = this;
            selector.GetComponent<MenuSelectorController>().GetClass(c.Key, c.Value);
            buttonPos -= 50;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
