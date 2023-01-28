using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�÷��̾� ������ Json ����
public class PlayerData
{
    public string userId; // userId(NickName)
    public int coin; // ���� ����
    public int highScore; // �ְ� ����
    public int character; // ���� ���� ĳ���� ����
    public List<bool> characterStatus; //ĳ���� ���� ��Ȳ ����
    public List<int> itemLevel; // ������ ���� ��Ȳ ����

    public void printData()
    {
        Debug.Log(coin);
        Debug.Log(highScore);
    }
}
