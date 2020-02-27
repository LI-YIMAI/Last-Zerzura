using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField]
    private GameObject[] tilePrefabs;
    
    // Start is called before the first frame update
    private Point blueSpawn, redSpawn;
    [SerializeField]
    private GameObject bluePortalPrefab;
    [SerializeField]
    private GameObject redPortalPrefab;
    [SerializeField]
    private Transform map;
    public float TileSize {
        get { return tilePrefabs[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x;  }
    }
    public Dictionary<Point, TileScript> Tiles { get; set; }
    public Dictionary<char, GameObject> dic_Prefabs { get; set; }
    private char[] Alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
    void Start()
    {
        CreateLevel();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateLevel()
    {
        
        storePrefabs();
        //Debug.Log(Alphabet[0]);
        //Debug.Log(tilePrefabs.Length);
        
        //Debug.Log(test['a']);
        Tiles = new Dictionary<Point, TileScript>();
        string[] mapData = ReadLevelText();

        int mapX = mapData[0].ToCharArray().Length;
        int mapY = mapData.Length;

        Vector3 worldStart = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));
        for (int y = 0; y < mapY; y++)
        {
            char[] newTiles = mapData[y].ToCharArray();
            for (int x = 0; x < mapX; x++)
            {
                PlaceTile(newTiles[x],x, y,worldStart);
                
            }
        }
        SpawnPortals();
    }

    private void PlaceTile(char tileType, int x, int y, Vector3 worldStart) {
        // get tilePrefab index 
        //int tileIndex = int.Parse(tileType);
        bool empty;
        if (tileType=='d'||tileType=='e')
        {
            empty = false;
        }
        else
        {
            empty = true;
        }
        // create TileScript object 
        TileScript newTile = Instantiate(dic_Prefabs[tileType]).GetComponent<TileScript>();

        // call Setup which is kind like constructor, it will save grid postion, world postion,
        // and add it to dictionary<point, tilescript> for each newTile
        newTile.Setup(new Point(x, y), new Vector3(worldStart.x + (TileSize * x), worldStart.y - (TileSize * y), 0), map, empty);
        //Point position = new Point(4, 4);
        //Instantiate(test['f'], new Vector3(worldStart.x + (TileSize * 4), worldStart.y - (TileSize * 4), 0), Quaternion.identity);
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
        Instantiate(bluePortalPrefab, Tiles[blueSpawn].GetComponent<TileScript>().WorldPosition_center, Quaternion.identity);

        redSpawn = new Point(11, 6);
        Instantiate(redPortalPrefab, Tiles[redSpawn].GetComponent<TileScript>().WorldPosition_center, Quaternion.identity);
        

    }

    private void storePrefabs()
    {
        dic_Prefabs = new Dictionary<char, GameObject>();
        // map representation, <key, index> == > <a,0>, <b,1>, ..... index for accesing corresponding tilePrefabs[index]
        for (int index = 0; index < tilePrefabs.Length; index++)
        {
            dic_Prefabs.Add(Alphabet[index], tilePrefabs[index]);
        }
        
    }
}
