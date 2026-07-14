using UnityEngine;
using UnityEngine.InputSystem;

public class Burst : Weapon
{
    [SerializeField] Transform firePosition;

    [SerializeField] GameObject Bullet;

    protected override void Attack()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame && canAttack)
        {
            GameObject arr = ObjectPoolManager.instance.GetObject("Bullet");
            arr.transform.position = firePosition.position;
            arr.transform.rotation = transform.rotation;
            arr.GetComponent<Bullet>().SetDamage(5);
            canAttack = false;
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
