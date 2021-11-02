using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject player;
    bool isSpawning = false;
    public GameObject[] obstacles;
    public float obstacleXRange = 8;
    int obstacleIndex;

    void SpawnObstacle()
    {
        Vector3 spawnPos = new Vector3(Random.Range(-obstacleXRange, obstacleXRange), 0, 10);
        obstacleIndex = Random.Range(0, obstacles.Length);
        Instantiate(obstacles[obstacleIndex], spawnPos, obstacles[obstacleIndex].transform.rotation);
        isSpawning = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(player, player.transform.position, player.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        switch(PlayerPrefs.GetInt("GameOver"))
        {
            case 0:
                if(isSpawning == false)
                {
                    Invoke("SpawnObstacle", 1);
                    isSpawning = true;
                }
                break;
            case 1:
                break;
        }

        if(Input.GetKeyDown(KeyCode.R) == true)
        {
            PlayerPrefs.SetInt("GameOver", 0);
        }
    }
}