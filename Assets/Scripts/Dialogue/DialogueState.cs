using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DialogueState")]
public class DialogueState : ScriptableObject
{
    [TextArea(3, 10)] [SerializeField] string dialogueText;
    [SerializeField] string NpcName;

    [SerializeField] DialogueState[] nextStates;

    //return the text for this dialogue state
    public string GetDialogueText()
    {
        return dialogueText;
    }

    //return the name of the person that is talking in this dialogue state (e.g. Player, Wizard)
    public string GetName()
    {
        return NpcName;
    }

    //return the name of this object
    public string GetObjectName()
    {
        return name;
    }


    //return an array containing all possible next States after this dialogue
    public DialogueState[] GetNextStates()
    {
        return nextStates;
    }

    //returns whether this state requires a user choice
    public bool GetOptional()
    {
        return nextStates.Length > 1;
    }
}
