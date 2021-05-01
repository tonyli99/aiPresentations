using UnityEngine;

namespace BIG.LayeredFSMs
{

    /// <summary>
    /// FSM: Observes the blackboard target value.
    /// States: Idle <--> Approach <--> Fire.
    /// </summary>
    public class AttackFSM : BlackboardClient
    {
        private enum State { Idle, Approach, Fire}

        [ReadOnly] [SerializeField] private State state = State.Idle;

        private float timeUntilFire;

        private void Update()
        {
            switch (state)
            {
                case State.Idle:
                    state = UpdateIdleState();
                    break;
                case State.Approach:
                    state = UpdateApproachState();
                    break;
                case State.Fire:
                    state = UpdateFireState();
                    break;
            }
        }

        private State UpdateIdleState()
        {
            if (Blackboard.target != null)
            {
                EnterApproachState();
                return State.Approach;
            }
            return State.Idle;
        }

        private void EnterApproachState()
        {
            Blackboard.destinationTransform = Blackboard.target.transform;
            Blackboard.stoppingDistance = Gun.FiringDistance;
            Blackboard.hasDestination = true;
            Blackboard.isAtDestination = false;
        }

        private State UpdateApproachState()
        {
            if (Blackboard.target == null)
            {
                Blackboard.hasDestination = false;
                return State.Idle;
            }
            else if (Blackboard.isAtDestination)
            {
                timeUntilFire = Gun.FireRate;
                return State.Fire;
            }
            return State.Approach;
        }

        private State UpdateFireState()
        {
            Blackboard.hasDestination = false;
            if (Blackboard.target == null)
            {
                return State.Idle;
            }
            else
            {
                var directionVector = (Blackboard.target.transform.position - transform.position).normalized;
                Blackboard.Locomotion.Model.up = directionVector;
                timeUntilFire -= Time.deltaTime;
                if (timeUntilFire <= 0)
                {
                    timeUntilFire = Gun.FireRate;
                    Blackboard.Gun.Fire();
                }
                return State.Fire;
            }
        }

    }
}
