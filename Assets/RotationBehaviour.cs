using UnityEngine;

public class RotationBehaviour : MonoBehaviour
{
    private MovementBehaviour m_Movement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_Movement = GetComponent<MovementBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        //m_Movement.
    }
}
