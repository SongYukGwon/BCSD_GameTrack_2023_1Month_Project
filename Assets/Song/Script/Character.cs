using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Rigidbody rigid;
    private Collider colider;


    private int[] route = {-2,0,2};
    private bool change;
    private int routeIndex;
    private float value;

    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        routeIndex = 1;
    }

    // Update is called once per frame
    void Update()
    {
        CharacterMove();
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(route[routeIndex], 0, 0), 0.05f);
    }

    //캐릭터 움직임 입력
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

    
}
