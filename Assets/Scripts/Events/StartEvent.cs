using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartEvent : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private GameObject _imageToShow;
    [SerializeField] private CanvasAnimation m_Animation;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;
    public float dialogueTime;
    private bool isPlayerInRange;
    private bool didDialogueStart, _didDialogueEnd;
    private int lineIndex;

    void Update()
    {
        if (!_didDialogueEnd)
        {
            if (!didDialogueStart)
            {
                StartDialogue();
            }
            else if (dialogueText.text == dialogueLines[lineIndex] && Input.GetKeyDown(KeyCode.F))
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
        m_Animation.Func_PlayUIAnim(_imageToShow);
        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        Time.timeScale = 0f;
        lineIndex = 0;
        StartCoroutine(ShowLine());
    }

    private void NextDialogueLine()
    {
        lineIndex++;
        if (lineIndex < dialogueLines.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            didDialogueStart = false;
            dialoguePanel.SetActive(false);
            Time.timeScale = 1f;

            m_Animation.Func_StopUIAnim();
            _imageToShow.SetActive(false);
            _didDialogueEnd = true;

        }

    }

    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;

        foreach (char ch in dialogueLines[lineIndex])
        {
            dialogueText.text += ch;
            yield return new WaitForSecondsRealtime(dialogueTime);
        }
    }
}