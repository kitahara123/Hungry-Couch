using System;
using UnityEngine;

public abstract class AIState : MonoBehaviour
{
    public bool isDefault = false;
    
    public abstract event Action<AIState> OnStateEnded; 
    
    public abstract bool IsSwitchNeeded();
    
}