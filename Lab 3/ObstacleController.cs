using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    GameObject spawnManager;
    SpawnManager spawnManagerScript;
    Collider col;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetFloat("Speed", 2f);
        col = GetComponent<Collider>();
        spawnManager = GameObject.Find("SpawnManager");
        spawnManagerScript = spawnManager.GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerPrefs.GetInt("GameOver") == 0)
        {
            transform.Translate(Vector3.right * PlayerPrefs.GetFloat("Speed") * Time.deltaTime);
        }
        if(Input.GetKeyDown(KeyCode.R) == true)
        {
            Destroy(gameObject);
        }
        if(transform.position.z < -15)
        {
            Destroy(gameObject);
            if(PlayerPrefs.GetFloat("Speed") <= 5f)
            {
                PlayerPrefs.SetFloat("Speed", PlayerPrefs.GetFloat("Speed") + 0.1f);
                PlayerPrefs.SetInt("ObstacleNumber", PlayerPrefs.GetInt("ObstacleNumber") + 1);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            spawnManagerScript.AddToScore(1);
        }
    }

    void OnCollisionEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            PlayerPrefs.SetInt("GameOver", 1);
        }
    }
}