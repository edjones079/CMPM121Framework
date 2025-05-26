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

    List<Relic> player_relics = new List<Relic>();
    PlayerController player;

    // Methods

    public void AddRelic(Relic relic)
    {
        if (!player_relics.Contains(relic))
        {
            player_relics.Add(relic);
        }
    }
}
