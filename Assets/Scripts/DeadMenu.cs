using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadMenu : MonoBehaviour
{
    private GameManager gameManager;
    private GameObject menu;

    private void Start()
    {
        if (menu == null)
            menu = transform.Find("DeadMenu").gameObject;
    }

    public void SeeDeadMenu()
    {
        menu.SetActive(true);
    }

    public void DownMenu()
    {
        menu.SetActive(false);
    }

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
