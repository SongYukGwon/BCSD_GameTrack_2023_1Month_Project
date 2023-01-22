using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticItem : Item
{
    [SerializeField] GameObject coinTaker;
    protected override void ItemUse()
    {
        if (player == null) player = GameObject.Find("Player");
        coinTaker = Instantiate(coinTaker, player.transform);
    }
    protected override void ItemEnd()
    {
        Destroy(coinTaker);
    }
}
