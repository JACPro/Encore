using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Image newQuestBox;

    [SerializeField]
    Image fadePanel;

    [SerializeField]
    Quest main1, main2, main3, main4, main5, nativeTalk, colonistTalk;

    [SerializeField]
    NPC minuit, seyseys;

    [SerializeField]
    DialogueState part1a, part2a, part3, part3a, part4, part4a, part5;

    [SerializeField]
    QuestManager questManager;

    [SerializeField] 
    GameObject sackOfSupplies;

    TalkGoal thisGoal;
    FetchGoal fetchGoal;
    MultipleTalkGoal multipleTalk;

    void Start() {
        thisGoal = new TalkGoal();
        fetchGoal = new FetchGoal();
        multipleTalk = new MultipleTalkGoal();
        switch (SceneManager.GetActiveScene().name) {
            case "MainMenu":
                break;
            case "ClassroomIntro":
                StartCoroutine(DelayedDialogueStart());
                break;
            case "Main":
                //hide the questbox on start
                newQuestBox.canvasRenderer.SetAlpha(0f);
                foreach (Transform child in newQuestBox.gameObject.transform)
                {
                    child.GetComponent<TextMeshProUGUI>().canvasRenderer.SetAlpha(0f);
                }
                foreach (Goal goal in main1.goals)
                {
                    goal.AssignQuest(main1);
                }
                StartCoroutine(DelayedGiveQuest(main1));
                foreach (Goal goal in nativeTalk.goals)
                {
                    goal.AssignQuest(nativeTalk);
                }
                FindObjectOfType<QuestManager>().GiveQuest(nativeTalk);
                foreach (Goal goal in colonistTalk.goals)
                {
                    goal.AssignQuest(colonistTalk);
                }
                FindObjectOfType<QuestManager>().GiveQuest(colonistTalk);
                break;
            case "ClassroomEnd":
                StartCoroutine(DelayedDialogueStart());
                break;
        }
    }

    public void DialogueEnd(string name) {
        switch (name) {
            //Classroom Intro
            case "Intro5":
                fadePanel.GetComponent<FadeIn>().Fade();
                StartCoroutine(DelayedLoadScene("Main"));
                break;

            //Main Game
            case "Ch1P10":
                minuit.SetDialogueState(part1a);
                seyseys.gameObject.tag = "Canarsee";
                thisGoal = (TalkGoal) main1.goals[0];
                thisGoal.Talk(minuit);
                foreach (Goal goal in main2.goals)
                {
                    goal.AssignQuest(main2);
                }
                questManager.GiveQuest(main2);
                break;
            case "Ch2P9":
                seyseys.SetDialogueState(part2a);
                minuit.SetDialogueState(part3);
                thisGoal = (TalkGoal)main2.goals[0];
                thisGoal.Talk(seyseys);
                foreach (Goal goal in main3.goals)
                {
                    goal.AssignQuest(main3);
                }
                questManager.GiveQuest(main3);
                break;
            case "Ch3P12":
                minuit.SetDialogueState(part3a);
                thisGoal = (TalkGoal)main3.goals[0];
                thisGoal.Talk(minuit);
                sackOfSupplies.tag = "Item";
                foreach (Goal goal in main4.goals)
                {
                    goal.AssignQuest(main4);
                }
                questManager.GiveQuest(main4);
                break;
            case "Ch4P7":
                seyseys.SetDialogueState(part4a);
                minuit.SetDialogueState(part5);
                thisGoal = (TalkGoal)main4.goals[1];
                thisGoal.Talk(seyseys);
                foreach (Goal goal in main5.goals)
                {
                    goal.AssignQuest(main5);
                }
                questManager.GiveQuest(main5);
                break;
            case "Ch5P7":
                fadePanel.GetComponent<FadeIn>().Fade();
                thisGoal = (TalkGoal)main5.goals[0];
                thisGoal.Talk(minuit);
                StartCoroutine(DelayedLoadScene("ClassroomEnd"));
                break;
            //SideQuests
            case "Col1": case "Col2": case "Col3": case "Col4a1c": case "Col4a2e": case "Col4a3b":
                multipleTalk = (MultipleTalkGoal)colonistTalk.goals[0];
                multipleTalk.Talk("Colonial");
                break;
            case "Nat1": case "Nat2": case "Nat3c": case "Nat4": case "Nat5":
                multipleTalk = (MultipleTalkGoal)nativeTalk.goals[0];
                multipleTalk.Talk("Canarsee");
                break;

            //Classroom Outro
            case "Outro12":
                fadePanel.GetComponent<FadeIn>().Fade();
                StartCoroutine(DelayedLoadScene("MainMenu"));
                //Ensure cursor shows in main menu
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                break;
        }
    }

    public void PickupItem(string name) {
        if (name == "Sack of Supplies") {
            fetchGoal = (FetchGoal)main4.goals[0];
            fetchGoal.NewItem(name);
            seyseys.SetDialogueState(part4);
        }
    }

    public void ShowQuestBox() {
        newQuestBox.canvasRenderer.SetAlpha(0f);
        newQuestBox.CrossFadeAlpha(1f, 1f, false);
        foreach (Transform child in newQuestBox.gameObject.transform) {
            child.GetComponent<TextMeshProUGUI>().canvasRenderer.SetAlpha(0f);
            child.GetComponent<TextMeshProUGUI>().CrossFadeAlpha(1f, 1f, false);
        }
        StartCoroutine(WaitHideBox());
    }

    IEnumerator WaitHideBox() {
        yield return new WaitForSeconds(4f);
        newQuestBox.CrossFadeAlpha(0f, 1f, false);
        foreach (Transform child in newQuestBox.gameObject.transform)
        {
            child.GetComponent<TextMeshProUGUI>().CrossFadeAlpha(0f, 1f, false);
        }
    }

    IEnumerator DelayedDialogueStart() {
        yield return new WaitForSeconds(2f);
        FindObjectOfType<DialogueManager>().StartDialogue(FindObjectOfType<NPC>().GetDialogueState());
    }

    IEnumerator DelayedLoadScene(string name) {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(name);
    }

    IEnumerator DelayedGiveQuest(Quest quest) {
        yield return new WaitForSeconds(2f);
        FindObjectOfType<QuestManager>().GiveQuest(quest);
    }
}
