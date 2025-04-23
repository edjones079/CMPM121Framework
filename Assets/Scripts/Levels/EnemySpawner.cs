using UnityEngine;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Runtime.Versioning;
using System.Collections.Specialized;
using System.Security.Principal;
using System.Diagnostics;

public class RPNEvaluator : MonoBehaviour
{

    Stack<int> stack = new Stack<int>();

    public RPNEvaluator()
    {
        
    }

    public int eval(string expression)
    {
        string[] tokens = expression.Split(' ');
        int final_result;

        foreach (string token in tokens)
        {
            int myInt;
            int value1;
            int value2;
            int result;

            if (int.TryParse(token, out myInt))
                stack.Push(myInt);

            switch (token)
            {
                case "%":
                    value1 = stack.Pop();
                    value2 = stack.Pop();
                    result = value1 % value2;
                    stack.Push(result);
                    break;

                case "*":
                    value1 = stack.Pop();
                    value2 = stack.Pop();
                    result = value1 * value2;
                    stack.Push(result);
                    break;

                case "/":
                    value1 = stack.Pop();
                    value2 = stack.Pop();
                    result = value1 / value2;
                    stack.Push(result);
                    break;

                case "+":
                    value1 = stack.Pop();
                    value2 = stack.Pop();
                    result = value1 + value2;
                    stack.Push(result);
                    break;

                case "-":
                    value1 = stack.Pop();
                    value2 = stack.Pop();
                    result = value1 - value2;
                    stack.Push(result);
                    break;

            }
        }

        final_result = stack.Pop();
        return final_result;

    }
}

// Determines the type and values of constructed objects. 

public class Level : MonoBehaviour
{

    public string enemy;
    public int count;
    public int seqeuence;
    public int delay;
    public string location;
    public int hp;
    public int speed;
    public int damage;

    void Start()
    {
        Dictionary<string, Level> level_types = new Dictionary<string, Level>();
        var leveltext = Resources.Load<TextAsset>("levels");

        JToken jo = JToken.Parse(leveltext.text);
        foreach (var level in jo)
        {
            Level lev = level.ToObject<Level>();
            level_types[lev.name] = lev;
            UnityEngine.Debug.Log(level_types[lev.name]);
        }

    }

    void Update()
    {

    }
}

public class Enemy : MonoBehaviour
{

    public string name;
    public int sprite;
    public int hp;
    public int speed;
    public int damage;

    void Start()
    {
        // 
        Dictionary<string, Enemy> enemy_types = new Dictionary<string, Enemy>();
        var enemytext = Resources.Load<TextAsset>("enemies");

        JToken jo = JToken.Parse(enemytext.text);
        foreach(var enemy in jo)
        {
            Enemy en = enemy.ToObject<Enemy>();
            enemy_types[en.name] = en;
        }

    }

    public class Spawn : MonoBehaviour
    {
        void Start()
        {

        }

        void Update()
        {

        }
    }

    void Update()
    {

    }
}

public class EnemySpawner : MonoBehaviour
{
    public Image level_selector;
    public GameObject button;
    public GameObject enemy;
    public SpawnPoint[] SpawnPoints;    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject selector = Instantiate(button, level_selector.transform);
        selector.transform.localPosition = new Vector3(0, 130);
        selector.GetComponent<MenuSelectorController>().spawner = this;
        selector.GetComponent<MenuSelectorController>().SetLevel("Start");
    }

    // Update is called once per frame
    void Update()
    {
       

    }

    public void StartLevel(string levelname)
    {
        level_selector.gameObject.SetActive(false);
        // this is not nice: we should not have to be required to tell the player directly that the level is starting
        GameManager.Instance.player.GetComponent<PlayerController>().StartLevel();
        StartCoroutine(SpawnWave());
    }

    public void NextWave()
    {
        StartCoroutine(SpawnWave());
    }


    IEnumerator SpawnWave()
    {
        GameManager.Instance.state = GameManager.GameState.COUNTDOWN;
        GameManager.Instance.countdown = 3;
        for (int i = 3; i > 0; i--)
        {
            yield return new WaitForSeconds(1);
            GameManager.Instance.countdown--;
        }
        GameManager.Instance.state = GameManager.GameState.INWAVE;
        for (int i = 0; i < 10; ++i)
        {
            yield return SpawnZombie();
        }
        yield return new WaitWhile(() => GameManager.Instance.enemy_count > 0);
        GameManager.Instance.state = GameManager.GameState.WAVEEND;
    }

    

    IEnumerator SpawnZombie()
    {
        SpawnPoint spawn_point = SpawnPoints[Random.Range(0, SpawnPoints.Length)];
        Vector2 offset = Random.insideUnitCircle * 1.8f;
                
        Vector3 initial_position = spawn_point.transform.position + new Vector3(offset.x, offset.y, 0);
        GameObject new_enemy = Instantiate(enemy, initial_position, Quaternion.identity); // Creates a new enemy

        new_enemy.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.enemySpriteManager.Get(0);
        EnemyController en = new_enemy.GetComponent<EnemyController>();
        en.hp = new Hittable(50, Hittable.Team.MONSTERS, new_enemy);
        en.speed = 10;
        GameManager.Instance.AddEnemy(new_enemy);
        yield return new WaitForSeconds(0.5f);
    }

}
