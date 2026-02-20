using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    public Transform target;           // The car to follow
    public Vector3 offset = new Vector3(0, 3, -7); // Distance from car
    public float followSpeed = 10f;    // Smooth movement speed
    public float rotationSpeed = 5f;   // Smooth rotation speed

    void FixedUpdate()
    {
        if (!target) return;

        // Calculate desired position based on target rotation
        Vector3 desiredPosition = target.TransformPoint(offset);
        
        // Smoothly move the camera
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

        // Calculate rotation to look at the car
        Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
        
        // Smoothly rotate the camera
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
