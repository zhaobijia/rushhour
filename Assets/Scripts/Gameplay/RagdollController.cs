using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody[] rigidbodies;
    Collider[] colliders;
    private void Awake()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        colliders = GetComponentsInChildren<Collider>();
        
    }

    public void TurnOffRagdoll()
    {
        foreach(Collider col in colliders)
        {
            col.enabled = false;
        }

        foreach(Rigidbody rb in rigidbodies)
        {
            rb.detectCollisions = false;
            rb.isKinematic = true;
        }
    }
    public void TurnOnRagdoll()
    {

    }
}
