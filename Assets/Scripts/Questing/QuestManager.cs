using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public List<Quest> activeQuests = new List<Quest>();
    List<Quest> completedQuests = new List<Quest>();

    [SerializeField]
    GameObject textBox;

    //the maximum number of quests you can have at any one time
    int maxQuests = 5;

    float paddingBetweenQuests = 10f;

    void Start() {
        Quest quest1 = new Quest();
        quest1.name = "Wow! My first Quest!";
        quest1.description = "Wow! My first Quest!";
        GiveQuest(quest1);
        Goal goal1 = new FetchGoal();
        goal1.name = "Go fetch a thing!";
        goal1.description = "Description says you should go fetch a thing!";
        goal1.completed = true;
        Goal goal2 = new TalkGoal();
        goal2.name = "Go talk to someone!";
        goal2.description = "Description says you should go talk to someone!";
        quest1.AssignGoal(goal1);
        quest1.AssignGoal(goal2);
        Quest quest2 = new Quest();
        quest2.name = "A second quest. Time for an adventure!";
        quest2.description = "A second quest. Time for an adventure!";
        GiveQuest(quest2);
        Quest quest3 = new Quest();
        quest3.name = "Congratulations on being given your third quest.";
        quest3.description = "Congratulations on being given your third quest.";
        GiveQuest(quest3);
        Quest quest4 = new Quest();
        quest4.name = "Congratulations on your fourth quest.";
        quest4.description = "Congratulations on your fourth quest.";
        GiveQuest(quest4);
        Quest quest5 = new Quest();
        quest5.name = "Congratulations on being given your fifth quest.";
        quest5.description = "Congratulations on being given your fifth quest.";
        GiveQuest(quest5);
        Quest quest6 = new Quest();
        quest6.name = "Congratulations on being given your sixth quest.";
        quest6.description = "Congratulations on being given your sixth quest.";
        GiveQuest(quest6);

        foreach(Quest quest in completedQuests) {
            Debug.Log(quest.name);
        }
    }

    public void GiveQuest(Quest quest) {
        if (CheckIfSpaceForQuest()) {
            //create a duplicate of the original quest name text box
            GameObject questTextBox = Instantiate(textBox);
            questTextBox.name = "" + activeQuests.Count;
            questTextBox.transform.SetParent(textBox.transform.parent, false);
            //reposition the new quest to be below the previous quest in the list
            questTextBox.transform.localPosition = new Vector3(
                textBox.transform.localPosition.x, 
                textBox.transform.localPosition.y - textBox.GetComponent<RectTransform>().rect.height * activeQuests.Count - paddingBetweenQuests * activeQuests.Count, 
                0f
            );
            questTextBox.gameObject.SetActive(true);            
            quest.AssignTextbox(questTextBox);
            activeQuests.Add(quest);
        } else {
            Debug.Log("ERROR: Cannot add quest as quest log already full");
        }
    }

    public void RemoveQuest(Quest quest) {
        //delete quest from quest board
        quest.DeleteTextBox();
        int indexToRemove = activeQuests.IndexOf(quest);
        activeQuests.RemoveAt(indexToRemove);
        completedQuests.Add(quest);

        //reposition quests in journal
        for (int i = indexToRemove; i < activeQuests.Count; i++) {
            activeQuests[i].RepositionTextbox(textBox.transform.localPosition.y - textBox.GetComponent<RectTransform>().rect.height * i - paddingBetweenQuests * i);
            activeQuests[i].RenameTextbox("" + i);
        }
    }

    public bool CheckIfSpaceForQuest() {
        return activeQuests.Count < maxQuests;
    }

    public bool CheckCompleted(Quest questToCheck) {
        return completedQuests.Contains(questToCheck);
    }
}
