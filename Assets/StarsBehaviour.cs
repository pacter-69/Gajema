using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class StarsBehaviour : MonoBehaviour
{
    public Sprite iceSprite, windSprite, celoSprite;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IceChanger(Tilemap tilemap)
    {
        foreach (Vector3Int pos in tilemap.cellBounds.allPositionsWithin)
        {
            TileBase tile = tilemap.GetTile(pos);
            if (tile is Tile originalTile)
            {
                Tile newTile = ScriptableObject.CreateInstance<Tile>();
                newTile.sprite = iceSprite;
                newTile.color = originalTile.color;
                newTile.colliderType = originalTile.colliderType;
                newTile.transform = originalTile.transform;

                tilemap.SetTile(pos, newTile);
            }
        }
    }
    public void WindChanger(Tilemap tilemap)
    {
        foreach (Vector3Int pos in tilemap.cellBounds.allPositionsWithin)
        {
            TileBase tile = tilemap.GetTile(pos);
            if (tile is Tile originalTile)
            {
                Tile newTile = ScriptableObject.CreateInstance<Tile>();
                newTile.sprite = windSprite;
                newTile.color = originalTile.color;
                newTile.colliderType = originalTile.colliderType;
                newTile.transform = originalTile.transform;

                tilemap.SetTile(pos, newTile);
            }
        }
    }

    public void CeloChanger(Tilemap tilemap)
    {
        foreach (Vector3Int pos in tilemap.cellBounds.allPositionsWithin)
        {
            TileBase tile = tilemap.GetTile(pos);
            if (tile is Tile originalTile)
            {
                Tile newTile = ScriptableObject.CreateInstance<Tile>();
                newTile.sprite = celoSprite;
                newTile.color = originalTile.color;
                newTile.colliderType = originalTile.colliderType;
                newTile.transform = originalTile.transform;

                tilemap.SetTile(pos, newTile);
            }
        }

        //tilemap.gameObject.GetComponent
    }
}
