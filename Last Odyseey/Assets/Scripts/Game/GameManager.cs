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
    // property
    public bool WaveActive
    {
        get { return Activemonster.Count > 0; }
         
    }

    private int gold;
    private int wave = 0;
    [SerializeField]
    private Text WaveTxt; // this will have a referece for the actual waveTxt
    [SerializeField]
    private GameObject wavebtn;
    [SerializeField]
    private Text Goldtext;
    private List<Monster> Activemonster = new List<Monster >();

    public ObjectPool Pool { get; set; }

    private void Awake()
    {
        Pool = GetComponent<ObjectPool>();
    }
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
        
        // check if have enough gold to buy a tower 
        if (Gold >= towerBtn.Price && !WaveActive)
        {
            this.ClickedBtn = towerBtn;
            Hover.Instance.Activate(towerBtn.Sprite);  
        }
        if (Gold == 0 || Gold < towerBtn.Price)
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

    public void StartWave()
    {
        wave++;
        WaveTxt.text = string.Format("Level: <color=lime>{0}</color>",wave);
        // user clikc Next Wave button and activate this function to call SpawnWave
        StartCoroutine(SpawnWave());
        wavebtn.SetActive(false);
    }

    private IEnumerator SpawnWave()
    {

        // call GeneratePath function in LevelManger and assigned the path to the path property in LevelManager
        LevelManager.Instance.GeneratePath();
        // spawnwave based on wave number or level number 
        for (int i = 0; i < wave; i++)
        {           
            // random pick a index for MonsterPrefabs
            // need to finish all animation for other monster, so far, we just use index 3 monster 
            int monsterIndex = 3; //Random.Range(0, 4);
            string type = string.Empty;
            // according to the index to save the type of monster 
            switch (monsterIndex)
            {
                case 0:
                    type = "BlueMonster";
                    break;
                case 1:
                    type = "GreenMonster";
                    break;
                case 2:
                    type = "PurpleMonster";
                    //type = "aba";
                    break;
                case 3:
                    type = "RedMonster";
                    //type = "aba";
                    break;
            }
            // use the ObjectPool object to find the required type object and get its component Monster
            Monster monster = Pool.GetObject(type).GetComponent<Monster>();
            // call the spawn function in Monster script and set the Monster transform postion
            monster.spawn();
            // add active monster to the list 
            Activemonster.Add(monster);

            yield return new WaitForSeconds(2.5f);
        }
        
    }
    public void Removemonster(Monster monster)
    {
        Activemonster.Remove(monster);
        // when there is no active monster, we need show the wave button 
        if (!WaveActive)
        {
            wavebtn.SetActive(true);
        }
    }

}