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

    public void GiveQuest(Quest quest) {
        if (CheckIfSpaceForQuest()) {
            FindObjectOfType<GameManager>().ShowQuestBox();
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
