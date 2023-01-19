using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public int coin;
    public int highScore;
    public List<int> itemLevel;

    public void printData()
    {
        Debug.Log(coin);
        Debug.Log(highScore);
    }
}
