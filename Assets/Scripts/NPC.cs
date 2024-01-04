using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPC : MonoBehaviour
{

    [SerializeField] private GameObject dialogueMark;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField, TextArea(4,6)] private string[] dialogueLines;
    public float dialogueTime;
    private bool isPlayerInRange;
    private bool didDialogueStart;
    private int lineIndex;

    public GameObject brujitaImage;
    public GameObject belleImage;
    public GameObject princessImage;
    public GameObject michiImage;

    
    void Update()
    {
        if(isPlayerInRange == true && Input.GetKeyDown(KeyCode.F))
        {

            if(gameObject.layer == 13)
            {
                brujitaImage.SetActive(true);
            }
            if(gameObject.layer == 14)
            {
                belleImage.SetActive(true);
            }
            if(gameObject.layer == 15)
            {
                princessImage.SetActive(true);
            }
            if(gameObject.layer == 16)
            {
                michiImage.SetActive(true);
            }

            if(!didDialogueStart)
            {
                StartDialogue();
            }
            else if(dialogueText.text == dialogueLines[lineIndex])
            {
                NextDialogueLine();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = dialogueLines[lineIndex];
            }
        }
    }

    private void StartDialogue()
    {
        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        dialogueMark.SetActive(false);
        Time.timeScale = 0f;
        lineIndex = 0;
        StartCoroutine(ShowLine());
    }

    private void NextDialogueLine()
    {
        lineIndex++;
        if(lineIndex < dialogueLines.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            didDialogueStart = false;
            dialoguePanel.SetActive(false);
            dialogueMark.SetActive(true);
            Time.timeScale = 1f;

            brujitaImage.SetActive(false);
            belleImage.SetActive(false);
            princessImage.SetActive(false);
            michiImage.SetActive(false);
        
        }
        
    }

    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;

        foreach(char ch in dialogueLines[lineIndex])
        {
            dialogueText.text += ch;
            yield return new WaitForSecondsRealtime(dialogueTime);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.layer == 6)
        {
            isPlayerInRange = true;
            dialogueMark.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.layer == 6)
        {
            isPlayerInRange = false;
            dialogueMark.SetActive(false);
        }
    }
}
