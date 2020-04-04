using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerBtn : MonoBehaviour
{
    [SerializeField]
    private GameObject towerPrefab;

    [SerializeField]
    private Sprite sprite;

    [SerializeField]
    private int price;
    [SerializeField]
    private Text pricetext;

    public GameObject TowerPrefab
    {
        get
        {
            return towerPrefab;
        }
    }

    public Sprite Sprite
    {
        get
        {
            return sprite;
        }
    }

    public int Price
    {
        get
        {
            return price;
        }
    }

    public void Start()
    {
        pricetext.text = price + "$";
    }

    public void ShowInfo(string type)
    {
        string tooltip = string.Empty;

        switch (type)
        {
            case "Brand":
                tooltip = string.Format("<color=#ffa500ff><size=20><b>Brand:FireTower</b></size></color>\nDamage: 7\nDebuff: slowing speed 3 sec\nAttack Speed: 5");
                if (GameManager.Instance.getGold() >= 2)
                {
                    GameManager.Instance.setToolText(tooltip);
                    GameManager.Instance.showStats();
                }
                break;
            case "Ashe":
                tooltip = string.Format("<color=#00ffffff><size=20><b>Ashe:IceTower</b></size></color>\nDamage: 7\nDebuff: slowing speed 3 sec\nAttack Speed: 5");
                if (GameManager.Instance.getGold() >= 3)
                {
                    GameManager.Instance.setToolText(tooltip);
                    GameManager.Instance.showStats();
                }
                break;
        }
        
    }
}

