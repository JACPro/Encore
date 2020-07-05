using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField]
    string name;

    [SerializeField]
    DialogueState startingState;

    public string GetName() {
        return name;
    }

    public DialogueState GetDialogueState() {
        return startingState;
    }

    public void SetDialogueState(DialogueState state) {
        startingState = state;
    }
}
