using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatus : MonoBehaviour
{

    public static PlayerStatus instance;

    private void Awake()
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


    [Header("Health")]
    [SerializeField] TextMeshProUGUI DiedText;


    [Header("Player Info")]
    private int maxHealth = 200;
    public int currentHealth = 100;
    public float BaseDamage = 10f;
    public float AttackSpeed = 1.0f;

    [Header("Gold and Point")]
    public int Gold = 500;
    public int StatPoint = 10;

    [Header("Weapon")]
    public Transform weaponContainer;
    public GameObject currentWeaponObject;

    private void RefreshLobbyUI()
    {
        UIManager uiManager = Object.FindAnyObjectByType<UIManager>();
        if (uiManager != null)
        {
            uiManager.UpdateLobbyUI();
        }
    }


    public bool TryUpgradeHp()
    {
        if (StatPoint <= 0)
            return false;

        StatPoint--;
        currentHealth += 20;
        return true;
    }

    public bool TryUpgradeAtk()
    {
        if (StatPoint <= 0)
            return false;

        StatPoint--;
        BaseDamage += 1f;
        return true;
    }

    public bool TryUpgradeAts()
    {
        if (StatPoint <= 0)
            return false;

        StatPoint--;
        AttackSpeed += 0.1f;
        return true;
    }





    public float GetHPPercent()
    {
        return (float)currentHealth / maxHealth;
    }

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"{damage}의 데미지를 입었습니다! 남은 체력: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {

        if (DiedText != null)
        {
            DiedText.text = "You Died!";
            DiedText.gameObject.SetActive(true);
        }

        Debug.Log("죽었습니다!");
        this.enabled = false;
        Invoke("GoMain", 3f);
    }

    private void GoMain()
    {
        SceneManager.LoadScene("LobbyScene");
    }

    public void EquipWeaponPrefab(GameObject weaponPrefab)
    {
        if (weaponPrefab == null) return;

        if (weaponContainer == null)
        {
            weaponContainer = this.transform;
        }

        if (currentWeaponObject != null)
        {
            Destroy(currentWeaponObject);
        }

        currentWeaponObject = Instantiate(weaponPrefab, weaponContainer);

        currentWeaponObject.transform.localPosition = Vector3.zero;
        currentWeaponObject.transform.localRotation = Quaternion.identity;

        Debug.Log($"[PlayerStatus] 새 무기({weaponPrefab.name}) 실시간 생성 및 장착 완료!");
    }
}