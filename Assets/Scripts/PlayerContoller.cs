using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContoller : MonoBehaviour
{
    private Rigidbody rigid;
    [SerializeField]
    public Collider[] colider;
    public int speed;
    private Animator anim;
    private GameManager gameManager;

    private Vector3 moveVec;


    //캐릭터 움직임 변수
    private int[] route = {-2,0,2};
    private int routeIndex;


    //캐릭터 상태 변수
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

    // Update is called once per frame
    void Update()
    {
        if(!isDead)
        {
            CharacterMove();
            CharacterJump();
            CharacterSlide();
        }
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(route[routeIndex], 0, transform.position.z+speed), Time.deltaTime*7f);
        //zPos += speed * Time.deltaTime;
    }




    //캐릭터 움직임
    void CharacterMove()
    {
        if (Input.GetKeyDown(KeyCode.A) && routeIndex > 0)
            routeIndex--;
        if(Input.GetKeyDown(KeyCode.D) && routeIndex < 2)
            routeIndex++;
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
        else
        {
            colider[0].enabled = true;
            colider[1].enabled = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isJump = false;

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Dead();
        }
            
    }


    //캐릭터 죽었을때 실행되는 함수
    private void Dead()
    {
        //캐릭터 정산 필요
        speed = 0;
        isDead = true;
        gameManager = GameManager.GetInstaince();
        gameManager.SeeDeadMenu();
        
        anim.SetTrigger("Dead");
    }
}
