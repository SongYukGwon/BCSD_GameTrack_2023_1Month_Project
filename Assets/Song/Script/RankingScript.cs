using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System;
using TMPro;



public class RankingScript : MonoBehaviour
{
    DatabaseReference reference;

    static RankingScript instance;
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
    }

    public void OnClickTransactionSave(string userId, int score)
    {
        const int MaxScoreRecordCount = 5;

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