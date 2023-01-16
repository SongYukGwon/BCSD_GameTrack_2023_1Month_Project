using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldItem : Item
{
    protected override void ItemUse()
    {
        player.GetComponent<PlayerContoller>().ChangePlayerState(PlayerState.Invincible);
    }

    protected override void ItemEnd()
    {
        player.GetComponent<PlayerContoller>().ChangePlayerState(PlayerState.Running);
    }
}
