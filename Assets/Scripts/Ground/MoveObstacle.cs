using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//�����̴� ��ֹ� ��ũ��Ʈ
public class MoveObstacle : MonoBehaviour
{
    private bool isMoving;

    //���� ���� 1:�� 2:�� 3:�� 4:��
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

    //Ʈ���ſ��� �����̰��ϴ� �Լ�
    public void StartMoving(float plusSpeed)
    {
        orginPos = transform.position;
        speed = speed* plusSpeed;
        isMoving = true;
    }



    //�����̴� ���� ����
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
