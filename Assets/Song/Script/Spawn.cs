using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField]
    private GameObject[] characters;

    // Start is called before the first frame update
    private void Start()
    {
        PlayerData data = DataManager.GetInstance().LoadData();
        characters[1].SetActive(true);
    }
}
