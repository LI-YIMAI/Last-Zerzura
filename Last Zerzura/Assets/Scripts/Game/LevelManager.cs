using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public Portal BluePortal { get; set; }
    string sceneName;
    [SerializeField]
    private Transform map;

    public float TileSize
    {
        get { return tilePrefabs[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x; }
    }

    private Point Mapsize;

    private Stack<Node> path;

    public Stack<Node> Path
    {
        get
        {
            if (path == null)
            {
                GeneratePath();
            }
            return new Stack<Node>(new Stack<Node>(path));
        }
    }
    public Point BlueSpawn
    {
        get
        {
            return blueSpawn;
        }
    }
    // store each tiles
    public Dictionary<Point, TileScript> Tiles { get; set; }

    // store each Prefabs 
    public Dictionary<char, GameObject> dic_Prefabs { get; set; }

    //representation for each prefab
    private char[] Alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();


    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        // Retrieve the name of this scene.
        sceneName = currentScene.name;


        CreateLevel();

    }

    // Update is called once per frame, thus, the path will be updated per frame
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
        Mapsize = new Point(mapX, mapY);

        Vector3 worldStart = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));
        for (int y = 0; y < mapY; y++)
        {
            char[] newTiles = mapData[y].ToCharArray();
            for (int x = 0; x < mapX; x++)
            {
                PlaceTile(newTiles[x], x, y, worldStart);

            }
        }
        SpawnPortals();
    }

    private void PlaceTile(char tileType, int x, int y, Vector3 worldStart)
    {
        // get tilePrefab index 
        //int tileIndex = int.Parse(tileType);
        bool empty;
        bool walkable;
        //e, f prefab is monster path
        if (tileType == 'e')
        {
            empty = false;
            walkable = true;
        }
        else
        {
            // tile for tower 
            if (tileType == 'b')
            {
                empty = true;
                walkable = false;
            }
            else
            {
                empty = false;
                walkable = false;
            }

        }
        // create TileScript object

        TileScript newTile = Instantiate(dic_Prefabs[tileType]).GetComponent<TileScript>();
        if (tileType == 'a')
        {
            //newTile.GetComponentInChildren<SpriteRenderer>().sortingOrder = y;
            GameObject child = newTile.transform.GetChild(0).gameObject;
            child.GetComponent<SpriteRenderer>().sortingOrder = y;
        }

        // call Setup which is kind like constructor, it will save grid postion, world postion,
        // and add it to dictionary<point, tilescript> for each newTile
        newTile.Setup(new Point(x, y), new Vector3(worldStart.x + (TileSize * x), worldStart.y - (TileSize * y), 0), map, empty, walkable);
        //Point position = new Point(4, 4);
        //Instantiate(test['f'], new Vector3(worldStart.x + (TileSize * 4), worldStart.y - (TileSize * 4), 0), Quaternion.identity);
    }
    private string[] ReadLevelText()
    {
        TextAsset bindData;
        //scene: Game1-map1
        if (sceneName == "Game1")
        {
            bindData = Resources.Load("map1") as TextAsset;
        }
        //scene: Game2-map2
        else if (sceneName == "Game2")
        {
            bindData = Resources.Load("map2") as TextAsset;
        }
        //scene: Game3-map3
        else if (sceneName == "Game3")
        {
            bindData = Resources.Load("map3") as TextAsset;
        }
        //scene: Game-Default_map
        else
        {
            bindData = Resources.Load("Level") as TextAsset;
        }
        string data = bindData.text.Replace(Environment.NewLine, string.Empty);

        return data.Split('-');
    }

    private void SpawnPortals()
    {
        blueSpawn = new Point(0, 0);
        // get the prefab protal reference 
        GameObject tmp = (GameObject)Instantiate(bluePortalPrefab, Tiles[blueSpawn].GetComponent<TileScript>().WorldPosition_center, Quaternion.identity);
        // get its attached component Portal script 
        BluePortal = tmp.GetComponent<Portal>();

        BluePortal.name = "BluePortal";


        redSpawn = new Point(11, 6);
        //redSpawn = new Point(1, 1);

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

    public bool InBounds(Point position)
    {
        return position.X >= 0 && position.Y >= 0; // && position.X < Mapsize.X && position.Y < Mapsize.Y;
    }

    // Called by LevelManager and call the Astar.GetPath(start_point, goal_point)
    public void GeneratePath()
    {
        // call GetPath and pass the start point and end point, and get the returned path back which is type of stack
        path = AStar.GetPath(blueSpawn, redSpawn);
    }

}
