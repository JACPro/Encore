using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class QuestMouseHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField]
    GameObject tooltip;

    TextMeshProUGUI tmpText;

    QuestManager questManager;

    GoalManager goalManager;

    void Start() {
        tmpText = GetComponent<TextMeshProUGUI>();
        questManager = FindObjectOfType<QuestManager>();
        goalManager = FindObjectOfType<GoalManager>();
    }    

    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltip.SetActive(true);
        tmpText.color = new Color(253/255f, 117/255f, 80/255f); //change text to orange/red when hover, same colour as close button
        //change description text of tooltip
        tooltip.GetComponentInChildren<TextMeshProUGUI>().text = questManager.activeQuests[Int32.Parse(name)].description;
        tooltip.GetComponentInChildren<TextMeshProUGUI>().ForceMeshUpdate();
        float toolTextPadding = 10f;
        Vector2 backgroundSize = new Vector2(tooltip.GetComponentInChildren<TextMeshProUGUI>().textBounds.size.x + toolTextPadding * 2f, tooltip.GetComponentInChildren<TextMeshProUGUI>().preferredHeight + toolTextPadding * 2f);
        tooltip.GetComponent<RectTransform>().sizeDelta = backgroundSize;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tmpText.color = Color.black; //change text to black on exit
        tooltip.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        goalManager.ShowGoals(questManager.activeQuests[Int32.Parse(name)]);
    }

    private void Update() {
        //put the tooltip above the cursor if it's on the bottom half of the screen and below the cursor otherwise
        if (Input.mousePosition.y > Screen.height / 2) {
            tooltip.GetComponent<RectTransform>().pivot = new Vector2(tooltip.GetComponent<RectTransform>().pivot.x, 1.6f);
        } else {
            tooltip.GetComponent<RectTransform>().pivot = new Vector2(tooltip.GetComponent<RectTransform>().pivot.x, -0.2f);
        }

        //put the tooltip to the left of the cursor if it's on the right half of the screen and right of the cursor otherwise
        if (Input.mousePosition.x > Screen.width / 2)
        {
            tooltip.GetComponent<RectTransform>().pivot = new Vector2(0.9f, tooltip.GetComponent<RectTransform>().pivot.y);
        }
        else
        {
            tooltip.GetComponent<RectTransform>().pivot = new Vector2(0.1f, tooltip.GetComponent<RectTransform>().pivot.y);
        }
        tooltip.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);
    }
}
