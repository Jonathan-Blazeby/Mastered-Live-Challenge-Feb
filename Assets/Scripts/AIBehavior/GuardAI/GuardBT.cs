using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorTree;

public class GuardBT : BehaviorTree.Tree
{
    #region Private Fields
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float fovRange = 4.0f;
    [SerializeField] private float attackRange = 1.0f;
    [SerializeField] private float attackTime = 1.0f;
    [SerializeField] private float patrolSpeed = 2.0f;
    [SerializeField] private float chaseSpeed = 3.5f;

    public AudioClip[] FootstepAudioClips;
    [Range(0, 1)] public float FootstepAudioVolume = 0.35f;
    #endregion


    #region Private Methods
    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new CheckEnemyInAttackRange(transform, attackRange),
                new TaskAttack(attackTime),
            }),
            new Sequence(new List<Node>
            {
                new CheckEnemyInFOVRange(transform, fovRange),
                new TaskGoToTarget(transform, chaseSpeed),
            }),
            new TaskPatrol(transform, waypoints, patrolSpeed),
        });

        return root;
    }

    private void OnFootstep(AnimationEvent animationEvent)
    {
        if (animationEvent.animatorClipInfo.weight > 0.5f)
        {
            if (FootstepAudioClips.Length > 0)
            {
                var index = Random.Range(0, FootstepAudioClips.Length);
                AudioSource.PlayClipAtPoint(FootstepAudioClips[index], transform.position, FootstepAudioVolume);
            }
        }
    }
    #endregion

    #region Public Methods

    #endregion
}
