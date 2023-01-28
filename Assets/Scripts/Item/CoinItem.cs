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
        Debug.Log(other.name);
        if (other.CompareTag("CoinTaker"))
        {
            isDetected = true;
            Debug.Log("detected");
        }
        else if (other.CompareTag("Player"))
        {
            // 코인 개수 증가 코드
            GameManager.GetInstaince().PlusCoin(1);
            SoundManager.PlaySfx(SFX.COIN);
            Debug.Log("Coin Added");
            Destroy(gameObject);
        }
    }
}
