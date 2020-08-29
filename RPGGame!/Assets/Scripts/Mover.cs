using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    [SerializeField] Transform target;
    NavMeshAgent navMesh;
    void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        navMesh.destination = target.position;
    }
}
