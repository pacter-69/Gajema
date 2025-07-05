using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalBehaviour : MonoBehaviour, IDataPersistence
{
    [SerializeField] private int levelNumber;
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
        DataPersistenceManager.instance.SaveGame();
        SceneManager.LoadScene("Level Selector");
    }
}
