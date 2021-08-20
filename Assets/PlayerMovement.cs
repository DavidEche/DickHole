using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;

    float horizontal;
    float vertical;

    public float runSpeed = 350f;

    PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if(view.IsMine){
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");
        }
    }
    void FixedUpdate()
    {
        if(view.IsMine){
            rb.velocity = new Vector2(horizontal * runSpeed * Time.deltaTime, rb.velocity.y);
            jump();
        }
    }


    public float jumpForce, jumpTime, airtime;
    float jumpTimeCounter, airtimecounter;
    bool presedonce;
    public bool itsgrounded, stoppedJumping;
    RaycastHit groundhit;
    void jump()
    {
        Debug.DrawRay(transform.position, Vector3.down, Color.blue,1);
        //grounded
        if (Physics.Raycast(transform.position, Vector3.down, out groundhit))
        {

            if (groundhit.distance <= 1.1f)
            {
                print(groundhit.collider.name);
                itsgrounded = true;
                if (presedonce == false)
                {
                    jumpTimeCounter = jumpTime;
                }
            }
            else
            { itsgrounded = false; }
        }
        //keydown
        if (Input.GetKeyDown(KeyCode.W) && itsgrounded == true)
        {

            airtimecounter = airtime;
            jumpTimeCounter = jumpTime;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
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
