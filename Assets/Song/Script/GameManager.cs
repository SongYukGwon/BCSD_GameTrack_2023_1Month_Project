using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    [SerializeField]
    private TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    public void SeeDeadMenu()
    {
        DeadMenu.SetActive(true);
    }

    public void UpdateScoreText(int text)
    {
        scoreText.text = text.ToString();
    }

}