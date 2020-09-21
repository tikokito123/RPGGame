using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using UnityEditor;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 0.5f;
    Health target = null;
    float damage = 0;
    void Update()
    {
        if (target == null) return;
        transform.LookAt(GetAimLoacation());
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    public void SetTarget(Health target, float damage)
    {
        this.target = target;
        this.damage = damage;
    }
    private Vector3 GetAimLoacation()
    {
        CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
        if (targetCapsule == null) return target.transform.position;
        return target.transform.position + Vector3.up * targetCapsule.height / 2;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Health>() != target) return;
        target.HealthDamage(damage);
        Destroy(gameObject, 0.45f);
    }
}
