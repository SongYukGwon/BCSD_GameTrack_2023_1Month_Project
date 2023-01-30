using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class ShopUIManager : MonoBehaviour
{
    public GameObject itemTab;
    public GameObject shopTab;
    public Camera skinCamera;

    public List<int> characterPrice;

    public GameObject UISpawner;
    public TextMeshProUGUI CoinText;

    public TextMeshProUGUI characterStatus;
    public TextMeshProUGUI characterCoin;


    public TextMeshProUGUI item1_lv;
    public TextMeshProUGUI item2_lv;
    public TextMeshProUGUI item3_lv;
    public TextMeshProUGUI item4_lv;

    public TextMeshProUGUI item1_tab_lv;
    public TextMeshProUGUI item2_tab_lv;
    public TextMeshProUGUI item3_tab_lv;
    public TextMeshProUGUI item4_tab_lv;

    private void Start()
    {
        // ���� �� ������ �� ������ ���� UI�� ����
        for(int i = 1; i <= 4; ++i)
        {
            int level = DataManager.GetInstance().LoadData().itemLevel[i - 1];
            UpdateItemLevelUI(i, level);
        }
        skinCamera.enabled = false;
        Invoke("UpdateCoin", 0.3f);
    }

    //���� �Һ� �Լ�
    private void UpdateCoin()
    {
        PlayerData data = DataManager.GetInstance().LoadData();
        CoinText.text = data.coin.ToString();
    }

    //���� �� ���� ��ư �̺�Ʈ �Լ�
    public void BuyAndEquipBtn(TextMeshProUGUI text)
    {
        string state = text.text;
        int index = UISpawner.GetComponent<Spawn>().GetIndex();
        PlayerData data = DataManager.GetInstance().LoadData();
        switch (state)
        {
            case "Buy":
                int price = characterPrice[index];
                if (data.characterStatus[index] == false && data.coin >= price)
                {
                    data.coin -= price;
                    data.characterStatus[index] = true;
                    characterCoin.text = "";
                    characterStatus.text = "Equip";
                }
                break;
            case "Equip":
                data.character = index;
                characterStatus.text = "Equiped";
                GameObject PUISpawner = GameObject.Find("PlayerSpawn");
                PUISpawner.GetComponent<Spawn>().setCharacter(index);
                break;
            default:
                characterCoin.text = "Already Equiped";
                break;
        }
        DataManager.GetInstance().SaveData(data);
        UpdateCoin();
    }

    //������ �� ��ư �̺�Ʈ �Լ�
    public void OpenItemShop()
    {
        itemTab.SetActive(true);
    }

    //ĳ���� �� ��ư �̺�Ʈ �Լ�
    public void OpenSkinShop()
    {
        shopTab.SetActive(true);
        skinCamera.enabled = true;
        int index = UISpawner.GetComponent<Spawn>().GetIndex();
        PlayerData data = DataManager.GetInstance().LoadData();
        if (data.characterStatus[data.character] == true)
        {
            characterCoin.text = "";
            characterStatus.text = "Equiped";
        }
    }


    //���� ������ ��ư �̺�Ʈ �Լ�
    public void BackToMenu()
    {
        SceneManager.LoadScene("Title");
    }

    //������ �� ������ ��ư �̺�Ʈ �Լ�
    public void ItemTab_BackButton()
    {
        itemTab.SetActive(false);
    }

    //���� �� ������ ��ư �̺�Ʈ �Լ�
    public void ShopTap_BackButton()
    {
        shopTab.SetActive(false);
        skinCamera.enabled = false;
    }

    //���� UI ���� ĳ���� �ٲٱ� ��ư �̺�Ʈ �Լ�
    public void Next_CharacterBtn(int num)
    {
        UISpawner.GetComponent<Spawn>().ChangeCharacter(num);
        int index = UISpawner.GetComponent<Spawn>().GetIndex();
        PlayerData data = DataManager.GetInstance().LoadData();
        if (data.characterStatus[index] == false)
        {
            characterCoin.text = characterPrice[index].ToString();
            characterStatus.text = "Buy";
        }
        else
        {
            characterCoin.text = "";
            if (data.character == index)
            {
                characterStatus.text = "Equiped";
            }
            else
            {
                characterStatus.text = "Equip";
            }
        }
    }

    //������ ���׷��̵� ��ư �̺�Ʈ �Լ�
    public void ItemTab_Upgrade(GameObject text)
    {
        PlayerData data = DataManager.GetInstance().LoadData();
        int cost = int.Parse(text.GetComponent<Text>().text);
        if (data.coin - cost >= 0)
        {
            data.coin -= cost; //���� ����

            int itemNum = int.Parse(text.name); // ������ ��ȣ (1, 2, 3, 4)
            data.itemLevel[itemNum - 1] += 1; // ������ ���� ����
            DataManager.GetInstance().SaveData(data);

            UpdateItemLevelUI(itemNum, data.itemLevel[itemNum - 1]); // ������ ���� UI ����
            UpdateCoin();
        }
        else Debug.Log("No enough money");
    }

    //������ ���� �� �Լ�
    private void UpdateItemLevelUI(int itemNum, int level)
    {
        switch (itemNum)
        {
            case 1:
                {
                    item1_lv.text = $"Lv {level}";
                    item1_tab_lv.text = $"Lv {level}";
                    break;
                }
            case 2:
                {
                    item2_lv.text = $"Lv {level}";
                    item2_tab_lv.text = $"Lv {level}";
                    break;
                }
            case 3:
                {
                    item3_lv.text = $"Lv {level}";
                    item3_tab_lv.text = $"Lv {level}";
                    break;
                }
            case 4:
                {
                    item4_lv.text = $"Lv {level}";
                    item4_tab_lv.text = $"Lv {level}";
                    break;
                }
            default:
                break;
        }
    }
}
