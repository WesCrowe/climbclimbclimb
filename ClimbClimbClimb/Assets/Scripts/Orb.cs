/* Orb.cs
 * Author: Wesley Crowe
 * A script that makes an orb object into an interactible level selector
 */
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Orb : MonoBehaviour
{
    public TMP_Text prompt;
    public Portal portal; //reference to the portal gameobject's script
    
    public int sceneIndex; //index keeping track of which level the portal will load
    
    
    // Start is called before the first frame update
    void Start()
    {
        portal = GameObject.Find("Portal").GetComponent<Portal>();
        sceneIndex = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)){
            ChangeLevel();
        }
    }

    //iterate through the scene index
    void ChangeLevel(){
        if (!portal.active){ portal.Activate(); }
        
        else{
            sceneIndex++;

            if (sceneIndex > SceneManager.sceneCountInBuildSettings-1){
                sceneIndex = 2;
            }
        }
        portal.levelTextUpdate(sceneIndex);
    }

    void OnTriggerStay(Collider c){
        if (c.gameObject.tag == "Player"){
            prompt.enabled = true;
            if (Input.GetKeyDown(KeyCode.E)){
                ChangeLevel();
            }
        }
    }

    void OnTriggerExit(Collider c){
        if (c.gameObject.tag == "Player"){
            prompt.enabled = false;
        }
    }
}
