using UnityEngine;
using UnityEngine.SceneManagement;

/* put this in Player Movement
 * connect Win Menu to winMenuUI
 * connect ScreenUI (Timer) to Timer
 
 public GameObject winMenuUI;
 public Timer timer;

 void OnCollisionEnter(Collision collisionInfo) {
      if (collisionInfo.collider.tag == "Win") {
           Debug.Log("Hit.");
           timer.End();   
      }
}
*/

public class WinMenu : MonoBehaviour {
    public GameObject winMenuUI;
    public static bool currentWin = false;

    // Connect next Level Scene to sceneName in Inspector
    public void NextLevel() {
        int cur = SceneManager.GetActiveScene().buildIndex;
        //if (cur+1 < SceneManager.sceneCount)
        SceneManager.LoadScene(cur+1);
        Time.timeScale = 1f;
    }

    //Restart
    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //replace this with current scene
        Time.timeScale = 1f;
    }

    // Connect Hub Scene to sceneName in Inspector
    public void BackToHub() {
        SceneManager.LoadScene("HubHouse");
        Time.timeScale = 1f;
    }

}
