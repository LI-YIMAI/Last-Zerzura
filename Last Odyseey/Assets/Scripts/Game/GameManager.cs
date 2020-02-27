using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    
    

    public TowerBtn ClickedBtn { get; set; }

    // property
    public int Gold
    {
        get
        {
            return gold;
        }
        set
        {
            // update gold value for script and ui text 
            this.gold = value;
            this.Goldtext.text = value.ToString() + " <color=lime>$</color>"; 
        }
    }

    private int gold;

    [SerializeField]
    private Text Goldtext;
    // Start is called before the first frame update
    void Start()
    {
        // set the Gold to 5 and assign it to Goldtext 
        Gold = 10;
    }

    // Update is called once per frame
    void Update()
    {
        HandleEscape();
    }
    public void PickTower(TowerBtn towerBtn)
    {
        this.ClickedBtn = towerBtn;
        // check if have enough gold to buy a tower 
        if (Gold >= towerBtn.Price)
        {
            Hover.Instance.Activate(towerBtn.Sprite);  
        }
        if (Gold == 0 || Gold < ClickedBtn.Price)
        { 
            Hover.Instance.Deactivate();
        }
    }

    public void Buytower()
    {
        // reduce the gold when placing the tower
        
        if (Gold >= ClickedBtn.Price)
        {
            Gold -= ClickedBtn.Price;
            Hover.Instance.Deactivate();
            shandow_tower_check();
        }
        else
        {
            Hover.Instance.Deactivate();
            shandow_tower_check();
        }   
    }

    public void HandleEscape()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Hover.Instance.Deactivate();

        }
    }

    public void shandow_tower_check()
    {
        //Get gameobject for all button save as TowerBtn
        //Check if the btn.price is greater than the current gold we have
        //Yes -> shandow button
        //Else no
        TowerBtn Storm = GameObject.FindGameObjectWithTag("StormBtn").GetComponent<TowerBtn>();
        TowerBtn Frozen = GameObject.FindGameObjectWithTag("FrozBtn").GetComponent<TowerBtn>();
        TowerBtn Poision = GameObject.FindGameObjectWithTag("PoisionBtn").GetComponent<TowerBtn>();
        TowerBtn Fire = GameObject.FindGameObjectWithTag("FireBtn").GetComponent<TowerBtn>();
        if(Gold < 5)
        {
            Poision.GetComponent<Image>().color = new Color32(185, 155, 155, 255);
        }
        if (Gold < 3)
        {
            Frozen.GetComponent<Image>().color = new Color32(185, 155, 155, 255);
            Fire.GetComponent<Image>().color = new Color32(185, 155, 155, 255);
        }
        if (Gold < 2)
        {
            Storm.GetComponent<Image>().color = new Color32(185, 155, 155, 255);
        }
        
    }

   
}