using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Play : MonoBehaviour
{


    public void goToPlayScene(){
        
        GameStaticValue.currentScene = 2;
        SceneManager.LoadScene(1);

    } 
}
