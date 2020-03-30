using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstarDebugging : MonoBehaviour 
{
    //[SerializeField]
    private TileScript start, goal;

    [SerializeField]
    private Sprite blankTile;

    [SerializeField]
    private GameObject arrowPrefab;

    [SerializeField]
    private GameObject debugTilePrefab;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    //void Update()
    //{
    //    // Clicktile to set start point and goal point with different color
    //    ClickTile();
    //    // once click space btn, call Astar.GetPath
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        // call GetPath function in AStar when type space
    //        AStar.GetPath(start.GridPosition, goal.GridPosition);

    //    }
    //}
    // ClickTile function set start point and goal point
    private void ClickTile()
    {

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                TileScript tmp = hit.collider.GetComponent<TileScript>();

                if (tmp != null)
                {
                    if (start == null)
                    {
                        // assign the start point to the tilescript 
                        start = tmp;
                        // Create debug tile prefab as color ...
                        GreateDebugTile(start.WorldPosition_center, new Color32(255, 135, 0, 255));
                      
                    }
                    else if (goal == null)
                    {
                        // asign the goal point to the tilescript
                        goal = tmp;
                        // Create debugTile Prefab as color ...
                        GreateDebugTile(goal.WorldPosition_center, new Color32(255, 0, 0, 255));
                    }
                }
            }
        }
    }
    // DebugPath been called once the GetPath function finished in Astar class
    public void DebugPath(HashSet<Node> openList, HashSet<Node> closedList, Stack<Node> path)
    {
        foreach (Node item in openList)
        {
            Debug.Log("check openList");
            if (item.TileRef != start && item.TileRef != goal)
            {
                // call CreateDebugTile and pass the position, corlor and node object
                GreateDebugTile(item.TileRef.WorldPosition_center, Color.cyan,item);
            }
            // make arrow to point to the parent node
            PointToParent(item, item.TileRef.WorldPosition_center);
        }
        foreach (Node item in closedList)
        {
            Debug.Log("check closedList");
            // show cost value if the node is not start and goal
            if (item.TileRef != start && item.TileRef != goal && !path.Contains(item))
            {
                GreateDebugTile(item.TileRef.WorldPosition_center, Color.blue,item);
            }
            PointToParent(item, item.TileRef.WorldPosition_center);
        }
        foreach (Node node in path)
        {
            if(node.TileRef != start && node.TileRef != goal)
            {
                GreateDebugTile(node.TileRef.WorldPosition_center, Color.green, node);
            }
        }
    }

    public void PointToParent(Node node, Vector2 position)
    {
        if (node.Parent != null)
        {
            // instantiate arrow on it 
            GameObject arrow = (GameObject)Instantiate(arrowPrefab, position, Quaternion.identity);
            // set arrow sorting order 
            arrow.GetComponent<SpriteRenderer>().sortingOrder = 3;
            //Right
            if ((node.Gridposition.X < node.Parent.Gridposition.X) && (node.Gridposition.Y == node.Parent.Gridposition.Y))
            {
                arrow.transform.eulerAngles = new Vector3(0, 0, 0);
            }
            //Top Right
            else if ((node.Gridposition.X < node.Parent.Gridposition.X) && (node.Gridposition.Y > node.Parent.Gridposition.Y))
            {
                arrow.transform.eulerAngles = new Vector3(0, 0, 45);
            }
            //UP
            else if ((node.Gridposition.X == node.Parent.Gridposition.X) && (node.Gridposition.Y > node.Parent.Gridposition.Y))
            {
                arrow.transform.eulerAngles = new Vector3(0, 0, 90);
            }
            //TOP LEFT
            else if ((node.Gridposition.X > node.Parent.Gridposition.X) && (node.Gridposition.Y > node.Parent.Gridposition.Y))
            {
                arrow.transform.eulerAngles = new Vector3(0, 0, 135);
            }
            //LEFT
            else if ((node.Gridposition.X > node.Parent.Gridposition.X) && (node.Gridposition.Y == node.Parent.Gridposition.Y))
            {
                arrow.transform.eulerAngles = new Vector3(0, 0, 180);
            }
            //Bottom left
            else if ((node.Gridposition.X > node.Parent.Gridposition.X) && (node.Gridposition.Y < node.Parent.Gridposition.Y))
            {
                arrow.transform.eulerAngles = new Vector3(0, 0, 225);
            }
            //Bottom
            else if ((node.Gridposition.X == node.Parent.Gridposition.X) && (node.Gridposition.Y < node.Parent.Gridposition.Y))
            {
                arrow.transform.eulerAngles = new Vector3(0, 0, 270);
            }
            //Bottom right
            else if ((node.Gridposition.X < node.Parent.Gridposition.X) && (node.Gridposition.Y < node.Parent.Gridposition.Y))
            {
                arrow.transform.eulerAngles = new Vector3(0, 0, 315);
            }
        }
        
    }

    private void GreateDebugTile(Vector3 worldPos, Color32 color, Node node = null)
    {
        GameObject debugTile = (GameObject)Instantiate(debugTilePrefab, worldPos, Quaternion.identity);
        if(node != null)
        {
            DebugTile temp = debugTile.GetComponent<DebugTile>();
            temp.G.text += node.G;
            temp.H.text += node.H;
            temp.F.text += node.F;
        }
        debugTile.GetComponent<SpriteRenderer>().color = color;
    }
}
