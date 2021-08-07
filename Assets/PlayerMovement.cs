using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;

    float horizontal;
    float vertical;

    public float runSpeed = 350f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        jump();
    }
    void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * runSpeed * Time.deltaTime, rb.velocity.y);
        
    }


    public float jumpForce, jumpTime, airtime;
    float jumpTimeCounter, airtimecounter;
    bool presedonce;
    public bool itsgrounded, stoppedJumping;
    RaycastHit groundhit;
    void jump()
    {
        Debug.DrawRay(transform.position, Vector3.down * 1.3f, Color.blue,3);
        //grounded
        //if (Physics.Raycast(transform.position, Vector3.down*1.3f, out groundhit))
        //{

        //    if (groundhit.distance <= 1.2f)
        //    {
        //        itsgrounded = true;
        //    }
        //    else
        //    { itsgrounded = false; }
        //}
        //keydown
        if (Input.GetKeyDown(KeyCode.W) && itsgrounded == true)
        {
            airtimecounter = airtime;
            jumpTimeCounter = jumpTime;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            itsgrounded = false;
            stoppedJumping = false;
            presedonce = true;
        }

        if (Input.GetKey(KeyCode.W) && stoppedJumping == false)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                if (airtimecounter > 0)
                {
                    //rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce * ((airtimecounter / airtime) * 1.3f));
                    airtimecounter -= Time.deltaTime;
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            airtimecounter = airtime;
            jumpTimeCounter = jumpTime;
            stoppedJumping = true;
            presedonce = false;
        }
        if (Input.GetKey(KeyCode.W) == false)
        {
            airtimecounter = airtime;
            jumpTimeCounter = jumpTime;
            stoppedJumping = true;
            presedonce = false;
        }
    }

}
