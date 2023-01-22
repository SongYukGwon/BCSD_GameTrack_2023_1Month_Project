using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState { Running, Dead, Invincible }

public class PlayerContoller : MonoBehaviour
{
    private Rigidbody rigid;
    [SerializeField]
    public Collider[] colider; // 0:Idle 1:Slide
    [SerializeField]
    private ParticleSystem footEffect;
    [SerializeField]
    private ParticleSystem hitEffect;
    public float basicSpeed;
    private float speed;
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
    private bool isInvincible;
    private int coin;

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
        isInvincible = false;
    }

    private void Start()
    {
        SoundManager.PlayBgm(BGM.INGAME);
        gameManager = GameManager.GetInstaince();
        coin = DataManager.GetInstance().LoadData().coin;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDead)
        {
            CharacterMove(); // 캐릭터 좌우 인식
            CharacterJump(); // 캐릭터 점프 인식
            CharacterSlide(); // 캐릭터 슬라이드 인식
            CharacterScore(); // 캐릭터 점수 업데이트
        }
    }

    //캐릭터 움직임
    private void FixedUpdate()
    {
        Vector3 movement = Vector3.Lerp(transform.position, new Vector3(route[routeIndex], 0, transform.position.z+speed), Time.deltaTime*7f);
        //zPos += speed * Time.deltaTime;
        //Vector3 movement = Vector3.Lerp(route[routeIndex], 0, transform.position.z+speed);
        rigid.MovePosition(movement);
    }

    //캐릭터 점수 및 속도 업데이트
    private void CharacterScore()
    {
        zPos += speed * Time.deltaTime;
        gameManager.UpdateScoreText((int)(zPos*10));
        speed = basicSpeed + zPos / 500;
    }


    //캐릭터 움직임
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
            footEffect.Stop();
            rigid.AddForce(Vector3.up * 25, ForceMode.Impulse);
            anim.SetTrigger("Jumped");
            isJump = true;
        }
    }

    void CharacterSlide()
    {
        if (Input.GetKeyDown(KeyCode.S) && slideTime<=0)
        {
            anim.SetTrigger("Slided");
            footEffect.Stop();
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

    //플레이어 충돌
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJump = false;
            if(!isDead && slideTime<=0)
                footEffect.Play();
        }

        if (collision.gameObject.CompareTag("Obstacle") && !isInvincible)
        {
            Dead();
        }
    }


    //캐릭터 죽었을때 실행되는 함수
    private void Dead()
    {
        SoundManager.StopBgm();
        SoundManager.PlaySfx(SFX.DEAD);

        gameObject.layer = 3;
        hitEffect.Play();
        footEffect.Stop();
        speed = 0;
        isDead = true;
        gameManager.UpdateCoin();
        gameManager.SeeDeadMenu();
        anim.SetTrigger("Dead");
    }

    public void ChangePlayerState(PlayerState state)
    {
        switch (state)
        {
            case PlayerState.Running:
                {
                    isDead = false;
                    isInvincible = false;
                    break;
                }
            case PlayerState.Dead:
                {
                    isDead = true;
                    isInvincible = false;
                    break;
                }
            case PlayerState.Invincible:
                {
                    isDead = false;
                    isInvincible = true;
                    break;
                }
            default:
                break;
        }
    }
}
