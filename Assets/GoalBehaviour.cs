using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoalBehaviour : MonoBehaviour
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

    void Win()
    {
        audioManager.PlaySFX(audioManager.Win);
        WinPanel.SetActive(true);
        Canvas.SetActive(false);
    }
}
