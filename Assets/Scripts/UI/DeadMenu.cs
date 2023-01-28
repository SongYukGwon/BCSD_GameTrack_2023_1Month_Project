using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//�׾��� �� ������ UI ��ũ��Ʈ
public class DeadMenu : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField]
    private GameObject menu;

    //ȭ�� ���-�����
    public void SeeDeadMenu()
    {
        menu.SetActive(true);
    }

    public void DownMenu()
    {
        menu.SetActive(false);
    }

    //�ٽý��� ��ư �Լ�
    public void ReStartBtn()
    {
        Invoke("GameRestart", 0.3f);
    }
    private void GameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //���� �޴��� ���� �Լ�
    public void MainMenuBtn()
    {
        Invoke("MainMenu", 0.3f);
    }

    private void MainMenu()
    {
        SceneManager.LoadScene("Title");    
    }
}
