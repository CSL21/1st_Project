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
        }
        else
        {
            Destroy(gameObject);
        }
    }


        [Header("Health")]

    [SerializeField] TextMeshProUGUI DiedText;
    [SerializeField] private int maxHealth = 100;

    public float BaseDamage = 10f;
    public float AttackSpeed = 1.0f;






    private int currentHealth;

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
        SceneManager.LoadScene("MainScene");
    }
}