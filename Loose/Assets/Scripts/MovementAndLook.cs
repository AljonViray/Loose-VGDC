using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MovementAndLook : MonoBehaviour {

    private float speedMult;
    public float normalSpeed;
    public float sprintSpeed;
    private Vector3 movement = Vector3.zero;

    public float lookSensitivity;
    public float jumpPower;

    private Rigidbody rb;


	void Start ()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        rb = GetComponent<Rigidbody>();

        speedMult = normalSpeed;
	}
	

	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speedMult = sprintSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speedMult = normalSpeed;
        }

        if (Input.GetKeyDown(KeyCode.Space) && rb.velocity.y == 0)
        {
            rb.AddForce(new Vector3(0, jumpPower, 0), ForceMode.VelocityChange);
        }

        Look();
    }


    void FixedUpdate ()
    {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        Vector3 moveHorizontal = transform.right * hAxis;
        Vector3 moveVertical = transform.forward * vAxis;

        Vector3 velocity = (moveHorizontal + moveVertical).normalized * speedMult;

        Move(velocity);
    }



    void Move (Vector3 velocity)
    {
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
    }


    void Look ()
    {
        float scaledMouseDeltaX = Input.GetAxis("Mouse X") * lookSensitivity;
        float scaledMouseDeltaY = Input.GetAxis("Mouse Y") * lookSensitivity;

        transform.Rotate(0, scaledMouseDeltaX, 0);
        float upDownLookAngle = transform.GetComponentInChildren<Camera>().transform.eulerAngles.x;
        if (upDownLookAngle > 90)
        {
            upDownLookAngle -= 360;
        }

        if (upDownLookAngle - scaledMouseDeltaY < 80 && upDownLookAngle - scaledMouseDeltaY > -80)
        {
            transform.GetComponentInChildren<Camera>().transform.Rotate(-scaledMouseDeltaY, 0, 0);
        }
    }
}
