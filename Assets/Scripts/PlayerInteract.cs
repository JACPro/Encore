using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour
{
    GameObject objectToInteract;

    [SerializeField] int rayLength = 10;
    [SerializeField] LayerMask interactionMask;

    [SerializeField] Image crosshair;

    bool crosshairActive = false;

    void Update() 
    {
        RaycastHit hit;        

        if (Physics.Raycast(transform.position, transform.forward, out hit, rayLength, interactionMask.value)) 
        {
            if(hit.collider.CompareTag("Item")) 
            {
                objectToInteract = hit.collider.gameObject;
                if (!crosshairActive) {
                    CrosshairActive(true);                
                }

                if (Input.GetKeyDown(KeyCode.E)) {
                    objectToInteract.SetActive(false);
                }
            }
        }    
        else if (crosshairActive)
        {
            CrosshairActive(false);
        }
    }

    void CrosshairActive(bool isActive) 
    {
        if (isActive) 
        {
            crosshair.color = Color.red;
            Debug.Log("Red now!");
            crosshairActive = true;
        } 
        else 
        {
            crosshair.color = Color.white;
            Debug.Log("White now!");
            crosshairActive = false;
        }
    }
}
