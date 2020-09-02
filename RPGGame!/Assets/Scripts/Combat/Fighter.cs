using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour
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
                GetComponent<Mover>().Stop();
            }
        }

        private bool GetIsInDistance()
        {
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }

        public void Attack(CombatTarget CombatTarget)
        {
            target = CombatTarget.transform;
        } 
        public void Cancel()
        {
            target = null;
        }
    }
}

