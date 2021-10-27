using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public clas SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject player;
    public float spawnXRange = 12;
    public float spawnPosZ = 8;
    public float spawnInterval;
    bool isSpawning = false;
    public TextMeshProUGUI scoreCounter;
    public TextMeshProUGUI gameOver;
    int score;

    void SpawnRandom()
    {
        int enemyIndex = Random.Range(0, enemyPrefabs.Length);
        Vector3 spawnPos = new Vector3(Random.Range(-spawnXRange, spawnXRange), 0, spawnPosZ);
        Instantiate(enemyPrefabs[enemyIndex], spawnPos, enemyPrefabs[enemyIndex].transform.rotation);
        isSpawning = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(player, player.transform.position, player.transform.rotation);
        PlayerPrefs.SetInt("GameOver", 0);
        PlayerPrefs.SetInt("Score", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("GameOver") == 0)
        {
            if (isSpawning == false)
            {
                spawnInterval = Random.Range(1, 1.5f);
                Invoke("SpawnRandom", spawnInterval);
                isSpawning = true;
            }
        }

        score = PlayerPrefs.GetInt("Score");

        scoreConter.SetText("Score:\n" + score);

        if (PlayerPrefs.GetInt("GameOver") == 1)
        {
            gameOver.SetText("GameOver!\nPress \"R\" to restart\n");
        }
    }
}
