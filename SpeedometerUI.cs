using UnityEngine;
using TMPro; // Use TextMeshPro for better quality
using UnityEngine.UI;

public class SpeedometerUI : MonoBehaviour
{
    public Rigidbody carRigidbody; // Reference to the car's Rigidbody
    public TextMeshProUGUI speedText; // UI Text to display speed
    public RectTransform needle; // Optional: Needle for visual gauge

    public float maxSpeed = 200f; // Max speed for the gauge
    private float currentSpeed;

    void Update()
    {
        // Calculate speed in KM/H (Magnitude is m/s, multiply by 3.6 for km/h)
        currentSpeed = carRigidbody.velocity.magnitude * 3.6f;

        // Update Text
        if (speedText != null)
        {
            speedText.text = Mathf.Round(currentSpeed).ToString() + " KM/H";
        }

        // Update Needle Rotation (Optional visual effect)
        if (needle != null)
        {
            // Rotate needle between 0 and -180 degrees based on speed
            float speedFraction = currentSpeed / maxSpeed;
            float needleAngle = Mathf.Lerp(0, -180, speedFraction);
            needle.localRotation = Quaternion.Euler(0, 0, needleAngle);
        }
    }
}
