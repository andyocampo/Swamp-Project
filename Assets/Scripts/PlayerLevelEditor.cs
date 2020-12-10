using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLevelEditor : MonoBehaviour
{
    Vector2 cursorPosition;
    Vector3 tilePosition;

    private bool deleteModeOn;
    public int[] tilesRemaining; //how many tiles remain for each tile/tool
    [SerializeField]private int currentTile; //current tile chosen

    [SerializeField] GameObject[] tile;
    [SerializeField] GameObject cursor = null;
    [SerializeField] GameObject playerSpawnedTiles;

    SpriteRenderer currentTileSpriteRenderer;
    SpriteRenderer cursorSpriteRenderer;
    SpriteRenderer invalidToolSpriteRenderer;
    SpriteRenderer deleteTileSpriteRenderer;
    Sprite deleteClosedSprite;
    [SerializeField] Sprite deleteOpenSprite;
    private static Color invalidColor = new Color(1, 0, 0, .5f);

    [SerializeField] LayerMask allTilesLayer; //this is a layermask that dictates where the player can place a tile

    private void Awake()
    {
        deleteModeOn = false;
        invalidToolSpriteRenderer = cursor.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();//sprite that appears if the player is out of toolsor 
        currentTileSpriteRenderer = cursor.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>();//gets the sprite renderer of the current tool
        deleteTileSpriteRenderer = cursor.transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>();//gets the sprite renderer of the current tool
        cursorSpriteRenderer = cursor.GetComponent<SpriteRenderer>();//cursor's sprite renderer
        deleteClosedSprite = deleteTileSpriteRenderer.sprite;
    }

    private void Update()
    {
        CalculatePosition();
        ChangeCursorPosition();
        ChangeTileSprite();
        PlaceTile();
        RemoveTile();
        ToggleDeleteMode(UIHandler.deleteOn);
    }

    /// <summary>
    /// Places tile where cursor is located and stores it in player spawned tiles gameobject
    /// </summary>
    private void PlaceTile()
    {

        if(deleteModeOn == false)
        {
            deleteTileSpriteRenderer.color = Color.clear;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D rayHit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, allTilesLayer);


            if (rayHit.collider != null)//checks whether cursor is over an already placed tile (includes ground, walls, player spawned tiles and foreground items)
            {
                SetInvalidColors();
            }
            else //checks whether cursor is in available spot
            {
                cursorSpriteRenderer.color = Color.white;
                currentTileSpriteRenderer.color = Color.white;
                invalidToolSpriteRenderer.color = Color.clear;

                if (tilesRemaining[currentTile] > 0)
                {
                    if (Input.GetMouseButtonDown(0))//places tile
                    {
                        tilesRemaining[currentTile]--;
                        Instantiate(tile[currentTile], tilePosition, Quaternion.identity, playerSpawnedTiles.transform);
                    }
                }
                else if (tilesRemaining[currentTile] <= 0)//when the player is out of tools.
                {
                    SetInvalidColors();
                }
            }
        }
        else { return; }     
    }

    //(MAY BE REMOVED TO ADD DIFFICULTY, PLAYER WOULD HAVE TO RESTART IF THEY MAKE MISTAKE)
    /// <summary>
    /// Removes tiles where cursor is located and player has placed a tile
    /// </summary>
    private void RemoveTile()
    {
        if (deleteModeOn == true)
        {
            invalidToolSpriteRenderer.color = Color.clear;
            currentTileSpriteRenderer.color = Color.clear;
            cursorSpriteRenderer.color = Color.white;
            deleteTileSpriteRenderer.color = Color.white;
            string nameOfCurrentTile = $"{tile[currentTile].name}(Clone)";
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D rayHit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);

            if ((rayHit.collider != null) && rayHit.transform.CompareTag("PlacedTiles")) //checks whether cursor is over an already placed tile
            {
                deleteTileSpriteRenderer.sprite = deleteOpenSprite; //sets to open trash can sprite
                if (Input.GetMouseButtonDown(0))
                {
                    if (rayHit.transform.name != nameOfCurrentTile)
                    {
                        switch (currentTile)
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
            else { deleteTileSpriteRenderer.sprite = deleteClosedSprite; } //sets to closed trash can sprite
        }
        else { return; }

    }
    private void SetInvalidColors()
    {
        cursorSpriteRenderer.color = invalidColor;
        currentTileSpriteRenderer.color = invalidColor;
        invalidToolSpriteRenderer.color = Color.white;
    }

    //turns on delete mode
    private void ToggleDeleteMode(bool mode)
    {
        deleteModeOn = mode;
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
        currentTileSpriteRenderer.sprite = tile[currentTile].GetComponent<SpriteRenderer>().sprite;
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
