using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BooletController : MonoBehaviour
{
    public float speed = 30f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if (transform.position.z > 11)
        {
            Destroy(gameObject);
        }
    }   

    private void OnTriggerEnter(Collider other)
    {
        if (other.Tag == "Ghoul" || other.Tag == "Raider")
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
            PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + 1);
        }
    }
}