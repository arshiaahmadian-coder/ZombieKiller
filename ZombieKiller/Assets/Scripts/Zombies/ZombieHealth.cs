using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieHealth : MonoBehaviour
{
    [SerializeField] float maxHealth;
    [SerializeField] int killScore;
    [SerializeField] private RagdollManager ragdollManager;
    private float currentHealth;
    public List<GameObject> attachedObjects;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damageAmount, Vector3 hitPoint, Vector3 forceDir)
    {
        currentHealth -= damageAmount;
        // TODO: Damage dealt anim, sound
        if(currentHealth <= 0f)
        {
            Die(hitPoint, forceDir);
        }
    }

    void Die(Vector3 hitPoint, Vector3 forceDir)
    {
        print("Died");
        GetComponent<ZombiePathFinding>().StopMoving();
        Destroy(GetComponent<CapsuleCollider>());
        Destroy(GetComponent<NavMeshAgent>());

        foreach(GameObject _object in attachedObjects)
        {
            Destroy(_object);
        }

        ragdollManager.EnableRagdoll();
        ragdollManager.ApplyForceAtHit(hitPoint, forceDir);

        GameManager.instance.AddCoin(killScore);

        Invoke(nameof(CallFindZombies), 3f);
    }

    private void CallFindZombies()
    {
        FindFirstObjectByType<ZombieSpawner>().FindAllAliveZombies();
        Destroy(gameObject);
    }

    public void AddObjectToList(GameObject _object)
    {
        attachedObjects.Add(_object);
    }
}
