using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundOutlineController : MonoBehaviour
{
    public float speed = 4f;
    float horizontalInput;
    public float horizontalLimit = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        switch(PlayerPrefs.GetInt("GameOver"))
        {
            case 0:
                transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);

                if(transform.position.x > horizontalLimit)
                {
                    transform.position = new Vector3(horizontalLimit, transform.position.y, transform.position.z);
                }
                if(transform.position.x < -horizontalLimit / 2)
                {
                    transform.position = new Vector3(-horizontalLimit / 2, transform.position.y, transform.position.z);
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
}