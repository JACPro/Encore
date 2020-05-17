using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    public Animator animator;

    Queue<string> sentences;

    string currentSentence;

    bool isTyping = false;

    void Start()
    {
        sentences = new Queue<string>();        
    }

    public void StartDialogue (DialogueSequence dialogue) {
        animator.SetBool("IsOpen", true);
        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
        }
        currentSentence = sentences.Peek();
        DisplayNextSentence();
    }

    public void DisplayNextSentence() {
        //If typing, stop typing and complete message
        if (isTyping) {
            StopAllCoroutines();
            dialogueText.text = currentSentence;
            isTyping = false;
            return;
        }

        //If no more sentences and not typing, end dialogue
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        //If not typing and there are more sentences, load next sentence        
        currentSentence = sentences.Dequeue(); 
        isTyping = true;
        StopAllCoroutines();       
        StartCoroutine(TypeSentence(currentSentence, 0.001f));
    }

    IEnumerator TypeSentence (string sentence, float typeTime) {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray()) {
            yield return new WaitForSeconds(typeTime);
            dialogueText.text += letter;
            yield return null;
        }
    }    

    void EndDialogue() {
        animator.SetBool("IsOpen", false);
        isTyping = false;
        StopAllCoroutines();
    }
}
