using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostItem : Item
{
    public float boostSpeed = 0;
    float prevSpeed = 1;

    protected override void ItemUse()
    {
        if (GameManager.ItemUseManager.CheckItemUsing(ItemKind.Boost) == false)
        {
            //if (player == null) player = GameObject.Find("Player");
            prevSpeed = player.GetComponent<PlayerContoller>().basicSpeed;
            player.GetComponent<PlayerContoller>().basicSpeed = boostSpeed;
            player.GetComponent<PlayerContoller>().ChangePlayerState(PlayerState.Invincible);
            player.GetComponent<PlayerContoller>().BoostEffectSwitch(true);
        }

        // 아이템 사용시간 할당
        GameManager.ItemUseManager.AddItemDuration(ItemKind.Boost, itemInfo.duration);
    }

    protected override void ItemEnd()
    {
        Debug.Log($"{gameObject.name} : End");
        if (GameManager.ItemUseManager.CheckItemUsing(ItemKind.Boost) == false)
        {
            player.GetComponent<PlayerContoller>().basicSpeed = prevSpeed;
            player.GetComponent<PlayerContoller>().BoostEffectSwitch(false);

            StartCoroutine(BoostEndTerm());
            /*if (GameManager.ItemUseManager.CheckItemUsing(ItemKind.Shield) == false)
                player.GetComponent<PlayerContoller>().ChangePlayerState(PlayerState.Running);*/
        }
    }

    IEnumerator BoostEndTerm()
    {
        yield return new WaitForSeconds(3.0f);
        if (GameManager.ItemUseManager.CheckItemUsing(ItemKind.Shield) == false)
            player.GetComponent<PlayerContoller>().ChangePlayerState(PlayerState.Running);
    }
}
