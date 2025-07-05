using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    public int completedLevels;
    // the values defined in this constructor will be the default values
    // the game starts with when there's no data to load
    public GameData()
    {
        completedLevels = 1;
    }
}
