using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public SceneLoader sceneLoader; //reference to the scene loader
    //public PlayerScores playerScores; //reference to the player's scores
    public Orb orb; //reference to control orb
    public GameObject magicCircle; //reference to inert magic circle
    public GameObject magicCircleActive; //reference to magic circle to have active
    public ParticleSystem portal; //reference to portal's particles
    public TMP_Text levelText; //text to display on portal

    public bool active; //portal is active or not

    public string[] arrayOfNames; //scene names
    
    // Start is called before the first frame update
    void Start()
    {
        //these rely on objects labeled don't destroy on load
        sceneLoader = GameObject.Find("SceneLoader").GetComponent<SceneLoader>();
        //playerScores = GameObject.Find("PlayerScores").GetComponent<PlayerScores>();

        
        //assign variables
        orb = GameObject.Find("Orb").GetComponent<Orb>();
        magicCircle = GameObject.Find("Magic circle");
        magicCircleActive = GameObject.Find("Magic circle active");
        portal = GameObject.Find("Portal blue").GetComponent<ParticleSystem>();
        levelText = GameObject.Find("LevelText").GetComponent<TMP_Text>();

        //set the magic circle status
        magicCircleActive.SetActive(false);
        portal.gameObject.SetActive(false);
        active = false;

        // Get build scenes
        var sceneNumber = SceneManager.sceneCountInBuildSettings;
        arrayOfNames = new string[sceneNumber];
        for (int i = 0; i < sceneNumber; i++)
        {
            arrayOfNames[i] = System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)){
            GoToLevel();
        }
    }

    public void levelTextUpdate(int index){

        //display the text for the input level
        /*levelText.text = arrayOfNames[index] + "\n" +
                         "Best Rank: " + playerScores.getRank(index) + "\n" +
                         "Best Time: " + playerScores.getTime(index).ToString("mm':'ss'.'ms");
                         */
        
        float levelTime = PlayerPrefs.GetFloat("besttime"+SceneManager.GetSceneByBuildIndex(index).name);

        float minutes = Mathf.FloorToInt(levelTime / 60);
        float seconds = Mathf.FloorToInt(levelTime % 60);
        float hundredths = Mathf.FloorToInt(levelTime-(int)levelTime) * 100;
        //string time = string.Format("{0:00}:{1:00}", minutes, seconds);
        levelText.text = arrayOfNames[index] + "\n" +
                         "Best Rank: " + PlayerPrefs.GetString("rank"+SceneManager.GetSceneByBuildIndex(index).name) + "\n" +
                         "Best Time: " + minutes.ToString("00") + ":" + seconds.ToString("00") + "." + hundredths.ToString("00");
    }

    //function that changes the portal from
    // an inactive state to an active state
    public void Activate(){
        magicCircle.SetActive(false);
        magicCircleActive.SetActive(true);
        portal.gameObject.SetActive(true);
        active = true;
    }

    //function that invokes loadScene
    public void GoToLevel(){
        Debug.Log("GoToLevel invoked");
        Debug.Log("orb.sceneIndex = "+ orb.sceneIndex);
        sceneLoader.loadScene(orb.sceneIndex);
    }

    //when the player touches the trigger of the portal, teleport
    // them to the displayed level
    void OnTriggerEnter(Collider c){
        if (c.gameObject.tag == "Player"){ GoToLevel(); }
    }
}
