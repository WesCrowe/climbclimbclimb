using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    public static bool currentlyPaused = false;
    public GameObject pauseMenuUI;
    

    void Start(){
        pauseMenuUI = GameObject.Find("Pause Menu");

        pauseMenuUI.SetActive(false);
    }
    
    // during Pause Menu
    public void Pausing() {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        currentlyPaused = true;

        //Debug.Log("Game is Paused");
        
    }

    // Resume
    public void UnPausing() {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        currentlyPaused = false;

        //Debug.Log("Game is Unpaused");
    }

    // Restart
    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //replace this with current scene
        Time.timeScale = 1f;
    }

    // Remember to write the actual scene later
    public void GoToHub() {
        SceneManager.LoadScene("HubHouse");
        Time.timeScale = 1f;
    }
}
