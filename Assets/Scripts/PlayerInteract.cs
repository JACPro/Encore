using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour
{
    //The object the player is currently looking at
    GameObject objectToInteract;

    //How far from the camera to detect interactable objects
    [SerializeField] int rayLength = 4;

    //The layer to be ignored when looking for objects
    [SerializeField] LayerMask ignoreMask;

    //For displaying a visual crosshair in the middle of the screen
    [SerializeField] Image crosshair;
    bool crosshairActive = false;

    //For picking up items
    [SerializeField] float pickupTime;
    [SerializeField] Image pickupProgressImage;
    [SerializeField] TextMeshProUGUI PressEText;    
    float currentTimeElapsed;

    bool canInteract = true;

    /*
    Send a ray out in the direction the player is facing and check if it finds any objects
    If found objects are items, change crosshair and allow interaction
    */
    void Update() 
    {
        if (canInteract) {
            RaycastHit hit;
        
            if (Physics.Raycast(transform.position, transform.forward, out hit, rayLength, ~ignoreMask.value))
            {
                if (hit.collider.CompareTag("Item"))
                {
                    objectToInteract = hit.collider.gameObject;
                    if (!crosshairActive)
                    {
                        CrosshairActive(true, "Item");
                    }
                    PickupItem();
                } else if (hit.collider.CompareTag("Colonial") || hit.collider.CompareTag("Canarsee")) {
                    objectToInteract = hit.collider.gameObject;
                    if (!crosshairActive)
                    {
                        CrosshairActive(true, "NPC");
                    }
                    if (hit.transform.GetComponent<NPC>().GetDialogueState() != null) {
                        TalkToNPC(hit.transform.GetComponent<NPC>().GetDialogueState());
                    }
                } else {
                    CrosshairActive(false, "");
                    pickupProgressImage.fillAmount = 0;
                    currentTimeElapsed = 0;
                }
            }
            else if (crosshairActive)
            {
                CrosshairActive(false, "");
                pickupProgressImage.fillAmount = 0;
                currentTimeElapsed = 0;
            }
        }
    }

    void TalkToNPC(DialogueState state) {
        if (Input.GetKeyDown(KeyCode.E))
        {
            FindObjectOfType<DialogueManager>().StartDialogue(state);
        }
    }

    /*
    If the player is holding the interact button, increases the progress of picking item up
    When the player has held the button long enough, the item is added to their inventory
    */
    void PickupItem() 
    {
        if (Input.GetButton("Fire1"))
        {
            currentTimeElapsed += Time.deltaTime;
            if (currentTimeElapsed >= pickupTime) 
            {
                MoveItemToInventory();
            }            
        } 
        else 
        {
            currentTimeElapsed = 0f;
        }
        float pctComplete = currentTimeElapsed / pickupTime;
        pickupProgressImage.fillAmount = pctComplete;
    }

    /*
    Change the crosshair colour and displayed interaction text
    */
    void CrosshairActive(bool isActive, string tag) 
    {
        if (isActive) 
        {
            Debug.Log(objectToInteract);

            crosshair.color = Color.red;
            crosshairActive = true;
            if (tag == "Item") {
                PressEText.text = "Hold E to pick up " + objectToInteract.GetComponent<Item>().GetName();
            } else if (tag == "NPC") {
                PressEText.text = "Press E to talk to " + objectToInteract.GetComponent<NPC>().GetName();                
            }
        
        } 
        else 
        {
            crosshair.color = Color.white;
            crosshairActive = false;
            PressEText.text = "";
        }
    }

    /*
    Called as the result of a successful interaction with the object
    Moves the picked up object to the player's inventory and deletes it from the game
    */
    void MoveItemToInventory() {
        //TODO change add the object to the inventory
        FindObjectOfType<GameManager>().PickupItem(objectToInteract.GetComponent<Item>().GetName());
        objectToInteract.SetActive(false);
        PressEText.text = "";
    }

    public void SetCanInteract(bool canInteract) {
        this.canInteract = canInteract;
    }
}