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
        
        private void Update()
        {
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
            GetComponent<Animator>().SetTrigger("Attack");
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
            GetComponent<Animator>().SetTrigger("Attack");
        }
        //animation event
        void Hit()
        {
        }
    }
}

