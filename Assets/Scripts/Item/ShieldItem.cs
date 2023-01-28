using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldItem : Item
{
    protected override void ItemUse()
    {
        //if (player == null) player = GameObject.Find("Player");
        if (GameManager.ItemUseManager.CheckItemUsing(ItemKind.Shield) == false)
            player.GetComponent<PlayerContoller>().ChangePlayerState(PlayerState.Invincible);
        // 아이템 지속시간 할당
        GameManager.ItemUseManager.AddItemDuration(ItemKind.Shield, itemInfo.duration);
    }

    protected override void ItemEnd()
    {
        Debug.Log($"{gameObject.name} : End");
        if(GameManager.ItemUseManager.CheckItemUsing(ItemKind.Shield) == false &&
           GameManager.ItemUseManager.CheckItemUsing(ItemKind.Boost) == false)
            player.GetComponent<PlayerContoller>().ChangePlayerState(PlayerState.Running);
    }
}
