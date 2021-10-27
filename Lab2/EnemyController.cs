using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyController : using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public float rowNumber = 1;
    public bool moveRight = true;
    Animation anim;

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.Tag == "Raider")
        {
            speed = 5f;
        }
        else if (gameObject.tag == "Ghoul")
        {
            speed = 8f;
            anim = GetComponent<Animation>();
        }
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
                    speed += rowNumber / 5;
                }
                if (transform.position.x < -16)
                {
                    moveRight = true;
                    rowNumber++;
                    transform.Translate(0, 0, 0.5f);
                    speed += rowNumber / 5
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.Tag == "Player")
        {
            PlayerPrefs.SetInt("GameOver", 1);
            Destroy(other);
        }
    }
}