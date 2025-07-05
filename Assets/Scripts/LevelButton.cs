using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    public string levelSceneName;
    public bool unlocked;

    public Sprite unlockedSprite, lockedSprite;

    private void Start()
    {
        if (unlocked)
        {
            gameObject.GetComponent<Image>().sprite = unlockedSprite;
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = lockedSprite;
        }
    }


    public void GoToLevel()
    {
        if (unlocked)
        {
            SceneManager.LoadScene(levelSceneName);
        }
    }
}
