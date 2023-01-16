using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScript : MonoBehaviour
{
    public void GameStartBtn()
    {
        Debug.Log("게임시작");
        Invoke("StartGame", .3f);
    }

    private void StartGame()
    {
        SceneManager.LoadScene("MainScean");
    }
}
