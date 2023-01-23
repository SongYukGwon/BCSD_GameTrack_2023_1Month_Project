using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostItem : Item
{
    public float boostSpeed = 0;
    float prevSpeed;

    protected override void ItemUse()
    {
        if (player == null) player = GameObject.Find("Player");
        prevSpeed = player.GetComponent<PlayerContoller>().basicSpeed;
        player.GetComponent<PlayerContoller>().basicSpeed = boostSpeed;
        player.GetComponent<PlayerContoller>().ChangePlayerState(PlayerState.Invincible);
        player.GetComponent<PlayerContoller>().BoostEffectSwitch(true);
    }

    protected override void ItemEnd()
    {
        player.GetComponent<PlayerContoller>().basicSpeed = prevSpeed;
        player.GetComponent<PlayerContoller>().BoostEffectSwitch(false);
        player.GetComponent<PlayerContoller>().ChangePlayerState(PlayerState.Running);
    }
}
