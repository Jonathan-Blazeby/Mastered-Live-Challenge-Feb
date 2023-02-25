using UnityEngine;
using UnityEngine.AI;
using BehaviorTree;

public class CheckEnemyInAttackRange : Node
{
    #region Private Fields
    private Transform _transform;
    private Animator _animator;
    private NavMeshAgent _navAgent;

    private float _attackRange;
    #endregion

    #region Constructors
    public CheckEnemyInAttackRange(Transform transform, float attackRange)
    {
        _transform = transform;
        _animator = transform.GetComponent<Animator>();
        _navAgent = transform.GetComponent<NavMeshAgent>();
        _attackRange = attackRange;

        _animator.SetFloat("MotionSpeed", 1.0f);
    }
    #endregion

    #region Public Methods
    public override NodeState Evaluate()
    {
        object target = GetData("target");
        if (target == null)
        {
            state = NodeState.FAILURE;
            return state;
        }

        Transform tar = (Transform)target;
        float dist = Vector3.Distance(_transform.position, tar.position);
        if (Vector3.Distance(_transform.position, tar.position) <= _attackRange)
        {
            state = NodeState.SUCCESS;
            return state;
        }

        state = NodeState.FAILURE;
        return state;
    }
    #endregion
}
