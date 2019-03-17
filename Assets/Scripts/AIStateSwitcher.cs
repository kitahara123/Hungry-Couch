using System.Collections.Generic;
using UnityEngine;

public class AIStateSwitcher : MonoBehaviour
{
    private List<AIState> states;
    private bool locked;
    private AIState prevState;
    private AIState defaultState;

    private void Start()
    {
        states = new List<AIState>();

        foreach (var component in GetComponents<AIState>())
        {
            states.Add(component);
        }

        Debug.Log(states.Count);

        foreach (var state in states)
        {
            if (state.isDefault)
            {
                state.enabled = true;
                prevState = state;
                defaultState = state;
            }
            else
                state.enabled = false;
        }
    }

    private void Update()
    {
        if (locked) return;

        foreach (var state in states)
        {
            if (state.IsSwitchNeeded())
            {
                locked = true;
                prevState.enabled = false;
                state.enabled = true;

                state.OnStateEnded += EndState;
                return;
            }
        }
    }

    private void EndState(AIState state)
    {
        state.enabled = false;
        prevState = state;
        defaultState.enabled = true;
        locked = false;
    }
}