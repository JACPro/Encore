using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class Goal : ScriptableObject
{
    public string title;
    public string description;
    public bool completed;
    
    GameObject textBox;

    public Goal nextGoal;
    public Goal prevGoal;

    public void SetNextGoal(Goal goal) {
        nextGoal = goal;
        nextGoal.prevGoal = this;
    }

    public void AssignTextbox(GameObject textBox)
    {
        this.textBox = textBox;
        textBox.GetComponent<TextMeshProUGUI>().text = name;
    }

    public void RenameTextbox(string name)
    {
        textBox.name = name;
    }

    public void RepositionTextbox(float newYPos)
    {
        textBox.transform.localPosition = new Vector3(textBox.transform.localPosition.x, newYPos, 0f);
    }

    public void DeleteTextBox()
    {
        Destroy(textBox);
    }
}
