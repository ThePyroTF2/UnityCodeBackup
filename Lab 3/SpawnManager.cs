using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnManager : MonoBehaviour
{
    public GameObject player;
    bool isSpawning = false;
    public GameObject[] obstacles;
    public GameObject groundOutline;
    public float obstacleXRange = 10;
    int obstacleIndex;
    public TextMeshPro scoreCounter;
    int score = 0;
    BoxCollider[] obstacleHitCollider;

    public void AddToScore(int increase)
    {
        score += increase;
    }

    void SpawnObstacle()
    {
        Vector3 spawnScale = new Vector3(obstacles[obstacleIndex].transform.localScale.x, Random.Range(PlayerPrefs.GetFloat("ObstacleLowerHeightLimit"), PlayerPrefs.GetFloat("ObstacleUpperHeightLimit")), Random.Range(0.25f , 3));
        Vector3 spawnPos = new Vector3(Random.Range(-obstacleXRange / 2, obstacleXRange), 0, 10);
        obstacleIndex = Random.Range(0, obstacles.Length);
        obstacleHitCollider = obstacles[obstacleIndex].GetComponents<BoxCollider>();
        obstacles[obstacleIndex].transform.localScale = spawnScale;
        obstacleHitCollider[1].size = obstacles[obstacleIndex].transform.localScale;
        obstacleHitCollider[1].center = new Vector3(obstacles[obstacleIndex].transform.position.x, obstacles[obstacleIndex].transform.localScale.y / 2, obstacles[obstacleIndex].transform.position.z);
        Instantiate(obstacles[obstacleIndex], spawnPos, obstacles[obstacleIndex].transform.rotation);
        isSpawning = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(player, player.transform.position, player.transform.rotation);
        Instantiate(groundOutline, groundOutline.transform.position, player.transform.rotation);
        obstacles[0].transform.localScale = new Vector3(0.25f, 0.25f, 3);
        PlayerPrefs.SetFloat("ObstacleLowerHeightLimit", 0.25f);
        PlayerPrefs.SetFloat("ObstacleUpperHeightLimit", 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        switch(PlayerPrefs.GetInt("GameOver"))
        {
            case 0:
                if(isSpawning == false)
                {
                    Invoke("SpawnObstacle", 2);
                    isSpawning = true;
                }
                scoreCounter.SetText($"Score: {score}");
                break;
            case 1:
                break;
        }

        if(Input.GetKeyDown(KeyCode.R) == true)
        {
            PlayerPrefs.SetInt("GameOver", 0);
            Instantiate(player, player.transform.position, player.transform.rotation);
            Instantiate(groundOutline, player.transform.position, player.transform.rotation);
        }
    }
}