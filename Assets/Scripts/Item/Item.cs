using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public ItemInfo itemInfo; //Item���� ���� Ư¡�� ���� Scriptable Object
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
            // �ش� ������ Ÿ���� ���� ���ð��� ������ ���� ������ ��� ���� ���� ����
            if (ItemUseManager.GetItemUseManager().CheckItemUsing(itemInfo.itemKind) == false)
            {
                ItemEnd();
                Destroy(gameObject);
                haveItem = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other) // �����۰� �����ϸ� �ٷ� ������ ����
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
