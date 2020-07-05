using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;

public class QuitToMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
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
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
