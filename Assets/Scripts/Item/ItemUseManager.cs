using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUseManager : MonoBehaviour
{
    static ItemUseManager itemUseManager;
    public static ItemUseManager GetItemUseManager()
    {
        return itemUseManager;
    }
    // 현재 아이템들의 사용 현황 표시
    [SerializeField] bool[] itemUseList = { false, false, false, false };

    [SerializeField] private float[] currentDuration = { 0, 0, 0, 0 };
    public int additionalTime = 3; // 이미 사용중인 아이템을 또 획득할 시 추가되는 사용시간

    [SerializeField] GameObject ShieldEffect;
    Animator shieldAnimator;

    private void Awake()
    {
        itemUseManager = GetComponent<ItemUseManager>();
    }

    void Start()
    {
        GameObject player = GameObject.Find("Player");
        ShieldEffect = Instantiate(ShieldEffect, player.transform);
        ShieldEffect.transform.localPosition = Vector3.up;
        ShieldEffect.SetActive(false);
        shieldAnimator = ShieldEffect.GetComponent<Animator>();
    }

    void Update()
    {
        for (int i = 0; i < 4; ++i)
        {
            if (itemUseList[i])
            {
                if (currentDuration[i] > 0) currentDuration[i] -= Time.deltaTime;
                else
                {
                    currentDuration[i] = 0;
                    itemUseList[i] = false;
                }
            }
        }
        PlayShieldEffect();
    }

    public void AddItemDuration(ItemKind itemKind, float duration)
    {
        if (itemUseList[(int)itemKind] == false)
        {
            itemUseList[(int)itemKind] = true;
            // 계산된 사용시간 적용
            currentDuration[(int)itemKind] += (duration +
            DataManager.GetInstance().LoadData().itemLevel[(int)itemKind]);
        }
        else
        {
            // 이미 해당 아이템이 사용중일 시, 추가 사용시간을 줌
            currentDuration[(int)itemKind] += additionalTime;
        }
    }

    private void PlayShieldEffect()
    {
        if (itemUseList[(int)ItemKind.Boost] || itemUseList[(int)ItemKind.Shield])
        {
            ShieldEffect.SetActive(true);
            if (currentDuration[(int)ItemKind.Boost] > 2.0f || currentDuration[(int)ItemKind.Shield] > 2.0f)
            {
                shieldAnimator.SetBool("isTimeOver", false);
            }
            else
            {
                shieldAnimator.SetBool("isTimeOver", true);
            }
        }
        else ShieldEffect.SetActive(false);
    }

    public bool CheckItemUsing(ItemKind itemKind) // 해당 아이템이 사용중이면 true, 아니면 false
    {
        if (itemUseList[(int)itemKind] == false) return false;
        else return true;
    }
}
