using UnityEngine;
using UnityEngine.AI;
using BehaviorTree;

public class GuardBT : BehaviorTree.Tree
{
    #region Private Fields
    [SerializeField] Transform[] waypoints;

    public AudioClip[] FootstepAudioClips;
    [Range(0, 1)] public float FootstepAudioVolume = 0.5f;
    #endregion

    #region Public Fields

    #endregion

    #region Monobehavior Callbacks

    #endregion

    #region Private Methods
    protected override Node SetupTree()
    {
        Node root = new TaskPatrol(transform, waypoints);
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
