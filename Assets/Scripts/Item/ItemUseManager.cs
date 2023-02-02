using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUseManager : MonoBehaviour
{
    // �� ������ Ÿ�Ե��� ��� ����(Ȥ�� ���ð�)�� �����ϰ� ������ ��� ����Ʈ�� ����
    static ItemUseManager itemUseManager;
    public static ItemUseManager GetItemUseManager()
    {
        return itemUseManager;
    }
    // ���� �����۵��� ��� ��Ȳ ǥ��
    [SerializeField] bool[] itemUseList = { false, false, false, false };

    [SerializeField] private float[] currentDuration = { 0, 0, 0, 0 };
    public int additionalTime = 3; // �̹� ������� �������� �� ȹ���� �� �߰��Ǵ� ���ð�

    [SerializeField] GameObject ShieldEffect; // ���� ������ ��� �� ��Ÿ�� ȿ�� ������Ʈ
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

    // Shield Ȥ�� Boost ��� ��(Player�� ���� ������ ��) ���� ȿ�� ������Ʈ Ȱ��ȭ
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

    public bool CheckItemUsing(ItemKind itemKind) // �ش� �������� ������̸� true, �ƴϸ� false
    {
        if (itemUseList[(int)itemKind] == false) return false;
        else return true;
    }
}
