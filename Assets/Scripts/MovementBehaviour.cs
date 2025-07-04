using UnityEngine;
using UnityEngine.InputSystem;

public class MovementBehaviour : MonoBehaviour
{
    public float stepSize = 1f;
    public float moveDuration = 0.1f;
    public float moveCooldown = 0.1f;
    public int gridSize;

    private bool isMoving = false;
    private float moveTimer = 0f;
    private float cooldownTimer = 0f;

    private Vector3 startPosition;
    private Vector3 targetPosition;

    [SerializeField] private InputActionReference movementLeft;
    [SerializeField] private InputActionReference movementRight;
    [SerializeField] private InputActionReference movementUp;
    [SerializeField] private InputActionReference movementDown;

    [Header("Limits")]

    [SerializeField] private Transform limitXPlus, limitXMinus, limitYPlus, limitYMinus;

    private Vector2Int currentGridPosition;

    private void Start()
    {


        stepSize = GetComponentInParent<Grid>().cellSize.x;

        // Start in grid space, convert from world
        Vector3 worldPos = transform.position;
        currentGridPosition = new Vector2Int(
            Mathf.RoundToInt(worldPos.x / stepSize),
                Mathf.RoundToInt(worldPos.y / stepSize)
            );

        SnapToGrid();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (!isMoving && cooldownTimer >= 0.4f)
        {
            Vector2Int direction = Vector2Int.zero;

            if (movementLeft.action.IsInProgress()) direction = Vector2Int.left;
            else if (movementRight.action.IsInProgress()) direction = Vector2Int.right;
            else if (movementUp.action.IsInProgress()) direction = Vector2Int.up;
            else if (movementDown.action.IsInProgress()) direction = Vector2Int.down;

            if (direction != Vector2Int.zero)
            {
                Vector2Int nextPos = currentGridPosition + direction;
                GameObject boxObj = GetPushableAt(nextPos);

                if (boxObj != null)
                {
                    var box = boxObj.GetComponent<BoxBehaviour>();
                    var boxCurrentPos = box.GetGridPosition();

                    if (box.TryPush(direction, boxCurrentPos, IsBlocked))
                    {
                        // Player does NOT move when pushing
                        cooldownTimer = moveCooldown;
                    }
                }
                else if (!IsBlocked(nextPos))
                {
                    Vector3 worldPos = transform.position;
                    currentGridPosition = new Vector2Int(
                        Mathf.RoundToInt(worldPos.x / stepSize),
                            Mathf.RoundToInt(worldPos.y / stepSize)
                        );
                    Save();
                    StartMovement(direction); // Valid empty tile, move the player
                }
            }
        }

        if (isMoving)
        {
            AnimateMovement();
        }
    }


    private void StartMovement(Vector2Int direction)
    {
        isMoving = true;
        moveTimer = 0f;
        cooldownTimer = moveCooldown;

        startPosition = transform.position;
        currentGridPosition += direction;
        targetPosition = new Vector3(currentGridPosition.x * stepSize, currentGridPosition.y * stepSize, 0f);
    }


    private void AnimateMovement()
    {
        moveTimer += Time.deltaTime;
        float t = Mathf.Clamp01(moveTimer / moveDuration);
        transform.position = Vector3.Lerp(startPosition, targetPosition, t);

        if (t >= 1f)
        {
            isMoving = false;
        }
    }

    private bool IsWithinBounds(Vector2Int gridPos)
    {
        return gridPos.x >= limitXMinus.position.x && gridPos.x <= limitXPlus.position.x &&
               gridPos.y >= limitYMinus.position.y && gridPos.y <= limitYPlus.position.y;
    }

    private void SnapToGrid()
    {
        transform.position = new Vector3(currentGridPosition.x * stepSize, currentGridPosition.y * stepSize, 0f);
    }

    GameObject GetPushableAt(Vector2Int gridPos)
    {
        // Could be improved with spatial partitioning for performance
        foreach (var box in FindObjectsByType<BoxBehaviour>(FindObjectsSortMode.None))
        {
            if (box.GetGridPosition() == gridPos)
                return box.gameObject;
        }
        return null;
    }

    bool IsBlocked(Vector2Int pos)
    {
        // Customize this to include walls or other boxes
        if (!IsWithinBounds(pos)) return true;

        var box = GetPushableAt(pos);
        if (box != null) return true;

        // Add checks for other obstacles here
        return false;
    }

    void Save()
    {
        GetComponentInParent<ControlZ>().SaveScene();
    }
}
