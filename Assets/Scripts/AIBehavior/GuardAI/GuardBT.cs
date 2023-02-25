using UnityEngine.AI;
using BehaviorTree;

public class GuardBT : Tree
{
    #region Private Fields
    [UnityEngine.SerializeField] UnityEngine.Transform[] waypoints;
    #endregion

    #region Public Fields

    #endregion

    #region Monobehavior Callbacks

    #endregion

    #region Private Methods
    protected override Node SetupTree()
    {
        NavMeshAgent navAgent = GetComponent<NavMeshAgent>();
        Node root = new TaskPatrol(navAgent, transform, waypoints);
    }
    #endregion

    #region Public Methods

    #endregion
}
