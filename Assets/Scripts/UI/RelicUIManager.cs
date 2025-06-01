using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;
using TMPro;
using static System.Net.Mime.MediaTypeNames;

public class RelicUIManager : MonoBehaviour
{

    public GameObject relicUIPrefab;
    public GameObject player;
    public PlayerController playerController;

    public GameObject rewardRelicContainer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnRelicPickup(Relic r)
    {
        // make a new Relic UI representation
        GameObject rui = Instantiate(relicUIPrefab, transform);
        rui.transform.localPosition = new Vector3(-525 + 40 * (playerController.relics.Count - 1), 0, 0);
        PlayerRelicUI ruic = rui.GetComponent<PlayerRelicUI>();
        ruic.player = playerController;
        ruic.index = playerController.relics.Count - 1;
        
    }

    public void AddRelic(Relic relic)
    {
        if (!playerController.relics.Contains(relic))
        {
            relic.SetOwner(playerController);
            playerController.relics.Add(relic);

            rewardRelicContainer.SetActive(false);
            OnRelicPickup(relic);
        }

    }
}
