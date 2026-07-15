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

    private void RefreshLobbyUI()
    {
        UIManager uiManager = Object.FindFirstObjectByType<UIManager>();
        if (uiManager != null)
        {
            uiManager.UpdateLobbyUI();
        }
    }


    public void ClickHp()
    {
        if (StatPoint > 0)
        {
            StatPoint--;
            currentHealth += 20;
            RefreshLobbyUI();
        }
    }

    public void ClickATK()
    {
        if (StatPoint > 0)
        {
            StatPoint--;
            BaseDamage += 1f;
            RefreshLobbyUI();
        }
    }

    public void ClickATS()
    {
        SceneManager.LoadScene("MainScene");
        if (StatPoint > 0)
        {
            StatPoint--;
            AttackSpeed += 0.1f;
            RefreshLobbyUI();
        }
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
}