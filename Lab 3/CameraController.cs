using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerPrefs.GetFloat("PlayerHeight") > 10f)
        {
            transform.position = new Vector3(transform.position.x, PlayerPrefs.GetFloat("PlayerHeight"), transform.position.z);
        }
        if(PlayerPrefs.GetFloat("PlayerHeight") < 10f)
        {
            transform.position = new Vector3(transform.position.x, 10, transform.position.z);
        }
    }
}