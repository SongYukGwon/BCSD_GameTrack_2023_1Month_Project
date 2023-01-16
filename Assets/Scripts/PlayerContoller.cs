using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContoller : MonoBehaviour
{
    private Rigidbody rigid;
    [SerializeField]
    public Collider[] colider; // 0:Idle 1:Slide
    public float basicSpeed;
    private float speed;
    private Animator anim;
    private GameManager gameManager;
    private int coin=0;
    private Vector3 moveVec;


    //ĳ���� ������ ����
    private int[] route = {-2,0,2};
    private int routeIndex;


    //ĳ���� ���� ����
    private bool isJump;
    private float slideTime;
    private float zPos;
    private bool isDead;

    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        routeIndex = 1;
        isJump = false;
        slideTime = 0;
        zPos = 0;
        isDead = false;
    }

    private void Start()
    {
        gameManager = GameManager.GetInstaince();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDead)
        {
            CharacterMove(); // ĳ���� �¿� �ν�
            CharacterJump(); // ĳ���� ���� �ν�
            CharacterSlide(); // ĳ���� �����̵� �ν�
            CharacterScore(); // ĳ���� ���� ������Ʈ
        }
    }

    //ĳ���� ������
    private void FixedUpdate()
    {
        Vector3 movement = Vector3.Lerp(transform.position, new Vector3(route[routeIndex], 0, transform.position.z+speed), Time.deltaTime*7f);
        //zPos += speed * Time.deltaTime;
        //Vector3 movement = Vector3.Lerp(route[routeIndex], 0, transform.position.z+speed);
        rigid.MovePosition(movement);
    }

    //ĳ���� ���� �� �ӵ� ������Ʈ
    private void CharacterScore()
    {
        zPos += speed * Time.deltaTime;
        gameManager.UpdateScoreText((int)(zPos*10));
        speed = basicSpeed + zPos / 500;
    }


    //ĳ���� ������
    void CharacterMove()
    {
        if (Input.GetKeyDown(KeyCode.A) && routeIndex > 0)
        {
            routeIndex--;
        }

        if(Input.GetKeyDown(KeyCode.D) && routeIndex < 2)
        {
            routeIndex++;
        }

    }

    void CharacterJump()
    {
        if (Input.GetKeyDown(KeyCode.W) && !isJump)
        {
            rigid.AddForce(Vector3.up * 10, ForceMode.Impulse);
            anim.SetTrigger("Jumped");
            isJump = true;
        }
    }

    void CharacterSlide()
    {
        if (Input.GetKeyDown(KeyCode.S) && slideTime<=0)
        {
            anim.SetTrigger("Slided");
            rigid.AddForce(Vector3.down * 5, ForceMode.Impulse);
            slideTime = 0.8f;
            colider[0].enabled = false;
            colider[1].enabled = true;
        }
        if (slideTime>0)
        {
            slideTime -= Time.deltaTime;
        }
        else if(!isJump)
        {
            colider[0].enabled = true;
            colider[1].enabled = false;
        }
    }

    //�÷��̾� �浹
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isJump = false;

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Dead();
        }
    }


    //ĳ���� �׾����� ����Ǵ� �Լ�
    private void Dead()
    {
        //ĳ���� ���� �ʿ�
        speed = 0;
        isDead = true;
        coin += gameManager.UpdateCoin();
        gameManager.SeeDeadMenu();
        anim.SetTrigger("Dead");
    }
}
