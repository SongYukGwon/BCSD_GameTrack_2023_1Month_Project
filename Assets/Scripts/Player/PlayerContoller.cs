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
    [SerializeField]
    private GameObject boostEffect;
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

    Vector3 startPos;
    Vector3 endPos;

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
            if (Input.GetMouseButtonDown(0))
            {
                startPos = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                endPos = Input.mousePosition;
                CalcDirection(startPos, endPos);
            }

            //슬라이드 부분
            if (slideTime > 0)
            {
                slideTime -= Time.deltaTime;
            }
            else
            {
                colider[0].enabled = true;
                colider[1].enabled = false;
            }
            //CharacterMove(); // 캐릭터 좌우 인식
            //CharacterJump(); // 캐릭터 점프 인식
            //CharacterSlide(); // 캐릭터 슬라이드 인식
            CharacterScore(); // 캐릭터 점수 업데이트*/
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

    void CalcDirection(Vector2 s, Vector2 e)
    {
        float dy = (e.y - s.y);
        float dx = (e.x - s.x);
        float incline = dy / dx;
        if (Mathf.Abs(incline) > 1 && dy > 0) // 점프
        {
            if(!isJump)
            {
                footEffect.Stop();
                rigid.AddForce(Vector3.up * 20, ForceMode.Impulse);
                anim.SetTrigger("Jumped");
                isJump = true;
            }
        }
        else if (Mathf.Abs(incline) > 1 && dy < 0) // 슬라이딩
        {
            if (slideTime <= 0)
            {
                anim.SetTrigger("Slided");
                footEffect.Stop();
                rigid.AddForce(Vector3.down * 15, ForceMode.Impulse);
                slideTime = 0.8f;
                colider[0].enabled = false;
                colider[1].enabled = true;
            }
        }
        else if (Mathf.Abs(incline) < 1 && dx > 0 && routeIndex<2) // 오른쪽
        {
            routeIndex++;
        }
        else if (Mathf.Abs(incline) < 1 && dx < 0 && routeIndex>0) // 왼쪽
        {
            routeIndex--;
        }
    }

    //캐릭터 점수 및 속도 업데이트
    private void CharacterScore()
    {
        zPos += speed * Time.deltaTime;
        gameManager.UpdateScoreText((int)(zPos*10));
        speed = basicSpeed + zPos / 400;
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
            rigid.AddForce(Vector3.up * 20, ForceMode.Impulse);
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
            rigid.AddForce(Vector3.down * 15, ForceMode.Impulse);
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
            if (!isDead && slideTime <= 0)
            {
                footEffect.Play();
                anim.Play("RunForward");
            }
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
        gameManager.UpdateCoinData();
        gameManager.UpdateScoreData();
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
                    gameObject.layer = 0;
                    break;
                }
            case PlayerState.Dead:
                {
                    isDead = true;
                    isInvincible = false;
                    gameObject.layer = 0;
                    break;
                }
            case PlayerState.Invincible:
                {
                    isDead = false;
                    isInvincible = true;
                    gameObject.layer = 8; // invincible layer
                    break;
                }
            default:
                break;
        }
    }

    public void BoostEffectSwitch(bool isOn)
    {
        if(isOn) { boostEffect.SetActive(true); }
        else { boostEffect.SetActive(false); }
    }
}
