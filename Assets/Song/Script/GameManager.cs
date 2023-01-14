using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    static GameManager instance;
    public static GameManager GetInstaince() { return instance; }

    [SerializeField]
    private PlayerContoller playerContoller;

    // Start is called before the first frame update
    void Awake()
    {
        Init();
    }

    void Init()
    {
        if (instance == null)
        {

            GameObject go = GameObject.Find("GameManager");
            //없으면 생성
            if (go == null)
            {
                go = new GameObject { name = "GameManager" };
            }
            if (go.GetComponent<GameManager>() == null)
            {
                go.AddComponent<GameManager>();
            }
            //없어지지 않도록 해줌
            DontDestroyOnLoad(go);
            //instance 할당
            instance = go.GetComponent<GameManager>();
        }
    }
}