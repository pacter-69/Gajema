using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseHandler : MonoBehaviour
{
    public GameObject pausePanel;
    [SerializeField] public InputActionReference pause;
    public Slider musicSlider;
    public Slider SFXSlider;

    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Sound").GetComponent<AudioManager>();
    }

    private void Update()
    {
        if (pause.action.WasPerformedThisFrame())
        {
            if (Time.timeScale == 1)
            {
                PauseGame();
            }
            else
            {
                ContinueCurrent();
            }

        }
        //audioManager.MusicVolume(musicSlider.value);
        //audioManager.SFXVolume(SFXSlider.value);
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }
    public void ContinueCurrent()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }
    public void RestartCurrent()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
