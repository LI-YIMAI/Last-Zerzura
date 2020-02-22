using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // An array of tilePrefabs, these are used for creating the tiles in the game
    [SerializeField]
    private GameObject[] tilePrefabs;

    [SerializeField]
    private GameObject bluePortalPrefab;

    [SerializeField]
    private GameObject redPortalPrefab;

    private Point blueSpawn, redSpawn;


    public Dictionary<Point, TileScript> Tiles { get; set; }

    // A property for returning the size of a tile
    public float TileSize {
        get { return tilePrefabs[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x;  }
    }
    // Use this for initialization
    void Start()
    {
        CreateLevel();

    }

    // Update is called once per frame
    void Update()
    {

    }
    // Creates our level
    private void CreateLevel()
    {
        Tiles = new Dictionary<Point, TileScript>();
        //A tmp instantioation of the tile map, we will use a text document to load this later.
        string[] mapData = ReadLevelText();
        //Calculates the x map size
        int mapX = mapData[0].ToCharArray().Length;
        //Calculates the y map size
        int mapY = mapData.Length;
        //Calculates the world start point, this is the top left corner of the screen
        Vector3 worldStart = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));
        for (int y = 0; y < mapY; y++)
        {
            char[] newTiles = mapData[y].ToCharArray();//Gets all the tiles, that we need to place on the current horizontal line
            for (int x = 0; x < mapX; x++)
            {   //Places the tile in the world
                PlaceTile(newTiles[x].ToString(), x, y, worldStart);
            }
        }

        SpawnPortals();

    }
    // <summary>
    // Places a tile in the gameworld
    // </summary>
    // <param name="tileType">The type of tile to palce for example 0</param>
    // <param name="x">x position of the tile</param>
    // <param name="y">y position of the tile</param>
    // <param name="worldStart">The world start position</param>
    private void PlaceTile(string tileType, int x, int y, Vector3 worldStart) {
        //Parses the tiletype to an int, so that we can use it as an indexer when we create a new tile
        int tileIndex = int.Parse(tileType);

        //Creates a new tile and makes a reference to that tile in the newTile variable
        TileScript newTile = Instantiate(tilePrefabs[tileIndex]).GetComponent<TileScript>();

        //Uses the new tile variable to change the position of the tile

        newTile.Setup(new Point(x, y), new Vector3(worldStart.x + (TileSize * x), worldStart.y - (TileSize * y), 0));

        Tiles.Add(new Point(x, y), newTile);
    }
    private string[] ReadLevelText()
    {
        TextAsset bindData = Resources.Load("Level") as TextAsset;

        string data = bindData.text.Replace(Environment.NewLine, string.Empty);

        return data.Split('-');
    }


    private void SpawnPortals()
    {
        blueSpawn = new Point(0, 0);

        Instantiate(bluePortalPrefab, Tiles[blueSpawn].GetComponent<TileScript>().WorldPosition, Quaternion.identity);

        redSpawn = new Point(11, 6);

        Instantiate(redPortalPrefab, Tiles[redSpawn].GetComponent<TileScript>().WorldPosition, Quaternion.identity);
    }
}
