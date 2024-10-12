using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour {
    public GameObject winMenuUI;
    private float timer;
    private float besttime;
    private string activeScene;
    public float aTime;
    public float bTime;

    public GameObject aRank;
    public GameObject bRank;
    public GameObject cRank;
    
    [SerializeField]
    private TextMeshProUGUI timerText;
    [SerializeField]
    private TextMeshProUGUI winTimerText;
    [SerializeField]
    private TextMeshProUGUI bestTimeText;

    void Start() {
        //get the name of the active scene
        activeScene = SceneManager.GetActiveScene().name;

        ResetTimer();
        besttime = PlayerPrefs.GetFloat("besttime"+SceneManager.GetActiveScene().name, 5999);
        float minutes = Mathf.FloorToInt(besttime / 60);
        float seconds = Mathf.FloorToInt(besttime % 60);

        string currentTime = string.Format("{0:00}:{1:00}", minutes, seconds);
        bestTimeText.text = currentTime;
    }

    void Update() {
        if (timer < 6000) {
            timer += Time.deltaTime;
            UpdateTimerDisplay(timer);
        } else {
            End();
            UpdateTimerDisplay(timer);
        }
    }

    void CheckBestTime(float time) {
        //compare the current time (input) to the recorded best time
        if (time < besttime) {
            besttime = time;
            bestTimeText.text = timerText.text;
            PlayerPrefs.SetFloat("besttime"+activeScene, time);
        }

        string currentRank = "";
        //check if the rank needs to be updated
        if (time < aTime){ //A rank time
            currentRank += "A";
        }
        else if (time < bTime){ //B rank time
            currentRank += "B";
        }
        else{ //C rank
            currentRank += "C";
        }
        PlayerPrefs.SetString("rank"+activeScene, currentRank);

        string bestrank = PlayerPrefs.GetString("bestrank"+activeScene, "D");
        if (currentRank.CompareTo(bestrank) < 0){
            PlayerPrefs.SetString("bestrank"+activeScene, currentRank);
        }

        PlayerPrefs.Save();

    }
    private void ResetTimer() {
        timer = 0;
        UpdateTimerDisplay(timer);
    }

    private void UpdateTimerDisplay(float time) {
        //update the timer at the top of the screen
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);

        string currentTime = string.Format("{0:00}:{1:00}", minutes, seconds);
        timerText.text = currentTime;
        winTimerText.text = currentTime;
    }

    public void End() {
        Debug.Log("Timer: End()");

        //freeze game and display win menu
        Time.timeScale = 0f;
        winMenuUI.SetActive(true);
        CheckBestTime(timer);


        //display the rank on the win menu
        if (PlayerPrefs.GetString("rank"+activeScene).CompareTo("A") == 0){
            aRank.SetActive(true);
        }
        else if (PlayerPrefs.GetString("rank"+activeScene).CompareTo("B") == 0){
            bRank.SetActive(true);
        }
        else{
            cRank.SetActive(true);
        }
    }

}
