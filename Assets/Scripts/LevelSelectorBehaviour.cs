using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using JetBrains.Annotations;

public class LevelSelectorBehaviour : MonoBehaviour, IDataPersistence
{
    public List<LevelButton> allLevels;
    public int levelsUnlocked;

    public void Awake()
    {
        for (int i = 1; i <= allLevels.Count; i++)
        {
            allLevels[i].unlocked = true;
        }
    }

    public void LoadData(GameData data)
    {
        levelsUnlocked = data.completedLevels;
    }

    public void SaveData(ref GameData data)
    {
        data.completedLevels = levelsUnlocked;
    }
}
