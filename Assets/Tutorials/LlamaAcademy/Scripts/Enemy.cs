using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace LlamAcademy
{
    public class Enemy : PoolableObject
    {

        public EnemyMovement Movement;
        public NavMeshAgent Agent;
        public EnemyScriptableObject EnemyScriptableObject;
        public int Health = 100;


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


        }

    }

}
