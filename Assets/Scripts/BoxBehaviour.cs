using UnityEngine;
using UnityEngine.UIElements;
using static MagnetBehaviour;

public class BoxBehaviour : MonoBehaviour
{
    public float stepSize = 1f;
    public float moveDuration = 0.1f;

    private bool isMoving = false;
    private bool hasSavedThisSlide = false;
    private Vector3 targetPosition;
    private Vector3 startPosition;
    private float moveTimer = 0f;
    public bool magnet;


    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Sound").GetComponent<AudioManager>();
    }

    public Vector2Int lastDirection;
    public System.Func<Vector2Int, bool> isBlockedFunc;
    public Type type;
    public enum Type
    {
        Normal,
        Steel,
        Linked, 
        Magnet
    }

    [Header("Linked boxes")]
    public BoxBehaviour[] otherBoxes;

    public enum RayDirection {
        Up,
        Down,
        Left,
        Right
    }

    public RayDirection currentDirection;
    Vector2Int magnetDirection;
    public float rayDistance;

    private void Start()
    {
        if (type == Type.Steel)
        {
            transform.gameObject.tag = "Metal";
        }
        else if (type == Type.Magnet) 
        {
            switch (currentDirection)
            {
                case RayDirection.Up:
                    magnetDirection = Vector2Int.up;
                    break;
                case RayDirection.Down:
                    magnetDirection = Vector2Int.down;
                    break;
                case RayDirection.Left:
                    magnetDirection = Vector2Int.left;
                    break;
                case RayDirection.Right:
                    magnetDirection = Vector2Int.right;
                    break;
            }
        }
        magnet = false;
        stepSize = GetComponentInParent<Grid>().cellSize.x;
        SnapToGrid();
        SetBlockChecker(isBlockedFunc);
    }

    private void Update()
    {
        if (!isMoving)
        {
            Vector2Int current = GetGridPosition();

            if (IsWindActive() && type != Type.Steel)
            {
                Vector2Int windDir = GetWindDirection();
                if (windDir != Vector2Int.zero && !isBlockedFunc(current + windDir))
                {
                    TryPush(windDir, current, isBlockedFunc);
                    return;
                }
            }
            else if (IsIceActive())
            {
                if (lastDirection != Vector2Int.zero && !isBlockedFunc(current + lastDirection))
                {
                    TryPush(lastDirection, current, isBlockedFunc);
                }
            }

            if (magnet && type == Type.Steel)
            {
                Debug.Log(isBlockedFunc(current + magnetDirection));
                if (magnetDirection != Vector2Int.zero && !isBlockedFunc(current + magnetDirection))
                {
                    Debug.Log("caca");
                    TryPush(magnetDirection*-1, current, isBlockedFunc);
                    return;
                }
            }

            if(type == Type.Magnet)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0, 0.55f, 0), magnetDirection, rayDistance);
                Debug.DrawRay(transform.position + new Vector3(0, 0.55f, 0), (Vector2)magnetDirection * rayDistance, Color.red);
                if (hit.collider.gameObject != null)
                {
                    Debug.Log(hit.collider.gameObject.tag == "Metal");
                    Debug.Log(hit.collider.name);
                    var box = hit.collider.gameObject.GetComponent<BoxBehaviour>();
                    if (hit.collider.gameObject.tag == "Metal")
                    {
                        box.magnet = true;
                        box.magnetDirection = magnetDirection;
                        box.SetBlockChecker(box.isBlockedFunc);
                    }
                    else
                    {
                        box.magnet = false;
                    }
                }
            }  

            hasSavedThisSlide = false; // reset for the next slide chain
        }

        if (isMoving)
        {
            moveTimer += Time.deltaTime;
            float t = Mathf.Clamp01(moveTimer / moveDuration);
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);

            if (t >= 1f)
            {
                isMoving = false;
            }
        }
        
    }


    public bool TryPush(Vector2Int direction, Vector2Int currentBoxGridPos, System.Func<Vector2Int, bool> isBlocked)
    {
        if (GetComponentInParent<LayerBehaviour>().isSticky || (type == BoxBehaviour.Type.Steel && !magnet) || isMoving)
        { return false; }

        Vector2Int nextPos = currentBoxGridPos + direction;
        if (isBlocked(nextPos)) return false;

        // Save once at the beginning of the slide
        if (!hasSavedThisSlide)
        {
            Save();
            hasSavedThisSlide = true;
        }

        // Store direction and blocking function
        lastDirection = direction;
        isBlockedFunc = isBlocked;

        // Start moving
        startPosition = transform.position;
        targetPosition = new Vector3(nextPos.x * stepSize, nextPos.y * stepSize, 0f);
        moveTimer = 0f;
        isMoving = true;

        for (int i = 0; i < otherBoxes.Length; i++)
        {
            otherBoxes[i].TryPush(direction, otherBoxes[i].GetGridPosition(), otherBoxes[i].isBlockedFunc);
        }

        return true;
    }

    public Vector2Int GetGridPosition()
    {
        return new Vector2Int(
            Mathf.RoundToInt(transform.position.x / stepSize),
            Mathf.RoundToInt(transform.position.y / stepSize)
        );
    }

    public void SetBlockChecker(System.Func<Vector2Int, bool> blockChecker)
    {
        isBlockedFunc = blockChecker;
    }


    private void SnapToGrid()
    {
        var gridPos = GetGridPosition();
        transform.position = new Vector3(gridPos.x * stepSize, gridPos.y * stepSize, 0f);
    }

    private bool IsIceActive()
    {
        var layer = GetComponentInParent<LayerBehaviour>();
        return layer != null && layer.isIce;
    }

    private bool IsWindActive()
    {
        var layer = GetComponentInParent<LayerBehaviour>();
        return layer != null && layer.isWindy;
    }

    private Vector2Int GetWindDirection()
    {
        var layer = GetComponentInParent<LayerBehaviour>();
        return layer.wDirection switch
        {
            LayerBehaviour.WindDirection.North => Vector2Int.up,
            LayerBehaviour.WindDirection.South => Vector2Int.down,
            LayerBehaviour.WindDirection.East => Vector2Int.right,
            LayerBehaviour.WindDirection.West => Vector2Int.left,
            _ => Vector2Int.zero
        };
    }

    public void GoToMagnet(Vector2Int dir)
    {
        TryPush(dir, GetGridPosition(), isBlockedFunc);
    }


    private void Save()
    {
        GetComponentInParent<ControlZ>()?.SaveScene();
    }
}
