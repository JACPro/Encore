using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PickupItem : MonoBehaviour
{
    [SerializeField] Camera camera;
    [SerializeField] LayerMask layerMask;
    [SerializeField] float pickupTime;
    [SerializeField] RectTransform pickupImageRoot;
    [SerializeField] Image pickupProgressImage;
    [SerializeField] TextMeshProUGUI itemNameText;

    GameObject itemBeingPickedUp;
    float currentTimeElapsed; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
