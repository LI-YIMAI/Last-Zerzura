using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// make it static class
public static class AStar
{
    private static Dictionary<Point, Node> nodes;

    // store each tilescript object by calling node constructor and save its self as value. The key will be the poinsiiton of tile
    private static void CreateNodes()
    {
        nodes = new Dictionary<Point, Node>();
        foreach (TileScript tile in LevelManager.Instance.Tiles.Values)
        {
            // add each node object into dictionry, key is the postion we get from Tiles dictionary, we call Node() constructor to
            // build reference relationship with each tilescript.
            Node node = new Node(tile);
            Debug.Log(node);
            nodes.Add(tile.GridPosition, node); 
        }
    }

    // -----> just for Debug This function will be called from AstarDebugging.update()
    // -----> For Real Game: called from Level Manager
    public static Stack<Node> GetPath(Point start, Point goal)
    {
        if (nodes == null) //If we don't have nodes dictionary, then we need to create them
        {
            CreateNodes();
        }

        //Creates an open list to be used with the A* algorithm
        HashSet<Node> openList = new HashSet<Node>();

        //Creates an closed list to be used with the A* algorithm
        HashSet<Node> closedList = new HashSet<Node>();

        //1, 2 ,3
        Stack<Node> finalPath = new Stack<Node>();
        //Finds the start node and creates a reference to it called current node
        Node currentNode = nodes[start];

        //1. Adds the start node to the OpenList
        openList.Add(currentNode); // the current node will be the parent of their neighbour
        while (openList.Count > 0) // step 10
        {
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    Point neighbourPos = new Point(currentNode.Gridposition.X - x, currentNode.Gridposition.Y - y);

                    if (LevelManager.Instance.InBounds(neighbourPos) && LevelManager.Instance.Tiles[neighbourPos].IsWalkable && neighbourPos != currentNode.Gridposition)
                    {
                        //[14][ 10][14]
                        //[10][  s][10]
                        //[14][ 10][14]
                        int gCost = 0;
                        if (Mathf.Abs(x - y) == 1)
                        {
                            gCost = 10; // accoring to the x and y, update the gCost value
                        }
                        else
                        {
                            if (!connectedDiagonally(currentNode, nodes[neighbourPos]))
                            {
                                continue; // it jump to the next loop and past the code shown below
                            }
                            gCost = 14; // 14 if diagonal
                        }
                        Node neighbour = nodes[neighbourPos];
                        if (openList.Contains(neighbour))
                        {
                            if (currentNode.G + gCost < neighbour.G)
                            {
                                neighbour.CalcValues(currentNode, nodes[goal], gCost); // 9.4
                            }
                        }
                        else if (!closedList.Contains(neighbour)) // step 9.1
                        {
                            // is not in openlist and not in the closed list, it means it is new neighbour
                            openList.Add(neighbour); // step 9.2
                            neighbour.CalcValues(currentNode, nodes[goal], gCost); // step9.3
                        }
                        //if (!openList.Contains(neighbour))
                        //{
                        //    // add the neighbour node to openlist 
                        //    openList.Add(neighbour);
                        //}
                        //// call CalcValues function in Node class and save it's parent node and update it's gCost, hCost, and fCost
                        //neighbour.CalcValues(currentNode, nodes[goal], gCost);
                    }
                }
            }

            // 5.& 8 Move the current node from openList to closedList
            openList.Remove(currentNode);
            closedList.Add(currentNode);


            if (openList.Count > 0)
            {
                //7. sorts the list by F value and selects the first value
                currentNode = openList.OrderBy(n => n.F).First();
            }
            if (currentNode == nodes[goal])
            {
                while (currentNode.Gridposition != start)
                {
                    finalPath.Push(currentNode);
                    currentNode = currentNode.Parent;
                }
                break;
            }
        }
        return finalPath;
        //****THIS IS ONLY FOR DEBUGGING NEEDS TO BE REMOVED LATER!*****
        //GameObject.Find("Debugger").GetComponent<AstarDebugging>().DebugPath(openList, closedList, finalPath);
    }
    // check corner cutting 
    private static bool connectedDiagonally(Node currentNode, Node neighbor)
    {
        Point direction = neighbor.Gridposition - currentNode.Gridposition;
        Point first = new Point(currentNode.Gridposition.X + direction.X, currentNode.Gridposition.Y);
        Point second = new Point(currentNode.Gridposition.X, currentNode.Gridposition.Y + direction.Y);
        if (LevelManager.Instance.InBounds(first)&&LevelManager.Instance.Tiles[first].IsWalkable)
        {
            return false;
        }
        if (LevelManager.Instance.InBounds(second) && LevelManager.Instance.Tiles[second].IsWalkable)
        {
            return false;
        }

        return true;
    }
}
