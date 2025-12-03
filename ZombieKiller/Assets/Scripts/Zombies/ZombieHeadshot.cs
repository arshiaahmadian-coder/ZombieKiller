using UnityEngine;

public class ZombieHeadshot : MonoBehaviour
{
    public ZombieHealth zombieHealth;
    
    public void TakeHeadshotDamage(float damageAmount, Vector3 hitPoint, Vector3 forceDir)
    {
        zombieHealth.TakeDamage(damageAmount * 2, hitPoint, forceDir);
    }
}
