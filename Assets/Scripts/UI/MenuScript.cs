using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//메인메뉴 UI이벤트 스크립트
public class MenuScript : MonoBehaviour
{
    [SerializeField]
    private GameObject RankTab;

    //게임 시작 버튼 이벤트 함수
    public void GameStartBtn()
    {
        SoundManager.PlaySfx(SFX.BUTTON);
        Invoke("StartGame", .3f);
    }
    //상점 버튼 이벤트 함수
    public void EngerShopButton()
    {
        SceneManager.LoadScene("Shop");
    }
    //게임시작 버튼 이벤트 함수
    private void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }
    //랭크 버튼 이벤트 함수
    public void RankButton()
    {
        RankTab.SetActive(true);
        RankingScript.GetInstaince().TextLoad();
    }
    //랭크 Back 버튼 이벤트 함수
    public void RankBackButton()
    {
        RankTab.SetActive(false);
    }
}
