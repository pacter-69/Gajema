using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

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
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Sound").GetComponent<AudioManager>();
    }

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

    private void Start()
    {
        stepSize = GetComponentInParent<Grid>().cellSize.x;
        SnapToGrid();
        SetBlockChecker(isBlockedFunc);
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
                if (lastDirection != Vector2Int.zero && !isBlockedFunc(current + lastDirection))
                {
                    TryPush(lastDirection, current, isBlockedFunc);
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
                audioManager.PlaySFX(audioManager.push);
                isMoving = false;
            }
        }
    }


    public bool TryPush(Vector2Int direction, Vector2Int currentBoxGridPos, System.Func<Vector2Int, bool> isBlocked)
    {
        if (GetComponentInParent<LayerBehaviour>().isSticky || type == BoxBehaviour.Type.Steel || isMoving)
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


    private void Save()
    {
        GetComponentInParent<ControlZ>()?.SaveScene();
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("toy en collider");
        if((collision.gameObject == starTilemapCollider || iceTilemapCollider) && collision.gameObject.CompareTag("Ice"))
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

        if ((collision.gameObject == starTilemapCollider || windTilemapCollider) && collision.gameObject.CompareTag("Celo"))
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
