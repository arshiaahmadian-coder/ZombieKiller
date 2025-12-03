using UnityEngine;

public class RagdollManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private Rigidbody[] rigidbodies;
    private Collider[] ragdollColliders;
    [SerializeField] private Collider mainCollider;

    [Header("dead settings")]
    [SerializeField] private string ragdollLayerName = "Ragdoll";
    [SerializeField] private bool disableMainCollider = true;

    void Start()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        ragdollColliders = GetComponentsInChildren<Collider>();

        SetKinematic(true);
    }

    public void EnableRagdoll()
    {
        if (animator != null)
            animator.enabled = false;

        SetKinematic(false);

        if (disableMainCollider && mainCollider != null)
            mainCollider.enabled = false;

        int ragdollLayer = LayerMask.NameToLayer(ragdollLayerName);
        if (ragdollLayer == -1)
        {
            return;
        }

        foreach (Collider col in ragdollColliders)
        {
            if (col == mainCollider) continue;

            col.gameObject.layer = ragdollLayer;
        }
    }

    public void SetKinematic(bool kinematic)
    {
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = kinematic;
            rb.useGravity = !kinematic;
        }
    }

    public void ApplyForceAtHit(Vector3 hitPoint, Vector3 forceDirection, float forceAmount = 10f)
    {
        Rigidbody hitRb = GetClosestRigidbody(hitPoint);
        if (hitRb != null)
            hitRb.AddForce(forceDirection * forceAmount, ForceMode.Impulse);
    }

    private Rigidbody GetClosestRigidbody(Vector3 point)
    {
        Rigidbody closest = null;
        float closestDist = float.MaxValue;
        foreach (Rigidbody rb in rigidbodies)
        {
            float dist = Vector3.Distance(rb.worldCenterOfMass, point);
            if (dist < closestDist)
            {
                closestDist = dist;
                closest = rb;
            }
        }
        return closest;
    }
}