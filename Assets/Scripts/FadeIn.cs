using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{    
    Image panel;

    void Start()
    {
        panel = GetComponent<Image>();
        Color panelColour = panel.color;
        panelColour.a = 0;
        panel.color = panelColour;
    }

    public void Fade() {
        panel.CrossFadeAlpha(1, 2, false);
    }
}
