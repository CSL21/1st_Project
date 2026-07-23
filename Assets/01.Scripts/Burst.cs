using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;


public class Burst : Weapon
{
    [SerializeField] Transform firePosition;
    [SerializeField] GameObject Bullet;

    [Header("Burst Settings")]
    [SerializeField] private int burstCount = 3;
    [SerializeField] private float burstInterval = 0.08f;

    private bool isBursting = false;

    protected override void Attack()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame && canAttack && !isBursting)
        {
            StartCoroutine(BurstFireRoutine());
        }
    }


    private IEnumerator BurstFireRoutine()
    {
        isBursting = true;

        for (int i = 0; i < burstCount; i++)
        {
            GameObject arr = ObjectPoolManager.instance.GetObject("Bullet");
            if (arr != null)
            {
                arr.transform.position = firePosition.position;
                arr.transform.rotation = transform.rotation;
                arr.GetComponent<Arrow>().SetDamage();
            }

            if (i < burstCount - 1)
            {
                yield return new WaitForSeconds(burstInterval);
            }
        }

        isBursting = false;

    }


    protected override void Start()
    {
        base.Start();
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        LookMouse();
        Attack();
    }

}
