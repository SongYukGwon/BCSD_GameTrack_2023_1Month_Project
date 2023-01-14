using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    static GameManager instance;
    public static GameManager GetInstaince() {
        return instance; 
    }

    [SerializeField]
    private PlayerContoller playerContoller;

    [SerializeField]
    private GameObject DeadMenu; 

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public void SeeDeadMenu()
    {
        DeadMenu.SetActive(true);
    }
}