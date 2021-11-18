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
    public TextMeshPro gameOver;
    public int quitCountDown = 3;
    public bool quitting = false;
    public bool displayingQuit = false;

    public void AddToScore(int increase)
    {
        score += increase;
    }

    void Quit()
    {
        if(Input.GetKey(KeyCode.Escape) == true)
        {
            Application.Quit();
        }
    }

    void DisplayQuit()
    {
        gameOver.color = Color.black;
        gameOver.SetText($"Quitting... {quitCountDown}");
        if(quitCountDown != 3)
        {
            quitting = false;
        }
        quitCountDown--;
        displayingQuit = false;
    }

    void SpawnObstacle()
    {
        Vector3 spawnScale = new Vector3(obstacles[obstacleIndex].transform.localScale.x, Random.Range(BetterPlayerPrefs.GetFloat("ObstacleLowerHeightLimit"), BetterPlayerPrefs.GetFloat("ObstacleUpperHeightLimit")), Random.Range(0.25f , 3));
        Vector3 spawnPos = new Vector3(Random.Range(-obstacleXRange / 2, obstacleXRange), 0, 10);
        obstacleIndex = Random.Range(0, obstacles.Length);
        obstacleHitCollider = obstacles[obstacleIndex].GetComponents<BoxCollider>();
        obstacles[obstacleIndex].transform.localScale = spawnScale;
        obstacleHitCollider[1].size = new Vector3(2.4f, 4, 5);
        obstacleHitCollider[1].center = new Vector3(1.1f, 2, 0);
        Instantiate(obstacles[obstacleIndex], spawnPos, obstacles[obstacleIndex].transform.rotation);
        isSpawning = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(player, player.transform.position, player.transform.rotation);
        Instantiate(groundOutline, groundOutline.transform.position, player.transform.rotation);
        obstacles[0].transform.localScale = new Vector3(0.25f, 0.25f, 3);
        BetterPlayerPrefs.SetFloat("ObstacleLowerHeightLimit", 0.25f);
        BetterPlayerPrefs.SetFloat("ObstacleUpperHeightLimit", 1.5f);
        BetterPlayerPrefs.SetBool("GameRunning", false);
    }

    // Update is called once per frame
    void Update()
    {
        switch(BetterPlayerPrefs.GetBool("GameRunning"))
        {
            case true:
                switch(BetterPlayerPrefs.GetBool("GameOver"))
                {
                    case false:
                        if(isSpawning == false)
                        {
                            Invoke("SpawnObstacle", 2);
                            isSpawning = true;
                        }
                        scoreCounter.SetText($"Score: {score}");
                        if(Input.GetKey(KeyCode.Escape) == false)
                        {
                            gameOver.SetText("");
                        }
                        if(score > BetterPlayerPrefs.GetInt("HighScore"))
                        {
                            BetterPlayerPrefs.SetInt("HighScore", score);
                        }
                        break;
                    case true:
                        if(Input.GetKey(KeyCode.Escape) == false)
                        {
                            gameOver.color = Color.red;
                            gameOver.SetText($"Game over!\nPress \"R\" to restart\n High Score: {BetterPlayerPrefs.GetInt("HighScore")}\nPress Ctrl+D to reset high score");
                        }
                        break;
                }

                if(Input.GetKeyDown(KeyCode.R) == true)
                {
                    BetterPlayerPrefs.SetBool("GameOver", false);
                    Instantiate(player, player.transform.position, player.transform.rotation);
                    Instantiate(groundOutline, groundOutline.transform.position, player.transform.rotation);
                    BetterPlayerPrefs.SetFloat("ObstacleLowerHeightLimit", 0.25f);
                    BetterPlayerPrefs.SetFloat("ObstacleUpperHeightLimit", 1.5f);
                    score = 0;
                }
                if(Input.GetKey(KeyCode.LeftControl) == true)
                {
                    if(Input.GetKeyDown(KeyCode.D) == true)
                    {
                        BetterPlayerPrefs.SetInt("HighScore", 0);
                    }
                }
                break;
            default:
                if(Input.GetKey(KeyCode.Escape) == false)
                {
                    gameOver.color = Color.black;
                    gameOver.SetText("To the Skies!\nPress Space to begin");
                    if(Input.GetKeyDown(KeyCode.Space) == true)
                    {
                        BetterPlayerPrefs.SetBool("GameRunning", true);
                    }
                }
                break;
        }
        if(Input.GetKeyDown(KeyCode.Escape) == true)
        {
            Invoke("Quit", 3);
        }
        if(Input.GetKey(KeyCode.Escape) == true && quitting == false)
        {
            if(displayingQuit == false)
            {
                if(quitCountDown == 3)
                {
                    DisplayQuit();
                }
                Invoke("DisplayQuit", 1);
                displayingQuit = true;
            }
            quitting = true;
        }
        if(Input.GetKey(KeyCode.Escape) == false)
        {
            quitCountDown = 3;
            quitting = false;
        }
    }
}