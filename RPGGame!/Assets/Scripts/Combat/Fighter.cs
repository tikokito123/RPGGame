using UnityEngine;
using RPG.Movement;
using RPG.Core;
using RPG.Saving;
using RPG.Resources;
using RPG.Stats;
using System.Collections.Generic;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction, ISaveable, IModifierProvider
    {
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] Transform rightHandTransform = null;
        [SerializeField] Transform leftHandTransform = null;
        [SerializeField] Weapon deafultWeapon = null;
        [SerializeField] string deafultWeaponName = "Unarmed";
        Health target;
        float timeSinceLastAttack = Mathf.Infinity;
        Weapon currentWeapon = null;
        void Start()
        {
            if (currentWeapon == null)
            {
               EquipWeapon(deafultWeapon);
            }
        }
        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            if (target == null) return;
            if (target.IsDead()) return;
            if (!GetIsInDistance())
            {
                GetComponent<Mover>().MoveTo(target.transform.position, 1f);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }
        void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if (timeSinceLastAttack >= timeBetweenAttacks)
            {
                TriggerAttackAnimation();
                timeSinceLastAttack = 0;
            }
        }

        public void TriggerAttackAnimation()
        {
            GetComponent<Animator>().ResetTrigger("StopAttack");
            GetComponent<Animator>().SetTrigger("Attack");
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) { return false; }
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }
        //animation event
        void Hit()
        {
            if (target == null) return;
            float damage = GetComponent<BaseStats>().GetStat(Stat.Damage);
            if (currentWeapon.HasProjectile())
            {
                currentWeapon.LunchProjectile(target, rightHandTransform, leftHandTransform, gameObject,damage);
            }
            else
            {
                target.HealthDamage(gameObject, damage);
            }
        }
        
        void Shoot()
        {
            Hit();
        }
        private bool GetIsInDistance()
        {
            return Vector3.Distance(transform.position, target.transform.position) < currentWeapon.GetWeaponRange();
        }
        public void Attack(GameObject CombatTarget)
        {
            GetComponent<ActionShcelduler>().StartAction(this);
            target = CombatTarget.GetComponent<Health>();
        }
        public void Cancel()
        {
            StopAttackAnimation();
            target = null;
            GetComponent<Mover>().Cancel();
        }
        private void StopAttackAnimation()
        {
            GetComponent<Animator>().ResetTrigger("Attack");
            GetComponent<Animator>().SetTrigger("StopAttack");
        }
        public void EquipWeapon(Weapon weapon)
        {
            currentWeapon = weapon;
            Animator animator = GetComponent<Animator>();
            weapon.Spawn(rightHandTransform, leftHandTransform, animator);
        }
        public Health GetTarget()
        {
            return target;
        }
        public object CaptureState()
        {
            return currentWeapon.name;
        }

        public void RestoreState(object state)
        {
            string weaponName = (string)state;
            Weapon weapon = UnityEngine.Resources.Load<Weapon>(weaponName);
            EquipWeapon(weapon);
        }

        public IEnumerable<float> GetAdditiveModifier(Stat stat)
        {
            if (stat == Stat.Damage)
            {
                yield return currentWeapon.GetWeaponDamage();
            }
        }
    }
}

