using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    GameObject spawnManager;
    SpawnManager spawnManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        BetterPlayerPrefs.SetFloat("Speed", 2f);
        spawnManager = GameObject.Find("SpawnManager");
        spawnManagerScript = spawnManager.GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(BetterPlayerPrefs.GetBool("GameOver") == false)
        {
            transform.Translate(Vector3.right * BetterPlayerPrefs.GetFloat("Speed") * Time.deltaTime);
        }
        if(Input.GetKeyDown(KeyCode.R) == true)
        {
            Destroy(gameObject);
        }
        if(transform.position.z < -15)
        {
            Destroy(gameObject);
            if(BetterPlayerPrefs.GetFloat("Speed") <= 5f)
            {
                BetterPlayerPrefs.PlusEqualsFloat("Speed", 0.1f);
                BetterPlayerPrefs.PlusPlusInt("ObstacleNumber");
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

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            BetterPlayerPrefs.SetBool("GameOver", true);
        }
    }
}