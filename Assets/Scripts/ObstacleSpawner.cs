using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    GameObject player;
    public GameObject[] grounds;
    [SerializeField] float createDistance;
    int createTime;

    void Start()
    {
        player = GameObject.Find("Player");
        if (player == null)
        {
            Debug.Log("There is no player");
            Destroy(gameObject);
        }
    }

    void Update()
    {
        
    }
}
