using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Point Gridposition { get; private set; }

    public TileScript TileRef { get; private set; }

    public Vector2 WorldPosition { get; set; }


    public Node Parent { get; private set;}

    public int G { get; private set; }
    public int H { get; private set; }
    public int F { get; private set; }
    // constructor, once we have node, we need to know it's combined tile as reference 
    public Node(TileScript tileRef)
    {
        this.TileRef = tileRef;
        this.Gridposition = tileRef.GridPosition;
        this.WorldPosition = tileRef.WorldPosition_center;
    }

    public void CalcValues(Node parent, Node goal, int gCost)
    {
        this.Parent = parent;
        this.G = parent.G+gCost;
        this.H = ((Math.Abs(Gridposition.X - goal.Gridposition.X))+ (Math.Abs(goal.Gridposition.Y - Gridposition.Y))) * 10;
        this.F = G + H;
    }

}
