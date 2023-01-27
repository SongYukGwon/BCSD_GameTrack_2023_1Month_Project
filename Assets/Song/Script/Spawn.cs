using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    //���� ĳ���� ����
    [SerializeField]
    private GameObject[] characters;

    private int index;


    private void Start()
    {
        //����ڰ� �����ϰ� �ִ� ĳ���� index�� �޾ƿͼ� Ȱ��ȭ
        PlayerData data = DataManager.GetInstance().LoadData();
        characters[data.character].SetActive(true);
        index = data.character;
    }

    //���õ� ĳ���� ���̱�
    public void setCharacter(int num)
    {
        characters[index].SetActive(false);
        characters[num].SetActive(true);
        index = num;
    }

    //ĳ���� ü���� 2
    public void ChangeCharacter(int next)
    {
        int prevIndex = index;
        if (next == -1)
        {
            if (index == 0)
            {
                index = characters.Length - 1;
            }
            else
            {
                index--;
            }
        }
        else if (next == 1)
        {
            if (index == characters.Length - 1)
            {
                index = 0;
            }
            else
            {
                index++;
            }
        }
        characters[prevIndex].SetActive(false);
        characters[index].SetActive(true);
    }

    public int GetIndex()
    {
        return index;
    }
}
