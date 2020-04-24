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
        panelColour.a = 1;
        panel.color = panelColour;
        
        Fade();
    }

    void Fade() {
        panel.CrossFadeAlpha(0, 2, false);
    }
}
