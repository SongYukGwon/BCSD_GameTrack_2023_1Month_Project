using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//죽었을 때 나오는 UI 스크립트
public class DeadMenu : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField]
    private GameObject menu;

    //화면 출력-미출력
    public void SeeDeadMenu()
    {
        menu.SetActive(true);
    }

    public void DownMenu()
    {
        menu.SetActive(false);
    }

    //다시시작 버튼 함수
    public void ReStartBtn()
    {
        Invoke("GameRestart", 0.3f);
    }
    private void GameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //메인 메뉴로 가는 함수
    public void MainMenuBtn()
    {
        Invoke("MainMenu", 0.3f);
    }

    private void MainMenu()
    {
        SceneManager.LoadScene("Title");    
    }
}
