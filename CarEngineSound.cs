using UnityEngine;

public class CarEngineSound : MonoBehaviour
{
    public AudioSource engineAudioSource;
    public Rigidbody carRigidbody;

    public float minPitch = 1.0f;
    public float maxPitch = 3.0f;
    private float currentSpeed;

    void Start()
    {
        if (engineAudioSource != null)
        {
            engineAudioSource.loop = true;
            engineAudioSource.Play();
        }
    }

    void Update()
    {
        // Calculate speed in KM/H
        currentSpeed = carRigidbody.velocity.magnitude * 3.6f;

        // Change pitch based on speed (Max speed assumed 200 KM/H)
        float pitchMultiplier = currentSpeed / 200f;
        engineAudioSource.pitch = Mathf.Lerp(minPitch, maxPitch, pitchMultiplier);
    }
}
