using UnityEngine;
using UnityEngine.InputSystem;

public class Pierce : Weapon
{
    [SerializeField] Transform firePosition;

    [SerializeField] GameObject projectile;


    protected override void Attack()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame && canAttack)
        {
            GameObject arr = ObjectPoolManager.instance.GetObject(projectile.name);
            arr.transform.position = firePosition.position;
            arr.transform.rotation = transform.rotation;
            arr.GetComponent<Arrow>().SetDamage();
        }
    }

    protected override void Start()
    {
        base.Start();
        camera = Camera.main;
    }

    void Update()
    {
        LookMouse();
        Attack();
    }

}
