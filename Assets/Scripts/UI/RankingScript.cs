using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System;
using TMPro;
using System.Linq;
using Unity.VisualScripting;

//��ŷ
public class RankingScript : MonoBehaviour, ISingleton
{
    DatabaseReference reference;

    static RankingScript instance;

    long strLen = 0;
    List<string> strRank = new List<string>();

    public static RankingScript GetInstaince()
    {
        return instance;
    }

    void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        reference = FirebaseDatabase.DefaultInstance.RootReference;

        LoadRanking();
    }

    //��ŷ �ҷ����� �Լ�
    public void LoadRanking()
    {
        //users����� �ҷ��� �����ϸ� �������� ��� ������ ������ ��
        reference.Child("users").GetValueAsync().ContinueWith(task => {
            if (task.IsFaulted)
            {
                LoadRanking();
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                int count = 0;
                strLen = snapshot.ChildrenCount;
                strRank = new List<string>(Enumerable.Repeat("", (int)strLen));
                foreach (DataSnapshot data in snapshot.Children)
                {
                    IDictionary rankInfo = (IDictionary)data.Value;
                    strRank[count] = rankInfo["userId"].ToString() + ","
+ rankInfo["score"];
                    count++;
                }
            }
            
        });
    }

    //�ҷ��� �����͸� ������ UI�Է�
    public void TextLoad()
    {
        try
        {
            //�޾ƿ� ������ �������� ����
            strRank.Sort((x, y) =>
            {
                string[] a = x.Split(',');
                string[] b = y.Split(",");
                int sc = Int32.Parse(a[1]);
                int bc = Int32.Parse(b[1]);

                if (sc >= bc)
                    return -1;
                else
                    return 1;
            });
        }
        catch (NullReferenceException e)
        {
            return;
        }
        RankingUI ui = GameObject.Find("Canvas").GetComponent<RankingUI>();
        if(ui != null)
        {
            for (int i = 0; i < strRank.Count; i++)
            {
                string[] tm = strRank[i].Split(',');
                ui.SetRanking(i, tm[0], tm[1]);
            }
        }
    }

    //��ŷ ������Ʈ �õ� �Լ�
    public void OnClickTransactionSave(string userId, int score)
    {
        const int MaxScoreRecordCount = 10;

        reference.Child("users").RunTransaction(mutableData => {
            List<object> leaders = mutableData.Value as List<object>;

            if (leaders == null)
            {
                leaders = new List<object>();
            }

            // ��ŷ�� ��ϵ� ������ ���մϴ�.
            else if (mutableData.ChildrenCount >= MaxScoreRecordCount)
            {
                long minScore = long.MaxValue;
                object minVal = null;
                foreach (var child in leaders)
                {
                    if (!(child is Dictionary<string, object>))
                        continue;
                    long childScore = (long)((Dictionary<string, object>)child)["score"];
                    if (childScore < minScore)
                    {
                        minScore = childScore;
                        minVal = child;
                    }
                }
                if (minScore > score)
                {
                    // ���� ������ ������ �������� ������ �ߴ�.
                    return TransactionResult.Abort();
                }

                // ���� ������ �����͸� �����մϴ�.(��ŷ ����)
                leaders.Remove(minVal);
            }

            Dictionary<string, object> newScoreMap = new Dictionary<string, object>();

            newScoreMap["score"] = score;
            newScoreMap["userId"] = userId;

            leaders.Add(newScoreMap);
            mutableData.Value = leaders;


            LoadRanking();
            return TransactionResult.Success(mutableData);
        });
    }

}