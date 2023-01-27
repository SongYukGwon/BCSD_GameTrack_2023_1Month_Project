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

    //�г��� â ���� ���� Ȯ��
    private void CheckOpen()
    {
        PlayerData data = DataManager.GetInstance().LoadData();
        if(data.userId != "Null")
        {
            this.gameObject.SetActive(false);
        }
    }

    //�г���â OK��ư Ŭ�� �̺�Ʈ
    public void OnClickOK()
    {
        //�г��� �Է�â Ȯ���� ó��
        if(nickName.text == "")
        {
            notice.text = "�г����� �Է��ϼ���!";
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
