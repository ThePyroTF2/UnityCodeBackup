using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounterController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.tag == "Score Counter")
        {
            transform.position = new Vector3(transform.position.x, BetterPlayerPrefs.GetFloat("CameraHeight") + 2, transform.position.z);
        }
        else if(gameObject.tag == "Game Over")
        {
            transform.position = new Vector3(transform.position.x, BetterPlayerPrefs.GetFloat("CameraHeight") - 2, transform.position.z);
        }
    }
}