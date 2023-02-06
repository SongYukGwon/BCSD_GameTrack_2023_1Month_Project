using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//움직이는 장애물 스크립트
public class MoveObstacle : MonoBehaviour
{
    private bool isMoving;

    //방향 변수 1:앞 2:좌 3:우 4:뒤
    public int direction;
    private Vector3 dir;
    private Vector3 orginPos;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        isMoving = false;
        setDirection();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        if (isMoving)
            transform.position += dir * speed;
    }

    //트리거에서 움직이게하는 함수
    public void StartMoving(float plusSpeed)
    {
        orginPos = transform.position;
        speed = speed* plusSpeed;
        isMoving = true;
    }



    //움직이는 방향 설정
    void setDirection()
    {
        switch (direction)
        {
            case 1:
                dir = Vector3.forward;
                break;
            case 2:
                dir = Vector3.left;
                break;
            case 3:
                dir = Vector3.right;
                break;
            case 4:
                dir = Vector3.back;
                break;
            default:
                dir = Vector3.zero;
                break;
        }
    }

}
