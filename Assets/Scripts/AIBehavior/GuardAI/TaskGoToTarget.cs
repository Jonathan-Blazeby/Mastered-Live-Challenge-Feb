using UnityEngine;
using UnityEngine.AI;
using BehaviorTree;

public class TaskGoToTarget : Node
{
    #region Private Fields
    private Transform _transform;
    private Animator _animator;
    private NavMeshAgent _navAgent;

    private float _speed;

    private float thresholdDistance = 0.55f;
    #endregion

    #region Constructors
    public TaskGoToTarget(Transform transform, float speed)
    {
        _transform = transform;
        _animator = transform.GetComponent<Animator>();
        _navAgent = transform.GetComponent<NavMeshAgent>();
        _speed = speed;

        _animator.SetFloat("MotionSpeed", 1.0f);
    }
    #endregion

    #region Private Methods
    private void FaceTarget()
    {
        var turnTowardNavSteeringTarget = _navAgent.steeringTarget;

        Vector3 direction = (turnTowardNavSteeringTarget - _transform.position).normalized;
        if (direction.x != 0 && direction.z != 0) //Eliminates unnecessary calculations
        {
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            _transform.rotation = Quaternion.Slerp(_transform.rotation, lookRotation, Time.deltaTime * 5);
        }
    }
    #endregion

    #region Public Methods
    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");

        if(Vector3.Distance(_transform.position, target.position) > thresholdDistance)
        {
            _navAgent.SetDestination(target.position);
            _navAgent.speed = _speed;
            FaceTarget();
        }

        _animator.SetFloat("Speed", _navAgent.velocity.magnitude);
        state = NodeState.RUNNING;
        return state;
    }
    #endregion
}
