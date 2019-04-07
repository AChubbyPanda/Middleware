using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float walkSpeed;
    public float jumpForce;
    public float runSpeed = 8f;
    public bool isRunning = false;

    public CharacterController controller;

    private Vector3 moveDirection;
    public float gravityScale;

    public Transform pivot;
    public float rotateSpeed;

    public GameObject playerModel;

    public Animator anim;

    // Use this for initialization
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame

    // private Vector3 direction;
    void Update()
    {

        if (controller.isGrounded)
        {
            moveDirection.y = 0f;
            if (Input.GetKeyDown("space"))
            {
                moveDirection.y = jumpForce;
            }
        }

        if (isRunning == false)
        {
            float ystore = moveDirection.y;
            moveDirection = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));
            moveDirection = moveDirection.normalized * walkSpeed;
            moveDirection.y = ystore;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
            float ystore = moveDirection.y;
            moveDirection = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));
            moveDirection = moveDirection.normalized * runSpeed;
            moveDirection.y = ystore;
        }
        else
        {
            isRunning = false;
        }

        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
        controller.Move(moveDirection * Time.deltaTime);

        //Move the player in different directions based on camera look direction
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            transform.rotation = Quaternion.Euler(0f, pivot.rotation.eulerAngles.y, 0f);
            Quaternion newRoatation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
            playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRoatation, rotateSpeed * Time.deltaTime);
        }
    }
}
