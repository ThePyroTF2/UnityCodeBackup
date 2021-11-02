using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public float rowNumber = 1;
    public bool moveRight = true;
    Animation anim;
    AudioSource source;

    void Restart()
    {
        PlayerPrefs.SetInt("Restart", 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.tag == "Raider")
        {
            speed = 5f;
        }
        else if (gameObject.tag == "Ghoul")
        {
            speed = 8f;
        }
        if (gameObject.tag == "Ghoul")
        {
            anim = GetComponent<Animation>();
        }

        PlayerPrefs.SetInt("Restart", 0);

        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (PlayerPrefs.GetInt("GameOver"))
        {
            case 0:
                if (transform.position.x > 16)
                {
                    moveRight = false;
                    rowNumber++;
                    transform.Translate(0, 0, 0.5f);
                    speed = speed + (rowNumber / 5);
                }
                if (transform.position.x < -16)
                {
                    moveRight = true;
                    rowNumber++;
                    transform.Translate(0, 0, 0.5f);
                    speed = speed + (rowNumber / 5);
                }

                switch (moveRight)
                {
                    case true:
                        transform.Translate(Vector3.left * Time.deltaTime * speed);
                        break;
                    case false:
                        transform.Translate(Vector3.right * Time.deltaTime * speed);
                        break;
                }
                break;
            case 1:
                if (gameObject.tag == "Ghoul")
                {
                    anim.Stop();
                }
                break;
        }
        
        if (PlayerPrefs.GetInt("Restart") == 1)
        {
            Destroy(gameObject);
            Invoke("Restart", 0.5f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerPrefs.SetInt("GameOver", 1);
            Destroy(other);
        }

        if (other.tag == "Boolet")
        {
            source.Play();
        }
    }
}