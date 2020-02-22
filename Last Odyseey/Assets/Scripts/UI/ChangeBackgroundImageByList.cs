using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeBackgroundImageByList : MonoBehaviour
{
    public GameObject listToLook;
    public Scrollbar bar;
    public GameObject backgroundToChange;

    private int listSize;

    // Start is called before the first frame update
    void Start()
    {
        listSize = listToLook.transform.childCount;

    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {

        if (gameObject.GetComponent<CanvasGroup>().alpha > 0)
        {
            int listOrder = getListOrderByScrollBarValue(bar.value);

            backgroundToChange.GetComponent<Image>().sprite = listToLook.transform.GetChild(listOrder).FindChild("Background").GetComponent<Image>().sprite;
        }
    }

    private int getListOrderByScrollBarValue(float scrollValue)
    {
        for (int count = 1; count <listSize; count++)
        {

            if ((((count - 1) * 1.0f / listSize) <= scrollValue) && ((count * 1.0f / listSize) > scrollValue))
            {
                return count - 1;
            }
        }
        return 0;
    }
}
