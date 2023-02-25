using UnityEngine;
using UnityEngine.AI;
using BehaviorTree;

public class TaskPatrol : Node
{
    #region Private Fields
    private Transform _transform;
    private Animator _animator;
    private NavMeshAgent _navAgent;

    private Transform[] _waypoints;

    private int _currentWaypointIndex;
    private float thresholdDistance = 0.55f;

    private float _waitTime = 1.0f;
    private float _waitCounter = 0.0f;
    private bool _waiting = false;


    #endregion

    #region Constructors
    public TaskPatrol(Transform transform, Transform[] waypoints)
    {
        _transform = transform;
        _animator = transform.GetComponent<Animator>();
        _navAgent = transform.GetComponent<NavMeshAgent>();
        _waypoints = waypoints;

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
        if(_waiting)
        {
            _waitCounter += Time.deltaTime;
            if(_waitCounter >= _waitTime) { _waiting = false; }
        }
        else
        {
            Transform wp = _waypoints[_currentWaypointIndex];
            float dist = Vector3.Distance(_transform.position, wp.position);
            if (Vector3.Distance(_transform.position, wp.position) < thresholdDistance)
            {
                _waitCounter = 0.0f;
                _waiting = true;

                _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;
            }
            else
            {
                _navAgent.SetDestination(wp.position);
                FaceTarget();
            }
        }

        _animator.SetFloat("Speed", _navAgent.velocity.magnitude);
        state = NodeState.RUNNING;
        return state;
    }
    #endregion
}
