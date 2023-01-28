using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticItem : Item
{
    [SerializeField] GameObject coinTaker;
    protected override void ItemUse()
    {
        if (player == null) player = GameObject.Find("Player");
        if (GameManager.ItemUseManager.CheckItemUsing(ItemKind.Magnetic) == false)
            coinTaker = Instantiate(coinTaker, player.transform);
        GameManager.ItemUseManager.AddItemDuration(ItemKind.Magnetic, itemInfo.duration);
    }
    protected override void ItemEnd()
    {
        if (GameManager.ItemUseManager.CheckItemUsing(ItemKind.Magnetic) == false)
        {
            Destroy(coinTaker.gameObject);
        }
    }
}
