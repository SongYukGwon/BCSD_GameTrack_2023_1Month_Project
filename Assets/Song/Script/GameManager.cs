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
    [SerializeField]
    private TextMeshProUGUI coinText;
    private int getCoin =0;

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

    public void PlusCoin(int coin)
    {
        getCoin += coin;
        coinText.text = getCoin.ToString();
    }

    public int UpdateCoin()
    {
        return getCoin;
    }

}