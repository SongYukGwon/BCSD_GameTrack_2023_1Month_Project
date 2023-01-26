using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System;
using TMPro;
using System.Linq;

public class RankingScript : MonoBehaviour
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

    public void LoadRanking()
    {

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
                    Debug.Log(strRank[count]);
                    Debug.Log(123);
                    count++;
                }
            }
            
        });
    }

    public void TextLoad()
    {
        try
        {
            //받아온 데이터 정렬 = > 위에서부터 아래로
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

    public void OnClickTransactionSave(string userId, int score)
    {
        const int MaxScoreRecordCount = 10;

        reference.Child("users").RunTransaction(mutableData => {
            List<object> leaders = mutableData.Value as List<object>;

            if (leaders == null)
            {
                leaders = new List<object>();
            }

            // 랭킹에 등록된 점수를 비교합니다.
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
                    // 현재 점수가 최하위 점수보다 낮으면 중단합니다.(랭킹에 못오르니깐)
                    return TransactionResult.Abort();
                }

                // 기존 최하위 데이터를 제거합니다.(랭킹 변경)
                leaders.Remove(minVal);
            }

            Dictionary<string, object> newScoreMap = new Dictionary<string, object>();

            newScoreMap["score"] = score;
            newScoreMap["userId"] = userId;

            leaders.Add(newScoreMap);
            mutableData.Value = leaders;
            return TransactionResult.Success(mutableData);
        });
    }

}