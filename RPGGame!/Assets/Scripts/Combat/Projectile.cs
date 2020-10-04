﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using UnityEditor;
using UnityEngine.XR.WSA.Input;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 0.5f;
        [SerializeField] bool isHoming = true;
        [SerializeField] GameObject hitEffects = null;
        [SerializeField] float maxLifeTime = 3f;
        [SerializeField] GameObject[] destroyOnHit = null;
        [SerializeField] float lifeAfterImpact = 2f;

        Health target = null;
        float damage = 0;
        private void Start()
        {
            transform.LookAt(GetAimLoacation());
        }
        void Update()
        {
            if (target == null) return;
            if (isHoming && !target.IsDead())
            {
                transform.LookAt(GetAimLoacation());
            }
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        public void SetTarget(Health target, float damage)
        {
            this.target = target;
            this.damage = damage;
            Destroy(gameObject, maxLifeTime);
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
            if (target.IsDead()) return;
            target.HealthDamage(damage);
            speed = 0;
            if (hitEffects != null)
            {
                var impact = Instantiate(hitEffects, GetAimLoacation(), transform.rotation);
                Destroy(impact, 2f);
            }
            foreach (GameObject toDestroy in destroyOnHit)
            {
                Destroy(toDestroy, lifeAfterImpact);
            }
        }
    }

}

