using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class StarsBehaviour : MonoBehaviour
{
    public Sprite iceSprite, windSprite, celoSprite, normalFloor1, starFloor2, starFloor3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

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
                tilemap.gameObject.tag = "Ice";
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
                tilemap.gameObject.tag = "Wind";
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
                tilemap.gameObject.tag = "Celo";
            }
        }
    }

    public void Floor1Changer(Tilemap tilemap)
    {
        foreach (Vector3Int pos in tilemap.cellBounds.allPositionsWithin)
        {
            TileBase tile = tilemap.GetTile(pos);
            if (tile is Tile originalTile)
            {
                Tile newTile = ScriptableObject.CreateInstance<Tile>();
                newTile.sprite = normalFloor1;
                newTile.color = originalTile.color;
                newTile.colliderType = originalTile.colliderType;
                newTile.transform = originalTile.transform;

                tilemap.SetTile(pos, newTile);
                tilemap.gameObject.tag = "Floor";
            }
        }
    }

    public void Floor2Changer(Tilemap tilemap)
    {
        foreach (Vector3Int pos in tilemap.cellBounds.allPositionsWithin)
        {
            TileBase tile = tilemap.GetTile(pos);
            if (tile is Tile originalTile)
            {
                Tile newTile = ScriptableObject.CreateInstance<Tile>();
                newTile.sprite = starFloor2;
                newTile.color = originalTile.color;
                newTile.colliderType = originalTile.colliderType;
                newTile.transform = originalTile.transform;

                tilemap.SetTile(pos, newTile);
                tilemap.gameObject.tag = "Floor";
            }
        }
    }
    public void Floor3Changer(Tilemap tilemap)
    {
        foreach (Vector3Int pos in tilemap.cellBounds.allPositionsWithin)
        {
            TileBase tile = tilemap.GetTile(pos);
            if (tile is Tile originalTile)
            {
                Tile newTile = ScriptableObject.CreateInstance<Tile>();
                newTile.sprite = starFloor3;
                newTile.color = originalTile.color;
                newTile.colliderType = originalTile.colliderType;
                newTile.transform = originalTile.transform;

                tilemap.SetTile(pos, newTile);
                tilemap.gameObject.tag = "Floor";
            }
        }
    }
}
