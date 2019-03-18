using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIStateSwitcher : MonoBehaviour
{
    private List<AIState> states;
    private AIState prevState;
    private AIState defaultState;

    private void Start()
    {
        states = GetComponents<AIState>().ToList();

        foreach (var state in states)
        {
            state.OnStateStarted += StartState;
            state.OnStateEnded += EndState;
            if (state.IsDefault)
            {
                state.Active = true;
                prevState = state;
                defaultState = state;
            }
            else
                state.Active = false;
        }
    }

    private void EndState(AIState state)
    {
        prevState = state;
        StartState(defaultState);
    }
    
    private void StartState(AIState state)
    {
        prevState.Active = false;
        state.Active = true;
        prevState = state;
    }
}