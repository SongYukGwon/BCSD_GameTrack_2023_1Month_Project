using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class DataManager : MonoBehaviour
{
    private static DataManager instance;

    public static DataManager GetInstance() { return instance; }

    private void Awake()
    {
        if(instance != null)
            Destroy(instance);

        instance= this;
        DontDestroyOnLoad(gameObject);
    }

    public void SaveData(PlayerData data)
    {
        File.WriteAllText(Application.dataPath + "/PlayerData.json", JsonUtility.ToJson(data));
    }

    public PlayerData LoadData()
    {
        PlayerData data;


        if (File.Exists(Application.dataPath + "/PlayerData.json"))
        {
            string str2 = File.ReadAllText(Application.dataPath + "/PlayerData.json");
            data = JsonUtility.FromJson<PlayerData>(str2);
        }
        else
        {
            data = new PlayerData();
            data.character = 0;
            data.coin = 100;
            data.characterStatus = Enumerable.Repeat(false, 2).ToList();
            data.characterStatus[0] = true;
            data.itemLevel = Enumerable.Repeat(1, 4).ToList();
            SaveData(data);
        }



        return data;

    }

}
