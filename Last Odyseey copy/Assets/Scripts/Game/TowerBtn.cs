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
                tooltip = string.Format("<color=#ffa500ff><size=20><b>Fire</b></size></color>");
                break;
            case "Ashe":
                Tower
                tooltip = string.Format("<color=#00ffffff><size=20><b>Frozen</b></size></color>\nDamage: {0} \nProc: {1}% \nDebuff duration: {2}sec \nSlowing factor: {3}% \n Has a chance to slow down the targets",);
                break;

        }
        GameManager.Instance.setToolText(type);
        GameManager.Instance.showStats();
    }
}

