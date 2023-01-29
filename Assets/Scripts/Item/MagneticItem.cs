using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticItem : Item
{
    [SerializeField] GameObject coinTaker;
    protected override void ItemUse()
    {
        if (player == null) player = GameObject.Find("Player");
        if (ItemUseManager.GetItemUseManager().CheckItemUsing(ItemKind.Magnetic) == false)
            coinTaker = Instantiate(coinTaker, player.transform);
        ItemUseManager.GetItemUseManager().AddItemDuration(ItemKind.Magnetic, itemInfo.duration);
    }
    protected override void ItemEnd()
    {
        if (ItemUseManager.GetItemUseManager().CheckItemUsing(ItemKind.Magnetic) == false)
        {
            Destroy(coinTaker.gameObject);
        }
    }
}
