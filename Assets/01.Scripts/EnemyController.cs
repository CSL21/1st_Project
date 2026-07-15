using System.Diagnostics;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float maxHP;
    [SerializeField] float nowHP;

    float moveSpeed;

    Transform target;

    SpriteRenderer sr;

    [SerializeField] float range = 5f;

    HPBar hpBar;

    [SerializeField] int touchDamage = 10;
    [SerializeField] float damageCooldown = 0.5f;
    private float lastDamageTime;

    private void OnEnable()
    {
        nowHP = 10;
        maxHP = 10;
    }

    void Start()
    {
        moveSpeed = 2f;
        sr = GetComponent<SpriteRenderer>();

        GameObject playerObj = GameObject.Find("Player");
        target = playerObj.transform;
    }

    void Update()
    {
        CheckDistance();
    }

    void CheckDistance()
    {
        float distance = Vector3.Distance(transform.position, target.position);

        if (distance <= range)
        {
            Trace();
        }
    }

    void Trace()
    {

        sr.flipX = CheckFlip();
        
        Move();
    }

    void Move()
    {

        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }

    bool CheckFlip()
    {
        return transform.position.x > target.position.x ? true : false;
    }

    public void TakeDamage(float damage)
    {
        nowHP -= damage;

        if (hpBar != null)
        {
            hpBar.SetGauge(nowHP / maxHP);
        }


        if (nowHP <= 0)
        {
            nowHP = 0;
            Die();
        }
    }

    public void EnemyHpBar(HPBar bar)
    {
        hpBar = bar;
        if (hpBar != null)
        {
            hpBar.SetTarget(this.transform);
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        if (Time.time < lastDamageTime + damageCooldown) return;

        PlayerStatus player = collision.gameObject.GetComponent<PlayerStatus>();
        if (player != null)
        {
            player.TakeDamage(touchDamage);
            lastDamageTime = Time.time;
        }
    }
}