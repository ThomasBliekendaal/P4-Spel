using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : HealthScript {
    public GameObject pCamera;
    public float walkSpeed;
    public int jumps;
    public float sensitivity;
    public Vector3 jumpVel;
    public int totalJumps;
    public float sprintSpeed;
    private float walkSpeed2;
    private RaycastHit hit;
    public float rayLength;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    void Start () {
        MovementStart();
        health = maxHealth;
    }
	
	void FixedUpdate () {
        Movement();
        Health();
    }


    public void MovementStart()
    {
        Cursor.lockState = CursorLockMode.Locked;
        totalJumps = jumps;
        walkSpeed2 = walkSpeed;
    }
    public void Movement()
    {
        transform.Translate(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * walkSpeed * Time.deltaTime);
        if (jumps <= 0)
        {
            Debug.DrawRay(gameObject.transform.position, Vector3.down, Color.red, rayLength);
            Ray floorFinder = new Ray(gameObject.transform.position, Vector3.down); ;
            if (Physics.Raycast(floorFinder, out hit, rayLength))
            {
                jumps = totalJumps;
            }
        }
        if (Input.GetButtonDown("Jump"))
        {
            if (jumps > 0)
            {
                GetComponent<Rigidbody>().velocity = jumpVel;
                jumps -= 1;
            }
        }
        if (gameObject.GetComponent<Rigidbody>().velocity.y < 0)
        {
            gameObject.GetComponent<Rigidbody>().velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;        
        }
        else if (gameObject.GetComponent<Rigidbody>().velocity.y > 0 && !Input.GetButton("Jump"))
        {
            gameObject.GetComponent<Rigidbody>().velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
        if (Input.GetButton("Sprint"))
        {
            walkSpeed = sprintSpeed;
        }
        else
        {
            walkSpeed = walkSpeed2;
        }
        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime, 0));
        pCamera.transform.Rotate(-Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime, 0, 0);
    }
}
