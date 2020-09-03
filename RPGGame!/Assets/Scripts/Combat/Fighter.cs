using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float timeSinceLastAttack = 0f;
        [SerializeField] float damage = 5f;
        
        Health target;
        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            if (target == null) return;
            if (target.IsDead()) return;
            if (!GetIsInDistance())
            {
                GetComponent<Mover>().MoveTo(target.transform.position);
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
            target.HealthDamage(damage);
        }
        private bool GetIsInDistance()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }
        public void Attack(CombatTarget CombatTarget)
        {
            target = CombatTarget.GetComponent<Health>();
            GetComponent<ActionShcelduler>().StartAction(this);
        } 
        public void Cancel()
        {
            target = null;
            GetComponent<Animator>().SetTrigger("StopAttack");

        }
    }
}

