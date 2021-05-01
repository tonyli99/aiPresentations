using UnityEngine;

namespace BIG.LayeredFSMs
{

    /// <summary>
    /// Just holds references to the actor's move speed and model.
    /// Navigation rotates the model in the direction of movement.
    /// </summary>
    public class Locomotion : MonoBehaviour
    {
        public float moveSpeed = 3;

        public Transform Model { get; private set; }

        private void Awake()
        {
            Model = GetComponentInChildren<Animator>().transform;
        }
    }
}
