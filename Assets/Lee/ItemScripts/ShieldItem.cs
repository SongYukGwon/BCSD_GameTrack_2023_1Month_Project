using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldItem : Item
{
    protected override void ItemUse()
    {
        if (player == null) player = GameObject.Find("Player");
        player.GetComponent<PlayerContoller>().ChangePlayerState(PlayerState.Invincible);
    }

    protected override void ItemEnd()
    {
        Debug.Log($"{gameObject.name} : End");
        player.GetComponent<PlayerContoller>().ChangePlayerState(PlayerState.Running);
    }
}
