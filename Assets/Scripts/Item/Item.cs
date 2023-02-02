using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public ItemInfo itemInfo; //Item들의 공통 특징을 가진 Scriptable Object
    public bool haveItem = false;
    protected GameObject player;
    Material newMaterial; // 투명한 Material

    void Start()
    {
        player = GameObject.Find("Player");
        newMaterial = Resources.Load<Material>("BackGroundObject/Materials/Transparent");
    }

    void Update()
    {
        if (haveItem)
        {
            // 해당 아이템 타입의 남은 사용시간을 가져와 현재 아이템 사용 종료 여부 결정
            if (ItemUseManager.GetItemUseManager().CheckItemUsing(itemInfo.itemKind) == false)
            {
                ItemEnd();
                Destroy(gameObject);
                haveItem = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other) // 아이템과 접촉하면 바로 아이템 실행
    {
        if (other.CompareTag("Player"))
        {
            gameObject.GetComponent<MeshRenderer>().material = newMaterial;
            ItemUse();
            haveItem = true;
        }
    }

    protected virtual void ItemUse() {} // 아이템 획득 시 사용되는 함수
    protected virtual void ItemEnd() {} // 아이템 시간 종료시 사용되는 함수
}
