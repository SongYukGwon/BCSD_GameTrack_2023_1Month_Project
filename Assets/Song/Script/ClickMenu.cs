using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMenu : MonoBehaviour
{
    public GameObject gameStartMenu;

    public void GameStartClick()
    {
        gameStartMenu.SetActive(false);
    }
}
