using UnityEngine;
using UnityEngine.AI;
using BehaviorTree;

public class TaskPatrol : Node
{
    #region Private Fields
    private Transform[] _waypoints;
    private float _speed = 2.0f;

    private Transform _transform;
    private int _currentWaypointIndex;
    private float minimumAdjustDistance = 0.01f;

    private float _waitTime = 1.0f;
    private float _waitCounter = 0.0f;
    private bool _waiting = false;


    #endregion

    #region Constructors
    public TaskPatrol(NavMeshAgent navAgent, Transform transform, Transform[] waypoints)
    {
        _transform = transform;
        _waypoints = waypoints;
    }
    #endregion

    #region Private Methods

    #endregion

    #region Public Methods
    public override NodeState Evaluate()
    {
        if(_waiting)
        {
            _waitCounter += Time.deltaTime;
            if(_waitCounter < _waitTime) { _waiting = false; }
        }
        else
        {
            Transform wp = _waypoints[_currentWaypointIndex];
            if (Vector3.Distance(_transform.position, wp.position) < minimumAdjustDistance)
            {
                _transform.position = wp.position;
                _waitCounter = 0.0f;
                _waiting = true;

                _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;
            }
            else
            {
                _transform.position = Vector3.MoveTowards(_transform.position, wp.position, _speed * Time.deltaTime);
                _transform.LookAt(wp.position);
            }
        }


        state = NodeState.RUNNING;
        return state;
    }
    #endregion
}
