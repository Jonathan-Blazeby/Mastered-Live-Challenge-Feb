using System.Collections.Generic;

namespace BehaviorTree
{
    //Functions as somewhat of an AND gate - only if all child nodes succeed will it succeed
    public class Sequence : Node
    {
        #region Constructors
        public Sequence() : base() { }
        public Sequence(List<Node> children) : base(children) { }
        #endregion

        #region Public Methods
        public override NodeState Evaluate()
        {
            bool anyChildIsRunning = false;

            foreach(Node node in children)
            {
                switch(node.Evaluate())
                {
                    case NodeState.FAILURE:
                        state = NodeState.FAILURE;
                        return state;
                    case NodeState.RUNNING:
                        anyChildIsRunning = true;
                        continue;
                    default:
                        state = NodeState.SUCCESS;
                        return state;
                }
            }

            state = anyChildIsRunning ? NodeState.RUNNING : NodeState.SUCCESS;
            return state;
        }
        #endregion
    }
}
