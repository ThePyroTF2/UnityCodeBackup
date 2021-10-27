using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject player;
    public float startDelay = 2f;
    public float spawnInterval = 1.5f;
    public float spawnXRange = 12;
    public float spawnPosZ = 8;
    bool isSpawning = false;
    public TextMeshProUGUI scoreCounter;
    public TextMeshProUGUI gameOver;

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
        PlayerPrefs.SetInt("Restart", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("GameOver") == 0)
        {
            if (isSpawning == false)
            {
                switch(PlayerPrefs.GetInt("Score"))
                {
                    case 0:
                        spawnInterval = Random.Range(1, 1.5f);
                        break;
                    case 1:
                        spawnInterval = Random.Range(0.9f, 1.4f);
                        break;
                    case 2:
                        spawnInterval = Random.Range(0.8f, 1.3f);
                        break;
                    case 3:
                        spawnInterval = Random.Range(0.7f, 1.2f);
                        break;
                    case 4:
                        spawnInterval = Random.Range(0.6f, 1.1f);
                        break;
                    case 5:
                        spawnInterval = Random.Range(0.5f, 1);
                        break;
                    case 6:
                        spawnInterval = Random.Range(0.5f, 0.9f);
                        break;
                    case 7:
                        spawnInterval = Random.Range(0.5f, 0.8f);
                        break;
                    case 8:
                        spawnInterval = Random.Range(0.5f, 0.7f);
                        break;
                    case 9:
                        spawnInterval = Random.Range(0.5f, 0.6f);
                        break;
                    default:
                        spawnInterval = 0.5f;
                        break;
                }
                Invoke("SpawnRandom", spawnInterval);
                isSpawning = true;
            }
        }

        scoreCounter.SetText("Score:\n" + PlayerPrefs.GetInt("Score"));

        if (PlayerPrefs.GetInt("GameOver") == 1)
        {
            if (PlayerPrefs.GetInt("Score") > PlayerPrefs.GetInt("HighScore"))
            {
                PlayerPrefs.SetInt("HighScore", PlayerPrefs.GetInt("Score"));
            }
            gameOver.SetText("Game Over!\nPress \"R\" to restart\nHigh score: " + PlayerPrefs.GetInt("HighScore") + "\nCtrl+D to reset high score\nEsc to quit");
        }

        if (Input.GetKeyDown(KeyCode.R)){
            PlayerPrefs.SetInt("Restart", 1);
            PlayerPrefs.SetInt("GameOver", 0);
            PlayerPrefs.SetInt("Score", 0);
            gameOver.SetText("");
            Instantiate(player, player.transform.position, player.transform.rotation);
        }

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.D))
        {
            PlayerPrefs.SetInt("HighScore", 0);
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}