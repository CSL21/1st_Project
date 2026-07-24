using UnityEngine;
using UnityEngine.SceneManagement;

public class Arrow : MonoBehaviour
{

    [SerializeField] private string poolKey = "Arrow";
    [SerializeField] float speed = 5f;
    [SerializeField] float totalDamage = 0f;
    [SerializeField] float arrowDamage = 10f;
    [SerializeField] private bool isPiercing = false;
    [SerializeField] private int pierceCount = 1;

    private int currentPierceCount;
    float lifeTime;
    float timer;

    Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lifeTime = 2f;
        timer = 0f;
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        timer = 0f;
        SceneManager.sceneLoaded += OnSceneLoaded;
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

            if (isPiercing)
            {
                currentPierceCount--;
                if (currentPierceCount <= 0)
                {
                    ReturnPool();
                }
            }
            else
            {
                ReturnPool();
            }
        }


    }

    void ReturnPool()
    {
        SoundManager.instance.PlaySFX(SFXType.Arrow);
        ObjectPoolManager.instance.ReturnObject("poolKey", this.gameObject);
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene Scene, LoadSceneMode mode)
    {
        if (Scene.name == "LobbyScene" || Scene.name == "MainScene")
        {
            ObjectPoolManager.instance.ReturnObject("poolKey", this.gameObject);
        }
    }
}
