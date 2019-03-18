using System.Collections;
using UnityEngine;


[RequireComponent(typeof(WeaponSystem))]
public class MouseShooter : MonoBehaviour
{
    [SerializeField] private Transform gun;

    private WeaponSystem weaponSystem;

    private bool cooldown = false;

    private void Start()
    {
        weaponSystem = GetComponent<WeaponSystem>();
    }

    private void Update()
    {
        if (!cooldown && Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        cooldown = true;

        var proj = weaponSystem.CurrentWeapon.ProjPool.CreateInstance(weaponSystem.CurrentWeapon.Projectile.Lifetime);
        proj.OnDestroyed += weaponSystem.CurrentWeapon.DestroyProj;
        proj.transform.rotation = transform.rotation * weaponSystem.CurrentWeapon.Projectile.transform.rotation;
        proj.transform.position = gun.position;
        weaponSystem.PlayShot();

        yield return new WaitForSeconds(weaponSystem.CurrentWeapon.Cooldown);
        cooldown = false;
    }
}