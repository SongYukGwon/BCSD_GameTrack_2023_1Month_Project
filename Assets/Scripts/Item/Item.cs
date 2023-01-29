using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public ItemInfo itemInfo;
    public bool haveItem = false;
    protected GameObject player;
    Material newMaterial; // ������ Material

    void Start()
    {
        player = GameObject.Find("Player");
        newMaterial = Resources.Load<Material>("BackGroundObject/Materials/Transparent");
    }

    void Update()
    {
        if (haveItem)
        {
            if (GameManager.ItemUseManager.CheckItemUsing(itemInfo.itemKind) == false)
            {
                ItemEnd();
                Destroy(gameObject);
                haveItem = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.GetComponent<MeshRenderer>().material = newMaterial;
            ItemUse();
            haveItem = true;
        }
    }

    protected virtual void ItemUse() {} // ������ ȹ�� �� ���Ǵ� �Լ�
    protected virtual void ItemEnd() {} // ������ �ð� ����� ���Ǵ� �Լ�
}
