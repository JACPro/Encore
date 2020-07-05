using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    public Button continueButton;

    public Animator animator;

    [SerializeField]
    Image crosshair;

    [SerializeField]
    TextMeshProUGUI pressEText;

    DialogueState currentState;

    bool isTyping = false;

    bool waitingForResponse = false;

    bool inDialogue = false; 

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
                if (currentState.GetNextStates().Length > 1)
                {
                    DisplayNextSentence(2);
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (currentState.GetNextStates().Length > 2) {
                    DisplayNextSentence(3);
                }
            }        
        }
    }

    public void StartDialogue (DialogueState startingDialogue) {
        inDialogue = true;
        FindObjectOfType<PlayerInteract>().SetCanInteract(false);
        pressEText.text = "";
        crosshair.gameObject.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        animator.SetBool("IsOpen", true);
        currentState = startingDialogue;
        SetNextState();
    }

    public void DisplayNextSentence(int choice) {        
        //If typing, stop typing and complete message
        if (isTyping) {
            StopAllCoroutines();
            dialogueText.text = currentState.GetDialogueText();
            isTyping = false;
            return;
        }

        //If no more sentences and not typing, end dialogue
        if (currentState.GetNextStates()[0] == null || currentState.GetNextStates().Length < 1)
        {
            EndDialogue();
            return;
        }

        //determine the next state
        switch (choice)
        {
            case 0: //if this state is non-user choice
                currentState = currentState.GetNextStates()[0];
                break;
            default: //if user choice, load the user's choice
                currentState = currentState.GetNextStates()[choice - 1];
                break;
        }

        SetNextState();
    }

    void SetNextState() {
        nameText.text = currentState.GetName();
        if (currentState.GetName() != "You")
        {
            continueButton.gameObject.SetActive(true); //show the continue button
            waitingForResponse = false;
            isTyping = true;
            StopAllCoroutines();
            StartCoroutine(TypeSentence(currentState.GetDialogueText()));
        }
        else
        {
            continueButton.gameObject.SetActive(false); //hide the continue button
            waitingForResponse = true;
            dialogueText.text = currentState.GetDialogueText(); //set choice text        
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
        inDialogue = false;
        FindObjectOfType<PlayerInteract>().SetCanInteract(true);
        crosshair.gameObject.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        FindObjectOfType<GameManager>().DialogueEnd(currentState.name);
        animator.SetBool("IsOpen", false);
        isTyping = false;
        StopAllCoroutines();
    }

    public bool GetInDialogue() {
        return inDialogue;
    }
}