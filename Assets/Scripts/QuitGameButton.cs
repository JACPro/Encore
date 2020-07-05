using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class QuitGameButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Image>().color = new Color(234f / 255f, 181f / 255f, 115f / 255f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Image>().color = new Color(22f / 255f, 135f / 255f, 126f / 255f); 
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Application.Quit();
    }
}

