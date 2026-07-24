using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class WeaponSwapManager : MonoBehaviour
{
    [Header("Weapon List")]

    [SerializeField] private List<GameObject> weapons = new List<GameObject>();

    private int currentWeaponIndex = 0;

    private void Start()
    {
        SwapWeapon(0);
    }

    private void Update()
    {
        // 키보드 숫자 1, 2, 3 키 입력 감지 (New Input System 방식)
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            SwapWeapon(0); // 1번 키 누르면 0번째 무기(Bow) 장착
        }
        else if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            SwapWeapon(1); // 2번 키 누르면 1번째 무기(Burst) 장착
        }
        else if (Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            SwapWeapon(2); // 3번 키 누르면 2번째 무기(Multi) 장착
        }
    }

    private void SwapWeapon(int index)
    {
        // 잘못된 인덱스가 들어오거나 무기 리스트가 비어있다면 방어
        if (index < 0 || index >= weapons.Count) return;

        // 리스트를 한 바퀴 돌면서 선택한 인덱스의 무기만 켜고, 나머지는 다 꺼버립니다.
        for (int i = 0; i < weapons.Count; i++)
        {
            if (weapons[i] != null)
            {
                // i 가 내가 선택한 번호(index)와 같을 때만 true가 됩니다.
                weapons[i].SetActive(i == index);
            }
        }

        currentWeaponIndex = index;
        Debug.Log($"{weapons[index].name} 무기로 교체되었습니다!");
    }
}