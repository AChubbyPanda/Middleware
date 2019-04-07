using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    private Rigidbody rb;
    public float speed = 3f;
    public float runSpeed = 6f;
    private float moveSpeed;
    private Vector3 moveDirection;
    public float jumpForce = 10;
    public CapsuleCollider col;
    public LayerMask groundLayers;
    Animator anim;
    int jumpHash = Animator.StringToHash("Jump");

    // Use this for initialization
    void Start ()
    {
        anim = GetComponent<Animator>();
      rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        /*// Allows movement without Charater Controller
        // Allows use of W,S,A,D or arrow keys.
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
          transform.Translate(0f, 0f, 1f * moveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(0f, 0f, -1f * moveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(1f * moveSpeed * Time.deltaTime, 0f, 0f);
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-1f * moveSpeed * Time.deltaTime, 0f, 0f);
        }

        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");
        Vector3 movement;

        movement = new Vector3(hAxis, 0, vAxis);
        
        rb.MovePosition(transform.position + movement);

        //Changes the moveSpeed so the charater can run
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = runSpeed;
        }
        else
        {
            moveSpeed = speed;
        }*/

        // Moves forward in the direction the camera is faceing
        moveDirection = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime);
        moveDirection = moveDirection.normalized * moveSpeed;
        anim.SetFloat("Speed", moveSpeed);

        //Changes the moveSpeed so the charater can run
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
        {
            moveSpeed = runSpeed;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            moveSpeed = speed;
        }
        else
        {
            moveSpeed = 0;
        }

        // If the player is grounded then allow it to jump.
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            anim.SetBool("Jump", true);
        }
        else
        {
            anim.SetBool("Jump", false);
        }

        if (Input.GetMouseButton(0))
        {
            anim.SetBool("Attack", true);
        }
        else
        {
            anim.SetBool("Attack", false);
        }

        // moved the player using the RigidBody
        rb.MovePosition(transform.position + moveDirection * Time.deltaTime);
    }

    // Custom ground check
    private bool IsGrounded()
    {
        // will check if the layer is touching anything under it, if there is its classed as grounded.
        return Physics.CheckCapsule(col.bounds.center, new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z), col.radius * .9f, groundLayers);
    }
}
