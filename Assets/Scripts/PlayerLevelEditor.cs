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

    public int[] tilesRemaining; //how many tiles remain for each tile/tool
    private int currentTile; //current tile chosen

        SpriteRenderer currentTileSprite;

    [SerializeField] LayerMask allTilesLayer; //this is a layermask that dictates where the player can place a tile

    private void Update()
    {
        CalculatePosition();
        ChangeCursorPosition();
        ChangeTileSprite();
        PlaceTile();
        RemoveTile();
    }

    /// <summary>
    /// Places tile where cursor is located and stores it in player spawned tiles gameobject
    /// </summary>
    private void PlaceTile()
    {
        Color cursorColor = cursor.GetComponent<SpriteRenderer>().color; //cursor's opacity
        Color tileColor; //cursor's opacity
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D rayHit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, allTilesLayer);

        if (rayHit.collider != null)//checks whether cursor is over an already placed tile (includes ground, walls, player spawned tiles and foreground items)
        {
            tileColor = new Color(1, 0, 0, 0.5f);
            cursorColor.a = 0.5f;
            cursor.GetComponent<SpriteRenderer>().color = cursorColor;
            currentTileSprite.GetComponent<SpriteRenderer>().color = tileColor;
        }
        else //checks whether cursor is in available spot
        {
            tileColor = new Color(1, 1, 1, 1);
            cursorColor.a = 1;
            cursor.GetComponent<SpriteRenderer>().color = cursorColor;
            currentTileSprite.GetComponent<SpriteRenderer>().color = tileColor;

            if (tilesRemaining[currentTile] > 0)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    tilesRemaining[currentTile]--;
                    Instantiate(tile[currentTile], tilePosition, Quaternion.identity, playerSpawnedTiles.transform);
                }
            }
            else if (tilesRemaining[currentTile] < 0)
            {
                tileColor.a = 0;
                currentTileSprite.GetComponent<SpriteRenderer>().color = tileColor;
            }
        }
    }

    //(MAY BE REMOVED TO ADD DIFFICULTY, PLAYER WOULD HAVE TO RESTART IF THEY MAKE MISTAKE)
    /// <summary>
    /// Removes tiles where cursor is located and player has placed a tile
    /// </summary>
    private void RemoveTile()
    {
        string nameOfCurrentTile = $"{tile[currentTile].name}(Clone)";
        Sprite deleteTile = this.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D rayHit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);

        if ((rayHit.collider != null) && rayHit.transform.CompareTag("PlacedTiles")) //checks whether cursor is over an already placed tile
        { 
            currentTileSprite.GetComponent<SpriteRenderer>().sprite = deleteTile;

            if (Input.GetMouseButtonDown(1))
            {
                if(rayHit.transform.name != nameOfCurrentTile)
                {
                    switch(currentTile)
                    {
                        case 0:
                            tilesRemaining[1]++;
                            break;
                        case 1:
                            tilesRemaining[0]++;
                            break;
                    }
                }
                else
                {
                    tilesRemaining[currentTile]++;
                }
                Destroy(rayHit.transform.gameObject);
            }
        }

    }

    /// <summary>
    /// Calcutates where the tile will be placed in a grid
    /// </summary>
    private void CalculatePosition()
    {
        cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        tilePosition = new Vector3(Mathf.Round(cursorPosition.x), Mathf.Round(cursorPosition.y), 1);
        
    }

    /// <summary>
    /// Moves player cursor around grid to indicate to player where tile will be placed
    /// </summary>
    private void ChangeCursorPosition()
    {
        cursor.transform.position = tilePosition;
    }

    private void ChangeTileSprite()
    {
        currentTileSprite = cursor.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        currentTileSprite.sprite = tile[currentTile].GetComponent<SpriteRenderer>().sprite;
    }


    /// <summary>
    /// Takes event from UI Handler script to change chosen tile
    /// </summary>
    /// <param name="TileNumber">Index for tiles array</param>
    private void OnCurrentToolTriggered(int TileNumber) 
    {
        currentTile = TileNumber;
    }

    private void OnEnable()
    {
        UIHandler.CurrentToolTriggered += OnCurrentToolTriggered;
    }

    private void OnDisable()
    {
        UIHandler.CurrentToolTriggered -= OnCurrentToolTriggered;
    }
}
