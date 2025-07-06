using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoalBehaviour : MonoBehaviour, IDataPersistence
{
    [SerializeField] private int levelNumber;
    public GameObject WinPanel;
    public GameObject Canvas;
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Sound").GetComponent<AudioManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<MovementBehaviour>() != null)
        {
            Win();
        }
    }

    public void LoadData(GameData data)
    {

    }

    public void SaveData(ref GameData data)
    {
        if (data.completedLevels < levelNumber)
        {
            data.completedLevels = levelNumber;
        }
    }

    void Win()
    {
        audioManager.PlaySFX(audioManager.Win);
        WinPanel.SetActive(true);
        Canvas.SetActive(false);
        DataPersistenceManager.instance.SaveGame();
    }
}
