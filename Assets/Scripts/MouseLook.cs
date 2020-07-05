using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float sensitivity = 100f;

    public Transform playerBody;
    
    float xRotation = 0f;

    void Start()
    {
        //Don't show the cursor in game
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //if the user is not currently using the mouse cursor
        if (Cursor.lockState == CursorLockMode.Locked) {
            //Get mouse input on the horizontal axis
            float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        
            //Get mouse input on the vertical axis
            float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

            //Update the current player rotation every frame based on mouse input from that frame
            xRotation -= mouseY;
            //Prevents looking around 360 degrees (only allows looking between -85 and 85 degrees)
            xRotation = Mathf.Clamp(xRotation, -85f, 85f);

            //Apply the rotation
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}