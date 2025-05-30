using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;
using TMPro;
using static System.Net.Mime.MediaTypeNames;

public class RelicUI : MonoBehaviour
{
    public Relic relic;
    public PlayerController player;
    public RelicUIManager relicUIManager;

    public Image icon;
    public TextMeshProUGUI description;
    public GameObject takebutton;

    public void SetRelic(Relic relic)
    {
        this.relic = relic;
        GameManager.Instance.relicIconManager.PlaceSprite(relic.GetIcon(), icon.GetComponent<Image>());
        description.text = relic.GetDescription();
    }

    public void AddRelic()
    {
        relicUIManager.GetComponent<RelicUIManager>().AddRelic(relic);
    }

}
