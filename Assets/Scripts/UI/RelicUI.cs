using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;
using TMPro;
using static System.Net.Mime.MediaTypeNames;

public class RelicUI : MonoBehaviour
{
    public Relic relic;
    public RelicUIManager relic_ui_manager;

    public Image icon;
    public TextMeshProUGUI description;
    public GameObject takebutton;

    public RelicUI()
    {

    }

    public void SetRelic(Relic relic)
    {
        this.relic = relic;
        GameManager.Instance.relicIconManager.PlaceSprite(relic.GetIcon(), icon.GetComponent<Image>());
        description.text = relic.GetDescription();
    }

    public void AddRelic()
    {
        relic_ui_manager.AddRelic(relic);
    }

}
