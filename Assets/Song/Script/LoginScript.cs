using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginScript : MonoBehaviour
{

    public TMP_InputField nickName;
    public TextMeshProUGUI notice;

    public void Start()
    {
        Invoke("CheckOpen", 0.1f);
    }

    //닉네임 창 오픈 여부 확인
    private void CheckOpen()
    {
        PlayerData data = DataManager.GetInstance().LoadData();
        if(data.userId != "Null")
        {
            this.gameObject.SetActive(false);
        }
    }

    //닉네임창 OK버튼 클릭 이벤트
    public void OnClickOK()
    {
        //닉네임 입력창 확인후 처리
        if(nickName.text == "")
        {
            notice.text = "닉네임을 입력하세요!";
        }
        else
        {
            PlayerData data = DataManager.GetInstance().LoadData();
            data.userId = nickName.text;
            DataManager.GetInstance().SaveData(data);
            this.gameObject.SetActive(false);
        }
        
    }



}
