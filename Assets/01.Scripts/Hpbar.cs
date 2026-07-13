using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    Image img;

    [SerializeField] Transform target;  // 이 체력바가 달라붙을 대상

    [SerializeField] Vector3 offset = Vector3.up;

    float gauge;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        img = GetComponent<Image>();
    }

    private void LateUpdate()
    {
        if (target == null)
            return;

        transform.position = target.position + offset;

        PlayerController player = target.GetComponent<PlayerController>();
        if (player != null && img != null)
        {
            img.fillAmount = player.GetHPPercent();
        }
    }

    public void SetGauge(float gauge)
    {
        img.fillAmount = gauge;
    }

    public void SetTarget(Transform t)
    {
        target = t;
    }


}
