using UnityEngine;

namespace BIG.LayeredFSMs
{

    /// <summary>
    /// FSM: Moves to a destination. Observes blackboard's destination values. Sets isAtDestination when done.
    /// States: Idle <--> Navigate
    /// </summary>
    public class NavigationFSM : BlackboardClient
    {

        private enum State { Idle, Navigate }

        [ReadOnly] [SerializeField] private State state = State.Idle;

        private const float DistanceRoundoff = 0.01f; // Floating point leeway for distance calculations.

        private void Update()
        {
            switch (state)
            {
                case State.Idle:
                    state = UpdateIdleState();
                    break;
                case State.Navigate:
                    state = UpdateNavigateState();
                    break;
            }
        }

        private State UpdateIdleState()
        {
            if (Blackboard.hasDestination)
            {
                return State.Navigate;
            }
            else
            {
                return State.Idle;
            }
        }

        private State UpdateNavigateState()
        {
            if (Blackboard.destinationTransform != null)
            {
                Blackboard.destinationPosition = Blackboard.destinationTransform.position;
            }
            var currentDistance = Vector3.Distance(transform.position, Blackboard.destinationPosition);

            if (currentDistance > (Blackboard.stoppingDistance + DistanceRoundoff))
            {
                Blackboard.isAtDestination = false;
                var directionVector = (Blackboard.destinationPosition - transform.position).normalized;
                Blackboard.Locomotion.Model.up =  directionVector;
                transform.Translate(directionVector * Blackboard.MoveSpeed * Time.deltaTime);
                Blackboard.Animator.SetBool("isRunning", true);
                return State.Navigate;
            }
            else
            {
                Blackboard.isAtDestination = true;
                Blackboard.Animator.SetBool("isRunning", false);
                return State.Idle;
            }
        }

    }
}
