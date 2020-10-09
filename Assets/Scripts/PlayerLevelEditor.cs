using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevelEditor : MonoBehaviour
{
    Vector2 cursorPosition;
    Vector3 tilePosition;

    [SerializeField] GameObject tile;
    [SerializeField] GameObject cursor;
    [SerializeField] GameObject playerSpawnedTiles;

    [SerializeField] LayerMask allTilesLayer; //these are tiles that have already been placed by the player or the tilemap

    private void Update()
    {
        CalculatePosition();
        ChangeCursorPosition();
        PlaceTile();
    }

    //Places tile where cursor is located and stores it in player spawned tiles gameobject
    private void PlaceTile()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D rayHit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, allTilesLayer);

            if(rayHit.collider == null) //checks whether cursor is over an already placed tile (includes ground, walls, player spawned tiles and foreground items)
            {
                GameObject spawnedTile = Instantiate(tile, tilePosition, Quaternion.identity, playerSpawnedTiles.transform);
            }
        }
    }

    // Calcutates where the tile will be placed in a grid
    private void CalculatePosition()
    {
        cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        tilePosition = new Vector3(Mathf.Round(cursorPosition.x), Mathf.Round(cursorPosition.y), 1);
        
    }

    //Moves player cursor around grid to indicate to player where tile will be placed
    private void ChangeCursorPosition()
    {
        cursor.transform.position = tilePosition;
    }
}
