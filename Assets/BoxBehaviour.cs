using UnityEngine;

public class BoxBehaviour : MonoBehaviour
{
    public float stepSize = 1f;
    public float moveDuration = 0.1f;

    private bool isMoving = false;
    private Vector3 targetPosition;
    private Vector3 startPosition;
    private float moveTimer = 0f;

    private void Start()
    {
        stepSize = GetComponentInParent<Grid>().cellSize.x;
        SnapToGrid();
    }

    private void Update()
    {
        if (isMoving)
        {
            moveTimer += Time.deltaTime;
            float t = Mathf.Clamp01(moveTimer / moveDuration);
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);

            if (t >= 1f)
                isMoving = false;
        }
    }

    public bool TryPush(Vector2Int direction, Vector2Int currentBoxGridPos, System.Func<Vector2Int, bool> isBlockedFunc)
    {
        if (isMoving) return false;

        Vector2Int nextPos = currentBoxGridPos + direction;
        if (isBlockedFunc(nextPos)) return false; // Something is blocking the box

        // Start moving
        startPosition = transform.position;
        targetPosition = new Vector3(nextPos.x * stepSize, nextPos.y * stepSize, 0f);
        moveTimer = 0f;
        isMoving = true;

        return true;
    }

    public Vector2Int GetGridPosition()
    {
        return new Vector2Int(
            Mathf.RoundToInt(transform.position.x / stepSize),
            Mathf.RoundToInt(transform.position.y / stepSize)
        );
    }

    private void SnapToGrid()
    {
        var gridPos = GetGridPosition();
        transform.position = new Vector3(gridPos.x * stepSize, gridPos.y * stepSize, 0f);
    }
}
