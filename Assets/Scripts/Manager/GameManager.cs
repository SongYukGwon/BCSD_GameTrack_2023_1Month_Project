using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GameManager : MonoBehaviour, ISingleton
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

    //최고점수 달성 처리 함수
    public void UpdateScoreData()
    {
        PlayerData data = DataManager.GetInstance().LoadData();
        if (data.highScore < Int32.Parse(scoreText.text))
        {
            data.highScore = Int32.Parse(scoreText.text);
            RankingScript.GetInstaince().OnClickTransactionSave(data.userId, data.highScore);
            DataManager.GetInstance().SaveData(data);
        }
    }

    //플레이어 죽었을때 함수 보이기.
    public void SeeDeadMenu()
    {
        if(canvas == null)
            canvas = GameObject.FindGameObjectWithTag("UI");
        canvas.GetComponent<DeadMenu>().SeeDeadMenu();
    }

    //플레이중 score UI갱신 함수
    public void UpdateScoreText(int text)
    {
        if (scoreText == null)
        {
            GameObject obj = GameObject.Find("Number");
            scoreText = obj.GetComponent<TextMeshProUGUI>();
        }
        scoreText.text = text.ToString();
    }

    //코인 획득 함수 (UI및 현황 업데이트)
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

    //플레이어 소지 코인 업데이트
    public void UpdateCoinData()
    {
        PlayerData data = DataManager.GetInstance().LoadData();
        data.coin += getCoin;
        getCoin = 0;
        DataManager.GetInstance().SaveData(data);
    }



}