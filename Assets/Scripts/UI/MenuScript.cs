using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//���θ޴� UI�̺�Ʈ ��ũ��Ʈ
public class MenuScript : MonoBehaviour
{
    [SerializeField]
    private GameObject RankTab;

    //���� ���� ��ư �̺�Ʈ �Լ�
    public void GameStartBtn()
    {
        SoundManager.PlaySfx(SFX.BUTTON);
        Invoke("StartGame", .3f);
    }
    //���� ��ư �̺�Ʈ �Լ�
    public void EngerShopButton()
    {
        SceneManager.LoadScene("Shop");
    }
    //���ӽ��� ��ư �̺�Ʈ �Լ�
    private void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }
    //��ũ ��ư �̺�Ʈ �Լ�
    public void RankButton()
    {
        RankTab.SetActive(true);
        RankingScript.GetInstaince().TextLoad();
    }
    //��ũ Back ��ư �̺�Ʈ �Լ�
    public void RankBackButton()
    {
        RankTab.SetActive(false);
    }
}
