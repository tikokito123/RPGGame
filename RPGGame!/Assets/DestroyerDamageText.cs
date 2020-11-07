using UnityEngine;

public class DestroyerDamageText : MonoBehaviour
{
    [SerializeField] GameObject targetToDestroy = null;
    
    public void DestroyTarget()
    {
        Destroy(targetToDestroy);
    }
    

}
