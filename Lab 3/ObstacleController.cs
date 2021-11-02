using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerPrefs.GetInt("GameOver") == 0)
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime);
        }
        if(Input.GetKeyDown(KeyCode.R) == true)
        {
            Destroy(gameObject);
        }
        if(transform.position.z > -5)
        {
            Destroy(gameObject);
        }
    }
}