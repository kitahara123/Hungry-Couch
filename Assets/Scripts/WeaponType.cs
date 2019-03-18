using System;
using UnityEngine;

[Serializable]
public class WeaponType
{
    [SerializeField] private Sprite body;
    [SerializeField] private AudioClip shotSound;
    [SerializeField] private AudioClip pullSound;
    [SerializeField] private Projectile projectile;
    [SerializeField] private string hotKey;
    [SerializeField] private float cooldown;
    private MonoObjectsPool<Projectile> projPool;

    public Sprite Body => body;

    public AudioClip ShotSound => shotSound;

    public AudioClip PullSound => pullSound;

    public Projectile Projectile => projectile;

    public string HotKey => hotKey;

    public float Cooldown => cooldown;

    public MonoObjectsPool<Projectile> ProjPool => projPool ?? (projPool = new MonoObjectsPool<Projectile>(projectile));

    public void DestroyProj(Projectile obj)
    {
        ProjPool.RemoveInstance(obj);
    }
}