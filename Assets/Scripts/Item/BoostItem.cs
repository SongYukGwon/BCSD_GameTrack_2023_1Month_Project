using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostItem : Item
{
    public float boostSpeed = 0;
    float prevSpeed = 1;

    protected override void ItemUse()
    {
        if (ItemUseManager.GetItemUseManager().CheckItemUsing(ItemKind.Boost) == false)
        {
            prevSpeed = player.GetComponent<PlayerContoller>().basicSpeed;
            player.GetComponent<PlayerContoller>().basicSpeed = boostSpeed;
            player.GetComponent<PlayerContoller>().ChangePlayerState(PlayerState.Invincible);
            //부스트 이펙트 생성
            player.GetComponent<PlayerContoller>().BoostEffectSwitch(true);
        }

        // 아이템 사용시간 할당
        ItemUseManager.GetItemUseManager().AddItemDuration(ItemKind.Boost, itemInfo.duration);
    }

    protected override void ItemEnd()
    {
        if (ItemUseManager.GetItemUseManager().CheckItemUsing(ItemKind.Boost) == false)
        {
            player.GetComponent<PlayerContoller>().BoostEffectSwitch(false);
            player.GetComponent<PlayerContoller>().basicSpeed = prevSpeed;

            if (ItemUseManager.GetItemUseManager().CheckItemUsing(ItemKind.Shield) == false)
            {
                player.GetComponent<PlayerContoller>().ChangePlayerState(PlayerState.Running);
            }
        }
    }
}
