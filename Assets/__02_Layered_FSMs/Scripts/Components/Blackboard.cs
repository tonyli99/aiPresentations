using UnityEngine;

namespace BIG.LayeredFSMs
{

    /// <summary>
    /// Shared data used by FSMs.
    /// I'm playing a little loose with the idea of a blackboard here since it
    /// also holds references to components for convenience.
    /// </summary>
    public class Blackboard : MonoBehaviour
    {
        // Note: Public to make it easy to see in inspector.
        // Ideally they'd be private with [SerializeField], but keeping code simple.
        public Health target;
        public bool hasDestination;
        public Transform destinationTransform;
        public Vector3 destinationPosition;
        public float stoppingDistance;
        public bool isAtDestination;

        public Health Health { get; private set; }
        public Gun Gun { get; private set; }
        public Locomotion Locomotion { get; private set; }
        public Animator Animator { get; private set; }
        public bool IsAlive { get { return Health.currentHealth > 0; } }
        public int CurrentHealth { get { return Health.currentHealth; } }
        public int CurrentAmmo { get { return Gun.currentAmmo; } }
        public float MoveSpeed { get { return Locomotion.moveSpeed; } }

        private void Awake()
        {
            Health = GetComponent<Health>();
            Gun = GetComponent<Gun>();
            Locomotion = GetComponent<Locomotion>();
            Animator = GetComponentInChildren<Animator>();
        }

    }
}
