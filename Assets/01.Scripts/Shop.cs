using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    [Header("상점 판매 목록 (인스펙터에서 5개 등록)")]
    [SerializeField] private ShopItemData[] shopItems;

    [Header("상점 UI 요소")]
    [SerializeField] private TMP_Text shopGoldText;

    private void OnEnable()
    {
        UpdateShopUI();
    }

    private void UpdateShopUI()
    {
        if (PlayerStatus.instance != null && shopGoldText != null)
        {
            shopGoldText.text = $"보유 골드: {PlayerStatus.instance.Gold} G";
        }
    }

    // 상점의 버튼들이 클릭되었을 때 호출 (인덱스: 0 ~ 4)
    public void BuyItem(int itemIndex)
    {
        if (itemIndex < 0 || itemIndex >= shopItems.Length) return;
        if (PlayerStatus.instance == null || InventoryManager.instance == null) return;

        ShopItemData selectedItem = shopItems[itemIndex];

        // 1차 검증: 돈이 충분한가?
        if (PlayerStatus.instance.Gold < selectedItem.price)
        {
            Debug.LogError("골드가 부족합니다.");
            return;
        }

        // 2차 검증: 소모품의 경우 이미 최대 소지 개수를 넘지 않았는가?
        if (selectedItem.itemType == ItemType.Potion && InventoryManager.instance.potionCount >= selectedItem.maxCarryLimit)
        {
            Debug.LogError("포션을 더 이상 소지할 수 없습니다.");
            return;
        }
        if (selectedItem.itemType == ItemType.Bomb && InventoryManager.instance.bombCount >= selectedItem.maxCarryLimit)
        {
            Debug.LogError("폭탄을 더 이상 소지할 수 없습니다.");
            return;
        }
        if (selectedItem.itemType == ItemType.Weapon && InventoryManager.instance.ownedWeapons.Contains(selectedItem))
        {
            Debug.LogError("이미 소유한 무기입니다.");
            return;
        }

        // 모든 검증 통과 후 구매 확정 처리
        PlayerStatus.instance.Gold -= selectedItem.price; // 돈 차감
        InventoryManager.instance.AddItem(selectedItem);  // 인벤토리에 지급
        UpdateShopUI();                                  // 상점 UI 새로고침

        Debug.Log($"{selectedItem.itemName} 구매 완료!");
    }
}