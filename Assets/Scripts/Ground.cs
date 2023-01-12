using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public float groundLength;
    GameObject player;
    void Start()
    {
        player = GameObject.Find("Player");
        if(player == null)
        {
            Debug.Log("No Player");
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (player.transform.position.z > transform.position.z + groundLength)
            Destroy(gameObject);
    }
}
