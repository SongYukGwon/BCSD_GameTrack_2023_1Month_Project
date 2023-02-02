using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public Vector3 startPos;
    public GameObject[] grounds;
    public float groundLength;

    Vector3 currentGroundPos;

    void Start()
    {
        currentGroundPos = startPos;
    }

    void Update()
    {
        if (transform.position.z > currentGroundPos.z)
        {
            SpawnGround();
        }
    }

    void SpawnGround() // ����Ʈ���� �������� Ground�� ������
    {
        int index = Random.Range(0, grounds.Length);
        Vector3 newPosition = new Vector3(currentGroundPos.x, currentGroundPos.y,
                currentGroundPos.z + groundLength);
        Instantiate(grounds[index], newPosition, transform.rotation);
        currentGroundPos = newPosition;
    }
}
