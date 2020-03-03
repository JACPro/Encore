using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Unity's CharacterController class
    public CharacterController controller;
    
    //How fast the player moves
    [SerializeField] float speed = 12f;
    [SerializeField] float sprintModifier = 2f;
    [SerializeField] float crouchModifer = 0.75f;

    float playerHeight;
    [SerializeField] float crouchTime = 5f;

    //How high the player jumps
    [SerializeField] float jumpHeight = 3f;

    //"Standard Gravity (i.e. that of Earth) is 9.80665 m/s squared
    [SerializeField] float gravity = -9.80665f;

    //For checking if the player is currently on ground
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    void Start() 
    {
        playerHeight = controller.height;
    }

    void Update()
    {
        //The player's current height (changes when crouching)
        float currentHeight = playerHeight;

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
        //Check for sprint
        if (Input.GetKey(KeyCode.LeftShift)) {
            //Sprint movement speed
            controller.Move(move * speed * sprintModifier *  Time.deltaTime);
        } 
        //Check for crouch
        else if (Input.GetKey(KeyCode.LeftControl)) 
        {
            //Crouch movement speed
            controller.Move(move * speed * crouchModifer * Time.deltaTime);
            currentHeight = playerHeight / 2;
        }
        else
        {
            //Normal movement speed
            controller.Move(move * speed * Time.deltaTime);
        }

        //If player presses jump key and is currently on the ground, jump
        if(Input.GetButtonDown("Jump") && isGrounded) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        //Calculate current y velocity (for gravity)
        velocity.y += gravity * Time.deltaTime;
        //Apply gravity
        controller.Move(velocity * Time.deltaTime);

        //Change player height (if switching between crouch)
        controller.height = Mathf.Lerp(controller.height, currentHeight, crouchTime * Time.deltaTime);
        float prevHeight = controller.height;
        transform.position = new Vector3(
            transform.position.x, 
            transform.position.y + (controller.height - prevHeight) / 2, 
            transform.position.z);
    }
}
