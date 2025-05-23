using UnityEngine;
using UnityEngine.InputSystem;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public Hittable hp;
    public HealthBar healthui;
    public ManaBar manaui;

    public SpellCaster spellcaster;
    public SpellUI spellui;

    public int speed = 5;
    public int spellpower = 0;

    string hp_scalar = "95 wave 5 * +";
    string mana_scalar = "90 wave 10 * +";
    string mana_regen_scalar = "10 wave +";
    string spellpower_scalar = "wave 10 *";

    public Unit unit;

    Dictionary<string, int> variables = new Dictionary<string, int>();
    RPNEvaluator rpn = new RPNEvaluator();

    public TextMeshProUGUI gameOverText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        unit = GetComponent<Unit>();
        GameManager.Instance.player = gameObject;
        EventBus.Instance.OnWaveEnd += GenerateRandomSpell;
    }

    public void StartLevel()
    {
        variables["wave"] = 1;

        int max_hp = rpn.Eval(hp_scalar, variables);
        int mana = rpn.Eval(mana_scalar, variables);
        int mana_regen = rpn.Eval(mana_regen_scalar, variables);
        spellpower = rpn.Eval(spellpower_scalar, variables);

        spellcaster = new SpellCaster(mana, mana_regen, spellpower, Hittable.Team.PLAYER);
        StartCoroutine(spellcaster.ManaRegeneration());
        
        hp = new Hittable(max_hp, Hittable.Team.PLAYER, gameObject);
        hp.OnDeath += Die;
        hp.team = Hittable.Team.PLAYER;

        // tell UI elements what to show
        healthui.SetHealth(hp);
        manaui.SetSpellCaster(spellcaster);
        spellui.SetSpell(spellcaster.spellbook[0]);

        //Debug.Log("Player HP: " + hp.hp);
        //Debug.Log("Player Mana: " + spellcaster.mana);
        //Debug.Log("Player Mana Regen: " + spellcaster.mana_reg);
    }

    public void NextWave()
    {
        variables["wave"] = GameManager.Instance.GetWave();
        int max_hp = rpn.Eval(hp_scalar, variables);
        hp.SetMaxHP(max_hp);
        spellcaster.mana = rpn.Eval(mana_scalar, variables);
        spellcaster.mana_reg = rpn.Eval(mana_regen_scalar, variables);
        spellcaster.spellpower = rpn.Eval(spellpower_scalar, variables);

        //Debug.Log("Player Max_HP: " + hp.max_hp);
        //Debug.Log("Player Mana: " + spellcaster.mana);
        //Debug.Log("Player Mana Regen: " + spellcaster.mana_reg);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddSpell()
    {
        spellcaster.AddSpell();
    }

    void OnChangeSpell()
    {
        spellcaster.ChangeSpell();
    }

    void GenerateRandomSpell()
    {
        spellcaster.GenerateRandomSpell();
    }

    void OnAttack(InputValue value)
    {
        if (GameManager.Instance.state == GameManager.GameState.PREGAME || GameManager.Instance.state == GameManager.GameState.GAMEOVER) return;
        Vector2 mouseScreen = Mouse.current.position.value;
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(mouseScreen);
        mouseWorld.z = 0;
        StartCoroutine(spellcaster.Cast(transform.position, mouseWorld));
    }

    void OnMove(InputValue value)
    {
        if (GameManager.Instance.state == GameManager.GameState.PREGAME || GameManager.Instance.state == GameManager.GameState.GAMEOVER) return;
        unit.movement = value.Get<Vector2>()*speed;
    }

    void Die()
    {
        Debug.Log("You Lost");
        gameOverText.text = "You Lost...";
        GameManager.Instance.state = GameManager.GameState.GAMEOVER;
    }
}
