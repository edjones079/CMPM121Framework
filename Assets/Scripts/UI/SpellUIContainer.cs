using UnityEngine;

public class SpellUIContainer : MonoBehaviour
{
    public GameObject[] spellUIs;
    public PlayerController player;

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
    public void DropSpell()
    {
        /*if (this.spell != player.spellcaster.spellbook[player.spellcaster.spellbook.Count - 1])
        {

        }
        else
        {
            this.spell = null;
        }*/
    }

    public void AddSpell()
    {
        return;
    }
}
