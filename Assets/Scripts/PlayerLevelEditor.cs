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
            GameObject spawnedTile = Instantiate(tile, tilePosition, Quaternion.identity, playerSpawnedTiles.transform);
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
