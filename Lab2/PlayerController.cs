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

    void ShootBoolet()
    {
        isShooting = false;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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
                Invoke("ShootBoolet", 0.5f);
                isShooting = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Destroy(gameObject);
        }
    }
}