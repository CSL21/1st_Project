using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI statPointText;
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private TextMeshProUGUI speedText;

    public void UpdateLobbyUI()
    {
        {
            if (PlayerStatus.instance != null)
            {
                goldText.text = PlayerStatus.instance.Gold.ToString("N0");
                statPointText.text = PlayerStatus.instance.StatPoint.ToString();
                hpText.text = PlayerStatus.instance.currentHealth.ToString();
                damageText.text = PlayerStatus.instance.BaseDamage.ToString("F1");
                speedText.text = PlayerStatus.instance.AttackSpeed.ToString("F1");
            }
        }
    }
    void Start()
    {
        UpdateLobbyUI();
    }

}