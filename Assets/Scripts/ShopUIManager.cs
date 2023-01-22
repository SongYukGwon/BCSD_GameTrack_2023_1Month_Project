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
        // 상점 씬 들어왔을 때 아이템 레벨 UI에 적용
        for(int i = 1; i <= 4; ++i)
        {
            int level = DataManager.GetInstance().LoadData().itemLevel[i - 1];
            UpdateItemLevelUI(i, level);
        }
        skinCamera.enabled = false;
        Invoke("UpdateCoin", 0.3f);
    }

    private void UpdateCoin()
    {
        PlayerData data = DataManager.GetInstance().LoadData();
        CoinText.text = data.coin.ToString();
    }

    public void BuyAndEquipBtn(TextMeshProUGUI text)
    {
        string state = text.text;
        int index = UISpawner.GetComponent<Spawn>().GetIndex();
        PlayerData data = DataManager.GetInstance().LoadData();
        switch (state)
        {
            case "Buy":
                if (data.characterStatus[index] == false && data.coin >= 100)
                {
                    data.coin -= 100;
                    data.characterStatus[index] = true;
                    characterCoin.text = "";
                    characterStatus.text = "Equip";
                }
                break;
            case "Equip":
                data.character = index;
                characterStatus.text = "Equiped";
                GameObject UISpawner = GameObject.Find("PlayerSpawn");
                UISpawner.GetComponent<Spawn>().setCharacter(index);
                break;
            default:
                characterCoin.text = "Already Equiped";
                break;
        }
        DataManager.GetInstance().SaveData(data);
        UpdateCoin();
    }

    public void OpenItemShop()
    {
        itemTab.SetActive(true);
    }

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

    public void BackToMenu()
    {
        SceneManager.LoadScene("Title");
    }

    public void ItemTab_BackButton()
    {
        itemTab.SetActive(false);
    }

    public void ShopTap_BackButton()
    {
        shopTab.SetActive(false);
        skinCamera.enabled = false;
    }


    public void Next_CharacterBtn(int num)
    {
        Debug.Log(num);
        UISpawner.GetComponent<Spawn>().ChangeCharacter(num);
        int index = UISpawner.GetComponent<Spawn>().GetIndex();
        PlayerData data = DataManager.GetInstance().LoadData();
        if (data.characterStatus[index] == false)
        {
            characterCoin.text = "100G";
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


    public void ItemTab_Upgrade(GameObject text)
    {
        PlayerData data = DataManager.GetInstance().LoadData();
        int cost = int.Parse(text.GetComponent<Text>().text);
        if (data.coin - cost >= 0)
        {
            data.coin -= cost; //코인 차감

            int itemNum = int.Parse(text.name); // 아이템 번호 (1, 2, 3, 4)
            data.itemLevel[itemNum - 1] += 1; // 아이템 레벨 증가
            DataManager.GetInstance().SaveData(data);

            UpdateItemLevelUI(itemNum, data.itemLevel[itemNum - 1]); // 아이템 레벨 UI 변경
        }
        else Debug.Log("No enough money");
    }

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
