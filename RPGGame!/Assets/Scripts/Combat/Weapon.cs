using UnityEngine;
using RPG.Core;
using System;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] AnimatorOverrideController animatorOverride = null;
        [SerializeField] GameObject EquippedPrefab = null;
        [SerializeField] float damage = 5f;
        [SerializeField] float weaponRange = 2f;
        [SerializeField] bool isRightHanded = true;
        [SerializeField] Projectile projectile = null;

        const string weaponName = "Weapon";
        public float GetWeaponDamage()
        {
            return damage;
        }
        public float GetWeaponRange()
        {
            return weaponRange;
        }
        public bool HasProjectile()
        {
            return projectile != null;
        }

        public void LunchProjectile(Health target, Transform rightHand, Transform leftHand)
        {
            Projectile projectileInstance = Instantiate(projectile,GetTransform(rightHand, leftHand).position, Quaternion.identity);
            projectileInstance.SetTarget(target, damage);
        }
        public void Spawn(Transform rightHand,Transform leftHand, Animator animator)
        {
            DestroyOldWeapon(rightHand, leftHand);
            if (EquippedPrefab != null)
            {
                Transform handTransform = GetTransform(rightHand, leftHand);
                GameObject weapon = Instantiate(EquippedPrefab, handTransform);
                weapon.name = weaponName;
            }
            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
        }

        private void DestroyOldWeapon(Transform rightHand, Transform leftHand)
        {
            Transform oldWeapon = rightHand.Find(weaponName);
            if (oldWeapon == null)
            {
                oldWeapon = leftHand.Find(weaponName);
            }
            if (oldWeapon == null) return;

            oldWeapon.name = "DESTROYING";
            Destroy(oldWeapon.gameObject);
        }

        private Transform GetTransform(Transform rightHand, Transform leftHand)
        {
            Transform handTransform;
            if (isRightHanded) handTransform = rightHand;
            else handTransform = leftHand;
            return handTransform;
        }
    }
}