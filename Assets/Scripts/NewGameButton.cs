using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class NewGameButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField]
    Image panel;

    [SerializeField]
    TextMeshProUGUI title;

    bool canClick = true;

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
        if (canClick) {
            canClick = false;
            panel.GetComponent<FadeIn>().Fade();
            title.GetComponent<Animator>().Play("TMPFadeOut");
            foreach (Transform child in transform.parent) {
                child.gameObject.GetComponent<Animator>().Play("FadeOut");
                child.GetChild(0).gameObject.GetComponent<Animator>().Play("TMPFadeOut");

            }
            StartCoroutine(LoadScene());
        }
    }

    IEnumerator LoadScene() {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("ClassroomIntro");
    }

}

