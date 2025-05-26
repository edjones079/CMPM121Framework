using UnityEngine;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using TMPro;

public class MenuSelectorController : MonoBehaviour
{
    public TextMeshProUGUI label;
    public string level;
    public EnemySpawner spawner;
    public ClassSelector class_selector;
    public PlayerController player;
    public JToken class_stats;

    public GameObject curr_screen;
    public GameObject next_screen;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        curr_screen = GameObject.Find("ClassSelectorScreen");
        next_screen = GameObject.Find("DifficultySelector");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLevel(string text)
    {
        level = text;
        label.text = text;
    }

    public void GetClass(string text, JToken c)
    {
        class_stats = c;
        level = text;
        label.text = text;
    }

    public void StartLevel()
    {
        spawner.StartLevel(level);
    }

    public void SetClass()
    {
        player.SetClass(class_stats);
        next_screen.SetActive(true);
        curr_screen.SetActive(false);
    }

}
