using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUseManager : MonoBehaviour
{
    // ���� �����۵��� ��� ��Ȳ ǥ��
    [SerializeField] bool[] itemUseList = { false, false, false, false };

    [SerializeField] private float[] currentDuration = { 0, 0, 0, 0 };
    public int additionalTime = 3; // �̹� ������� �������� �� ȹ���� �� �߰��Ǵ� ���ð�

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
    }

    public void AddItemDuration(ItemKind itemKind, float duration)
    {
        if (itemUseList[(int)itemKind] == false)
        {
            itemUseList[(int)itemKind] = true;
            // ���� ���ð� ����
            currentDuration[(int)itemKind] += (duration +
                DataManager.GetInstance().LoadData().itemLevel[(int)itemKind]);
        }
        else
        {
            // �̹� �ش� �������� ������� ��, �߰� ���ð��� ��
            currentDuration[(int)itemKind] += additionalTime;
        }
    }

    public bool CheckItemUsing(ItemKind itemKind) // �ش� �������� ������̸� true, �ƴϸ� false
    {
        if (itemUseList[(int)itemKind] == false) return false;
        else return true;
    }
}
