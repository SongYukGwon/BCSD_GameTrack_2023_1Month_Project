using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RankingUI : MonoBehaviour
{
    [SerializeField]
    private GameObject[] ranking;
    [SerializeField]
    private TextMeshProUGUI[] nickName;
    [SerializeField]
    private TextMeshProUGUI[] score;


    public void SetRanking(int i, string name, string s)
    {
        ranking[i].SetActive(true);
        nickName[i].text = name;
        score[i].text = s;
    }

}
