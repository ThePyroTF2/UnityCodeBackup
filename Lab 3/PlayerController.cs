using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public KeyCode jumpKey;
    public float jumpForce = 10f;
    Rigidbody rb;
    int jumpCounter = 0;
    public int jumpLimit = 2;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        PlayerPrefs.SetInt("GameOver", 0);
    }

    // Update is called once per frame
    void Update()
    {
        switch(PlayerPrefs.GetInt("GameOver"))
        {
            case 0:
                if(Input.GetKeyDown(jumpKey) == true &&  jumpCounter <= jumpLimit - 1)
                {
                    jumpCounter++;
                    if(jumpCounter == 0)
                    {
                        rb.AddForce(Vector3.up * jumpForce);
                    }
                    else
                    {
                        //Half the upward force on the second jump, but with the current downward velocity added so it isn't negated by the falling
                        //Also if it's going up the velocity shouldn't be added so that is accounted for
                        if(rb.velocity.y <= 0)
                        {
                            rb.AddForce(Vector3.up * ((jumpForce / 1.5f) + Math.Abs(rb.velocity.y)), ForceMode.Impulse);
                        }
                        else
                        {
                            rb.AddForce(Vector3.up * (jumpForce / 1.5f), ForceMode.Impulse);
                        }
                    }
                }
                break;
            case 1:
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ground")
        {
            jumpCounter = 0;
        }
        if(other.tag == "Obstacle")
        {
            PlayerPrefs.SetInt("GameOver", 1);
        }
    }
}