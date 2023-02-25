using UnityEngine;
using UnityEngine.AI;
using BehaviorTree;

public class CheckEnemyInFOVRange : Node
{
    #region Private Fields
    private Transform _transform;
    private Animator _animator;
    private NavMeshAgent _navAgent;

    private float _fovRange;
    private LayerMask _playerLayerMask = 1 << 7;
    #endregion

    #region Constructors
    public CheckEnemyInFOVRange(Transform transform, float fovRange)
    {
        _transform = transform;
        _animator = transform.GetComponent<Animator>();
        _navAgent = transform.GetComponent<NavMeshAgent>();
        _fovRange = fovRange;

        _animator.SetFloat("MotionSpeed", 1.0f);
    }
    #endregion

    #region Public Methods
    public override NodeState Evaluate()
    {
        object target = GetData("target");
        if(target == null)
        {
            Collider[] colliders = Physics.OverlapSphere(_transform.position, _fovRange, _playerLayerMask);

            if(colliders.Length > 0)
            {
                parent.parent.SetData("target", colliders[0].transform);
                _animator.SetFloat("Speed", _navAgent.velocity.magnitude);

                state = NodeState.SUCCESS;
                return state;
            }

            state = NodeState.FAILURE;
            return state;
        }

        Transform tar = (Transform)target;
        if (Vector3.Distance(_transform.position, tar.position) > _fovRange)
        {
            ClearData("target");

            state = NodeState.FAILURE;
            return state;
        }

        state = NodeState.SUCCESS;
        return state;
    }
    #endregion
}
