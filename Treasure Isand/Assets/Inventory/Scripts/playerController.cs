using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

    CharacterController cc;
    Camera cam;
    Animator anim;
    Health health;

    string state = "Movement";

    float gravity = 0f;
    float jumpVelocity = 0;

    public float jumpHeight = 16f;

    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        cc = GetComponent<CharacterController>();
        cam = Camera.main;
        anim = GetComponent<Animator>();
        health = GetComponent<Health>();
    }

    // Update is called once per frame

   // private Vector3 direction;
    void Update()
    {
        //float horizontalMovement = Input.GetAxisRaw("Horizontal");
        //float verticalMovement = Input.GetAxisRaw("Vertical");
        //direction = (horizontalMovement * transform.right + verticalMovement * transform.forward.normalized);

        if (state == "Movement")
        {
            Movement();
            Swing();
        }

        if (state == "Jump")
        {
            Jump();
            Movement();
        }

       /*Animator anim = this.GetComponent<Animator>();
        if (anim)
        {
            anim.SetTrigger("Hurt");
        }*/
    }

    // This sets the Movement speed of the character
    public float walkSpeed = 4f;
    public float runSpeed = 8f;
    public bool isRunning = false;
    private Vector3 moveDirection;

    void Movement()
    {
        cc.Move(moveDirection * Time.deltaTime);

        // moveDirection = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, 0f, Input.GetAxis("Vertical") * moveSpeed);
        float ystore = moveDirection.y;
        moveDirection = (transform.forward * Input.GetAxis("Vertical") * walkSpeed) + (transform.right * Input.GetAxis("Horizontal") * walkSpeed);
        moveDirection = moveDirection.normalized * walkSpeed;
        moveDirection.y = ystore;

        Vector3 velocity = moveDirection * walkSpeed * Time.deltaTime;
        //what direction   how far      per second

        Vector3 sprint = moveDirection * runSpeed * Time.deltaTime;
        //what direction   how far      per second

        float percentSpeed = velocity.magnitude / (walkSpeed * Time.deltaTime);
        anim.SetFloat("movePercent", percentSpeed);

        Vector3 gravityVector = -Vector3.up * gravity * Time.deltaTime;
        // a downward Vector    whose length is "gravity"    in units per second

        Vector3 jumpVector = Vector3.up * jumpVelocity * Time.deltaTime;
        // a upward Vector  whose length is "jumpVelocity" in units per second

        if (isRunning == false)
        {
            Vector3 walking = velocity;

            //CharacterController.Move (velocity)=move along a vector
            cc.Move(velocity + gravityVector + jumpVector);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
            Vector3 running = sprint;
            //CharacterController.Move (sprint)=move along a vector
            cc.Move(sprint + gravityVector + jumpVector);
        }
        else
        {
            isRunning = false;
        }

        //CharacterController.Move (jumpvelocity)=move along a vector

        cc.Move(gravityVector + jumpVector);

        if (cc.isGrounded)
        {
            gravity = 0;
            ChangeState("Movement");
        }
        else
        {
            gravity += 0.25f;
            gravity = Mathf.Clamp(gravity, 1f, 20f);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            jumpVelocity = jumpHeight;
            ChangeState("Jump");
        }
   
    }

    void Jump()
    {
        if (jumpVelocity < 0)
        {
            return;
        }
        jumpVelocity -= 1.25f;
    }

    void ChangeState(string stateName)
    {
        state = stateName;
        anim.SetTrigger(stateName); // this will trigger and animation that requires a trigger i.e keypress to use
    }

    void Swing()
    {
        if(Input.GetMouseButtonDown(0)) //MouseButton(0) is left click
        {
            ChangeState("Swing");
        }

        /*if (Input.GetKey(KeyCode.Return)) //MouseButton(0) is left click
        {
            ChangeState("Swing");
        }*/
    }

    void ReturnToMovement()
    {
        ChangeState("Movement");
    }
}
