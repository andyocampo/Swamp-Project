using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLevelEditor : MonoBehaviour
{
    Vector2 cursorPosition;
    Vector3 tilePosition;

    [SerializeField] GameObject[] tile;
    [SerializeField] GameObject cursor;
    [SerializeField] GameObject playerSpawnedTiles;

    private int currentTile;//current tile chosen

    [SerializeField] LayerMask allTilesLayer; //this is a layermask that dictates where the player can place a tile
    [SerializeField] LayerMask placedTilesLayer; //this is a layermask that contatines only player placed tiles

    private void Update()
    {
        CalculatePosition();
        ChangeCursorPosition();
        PlaceTile();
        RemoveTile();
        ChangeTile();
        Restart();
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
                GameObject spawnedTile = Instantiate(tile[currentTile], tilePosition, Quaternion.identity, playerSpawnedTiles.transform);
            }
        }
    }

    //Removes tiles where cursor is located and player has placed a tile
    private void RemoveTile()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D rayHit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, placedTilesLayer);

            if (rayHit.collider != null) //checks whether cursor is over an already placed tile (includes ground, walls, player spawned tiles and foreground items)
            {
                Destroy(rayHit.transform.gameObject);
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

    private void ChangeTile() //changes the tile currently being used
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentTile = 0;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentTile = 1;
        }
    }

    private void Restart() //restarts scene
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
    }
}
