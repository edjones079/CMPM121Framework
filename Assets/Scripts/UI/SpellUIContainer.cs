using System.Collections.Specialized;
using UnityEngine;
using static System.Net.Mime.MediaTypeNames;

public class SpellUIContainer : MonoBehaviour
{
    public GameObject[] spellUIs;
    public GameObject newSpell;
    public PlayerController player;
    SpellCaster spellcaster;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // we only have one spell (right now)
        spellUIs[0].SetActive(true);
        for(int i = 1; i< spellUIs.Length; ++i)
        {
            spellUIs[i].SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DropSpell(Spell spell_to_drop)
    {

        if (spellcaster.spellbook.Count <= 1)
            return;

        int indice = 0; 

        for(int i = 0; i< spellUIs.Length; ++i)
        {
            if (spellUIs[i].GetComponent<SpellUI>().spell == spell_to_drop)
            {
                break;
            }
        }

        for (int j = indice + 1; j < spellcaster.spellbook.Count; ++j) 
        {
            //spellUIs[indice].GetComponent<SpellUI>().spell = spellUIs[j].GetComponent<SpellUI>().spell;
            UnityEngine.Debug.Log("Indice: " + indice + ":: j: " + j);
            spellUIs[j - 1].GetComponent<SpellUI>().SetSpell(spellUIs[j].GetComponent<SpellUI>().spell);
        }

        spellUIs[spellcaster.spellbook.Count - 1].SetActive(false);
        spellcaster.DropSpell(indice);
    }

    public void AddSpell()
    {
        spellcaster = player.spellcaster;
        if (spellcaster.spellbook.Count < 4) {
            spellcaster.AddSpell();
            spellUIs[spellcaster.spellbook.Count - 1].SetActive(true);
            spellUIs[spellcaster.spellbook.Count - 1].GetComponent<SpellUI>().SetSpell(spellcaster.reward_spell);
        }
        newSpell.SetActive(false);
    }

    public void UnHighlightSpell(int old_spell)
    {
        spellUIs[old_spell].transform.GetChild(0).gameObject.SetActive(false);
    }

    public void HighlightSpell(int new_spell)
    {
        spellUIs[new_spell].transform.GetChild(0).gameObject.SetActive(true);
    }
}
