using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject[] items;
    void Start()
    {
        int index = Random.Range(0, items.Length);
        Instantiate(items[index], transform.position, transform.rotation);
    }
}
