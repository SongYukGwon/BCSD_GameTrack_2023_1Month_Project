using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{

    static GameManager instance;
    public static GameManager GetInstaince() {
        return instance; 
    }

    GameObject canvas;
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private TextMeshProUGUI coinText;
    private int getCoin =0;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void UpdateScore()
    {
        PlayerData data = DataManager.GetInstance().LoadData();
        if (data.highScore < Int32.Parse(scoreText.text))
        {
            data.highScore = Int32.Parse(scoreText.text);
            DataManager.GetInstance().SaveData(data);
        }
    }

    public void SeeDeadMenu()
    {
        if(canvas == null)
            canvas = GameObject.FindGameObjectWithTag("UI");
        canvas.GetComponent<DeadMenu>().SeeDeadMenu();
    }

    public void UpdateScoreText(int text)
    {
        if (scoreText == null)
        {
            GameObject obj = GameObject.Find("Number");
            scoreText = obj.GetComponent<TextMeshProUGUI>();
        }
        scoreText.text = text.ToString();
    }

    public void PlusCoin(int coin)
    {
        if (coinText == null)
        {
            GameObject obj = GameObject.Find("CoinNumber");
            coinText = obj.GetComponent<TextMeshProUGUI>();
        }
        getCoin += coin;
        coinText.text = getCoin.ToString();
    }

    public void UpdateCoin()
    {
        PlayerData data = DataManager.GetInstance().LoadData();
        data.coin += getCoin;
        getCoin = 0;
        DataManager.GetInstance().SaveData(data);
    }



}