using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    public GameObject panel;

    public Text dialogueText;

    public Button clickArea;

    private Coroutine typingCoroutine;//打字机效果
    private bool isTyping = false;//是否正在打字
    private string currentDialogue;
    private void OnEnable()
    {
        EventHandler.ShowDialogueEvent += ShowDialogue;
        clickArea.onClick.AddListener(OnClickDialogue);
    }

    private void OnDisable()
    {
        EventHandler.ShowDialogueEvent -= ShowDialogue;
        clickArea.onClick.RemoveListener(OnClickDialogue);
    }

    private void ShowDialogue(string dialogue)
    {
        if (string.IsNullOrEmpty(dialogue))
        {
            panel.SetActive(false);
            return;
        }
        else
        {
            panel.SetActive(true);
            currentDialogue = dialogue;
            dialogueText.text = "";

            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
            }

            typingCoroutine = StartCoroutine(TypeText(currentDialogue));
        }
    }

    private void OnClickDialogue()
    {
        if (isTyping)
        {
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
                typingCoroutine = null;
            }
            dialogueText.text = currentDialogue;
            isTyping = false;
            return;
        }
        else
        {
            EventHandler.CallDialogueContinueEvent();
        }
    }
    private IEnumerator TypeText(string text)
    {
        isTyping = true;
        dialogueText.text = "";
        foreach (char letter in text.ToCharArray())
        {
            dialogueText.text += letter;
            if (Time.timeScale <= 0)
            {
                Time.timeScale = 1;
            }
            yield return new WaitForSeconds(0.05f);
        }
        isTyping = false;
    }
}