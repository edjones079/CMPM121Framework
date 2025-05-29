using System.Collections.Generic;
using UnityEngine;

public class RelicManager
{
    //Constructor

    private static RelicManager theInstance;
    public static RelicManager Instance
    {
        get
        {
            if (theInstance == null)
                theInstance = new RelicManager();
            return theInstance;
        }
    }

    // Variables

    PlayerController player;

    // Methods

    public void AddRelic(Relic relic)
    {
        if (!player.relics.Contains(relic))
        {
            player.relics.Add(relic);
        }
    }
}
