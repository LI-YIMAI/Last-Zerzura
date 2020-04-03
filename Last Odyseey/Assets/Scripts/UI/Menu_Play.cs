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
        GameStaticValue.currentScene = 2;
        SceneManager.LoadScene("Game1");
    }
    public void goToGame2Scene()
    {
        GameStaticValue.currentScene = 2;
        SceneManager.LoadScene("Game2");
    }
    public void goToGame3Scene()
    {
        GameStaticValue.currentScene = 2;
        SceneManager.LoadScene("Game3");
    }
}
