/* Orb.cs
 * Author: Wesley Crowe
 * A script that makes an orb object into an interactible level selector
 */
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Orb : MonoBehaviour
{
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

    void ChangeLevel(){
        if (!portal.active){ portal.Activate(); Debug.Log("Activating portal through orb"); }
        
        else{
            Debug.Log("Changing Level");
            sceneIndex++;

            if (sceneIndex > SceneManager.sceneCountInBuildSettings-1){
                sceneIndex = 2;
            }
        }

        Debug.Log("sceneIndex in Orb: "+sceneIndex);
        portal.levelTextUpdate(sceneIndex);
    }

    void OnTriggerStay(Collider c){
        if (c.gameObject.tag == "Player"){
            if (Input.GetKeyDown(KeyCode.E)){
                ChangeLevel();
            }
        }
    }
}
