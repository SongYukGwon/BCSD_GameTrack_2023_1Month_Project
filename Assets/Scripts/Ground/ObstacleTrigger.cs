using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//장애물 움직이기 시작 함수
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
