using System;
using UnityEngine;

[RequireComponent(typeof(Creature))]
public abstract class AIState : MonoBehaviour
{
    [SerializeField] private bool isDefault = false;
    public bool IsDefault => isDefault;
    public bool Active { get; set; } = false;
    protected Creature creature;

    public abstract event Action<AIState> OnStateEnded;
    public abstract event Action<AIState> OnStateStarted;

    protected virtual void Awake()
    {
        creature = GetComponent<Creature>();
    }
}