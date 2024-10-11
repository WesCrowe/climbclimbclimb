/* Goal.cs
 * Author: Wesley Crowe
 * Script to control the behavior of the goal object for each level
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{

    //public GameObject winMenu;
    public Timer timer;

    // Start is called before the first frame update
    void Start()
    {
        //winMenu = GameObject.Find("Win Menu");

        //winMenu.SetActive(false);

        //Debug.Log("WinMenu set to false");

        timer = GameObject.Find("Screen UI").GetComponent<Timer>();
    }

    // Update is called once per frame
    void Update()
    {
        //spin the goal object
        gameObject.transform.Rotate(0,0.5f,0);
    }

    //When the player touches the goal, trigger victory event
    void OnTriggerEnter(Collider c){
        if (c.gameObject.tag == "Player"){

            //Time.timeScale = 0f;
            //winMenu.SetActive(true);
            timer.End();

            string rank = PlayerPrefs.GetString("rank"+SceneManager.GetActiveScene().name);
            Debug.Log("rank="+rank);
            
            /*for (int i = 0; i < 3; i++){
                Debug.Log("i="+i);
                Debug.Log("winMenu.transform.GetChild(i)"+winMenu.transform.GetChild(i));
                GameObject letter = winMenu.transform.GetChild(i).gameObject;
                if (letter.name.CompareTo(rank) == 0){
                    letter.SetActive(true);
                    break;
                }
            }*/

            Debug.Log("Victory!");
        }
    }
}
