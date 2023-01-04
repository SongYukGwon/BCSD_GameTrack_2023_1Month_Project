using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public Vector3 startPos;
    public GameObject ground;
    public float groundLength;

    //int groundCount = 1;
    Vector3 currentGroundPos;

    void Start()
    {
        currentGroundPos = startPos;
    }

    void Update()
    {
        if (transform.position.z > currentGroundPos.z)
        {
            Vector3 newPosition = new Vector3(currentGroundPos.x, currentGroundPos.y,
                currentGroundPos.z + groundLength);
            Instantiate(ground, newPosition, transform.rotation);
            currentGroundPos = newPosition;
            //groundCount++;
            //Debug.Log(groundCount);
        }
    }
}
