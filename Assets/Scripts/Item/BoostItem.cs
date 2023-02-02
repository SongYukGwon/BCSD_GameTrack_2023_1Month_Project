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
            //�ν�Ʈ ����Ʈ ����
            player.GetComponent<PlayerContoller>().BoostEffectSwitch(true);
        }

        // ������ ���ð� �Ҵ�
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
