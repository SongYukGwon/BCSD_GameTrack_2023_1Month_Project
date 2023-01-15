using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostItem : Item
{
    public int boostSpeed = 0;
    int prevSpeed;

    protected override void ItemUse()
    {
        if (player == null) Debug.Log("Player Null");
        prevSpeed = player.GetComponent<PlayerContoller>().speed;
        player.GetComponent<PlayerContoller>().speed = boostSpeed;
    }

    protected override void ItemEnd()
    {
        player.GetComponent<PlayerContoller>().speed = prevSpeed;
    }
}
