using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stateMachine : MonoBehaviour
{
    public baseState activeState;
    

    public void Initialise()
    {
        
        changeState(new PatrolState());
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (activeState != null)
        {
            activeState.Perform();
        }

    }
    public void changeState(baseState newState)
    {
        if(activeState != null)
        {
            activeState.Exit();

        }
        activeState = newState;

        if(activeState != null)
        {
            activeState.stateMachine = this;
            activeState.enemy = GetComponent<enemy>(); 
            activeState.Enter();
        }
    }
}
