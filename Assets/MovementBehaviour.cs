using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class MovementBehaviour : MonoBehaviour
{
    public float stepSize, timer, coolDown, lerpt, sum = 0;
    bool canMove;   

    [SerializeField] public InputActionReference movementLeft;
    [SerializeField] public InputActionReference movementRight;
    [SerializeField] public InputActionReference movementUp;
    [SerializeField] public InputActionReference movementDown;


    public void Start()
    {
        stepSize = gameObject.GetComponentInParent<Grid>().cellSize.x;
    }

    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        if (canMove)
        {
            if (movementLeft.action.IsInProgress())
            {
                Lerp(transform.position.x, transform.position.x - stepSize, true);
                canMove = false;
            }
            else if (movementRight.action.IsInProgress())
            {
                Lerp(transform.position.x, transform.position.x + stepSize, true);
                canMove = false;
            }
            else if (movementUp.action.IsInProgress())
            {
                Lerp(transform.position.y, transform.position.y + stepSize, false);
                canMove = false;
            }
            else if (movementDown.action.IsInProgress())
            {
                Lerp(transform.position.y, transform.position.y - stepSize, false);
                canMove = false;
            }
        }

        if (!canMove)
        {
            timer += Time.deltaTime;
            if (timer >= coolDown)
            {
                canMove = true;
                timer = 0f;
                lerpt = 0f;
            }
        }
    }

    void Lerp(float from, float where, bool x)
    {
        if (sum < 0.2)
        {
            sum += Time.deltaTime;
            lerpt = sum / 0.2f;
        }
        if (x)
        {
            transform.position -= new Vector3(Mathf.Lerp(from, where, lerpt), 0, 0) * Time.deltaTime;
        }
        else
        {
            transform.position += new Vector3(0, Mathf.Lerp(from, where, lerpt), 0) * Time.deltaTime;
        }
    }
}
