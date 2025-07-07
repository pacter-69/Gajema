using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using JetBrains.Annotations;

public class LevelSelectorBehaviour : MonoBehaviour, IDataPersistence
{
    public List<LevelButton> allLevels;
    public int levelsUnlocked;

    public FadeTransition fade; //A�adido para transici�n

    public void Awake()
    {
        for (int i = 0; i < levelsUnlocked; i++)
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
    

    
    public void LoadLevel(int levelNumber) //A�adido para transici�n
    {
        string sceneName = "Level" + levelNumber;
        fade.FadeToBlack(sceneName);
    }
    /* Nota: El script o los scripts LevelButton no los he tocado pero entiendo que es por lo de bloquear los
    niveles. Si es conflictivo con mis cambios avisar. Quiz�s sea m�s f�cil a�adir referencias gameObject de 
    los botones nivel y gestionar los bloqueos y el desbloqueo debug
    todo desde este mismo Script*/
    //aviso que es conflictivo

}
