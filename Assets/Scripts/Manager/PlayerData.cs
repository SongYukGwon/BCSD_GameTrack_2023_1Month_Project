using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//플레이어 데이터 Json 형식
public class PlayerData
{
    public string userId; // userId(NickName)
    public int coin; // 소지 코인
    public int highScore; // 최고 점수
    public int character; // 현재 장착 캐릭터 변수
    public List<bool> characterStatus; //캐릭터 보유 현황 변수
    public List<int> itemLevel; // 아이템 레벨 현황 변수

    public void printData()
    {
        Debug.Log(coin);
        Debug.Log(highScore);
    }
}
