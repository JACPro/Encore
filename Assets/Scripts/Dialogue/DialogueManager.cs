using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    public Button continueButton;

    public Animator animator;

    DialogueState currentState;

    bool isTyping = false;

    bool waitingForResponse = false;

    void Start() {
        currentState = new DialogueState();
    }

    void Update() {
        if (waitingForResponse) {
            //check which option user chooses
            if (Input.GetKeyDown(KeyCode.Alpha1)) {
                DisplayNextSentence(1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (currentState.getNextStates().Length > 1)
                {
                    DisplayNextSentence(2);
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (currentState.getNextStates().Length > 2) {
                    DisplayNextSentence(3);
                }
            }        
        }
    }

    public void StartDialogue (DialogueState startingDialogue) {
        animator.SetBool("IsOpen", true);
        currentState = startingDialogue;
        SetNextState();
    }

    public void DisplayNextSentence(int choice) {        
        //If typing, stop typing and complete message
        if (isTyping) {
            StopAllCoroutines();
            dialogueText.text = currentState.getDialogueText();
            isTyping = false;
            return;
        }

        //If no more sentences and not typing, end dialogue
        if (currentState.getNextStates()[0] == null || currentState.getNextStates().Length < 1)
        {
            EndDialogue();
            return;
        }

        //determine the next state
        switch (choice)
        {
            case 0: //if this state is non-user choice
                currentState = currentState.getNextStates()[0];
                break;
            default: //if user choice, load the user's choice
                currentState = currentState.getNextStates()[choice - 1];
                break;
        }

        SetNextState();
    }

    void SetNextState() {
        nameText.text = currentState.getName();
        if (currentState.getName() != "You")
        {
            continueButton.gameObject.SetActive(true); //show the continue button
            waitingForResponse = false;
            isTyping = true;
            StopAllCoroutines();
            StartCoroutine(TypeSentence(currentState.getDialogueText()));
        }
        else
        {
            continueButton.gameObject.SetActive(false); //hide the continue button
            waitingForResponse = true;
            dialogueText.text = currentState.getDialogueText(); //set choice text        
        }
    }

    /*
    To change the typing speed, add a float typeTime parameter and call 
    "yield return new WaitForSeconds(typeTime);" before appending the letter
    */
    IEnumerator TypeSentence (string sentence) {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray()) {
            dialogueText.text += letter;
            yield return null;
        }
        isTyping = false;
    }    

    void EndDialogue() {
        animator.SetBool("IsOpen", false);
        isTyping = false;
        StopAllCoroutines();
    }
}