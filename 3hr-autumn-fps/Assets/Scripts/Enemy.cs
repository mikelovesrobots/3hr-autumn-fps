using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public void Die(Vector3 knockbackVector)
    {
        // Set the enemy tag to "Untagged" so that the player can't shoot it anymore
        gameObject.tag = "Untagged";

        // Disable nav mesh agent
        GetComponent<NavMeshAgent>().enabled = false;

        // Ragdoll the enemy and shoot them backwards
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = false;
            rb.AddForce(knockbackVector * 10, ForceMode.Impulse);
        }
    }
}
