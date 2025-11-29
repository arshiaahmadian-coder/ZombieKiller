using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float maxHealth;
    [SerializeField] TMP_Text healthText;
    private float currentHealth;

    public static PlayerHealth instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        currentHealth = maxHealth;
        healthText.text = currentHealth.ToString();
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        healthText.text = currentHealth.ToString();
        // TODO: play hurt sound
        if(currentHealth <= 0f)
        {
            // TODO: Save Kill record, show menu;
            GameManager.instance.GameOver();
        }
    }
}
