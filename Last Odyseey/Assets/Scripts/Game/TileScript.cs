using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileScript : MonoBehaviour
{
    private Color32 fullColor = new Color32(255, 118, 118, 255);
    private Color32 emptyColor = new Color32(96, 255, 90, 255);
    private SpriteRenderer spriteRenderer;
    public SpriteRenderer SpriteRenderer
    {
        get
        {
            return this.spriteRenderer;
        }
    }
    public bool Debugging { get; set; }
    public bool IsEmpty { get; private set; }
    public bool IsWalkable { get; private set; }
    public Point GridPosition{ get; private set; }

    public Vector2 WorldPosition_center
    {
        get
        {
            return new Vector2(transform.position.x + (GetComponent<SpriteRenderer>().bounds.size.x / 2),
                transform.position.y - (GetComponent<SpriteRenderer>().bounds.size.y / 2));
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

  
    public void Setup(Point gridPos, Vector3 worldPos, Transform parent,bool condition, bool walkable)
    {
        IsEmpty = condition;
        IsWalkable = walkable;
        this.GridPosition = gridPos;
        transform.position = worldPos;
        transform.SetParent(parent);
        LevelManager.Instance.Tiles.Add(gridPos, this);
    }

    public void OnMouseOver()
    {
        
        // check if mouse click over the gameobejct and the ClickedBtn is not null, then call place tower
        // Once it click the tower,
        // the tower has attached with the
        // GameManager Script -> pick tower function --> TowerBtn as input(with tower prefab) --> initialize input to the ClickedBtn
       if(!EventSystem.current.IsPointerOverGameObject() && GameManager.Instance.ClickedBtn != null)
        {
            if (IsEmpty && !Debugging)
            {
                ColorTile(emptyColor);
            }
            if (!IsEmpty && !Debugging)
            {
                ColorTile(fullColor);
            }
            else if(Input.GetMouseButtonDown(0))
            {
                PlaceTower();
            }
        }

    }
    public void OnMouseExit()
    {
        if (!Debugging)
        {
            ColorTile(Color.white);
        }
       
    }

    private void PlaceTower()
    {
        // because we have already initialized the ClikcedBtn in OnMouseOver function, we can access the towerprefab


        GameObject tower = (GameObject)Instantiate(GameManager.Instance.ClickedBtn.TowerPrefab, transform.position, Quaternion.identity);
        // change the sorting order by Y-axis 
        tower.GetComponent<SpriteRenderer>().sortingOrder = GridPosition.Y;
        // put tiles clone to map GameObject 
        tower.transform.SetParent(transform);
        Debug.Log(transform.name);

        IsEmpty = false;
        ColorTile(Color.white);
        //Call Buytower and Reset the ClickedBtn as null
        GameManager.Instance.Buytower();

    }

    private void ColorTile(Color newColor)
    {
        
        spriteRenderer.color = newColor;
    }
}
