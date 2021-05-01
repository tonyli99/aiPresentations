using UnityEngine;

namespace BIG.LayeredFSMs
{

    /// <summary>
    /// Base class for FSMs that provides a reference to the blackboard.
    /// </summary>
    public class BlackboardClient : MonoBehaviour
    {

        public Blackboard Blackboard { get; private set; }

        protected virtual void Awake()
        {
            Blackboard = GetComponent<Blackboard>();
        }

    }
}
