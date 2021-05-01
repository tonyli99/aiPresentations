using System;
using System.Collections.Generic;
using UnityEngine;

namespace BIG.LayeredFSMs
{

    public class Zombie : MonoBehaviour
    {

        private enum State { Idle, Chase, Attack, Dead }

        [ReadOnly] [SerializeField] private State state = State.Idle;
        [ReadOnly] [SerializeField] private Health target;

        private Health health;
        private Transform model;
        private float idleTimeLeft;
        private float biteTimeLeft;

        private const float MoveSpeed = 2;
        private const float IdleDuration = 5;
        private const float BiteDuration = 1;
        private const float BiteRange = 0.5f;
        private const int BiteDamage = 10;

        private void Awake()
        {
            health = GetComponent<Health>();
            model = GetComponentInChildren<Animator>().transform;
        }

        private void Update()
        {
            switch (state)
            {
                case State.Idle:
                    state = UpdateIdleState();
                    break;
                case State.Chase:
                    state = UpdateChaseState();
                    break;
                case State.Attack:
                    state = UpdateAttackState();
                    break;
                case State.Dead:
                    break;
            }
        }

        private void EnterIdleState()
        {
            state = State.Idle;
            target = null;
            idleTimeLeft = IdleDuration;
        }

        private State UpdateIdleState()
        {
            idleTimeLeft -= Time.deltaTime;
            if (health.currentHealth <= 0)
            {
                return State.Dead;
            }
            else if (target != null)
            {
                return State.Attack;
            }
            else if (idleTimeLeft <= 0)
            {
                return State.Chase;
            }
            else
            {
                return State.Idle;
            }
        }

        private State UpdateChaseState()
        {
            if (health.currentHealth <= 0)
            {
                return State.Dead;
            }
            else if (target == null)
            {
                target = FindTarget();
            }
            if (target == null)
            {
                EnterIdleState();
                return State.Idle;
            }
            else
            {
                var distance = Vector3.Distance(target.transform.position, transform.position);
                if (distance <= BiteRange)
                {
                    return State.Attack;
                }
                else
                {
                    var directionVector = (target.transform.position - transform.position).normalized;
                    model.up = directionVector;
                    transform.Translate(directionVector * MoveSpeed * Time.deltaTime);
                    return State.Chase;
                }
            }
        }

        private State UpdateAttackState()
        {
            if (health.currentHealth <= 0)
            {
                return State.Dead;
            }
            biteTimeLeft -= Time.deltaTime;
            if (biteTimeLeft <= 0)
            {
                biteTimeLeft = BiteDuration;
                var distance = Vector3.Distance(target.transform.position, transform.position);
                if (distance <= BiteRange)
                { 
                    target.currentHealth -= BiteDamage;
                    if (target.currentHealth <= 0)
                    {
                        return State.Idle;
                    }
                }
                else
                {
                    return State.Chase;
                }
            }
            return State.Attack;
        }

        private Health FindTarget()
        {
            Health closest = null;
            float closestDistance = Mathf.Infinity;
            foreach (Locomotion candidate in FindObjectsOfType<Locomotion>())
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
            return closest;
        }

    }
}
