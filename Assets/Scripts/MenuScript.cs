using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
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
        SceneManager.LoadScene(1);
    }
}
