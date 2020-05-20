using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(menuName = "Quest")]
public class Quest : ScriptableObject
{
    public List<Goal> goals = new List<Goal>();
    GameObject textBox;
    public string name;
    public string description;

    public bool IsComplete() {
        foreach (Goal goal in goals) {
            if (!goal.IsCompleted()) {
                return false;
            }
        }
        FindObjectOfType<QuestManager>().RemoveQuest(this);
        //TODO show a quest complete pop up
        return true;
    }

    public void AssignGoal(Goal goal) {
        goals.Add(goal);
    }

    public void AssignTextbox(GameObject textBox) {
        this.textBox = textBox;
        textBox.GetComponent<TextMeshProUGUI>().text = name;
    }

    public void RenameTextbox(string name) {
        textBox.name = name;
    }

    public void RepositionTextbox(float newYPos) {
        textBox.transform.localPosition =  new Vector3(textBox.transform.localPosition.x, newYPos, 0f);
    }

    public void DeleteTextBox(){        
        Destroy(textBox);
    }
}
