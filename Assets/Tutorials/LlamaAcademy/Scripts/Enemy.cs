using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace LlamAcademy
{
    public class Enemy : PoolableObject, IDamageable
    {
        public EnemyAttackRadius attackRadius;
        public Animator animator;
        private Coroutine lookCoroutine;

        public EnemyMovement Movement;
        public NavMeshAgent Agent;
        public EnemyScriptableObject EnemyScriptableObject;
        public int Health = 100;

        private const string ATTACK_TRIGGER = "Attack";

        private void Awake()
        {
            attackRadius.OnAttack += OnAttack;
        }

        private void OnAttack(IDamageable target)
        {
            animator.SetTrigger(ATTACK_TRIGGER);

            if (lookCoroutine != null)
            {
                StopCoroutine(lookCoroutine);
            }

            lookCoroutine = StartCoroutine(LookAt(target.GetTransform()));

        }

        private IEnumerator LookAt(Transform targetTrans)
        {
            Quaternion lookRotation = Quaternion.LookRotation(targetTrans.position - transform.position);
            yield break;
        }



        public virtual void OnEnable()
        {
            SetupAgentFromConfiguration();
        }
        public override void OnDisable()
        {
            base.OnDisable();
            Agent.enabled = false;
        }

        public virtual void SetupAgentFromConfiguration()
        {
            Agent.acceleration = EnemyScriptableObject.Acceleration;
            Agent.angularSpeed = EnemyScriptableObject.AngularSpeed;
            Agent.areaMask = EnemyScriptableObject.AreaMask;
            Agent.avoidancePriority = EnemyScriptableObject.AvoidancePriority;
            Agent.obstacleAvoidanceType = EnemyScriptableObject.ObstacleAvoidanceType;
            Agent.radius = EnemyScriptableObject.Radius;
            Agent.speed = EnemyScriptableObject.Speed;
            Agent.stoppingDistance = EnemyScriptableObject.StoppingDistance;

            Movement.updateRate = EnemyScriptableObject.AIUpdateInterval;

            Health = EnemyScriptableObject.Health;

            attackRadius.RadiusCollider.radius = EnemyScriptableObject.AttackRadius;
            attackRadius.AttackDelay = EnemyScriptableObject.AttackDelay;
            attackRadius.DamageValue = EnemyScriptableObject.Damage;


        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            if(Health <= 0)
            {
                this.gameObject.SetActive(false);
            }
        }

        public Transform GetTransform()
        {
            return this.transform;
        }
    }

}
