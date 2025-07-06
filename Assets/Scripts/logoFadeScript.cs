using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class logoFadeScript : MonoBehaviour
{
    public float a,s;
    public Image logo;
    [SerializeField] public InputActionReference jump;
    // Update is called once per frame
    private void Start()
    {
        logo.color = new Color(logo.color.r, logo.color.g, logo.color.b, a);
    }
    void Update()
    {
        a += Time.deltaTime / 4;
        if (a < 1)
        {
            s += Time.deltaTime / 3;
        }
        logo.transform.localScale = new Vector3(s,s,s);
        logo.color = new Color(logo.color.r, logo.color.g, logo.color.b, a);
        if(a > 1.5 || jump.action.WasPerformedThisFrame())
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
