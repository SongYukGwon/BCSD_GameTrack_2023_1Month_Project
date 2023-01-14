using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadMenu : MonoBehaviour
{
    private GameManager gameManager;

    public void ReStartBtn()
    {
        Invoke("GameRestart", 0.3f);
    }

    private void GameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenuBtn()
    {
        Invoke("MainMenu", 0.3f);
    }

    private void MainMenu()
    {
        SceneManager.LoadScene("Title");    
    }
}
