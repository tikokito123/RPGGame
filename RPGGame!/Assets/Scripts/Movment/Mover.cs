﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Core;
using RPG.Saving;

namespace RPG.Movement 
{
    public class Mover : MonoBehaviour, IAction, ISaveable
    {
        [SerializeField] float maxSpeed = 6f;
        NavMeshAgent navMesh;
        Health health;
        void Start()
        {
            navMesh = GetComponent<NavMeshAgent>();
            health = GetComponent<Health>();
        }
        void Update()
        {
            navMesh.enabled = !health.IsDead();
            UpdateAnimator();
        }
        public void StartMoveAction(Vector3 destination, float SpeedFraction)
        {

            MoveTo(destination, SpeedFraction);
            GetComponent<ActionShcelduler>().StartAction(this);
        }
        public void MoveTo(Vector3 destination, float speedFraction)
        {
            navMesh.destination = destination;
            navMesh.speed = maxSpeed * Mathf.Clamp01(speedFraction);
            navMesh.isStopped = false;
        }
        public void Cancel()
        {
            navMesh.isStopped = true;
        }
        private void UpdateAnimator()
        {
            Vector3 velocity = navMesh.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("ForwardSpeed", speed);
        }

        public object CaptureState()
        {
            return new SerializableVector3(transform.position);
        }

        public void RestoreState(object state)
        {
            SerializableVector3 position = (SerializableVector3)state;
            GetComponent<NavMeshAgent>().enabled = false;
            transform.position  = position.ToVector();
            GetComponent<NavMeshAgent>().enabled = true;
        }
    }
}

