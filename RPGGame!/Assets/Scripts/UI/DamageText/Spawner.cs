using UnityEngine;

namespace RPG.UI.DamageText
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] DamageText damageTextPrefab = null;
        public void Spawn(float damageAmmount)
        {
            DamageText instance = Instantiate<DamageText>(damageTextPrefab, transform);
            instance.SetValue(damageAmmount);
        }
    }
}
