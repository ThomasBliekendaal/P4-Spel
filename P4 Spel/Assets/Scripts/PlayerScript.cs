using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : HealthScript
{
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
    public float weaponSlower;
    private bool jumpCheck;
    public bool openMenu;

    public bool godmode;

    void Start()
    {
        MovementStart();
        health = maxHealth;
    }

    void FixedUpdate()
    {
        if (!openMenu)
        {
            Movement();
            Health();
        }
    }

    private void Update()
    {
        if (health <= minHealth)
        {
            Destroy(gameObject);
        }
        if (godmode)
        {
            health = maxHealth;
        }
    }

    public void ToggleGod()
    {
        godmode = !godmode;
    }

    public void MovementStart()
    {
        totalJumps = jumps;
        walkSpeed2 = walkSpeed;
    }
    public void Movement()
    {
        transform.Translate(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * walkSpeed * Time.deltaTime * weaponSlower);
        if (jumps <= 0 && jumpCheck)
        {
            Debug.DrawRay(gameObject.transform.position, -transform.up, Color.red, rayLength);
            if (Physics.Raycast(transform.position, -transform.up, out hit, rayLength) && hit.collider.gameObject != null)
            {
                jumps = totalJumps;
            }
        }
        if (Input.GetButtonDown("Jump"))
        {
            if (jumps > 0)
            {
                GetComponent<Rigidbody>().velocity = jumpVel;
                jumps--;
                jumpCheck = false;
                StartCoroutine(JumpTimer());
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

    public IEnumerator JumpTimer()
    {
        yield return new WaitForSeconds(0.5f);
        jumpCheck = true;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            DoDam(collision.gameObject.GetComponent<Bullet>().damage);
        }
        if (collision.gameObject.tag == "Enemy")
        {
            EnemyAttackBase eAb = collision.gameObject.GetComponent<EnemyAttackBase>();
            if (eAb.attackType == EnemyAttackBase.State2.Melee)
            {
                DoDam(collision.gameObject.GetComponent<EnemyMovement>().damage);
            }
        }
    }
}