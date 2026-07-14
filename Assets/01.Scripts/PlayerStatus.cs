using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerStatus : MonoBehaviour
{

    [Header("Health")]

    [SerializeField] TextMeshProUGUI DiedText;
    [SerializeField] private int maxHealth = 100;

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