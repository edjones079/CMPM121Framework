using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;
using TMPro;
using static System.Net.Mime.MediaTypeNames;
using System.Collections.Generic;

public class RewardScreenManager : MonoBehaviour
{
    public GameObject rewardUI;

    public GameObject newSpell;
    public GameObject newSpellIcon;

    public List<Relic> reward_relics = new List<Relic>();

    public GameObject rewardRelicContainer;
    public GameObject[] rewardRelics;

    public GameObject player;
    public PlayerController playerController;

    public SpellCaster spellcaster;

    public RelicBuilder relicbuilder;

    public TextMeshProUGUI spellInfo;

    public delegate void Delegate(int i);
    public Delegate build_relic;
    public Delegate show_relics;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        relicbuilder = new RelicBuilder();

        build_relic = BuildRelic;
        show_relics = ShowRelics;

        EventBus.Instance.OnWaveEnd += BuildRelics;
    }

    // Update is called once per frame
    void Update()
    {
        spellcaster = playerController.spellcaster;
        if (GameManager.Instance.state == GameManager.GameState.WAVEEND)
        {
            rewardUI.SetActive(true);

            // Reward Spell UI

            GameManager.Instance.spellIconManager.PlaceSprite(spellcaster.reward_spell.GetIcon(), newSpellIcon.GetComponent<Image>());

            spellInfo.text = spellcaster.reward_spell.GetName();
            Spell rewardSpellCopy = spellcaster.reward_spell;
            while (rewardSpellCopy.isModifier)
            {
                spellInfo.text += ' ' + rewardSpellCopy.GetInnerSpell().GetName();
                rewardSpellCopy = rewardSpellCopy.GetInnerSpell();
            }
            spellInfo.text += '\n' + spellcaster.reward_spell.GetDescription();
            rewardSpellCopy = spellcaster.reward_spell;
            while (rewardSpellCopy.isModifier)
            {
                spellInfo.text += '\n' + rewardSpellCopy.GetInnerSpell().GetDescription();
                rewardSpellCopy = rewardSpellCopy.GetInnerSpell();
            }

            // Reward Relics UI

            if (OnThirdWave())
            {
                rewardRelicContainer.SetActive(true);
                OnXRelics(show_relics);
            }
        }
        else
        {
            newSpell.SetActive(true);
            rewardRelicContainer.SetActive(false);
            rewardUI.SetActive(false);
        }
    }

    public bool OnThirdWave()
    {
        if (GameManager.Instance.GetWave() % 3 == 0)
        {
            return true;
        }

        return false;
    }

    public void OnXRelics(Delegate relic_method)
    {
        UnityEngine.Debug.Log(rewardRelics.Length);
        for (int i = 0; i < rewardRelics.Length; ++i)
        {
            relic_method(i);
        }
    }

    public void BuildRelic(int i)
    {
        reward_relics.Add(relicbuilder.BuildRelic());
    }

    public void BuildRelics()
    {
        if (OnThirdWave())
            OnXRelics(build_relic);
    }

    public void ShowRelics(int i)
    {
        TextMeshProUGUI relicDescription = rewardRelics[i].transform.Find("RelicDescription").gameObject.GetComponent<TextMeshProUGUI>();;
        GameObject relicIcon = rewardRelics[i].transform.Find("RewardRelicIcon").gameObject;
        GameManager.Instance.spellIconManager.PlaceSprite(reward_relics[i].GetIcon(), relicIcon.GetComponent<Image>());
        relicDescription.text = reward_relics[i].GetDescription();
    }

}
