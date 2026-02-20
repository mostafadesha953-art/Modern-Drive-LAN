using UnityEngine;
using Mirror;

public class CarGameController : NetworkBehaviour
{
    [Header("Movement Settings")]
    public float motorForce = 1500f;
    public float breakForce = 3000f;
    public float maxSteerAngle = 30f;

    [Header("Wheel Colliders")]
    public WheelCollider frontLeft;
    public WheelCollider frontRight;
    public WheelCollider rearLeft;
    public WheelCollider rearRight;

    [Header("AI Settings")]
    public bool isAI = false;
    public Transform[] waypoints;
    private int currentWaypointIndex = 0;

    void FixedUpdate()
    {
        // Check if this is the local player or the Server running AI
        if (!isLocalPlayer && !isAI) return;

        if (isAI)
        {
            HandleAI();
        }
        else
        {
            HandleManualInput();
        }
    }

    void HandleManualInput()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        bool breaking = Input.GetKey(KeyCode.Space);

        ApplyMovement(h, v, breaking);
    }

    void HandleAI()
    {
        if (waypoints.Length == 0) return;

        Vector3 targetPos = waypoints[currentWaypointIndex].position;
        Vector3 relativePos = transform.InverseTransformPoint(targetPos);
        
        float steer = (relativePos.x / relativePos.magnitude);
        float drive = 1.0f; // AI always drives forward

        ApplyMovement(steer, drive, false);

        if (Vector3.Distance(transform.position, targetPos) < 5f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }

    void ApplyMovement(float steer, float motor, bool breaking)
    {
        frontLeft.steerAngle = steer * maxSteerAngle;
        frontRight.steerAngle = steer * maxSteerAngle;

        float torque = motor * motorForce;
        frontLeft.motorTorque = torque;
        frontRight.motorTorque = torque;

        float bForce = breaking ? breakForce : 0f;
        frontLeft.brakeTorque = bForce;
        frontRight.brakeTorque = bForce;
        rearLeft.brakeTorque = bForce;
        rearRight.brakeTorque = bForce;
    }
}

// Inside CarGameController.cs under OnStartLocalPlayer()
public override void OnStartLocalPlayer()
{
    Camera.main.GetComponent<SmoothCameraFollow>().target = this.transform;
}
