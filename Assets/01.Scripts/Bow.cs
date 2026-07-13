using UnityEngine;
using UnityEngine.InputSystem;

public class Bow : Weapon
{
    [SerializeField] Transform firePosition;

    [SerializeField] GameObject Arrow;

    protected override void Attack()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame && canAttack)
        {
            GameObject arr = ObjectPoolManager.instance.GetObject("Arrow");
            arr.transform.position = firePosition.position;
            arr.transform.rotation = transform.rotation;
            arr.GetComponent<Arrow>().SetDamage(5);
            canAttack = false;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
