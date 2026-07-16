using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    [Header("무기 보관함")]
    public List<ShopItemData> ownedWeapons = new List<ShopItemData>();
    public ShopItemData equippedWeapon;

    [Header("소모품 개수 관리")]
    public int potionCount = 0;
    public int bombCount = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void AddItem(ShopItemData item)
    {
        if (item == null) return;

        switch (item.itemType)
        {
            case ItemType.Weapon:
                if (!ownedWeapons.Contains(item))
                {
                    ownedWeapons.Add(item);
                    Debug.Log($"{item.itemName} 무기가 인벤토리에 추가되었습니다.");
                    EquipWeapon(item);
                }
                break;

            case ItemType.Potion:
                potionCount++;
                Debug.Log($"포션을 획득했습니다! 현재 개수: {potionCount}");
                break;

            case ItemType.Bomb:
                bombCount++;
                Debug.Log($"폭탄을 획득했습니다! 현재 개수: {bombCount}");
                break;
        }
    }

    public void EquipWeapon(ShopItemData weaponItem)
    {
        if (weaponItem == null || weaponItem.itemType != ItemType.Weapon) return;

        equippedWeapon = weaponItem;

        if (PlayerStatus.instance != null)
        {
            PlayerStatus.instance.EquipWeaponPrefab(weaponItem.weaponPrefab);
        }
    }
}