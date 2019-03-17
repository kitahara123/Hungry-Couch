using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private float speed = 1;
    public event Action<Projectile> OnDestroyed;

    private void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.isTrigger || other.CompareTag("Player")) return;
//        var creature = other.GetComponent<Creature>();
//        if (creature == null)
//        {
//            OnDestroyed?.Invoke(this);
//            return;
//        }

//        if (other.CompareTag(Shooter?.tag)) return;
//        creature.Hurt(damage);
        OnDestroyed?.Invoke(this);
    }

    private void OnDisable()
    {
        foreach (var del in OnDestroyed?.GetInvocationList())
            OnDestroyed -= (Action<Projectile>) del;
    }
}