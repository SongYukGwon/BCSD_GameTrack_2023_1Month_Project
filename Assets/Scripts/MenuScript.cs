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
        PlayerData data = DataManager.GetInstance().LoadData();
        RankingScript.GetInstaince().OnClickTransactionSave(data.userId, data.highScore);
        RankingScript.GetInstaince().TextLoad();
    }

    public void RankBackButton()
    {
        RankTab.SetActive(false);
    }
}
