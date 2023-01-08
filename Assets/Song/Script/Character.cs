using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Rigidbody rigid;
    private Collider colider;
    private Animator anim;


    //캐릭터 움직임 변수
    private int[] route = {-2,0,2};
    private int routeIndex;

    //캐릭터 점프 변수
    private bool isJump;

    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        routeIndex = 1;
        isJump = false;
    }

    // Update is called once per frame
    void Update()
    {
        CharacterMove();
        CharacterJump();
        CharacterSlide();
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(route[routeIndex], 0, 0), 0.05f);
    
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
        if (Input.GetKeyDown(KeyCode.S))
        {
            anim.SetTrigger("Slided");
            Debug.Log("123");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isJump = false;
    }
}
