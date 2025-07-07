using UnityEngine;
using UnityEngine.Tilemaps;

public class BoxBehaviour : MonoBehaviour
{
    public float stepSize = 1f;
    public float moveDuration = 0.1f;

    public TilemapCollider2D iceTilemapCollider, windTilemapCollider, celoTilemapCollider, starTilemapCollider, floorTilemapCollider;

    private bool isMoving = false;
    private bool hasSavedThisSlide = false;
    [SerializeField] private bool isInIce, isInWind, isInCelo;
    private Vector3 targetPosition;
    private Vector3 startPosition;
    private float moveTimer = 0f;

    AudioManager audioManager;

    private Vector2Int lastDirection;
    private System.Func<Vector2Int, bool> isBlockedFunc;

    public Type type;
    public enum Type
    {
        Normal,
        Steel,
        Linked
    }

    [Header("Linked boxes")]
    public BoxBehaviour[] otherBoxes;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Sound")?.GetComponent<AudioManager>();
    }

    private void Start()
    {
        stepSize = GetComponentInParent<Grid>().cellSize.x;
        SnapToGrid();
        SetBlockChecker(DefaultBlockCheck);
    }

    private void Update()
    {
        if (!isMoving)
        {
            Vector2Int current = GetGridPosition();

            if (IsWindActive() && type != Type.Steel && isInWind)
            {
                Vector2Int windDir = GetWindDirection();
                if (windDir != Vector2Int.zero && !isBlockedFunc(current + windDir))
                {
                    TryPush(windDir, current, isBlockedFunc);
                    return;
                }
            }
            else if (IsIceActive() && isInIce)
            {
                if (lastDirection != Vector2Int.zero)
                {
                    if (!isBlockedFunc(current + lastDirection))
                    {
                        TryPush(lastDirection, current, isBlockedFunc);
                    }
                    else
                    {
                        // Detenida por obstáculo: olvidamos dirección
                        lastDirection = Vector2Int.zero;
                    }
                }
            }

            hasSavedThisSlide = false;
        }

        if (isMoving)
        {
            moveTimer += Time.deltaTime;
            float t = Mathf.Clamp01(moveTimer / moveDuration);
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);

            if (t >= 1f)
            {
                transform.position = targetPosition;
                isMoving = false;
                audioManager?.PlaySFX(audioManager.push);
            }
        }
    }

    public bool TryManualPush(Vector2Int direction, System.Func<Vector2Int, bool> isBlocked)
    {
        if (!CanMove(direction, isBlocked)) return false;

        foreach (BoxBehaviour box in otherBoxes)
        {
            if (box != null && !box.CanMove(direction, isBlocked))
            {
                return false;
            }
        }

        bool moved = TryPush(direction, GetGridPosition(), isBlocked);

        foreach (BoxBehaviour box in otherBoxes)
        {
            if (box != null)
            {
                box.TryPush(direction, box.GetGridPosition(), isBlocked);
            }
        }

        return moved;
    }

    private bool CanMove(Vector2Int direction, System.Func<Vector2Int, bool> isBlocked)
    {
        if ((GetComponentInParent<LayerBehaviour>()?.isSticky ?? false) && isInCelo) return false;
        if (type == Type.Steel || isMoving) return false;

        Vector2Int nextPos = GetGridPosition() + direction;
        return !isBlocked(nextPos);
    }

    public bool TryPush(Vector2Int direction, Vector2Int currentBoxGridPos, System.Func<Vector2Int, bool> isBlocked)
    {
        if (type == Type.Steel || isMoving) return false;

        Vector2Int nextPos = currentBoxGridPos + direction;
        if (isBlocked(nextPos)) return false;

        if (!hasSavedThisSlide)
        {
            Save();
            hasSavedThisSlide = true;
        }

        lastDirection = direction;
        isBlockedFunc = isBlocked;

        startPosition = transform.position;
        targetPosition = new Vector3(nextPos.x * stepSize, nextPos.y * stepSize, 0f);
        moveTimer = 0f;
        isMoving = true;

        return true;
    }

    private bool DefaultBlockCheck(Vector2Int position)
    {
        Vector3 worldPos = new Vector3(position.x * stepSize, position.y * stepSize, 0f);
        Collider2D hit = Physics2D.OverlapBox(worldPos, new Vector2(0.4f, 0.4f), 0f, LayerMask.GetMask("Boxes", "Walls"));
        return hit != null;
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
        return layer?.wDirection switch
        {
            LayerBehaviour.WindDirection.North => Vector2Int.up,
            LayerBehaviour.WindDirection.South => Vector2Int.down,
            LayerBehaviour.WindDirection.East => Vector2Int.right,
            LayerBehaviour.WindDirection.West => Vector2Int.left,
            _ => Vector2Int.zero
        };
    }

    private void Save()
    {
        GetComponentInParent<ControlZ>()?.SaveScene();
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if ((collision.gameObject == starTilemapCollider || iceTilemapCollider) && collision.gameObject.CompareTag("Ice"))
        {
            isInIce = true;
            isInCelo = false;
            isInWind = false;
        }

        if ((collision.gameObject == starTilemapCollider || windTilemapCollider) && collision.gameObject.CompareTag("Wind"))
        {
            isInIce = false;
            isInCelo = false;
            isInWind = true;
        }

        if ((collision.gameObject == starTilemapCollider || celoTilemapCollider) && collision.gameObject.CompareTag("Celo"))
        {
            isInIce = false;
            isInCelo = true;
            isInWind = false;
        }

        if ((collision.gameObject == starTilemapCollider || floorTilemapCollider) && collision.gameObject.CompareTag("Floor"))
        {
            isInIce = false;
            isInCelo = false;
            isInWind = false;
        }
    }
}
