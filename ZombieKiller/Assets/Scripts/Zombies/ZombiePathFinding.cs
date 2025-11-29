using UnityEngine;
using UnityEngine.AI;

public class ZombiePathFinding : MonoBehaviour
{
    private Transform target;
    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = FindAnyObjectByType<PlayerMovement>().transform;
    }

    private void Update()
    {
        agent.SetDestination(target.position);
    }
}
