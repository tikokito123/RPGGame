using UnityEngine;
using RPG.Core;

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
            if (EquippedPrefab != null)
            {
                Transform handTransform = GetTransform(rightHand, leftHand);
                Instantiate(EquippedPrefab, handTransform);
            }
            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
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