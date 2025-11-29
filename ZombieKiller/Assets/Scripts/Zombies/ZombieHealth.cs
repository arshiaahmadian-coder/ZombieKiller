using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    [SerializeField] float maxHealth;
    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        // TODO: Damage dealt anim, sound
        if(currentHealth <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        // TODO: die sound
        Destroy(gameObject);
    }
}
