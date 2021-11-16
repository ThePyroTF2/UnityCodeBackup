using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Lab3
{
    public class PlayerController : MonoBehaviour
    {
        public KeyCode jumpKey;
        public float jumpForce = 10f;
        Rigidbody rb;
        int jumpCounter = 0;
        public int jumpLimit = 2;
        float horizontalInput;
        public float speed = 4f;
        public float horizontalLimit = 10;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            BetterPlayerPrefs.SetBool("GameOver", false);
            BetterPlayerPrefs.SetInt("ObstacleNumber", 0);
            BetterPlayerPrefs.SetFloat("PlayerHeight", 0);
            
        }

        // Update is called once per frame
        void Update()
        {
            horizontalInput = Input.GetAxis("Horizontal");
            switch(BetterPlayerPrefs.GetBool("GameRunning"))
            {
                case true:
                    switch(BetterPlayerPrefs.GetBool("GameOver"))
                    {
                        case false:
                            if(Input.GetKeyDown(jumpKey) == true &&  jumpCounter <= jumpLimit - 1)
                            {
                                jumpCounter++;
                                if(jumpCounter == 0)
                                {
                                    rb.AddForce(Vector3.up * jumpForce);
                                }
                                else
                                {
                                    //1/1.5 the upward force on the second jump, but with the current downward velocity added so it isn't negated by the falling
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

                            transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);

                            if(transform.position.x > horizontalLimit)
                            {
                                transform.position = new Vector3(horizontalLimit, transform.position.y, transform.position.z);
                            }
                            if(transform.position.x < -horizontalLimit / 2)
                            {
                                transform.position = new Vector3(-horizontalLimit / 2, transform.position.y, transform.position.z);
                            }
                            if(Input.GetKeyDown(KeyCode.S) == true)
                            {
                                transform.position = new Vector3(transform.position.x, 0.6f, transform.position.z);
                            }

                            BetterPlayerPrefs.SetFloat("PlayerHeight", transform.position.y);

                            if(BetterPlayerPrefs.GetInt("ObstacleNumber") % 5 == 0 && BetterPlayerPrefs.GetInt("ObstacleNumber") != 0 && BetterPlayerPrefs.GetInt("ObstacleNumber") / 5 != jumpLimit - 2)
                            {
                                jumpLimit++;
                                BetterPlayerPrefs.PlusPlusFloat("ObstacleLowerHeightLimit");
                                BetterPlayerPrefs.PlusPlusFloat("ObstacleUpperHightLimit");
                            }
                            
                            if(Input.GetKeyDown(KeyCode.R) == true)
                            {
                                Destroy(gameObject);
                            }
                            break;
                        case true:
                            break;
                    }
                    break;
                default:
                    break;
            }
        }


        void OnCollisionEnter(Collision collision)
        {
            jumpCounter = 0;
        }
    }
}