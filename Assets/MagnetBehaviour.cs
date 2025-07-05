using UnityEngine;
using UnityEngine.UIElements;

public class MagnetBehaviour : MonoBehaviour
{
    public enum RayDirection {
        Up,
        Down,
        Left,
        Right
    }

    public RayDirection currentDirection;
    Vector2Int direction;
    public float rayDistance;

    private void Start()
    {

        direction = Vector2Int.zero;

        switch (currentDirection)
        {
            case RayDirection.Up:
                direction = Vector2Int.up;
                break;
            case RayDirection.Down:
                direction = Vector2Int.down;
                break;
            case RayDirection.Left:
                direction = Vector2Int.left;
                break;
            case RayDirection.Right:
                direction = Vector2Int.right;
                break;
        }
    }

    void Update()
    {
        
    }
    //void GetBox()
    //{
    //    RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, rayDistance);
    //    Debug.DrawRay(transform.position+new Vector3(0,0.55f,0), (Vector2)direction * rayDistance, Color.red);
    //    if (hit.collider.gameObject != null)
    //    {
    //        if(hit.collider.gameObject != gameObject)
    //        {
    //            var box = hit.collider.gameObject.GetComponent<BoxBehaviour>();
    //            if (hit.collider.gameObject.tag == "Metal")
    //            {
    //                box = hit.collider.gameObject.GetComponent<BoxBehaviour>();
    //                box.GoToMagnet((direction * -1));
    //                box.magnet = true;
    //                //Debug.Log(box.name);
    //            }
    //            else
    //            {
    //                box.magnet = false;
    //            }
    //        }
    //    }
    //}
    public Vector2Int GetMagnet()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, rayDistance);
        Debug.DrawRay(transform.position+new Vector3(0,0.55f,0), (Vector2)direction * rayDistance, Color.red);
        if (hit.collider.gameObject != null)
        {
            if (hit.collider.gameObject != gameObject)
            {
                var box = hit.collider.gameObject.GetComponent<BoxBehaviour>();
                if (hit.collider.gameObject.tag == "Metal")
                {
                    box.magnet = true;
                    return direction * -1;
                }
                else
                {
                    box.magnet = false;
                }
            }
        }
        return Vector2Int.zero;
    }
}
