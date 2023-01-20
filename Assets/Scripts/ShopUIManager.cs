using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopUIManager : MonoBehaviour
{
    public GameObject itemTab;

    public void OpenItemShop()
    {
        itemTab.SetActive(true);
    }

    public void OpenSkinShop()
    {
        Debug.Log("Skin Shop Opened");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Title");
    }

    public void ItemTab_BackButton()
    {
        itemTab.SetActive(false);
    }

    public void ItemTab_Upgrade(GameObject text)
    {
        PlayerData data = DataManager.GetInstance().LoadData();
        int cost = int.Parse(text.GetComponent<Text>().text);
        if (data.coin - cost > 0)
        {
            data.coin -= cost; //코인 차감

            int itemNum = int.Parse(text.name);
            Debug.Log($"ItemNum : {itemNum}");
            data.itemLevel[itemNum - 1] += 1; // 아이템 레벨 증가
            DataManager.GetInstance().SaveData(data);
        }
        else Debug.Log("No enough money");
    }
}
