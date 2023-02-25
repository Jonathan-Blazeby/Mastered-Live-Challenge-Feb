using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskAttack : Node
{
    #region Private Fields
    private float _attackTime;
    private float _currentTime;
    #endregion

    #region Constructors
    public TaskAttack(float attackTime) 
    {
        _attackTime = attackTime;
    }
    #endregion

    #region Public Methods
    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");

        float newTime = Time.realtimeSinceStartup - _currentTime;
        if(newTime >= _attackTime)
        {
            _currentTime = Time.realtimeSinceStartup;
            bool isDead = target.GetComponent<PlayerHealth>().TakeDamage();
            if(isDead)
            {
                ClearData("target");

                state = NodeState.FAILURE;
                return state;
            }
        }

        state = NodeState.RUNNING;
        return state;
    }
    #endregion
}
