using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] AnimatorOverrideController animatorOverride = null;
        [SerializeField] GameObject EquippedPrefab = null;
        [SerializeField] float damage = 5f;
        [SerializeField] float weaponRange = 2f;

        public float GetWeaponDamage()
        {
            return damage;
        }
        public float GetWeaponRange()
        {
            return weaponRange;
        }
        public void Spawn(Transform handTransform, Animator animator)
        {
            if (EquippedPrefab != null)
            {
                Instantiate(EquippedPrefab, handTransform);
            }
            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
        }

    }
}