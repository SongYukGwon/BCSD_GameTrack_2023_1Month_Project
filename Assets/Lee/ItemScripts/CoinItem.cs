using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinItem : MonoBehaviour
{
    GameObject player;
    public bool isDetected = false;

    private void Start()
    {
        player = GameObject.Find("Player");
    }
    private void Update()
    {
        if (isDetected)
            transform.position = Vector3.Lerp(transform.position, player.transform.position, 0.3f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CoinTaker"))
        {
            isDetected = true;
        }
        else if (other.CompareTag("Player"))
        {
            // ���� ���� ���� �ڵ�
            GameManager.GetInstaince().PlusCoin(1);
            Destroy(gameObject);
        }
    }
}
