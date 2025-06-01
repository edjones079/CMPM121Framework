using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;
using TMPro;
using static System.Net.Mime.MediaTypeNames;

public class PlayerRelicUI : MonoBehaviour
{
    public PlayerController player;
    public int index;

    public Image icon;
    public GameObject highlight;
    //public TextMeshProUGUI label;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // if a player has relics, this is how you *could* show them

    }

    // Update is called once per frame
    void Update()
    {
        // Relics could have labels and/or an active-status
        if (HasRelics())
        {
            Relic r = player.relics[index];
            //label.text = r.GetName();
            //label.fontSize = 10;
            GameManager.Instance.relicIconManager.PlaceSprite(r.GetIcon(), icon);
        }
        
        //highlight.SetActive(r.IsActive());
        
    }

    public bool HasRelics()
    {
        return (player.relics.Count > 0);
    }
}
