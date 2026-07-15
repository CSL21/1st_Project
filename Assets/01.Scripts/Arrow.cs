using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float totalDamage = 5f;
    public float arrowDamage = 10f;

    float lifeTime;

    float timer;

    Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lifeTime = 3f;
        timer = 0f;
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= lifeTime)
        {
            ReturnPool();
        }
    }

    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;

        rb.linearVelocity = transform.right * speed;
    }

    public void SetDamage()
    {
       totalDamage = arrowDamage + PlayerStatus.instance.BaseDamage;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        //if(collision.gameObject.layer==6) // Wall
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            ReturnPool();
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            ReturnPool();
            collision.gameObject.GetComponent<EnemyController>().TakeDamage(totalDamage);
            Debug.Log($"µ¥¹ÌÁö {totalDamage} ");
        }
    }

    void ReturnPool()
    {
        SoundManager.instance.PlaySFX(SFXType.Arrow);
        ObjectPoolManager.instance.ReturnObject("Arrow", this.gameObject);
    }

}
