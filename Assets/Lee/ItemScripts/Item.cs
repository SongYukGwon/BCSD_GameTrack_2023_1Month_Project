using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public ItemInfo itemInfo;
    protected GameObject player;
    bool isItemUsing = false;
    float currentDuration;
    Material newMaterial; // ≈ı∏Ì«— Material

    private void Start()
    {
        currentDuration = itemInfo.duration;
        player = GameObject.Find("Player");
        if (player == null) Debug.Log($"{gameObject.name} : No Player");
        newMaterial = Resources.Load<Material>("Materials/Transparent");
    }

    void Update()
    {
        if (currentDuration < 0)
        {
            Debug.Log("Time Over");
            isItemUsing = false;
            ItemEnd();
            Destroy(gameObject);
        }
        if (isItemUsing)
        {
            currentDuration -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.GetComponent<MeshRenderer>().material = newMaterial;
            ItemUse();
            isItemUsing = true;
        }
    }

    protected virtual void ItemUse()
    {

    }
    protected virtual void ItemEnd()
    {

    }
}
