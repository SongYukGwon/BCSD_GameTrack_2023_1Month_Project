using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldItem : Item
{

    protected override void ItemUse()
    {
        if (ItemUseManager.GetItemUseManager().CheckItemUsing(ItemKind.Shield) == false)
        {
            player.GetComponent<PlayerContoller>().ChangePlayerState(PlayerState.Invincible);
        }
        // ������ ���ӽð� �Ҵ�
        ItemUseManager.GetItemUseManager().AddItemDuration(ItemKind.Shield, itemInfo.duration);
    }

    protected override void ItemEnd()
    {
        if (ItemUseManager.GetItemUseManager().CheckItemUsing(ItemKind.Shield) == false)
        {
            if (ItemUseManager.GetItemUseManager().CheckItemUsing(ItemKind.Boost) == false)
            {
                player.GetComponent<PlayerContoller>().ChangePlayerState(PlayerState.Running);
            }
        }
    }
}
