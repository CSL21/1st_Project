using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;


public class Multi : Weapon
{
    [SerializeField] Transform firePosition;

    [SerializeField] GameObject projectile;

    [Header("Multi Settings")]
    [SerializeField] private int arrowCount = 3;
    [SerializeField] private float spreadAngle = 15f;

    
    protected override void Attack()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame && canAttack)
        {
            FireMultiShot();
        }
    }


    private void FireMultiShot()
    {
        float startAngle = -((arrowCount - 1) * spreadAngle) / 2f;

        for (int i = 0; i < arrowCount; i++)
        {
            GameObject arr = ObjectPoolManager.instance.GetObject(projectile.name);
            if (arr != null)
            {
                arr.transform.position = firePosition.position;
                float currentSpreadAngle = startAngle + (i * spreadAngle);
                Quaternion spreadRotation = transform.rotation * Quaternion.Euler(0, 0, currentSpreadAngle);
                arr.transform.rotation = spreadRotation;
                arr.GetComponent<Arrow>().SetDamage();
            }
        }
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
