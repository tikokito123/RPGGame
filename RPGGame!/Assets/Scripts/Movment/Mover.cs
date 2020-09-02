using RPG.Combat;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace RPG.Movement 
{
    public class Mover : MonoBehaviour
    {
        NavMeshAgent navMesh;
        void Start()
        {
            navMesh = GetComponent<NavMeshAgent>();
        }
        void Update()
        {
            UpdateAnimator();
        }
        public void StartMoveAction(Vector3 destination)
        {
            MoveTo(destination);
            GetComponent<Fighter>().Cancel();
        }
        public void MoveTo(Vector3 destination)
        {
            navMesh.destination = destination;
            navMesh.isStopped = false;
        }
        public void Stop()
        {
            navMesh.isStopped = true;
        }
        private void UpdateAnimator()
        {
            Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("ForwardSpeed", speed);
        }
    }
}

