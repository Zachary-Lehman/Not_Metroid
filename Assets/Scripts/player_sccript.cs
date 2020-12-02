using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_sccript : MonoBehaviour
{

    public CharacterController controller;

    Vector3 velocity;
    public float Grav = -10f;
    public float WalkSpeed = 5f;

    public Transform GroundCheck;
    public float GroundDistance = 4f;
    public LayerMask GroundMask;
    bool IsGrounded;
    public float MaxJumpHeight = 4f;
    private float JumpHeight = 0f;
    bool JumpOver = false;


    // Update is called once per frame
    void Update()
    {
        //The way the game checks to see if you are on the ground or not is by using a collision sphere around the bottom of the player using an empty ground check object
        IsGrounded = Physics.CheckSphere(GroundCheck.position, GroundDistance, GroundMask);

        //Makes
        if(IsGrounded == true && velocity.y < 0)
        {
            velocity.y = 0f;
            JumpOver = false;
        }

        if(JumpOver == false)
        {
            if (Input.GetButton("Jump") && JumpHeight < MaxJumpHeight)
            {
                JumpHeight += .15f;
                velocity.y = Mathf.Sqrt(JumpHeight * -2 * Grav);
                JumpOver = false;
            }
        }
    
        if (Input.GetKeyUp("w") || Input.GetKeyUp("space"))
        {
            JumpHeight = 0f;
            JumpOver = true;
        }

        
        //Gets Horizontal input from player
        float HorSpeed = Input.GetAxis("Horizontal") * WalkSpeed;  

        Vector3 HorMove = transform.right * HorSpeed;

        controller.Move(HorMove * WalkSpeed * Time.deltaTime);

        velocity.y += Grav * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}