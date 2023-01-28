using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevivalItem : Item
{
    public bool useRevivalItem = false;

    protected override void ItemUse()
    {
        if(useRevivalItem)
            player.GetComponent<PlayerContoller>().ChangePlayerState(PlayerState.Running);
    }

    protected override void ItemEnd()
    {
        base.ItemEnd();
    }
}
