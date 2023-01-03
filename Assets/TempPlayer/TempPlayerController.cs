using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPlayerController : MonoBehaviour
{
    public float speed;
    Vector3 leftPos = new Vector3(-2, 0, 0);
    Vector3 rightPos = new Vector3(2, 0, 0);

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            transform.position += leftPos;
        else if (Input.GetKeyDown(KeyCode.D))
            transform.position += rightPos;
        transform.position += Vector3.forward * speed * Time.deltaTime;
    }
}
