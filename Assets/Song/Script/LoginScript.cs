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

    private void CheckOpen()
    {
        PlayerData data = DataManager.GetInstance().LoadData();
        if(data.userId != "Null")
        {
            this.gameObject.SetActive(false);
        }
    }

    public void OnClickOK()
    {
        
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
