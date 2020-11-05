using UnityEngine;

namespace RPG.UI.DamageText
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] DamageText damageTextPrefab = null;
        void Start()
        {
            Spawn(5f);
        }
        public void Spawn(float damageAmmount)
        {
            DamageText instance = Instantiate<DamageText>(damageTextPrefab, transform);
        }
    }
}
