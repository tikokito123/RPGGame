using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 0.5f;
    Health target = null;
    void Update()
    {
        if (target == null) return;
        transform.LookAt(GetAimLoacation());
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    public void SetTarget(Health target)
    {
        this.target = target;
    }
    private Vector3 GetAimLoacation()
    {
        CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
        if (targetCapsule == null) return target.transform.position;
        return target.transform.position + Vector3.up * targetCapsule.height / 2;
    }
}
