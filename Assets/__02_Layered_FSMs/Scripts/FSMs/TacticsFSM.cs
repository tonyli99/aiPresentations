using UnityEngine;

namespace BIG.LayeredFSMs
{

    /// <summary>
    /// FSM: Decides what to do.
    /// </summary>
    public class TacticsFSM : BlackboardClient
    {
        private enum State { Idle, Engage, GetHealth, GetAmmo }

        [ReadOnly] [SerializeField] private State state = State.Idle;

        private void Update()
        {
            switch (state)
            {
                case State.Idle:
                    state = UpdateIdleState();
                    break;
                case State.Engage:
                    state = UpdateEngageState();
                    break;
                case State.GetHealth:
                    state = UpdateGetHealthState();
                    break;
                case State.GetAmmo:
                    state = UpdateGetAmmoState();
                    break;
            }
        }

        private State UpdateIdleState()
        {
            if (IsHealthLow())
            {
                if (FindPickup<HealthPickup>())
                {
                    return State.GetHealth;
                }
            }
            else if (Blackboard.CurrentAmmo == 0)
            {
                if (FindPickup<AmmoPickup>())
                {
                    return State.GetAmmo;
                }
            }
            else if (FindZombie())
            {
                return State.Engage;
            }
            return State.Idle;
        }

        private State UpdateEngageState()
        {
            if (IsHealthLow())
            {
                if (FindPickup<HealthPickup>())
                {
                    return State.GetHealth;
                }
            }
            else if (Blackboard.CurrentAmmo == 0)
            {
                if (FindPickup<AmmoPickup>())
                {
                    return State.GetAmmo;
                }
            }
            else if (Blackboard.target == null || Blackboard.target.currentHealth <= 0)
            {
                Blackboard.target = null;
                return State.Idle;
            }
            return State.Engage;
        }

        private bool IsHealthLow()
        {
            return Blackboard.CurrentHealth < (0.25f * Health.MaxHealth);
        }

        private bool FindPickup<T>() where T : Pickup
        {
            T closest = null;
            float closestDistance = Mathf.Infinity;
            foreach (T candidate in FindObjectsOfType<T>())
            {
                if (candidate.IsAvailable())
                {
                    float candidateDistance = Vector3.Distance(candidate.transform.position, transform.position);
                    if (candidateDistance < closestDistance)
                    {
                        closest = candidate;
                        closestDistance = candidateDistance;
                    }
                }
            }
            if (closest != null)
            {
                Blackboard.destinationTransform = closest.transform;
                Blackboard.stoppingDistance = 0;
                Blackboard.hasDestination = true;
                Blackboard.isAtDestination = false;
            }
            return (closest != null);
        }

        private bool FindZombie()
        {
            Health closest = null;
            float closestDistance = Mathf.Infinity;
            foreach (Zombie candidate in FindObjectsOfType<Zombie>())
            {
                var candidateHealth = candidate.GetComponent<Health>();
                if (candidateHealth.currentHealth > 0)
                {
                    float candidateDistance = Vector3.Distance(candidate.transform.position, transform.position);
                    if (candidateDistance < closestDistance)
                    {
                        closest = candidateHealth;
                        closestDistance = candidateDistance;
                    }
                }
            }
            if (closest != null)
            {
                Blackboard.target = closest;
            }
            return (closest != null);
        }

        private State UpdateGetHealthState()
        {
            if (Blackboard.isAtDestination || !IsHealthLow())
            {
                Blackboard.hasDestination = false;
                return State.Idle;
            }
            return State.GetHealth;
        }

        private State UpdateGetAmmoState()
        {
            if (Blackboard.isAtDestination || Blackboard.CurrentAmmo > 0)
            {
                Blackboard.hasDestination = false;
                return State.Idle;
            }
            return State.GetAmmo;
        }

    }
}
