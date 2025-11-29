using UnityEngine;

public class ZombieHeadshot : MonoBehaviour
{
    public ZombieHealth zombieHealth;
    
    public void TakeHeadshotDamage(float damageAmount)
    {
        zombieHealth.TakeDamage(damageAmount * 2);
    }
}
