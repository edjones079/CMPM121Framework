using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RewardScreenManager : MonoBehaviour
{
    public GameObject rewardUI;

    public GameObject newSpell;
    public GameObject newSpellIcon;

    public GameObject[] newRelics;
    public GameObject[] newRelicIcons;

    public GameObject player;
    public PlayerController playerController;

    public SpellCaster spellcaster;

    public RelicBuilder relicbuilder;

    public TextMeshProUGUI spellInfo;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        relicbuilder = new RelicBuilder();
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
            
            for (int i = 0; i< 3; ++i)
            {
                Relic reward_relic = relicbuilder.BuildRelic();
                GameManager.Instance.spellIconManager.PlaceSprite(reward_relic.GetIcon(), newRelicIcons[i].GetComponent<Image>());
            }
        }
        else
        {
            newSpell.SetActive(true);
            rewardUI.SetActive(false);
        }
    }
}
