using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostItem : Item
{
    public float boostSpeed = 0;
    float prevSpeed;

    protected override void ItemUse()
    {
        if (player == null) player = GameObject.Find("Player"); ;
        prevSpeed = player.GetComponent<PlayerContoller>().basicSpeed;
        player.GetComponent<PlayerContoller>().basicSpeed = boostSpeed;
    }

    protected override void ItemEnd()
    {
        player.GetComponent<PlayerContoller>().basicSpeed = prevSpeed;
    }
}
