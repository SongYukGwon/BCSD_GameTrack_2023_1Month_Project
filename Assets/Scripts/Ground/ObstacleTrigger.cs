using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��ֹ� �����̱� ���� �Լ�
public class ObstacleTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            gameObject.GetComponentInParent<MoveObstacle>().StartMoving();
        }
    }
}
