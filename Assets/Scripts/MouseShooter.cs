using UnityEngine;

public class MouseShooter : MonoBehaviour
{

    [SerializeField] private Projectile projectile;
    [SerializeField] private Transform gun;
    
    private MonoObjectsPool<Projectile> projPool;

    private void Start()
    {
        projPool = new MonoObjectsPool<Projectile>(projectile);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var proj = projPool.CreateInstance(10);
            proj.OnDestroyed += OnProjectileDestroyed;
            proj.transform.rotation = transform.rotation * projectile.transform.rotation;
            proj.transform.position = gun.position;
        }
    }
    
    private void OnProjectileDestroyed(Projectile obj)
    {
        projPool.RemoveInstance(obj);
    }
}