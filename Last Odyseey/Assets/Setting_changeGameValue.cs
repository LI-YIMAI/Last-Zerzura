using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting_changeGameValue : MonoBehaviour
{

    [SerializeField]
    private Scrollbar musicVolSettingScrollBar;

    [SerializeField]
    private Text musicVolText;

    //Init Starting value
    private void Start()
    {
        int changeToInt = (int)(GameStaticValue.currentMusicVol * 100);

        musicVolSettingScrollBar.value = GameStaticValue.currentMusicVol;

        musicVolText.text = changeToInt.ToString() + "%";
    }


    public void changeMusicvol()
    {
        int changeToInt = (int)(musicVolSettingScrollBar.value * 100);
        GameStaticValue.currentMusicVol = musicVolSettingScrollBar.value;

        musicVolText.text = changeToInt.ToString() + "%";

    }


}
