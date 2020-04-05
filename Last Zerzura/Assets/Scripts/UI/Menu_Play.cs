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
    public void goToGame1Scene()
    {
        GameStaticValue.currentScene = 3;
        SceneManager.LoadScene(1);
    }
    public void goToGame2Scene()
    {
        GameStaticValue.currentScene = 4;
       SceneManager.LoadScene(1);
    }
    public void goToGame3Scene()
    {
        GameStaticValue.currentScene = 5;
       SceneManager.LoadScene(1);
    }
}
