using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [SerializeField]
    private GameObject RankTab;

    public void GameStartBtn()
    {
        SoundManager.PlaySfx(SFX.BUTTON);
        Invoke("StartGame", .3f);
    }

    public void EngerShopButton()
    {
        SceneManager.LoadScene("Shop");
    }

    private void StartGame()
    {
        Debug.Log("Click");
        SceneManager.LoadScene("MainScene");
    }

    public void RankButton()
    {
        RankTab.SetActive(true);
        GameManager.GetInstaince().UpdateScore();
        RankingScript.GetInstaince().TextLoad();
    }
}
