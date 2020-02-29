using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    public int level;
    public string map_selection = "";
    private int current_round;
    private int total_round;

    private int total_minion_on_theBoard;

    //gameobject user_select_map;


    //public gameobject_prefb map_prefb;
    //public gameobject_prefb[] towers_prefb;
    //public gameobject_prefb[] minions_prefb;


    // Start is called before the first frame update
    void Start()
    {

        //init a map 
        //map = Instantiate(map_prefb, transform.position, transform.rotation);
        //map will random select a map and create a map in the game

    }

    // Update is called once per frame
    void Update()
    {
        /*if (currentRound / TotalRound) != 1
            for loop init minions;
                minion.game_map = user_select_map.get_map(); //every minion needs to know where to go
                minion.saved_game_manager = this.gameObject; // mionion needs to decrease the total_minion_on_theBoard on the map when they are dead

                //player health is saved in gameStaticValue.cs, so it can be used in different level.

                create a minion
        else
            //nextLevel();
        */
    }

    /*
    void nextLevel(){
        //reload the game, go to next level
        GameStaticValue.currentScene = 2; //GameStaticValue saves all values that pass through different scenes
        SceneManager.LoadScene(1); // Scene 1 is the loading screen
    }
     */
}
