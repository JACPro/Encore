using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoalManager : MonoBehaviour
{
    public List<Goal> activeGoals = new List<Goal>();

    [SerializeField]
    GameObject[] textBox = new GameObject[5];

    //the maximum number of goals you can have per quest
    int maxGoals = 3;

    float paddingBetweenGoals = 10f;

    public void ShowGoals(Quest quest) {
        activeGoals = quest.goals;
        if (CheckIfSpaceForGoal()) {
            //show the goal menu            
            textBox[0].transform.parent.gameObject.SetActive(true);
            for (int i = 0; i < activeGoals.Count; i++) {
                textBox[i].SetActive(true);
                textBox[i].GetComponent<TextMeshProUGUI>().text = activeGoals[i].title;
                if (activeGoals[i].completed) {
                    textBox[i].GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Strikethrough;
                } else {
                    textBox[i].GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
                }
            }
            for (int i = activeGoals.Count; i < textBox.Length; i++) {
                textBox[i].GetComponent<TextMeshProUGUI>().text = "";
                textBox[i].SetActive(false);
            }
        } else {
            Debug.Log("ERROR: Cannot add goal as goal limit already reached for this quest");
        }
    }

    public void HideGoals() {
        textBox[0].transform.parent.gameObject.SetActive(false);
    }

    public void RemoveQuest(Goal goal)
    {
        //delete quest from quest board
        goal.DeleteTextBox();
        int indexToRemove = activeGoals.IndexOf(goal);
        activeGoals.RemoveAt(indexToRemove);

        //reposition quests in journal
        for (int i = indexToRemove; i < activeGoals.Count; i++)
        {
            //activeGoals[i].RepositionTextbox(textBox.transform.localPosition.y - textBox.GetComponent<RectTransform>().rect.height * i - paddingBetweenGoals * i);
            activeGoals[i].RenameTextbox("" + i);
        }
    }

    public bool CheckIfSpaceForGoal()
    {
        return activeGoals.Count < maxGoals;
    }
}
