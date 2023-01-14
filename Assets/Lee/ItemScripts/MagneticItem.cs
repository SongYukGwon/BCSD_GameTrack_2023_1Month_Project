using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticItem : Item
{
    [SerializeField] GameObject coinTaker;
    protected override void ItemUse()
    {
        coinTaker = Instantiate(coinTaker, player.transform);
    }
    protected override void ItemEnd()
    {
        Destroy(coinTaker);
    }
}
