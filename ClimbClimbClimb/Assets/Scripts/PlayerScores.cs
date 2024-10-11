/* PlayerScores.cs
 * Author: Wesley Crowe
 * A script to keep track of the player's ranks and times for levels
 * Adjusted: to setup PlayerPrefs
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScores : MonoBehaviour
{

    public char[] ranks;
    public TimeSpan[] times;

    // Start is called before the first frame update
    void Start()
    {
        //these scores need to persist through each level
        //DontDestroyOnLoad(gameObject);

        /*
        //make the ranks array have the same size as the number of scenes
        ranks = new char[SceneManager.sceneCountInBuildSettings];
        for (int i = 0; i < ranks.Length; i++){ ranks[i] = '-'; }

        //make the times array have the same size as the number of scenes
        times = new TimeSpan[SceneManager.sceneCountInBuildSettings];
        for (int j = 0; j < times.Length; j++){ times[j] = TimeSpan.FromSeconds(0); }
        */

        //create playerprefs keys for time and rank for each level
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++){
            PlayerPrefs.SetFloat("besttime"+SceneManager.GetSceneByBuildIndex(i).name, 0);//5999);
            PlayerPrefs.SetString("rank"+SceneManager.GetSceneByBuildIndex(i).name, "-");
        }

        PlayerPrefs.Save();

        //EXPERIMENTING WITH DISPLAYED TIME
        //times[2] = new TimeSpan(344455);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*
    //returns the player's rank score for the input index
    public char getRank(int index){

        Debug.Log("Getting Rank for Scene "+ index);
        Debug.Log("Rank: "+ ranks[index]);

        return ranks[index];
    }

    //returns the player's time score for the input index
    public TimeSpan getTime(int index){        
        return times[index];
    }

    //updates the player's rank score for the input index
    public void updateRank(int index, char rank){
        ranks[index] = rank;
    }

    //updates the player's time score for the input index
    //Note: calculate the imput time float on a per-level basis, subtracting start Time.time from end Time.time
    public void updateTime(int index, float time){
        times[index] = TimeSpan.FromSeconds(time);
    }
    */
}
