using UnityEngine;
using Mirror;

public class CarCollisionHandler : NetworkBehaviour
{
    public float speedReductionFactor = 0.5f; // Reduce speed to 50% on crash
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        // Only trigger for the local player and on hard impacts
        if (!isLocalPlayer) return;

        if (collision.relativeVelocity.magnitude > 5f)
        {
            // Reduce velocity
            rb.velocity *= speedReductionFactor;

            // Trigger Camera Shake
            CameraShake.Instance.Shake(0.2f, 0.5f);
            
            Debug.Log("Collision Detected! Speed Reduced.");
        }
    }
}
