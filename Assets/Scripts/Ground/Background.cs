using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] int moveDistance;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 currPos = transform.position;
            transform.position = new Vector3(currPos.x, currPos.y, currPos.z + moveDistance);
        }
    }
}
