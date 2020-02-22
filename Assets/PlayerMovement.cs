using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    
    //How fast the player moves
    public float speed = 12f;

    //How high the player jumps
    public float jumpHeight = 3f;

    //"Standard Gravity (i.e. that of Earth) is 9.80665 m/s squared
    public float gravity = -9.80665f;

    //For checking if the player is currently on ground
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    void Update()
    {
        //Create a small sphere at the base of the player with radius groundDistance to check for any collisions with the groundMask
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        
        //If the player is on the ground, set velocity to -2 (not 0 because isGrounded might trigger before the player is fully grounded)
        if (isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }

        //Get movement input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //Calculate how much the player should move based on input
        Vector3 move = transform.right * x + transform.forward * z;

        //Apply player movement
        controller.Move(move * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        //Calculate current y velocity (for gravity)
        velocity.y += gravity * Time.deltaTime;
        //Apply gravity
        controller.Move(velocity * Time.deltaTime);
    }
}
