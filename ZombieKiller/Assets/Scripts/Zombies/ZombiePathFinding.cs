using UnityEngine;
using UnityEngine.AI;

public class ZombiePathFinding : MonoBehaviour
{
    [Header("Attack")]
    [SerializeField] float AttackRange;
    [SerializeField] float AttackDamage;
    [SerializeField] float baseAttackTime;

    private bool readyToAttack = true;
    private Transform target;
    private NavMeshAgent agent;
    private Animator animator;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = FindFirstObjectByType<PlayerMovement>().transform;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!agent.isStopped) agent.SetDestination(target.position);
        float remainingDist = agent.remainingDistance;

        // Attack
        if(readyToAttack && remainingDist <= AttackRange) Attack();
    }

    private void Attack()
    {
        // deal damage
        // trigger animation
        animator.SetTrigger("Attack");
        // reset flags
        readyToAttack = false;
        Invoke(nameof(ResetAttackTime), baseAttackTime);
        Invoke(nameof(DealDamage), baseAttackTime * 0.35f);
    }

    private void ResetAttackTime()
    {
        readyToAttack = true;
    }

    private void DealDamage()
    {
        float remainingDist = agent.remainingDistance;
        if(remainingDist <= AttackRange)
        {
            // decrease player health
            PlayerHealth.instance.TakeDamage(AttackDamage);
        }
    }
}
