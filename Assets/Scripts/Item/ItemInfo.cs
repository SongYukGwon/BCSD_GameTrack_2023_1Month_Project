using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemKind
{
    Magnetic, Boost, Shield, Revival,
}

[CreateAssetMenu(fileName = "Item")]
public class ItemInfo : ScriptableObject
{
    public string itemName;
    public float duration;
    public ItemKind itemKind;
    public int itemLevel = 1;
}
