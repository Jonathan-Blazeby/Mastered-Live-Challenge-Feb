using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    //Functions as somewhat of an OR gate - returns if a child has succeeded or is running
    public class Selector : Node
    {
        #region Constructors
        public Selector() : base() { }
        public Selector(List<Node> children) : base(children) { }
        #endregion

        #region Public Methods
        public override NodeState Evaluate()
        {
            foreach (Node node in children)
            {
                switch (node.Evaluate())
                {
                    case NodeState.FAILURE:
                        continue;
                    case NodeState.SUCCESS:
                        state = NodeState.SUCCESS;
                        return state;
                    case NodeState.RUNNING:
                        state = NodeState.RUNNING;
                        return state;
                    default:
                        continue;
                }
            }

            state = NodeState.FAILURE;
            return state;
        }
        #endregion
    }
}
