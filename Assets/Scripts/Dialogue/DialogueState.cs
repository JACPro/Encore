using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DialogueState")]
public class DialogueState : ScriptableObject
{
    [TextArea(3, 10)] [SerializeField] string dialogueText;
    [SerializeField] string name;

    [SerializeField] DialogueState[] nextStates;

    //return the text for this dialogue state
    public string getDialogueText()
    {
        return dialogueText;
    }

    //return the name of the person that is talking in this dialogue state (e.g. Player, Wizard)
    public string getName()
    {
        return name;
    }

    //return an array containing all possible next States after this dialogue
    public DialogueState[] getNextStates()
    {
        return nextStates;
    }

    //returns whether this state requires a user choice
    public bool getOptional()
    {
        return nextStates.Length > 1;
    }
}
