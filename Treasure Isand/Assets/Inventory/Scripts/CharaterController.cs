using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaterController : MonoBehaviour {

    CharacterController cc;
    public float speed = 3f;
    public float runSpeed = 6f;
    private float moveSpeed;

    //gravity
    float ySpeed = 0f;
    float gravity = -15f;

	// Use this for initialization
	void Start ()
    {
        cc = GetComponent<CharacterController>();	
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Movement forward, back, sideways
        float xInput = Input.GetAxis("Horizontal") * moveSpeed;
        float zInput = Input.GetAxis("Vertical") * moveSpeed;

        // Allows to jump.
       /* if (cc.isGrounded)
        {
            ySpeed = 0f;
            if (Input.GetKeyDown("Jump"))
            {
                ySpeed = 10f;
            }
            else
            {
                // will make sure Charater is grounded when not jumping.
                ySpeed = gravity * Time.deltaTime;
            }
        }
        else
        {
            //Will make sure the Charater will come back down.
            ySpeed += gravity * Time.deltaTime;
        }*/

        // Changes moveSpeed to show running or walking and applys it to the Move Vector
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = runSpeed;
        }
        else
        {
            moveSpeed = speed;
        }

        // Actually apply the movement 
        cc.Move(new Vector3(xInput, ySpeed, zInput) * Time.deltaTime);

    }
}
