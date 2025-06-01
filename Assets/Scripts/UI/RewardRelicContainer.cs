using UnityEngine;

public class RewardRelicContainer : MonoBehaviour
{
    public GameObject[] rewardRelics;

    public void SetRelicUI(Relic relic_to_set, int index)
    {
        rewardRelics[index].GetComponent<RelicUI>().SetRelic(relic_to_set);
    }
}
