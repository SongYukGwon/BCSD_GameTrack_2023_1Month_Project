using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    //스폰 캐릭터 모음
    [SerializeField]
    private GameObject[] characters;
    [SerializeField]
    private GameObject mapMaker;

    private int index;


    private void Start()
    {
        //사용자가 선택하고 있는 캐릭터 index를 받아와서 활성화
        PlayerData data = DataManager.GetInstance().LoadData();
        characters[data.character].SetActive(true);
        index = data.character;

        //MapMaker 생성
        mapMaker = Instantiate(mapMaker, characters[data.character].transform);
        mapMaker.transform.localPosition += new Vector3(0, 0, 40);
    }

    //선택된 캐릭터 보이기
    public void setCharacter(int num)
    {
        characters[index].SetActive(false);
        characters[num].SetActive(true);
        index = num;
    }

    //캐릭터 체인지 2
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
