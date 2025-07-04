using UnityEngine;
using UnityEngine.InputSystem;

public class PauseHandler : MonoBehaviour
{
    [SerializeField] private InputActionReference pause;
    public GameObject pausePanel;
    void Update()
    {
        if (pause.action.WasPressedThisFrame())
        {
            pausePanel.SetActive(!pausePanel.activeSelf);
        }
    }
}
