using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour {

    //public GameObject target;
    float thirdPerson = 4f;
    float firstPerson = 0f;
    bool isThirdPerson= true;
    public Transform target;
    public Vector3 offset;
    public bool useOffsetValues;
    public float rotateSpeed;
    public Transform pivot;
    public float maxViewAngle;
    public float minViewAngle;
    public bool invertY;

    private void Start()
    {
        if(!useOffsetValues)
        {
            offset = target.position - transform.position;
        }

        pivot.transform.position = target.position;
        pivot.transform.parent = target.transform;

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update ()
    {
        //Get the x position of the mouse & rotate the target.
         float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
         target.Rotate (0, horizontal, 0);

        //Get the y position of the mouse & rotate the target.
        float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
       
        // creates the option to invert in unity without entering the code
        if (invertY)
        {
            pivot.Rotate(vertical, 0, 0);
        }
        else
        {
            pivot.Rotate(-vertical, 0, 0);
        }

        //limit up/down camera rotation
        //Also allows you to change the freshhold in unity.
        if (pivot.rotation.eulerAngles.x > maxViewAngle && pivot.rotation.eulerAngles.x < 180f)
        {
            pivot.rotation = Quaternion.Euler(maxViewAngle, 0, 0);
        }

        if (pivot.rotation.eulerAngles.x > 180 && pivot.rotation.eulerAngles.x < 360f + minViewAngle)
        {
            pivot.rotation = Quaternion.Euler(360f + minViewAngle, 0, 0);
        }

        //Move the camera based on the current rotation of the target and the original offset
        float desiredYAngle = target.eulerAngles.y;
         float desiredXAngle = pivot.eulerAngles.x;

         Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);
         transform.position = target.position - (rotation * offset);
         //transform.position = target.position - offset;

        transform.LookAt(target);

        //Will allow you to switch between 3rd and firts person in game
        if (isThirdPerson != false)
        {
            transform.position = target.transform.position - thirdPerson * transform.forward + Vector3.up * 1.5f;
        }

        if (Input.GetKeyUp("-"))
        {
            transform.position = target.transform.position - firstPerson * transform.forward + Vector3.up * 1.5f;
            isThirdPerson = false;
        }
        else if (Input.GetKeyUp("="))
        {
            isThirdPerson = true;
        }
	}
}
