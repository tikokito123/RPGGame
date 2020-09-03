using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        Transform target;
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float timeSinceLastAttack = 0f;
        [SerializeField] float damage = 5f;
        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            if (target == null) return;
            if (!GetIsInDistance())
            {
                GetComponent<Mover>().MoveTo(target.position);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }
        private void AttackBehaviour()
        {
            if (timeSinceLastAttack >= timeBetweenAttacks)
            {
                GetComponent<Animator>().SetTrigger("Attack");
                timeSinceLastAttack = 0;
            }
        }
        //animation event
        void Hit()
        {
            try
            {
                target.GetComponent<Health>().HealthDamage(damage);

            }
            catch
            {
                return;
            }
        }
        private bool GetIsInDistance()
        {
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }
        public void Attack(CombatTarget CombatTarget)
        {
            target = CombatTarget.transform;
            GetComponent<ActionShcelduler>().StartAction(this);
        } 
        public void Cancel()
        {
            target = null;
            GetComponent<Animator>().SetTrigger("StopAttack");

        }
    }
}

