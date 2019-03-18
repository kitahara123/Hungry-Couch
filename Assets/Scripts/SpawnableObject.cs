using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс с настройками юнита для спавнера 
/// </summary>
[Serializable]
public class SpawnableObject
{
    [SerializeField] private Creature prefab;
    [Tooltip("Максимальное количество на сцене")]
    [SerializeField] private int quantity;
    public int Quantity => quantity;
    [SerializeField] private int scoreDelta = 1;

    private MonoObjectsPool<Creature> pool;
    private MonoObjectsPool<Creature> Pool => pool ?? (pool = new MonoObjectsPool<Creature>(prefab));

    private HashSet<Creature> activeInstances;
    private HashSet<Creature> ActiveInstances => activeInstances ?? (activeInstances = new HashSet<Creature>());

    public int CountActiveInstances => ActiveInstances.Count;
        
    public Creature AddInstance()
    {
        var instance = Pool.CreateInstance();
        instance.Reset();
        ActiveInstances.Add(instance);
        instance.OnDeath += OnDeath;
        return instance;
    }

    private void OnDeath(Creature instance)
    {
        Pool.RemoveInstance(instance);
        ActiveInstances.Remove(instance);
        Messenger<int>.Broadcast(GameEvent.SCORE_EARNED, scoreDelta);
        instance.OnDeath -= OnDeath;
    }


}