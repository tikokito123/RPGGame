using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;
using UnityEngine.UIElements;

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
            transform.LookAt(target.transform);
            if (timeSinceLastAttack >= timeBetweenAttacks)
            {
                TriggerAttackAnimation();
                timeSinceLastAttack = 0;
            }
        }

        private void TriggerAttackAnimation()
        {
            GetComponent<Animator>().ResetTrigger("StopAttack");
            GetComponent<Animator>().SetTrigger("Attack");
        }

        public bool CanAttack(CombatTarget combatTarget)
        {
            if (combatTarget == null) { return false; }
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }
        //animation event
        void Hit()
        {
            if (target == null) return;
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
            StopAttackAnimation();
            target = null;

        }
        private void StopAttackAnimation()
        {
            GetComponent<Animator>().ResetTrigger("Attack");
            GetComponent<Animator>().SetTrigger("StopAttack");
        }
    }
}

