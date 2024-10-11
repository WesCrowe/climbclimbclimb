
using UnityEngine;
using TMPro;
using UnityEngine.Android;

public class DialogueTrigger : MonoBehaviour
{
    public GameObject dialogueCanvas;
    public TMP_Text npcName;
    public TMP_Text speech;
    public Dialogue dialogue;
    

    public void TriggerDialogue(int n){
        if(dialogueCanvas) dialogueCanvas.SetActive(true);
        npcName.text = dialogue.name;
        speech.text = dialogue.sentences[n];
    }
    
    public void InitialDialogue()
    {
        if(dialogueCanvas) dialogueCanvas.SetActive(true);
        npcName.text = dialogue.name;
        speech.text = dialogue.sentences[0];
    }

    public void OtherDialogue()
    {
        if(dialogueCanvas) dialogueCanvas.SetActive(true);
        npcName.text = dialogue.name;
        speech.text = GiveMeRand() ? dialogue.sentences[1]: dialogue.sentences[2] ;
    }

    public void Farewell()
    {
        if(dialogueCanvas) dialogueCanvas.SetActive(true);
        npcName.text = dialogue.name;
        speech.text = dialogue.sentences[3];
    }

    public void HideCanvas()
    {
        if(dialogueCanvas) dialogueCanvas.SetActive(false);
    }

    private bool GiveMeRand()
    {
        return  Random.Range(1, 10) % 2 == 0;
    }

    void OnTriggerStay(Collider other){
        if (other.gameObject.CompareTag("Player")){
            InitialDialogue();
        }
    }

    void OnTriggerExit(Collider other){
        HideCanvas();
    }
}
