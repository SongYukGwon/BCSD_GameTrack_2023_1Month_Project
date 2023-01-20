using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ShopUIManager : MonoBehaviour
{
    public GameObject itemTab;

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
    }

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
