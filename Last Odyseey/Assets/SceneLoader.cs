using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneLoader : MonoBehaviour
{
    public Slider ProgressBar;
    public Text progressTxt;

    // Start is called before the first frame update

    void Start(){
        //Debug.Log("Change to " + GameStaticValue.currentScene);
        StartCoroutine(LoadAsynchronously(GameStaticValue.currentScene));
    }

    IEnumerator LoadAsynchronously(int sceneIndex){
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        
        while(!operation.isDone){
           
            float progress = Mathf.Clamp01(operation.progress/.9f);
            
            ProgressBar.value = progress;
            progressTxt.text = progress*100f+ "%";
            yield return  null; 
        }
    }
}
