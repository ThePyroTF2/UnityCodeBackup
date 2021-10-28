using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 15f;
    public float horizontalInput;
    public float horizontalLimit = 16f;
    public GameObject boolet;
    bool isShooting = false;
    AudioSource source;

    void ShootBoolet()
    {
        isShooting = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(PlayerPrefs.GetInt("GameRunning"))
        {
            case 1:    
                horizontalInput = Input.GetAxis("Horizontal");

                transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);

                if (transform.position.x < -horizontalLimit)
                {
                    transform.position = new Vector3(horizontalLimit * -1, transform.position.y, transform.position.z);
                }
                if (transform.position.x > horizontalLimit)
                {
                    transform.position = new Vector3(horizontalLimit, transform.position.y, transform.position.z);
                }

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (isShooting == false)
                    {
                        Instantiate(boolet, new Vector3(transform.position.x, boolet.transform.position.y, transform.position.z), transform.rotation);
                        source.pitch = Random.Range(0.75f, 1.25f);
                        source.Play(0);
                        Invoke("ShootBoolet", 0.5f);
                        isShooting = true;
                    }
                }

                if (Input.GetKeyDown(KeyCode.R))
                {
                    Destroy(gameObject);
                }
                break;
            default:
                break;
        }
    }
}