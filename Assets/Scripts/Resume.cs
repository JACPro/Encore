using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Resume : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField]
    GameObject questUI;

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<TextMeshProUGUI>().color = new Color(234f / 255f, 181f / 255f, 115f / 255f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<TextMeshProUGUI>().color = new Color(23f / 255f, 139f / 255f, 130f / 255f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!FindObjectOfType<DialogueManager>().GetInDialogue() && !questUI.activeSelf) {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked; 
        }
        Time.timeScale = 1;
        transform.parent.gameObject.SetActive(false);
    }
}
