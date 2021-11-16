using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//using BetterPlayerPrefs;

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

        public static class BetterPlayerPrefs
        {
            public static void PlusEqualsFloat(string prefName, float increase)
            {
                PlayerPrefs.SetFloat(prefName, PlayerPrefs.GetFloat(prefName) + increase);
            }
            public static void PlusEqualsInt(string prefName, int increase)
            {
                PlayerPrefs.SetInt(prefName, PlayerPrefs.GetInt(prefName) + increase);
            }
            public static void PlusPlusFloat(string prefName)
            {
                PlayerPrefs.SetFloat(prefName, PlayerPrefs.GetFloat(prefName) + 1);
            }
            public static void PlusPlusInt(string prefName)
            {
                PlayerPrefs.SetInt(prefName, PlayerPrefs.GetInt(prefName) + 1);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            PlayerPrefs.SetInt("GameOver", 0);
            PlayerPrefs.SetInt("ObstacleNumber", 0);
            PlayerPrefs.SetFloat("PlayerHeight", 0);
            
        }

        // Update is called once per frame
        void Update()
        {
            horizontalInput = Input.GetAxis("Horizontal");
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

                    PlayerPrefs.SetFloat("PlayerHeight", transform.position.y);

                    if(PlayerPrefs.GetInt("ObstacleNumber") % 5 == 0 && PlayerPrefs.GetInt("ObstacleNumber") != 0 && PlayerPrefs.GetInt("ObstacleNumber") / 5 != jumpLimit - 2)
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
                case 1:
                    break;
            }
        }


        void OnCollisionEnter(Collision collision)
        {
            jumpCounter = 0;
        }
        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Obstacle")
            {
                PlayerPrefs.SetInt("GameOver", 1);
            }
        }
    }
}