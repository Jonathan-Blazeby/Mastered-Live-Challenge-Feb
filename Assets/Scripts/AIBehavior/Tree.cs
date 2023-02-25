using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviorTree
{
    public abstract class Tree : MonoBehaviour
    {
        #region Fields
        private Node _root = null;


        #endregion

        #region Monobehavior Callbacks
        protected void Start()
        {
            _root = SetupTree();
        }

        private void Update()
        {
            if (_root != null) _root.Evaluate();
        }
        #endregion

        #region Private Methods
        protected abstract Node SetupTree();
        #endregion
    }
}

