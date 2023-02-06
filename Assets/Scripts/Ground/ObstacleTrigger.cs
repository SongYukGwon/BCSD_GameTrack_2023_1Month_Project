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
            float speed = other.GetComponent<PlayerContoller>().getSpeedNumber();
            gameObject.GetComponentInParent<MoveObstacle>().StartMoving(speed);
        }
    }
}
